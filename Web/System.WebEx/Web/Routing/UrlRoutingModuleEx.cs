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
using System.Collections;
using System.Reflection;
using System.Patterns.Schema;
namespace System.Web.Routing
{
    public class UrlRoutingModuleEx : UrlRoutingModule
    {
#if !CLR4
        private static readonly object s_requestDataKey = typeof(UrlRoutingModule).GetField("_requestDataKey", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
        private static readonly Type s_requestDataType = Type.GetType("System.Web.Routing.UrlRoutingModule+RequestData, " + AssemblyRef.SystemWebRouting);
        private static readonly PropertyInfo s_originalPathProperty = s_requestDataType.GetProperty("OriginalPath", BindingFlags.Public | BindingFlags.Instance);
#endif

        public override void PostResolveRequestCache(HttpContextBase httpContext)
        {
            string originalPath;
            var uriSchema = httpContext.Get<UriSchemaBase>();
            if (uriSchema == null)
                originalPath = null;
            else
            {
                string requestPath = httpContext.Request.Path;
                var uriContext = httpContext.ParseRequestUriWithUriSchema(uriSchema);
                string newPath = uriContext.Path;
                if (string.CompareOrdinal(requestPath, newPath) != 0)
                {
                    httpContext.RewritePath(newPath);
                    originalPath = requestPath;
                }
                else
                    originalPath = null;
            }
            base.PostResolveRequestCache(httpContext);
            if (originalPath != null)
                SetOriginalPathInRequestData(httpContext.Items, originalPath);
        }

        private static void SetOriginalPathInRequestData(IDictionary items, string originalPath)
        {
            var requestData = items[s_requestDataKey];
            if (requestData != null)
                s_originalPathProperty.SetValue(requestData, originalPath, null);
        }
    }
}