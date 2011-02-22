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
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using System.Runtime.InteropServices;
namespace System.Interop.AudioVideo.DirectShow
{
    /// <summary>
    /// CapGrabber
    /// </summary>
	public class CapGrabber : DispatcherObject, Native_.DirectShow.ISampleGrabberCB
    {
        private int _height;
        private int _width;
        //+ framerate
        private Stopwatch _frameTimer = Stopwatch.StartNew();
        private double _frameCount;
        private bool _dispatchOnWorkerThread;

        /// <summary>
        /// Initializes a new instance of the <see cref="CapGrabber"/> class.
        /// </summary>
        public CapGrabber()
        {
        }

        /// <summary>
        /// Copies the memory.
        /// </summary>
        /// <param name="Destination">The destination.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Length">The length.</param>
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <value>The map.</value>
        public IntPtr Map { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dispatch on worker thread].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [dispatch on worker thread]; otherwise, <c>false</c>.
        /// </value>
        internal bool DispatchOnWorkerThread
        {
            get { return _dispatchOnWorkerThread; }
            set { _dispatchOnWorkerThread = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        /// <summary>
        /// Gets or sets the framerate.
        /// </summary>
        /// <value>The framerate.</value>
        public float Framerate { get; set; }

        /// <summary>
        /// Updates the framerate.
        /// </summary>
        private void UpdateFramerate()
        {
            _frameCount++;
            if (_frameTimer.ElapsedMilliseconds >= 1000)
            {
                Framerate = (float)Math.Round(_frameCount * 1000 / _frameTimer.ElapsedMilliseconds);
                _frameTimer.Reset();
                _frameTimer.Start();
                _frameCount = 0;
            }
        }

        /// <summary>
        /// Samples the CB.
        /// </summary>
        /// <param name="sampleTime">The sample time.</param>
        /// <param name="sample">The sample.</param>
        /// <returns></returns>
        public int SampleCB(double sampleTime, IntPtr sample)
        {
            return 0;
        }

        /// <summary>
        /// Buffers the CB.
        /// </summary>
        /// <param name="sampleTime">The sample time.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="bufferLen">The buffer len.</param>
        /// <returns></returns>
        public int BufferCB(double sampleTime, IntPtr buffer, int bufferLen)
        {
            if (Map != IntPtr.Zero)
            {
                CopyMemory(Map, buffer, bufferLen);
                UpdateFramerate();
                try
                {
                    if ((_dispatchOnWorkerThread ) && (Dispatcher != null))
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                        {
                            VerifyAccess();
                            OnNewFrameArrived();
                        }, null);
                    }
                    else
                    {
                        OnNewFrameArrived();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return 0;
        }

        /// <summary>
        /// Occurs when [new frame arrived].
        /// </summary>
        public event EventHandler NewFrameArrived;

        /// <summary>
        /// Called when [new frame arrived].
        /// </summary>
        protected virtual void OnNewFrameArrived()
        {
			EventHandler newFrameArrived = NewFrameArrived;
            if (newFrameArrived != null)
            {
                newFrameArrived(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #region MapToInteropBitmap
        private IntPtr _interopBitmapMap;
        private IntPtr _interopBitmapSection;

        ///<summary>
        /// Creates the file mapping.
        /// </summary>
        /// <param name="hFile">The h file.</param>
        /// <param name="lpFileMappingAttributes">The lp file mapping attributes.</param>
        /// <param name="flProtect">The fl protect.</param>
        /// <param name="dwMaximumSizeHigh">The dw maximum size high.</param>
        /// <param name="dwMaximumSizeLow">The dw maximum size low.</param>
        /// <param name="lpName">Name of the lp.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

        /// <summary>
        /// Maps the view of file.
        /// </summary>
        /// <param name="hFileMappingObject">The h file mapping object.</param>
        /// <param name="dwDesiredAccess">The dw desired access.</param>
        /// <param name="dwFileOffsetHigh">The dw file offset high.</param>
        /// <param name="dwFileOffsetLow">The dw file offset low.</param>
        /// <param name="dwNumberOfBytesToMap">The dw number of bytes to map.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        /// <summary>
        /// Maps to interop bitmap.
        /// </summary>
        /// <returns></returns>
        public InteropBitmap MapToInteropBitmap()
        {
            int width = Width;
            int height = Height;
            if ((width != 0) && (height != 0))
            {
                uint byteSize = (uint)(width * height * PixelFormats.Bgr32.BitsPerPixel / 8);
                _interopBitmapSection = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, byteSize, null);
                _interopBitmapMap = MapViewOfFile(_interopBitmapSection, 0xF001F, 0, 0, byteSize);
				InteropBitmap interopBitmap = (Imaging.CreateBitmapSourceFromMemorySection(_interopBitmapSection, width, height, PixelFormats.Bgr32, width * PixelFormats.Bgr32.BitsPerPixel / 8, 0) as InteropBitmap);
                Map = _interopBitmapMap;
                return interopBitmap;
            }
            return null;
        }
        #endregion
    }
}
