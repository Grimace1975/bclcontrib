using System.Runtime.CompilerServices;
namespace SystemEx.IO
{
    public class MemoryStream : Stream
    {
        private int _count;
        private byte[] _buffer;
        private int _position;

        [AlternateSignature]
        public extern MemoryStream();
        public MemoryStream(byte[] buffer)
        {
            _buffer = (buffer == null ? MakeBuffer(16) : buffer);
        }

        [AlternateSignature]
        public extern static byte[] MakeBuffer();
        public static byte[] MakeBuffer(int initialSize)
        {
            return new byte[initialSize != 0 ? initialSize : 16];
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }

        public byte[] ToArray()
        {
            byte[] result = new byte[_count];
            JSArrayEx.Copy(_buffer, 0, result, 0, _count);
            return result;
        }

        public int Length
        {
            get { return _count; }
        }

        public int Position
        {
            get { return _position; }
        }

        public override int ReadByte()
        {
            return (_position < _buffer.Length ? _buffer[_position++] : -1);
        }

        public override void WriteByte(int b)
        {
            if (_buffer.Length == _count)
            {
                byte[] newBuf = new byte[_buffer.Length * 3 / 2];
                JSArrayEx.Copy(_buffer, 0, newBuf, 0, _count);
                _buffer = newBuf;
            }
            _buffer[_count++] = (byte)b;
        }

        public override void Close()
        {
            _buffer = null;
        }

        public override void Flush() { }
    }
}
