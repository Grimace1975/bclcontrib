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
using System.Linq;
using System.Patterns.Generic;
using System.Collections.Generic;
namespace System.Web.Routing
{
    /// <summary>
    /// DynamicRoute
    /// </summary>
    public class DynamicRoute : RouteBase
    {
        private IDynamicRoutingContext _routingContext;

        public DynamicRoute()
            : this(new SiteMapDynamicRoutingContext(SiteMap.Provider)) { }
        public DynamicRoute(IDynamicRoutingContext routingContext)
        {
            _routingContext = routingContext;
        }

        public static void SetDynamicId(IEnumerable<Route> routes, string dynamicId)
        {
            foreach (var route in routes)
                route.DataTokens["dynamicId"] = dynamicId;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            var httpRequest = httpContext.Request;
            // virtualPath modeled from Route::GetRouteData
            string virtualPath = httpRequest.AppRelativeCurrentExecutionFilePath.Substring(1) + httpRequest.PathInfo;
            //var requestUri = _siteMapExRouteContext.GetRequestUri(httpContext);
            var nodeExtents = (_routingContext.FindNode(virtualPath) as IExtentsRepository);
            if (nodeExtents != null)
            {
                // func
                var func = nodeExtents.Get<Func<IExtentsRepository, RouteData>>();
                if (func != null)
                    return func(nodeExtents);
                // single
                var route = nodeExtents.Get<Route>();
                if (route != null)
                    return route.GetRouteData(httpContext);
                // many
                var routes = nodeExtents.GetMany<Route>();
                if (routes != null)
                    foreach (var route2 in routes)
                    {
                        var data = route2.GetRouteData(httpContext);
                        if (data != null)
                            return data;
                    }
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");
            // lookup node
            object value;
            if (values.TryGetValue("dynamicId", out value))
            {
                var nodeExtents = (_routingContext.FindNodeById(value as string) as IExtentsRepository);
                values.Remove("dynamicId");
                //
                if (nodeExtents != null)
                {
                    // func
                    var func = nodeExtents.Get<Func<IExtentsRepository, VirtualPathData>>();
                    if (func != null)
                        return func(nodeExtents);
                    // single
                    var route = nodeExtents.Get<Route>();
                    if (route != null)
                        return route.GetVirtualPath(requestContext, values);
                    // many
                    var routes = nodeExtents.GetMany<Route>();
                    if (routes != null)
                        foreach (var route2 in routes)
                        {
                            var path = route2.GetVirtualPath(requestContext, values);
                            if (path != null)
                                return path;
                        }
                }
            }
            return null;
        }
    }
}
