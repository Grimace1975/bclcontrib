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
using System.Collections.Generic;
using System.Text;
namespace System.Patterns.Schema
{
	/// <summary>
	/// UriSchema
	/// </summary>
    public class UriSchema : UriSchemaBase
	{
		private bool _isBound;

		public UriSchema()
		{
			Parts = new Dictionary<Type, UriPartBase>();
            //OnOverflow = delegate(IUriPartScanner scanner)
            //{
            //    throw new Exception("UriSchema: Overflow");
            //};
		}
        public UriSchema(Configuration.UriSchemaConfiguration c)
			: this() { }

        public override string CreatePath(UriContextBase context, string uri, Nattrib attrib)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (!uri.StartsWith("/"))
                throw new ArgumentException(Local.InvalidUri);
            if (!_isBound)
                throw new InvalidOperationException("!_isBound");
            return uri;
        }
        public override Uri CreateUri(UriContextBase context, Uri uri, Nattrib attrib)
		{
			if (uri == null)
				throw new ArgumentNullException("uri");
            //if (!uri.StartsWith("/"))
            //    throw new ArgumentException(Local.InvalidUri);
			if (!_isBound)
				throw new InvalidOperationException("!_isBound");
			//bool hasAttrib = ((attrib != null) && (attrib.Count > 0));
			////+ check caching
			//bool allowUriCaching = ((AllowCaching) && (!hasAttrib));
			//if (allowUriCaching)
			//{
			//    //if (m_isKeyListDirty)
			//    //{
			//    //    m_isKeyListDirty = false;
			//    //    m_urlCache = null;
			//    //}
			//    //else if (m_urlCache != null)
			//    //{
			//    //    return m_urlCache + url.Substring(1);
			//    //}
			//}
			////+ create uri
			//bool isUrlRoot;
			//bool isUrlState;
			//if (hasAttrib)
			//{
			//    isUrlRoot = !attrib.SliceBit(CreateUrlAttrib._NoUrlRoot);
			//    isUrlState = !attrib.SliceBit(CreateUrlAttrib._NoUrlState);
			//}
			//else
			//{
			//    isUrlRoot = true;
			//    isUrlState = true;
			//}
			//System.Text.StringBuilder textBuilder = new System.Text.StringBuilder(isUrlRoot ? m_urlRoot : "/");
			//foreach (System.Collections.Generic.KeyValuePair<string, UriStateFragmentBase> stateFragmentPair in StateFragments)
			//{
			//    UriStateFragmentBase fragment = stateFragmentPair.Value;
			//    string fragmentKey = stateFragmentPair.Key;
			//    string attribValue;
			//    textBuilder.Append(fragment.CreateUriFragment(null, (!hasAttrib) || (!attrib.TryGetValue(fragmentKey, out attribValue)) ? null : attribValue));
			//}
			//string createdUrl = textBuilder.ToString();
			//if (allowUriCaching)
			//{
			//    //m_urlCache = createdUrl;
			//}
			//return createdUrl + uri.Substring(1);
			var b = new UriBuilder();
			return b.Uri;
		}
        public override string CreateVirtualPath(UriContextBase context, string virtualPath, Nattrib attrib)
        {
            if (virtualPath == null)
                throw new ArgumentNullException("virtualPath");
            if (!_isBound)
                throw new InvalidOperationException("!_isBound");
            var contextParts = context.Parts;
            var parts = new List<UriPartBase>(Parts.Values);
            var b = new StringBuilder();
            for (int partIndex = 0; partIndex < parts.Count; partIndex++)
                b.Append(parts[partIndex].CreateUriPart(ref contextParts[partIndex], null));
            b.Append(virtualPath);
            return b.ToString();
        }

		public override UriContextBase ParseUri(Uri uri)
		{
			if (!_isBound)
				throw new InvalidOperationException("!_isBound");
            var context = new _UriContext(this, uri);
			var scanner = (IUriPartScanner)context;
			var contextParts = context.Parts;
			var parts = new List<UriPartBase>(Parts.Values);
			for (int partIndex = 0; partIndex < parts.Count; partIndex++)
				parts[partIndex].ParseUriPart(ref contextParts[partIndex], scanner);
			scanner.OnComplete();
			return context;
		}

		public override Dictionary<Type, UriPartBase> Parts { get; protected set; }

        #region FluentConfig
        public override UriSchemaBase MakeReadOnly()
		{
			_isBound = true;
			foreach (var part in Parts.Values)
				part.IsBound = true;
			return this;
		}

		public override UriSchemaBase AddUriPart<T>(T part)
		{
			if (_isBound)
				throw new InvalidOperationException("_isBound");
			Parts.Add(typeof(T), part);
			return this;
        }
        #endregion
    }
}
