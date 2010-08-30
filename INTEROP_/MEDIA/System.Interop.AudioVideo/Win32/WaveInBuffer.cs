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
namespace System.Interop.AudioVideo.Win32
{
    /// <summary>
    /// Wave In Buffer
    /// </summary>
    internal class WaveInBuffer : IDisposable
    {
        public WaveInBuffer NextBuffer;
        private AutoResetEvent _event = new AutoResetEvent(false);
        private IntPtr _wave;
        private Native_.WindowsMultimedia.WaveHdr _header;
        private byte[] _headerData;
        private GCHandle _headerHandle;
        private GCHandle _headerDataHandle;
        private bool _isEnable;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveInBuffer"/> class.
        /// </summary>
        /// <param name="waveHandle">The wave handle.</param>
        /// <param name="size">The size.</param>
        public WaveInBuffer(IntPtr waveHandle, int size)
        {
            _wave = waveHandle;
            _headerHandle = GCHandle.Alloc(_header, GCHandleType.Pinned);
            _header.dwUser = (IntPtr)GCHandle.Alloc(this);
            _headerData = new byte[size];
            _headerDataHandle = GCHandle.Alloc(_headerData, GCHandleType.Pinned);
            _header.lpData = _headerDataHandle.AddrOfPinnedObject();
            _header.dwBufferLength = size;
            Helper.Try(Native_.WindowsMultimedia.waveInPrepareHeader(_wave, ref _header, Marshal.SizeOf(_header)));
        }
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="WaveInBuffer"/> is reclaimed by garbage collection.
        /// </summary>
        ~WaveInBuffer()
        {
            Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_header.lpData != IntPtr.Zero)
            {
                Native_.WindowsMultimedia.waveInUnprepareHeader(_wave, ref _header, Marshal.SizeOf(_header));
                _headerHandle.Free();
                _header.lpData = IntPtr.Zero;
            }
            _event.Close();
            if (_headerDataHandle.IsAllocated)
            {
                _headerDataHandle.Free();
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Waves the in proc.
        /// </summary>
        /// <param name="hdrvr">The HDRVR.</param>
        /// <param name="uMsg">The u MSG.</param>
        /// <param name="dwUser">The dw user.</param>
        /// <param name="wavhdr">The wavhdr.</param>
        /// <param name="dwParam2">The dw param2.</param>
        internal static void WaveInProc(IntPtr hdrvr, int uMsg, int dwUser, ref Native_.WindowsMultimedia.WaveHdr wavhdr, int dwParam2)
        {
            if (uMsg == Native_.WindowsMultimedia.MM_WIM_DATA)
            {
                try
                {
                    GCHandle h = (GCHandle)wavhdr.dwUser;
                    WaveInBuffer buffer = (WaveInBuffer)h.Target;
                    buffer.OnCompleted();
                }
                catch { }
            }
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size
        {
            get { return _header.dwBufferLength; }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public IntPtr Data
        {
            get { return _header.lpData; }
        }

        /// <summary>
        /// Records this instance.
        /// </summary>
        /// <returns></returns>
        public bool Record()
        {
            lock (this)
            {
                _event.Reset();
                _isEnable = (Native_.WindowsMultimedia.waveInAddBuffer(_wave, ref _header, Marshal.SizeOf(_header)) == Native_.WindowsMultimedia.MMSYSERR_NOERROR);
                return _isEnable;
            }
        }

        /// <summary>
        /// Waits for.
        /// </summary>
        public void WaitFor()
        {
            if (_isEnable)
            {
                _isEnable = _event.WaitOne();
            }
            else
            {
                Thread.Sleep(0);
            }
        }

        /// <summary>
        /// Called when [completed].
        /// </summary>
        public void OnCompleted()
        {
            _event.Set();
            _isEnable = false;
        }
    }
}
