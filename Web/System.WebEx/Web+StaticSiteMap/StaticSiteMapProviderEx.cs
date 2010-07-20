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
using System.Threading;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.Routing;
namespace System.Web
{
    /// <summary>
    /// StaticSiteMapProviderEx
    /// </summary>
    public partial class StaticSiteMapProviderEx : StaticSiteMapProvider, ISiteMapProvider, IDynamicRoutingContext
    {
        private IStaticSiteMapProviderExNodeStore _nodeStore;
        private ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        private SiteMapNode _rootNode;
        private object _baseLock = typeof(SiteMapProvider).GetField("_lock", BindingFlags.NonPublic | BindingFlags.Instance);
        private MethodInfo _providerRemoveNode = typeof(SiteMapProvider).GetMethod("RemoveNode", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (string.IsNullOrEmpty(name))
                name = "StaticSiteMapProviderEx";
            string nodeStoreType = config["nodeStoreType"];
            if (string.IsNullOrEmpty(nodeStoreType))
                throw new ProviderException("config:nodeStoreType");
            // Add a default "description" attribute to config if the attribute doesn’t exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "static site map provider");
            }
            base.Initialize(name, config);
            _nodeStore = (IStaticSiteMapProviderExNodeStore)Activator.CreateInstance(Type.GetType(nodeStoreType));
            //if (_nodeStore == null)
            //    throw new ProviderException("Invalid Node Store");
            _nodeStore.Initialize(this, _rwLock, config);
            // SiteMapProvider processes the securityTrimmingEnabled attribute but fails to remove it. Remove it now so we can check for unrecognized configuration attributes.
            if (config["securityTrimmingEnabled"] != null)
                config.Remove("securityTrimmingEnabled");
        }

        private static SiteMapProvider GetProviderFromName(string providerName)
        {
            if (providerName == null)
                // StubSiteMapProvider
                return null;
            var provider = SiteMap.Providers[providerName];
            if (provider == null)
                throw new ProviderException(string.Format("Provider_Not_Found", providerName));
            return provider;
        }

        public TNode FindSiteMapNode<TNode>(string rawUrl)
            where TNode : SiteMapNode { return (TNode)FindSiteMapNode(rawUrl); }
        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            if (rawUrl == null)
                throw new ArgumentNullException("rawUrl");
            // check end of url
            var rawUrlLength = rawUrl.Length;
            if ((rawUrlLength > 1) && (rawUrl.EndsWithSlim("/")))
                rawUrl = rawUrl.Substring(0, rawUrlLength - 1);
            // locks handled by BuildSiteMap
            var node = base.FindSiteMapNode(rawUrl);
            if (node != null)
                return node;
            // child providers
            if (HasChildProviders)
            {
                node = FindSiteMapNodeFromChildProvider(rawUrl);
                if (node != null)
                    return node;
            }
            // get url segments
            rawUrl = rawUrl.Trim();
            string queryPart;
            string[] segments = GetUrlSegments(rawUrl, out queryPart);
            // tightest for partialproviders
            if (HasPartialProviders)
            {
                node = FindSiteMapNodeFromPartialProvider(segments);
                if (node != null)
                    return node;
            }
            // scan for node			
            node = FindSiteMapNodeFromScan(segments);
            SiteMapNodeEx nodeEx;
            if ((node != null) && ((nodeEx = (node as SiteMapNodeEx)) != null) && (nodeEx.HasExtents))
                node = ExNodeFoundFromScan(nodeEx, segments);
            return node;
        }

        private static string[] GetUrlSegments(string rawUrl, out string queryPart)
        {
            if (string.IsNullOrEmpty(rawUrl))
                throw new ArgumentNullException("rawUrl");
            rawUrl = rawUrl.Substring(1);
            int index = rawUrl.IndexOf("?", StringComparison.Ordinal);
            if (index == -1)
            {
                queryPart = null;
                return rawUrl.Split('/');
            }
            queryPart = rawUrl.Substring(index);
            return rawUrl.Substring(0, index).Split('/');
        }

        public TNode FindSiteMapNode<TNode>(HttpContext context)
            where TNode : SiteMapNode { return (TNode)FindSiteMapNode(context); }
        public override SiteMapNode FindSiteMapNode(HttpContext context)
        {
            return base.FindSiteMapNode(context);
        }

        public TNode FindSiteMapNodeFromKey<TNode>(string key)
            where TNode : SiteMapNode { return (TNode)FindSiteMapNodeFromKey(key); }
        public override SiteMapNode FindSiteMapNodeFromKey(string key)
        {
            // locks handled by BuildSiteMap
            var node = base.FindSiteMapNodeFromKey(key);
            if (node != null)
                return node;
            return FindSiteMapNodeFromChildProviderKey(key);
        }

        protected virtual SiteMapNode ExNodeFoundFromScan(SiteMapNodeEx nodeEx, string[] segments)
        {
            // locks handled by FindSiteMapNode
            var node = CheckExNodeFromScanAsPartialProvider(nodeEx, segments);
            return (node ?? nodeEx);
        }

        public override SiteMapNode BuildSiteMap()
        {
            _rwLock.EnterUpgradeableReadLock();
            // return immediately if this method has been called before
            if (_rootNode != null)
            {
                _rwLock.ExitUpgradeableReadLock();
                return _rootNode;
            }
            _rwLock.EnterWriteLock();
            //
            try
            {
                // clear - if Subscribe throws it will rebuild next request, make fresh.
                base.Clear();
                _nodeStore.Clear();
                _rootNode = null;
                // build
                var observable = _nodeStore.CreateObservable();
                observable.Subscribe(new Observer(this));
                _rootNode = observable.GetRootNode();
                // still in lock
                // if site map build fails then set to try next request
                try
                {
                    OnSiteMapBuilt();
                }
                catch { _rootNode = null; }
            }
            finally
            {
                _rwLock.ExitWriteLock();
                _rwLock.ExitUpgradeableReadLock();
            }
            return _rootNode;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            // locks handled by BuildSiteMap
            return BuildSiteMap();
        }

        protected override void AddNode(SiteMapNode node, SiteMapNode parentNode)
        {
            // assume in locked region
            if (!(node is IUnconstrainSiteMapNode))
            {
                base.AddNode(node, parentNode);
                return;
            }
            string lastUrl = node.Url;
            node.Url = string.Empty;
            base.AddNode(node, parentNode);
            node.Url = lastUrl;
        }

        protected override void Clear()
        {
            _rwLock.EnterWriteLock();
            try
            {
                base.Clear();
                _nodeStore.Clear();
                _rootNode = null;
            }
            finally { _rwLock.ExitWriteLock(); }
        }

        void ISiteMapProvider.Clear()
        {
            Clear();
        }

        protected virtual void OnSiteMapBuilt()
        {
            var siteMapBuild = SiteMapBuilt;
            if (siteMapBuild != null)
                siteMapBuild(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> SiteMapBuilt;

        public TNode GetParentNode<TNode>(SiteMapNode node)
            where TNode : SiteMapNode { return (TNode)GetParentNode(node); }

        #region DynamicRoutingContext

        IDynamicNode IDynamicRoutingContext.FindNode(string path)
        {
            return (FindSiteMapNode(path) as IDynamicNode);
        }

        IDynamicNode IDynamicRoutingContext.FindNodeById(string id)
        {
            return (FindSiteMapNodeFromKey(id) as IDynamicNode);
        }

        #endregion
    }
}