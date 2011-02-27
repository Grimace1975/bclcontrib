using System.Runtime.CompilerServices;
namespace SystemEx.IO
{
    public class MemoryStream : Stream
    {
        private long _length;
        private byte[] _buffer;
        private long _position;

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
            byte[] result = new byte[_length];
            JSArrayEx.Copy(_buffer, 0, result, 0, (int)_length);
            return result;
        }

        public override long Length
        {
            get { return _length; }
        }

        public override long Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public override int ReadByte()
        {
            return (_position < _buffer.Length ? _buffer[_position++] : -1);
        }

        public override void WriteByte(int b)
        {
            if (_buffer.Length == _length)
            {
                byte[] newBuf = new byte[_buffer.Length * 3 / 2];
                JSArrayEx.Copy(_buffer, 0, newBuf, 0, (int)_length);
                _buffer = newBuf;
            }
            _buffer[_length++] = (byte)b;
        }

        public override void Close()
        {
            _buffer = null;
        }

        public override void Flush() { }

        public override void SetLength(long value)
        {
            if (_buffer.Length != value)
            {
                byte[] newBuf = new byte[value * 3 / 2];
                JSArrayEx.Copy(_buffer, 0, newBuf, 0, (int)_length);
                _buffer = newBuf;
                while (_length < value)
                    _buffer[_length++] = 0;
            }
            _length = value;
        }
    }
}
