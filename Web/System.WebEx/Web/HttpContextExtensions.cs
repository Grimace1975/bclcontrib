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
using System.Patterns.Session;
namespace System.Web
{
    /// <summary>
    /// HttpContextExtensions
    /// </summary>
    public static partial class HttpContextExtensions
    {
        private static readonly object s_sessionExProviderKey = new object();

        public static T Get<T>(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (T)httpContext.Items[typeof(T)];
        }
        public static T Get<T>(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (T)httpContext.Items[typeof(T)];
        }

        public static void Set<T>(this HttpContext httpContext, T value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(T)] = value;
        }
        public static void Set<T>(this HttpContextBase httpContext, T value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(T)] = value;
        }

        public static void SetSessionExProvider(this HttpContext httpContext, HttpSessionExProviderBase sessionExProvider)
        {
            if (sessionExProvider == null)
                throw new ArgumentNullException("sessionExProvider");
            httpContext.Items[s_sessionExProviderKey] = sessionExProvider;
        }

        public static HttpSessionExProviderBase GetSessionExProvider(this HttpContext httpContext)
        {
            return (HttpSessionExProviderBase)httpContext.Items[s_sessionExProviderKey];
        }
    }
}