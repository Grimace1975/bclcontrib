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
namespace System.Web.Routing
{
    public static class WebFormRouteHandlerExtensions
    {
        public static Route CreateRouteEx<T>(this WebFormRouteHandler<T> handler, string url)
            where T : IHttpHandler, new() { return handler.CreateRouteEx(url, null, null); }
        public static Route CreateRouteEx<T>(this WebFormRouteHandler<T> handler, string url, object defaults)
            where T : IHttpHandler, new() { return handler.CreateRouteEx(url, defaults, null); }
        public static Route CreateRouteEx<T>(this WebFormRouteHandler<T> handler, string url, object defaults, object constraints)
            where T : IHttpHandler, new()
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            if (url == null)
                throw new ArgumentNullException("url");
            var route = new RouteEx(url, handler);
            route.Defaults = new RouteValueDictionary(defaults);
            route.Constraints = new RouteValueDictionary(constraints);
            route.DataTokens = new RouteValueDictionary();
            return route;
        }
    }
}