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
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace System.Web.Mvc
{
    /// <summary>
    /// ResourceFileStreamResult
    /// </summary>
    public class ResourceFileStreamResult : FileResult
    {
        private const int _bufferSize = 0x1000;
        private static readonly IDictionary s_cache = Hashtable.Synchronized(new Hashtable());
        private static readonly PropertyInfo s_contentTypeProp = typeof(FileResult).GetProperty("ContentType", BindingFlags.NonPublic | BindingFlags.Instance);
        //private static readonly Regex s_webResourceRegex = new WebResourceRegex();

        public ResourceFileStreamResult(Type resourceType, string resourceId)
            : base("plain/text")
        {
            if (resourceType == null)
                throw new ArgumentNullException("resourceType");
            ResourceType = resourceType;
            ResourceId = resourceId;
            string contentType;
            bool performSubstitution;
            FileStream = GetStream(resourceType.Assembly, resourceId, out contentType, out performSubstitution);
            SetContentType(contentType);
            PerformSubstitution = performSubstitution;
        }

        private void SetContentType(string contentType)
        {
            s_contentTypeProp.SetValue(this, contentType, null);
        }

        private Stream GetStream(Assembly assembly, string webResource, out string contentType, out bool performSubstitution)
        {
            int cacheId = HashCodeCombiner.CombineHashCodes(assembly.GetHashCode(), webResource.GetHashCode());
            var triplet = (Triplet)s_cache[cacheId];
            if (triplet == null)
            {
                var attribute = assembly.GetCustomAttributes(false).OfType<WebResourceAttribute>().Where(x => x.WebResource == webResource).FirstOrDefault();
                triplet = (attribute != null ? new Triplet { First = true, Second = attribute.ContentType, Third = attribute.PerformSubstitution } : new Triplet { First = false });
                s_cache[cacheId] = triplet;
            }
            if (!(bool)triplet.First)
                throw new HttpException(0x194, "AssemblyResourceLoader_InvalidRequest");
            contentType = (string)triplet.Second;
            performSubstitution = (bool)triplet.Third;
            return assembly.GetManifestResourceStream(webResource);
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            var outputStream = response.OutputStream;
            using (FileStream)
            {
                var b = new byte[0x1000];
                while (true)
                {
                    var count = FileStream.Read(b, 0, 0x1000);
                    if (count == 0)
                        return;
                    outputStream.Write(b, 0, count);
                }
            }
        }

        //protected void WriteFileWithReplace(HttpResponseBase response)
        //{
        //    string fileStreamAsText;
        //    Encoding fileStreamEncoding;
        //    using (var r = new StreamReader(FileStream, true))
        //    {
        //        fileStreamAsText = r.ReadToEnd();
        //        fileStreamEncoding = r.CurrentEncoding;
        //    }
        //    int startIndex = 0;
        //    var webResource = ResourceId;
        //    var assembly = ResourceType.Assembly;
        //    var b = new StringBuilder();
        //    foreach (Match match in s_webResourceRegex.Matches(fileStreamAsText))
        //    {
        //        b.Append(fileStreamAsText.Substring(startIndex, match.Index - startIndex));
        //        var group = match.Groups["resourceName"];
        //        if (group != null)
        //        {
        //            string a = group.ToString();
        //            if (a.Length > 0)
        //            {
        //                if (string.Equals(a, webResource, StringComparison.Ordinal))
        //                    throw new HttpException(0x194, "AssemblyResourceLoader_NoCircularReferences");
        //                b.Append(GetWebResourceUrlInternal(assembly, a, false));
        //            }
        //        }
        //        startIndex = match.Index + match.Length;
        //    }
        //    b.Append(fileStreamAsText.Substring(startIndex, fileStreamAsText.Length - startIndex));
        //    using (var w = new StreamWriter(response.OutputStream, fileStreamEncoding))
        //        w.Write(b.ToString());
        //}

        //private char[] GetWebResourceUrlInternal(Assembly assembly, string a, bool p)
        //{
        //    throw new NotImplementedException();
        //}

        public Type ResourceType { get; private set; }
        public string ResourceId { get; private set; }
        public Stream FileStream { get; private set; }
        public bool PerformSubstitution { get; private set; }
    }
}