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
    /// UrlHelperEx
    /// </summary>
    public static class UrlHelperEx
    {
        //public static string GenerateDynamicUrl(string routeName, string actionName, string dynamicId, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues) { IDynamicNode node; return DynamicGenerateUrl(out node, (IDynamicRoutingContext)null, routeName, actionName, dynamicId, routeValues, routeCollection, requestContext, includeImplicitMvcValues); }
        public static string GenerateDynamicUrl(out IDynamicNode node, string routeName, string actionName, string dynamicId, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues) { return GenerateDynamicUrl(out node, (IDynamicRoutingContext)null, routeName, actionName, dynamicId, routeValues, routeCollection, requestContext, includeImplicitMvcValues); }
        //public static string GenerateDynamicUrl(IDynamicRoutingContext routingContext, string routeName, string actionName, string dynamicId, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues) { IDynamicNode node; return DynamicGenerateUrl(out node, routingContext, routeName, actionName, dynamicId, routeValues, routeCollection, requestContext, includeImplicitMvcValues); }
        public static string GenerateDynamicUrl(out IDynamicNode node, IDynamicRoutingContext routingContext, string routeName, string actionName, string dynamicId, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues)
        {
            if (routeCollection == null)
                throw new ArgumentNullException("routeCollection");
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");
            var values = RouteValuesHelpers.DynamicMergeRouteValues(routingContext, actionName, dynamicId, requestContext.RouteData.Values, routeValues, includeImplicitMvcValues);
            var data = routeCollection.GetVirtualPathForArea(requestContext, routeName, values);
            if (data == null)
            {
                node = null;
                return null;
            }
            node = (IDynamicNode)data.DataTokens["dynamicNode"];
            return PathHelpers.GenerateClientUrl(requestContext.HttpContext, data.VirtualPath);
        }

        //public static string GenerateDynamicUrl(string routeName, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues) { IDynamicNode node; return DynamicGenerateUrl(out node, (IDynamicRoutingContext)null, routeName, actionName, dynamicId, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues); }
        public static string GenerateDynamicUrl(out IDynamicNode node, string routeName, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues) { return GenerateDynamicUrl(out node, (IDynamicRoutingContext)null, routeName, actionName, dynamicId, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues); }
        //public static string GenerateDynamicUrl(IDynamicRoutingContext routingContext, string routeName, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues) { IDynamicNode node; return DynamicGenerateUrl(out node, routingContext, routeName, actionName, dynamicId, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues); }
        public static string GenerateDynamicUrl(out IDynamicNode node, IDynamicRoutingContext routingContext, string routeName, string actionName, string dynamicId, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, RouteCollection routeCollection, RequestContext requestContext, bool includeImplicitMvcValues)
        {
            string dynamicUrl = GenerateDynamicUrl(out node, routingContext, routeName, actionName, dynamicId, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
            if (dynamicUrl == null)
                return dynamicUrl;
            if (!string.IsNullOrEmpty(fragment))
                dynamicUrl = dynamicUrl + "#" + fragment;
            if ((string.IsNullOrEmpty(protocol)) && (string.IsNullOrEmpty(hostName)))
                return dynamicUrl;
            var url = requestContext.HttpContext.Request.Url;
            protocol = (!string.IsNullOrEmpty(protocol) ? protocol : Uri.UriSchemeHttp);
            hostName = (!string.IsNullOrEmpty(hostName) ? hostName : url.Host);
            string str2 = string.Empty;
            string scheme = url.Scheme;
            if (string.Equals(protocol, scheme, StringComparison.OrdinalIgnoreCase))
                str2 = (url.IsDefaultPort ? string.Empty : (":" + Convert.ToString(url.Port, CultureInfo.InvariantCulture)));
            return (protocol + Uri.SchemeDelimiter + hostName + str2 + dynamicUrl);
        }
    }
}
