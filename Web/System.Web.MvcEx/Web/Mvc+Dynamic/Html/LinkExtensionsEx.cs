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
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, null, new RouteValueDictionary(), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, null, new RouteValueDictionary(), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary()); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, object routeValues) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, object routeValues) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, object routeValues) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, object routeValues) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary()); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, string dynamicId) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, dynamicId, new RouteValueDictionary(), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, string dynamicId) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, dynamicId, new RouteValueDictionary(), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, dynamicId, new RouteValueDictionary(), new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, string dynamicId) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, dynamicId, new RouteValueDictionary(), new RouteValueDictionary()); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, null, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, RouteValueDictionary routeValues) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, null, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, RouteValueDictionary routeValues) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, null, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, RouteValueDictionary routeValues) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, null, routeValues, new RouteValueDictionary()); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, null, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, null, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, null, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, null, routeValues, htmlAttributes); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, string dynamicId, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, dynamicId, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, dynamicId, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, dynamicId, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, dynamicId, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, dynamicId, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, dynamicId, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, dynamicId, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return MvcHtmlString.Create(HtmlHelperEx.GenerateDynamicLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, routingContext, linkText, null, actionName, dynamicId, routeValues, htmlAttributes));
        }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, string dynamicId, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, dynamicId, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, dynamicId, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, dynamicId, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, linkText, actionName, dynamicId, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }

        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, actionName, dynamicId, protocol, hostName, fragment, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, (IDynamicRoutingContext)null, linkText, actionName, dynamicId, protocol, hostName, fragment, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicActionLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, actionName, dynamicId, protocol, hostName, fragment, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicActionLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return MvcHtmlString.Create(HtmlHelperEx.GenerateDynamicLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, routingContext, linkText, null, actionName, dynamicId, protocol, hostName, fragment, routeValues, htmlAttributes));
        }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, object routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, object routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, object routeValues) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, object routeValues) { return DynamicRouteLink(htmlHelper, routingContext, linkText, new RouteValueDictionary(routeValues)); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, null); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, null); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, null); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName) { return DynamicRouteLink(htmlHelper, routingContext, linkText, routeName, null); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, routingContext, linkText, routeValues, new RouteValueDictionary()); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, linkText, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName, object routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName, object routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName, object routeValues) { return DynamicRouteLink(htmlHelper, routingContext, linkText, routeName, new RouteValueDictionary(routeValues)); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, routeValues, new RouteValueDictionary()); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName, RouteValueDictionary routeValues) { return DynamicRouteLink(htmlHelper, routingContext, linkText, routeName, routeValues, new RouteValueDictionary()); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, null, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, null, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, null, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, linkText, null, routeValues, htmlAttributes); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, linkText, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            throw new NotSupportedException();
            //IDynamicNode node = null;
            //string nodeAsLinkText = (linkText == null ? node.Title : linkText(node));
            //if (string.IsNullOrEmpty(nodeAsLinkText))
            //    throw new ArgumentNullException("linkText");
            //return MvcHtmlString.Create(HtmlHelperEx.DynamicGenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, routingContext, nodeAsLinkText, routeName, routeValues, htmlAttributes));
        }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, linkText, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes)); }

        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, (Func<IDynamicNode, string>)null, routeName, protocol, hostName, fragment, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, Func<IDynamicNode, string> linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, (IDynamicRoutingContext)null, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicRouteLink(htmlHelper, routingContext, (Func<IDynamicNode, string>)null, routeName, protocol, hostName, fragment, routeValues, htmlAttributes); }
        public static MvcHtmlString DynamicRouteLink(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, Func<IDynamicNode, string> linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            throw new NotSupportedException();
            //IDynamicNode node = null;
            //string nodeAsLinkText = (linkText == null ? node.Title : linkText(node));
            //if (string.IsNullOrEmpty(nodeAsLinkText))
            //    throw new ArgumentNullException("linkText");
            //return MvcHtmlString.Create(HtmlHelperEx.DynamicGenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, routingContext, nodeAsLinkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes));
        }
    }
}
