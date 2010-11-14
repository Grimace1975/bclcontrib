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
using System.Web.Routing;
namespace System.Web.Mvc
{
    /// <summary>
    /// PathHelpers
    /// </summary>
    internal static class PathHelpers
    {
        private const string _urlRewriterServerVar = "HTTP_X_ORIGINAL_URL";

        public static string GenerateClientUrl(HttpContextBase httpContext, string contentPath)
        {
            string str;
            if (string.IsNullOrEmpty(contentPath))
                return contentPath;
            contentPath = StripQuery(contentPath, out str);
            return (GenerateClientUrlInternal(httpContext, contentPath) + str);
        }

        private static string GenerateClientUrlInternal(HttpContextBase httpContext, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                return contentPath;
            if (contentPath[0] == '~')
            {
                string virtualPath = VirtualPathUtility.ToAbsolute(contentPath, httpContext.Request.ApplicationPath);
                string str2 = httpContext.Response.ApplyAppPathModifier(virtualPath);
                return GenerateClientUrlInternal(httpContext, str2);
            }
            var serverVariables = httpContext.Request.ServerVariables;
            if ((serverVariables == null) || (serverVariables["HTTP_X_ORIGINAL_URL"] == null))
                return contentPath;
            string relativePath = MakeRelative(httpContext.Request.Path, contentPath);
            return MakeAbsolute(httpContext.Request.RawUrl, relativePath);
        }

        public static string MakeAbsolute(string basePath, string relativePath)
        {
            string str;
            basePath = StripQuery(basePath, out str);
            return VirtualPathUtility.Combine(basePath, relativePath);
        }

        public static string MakeRelative(string fromPath, string toPath)
        {
            string str = VirtualPathUtility.MakeRelative(fromPath, toPath);
            if (!string.IsNullOrEmpty(str) && (str[0] != '?'))
                return str;
            return ("./" + str);
        }

        private static string StripQuery(string path, out string query)
        {
            int index = path.IndexOf('?');
            if (index >= 0)
            {
                query = path.Substring(index);
                return path.Substring(0, index);
            }
            query = null;
            return path;
        }
    }
}