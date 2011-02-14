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
using System.Globalization;
namespace System.Web.Mvc
{
    /// <summary>
    /// UrlHelperExtensions
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static string DynamicAction(this UrlHelper urlHelper, string actionName) { return GenerateDynamicUrl(urlHelper, (IDynamicRoutingContext)null, null, actionName, null, null); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName) { return GenerateDynamicUrl(urlHelper, routingContext, null, actionName, null, null); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, object routeValues) { return GenerateDynamicUrl(urlHelper, (IDynamicRoutingContext)null, null, actionName, null, new RouteValueDictionary(routeValues)); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, object routeValues) { return GenerateDynamicUrl(urlHelper, routingContext, null, actionName, null, new RouteValueDictionary(routeValues)); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, string dynamicId) { return GenerateDynamicUrl(urlHelper, (IDynamicRoutingContext)null, null, actionName, dynamicId, null); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId) { return GenerateDynamicUrl(urlHelper, routingContext, null, actionName, dynamicId, null); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, RouteValueDictionary routeValues) { return GenerateDynamicUrl(urlHelper, (IDynamicRoutingContext)null, null, actionName, null, routeValues); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, RouteValueDictionary routeValues) { return GenerateDynamicUrl(urlHelper, routingContext, null, actionName, null, routeValues); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, string dynamicId, object routeValues) { return GenerateDynamicUrl(urlHelper, (IDynamicRoutingContext)null, null, actionName, dynamicId, new RouteValueDictionary(routeValues)); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, object routeValues) { return GenerateDynamicUrl(urlHelper, routingContext, null, actionName, dynamicId, new RouteValueDictionary(routeValues)); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues) { return GenerateDynamicUrl(urlHelper, (IDynamicRoutingContext)null, null, actionName, dynamicId, routeValues); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, RouteValueDictionary routeValues) { return GenerateDynamicUrl(urlHelper, routingContext, null, actionName, dynamicId, routeValues); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, string dynamicId, object routeValues, string protocol) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, (IDynamicRoutingContext)null, null, actionName, dynamicId, protocol, null, null, new RouteValueDictionary(routeValues), urlHelper.RouteCollection, urlHelper.RequestContext, true); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, object routeValues, string protocol) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, routingContext, null, actionName, dynamicId, protocol, null, null, new RouteValueDictionary(routeValues), urlHelper.RouteCollection, urlHelper.RequestContext, true); }

        public static string DynamicAction(this UrlHelper urlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues, string protocol, string hostName) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, (IDynamicRoutingContext)null, null, actionName, dynamicId, protocol, hostName, null, routeValues, urlHelper.RouteCollection, urlHelper.RequestContext, true); }
        public static string DynamicAction(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string actionName, string dynamicId, RouteValueDictionary routeValues, string protocol, string hostName) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, routingContext, null, actionName, dynamicId, protocol, hostName, null, routeValues, urlHelper.RouteCollection, urlHelper.RequestContext, true); }

        private static string GenerateDynamicUrl(UrlHelper urlHelper, IDynamicRoutingContext routingContext, string routeName, string actionName, string dynamicId, RouteValueDictionary routeValues) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, routingContext, routeName, actionName, dynamicId, routeValues, urlHelper.RouteCollection, urlHelper.RequestContext, true); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, object routeValues) { return DynamicRouteUrl(urlHelper, (IDynamicRoutingContext)null, null, routeValues); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, object routeValues) { return DynamicRouteUrl(urlHelper, routingContext, null, routeValues); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, string routeName) { return DynamicRouteUrl(urlHelper, (IDynamicRoutingContext)null, routeName, null); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string routeName) { return DynamicRouteUrl(urlHelper, routingContext, routeName, null); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, RouteValueDictionary routeValues) { return DynamicRouteUrl(urlHelper, (IDynamicRoutingContext)null, null, routeValues); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, RouteValueDictionary routeValues) { return DynamicRouteUrl(urlHelper, routingContext, null, routeValues); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, string routeName, object routeValues) { return DynamicRouteUrl(urlHelper, (IDynamicRoutingContext)null, routeName, routeValues, null); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues) { return DynamicRouteUrl(urlHelper, routingContext, routeName, routeValues, null); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, string routeName, RouteValueDictionary routeValues) { return DynamicRouteUrl(urlHelper, (IDynamicRoutingContext)null, routeName, routeValues, null, null); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues) { return DynamicRouteUrl(urlHelper, routingContext, routeName, routeValues, null, null); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, string routeName, object routeValues, string protocol) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, (IDynamicRoutingContext)null, routeName, null, null, protocol, null, null, new RouteValueDictionary(routeValues), urlHelper.RouteCollection, urlHelper.RequestContext, false); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues, string protocol) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, routingContext, routeName, null, null, protocol, null, null, new RouteValueDictionary(routeValues), urlHelper.RouteCollection, urlHelper.RequestContext, false); }

        public static string DynamicRouteUrl(this UrlHelper urlHelper, string routeName, RouteValueDictionary routeValues, string protocol, string hostName) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, (IDynamicRoutingContext)null, routeName, null, null, protocol, hostName, null, routeValues, urlHelper.RouteCollection, urlHelper.RequestContext, false); }
        public static string DynamicRouteUrl(this UrlHelper urlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues, string protocol, string hostName) { IDynamicNode node; return UrlHelperEx.GenerateDynamicUrl(out node, routingContext, routeName, null, null, protocol, hostName, null, routeValues, urlHelper.RouteCollection, urlHelper.RequestContext, false); }
    }
}
