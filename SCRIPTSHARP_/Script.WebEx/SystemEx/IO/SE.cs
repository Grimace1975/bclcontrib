#if CODE_ANALYSIS
using System;
using System.Runtime.CompilerServices;
namespace SystemEx.IO
#else
using System.Text;
namespace System.IO
#endif
{
    public static class SE
    {
        #region Read

        public static int InternalReadByte(Stream s)
        {
            int v = s.ReadByte();
            if (v == -1)
                throw new Exception("IOException: EOF");
            return v;
        }

        private static int InternalReadInt16(Stream s)
        {
            int a = s.ReadByte();
            int b = InternalReadByte(s);
            return ((a << 8) | b);
        }

        public static byte ReadByte(Stream s)
        {
            int v = s.ReadByte();
            if (v == -1)
                throw new Exception("IOException: EOF");
            return (byte)v;
        }

#if CODE_ANALYSIS
        [AlternateSignature]
        public static extern void ReadBytes(Stream s, byte[] b);
#else
        public static void ReadBytes(Stream s, byte[] b) { ReadBytes(s, b, 0, 0); }
#endif
        public static void ReadBytes(Stream s, byte[] b, int offset, int length)
        {
            if ((offset == 0) && (length == 0))
                length = b.Length;
            while (length > 0)
            {
                int count = s.Read(b, offset, length);
                if (count <= 0)
                    throw new Exception("IOException: EOF");
                offset += count;
                length -= count;
            }
        }

        public static bool ReadBoolean(Stream s)
        {
            return (ReadByte(s) != 0);
        }

        public static char ReadChar(Stream s)
        {
            int a = s.ReadByte();
            int b = InternalReadByte(s);
            return (char)((a << 8) | b);
        }

        public static short ReadInt16(Stream s)
        {
            int a = s.ReadByte();
            int b = InternalReadByte(s);
            return (short)((a << 8) | b);
        }

        public static int ReadInt32(Stream s)
        {
            int a = s.ReadByte();
            int b = s.ReadByte();
            int c = s.ReadByte();
            int d = InternalReadByte(s);
            return (a << 24) | (b << 16) | (c << 8) | d;
        }

        public static long ReadInt64(Stream s)
        {
            long a = ReadInt32(s);
            long b = ReadInt32(s) & 0x0ffffffff;
            return (a << 32) | b;
        }

        public static float ReadSingle(Stream s)
        {
            return JSConvert.Int32BitsToSingle(ReadInt32(s));
        }

        public static double ReadDouble(Stream s)
        {
            throw new Exception("NotSupportedException: readDouble");
        }

        public static string ReadString(Stream s)
        {
            int bytes = InternalReadInt16(s);
            StringBuilder b = new StringBuilder();
            while (bytes > 0)
                bytes -= ReadUtfChar(s, b);
            return b.ToString();
        }

        public static string ReadStringLine(Stream s)
        {
            throw new Exception("NotSupportedException: ReadLine");
        }

        private static int ReadUtfChar(Stream s, StringBuilder sb)
        {
            int a = InternalReadByte(s);
            if ((a & 0x80) == 0)
            {
                sb.Append((char)a);
                return 1;
            }
            if ((a & 0xe0) == 0xb0)
            {
                int b = InternalReadByte(s);
                sb.Append((char)(((a & 0x1F) << 6) | (b & 0x3F)));
                return 2;
            }
            if ((a & 0xf0) == 0xe0)
            {
                int b = s.ReadByte();
                int c = InternalReadByte(s);
                sb.Append((char)(((a & 0x0F) << 12) | ((b & 0x3F) << 6) | (c & 0x3F)));
                return 3;
            }
            throw new Exception("IOException: UTFDataFormat:");
        }

        //public static int SkipBytes(Stream s, int n)
        //{
        //    // note: This is actually a valid implementation of this method, rendering it quite useless...
        //    return 0;
        //}

        #endregion

        #region Write

        public static void WriteByte(Stream s, int v)
        {
            s.WriteByte((byte)v);
        }

        public static void WriteBytes(Stream s, string v)
        {
            int length = v.Length;
            for (int index = 0; index < length; index++)
#if CODE_ANALYSIS
                s.WriteByte(v.CharAt(index) & 0xff);
#else
                s.WriteByte((byte)(v[index] & 0xff));
#endif
        }

        public static void WriteBoolean(Stream s, bool v)
        {
            s.WriteByte((byte)(v ? 1 : 0));
        }

        public static void WriteChar(Stream s, int v)
        {
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)v);
        }

        public static void WriteInt16(Stream s, int v)
        {
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)v);
        }

        public static void WriteInt32(Stream s, int v)
        {
            s.WriteByte((byte)(v >> 24));
            s.WriteByte((byte)(v >> 16));
            s.WriteByte((byte)(v >> 8));
            s.WriteByte((byte)v);
        }

        public static void WriteInt64(Stream s, long v)
        {
            WriteInt32(s, (int)(v >> 32));
            WriteInt32(s, (int)v);
        }

        public static void WriteSingle(Stream s, float v)
        {
            WriteInt32(s, JSConvert.SingleToInt32Bits(v));
        }

        public static void WriteDouble(Stream s, double v)
        {
            throw new Exception("NotSupportedException: writeDouble");
        }

        public static void WriteString(Stream s, string v)
        {
            MemoryStream baos = new MemoryStream();
            for (int index = 0; index < v.Length; index++)
            {
#if CODE_ANALYSIS
                char c = v.CharAt(index);
#else
                char c = v[index];
#endif
                if ((c > 0) && (c < 80))
                    baos.WriteByte((byte)c);
                else if (c < '\u0800')
                {
                    baos.WriteByte((byte)(0xc0 | (0x1f & (c >> 6))));
                    baos.WriteByte((byte)(0x80 | (0x3f & c)));
                }
                else
                {
                    baos.WriteByte((byte)(0xe0 | (0x0f & (c >> 12))));
                    baos.WriteByte((byte)(0x80 | (0x3f & (c >> 6))));
                    baos.WriteByte((byte)(0x80 | (0x3f & c)));
                }
            }
            WriteInt16(s, (int)baos.Length);
            s.Write(baos.GetBuffer(), 0, (int)baos.Length);
        }

        #endregion
    }
}
