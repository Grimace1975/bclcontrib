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
using System.Patterns.Schema.Configuration;
using System.Collections.Generic;
using System.Collections;
namespace System.Patterns.Schema
{
    /// <summary>
    /// UriVirtualSchema
    /// </summary>

    public class _UriVirtualSchema : UriVirtualSchemaBase
    {
        private bool _isBound;

        public _UriVirtualSchema()
        {
            Virtuals = new List<UriVirtual>();
        }
        public _UriVirtualSchema(UriVirtualConfigurationSet c)
            : this() { }

        public bool EnableExternAll { get; set; }

        public override IList<UriVirtual> Virtuals { get; protected set; }

        public Func<string, string> ExternGetFilePath { get; set; }

        public Uri ApplicationUri { get; set; }

        public override Uri GetVirtualUri(string uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            // get paths
            var virtuals = Virtuals;
            if ((virtuals == null) || (virtuals.Count == 0))
                return new Uri(uri);
            // find tightest match
            UriVirtual tightestValue;
            if (!virtuals.TryGetTightestMatch(uri, '/', out tightestValue))
                return new Uri(uri);
            int tightestValueLength = tightestValue.Value.Length;
            uri = (uri.Length > tightestValueLength ? uri.Substring(tightestValueLength + 1) : string.Empty);
            return new Uri(ApplicationUri, tightestValue.Path + uri);
        }

        public override string GetFilePath(string uri)
        {
            if (!EnableExternAll)
                return ExternGetFilePath(uri);
            // extract path from url if http
            if (uri.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                int urlPathIndex = uri.Substring(7).IndexOf("/");
                uri = (urlPathIndex > -1 ? uri.Substring(urlPathIndex + 7) : "/");
            }
            // get paths
            var virtuals = Virtuals;
            if ((virtuals == null) || (virtuals.Count == 0))
                return uri;
            // find tightest match
            UriVirtual tightestValue;
            if (!virtuals.TryGetTightestMatch(uri, '/', out tightestValue))
            {
                //// not a virtual directory, use default relative path
                //if (directoryPath.Length == 0)
                //{
                //    absoutePath = ExternMapPath("/" + uri);
                //}
                //else
                //{
                //    uri = uri.Substring(0, uri.Length - 1).Replace("/", "\\");
                //    absoutePath = directoryPath + virtualDirectoryHash.DefaultRelativePath + uri;
                //}
                return new Uri(ApplicationUri, uri).GetEnsuredFilePath(true);
            }
            if (tightestValue.UseExternGetFilePath)
                return ExternGetFilePath(uri);
            //// use a virtual directory relative path
            //uri = uri.Substring(0, uri.Length - 1).Replace("/", "\\");
            //if (directoryPath.Length == 0)
            //{
            //    directoryPath = AbsolutePath + "\\..\\";
            //}
            int tightestValueLength = tightestValue.Value.Length;
            uri = (uri.Length > tightestValueLength ? uri.Substring(tightestValueLength + 1) : string.Empty);
            return new Uri(ApplicationUri, tightestValue.Path + uri).GetEnsuredFilePath(true);
        }

        #region FluentConfig
        public override UriVirtualSchemaBase MakeReadOnly()
        {
            _isBound = true;
            Virtuals = ((List<UriVirtual>)Virtuals).AsReadOnly();
            return this;
        }

        public override UriVirtualSchemaBase AddVirtual(UriVirtual uriVirtual)
        {
            if (_isBound)
                throw new InvalidOperationException("_isBound");
            Virtuals.Add(uriVirtual);
            return this;
        }
        #endregion
    }
}
