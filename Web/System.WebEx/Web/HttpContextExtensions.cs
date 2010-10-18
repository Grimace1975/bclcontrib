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
using System.Patterns.Generic;
using System.Collections.Generic;
namespace System.Web
{
    /// <summary>
    /// HttpContextExtensions
    /// </summary>
    public static partial class HttpContextExtensions
    {
        private static readonly object s_sessionExProviderKey = new object();

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

        // HTTPCONTEXT
        public static bool HasExtent<T>(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return httpContext.Items.Contains(typeof(T));
        }
        public static bool HasExtent(this HttpContext httpContext, Type type)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            return httpContext.Items.Contains(type);
        }

        public static void Clear<T>(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(T)] = null;
        }
        public static void Clear(this HttpContext httpContext, Type type)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            httpContext.Items[type] = null;
        }

        public static T Get<T>(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (T)httpContext.Items[typeof(T)];
        }
        public static IEnumerable<T> GetMany<T>(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (IEnumerable<T>)httpContext.Items[typeof(IEnumerable<T>)];
        }
        public static object Get(this HttpContext httpContext, Type type)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            return httpContext.Items[type];
        }

        public static void Set<T>(this HttpContext httpContext, T value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(T)] = value;
        }
        public static void SetMany<T>(this HttpContext httpContext, IEnumerable<T> value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(IEnumerable<T>)] = value;
        }
        public static void Set(this HttpContext httpContext, Type type, object value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            httpContext.Items[type] = value;
        }

        public static bool TryGetExtent<T>(this HttpContext httpContext, out T extent)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            string key = typeof(T).ToString();
            if (!httpContext.Items.Contains(key))
            {
                extent = default(T);
                return false;
            }
            extent = (T)httpContext.Items[key];
            return true;
        }

        public static void AddRange(this HttpContext httpContext, IEnumerable<object> extents)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (extents == null)
                throw new ArgumentNullException("extents");
            foreach (var extent in extents)
                Set(httpContext, extent.GetType(), extent);
        }

        // HTTPCONTEXTBASE
        public static bool HasExtent<T>(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return httpContext.Items.Contains(typeof(T));
        }
        public static bool HasExtent(this HttpContextBase httpContext, Type type)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            return httpContext.Items.Contains(type);
        }

        public static void Clear<T>(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(T)] = null;
        }
        public static void Clear(this HttpContextBase httpContext, Type type)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            httpContext.Items[type] = null;
        }

        public static T Get<T>(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (T)httpContext.Items[typeof(T)];
        }
        public static IEnumerable<T> GetMany<T>(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            return (IEnumerable<T>)httpContext.Items[typeof(IEnumerable<T>)];
        }
        public static object Get(this HttpContextBase httpContext, Type type)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            return httpContext.Items[type];
        }

        public static void Set<T>(this HttpContextBase httpContext, T value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(T)] = value;
        }
        public static void SetMany<T>(this HttpContextBase httpContext, IEnumerable<T> value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            httpContext.Items[typeof(IEnumerable<T>)] = value;
        }
        public static void Set(this HttpContextBase httpContext, Type type, object value)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (type == null)
                throw new ArgumentNullException("type");
            httpContext.Items[type] = value;
        }
        public static bool TryGetExtent<T>(this HttpContextBase httpContext, out T extent)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            string key = typeof(T).ToString();
            if (!httpContext.Items.Contains(key))
            {
                extent = default(T);
                return false;
            }
            extent = (T)httpContext.Items[key];
            return true;
        }

        public static void AddRange(this HttpContextBase httpContext, IEnumerable<object> extents)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (extents == null)
                throw new ArgumentNullException("extents");
            foreach (var extent in extents)
                Set(httpContext, extent.GetType(), extent);
        }        
    }
}