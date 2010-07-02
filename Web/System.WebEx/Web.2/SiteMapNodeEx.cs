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
using System.Collections.Generic;
using System.Collections.Specialized;
namespace System.Web
{
    /// <summary>
    /// SiteMapNodeEx
    /// </summary>
    public class SiteMapNodeEx : SiteMapNode
    {
        public static readonly SiteMapNodeEx Empty = new SiteMapNodeEx(new EmptySiteMapProvider(), string.Empty);
        private Dictionary<Type, object> _extents;

        public SiteMapNodeEx(SiteMapProvider provider, string key)
            : base(provider, key) { Visible = true; }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url)
            : base(provider, key, url) { Visible = true; }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url, string title)
            : base(provider, key, url, title) { Visible = true; }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url, string title, string description)
            : base(provider, key, url, title, description) { Visible = true; }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes, NameValueCollection explicitResourceKeys, string implicitResourceKey)
            : base(provider, key, url, title, description, roles, attributes, explicitResourceKeys, implicitResourceKey) { Visible = true; }

        public bool Visible { get; set; }

        public bool HasExtents
        {
            get { return ((_extents != null) && (_extents.Count > 0)); }
        }

        public void Set<T>(T value)
        {
            if (_extents == null)
                _extents = new Dictionary<Type, object>();
            _extents[typeof(T)] = value;
        }

        public void Clear<T>()
        {
            if (_extents != null)
                _extents.Remove(typeof(T));
        }

        public T Get<T>()
        {
            object value;
            return ((_extents == null) || (!_extents.TryGetValue(typeof(T), out value)) ? default(T) : (T)value);
        }

        #region EmptySiteMapProvider
        private class EmptySiteMapProvider : SiteMapProvider
        {
            public override SiteMapNode FindSiteMapNode(string rawUrl)
            {
                return null;
            }

            public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
            {
                return null;
            }

            public override SiteMapNode GetParentNode(SiteMapNode node)
            {
                return null;
            }

            protected override SiteMapNode GetRootNodeCore()
            {
                return null;
            }
        }
        #endregion
    }
}