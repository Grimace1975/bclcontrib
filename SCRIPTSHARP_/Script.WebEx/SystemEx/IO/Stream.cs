using System;
using System.Runtime.CompilerServices;
namespace SystemEx.IO
{
    public abstract class Stream : IDisposable
    {
        public abstract int ReadByte();

        [AlternateSignature]
        public extern int Read(byte[] b);
        public int Read(byte[] b, int offset, int length)
        {
            if ((offset == 0) && (length == 0))
                length = b.Length;
            int end = offset + length;
            for (int index = offset; index < end; index++)
            {
                int r = ReadByte();
                if (r == -1)
                    return (index == offset ? -1 : index - offset);
                b[index] = (byte)r;
            }
            return length;
        }

        public abstract void WriteByte(int b);

        [AlternateSignature]
        public extern void Write(byte[] b);
        public void Write(byte[] b, int offset, int length)
        {
            if ((offset == 0) && (length == 0))
                length = b.Length;
            int end = offset + length;
            for (int index = offset; index < end; index++)
                WriteByte(b[index]);
        }

        public abstract void Close();
        public abstract void Flush();
        public abstract long Position { get; set; }
        public abstract long Length { get; }

        void IDisposable.Dispose()
        {
            Close();
        }

        public abstract void SetLength(long value);
    }
}
