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
using System.Threading;
namespace System.Interop.AudioVideo.Win32
{
    /// <summary>
    /// Wave In
    /// </summary>
	public class WaveIn : System.IDisposable
	{
		private IntPtr _wave;
		private WaveInBuffer _bufferList; //+ linked list
		private WaveInBuffer _currentBuffer;
		private Thread _thread;
		private BufferDoneEventHandler _doneDelegate;
		private bool _isComplete;
        private Native_.WindowsMultimedia.WaveDelegate _bufferDelegate = new Native_.WindowsMultimedia.WaveDelegate(WaveInBuffer.WaveInProc);

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveInRecorder"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="format">The format.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="bufferCount">The buffer count.</param>
        /// <param name="doneProc">The done proc.</param>
        public WaveIn(int device, Native_.WaveFormat format, int bufferSize, int bufferCount, BufferDoneEventHandler doneProc)
        {
            _doneDelegate = doneProc;
            Helper.Try(Native_.WindowsMultimedia.waveInOpen(out _wave, device, format, _bufferDelegate, 0, Native_.WindowsMultimedia.CALLBACK_FUNCTION));
            AllocateBufferList(bufferSize, bufferCount);
            for (int bufferIndex = 0; bufferIndex < bufferCount; bufferIndex++)
            {
                SelectNextBuffer();
                _currentBuffer.Record();
            }
            Helper.Try(Native_.WindowsMultimedia.waveInStart(_wave));
            _thread = new Thread(new ThreadStart(Thread));
            _thread.Start();
        }
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="WaveInRecorder"/> is reclaimed by garbage collection.
        /// </summary>
        ~WaveIn()
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
                        Native_.WindowsMultimedia.waveInReset(_wave);
                    }
                    WaitForAllBuffers();
                    _thread.Join();
                    _doneDelegate = null;
                    FreeBufferList();
                    if (_wave != IntPtr.Zero)
                    {
                        Native_.WindowsMultimedia.waveInClose(_wave);
                    }
                }
                finally
                {
                    _thread = null;
                    _wave = IntPtr.Zero;
                }
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the device count.
        /// </summary>
        /// <value>The device count.</value>
		public static int DeviceCount
		{
            get { return Native_.WindowsMultimedia.waveInGetNumDevs(); }
		}

        /// <summary>
        /// Threads this instance.
        /// </summary>
        private void Thread()
		{
			while (!_isComplete)
			{
				Advance();
                if ((_doneDelegate != null) && (!_isComplete))
                {
                    _doneDelegate(_currentBuffer.Data, _currentBuffer.Size);
                }
				_currentBuffer.Record();
			}
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
				_bufferList = new WaveInBuffer(_wave, bufferSize);
                WaveInBuffer prevBuffer = _bufferList;
				try
				{
					for (int bufferIndex = 1; bufferIndex < bufferCount; bufferIndex++)
					{
						WaveInBuffer buffer = new WaveInBuffer(_wave, bufferSize);
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
				WaveInBuffer firstBuffer = _bufferList;
				_bufferList = null;
                WaveInBuffer buffer = firstBuffer;
				do
				{
                    WaveInBuffer nextBuffer = buffer.NextBuffer;
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
			SelectNextBuffer();
			_currentBuffer.WaitFor();
		}

        /// <summary>
        /// Selects the next buffer.
        /// </summary>
		private void SelectNextBuffer()
		{
			_currentBuffer = (_currentBuffer == null ? _bufferList : _currentBuffer.NextBuffer);
		}

        /// <summary>
        /// Waits for all buffers.
        /// </summary>
		private void WaitForAllBuffers()
		{
			WaveInBuffer buffer = _bufferList;
            while (buffer.NextBuffer != _bufferList)
			{
                buffer.WaitFor();
                buffer = buffer.NextBuffer;
			}
		}
	}
}
