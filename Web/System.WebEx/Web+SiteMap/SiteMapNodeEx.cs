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
using System.Patterns.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Routing;
namespace System.Web
{
    /// <summary>
    /// SiteMapNodeEx
    /// </summary>
    public class SiteMapNodeEx : SiteMapNode, IDynamicNode, IExtentsRepository
    {
        public static readonly SiteMapNodeEx Empty = new SiteMapNodeEx(new EmptySiteMapProvider(), string.Empty);
        private static readonly Type s_type = typeof(SiteMapNodeEx);
        private IExtentsRepository _defaultRepository = new ExtentsRepository();
        private Dictionary<Type, IExtentsRepository> _repositories;
        private IHaveVirtualUrlSiteMapNode _haveVirtualUrlSiteMapNode;

        public SiteMapNodeEx(SiteMapProvider provider, string key)
            : base(provider, key) { Visible = true; _haveVirtualUrlSiteMapNode = (this as IHaveVirtualUrlSiteMapNode); }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url)
            : base(provider, key, url) { Visible = true; _haveVirtualUrlSiteMapNode = (this as IHaveVirtualUrlSiteMapNode); }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url, string title)
            : base(provider, key, url, title) { Visible = true; _haveVirtualUrlSiteMapNode = (this as IHaveVirtualUrlSiteMapNode); }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url, string title, string description)
            : base(provider, key, url, title, description) { Visible = true; _haveVirtualUrlSiteMapNode = (this as IHaveVirtualUrlSiteMapNode); }
        public SiteMapNodeEx(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes, NameValueCollection explicitResourceKeys, string implicitResourceKey)
            : base(provider, key, url, title, description, roles, attributes, explicitResourceKeys, implicitResourceKey) { Visible = true; _haveVirtualUrlSiteMapNode = (this as IHaveVirtualUrlSiteMapNode); }

        public bool Visible { get; set; }

        public bool HasExtents
        {
            get { return _defaultRepository.HasExtents; }
        }
        public bool HasExtent<T>() { return _defaultRepository.HasExtent<T>(); }
        public bool HasExtent<T, TShard>() { return GetRepository(typeof(TShard)).HasExtent<T>(); }
        public bool HasExtent(Type type) { return _defaultRepository.HasExtent(type); }
        public void Set<T>(T value) { _defaultRepository.Set<T>(value); }
        public void SetMany<T>(IEnumerable<T> value) { _defaultRepository.SetMany<T>(value); }
        public void Set<T, TShard>(T value) { GetRepository(typeof(TShard)).Set<T>(value); }
        public void SetMany<T, TShard>(IEnumerable<T> value) { GetRepository(typeof(TShard)).SetMany<T>(value); }
        public void Set(Type type, object value) { _defaultRepository.Set(type, value); }
        public void Clear<T>() { _defaultRepository.Clear<T>(); }
        public void Clear<T, TShard>() { GetRepository(typeof(TShard)).Clear<T>(); }
        public void Clear(Type type) { _defaultRepository.Clear(type); }
        public T Get<T>() { return _defaultRepository.Get<T>(); }
        public IEnumerable<T> GetMany<T>() { return _defaultRepository.GetMany<T>(); }
        public T Get<T, TShard>() { return GetRepository(typeof(TShard)).Get<T>(); }
        public IEnumerable<T> GetMany<T, TShard>() { return GetRepository(typeof(TShard)).GetMany<T>(); }
        public object Get(Type type) { return _defaultRepository.Get(type); }

        public IExtentsRepository GetRepository<TShard>() { return GetRepository(typeof(TShard)); }
        public IExtentsRepository GetRepository(Type shard)
        {
            if (shard == null)
                throw new ArgumentException("shard");
            if (shard == s_type)
                return _defaultRepository;
            // repositories
            if (_repositories == null)
                _repositories = new Dictionary<Type, IExtentsRepository>();
            IExtentsRepository repository;
            if (!_repositories.TryGetValue(shard, out repository))
                _repositories.Add(shard, (repository = new ExtentsRepository()));
            return repository;
        }
        public void SetRepository<TShard>(IExtentsRepository repository) { SetRepository(typeof(TShard), repository); }
        public void SetRepository(Type shard, IExtentsRepository repository)
        {
            if (shard == null)
                throw new ArgumentException("shard");
            if (repository == null)
                throw new ArgumentException("repository");
            if (shard == s_type)
                _defaultRepository = repository;
            // repositories
            if (_repositories == null)
                _repositories = new Dictionary<Type, IExtentsRepository>();
            _repositories[shard] = repository;
        }

        public string InteriorUrl
        {
            get { return base.Url; }
            set { base.Url = value; }
        }

        public new string Url
        {
            get { return (_haveVirtualUrlSiteMapNode == null ? base.Url : _haveVirtualUrlSiteMapNode.Url); }
            set
            {
                if (_haveVirtualUrlSiteMapNode == null)
                    base.Url = value;
                else
                    _haveVirtualUrlSiteMapNode.Url = value;
            }
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