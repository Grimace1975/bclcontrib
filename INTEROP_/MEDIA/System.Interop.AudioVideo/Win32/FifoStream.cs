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
using System.IO;
using System.Collections;
namespace System.Interopt.AudioVideo.Win32
{
    /// <summary>
    /// Fifo Stream
    /// </summary>
    public class FifoStream : Stream
    {
        private const int BlockSize = 65536;
        private const int MaxBlocksInCache = (3 * 1024 * 1024) / BlockSize;
        private int _size;
        private int _readPosition;
        private int _writePosition;
        private Stack _usedBlocks = new Stack();
        private ArrayList _blocks = new ArrayList();

        /// <summary>
        /// Allocs the block.
        /// </summary>
        /// <returns></returns>
        private byte[] AllocBlock()
        {
            return (_usedBlocks.Count > 0 ? (byte[])_usedBlocks.Pop() : new byte[BlockSize]);
        }

        /// <summary>
        /// Frees the block.
        /// </summary>
        /// <param name="block">The block.</param>
        private void FreeBlock(byte[] block)
        {
            if (_usedBlocks.Count < MaxBlocksInCache)
            {
                _usedBlocks.Push(block);
            }
        }

        /// <summary>
        /// Gets the write block.
        /// </summary>
        /// <returns></returns>
        private byte[] GetWriteBlock()
        {
            byte[] result;
            if ((_writePosition < BlockSize) && (_blocks.Count > 0))
            {
                result = (byte[])_blocks[_blocks.Count - 1];
            }
            else
            {
                result = AllocBlock();
                _blocks.Add(result);
                _writePosition = 0;
            }
            return result;
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
            get { return false; }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <value></value>
        /// <returns>true if the stream supports writing; otherwise, false.</returns>
        public override bool CanWrite
        {
            get { return true; }
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
            get
            {
                lock (this)
                {
                    return _size;
                }
            }
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
            get { throw new InvalidOperationException(); }
            set { throw new InvalidOperationException(); }
        }

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
        /// </summary>
        public override void Close()
        {
            Flush();
        }

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        public override void Flush()
        {
            lock (this)
            {
                foreach (byte[] block in _blocks)
                {
                    FreeBlock(block);
                }
                _blocks.Clear();
                _readPosition = 0;
                _writePosition = 0;
                _size = 0;
            }
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
            throw new InvalidOperationException();
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
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException();
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
            lock (this)
            {
                int value = Peek(buffer, offset, count);
                Advance(value);
                return value;
            }
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
            lock (this)
            {
                int index = count;
                while (index > 0)
                {
                    int writeLength = Math.Min(BlockSize - _writePosition, index);
                    Array.Copy(buffer, offset + count - index, GetWriteBlock(), _writePosition, writeLength);
                    _writePosition += writeLength;
                    index -= writeLength;
                }
                _size += count;
            }
        }

        /// <summary>
        /// Advances the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public int Advance(int count)
        {
            lock (this)
            {
                int sizeLeft = count;
                while ((sizeLeft > 0) && (_size > 0))
                {
                    if (_readPosition == BlockSize)
                    {
                        _readPosition = 0;
                        FreeBlock((byte[])_blocks[0]);
                        _blocks.RemoveAt(0);
                    }
                    int toFeed = (_blocks.Count == 1 ? Math.Min(_writePosition - _readPosition, sizeLeft) : Math.Min(BlockSize - _readPosition, sizeLeft));
                    _readPosition += toFeed;
                    sizeLeft -= toFeed;
                    _size -= toFeed;
                }
                return count - sizeLeft;
            }
        }

        /// <summary>
        /// Peeks the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public int Peek(byte[] buffer, int offset, int count)
        {
            lock (this)
            {
                int sizeLeft = count;
                int tempBlockPosition = _readPosition;
                int tempSize = _size;
                //+
                int currentBlock = 0;
                while ((sizeLeft > 0) && (tempSize > 0))
                {
                    if (tempBlockPosition == BlockSize)
                    {
                        tempBlockPosition = 0;
                        currentBlock++;
                    }
                    int upper = (currentBlock < _blocks.Count - 1 ? BlockSize : _writePosition);
                    int toFeed = Math.Min(upper - tempBlockPosition, sizeLeft);
                    System.Array.Copy((byte[])_blocks[currentBlock], tempBlockPosition, buffer, offset + count - sizeLeft, toFeed);
                    sizeLeft -= toFeed;
                    tempBlockPosition += toFeed;
                    tempSize -= toFeed;
                }
                return count - sizeLeft;
            }
        }
    }
}
