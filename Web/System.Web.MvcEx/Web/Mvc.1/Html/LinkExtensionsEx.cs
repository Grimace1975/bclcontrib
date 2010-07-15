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
using System.Web.Routing;
using System.Collections.Generic;
namespace System.Web.Mvc.Html
{
    /// <summary>
    /// LinkExtensionsEx
    /// </summary>
    public static class LinkExtensionsEx
    {
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, string actionName) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, (Func<SiteMapNode, string>)null, actionName); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, string actionName) { return SiteMapActionLink(htmlHelper, siteMap, cid, (Func<SiteMapNode, string>)null, actionName); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, linkTextBuilder, actionName); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName)
        {
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            var node = siteMap.FindSiteMapNodeFromKey(cid);
            if (node == null)
                throw new ArgumentNullException("Node");
            string linkText = (linkTextBuilder == null ? node.Title : linkTextBuilder(node));
            //
            var routeValues2 = new RouteValueDictionary();
            routeValues2.Add("cid", cid);
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues2, new RouteValueDictionary());
        }

        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, string actionName, object routeValues) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, (Func<SiteMapNode, string>)null, actionName, routeValues); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, string actionName, object routeValues) { return SiteMapActionLink(htmlHelper, siteMap, cid, (Func<SiteMapNode, string>)null, actionName, routeValues); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, object routeValues) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, linkTextBuilder, actionName, routeValues); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, object routeValues)
        {
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            var node = siteMap.FindSiteMapNodeFromKey(cid);
            if (node == null)
                throw new ArgumentNullException("Node");
            string linkText = (linkTextBuilder == null ? node.Title : linkTextBuilder(node));
            //
            var routeValues2 = new RouteValueDictionary(routeValues);
            routeValues2.Add("cid", cid);
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues2, new RouteValueDictionary());
        }

        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, string actionName, RouteValueDictionary routeValues) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, (Func<SiteMapNode, string>)null, actionName, routeValues, routeValues); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, string actionName, RouteValueDictionary routeValues) { return SiteMapActionLink(htmlHelper, siteMap, cid, (Func<SiteMapNode, string>)null, actionName, routeValues, routeValues); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, RouteValueDictionary routeValues) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, linkTextBuilder, actionName, routeValues, routeValues); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, RouteValueDictionary routeValues)
        {
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            var node = siteMap.FindSiteMapNodeFromKey(cid);
            if (node == null)
                throw new ArgumentNullException("Node");
            string linkText = (linkTextBuilder == null ? node.Title : linkTextBuilder(node));
            //
            var routeValues2 = new RouteValueDictionary(routeValues);
            routeValues2.Add("cid", cid);
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues2, new RouteValueDictionary());
        }

        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, string actionName, object routeValues, object htmlAttributes) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, (Func<SiteMapNode, string>)null, actionName, routeValues, htmlAttributes); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, string actionName, object routeValues, object htmlAttributes) { return SiteMapActionLink(htmlHelper, siteMap, cid, (Func<SiteMapNode, string>)null, actionName, routeValues, htmlAttributes); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, object routeValues, object htmlAttributes) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, linkTextBuilder, actionName, routeValues, htmlAttributes); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, object routeValues, object htmlAttributes)
        {
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            var node = siteMap.FindSiteMapNodeFromKey(cid);
            if (node == null)
                throw new ArgumentNullException("Node");
            string linkText = (linkTextBuilder == null ? node.Title : linkTextBuilder(node));
            //
            var routeValues2 = new RouteValueDictionary(routeValues);
            routeValues2.Add("cid", cid);
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues2, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, (Func<SiteMapNode, string>)null, actionName, routeValues, htmlAttributes); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return SiteMapActionLink(htmlHelper, siteMap, cid, (Func<SiteMapNode, string>)null, actionName, routeValues, htmlAttributes); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return SiteMapActionLink(htmlHelper, (SiteMapProvider)null, cid, linkTextBuilder, actionName, routeValues, htmlAttributes); }
        public static MvcHtmlString SiteMapActionLink(this HtmlHelper htmlHelper, SiteMapProvider siteMap, string cid, Func<SiteMapNode, string> linkTextBuilder, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            var node = siteMap.FindSiteMapNodeFromKey(cid);
            if (node == null)
                throw new ArgumentNullException("Node");
            string linkText = (linkTextBuilder == null ? node.Title : linkTextBuilder(node));
            //
            var routeValues2 = new RouteValueDictionary(routeValues);
            routeValues2.Add("cid", cid);
            return htmlHelper.ActionLink(linkText, actionName, null, routeValues2, htmlAttributes);
        }
    }
}
