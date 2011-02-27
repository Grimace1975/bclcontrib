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
using System.Patterns.Schema;
namespace System.Web
{
    // todo need to balance these more. maybe return an HttpRequestBase
    //must play nicely with:
    //    SiteMapExRouteContext
    //    UrlRoutingModuleEx::PostResolveRequestCache
    //    SiteMapExRoute::GetRouteData
    public static partial class HttpContextExtensions
    {
        public static UriContextBase ParseRequestUriWithUriSchema(this HttpContext httpContext)
        {
            return ParseRequestUriWithUriSchema(httpContext, httpContext.Get<UriSchemaBase>());
        }
        public static UriContextBase ParseRequestUriWithUriSchema(this HttpContextBase httpContext)
        {
            return ParseRequestUriWithUriSchema(httpContext, httpContext.Get<UriSchemaBase>());
        }

        public static UriContextBase ParseRequestUriWithUriSchema(this HttpContext httpContext, UriSchemaBase uriSchema)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (uriSchema == null)
                throw new ArgumentNullException("uriSchema");
            var uriContext = uriSchema.ParseUri(httpContext.Request);
            httpContext.Set<UriContextBase>(uriContext);
            return uriContext;
        }
        public static UriContextBase ParseRequestUriWithUriSchema(this HttpContextBase httpContext, UriSchemaBase uriSchema)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (uriSchema == null)
                throw new ArgumentNullException("uriSchema");
            var uriContext = uriSchema.ParseUri(httpContext.Request);
            httpContext.Set<UriContextBase>(uriContext);
            return uriContext;
        }

        public static string CreatePath(this HttpContext httpContext, string uri) { return CreatePath(httpContext, uri, null); }
        public static string CreatePath(this HttpContext httpContext, string uri, Nattrib attrib)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            var uriContext = httpContext.Get<UriContextBase>();
            if (uriContext == null)
                throw new NullReferenceException("uriContext");
            return uriContext.CreatePath(uri, attrib);
        }

        public static string CreatePath(this HttpContextBase httpContext, string uri) { return CreatePath(httpContext, uri, null); }
        public static string CreatePath(this HttpContextBase httpContext, string uri, Nattrib attrib)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            var uriContext = httpContext.Get<UriContextBase>();
            if (uriContext == null)
                throw new NullReferenceException("uriContext");
            return uriContext.CreatePath(uri, attrib);
        }
    }
}