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
using System.Web.UI;
using System.Web.SessionState;
using System.Threading.Async;
namespace System.Web.Routing
{
    public class GenericUrlRoutingHandler : UrlRoutingHandler, IHttpAsyncHandler, IHttpHandler, IRequiresSessionState
    {
        private static readonly object _processRequestTag = new object();

        protected virtual IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
        {
            var base2 = new HttpContextWrapper(httpContext);
            return BeginProcessRequest(base2, callback, state);
        }

        protected internal virtual IAsyncResult BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, object state)
        {
            BeginInvokeDelegate delegate4 = null;
            EndInvokeDelegate delegate5 = null;
            Action action2 = null;
            IHttpHandler httpHandler = GetHttpHandler(httpContext);
            IHttpAsyncHandler httpAsyncHandler = httpHandler as IHttpAsyncHandler;
            if (httpAsyncHandler != null)
            {
                if (delegate4 == null)
                    delegate4 = (asyncCallback, asyncState) => httpAsyncHandler.BeginProcessRequest(HttpContext.Current, asyncCallback, asyncState);
                BeginInvokeDelegate beginDelegate = delegate4;
                if (delegate5 == null)
                {
                    delegate5 = delegate(IAsyncResult asyncResult)
                    {
                        httpAsyncHandler.EndProcessRequest(asyncResult);
                    };
                }
                EndInvokeDelegate endDelegate = delegate5;
                return AsyncResultWrapper.Begin(callback, state, beginDelegate, endDelegate, _processRequestTag);
            }
            if (action2 == null)
                action2 = delegate
                {
                    httpHandler.ProcessRequest(HttpContext.Current);
                };
            Action action = action2;
            return AsyncResultWrapper.BeginSynchronous(callback, state, action, _processRequestTag);
        }

        protected internal virtual void EndProcessRequest(IAsyncResult asyncResult)
        {
            AsyncResultWrapper.End(asyncResult, _processRequestTag);
        }

        private static IHttpHandler GetHttpHandler(HttpContextBase httpContext)
        {
            var handler = new DummyHttpHandler();
            handler.PublicProcessRequest(httpContext);
            return handler.HttpHandler;
        }

        IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            return BeginProcessRequest(context, cb, extraData);
        }

        void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
        {
            EndProcessRequest(result);
        }

        protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext)
        {
            if (httpHandler == null)
                throw new ArgumentNullException("httpHandler");
            httpHandler.ProcessRequest(HttpContext.Current);
        }

        private sealed class DummyHttpHandler : UrlRoutingHandler
        {
            public IHttpHandler HttpHandler;

            public void PublicProcessRequest(HttpContextBase httpContext)
            {
                ProcessRequest(httpContext);
            }

            protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext)
            {
                HttpHandler = httpHandler;
            }
        }
    }
}