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
namespace System.Threading.Async
{
    public static class AsyncResultWrapper
    {
        public static IAsyncResult Begin(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate endDelegate)
        {
            return Begin(callback, state, beginDelegate, endDelegate, null);
        }

        public static IAsyncResult Begin<TResult>(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate<TResult> endDelegate)
        {
            return Begin<TResult>(callback, state, beginDelegate, endDelegate, null);
        }

        public static IAsyncResult Begin(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate endDelegate, object tag)
        {
            return Begin(callback, state, beginDelegate, endDelegate, tag, -1);
        }

        public static IAsyncResult Begin<TResult>(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate<TResult> endDelegate, object tag)
        {
            return Begin<TResult>(callback, state, beginDelegate, endDelegate, tag, -1);
        }

        public static IAsyncResult Begin<TResult>(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate<TResult> endDelegate, object tag, int timeout)
        {
            WrappedAsyncResult<TResult> result = new WrappedAsyncResult<TResult>(beginDelegate, endDelegate, tag);
            result.Begin(callback, state, timeout);
            return result;
        }

        public static IAsyncResult Begin(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate endDelegate, object tag, int timeout)
        {
            return Begin<AsyncVoid>(callback, state, beginDelegate, MakeVoidDelegate(endDelegate), tag, timeout);
        }

        public static IAsyncResult BeginSynchronous<TResult>(AsyncCallback callback, object state, Func<TResult> func)
        {
            return BeginSynchronous<TResult>(callback, state, func, null);
        }

        public static IAsyncResult BeginSynchronous(AsyncCallback callback, object state, Action action)
        {
            return BeginSynchronous(callback, state, action, null);
        }

        public static IAsyncResult BeginSynchronous(AsyncCallback callback, object state, Action action, object tag)
        {
            return BeginSynchronous<AsyncVoid>(callback, state, MakeVoidDelegate(action), tag);
        }

        public static IAsyncResult BeginSynchronous<TResult>(AsyncCallback callback, object state, Func<TResult> func, object tag)
        {
            BeginInvokeDelegate beginDelegate = delegate(AsyncCallback asyncCallback, object asyncState)
            {
                var result2 = new SimpleAsyncResult(asyncState);
                result2.MarkCompleted(true, asyncCallback);
                return result2;
            };
            EndInvokeDelegate<TResult> endDelegate = (_ => func());
            var result = new WrappedAsyncResult<TResult>(beginDelegate, endDelegate, tag);
            result.Begin(callback, state, -1);
            return result;
        }

        public static TResult End<TResult>(IAsyncResult asyncResult)
        {
            return End<TResult>(asyncResult, null);
        }

        public static void End(IAsyncResult asyncResult)
        {
            End(asyncResult, null);
        }

        public static void End(IAsyncResult asyncResult, object tag)
        {
            End<AsyncVoid>(asyncResult, tag);
        }

        public static TResult End<TResult>(IAsyncResult asyncResult, object tag)
        {
            return WrappedAsyncResult<TResult>.Cast(asyncResult, tag).End();
        }

        private static Func<AsyncVoid> MakeVoidDelegate(Action action)
        {
            return delegate
            {
                action();
                return new AsyncVoid();
            };
        }

        private static EndInvokeDelegate<AsyncVoid> MakeVoidDelegate(EndInvokeDelegate endDelegate)
        {
            return delegate(IAsyncResult ar)
            {
                endDelegate(ar);
                return new AsyncVoid();
            };
        }

        private sealed class WrappedAsyncResult<TResult> : IAsyncResult
        {
            private readonly BeginInvokeDelegate _beginDelegate;
            private readonly object _beginDelegateLockObj;
            private readonly EndInvokeDelegate<TResult> _endDelegate;
            private readonly SingleEntryGate _endExecutedGate;
            private readonly SingleEntryGate _handleCallbackGate;
            private IAsyncResult _innerAsyncResult;
            private AsyncCallback _originalCallback;
            private readonly object _tag;
            private volatile bool _timedOut;
            private Timer _timer;

            public WrappedAsyncResult(BeginInvokeDelegate beginDelegate, EndInvokeDelegate<TResult> endDelegate, object tag)
            {
                _beginDelegateLockObj = new object();
                _endExecutedGate = new SingleEntryGate();
                _handleCallbackGate = new SingleEntryGate();
                _beginDelegate = beginDelegate;
                _endDelegate = endDelegate;
                _tag = tag;
            }

            public void Begin(AsyncCallback callback, object state, int timeout)
            {
                bool completedSynchronously;
                _originalCallback = callback;
                lock (_beginDelegateLockObj)
                {
                    _innerAsyncResult = _beginDelegate(new AsyncCallback(HandleAsynchronousCompletion), state);
                    completedSynchronously = _innerAsyncResult.CompletedSynchronously;
                    if (!completedSynchronously && (timeout > -1))
                        CreateTimer(timeout);
                }
                if (completedSynchronously && (callback != null))
                    callback(this);
            }

            public static AsyncResultWrapper.WrappedAsyncResult<TResult> Cast(IAsyncResult asyncResult, object tag)
            {
                if (asyncResult == null)
                    throw new ArgumentNullException("asyncResult");
                var result = (asyncResult as AsyncResultWrapper.WrappedAsyncResult<TResult>);
                if ((result == null) || !object.Equals(result._tag, tag))
                    throw new InvalidOperationException(); // Error.AsyncCommon_InvalidAsyncResult("asyncResult");
                return result;
            }

            private void CreateTimer(int timeout)
            {
                _timer = new Timer(new TimerCallback(HandleTimeout), null, timeout, -1);
            }

            public TResult End()
            {
                if (!_endExecutedGate.TryEnter())
                    throw new InvalidOperationException(); // Error.AsyncCommon_AsyncResultAlreadyConsumed();
                if (_timedOut)
                    throw new TimeoutException();
                WaitForBeginToCompleteAndDestroyTimer();
                return _endDelegate(this._innerAsyncResult);
            }

            private void ExecuteAsynchronousCallback(bool timedOut)
            {
                WaitForBeginToCompleteAndDestroyTimer();
                if (_handleCallbackGate.TryEnter())
                {
                    _timedOut = timedOut;
                    if (_originalCallback != null)
                        _originalCallback(this);
                }
            }

            private void HandleAsynchronousCompletion(IAsyncResult asyncResult)
            {
                if (!asyncResult.CompletedSynchronously)
                    ExecuteAsynchronousCallback(false);
            }

            private void HandleTimeout(object state)
            {
                ExecuteAsynchronousCallback(true);
            }

            private void WaitForBeginToCompleteAndDestroyTimer()
            {
                lock (_beginDelegateLockObj)
                {
                    if (_timer != null)
                        _timer.Dispose();
                    _timer = null;
                }
            }

            public object AsyncState
            {
                get { return _innerAsyncResult.AsyncState; }
            }

            public WaitHandle AsyncWaitHandle
            {
                get { return _innerAsyncResult.AsyncWaitHandle; }
            }

            public bool CompletedSynchronously
            {
                get { return _innerAsyncResult.CompletedSynchronously; }
            }

            public bool IsCompleted
            {
                get { return _innerAsyncResult.IsCompleted; }
            }
        }
    }
}