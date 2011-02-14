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
using System.Xml.Linq;
using System.Security;
using Digital.ContentManagement.Nodes;
namespace System.Web.Integrate
{
    /// <summary>
    /// GoogleSiteMapHandler
    /// </summary>
    public class GoogleSiteMapHandler : IObservable<GoogleSiteMapNode>, IHttpHandler
    {
        private SiteMapNode _rootNode;

        public GoogleSiteMapHandler()
            : this(SiteMap.RootNode) { }
        public GoogleSiteMapHandler(SiteMapNode rootNode)
        {
            _rootNode = rootNode;
            ContentType = "text/xml";
        }

        public void ProcessRequest(HttpContext context)
        {
            var r = context.Response;
            if (!string.IsNullOrEmpty(ContentType))
                r.ContentType = ContentType;
            Subscribe(new GoogleSiteMapObserver(r.Output));
        }

        public string ContentType { get; set; }

        public bool IsReusable
        {
            get { return true; }
        }

        public IDisposable Subscribe(IObserver<GoogleSiteMapNode> observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer");
            try
            {
                AddSiteMapNode(observer, _rootNode);
                observer.OnCompleted();
            }
            catch (Exception ex) { observer.OnError(ex); }
            return null;
        }

        private void AddSiteMapNode(IObserver<GoogleSiteMapNode> observer, SiteMapNode node)
        {
            observer.OnNext(CreateSiteMapNode(node));
            foreach (SiteMapNode childNode in node.ChildNodes)
                AddSiteMapNode(observer, childNode);
        }

        private GoogleSiteMapNode CreateSiteMapNode(SiteMapNode node)
        {
            var pageNode = (node as SiteMapPageNode);
            return (pageNode != null ? new GoogleSiteMapNode
            {
                Url = pageNode.Url,
                LastModifyDate = pageNode.LastModifyDate,
                PageDynamism = pageNode.PageDynamism,
                PagePriority = pageNode.PagePriority,
            } : new GoogleSiteMapNode { Url = node.Url });
        }
    }
}