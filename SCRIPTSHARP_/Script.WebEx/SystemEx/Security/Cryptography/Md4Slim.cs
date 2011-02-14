#if !CODE_ANALYSIS
using System.IO;
namespace System.Security.Cryptography
#else
using System;
using SystemEx.IO;
using System.Runtime.CompilerServices;
namespace SystemEx.Security.Cryptography
#endif
{
    public class Md4Slim
#if !CODE_ANALYSIS
        : ICloneable
#endif
    {
        // The size in bytes of the input block to the tranformation algorithm.
        private static readonly int BLOCK_LENGTH = 64; //    = 512 / 8;
        // 4 32-bit words (interim result)
        private uint[] application = new uint[4];
        // Number of bytes processed so far mod. 2 power of 64.
        private long count;
        // 512 bits input buffer = 16 x 32-bit words holds until reaches 512 bits.
        private byte[] buffer = new byte[BLOCK_LENGTH];

        // 512 bits work buffer = 16 x 32-bit words
        private uint[] X = new uint[16];

        public Md4Slim()
        {
            Reset();
        }

#if !CODE_ANALYSIS
        // Returns a copy of this MD object.
        public object Clone()
        {
            Md4Slim md4 = new Md4Slim();
            //    This constructor is here to implement cloneability of this class.
            application = new uint[md4.application.Length];
            JSArrayEx.Copy(md4.application, 0, application, 0, application.Length);
            buffer = new byte[md4.buffer.Length];
            JSArrayEx.Copy(md4.buffer, 0, buffer, 0, buffer.Length);
            count = md4.count;
            return md4;
        }
#endif

        // Resets this object disregarding any temporary data present at the time of the invocation of this call.
        public void Reset()
        {
            // initial values of MD4 i.e. A, B, C, D as per rfc-1320; they are low-order byte first
            application[0] = 0x67452301;
            application[1] = 0xEFCDAB89;
            application[2] = 0x98BADCFE;
            application[3] = 0x10325476;
            count = 0L;
            for (int i = 0; i < BLOCK_LENGTH; i++)
                buffer[i] = 0;
        }

        // Continues an MD4 message digest using the input byte.
        public void BeginUpdate(byte b)
        {
            // compute number of bytes still unhashed; ie. present in buffer
            int i = (int)(count % BLOCK_LENGTH);
            count++; // update number of bytes
            buffer[i] = b;
            if (i == BLOCK_LENGTH - 1)
                Transform(buffer, 0);
        }

        // MD4 block update operation.
        // Continues an MD4 message digest operation, by filling the buffer, transform(ing) data in 512-bit message block(s), updating the variables
        // application and count, and leaving (buffering) the remaining bytes in buffer for the next update or finish.
        //
        // @param    input    input block
        // @param    offset    start of meaningful bytes in input
        // @param    len        count of bytes in input block to consider
        public void Update(byte[] input, int offset, int len)
        {
            // make sure we don't exceed input's allocated size/length
            if (offset < 0 || len < 0 || (long)offset + len > input.Length)
                throw new Exception("IndexOutOfRangeException:");
            // compute number of bytes still unhashed; ie. present in buffer
            int bufferNdx = (int)(count % BLOCK_LENGTH);
            count += len; // update number of bytes
            int partLen = BLOCK_LENGTH - bufferNdx;
            int i = 0;
            if (len >= partLen)
            {
                JSArrayEx.Copy(input, offset, buffer, bufferNdx, partLen);
                Transform(buffer, 0);
                for (i = partLen; i + BLOCK_LENGTH - 1 < len; i += BLOCK_LENGTH)
                    Transform(input, offset + i);
                bufferNdx = 0;
            }
            // buffer remaining input
            if (i < len)
                JSArrayEx.Copy(input, offset + i, buffer, bufferNdx, len - i);
        }

        // Completes the hash computation by performing final operations such  as padding. At the return of this engineDigest, the MD engine is reset.
        // @return the array of bytes for the resulting hash value.
        public byte[] GetDigest()
        {
            // pad output to 56 mod 64; as RFC1320 puts it: congruent to 448 mod 512
            int bufferNdx = (int)(count % BLOCK_LENGTH);
            int padLen = (bufferNdx < 56) ? (56 - bufferNdx) : (120 - bufferNdx);
            // padding is alwas binary 1 followed by binary 0s
            byte[] tail = new byte[padLen + 8];
            tail[0] = (byte)0x80;
            // append length before final transform:
            // save number of bits, casting the long to an array of 8 bytes
            // save low-order byte first.
            for (int i = 0; i < 8; i++)
                tail[padLen + i] = (byte)((count * 8) >> (8 * i));
            Update(tail, 0, tail.Length);
            byte[] result = new byte[16];
            // cast this MD4's application (array of 4 ints) into an array of 16 bytes.
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    result[i * 4 + j] = (byte)(application[i] >> (8 * j));
            // reset the engine
            Reset();
            return result;
        }

        //    MD4 basic transformation.
        //    Transforms application based on 512 bits from input block starting from the offset'th byte.
        //    @param    block    input sub-array.
        //    @param    offset    starting position of sub-array.
        private void Transform(byte[] block, int offset)
        {
            // encodes 64 bytes from input block into an array of 16 32-bit entities. Use A as a temp var.
            for (int i = 0; i < 16; i++)
                X[i] = (uint)((block[offset++] & 0xFF) | ((block[offset++] & 0xFF) << 8) | ((block[offset++] & 0xFF) << 16) | ((block[offset++] & 0xFF) << 24));
            uint A = application[0];
            uint B = application[1];
            uint C = application[2];
            uint D = application[3];
            A = FF(A, B, C, D, X[0], 3);
            D = FF(D, A, B, C, X[1], 7);
            C = FF(C, D, A, B, X[2], 11);
            B = FF(B, C, D, A, X[3], 19);
            A = FF(A, B, C, D, X[4], 3);
            D = FF(D, A, B, C, X[5], 7);
            C = FF(C, D, A, B, X[6], 11);
            B = FF(B, C, D, A, X[7], 19);
            A = FF(A, B, C, D, X[8], 3);
            D = FF(D, A, B, C, X[9], 7);
            C = FF(C, D, A, B, X[10], 11);
            B = FF(B, C, D, A, X[11], 19);
            A = FF(A, B, C, D, X[12], 3);
            D = FF(D, A, B, C, X[13], 7);
            C = FF(C, D, A, B, X[14], 11);
            B = FF(B, C, D, A, X[15], 19);
            //
            A = GG(A, B, C, D, X[0], 3);
            D = GG(D, A, B, C, X[4], 5);
            C = GG(C, D, A, B, X[8], 9);
            B = GG(B, C, D, A, X[12], 13);
            A = GG(A, B, C, D, X[1], 3);
            D = GG(D, A, B, C, X[5], 5);
            C = GG(C, D, A, B, X[9], 9);
            B = GG(B, C, D, A, X[13], 13);
            A = GG(A, B, C, D, X[2], 3);
            D = GG(D, A, B, C, X[6], 5);
            C = GG(C, D, A, B, X[10], 9);
            B = GG(B, C, D, A, X[14], 13);
            A = GG(A, B, C, D, X[3], 3);
            D = GG(D, A, B, C, X[7], 5);
            C = GG(C, D, A, B, X[11], 9);
            B = GG(B, C, D, A, X[15], 13);
            //
            A = HH(A, B, C, D, X[0], 3);
            D = HH(D, A, B, C, X[8], 9);
            C = HH(C, D, A, B, X[4], 11);
            B = HH(B, C, D, A, X[12], 15);
            A = HH(A, B, C, D, X[2], 3);
            D = HH(D, A, B, C, X[10], 9);
            C = HH(C, D, A, B, X[6], 11);
            B = HH(B, C, D, A, X[14], 15);
            A = HH(A, B, C, D, X[1], 3);
            D = HH(D, A, B, C, X[9], 9);
            C = HH(C, D, A, B, X[5], 11);
            B = HH(B, C, D, A, X[13], 15);
            A = HH(A, B, C, D, X[3], 3);
            D = HH(D, A, B, C, X[11], 9);
            C = HH(C, D, A, B, X[7], 11);
            B = HH(B, C, D, A, X[15], 15);
            //
            application[0] += A;
            application[1] += B;
            application[2] += C;
            application[3] += D;
        }

        // The basic MD4 atomic functions.
        private uint FF(uint a, uint b, uint c, uint d, uint x, int s)
        {
            uint t = a + ((b & c) | (~b & d)) + x;
            return t << s | t >> (32 - s);
        }

        private uint GG(uint a, uint b, uint c, uint d, uint x, int s)
        {
            uint t = a + ((b & (c | d)) | (c & d)) + x + 0x5A827999;
            return t << s | t >> (32 - s);
        }

        private uint HH(uint a, uint b, uint c, uint d, uint x, int s)
        {
            uint t = a + (b ^ c ^ d) + x + 0x6ED9EBA1;
            return t << s | t >> (32 - s);
        }

        // Bugfixed, now works prima (RST).
        public static int Com_BlockChecksum(byte[] buffer, int length)
        {
            int val;
            Md4Slim md4 = new Md4Slim();
            md4.Update(buffer, 0, length);
            byte[] data = md4.GetDigest();
            MemoryStream b = new MemoryStream(data);
            val = SE.ReadInt32(b) ^ SE.ReadInt32(b) ^ SE.ReadInt32(b) ^ SE.ReadInt32(b);
            return val;
        }
    }
}
