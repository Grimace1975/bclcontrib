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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Interop.AudioVideo.Native_.DirectShow;
namespace System.Interop.AudioVideo.DirectShow
{
    /// <summary>
    /// CapDevice
    /// </summary>
    //+ http://khason.net/blog/webcam-control-with-wpf-or-how-to-create-high-framerate-player-with-directshow-by-using-interopbitmap-in-wpf-application/
    public class CapDevice : IDisposable
    {
        private ManualResetEvent _stopSignal;
        private Thread _workerThread;
        private IGraphBuilder _graph;
        private ISampleGrabber _grabber;
        private IBaseFilter _sourceObject;
        private IBaseFilter _grabberObject;
        private IMediaControl _control;
        private CapGrabber _capGrabber;
        private static string _deviceMoniker;

        #region Class Types
        /// <summary>
        /// BehaviorVectorMask
        /// </summary>
        public enum BehaviorVectorMask
        {
            /// <summary>
            /// DispatchOnWorkerThread
            /// </summary>
            DispatchOnWorkerThread = 0x01
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CapDevice"/> class.
        /// </summary>
        public CapDevice()
            : this(GetDeviceMonikes()[0].MonikerString)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CapDevice"/> class.
        /// </summary>
        /// <param name="moniker">The moniker.</param>
        public CapDevice(string moniker)
        {
            _deviceMoniker = moniker;
        }

        /// <summary>
        /// BehaviorVector
        /// </summary>
        public BitVector32 BehaviorVector = new BitVector32((int)BehaviorVectorMask.DispatchOnWorkerThread);

        /// <summary>
        /// Gets the grabber.
        /// </summary>
        /// <value>The grabber.</value>
        public CapGrabber CapGrabber
        {
            get { return _capGrabber; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start(Func<CapGrabber> start)
        {
            if (_workerThread == null)
            {
                _stopSignal = new ManualResetEvent(false);
                _workerThread = new Thread(new ParameterizedThreadStart(WorkerThread)) { Name = "CapDeviceWorker", IsBackground = false };
                _workerThread.Start(start);
            }
            else
            {
                Stop();
                Start(null);
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                _stopSignal.Set();
                //+ TODO: remove Thread.Abort
                //_workerThread.Abort();
                if (_workerThread != null)
                {
                    _workerThread.Join();
                    Release();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get
            {
                if (_workerThread != null)
                {
                    if (!_workerThread.Join(0))
                    {
                        return true;
                    }
                    Release();
                }
                return false;
            }
        }

        /// <summary>
        /// Releases this instance.
        /// </summary>
        private void Release()
        {
            _workerThread = null;
            _stopSignal.Close(); _stopSignal = null;
        }

        /// <summary>
        /// Gets the device monikes.
        /// </summary>
        /// <value>The device monikes.</value>
        public static FilterInfo[] GetDeviceMonikes()
        {
			List<FilterInfo> filters = new List<FilterInfo>();
			IMoniker[] ms = new IMoniker[1];
			ICreateDevEnum deviceEnum = (Activator.CreateInstance(Type.GetTypeFromCLSID(Registry.SystemDeviceEnum)) as ICreateDevEnum);
            IEnumMoniker moniker;
            Guid g = Registry.VideoInputDevice;
            if (deviceEnum.CreateClassEnumerator(ref g, out moniker, 0) == 0)
            {
                while (true)
                {
                    int r = moniker.Next(1, ms, IntPtr.Zero);
                    if ((r != 0) || (ms[0] == null))
                    {
                        break;
                    }
                    filters.Add(new FilterInfo(ms[0]));
                    Marshal.ReleaseComObject(ms[0]);
                    ms[0] = null;
                }
            }
            return filters.ToArray();
        }

        /// <summary>
        /// Runs the worker.
        /// </summary>
        private void WorkerThread(object start)
        {
            _capGrabber = ((Func<CapGrabber>)start)();
            if (BehaviorVector[(int)BehaviorVectorMask.DispatchOnWorkerThread])
            {
                _capGrabber.DispatchOnWorkerThread = true;
            }
            OnEnterWorkerThread();
            try
            {
                _graph = (Activator.CreateInstance(Type.GetTypeFromCLSID(Registry.FilterGraph)) as IGraphBuilder);
                _sourceObject = FilterInfo.CreateFilter(_deviceMoniker);
                _grabber = (Activator.CreateInstance(Type.GetTypeFromCLSID(Registry.SampleGrabber)) as ISampleGrabber);
                _grabberObject = (_grabber as IBaseFilter);
                _graph.AddFilter(_sourceObject, "source");
                _graph.AddFilter(_grabberObject, "grabber");
                //+ Set media type for our grabber
				using (AMMediaType mediaType = new AMMediaType())
                {
                    mediaType.MajorType = Registry.MediaTypes.Video;
                    mediaType.SubType = Registry.MediaSubTypes.RGB32;
                    _grabber.SetMediaType(mediaType);
                    //+ And then connect device filter to out pin and grabber to in pin. Then get capabilities of video received (this stuff come from your web camera manufacturer)
                    if (_graph.Connect(_sourceObject.GetPin(PinDirection.Output, 0), _grabberObject.GetPin(PinDirection.Input, 0)) >= 0)
                    {
                        if (_grabber.GetConnectedMediaType(mediaType) == 0)
                        {
							VideoInfoHeader header = (VideoInfoHeader)Marshal.PtrToStructure(mediaType.FormatPtr, typeof(VideoInfoHeader));
                            _capGrabber.Width = header.BmiHeader.Width;
                            _capGrabber.Height = header.BmiHeader.Height;
                        }
                    }
                    //+ Out pin to grabber without buffering and callback to grabber object (this one will get all images from our source).
                    _graph.Render(_grabberObject.GetPin(PinDirection.Output, 0));
                    _grabber.SetBufferSamples(false);
                    _grabber.SetOneShot(false);
                    _grabber.SetCallback(_capGrabber, 1);
                    //+ Dump output window
					IVideoWindow wnd = (IVideoWindow)_graph;
                    wnd.put_AutoShow(false); wnd = null;
                    //+ And run the controller
                    _control = (IMediaControl)_graph;
                    _control.Run();
                    //+
                    Dispatcher.Run();
                    //while (!WaitHandle.WaitAll(new WaitHandle[] { _stopSignal }, 10, true))
                    while (!_stopSignal.WaitOne(10, true))
                    {
                        Dispatcher.Run();
                    }
                    Dispatcher.ExitAllFrames();
                    _control.StopWhenReady();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                _graph = null;
                _sourceObject = null;
                _grabberObject = null;
                _grabber = null;
                _capGrabber = null;
                _control = null;
                //+ guaranteed exit
                OnExitWorkerThread();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// Occurs when [enter worker thread].
        /// </summary>
        public event EventHandler EnterWorkerThread;

        /// <summary>
        /// Called when [enter worker thread].
        /// </summary>
        protected virtual void OnEnterWorkerThread()
        {
            EventHandler enterWorkerThread = EnterWorkerThread;
            if (enterWorkerThread != null)
            {
                enterWorkerThread(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when [exit worker thread].
        /// </summary>
        public event EventHandler ExitWorkerThread;

        /// <summary>
        /// Called when [exit worker thread].
        /// </summary>
        protected virtual void OnExitWorkerThread()
        {
			EventHandler exitWorkerThread = ExitWorkerThread;
            if (exitWorkerThread != null)
            {
                exitWorkerThread(this, EventArgs.Empty);
            }
        }
    }
}
