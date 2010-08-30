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
using System.Web;
namespace System.Patterns.Schema
{
    /// <summary>
    /// UriSchemaExtensions
    /// </summary>
    public static class UriSchemaExtensions
    {
        //public static UriSchemaBase BindToHttp(this UriSchemaBase schema)
        //{
        //    schema.OnOverflow = (IUriPartScanner scanner) =>
        //    {
        //        var httpContext = HttpContext.Current;
        //        var url = scanner.NormalizedPath + httpContext.Request.Url.Query;
        //        httpContext.Response.RedirectPermanent(url, true);
        //    };
        //    schema.BindCore();
        //    return schema;
        //}

        public static UriContextBase ParseUri(this UriSchemaBase schema, HttpRequest httpRequest)
        {
            string virtualPath = "/" + httpRequest.AppRelativeCurrentExecutionFilePath.Substring(2) + httpRequest.PathInfo;
            return schema.ParseUri(new Uri(virtualPath, UriKind.Relative));
        }
        public static UriContextBase ParseUri(this UriSchemaBase schema, HttpRequestBase httpRequest)
        {
            string virtualPath = "/" + httpRequest.AppRelativeCurrentExecutionFilePath.Substring(2) + httpRequest.PathInfo;
            return schema.ParseUri(new Uri(virtualPath, UriKind.Relative));
        }
    }
}