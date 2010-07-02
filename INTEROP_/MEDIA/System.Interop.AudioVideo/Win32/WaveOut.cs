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
using System.Runtime.InteropServices;
using System.Threading;
namespace System.Interopt.AudioVideo.Win32
{
    /// <summary>
    /// Wave Out
    /// </summary>
    public class WaveOut : System.IDisposable
    {
        private IntPtr _wave;
        private WaveOutBuffer _bufferList; //+ linked list
        private WaveOutBuffer _currentBuffer;
        private Thread _thread;
        private BufferFillEventHandler _fillDelegate;
        private bool _isComplete;
        private byte _isZero;
        private Native_.WindowsMultimedia.WaveDelegate _bufferDelegate = new Native_.WindowsMultimedia.WaveDelegate(WaveOutBuffer.WaveOutProc);

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveOut"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="format">The format.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="bufferCount">The buffer count.</param>
        /// <param name="fillProc">The fill proc.</param>
        public WaveOut(int device, Native_.WaveFormat format, int bufferSize, int bufferCount, BufferFillEventHandler fillProc)
        {
            _isZero = (format.wBitsPerSample == 8 ? (byte)128 : (byte)0);
            _fillDelegate = fillProc;
            Helper.Try(Native_.WindowsMultimedia.waveOutOpen(out _wave, device, format, _bufferDelegate, 0, Native_.WindowsMultimedia.CALLBACK_FUNCTION));
            AllocateBufferList(bufferSize, bufferCount);
            _thread = new Thread(new ThreadStart(Thread));
            _thread.Start();
        }
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="WaveOut"/> is reclaimed by garbage collection.
        /// </summary>
        ~WaveOut()
        {
            Dispose();
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_thread != null)
            {
                try
                {
                    _isComplete = true;
                    if (_wave != IntPtr.Zero)
                    {
                        Native_.WindowsMultimedia.waveOutReset(_wave);
                    }
                    _thread.Join();
                    _fillDelegate = null;
                    FreeBufferList();
                    if (_wave != IntPtr.Zero)
                    {
                        Native_.WindowsMultimedia.waveOutClose(_wave);
                    }
                }
                finally
                {
                    _thread = null;
                    _wave = IntPtr.Zero;
                }
            }
            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the device count.
        /// </summary>
        /// <value>The device count.</value>
        public static int DeviceCount
        {
            get { return Native_.WindowsMultimedia.waveOutGetNumDevs(); }
        }

        /// <summary>
        /// Threads this instance.
        /// </summary>
        private void Thread()
        {
            while (!_isComplete)
            {
                Advance();
                if ((_fillDelegate != null) && (!_isComplete))
                {
                    _fillDelegate(_currentBuffer.Data, _currentBuffer.Size);
                }
                else
                {
                    //+ zero out buffer
                    byte v = _isZero;
                    byte[] b = new byte[_currentBuffer.Size];
                    for (int i = 0; i < b.Length; i++)
                    {
                        b[i] = v;
                    }
                    Marshal.Copy(b, 0, _currentBuffer.Data, b.Length);
                }
                _currentBuffer.Play();
            }
            WaitForAllBuffers();
        }

        /// <summary>
        /// Allocates the buffer list.
        /// </summary>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="bufferCount">The buffer count.</param>
        private void AllocateBufferList(int bufferSize, int bufferCount)
        {
            FreeBufferList();
            if (bufferCount > 0)
            {
                _bufferList = new WaveOutBuffer(_wave, bufferSize);
                WaveOutBuffer prevBuffer = _bufferList;
                try
                {
                    for (int bufferIndex = 1; bufferIndex < bufferCount; bufferIndex++)
                    {
                        WaveOutBuffer buffer = new WaveOutBuffer(_wave, bufferSize);
                        prevBuffer.NextBuffer = buffer;
                        prevBuffer = buffer;
                    }
                }
                finally
                {
                    prevBuffer.NextBuffer = _bufferList;
                }
            }
        }

        /// <summary>
        /// Frees the buffer list.
        /// </summary>
        private void FreeBufferList()
        {
            _currentBuffer = null;
            if (_bufferList != null)
            {
                WaveOutBuffer firstBuffer = _bufferList;
                _bufferList = null;
                WaveOutBuffer buffer = firstBuffer;
                do
                {
                    WaveOutBuffer nextBuffer = buffer.NextBuffer;
                    buffer.Dispose();
                    buffer = nextBuffer;
                } while (buffer != firstBuffer);
            }
        }

        /// <summary>
        /// Advances this instance.
        /// </summary>
        private void Advance()
        {
            _currentBuffer = (_currentBuffer == null ? _bufferList : _currentBuffer.NextBuffer);
            _currentBuffer.WaitFor();
        }

        /// <summary>
        /// Waits for all buffers.
        /// </summary>
        private void WaitForAllBuffers()
        {
            WaveOutBuffer buffer = _bufferList;
            while (buffer.NextBuffer != _bufferList)
            {
                buffer.WaitFor();
                buffer = buffer.NextBuffer;
            }
        }
    }
}
