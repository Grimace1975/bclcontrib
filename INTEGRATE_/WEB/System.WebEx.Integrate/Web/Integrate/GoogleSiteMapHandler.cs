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
    public class GoogleSiteMapHandler : IHttpHandler
    {
        private static readonly XNamespace _xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var r = context.Response;
            r.ContentType = "text/xml";
            r.Write(CreateSiteMap().ToString());
        }

        private XDocument CreateSiteMap()
        {
            var siteMap = new XElement(_xmlns + "urlset");
            AddSiteMapNode(SiteMap.RootNode, siteMap);
            //
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            doc.Add(siteMap);
            return doc;
        }

        private void AddSiteMapNode(SiteMapNode node, XElement parent)
        {
            parent.Add(CreateSiteMapNode(node));
            foreach (SiteMapNode node2 in node.ChildNodes)
            {
                var pageNode = (node2 as SiteMapPageNode);
                if (pageNode != null)
                    AddSiteMapNode(pageNode, parent);
                else
                    AddSiteMapNode(node2, parent);
            }
        }

        private XElement CreateSiteMapNode(SiteMapNode node)
        {
            var e = new XElement(_xmlns + "url",
                new XElement(_xmlns + "loc", SecurityElement.Escape(node.Url))
            );
            return e;
        }

        private XElement CreateSiteMapNode(SiteMapPageNode node)
        {
            var e = new XElement(_xmlns + "url",
                new XElement(_xmlns + "loc", SecurityElement.Escape(node.Url)),
                new XElement(_xmlns + "lastmod", node.LastModifyDate),
                new XElement(_xmlns + "changefreq", node.PageDynamism),
                new XElement(_xmlns + "priority", node.PagePriority)
            );
            return e;
        }
    }
}