#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace System.Interop.AudioVideo.Win32
{
    /// <summary>
    /// Stream implementaion of Wave
    /// </summary>
    public class WaveStream : System.IO.Stream, System.IDisposable
    {
        private System.IO.Stream _parentStream;
        private long _dataIndex;
        private long _length;
        private Native_.WaveFormat _waveFormat;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveStream"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public WaveStream(string fileName)
            : this(new System.IO.FileStream(fileName, System.IO.FileMode.Open))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="WaveStream"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public WaveStream(System.IO.Stream stream)
        {
            _parentStream = stream;
            ReadHeader();
        }
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="WaveStream"/> is reclaimed by garbage collection.
        /// </summary>
        ~WaveStream()
        {
            if (!_disposed)
            {
                Dispose(false);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _disposed = true;
            if ((disposing) && (_parentStream != null))
            {
                _parentStream.Close();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        public Native_.WaveFormat Format
        {
            get { return _waveFormat; }
        }

        /// <summary>
        /// Reads the chunk.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private string ReadChunk(System.IO.BinaryReader r)
        {
            byte[] c = new byte[4];
            r.Read(c, 0, c.Length);
            return System.Text.Encoding.ASCII.GetString(c);
        }

        /// <summary>
        /// Reads the header.
        /// </summary>
        private void ReadHeader()
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(_parentStream);
            if (ReadChunk(reader) != "RIFF")
            {
                throw new System.Exception("Invalid file format");
            }
            //+ File length minus first 8 bytes of RIFF description, we don't use it
            reader.ReadInt32();
            if (ReadChunk(reader) != "WAVE")
            {
                throw new System.Exception("Invalid file format");
            }
            if (ReadChunk(reader) != "fmt ")
            {
                throw new System.Exception("Invalid file format");
            }
            int length = reader.ReadInt32();
            if (length < 16)
            {
                //+ bad format chunk length
                throw new System.Exception("Invalid file format");
            }
            //+ initialize to any format
            _waveFormat = new Native_.WaveFormat(22050, 16, 2);
            _waveFormat.wFormatTag = reader.ReadInt16();
            _waveFormat.nChannels = reader.ReadInt16();
            _waveFormat.nSamplesPerSec = reader.ReadInt32();
            _waveFormat.nAvgBytesPerSec = reader.ReadInt32();
            _waveFormat.nBlockAlign = reader.ReadInt16();
            _waveFormat.wBitsPerSample = reader.ReadInt16();
            //+ advance in the stream to skip the wave format block 
            //+ minimum format size
            length -= 16;
            while (length > 0)
            {
                reader.ReadByte();
                length--;
            }
            //+ assume the data chunk is aligned
            while ((_parentStream.Position < _parentStream.Length) && (ReadChunk(reader) != "data")) ;
            if (_parentStream.Position >= _parentStream.Length)
            {
                throw new System.Exception("Invalid file format");
            }
            _length = reader.ReadInt32();
            _dataIndex = _parentStream.Position;
            Position = 0;
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <value></value>
        /// <returns>true if the stream supports reading; otherwise, false.</returns>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <value></value>
        /// <returns>true if the stream supports seeking; otherwise, false.</returns>
        public override bool CanSeek
        {
            get { return true; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <value></value>
        /// <returns>true if the stream supports writing; otherwise, false.</returns>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <value></value>
        /// <returns>A long value representing the length of the stream in bytes.</returns>
        /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Length
        {
            get { return _length; }
        }

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        /// <value></value>
        /// <returns>The current position within the stream.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Position
        {
            get { return _parentStream.Position - _dataIndex; }
            set { Seek(value, System.IO.SeekOrigin.Begin); }
        }

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override void Flush()
        {
        }

        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override void SetLength(long value)
        {
            throw new System.InvalidOperationException();
        }

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>
        /// The new position within the current stream.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            switch (origin)
            {
                case System.IO.SeekOrigin.Begin:
                    _parentStream.Position = offset + _dataIndex;
                    break;
                case System.IO.SeekOrigin.Current:
                    _parentStream.Seek(offset, System.IO.SeekOrigin.Current);
                    break;
                case System.IO.SeekOrigin.End:
                    _parentStream.Position = _dataIndex + _length - offset;
                    break;
            }
            return this.Position;
        }

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length. </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="buffer"/> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="offset"/> or <paramref name="count"/> is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int readLength = (int)System.Math.Min(count, _length - Position);
            return _parentStream.Read(buffer, offset, readLength);
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is greater than the buffer length. </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="buffer"/> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="offset"/> or <paramref name="count"/> is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.InvalidOperationException();
        }
    }
}
