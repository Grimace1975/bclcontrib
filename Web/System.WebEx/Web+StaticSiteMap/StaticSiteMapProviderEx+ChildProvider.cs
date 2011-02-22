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
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Collections;
namespace System.Web
{
    public partial class StaticSiteMapProviderEx
    {
        private List<SiteMapProvider> _childProviders;
        private Dictionary<SiteMapProvider, SiteMapNode> _childProviderRootNodes;
        //private ReaderWriterLockSlim _childProviderRwLock = new ReaderWriterLockSlim();

        private SiteMapNode FindSiteMapNodeFromChildProvider(string rawUrl)
        {
            foreach (var provider in ChildProviders)
            {
                EnsureChildSiteMapProviderUpToDate(provider);
                var node = provider.FindSiteMapNode(rawUrl);
                if (node != null)
                    return node;
            }
            return null;
        }

        protected bool HasChildProviders
        {
            get { return !EnumerableEx.IsNullOrEmpty(ChildProviders); }
        }

        private SiteMapNode FindSiteMapNodeFromChildProviderKey(string key)
        {
            foreach (var provider in ChildProviders)
            {
                EnsureChildSiteMapProviderUpToDate(provider);
                var node = provider.FindSiteMapNodeFromKey(key);
                if (node != null)
                    return node;
            }
            return null;
        }

        public void AddProvider(string providerName, SiteMapNode parentNode) { AddProvider(providerName, parentNode, null); }
        public void AddProvider(string providerName, SiteMapNode parentNode, Action<SiteMapNode> rebaseAction)
        {
            if (parentNode == null)
                throw new ArgumentNullException("parentNode");
            if (parentNode.Provider != this)
                throw new ArgumentException(string.Format("StaticSiteMapProviderEx_cannot_add_node", parentNode.ToString()), "parentNode");
            var nodeFromProvider = GetNodeFromProvider(providerName);
            if (rebaseAction != null)
                rebaseAction(nodeFromProvider);
            AddNode(nodeFromProvider, parentNode);
        }

        private SiteMapNode GetNodeFromProvider(string providerName)
        {
            var providerFromName = GetProviderFromName(providerName);
            var rootNode = providerFromName.RootNode;
            if (rootNode == null)
                throw new InvalidOperationException(string.Format("StaticSiteMapProviderEx_invalid_GetRootNodeCore", providerFromName.Name));
            if (!ChildProviderRootNodes.ContainsKey(providerFromName))
            {
                ChildProviderRootNodes.Add(providerFromName, rootNode);
                _childProviders = null;
                providerFromName.ParentProvider = this;
            }
            return rootNode;
        }

        protected override void RemoveNode(SiteMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            var provider = node.Provider;
            if (provider != this)
                for (var parentProvider = provider.ParentProvider; parentProvider != this; parentProvider = parentProvider.ParentProvider)
                    if (parentProvider == null)
                        throw new InvalidOperationException(string.Format("StaticSiteMapProviderEx_cannot_remove_node", node.ToString(), Name, provider.Name));
            if (node.Equals(provider.RootNode))
                throw new InvalidOperationException("StaticSiteMapProviderEx_cannot_remove_root_node");
            if (provider != this)
                _providerRemoveNode.Invoke(provider, new[] { node });
            base.RemoveNode(node);
        }

        protected virtual void RemoveProvider(string providerName)
        {
            if (providerName == null)
                throw new ArgumentNullException("providerName");
            lock (_baseLock)
            {
                var providerFromName = GetProviderFromName(providerName);
                var node = ChildProviderRootNodes[providerFromName];
                if (node == null)
                    throw new InvalidOperationException(string.Format("StaticSiteMapProviderEx_cannot_find_provider", providerFromName.Name, Name));
                providerFromName.ParentProvider = null;
                ChildProviderRootNodes.Remove(providerFromName);
                _childProviders = null;
                base.RemoveNode(node);
            }
        }

        private List<SiteMapProvider> ChildProviders
        {
            get
            {
                if (_childProviders == null)
                    lock (_baseLock)
                        if (_childProviders == null)
                            _childProviders = new List<SiteMapProvider>(ChildProviderRootNodes.Keys);
                return _childProviders;
            }
        }

        private Dictionary<SiteMapProvider, SiteMapNode> ChildProviderRootNodes
        {
            get
            {
                if (_childProviderRootNodes == null)
                    lock (_baseLock)
                        if (_childProviderRootNodes == null)
                            _childProviderRootNodes = new Dictionary<SiteMapProvider, SiteMapNode>();
                return _childProviderRootNodes;
            }
        }

        private void EnsureChildSiteMapProviderUpToDate(SiteMapProvider childProvider)
        {
            var node = ChildProviderRootNodes[childProvider];
            if (node == null)
                return;
            var rootNode = childProvider.RootNode;
            if (rootNode == null)
                throw new InvalidOperationException(string.Format("XmlSiteMapProvider_invalid_sitemapnode_returned", childProvider.Name));
            if (!node.Equals(rootNode))
                lock (_baseLock)
                {
                    node = ChildProviderRootNodes[childProvider];
                    if (node != null)
                    {
                        rootNode = childProvider.RootNode;
                        if (rootNode == null)
                            throw new InvalidOperationException(string.Format("XmlSiteMapProvider_invalid_sitemapnode_returned", childProvider.Name));
                        if (!node.Equals(rootNode))
                        {
                            // double-lock reached
                            if (_rootNode.Equals(node))
                            {
                                RemoveNode(node);
                                AddNode(rootNode);
                                _rootNode = rootNode;
                            }
                            //var node3 = (SiteMapNode)base.ParentNodeTable[node];
                            //if (node3 != null)
                            //{
                            //    var nodes = (SiteMapNodeCollection)base.ChildNodeCollectionTable[node3];
                            //    int index = nodes.IndexOf(node);
                            //    if (index != -1)
                            //    {
                            //        nodes.Remove(node);
                            //        nodes.Insert(index, rootNode);
                            //    }
                            //    else
                            //        nodes.Add(rootNode);
                            //    base.ParentNodeTable[rootNode] = node3;
                            //    base.ParentNodeTable.Remove(node);
                            //    RemoveNode(node);
                            //    AddNode(rootNode);
                            //}
                            //else
                            //{
                            //    var parentProvider = (ParentProvider as StaticSiteMapProviderEx);
                            //    if (parentProvider != null)
                            //        parentProvider.EnsureChildSiteMapProviderUpToDate(this);
                            //}
                            ChildProviderRootNodes[childProvider] = rootNode;
                            _childProviders = null;
                        }
                    }
                }
        }
    }
}