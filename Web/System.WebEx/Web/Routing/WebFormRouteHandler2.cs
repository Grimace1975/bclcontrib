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
using System.Web.Compilation;
namespace System.Web.Routing
{
    // http://chriscavanagh.wordpress.com/2008/03/11/aspnet-routing-goodbye-url-rewriting/
    public class WebFormRouteHandler<T> : IRouteHandler
        where T : IHttpHandler
    {
        public string VirtualPath { get; set; }

        public WebFormRouteHandler(string virtualPath)
        {
            VirtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var httpHandler = (VirtualPath != null ? (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(T)) : null);
            if (httpHandler != null)
            {
                var routablePage = (httpHandler as IRoutablePage);
                if (routablePage != null)
                    routablePage.RequestContext = requestContext;
            }
            return httpHandler;
        }
    }
}