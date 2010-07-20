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
namespace System.Web.Mvc
{
    /// <summary>
    /// HtmlHelperEx
    /// </summary>
    public class HtmlHelperEx
    {
        public static string GenerateDynamicLink(RequestContext requestContext, RouteCollection routeCollection, IDynamicRoutingContext routingContext, string linkText, string routeName, string actionName, string dynamicId, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return GenerateDynamicLink(requestContext, routeCollection, routingContext, linkText, routeName, actionName, dynamicId, null, null, null, routeValues, htmlAttributes); }

        public static string GenerateDynamicLink(RequestContext requestContext, RouteCollection routeCollection, IDynamicRoutingContext routingContext, string linkText, string routeName, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicGenerateLinkInternal(requestContext, routeCollection, routingContext, linkText, routeName, actionName, dynamicId, protocol, hostName, fragment, routeValues, htmlAttributes, true); }

        private static string DynamicGenerateLinkInternal(RequestContext requestContext, RouteCollection routeCollection, IDynamicRoutingContext routingContext, string linkText, string routeName, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues)
        {
            string text = UrlHelperEx.DynamicGenerateUrl(routingContext, routeName, actionName, dynamicId, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
            var b = new TagBuilder("a");
            b.InnerHtml = (!string.IsNullOrEmpty(linkText) ? HttpUtility.HtmlEncode(linkText) : string.Empty);
            b.MergeAttributes<string, object>(htmlAttributes);
            b.MergeAttribute("href", text);
            return b.ToString(TagRenderMode.Normal);
        }

        public static string DynamicGenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, IDynamicRoutingContext routingContext, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicGenerateRouteLink(requestContext, routeCollection, routingContext, linkText, routeName, null, null, null, routeValues, htmlAttributes); }

        public static string DynamicGenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, IDynamicRoutingContext routingContext, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes) { return DynamicGenerateLinkInternal(requestContext, routeCollection, routingContext, linkText, routeName, null, null, protocol, hostName, fragment, routeValues, htmlAttributes, false); }

    }
}