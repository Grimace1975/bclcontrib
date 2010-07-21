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
using System;
using System.Linq;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web;
using System.Web.Routing;
using Digital.ContentManagement.Nodes;
using System.Primitives.TextPacks;
namespace Digital.ContentManagement
{
    public class SiteMapNodeStore<TRouteCreator> : IStaticSiteMapProviderExNodeStore
        where TRouteCreator : ISiteMapNodeStoreRouteCreator, new()
    {
        private string _connect;
        private ReaderWriterLockSlim _rwLock;
        private StaticSiteMapProviderEx _provider;

        #region Observable
        public class Observable : StaticSiteMapProviderEx.ObservableBase
        {
            private ISiteMapNodeStoreRouteCreator _routeCreator;
            private string _connect;
            private StaticSiteMapProviderEx _provider;
            private SiteMapNode _rootNode;

            #region Class Types
            protected class RootOrdinal
            {
                public int Name, Uri;
                public RootOrdinal(IDataReader r)
                {
                    Name = r.GetOrdinal("Name");
                    Uri = r.GetOrdinal("Uri");
                }
            }

            protected class PageOrdinal
            {
                public int Key, Uid, TreeId, Type, Id, Name, SectionId, AttribStream, IsHidden, PageDynamism, LastModifyDate, Virtualize;
                public PageOrdinal(IDataReader r)
                {
                    Key = r.GetOrdinal("Key");
                    Uid = r.GetOrdinal("Uid");
                    TreeId = r.GetOrdinal("TreeId");
                    Type = r.GetOrdinal("Type");
                    Id = r.GetOrdinal("Id");
                    Name = r.GetOrdinal("Name");
                    SectionId = r.GetOrdinal("SectionId");
                    AttribStream = r.GetOrdinal("AttribStream");
                    IsHidden = r.GetOrdinal("IsHidden");
                    PageDynamism = r.GetOrdinal("PageDynamism");
                    LastModifyDate = r.GetOrdinal("LastModifyDate");
                    Virtualize = r.GetOrdinal("Virtualize");
                }
            }
            #endregion

            public Observable(SiteMapNodeStore<TRouteCreator> parent)
            {
                _connect = parent._connect;
                _provider = parent._provider;
            }

            public override SiteMapNode GetRootNode()
            {
                return _rootNode;
            }

            public override IDisposable Subscribe(IObserver<StaticSiteMapProviderEx.NodeToAdd> observer)
            {
                if (observer == null)
                    throw new ArgumentNullException("observer");
                if (string.IsNullOrEmpty(_connect))
                    throw new ProviderException("Empty connection string");
                var nodes = new Dictionary<string, SiteMapNodeEx>(16);
                using (_routeCreator = new TRouteCreator())
                {
                    try
                    {
                        IDbCommand command;
                        using (var connection = Open(_connect, out command))
                        {
                            connection.Open();
                            using (var r = command.ExecuteReader())
                            {
                                var rootOrdinal = new RootOrdinal(r);
                                if (!r.Read())
                                    throw new InvalidOperationException("No Results");
                                // create the root SiteMapNode and add it to the site map
                                string rootName = r.Field<string>(rootOrdinal.Name, "Home");
                                string rootUri = "/" + r.Field<string>(rootOrdinal.Uri, "Index.htm");
                                _rootNode = new SiteMapRootNode(_provider, string.Empty, rootUri, rootName);
                                observer.OnNext(new StaticSiteMapProviderEx.NodeToAdd { Node = _rootNode });
                                // build a tree of SiteMapNodes underneath the root node
                                r.NextResult();
                                var pageOrdinal = new PageOrdinal(r);
                                string hiddenRootTreeId = null;
                                while (r.Read())
                                {
                                    var node = CreateSiteMapNodeFromDataReader(nodes, pageOrdinal, r, ref hiddenRootTreeId);
                                    observer.OnNext(new StaticSiteMapProviderEx.NodeToAdd { Node = node, ParentNode = GetParentNodeFromDataReader(nodes, pageOrdinal, r, false) });
                                }
                            }
                            observer.OnCompleted();
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                        return null;
                    }
                }
            }

            protected virtual IDbConnection Open(string connect, out IDbCommand command)
            {
                var sqlConnection = new SqlConnection(connect);
                var sqlCommand = new SqlCommand("dbo.[cn_Page::SiteMap]", sqlConnection) { CommandType = CommandType.StoredProcedure };
                sqlCommand.Parameters.AddWithValue("cLastModifyBy", "System");
                sqlCommand.Parameters.AddWithValue("nShard", 1);
                sqlCommand.Parameters.AddWithValue("cLocalCultureId", "en-US");
                command = sqlCommand;
                return sqlConnection;
            }

            private static readonly TextPackBase s_xmlTextPack = XmlTextPack.Instance;
            private IDictionary<string, string> BuildAttrib(string attribAsText)
            {
                return (string.IsNullOrEmpty(attribAsText) ? null : s_xmlTextPack.PackDecode(attribAsText));
            }

            private SiteMapNode CreateSiteMapNodeFromDataReader(Dictionary<string, SiteMapNodeEx> nodes, PageOrdinal ordinal, IDataReader r, ref string hiddenRootTreeId)
            {
                if (r.IsDBNull(ordinal.TreeId))
                    throw new ProviderException("Missing node ID");
                string treeId = r.GetString(ordinal.TreeId);
                if (nodes.ContainsKey(treeId))
                    throw new ProviderException("Duplicate node ID");
                bool visible = !r.Field<bool>(ordinal.IsHidden);
                int key = r.Field<int>(ordinal.Key);
                string uid = r.Field<string>(ordinal.Uid);
                string type = r.Field<string>(ordinal.Type);
                string id = r.Field<string>(ordinal.Id);
                string name = r.Field<string>(ordinal.Name);
                string sectionId = r.Field<string>(ordinal.SectionId);
                //var attrib = r.Field<Nattrib>(ordinal.AttribStream);
                var attrib = BuildAttrib(r.Field<string>(ordinal.AttribStream));
                //
                string pageDynamism = r.Field<string>(ordinal.PageDynamism);
                DateTime? lastModifyDate = r.Field<DateTime?>(ordinal.LastModifyDate);
                string virtualize = r.Field<string>(ordinal.Virtualize);
                // hidden branches
                if (!visible)
                {
                    if ((hiddenRootTreeId == null) || (!treeId.StartsWith(hiddenRootTreeId)))
                        hiddenRootTreeId = treeId;
                }
                else if (hiddenRootTreeId != null)
                {
                    if (treeId.StartsWith(hiddenRootTreeId))
                        visible = false;
                    else
                        hiddenRootTreeId = null;
                }
                // make url
                SiteMapNodeEx node;
                switch (type)
                {
                    case "X-Section":
                        node = new SiteMapSectionNode(_provider, uid, "/" + id, name);
                        SetRouteInNode(_routeCreator.CreateRoutes(node, id, virtualize), node);
                        break;
                    case "E-Form":
                        node = new SiteMapFormNode(_provider, uid, "/" + id, name);
                        node.Set<SiteMapNodePartialProviderExtent>(new SiteMapNodePartialProviderExtent());
                        SetRouteInNode(_routeCreator.CreateRoutes(node, id, virtualize), node);
                        break;
                    case "E-ListDetail":
                        node = new SiteMapListDetailNode(_provider, uid, "/" + id, name);
                        node.Set<SiteMapNodePartialProviderExtent>(new SiteMapNodePartialProviderExtent());
                        SetRouteInNode(_routeCreator.CreateRoutes(node, id, virtualize), node);
                        break;
                    case "X-Link":
                        id = StringEx.Axb(sectionId, "/", id);
                        string linkUri;
                        if (!attrib.TryGetValue("LinkUri", out linkUri))
                            linkUri = "/";
                        node = new SiteMapLinkNode(_provider, uid, "/" + id, name) { LinkUri = linkUri };
                        node.Set<Func<IDynamicNode, RouteData>>((nodeEx) =>
                        {
                            var linkNodeEx = (nodeEx as SiteMapLinkNode);
                            if (linkNodeEx != null)
                                HttpContext.Current.Response.Redirect(linkNodeEx.LinkUri);
                            return null;
                        });
                        break;
                    case "X-Content":
                        id = StringEx.Axb(sectionId, "/", id);
                        node = new SiteMapPageNode(_provider, uid, "/" + id, name);
                        SetRouteInNode(_routeCreator.CreateRoutes(node, id, virtualize), node);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                node.Visible = visible;
                // content
                var sectionNode = GetParentNodeFromDataReader(nodes, ordinal, r, true);
                node.Set<SiteMapNodeContentExtent>(new SiteMapNodeContentExtent { Key = key, TreeId = treeId, SectionNode = sectionNode });
                var contentNode = (node as SiteMapPageNode);
                if (contentNode != null)
                {
                    contentNode.LastModifyDate = lastModifyDate;
                    contentNode.PageDynamism = pageDynamism;
                    contentNode.PagePriority = null;
                }
                nodes.Add(treeId, node);
                return node;
            }

            private void SetRouteInNode(IEnumerable<Route> routes, SiteMapNodeEx node)
            {
                DynamicRoute.SetRouteDefaults(routes, node);
                int routes2 = routes.Count();
                if (routes2 == 1)
                    node.Set<Route>(routes.Single());
                else if (routes2 > 0)
                    node.SetMany<Route>(routes);
            }

            private SiteMapNode GetParentNodeFromDataReader(Dictionary<string, SiteMapNodeEx> nodes, PageOrdinal ordinal, IDataReader r, bool findSection)
            {
                if (r.IsDBNull(ordinal.TreeId))
                    throw new ProviderException("Missing parent ID");
                // Get the parent ID from the DataReader
                string parentTreeId = r.GetString(ordinal.TreeId);
                if (parentTreeId.Length == 4)
                    return _rootNode;
                // Make sure the parent ID is valid
                SiteMapNodeEx value;
                parentTreeId = parentTreeId.Substring(0, (!findSection ? parentTreeId.Length - 4 : 4));
                if (!nodes.TryGetValue(parentTreeId, out value)) { }
                //throw new ProviderException("Invalid parent ID");
                return (!findSection ? value : value as SiteMapSectionNode);
            }
        }
        #endregion

        public void Initialize(StaticSiteMapProviderEx provider, ReaderWriterLockSlim rwLock, NameValueCollection config)
        {
            _provider = provider;
            _rwLock = rwLock;
            string connect = config["connectionStringName"];
            if (string.IsNullOrEmpty(connect))
                throw new ProviderException("Empty or missing connectionStringName");
            config.Remove("connectionStringName");
            if (WebConfigurationManager.ConnectionStrings[connect] == null)
                throw new ProviderException("Missing connection string");
            _connect = WebConfigurationManager.ConnectionStrings[connect].ConnectionString;
            if (string.IsNullOrEmpty(_connect))
                throw new ProviderException("Empty connection string");
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (string.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " + attr);
            }
        }

        public StaticSiteMapProviderEx.ObservableBase CreateObservable()
        {
            return new Observable(this);
        }

        public void Clear() { }
    }
}
