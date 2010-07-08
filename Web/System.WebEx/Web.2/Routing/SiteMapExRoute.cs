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
namespace System.Web.Routing
{
    /// <summary>
    /// SiteMapExRoute
    /// </summary>
    public class SiteMapExRoute : RouteBase
    {
        private SiteMapProvider _siteMapProvider;
        private ISiteMapExRouteContext _siteMapExRouteContext;

        public SiteMapExRoute(SiteMapProvider siteMapProvider)
            : this(siteMapProvider, new SiteMapExRouteContext()) { }
        public SiteMapExRoute(SiteMapProvider siteMapProvider, ISiteMapExRouteContext siteMapExRouteContext)
        {
            _siteMapProvider = siteMapProvider;
            _siteMapExRouteContext = siteMapExRouteContext;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {            
            var httpRequest = httpContext.Request;
            // virtualPath modeled from Route::GetRouteData
            string virtualPath = httpRequest.AppRelativeCurrentExecutionFilePath.Substring(1) + httpRequest.PathInfo;
            //var requestUri = _siteMapExRouteContext.GetRequestUri(httpContext);
            var nodeEx = (_siteMapProvider.FindSiteMapNode(virtualPath) as SiteMapNodeEx);
            if (nodeEx != null)
            {
                var func = nodeEx.Get<Func<SiteMapNodeEx, RouteData>>();
                if (func != null)
                    return func(nodeEx);
                var route = nodeEx.Get<Route>();
                if (route != null)
                    return route.GetRouteData(httpContext);
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            object value;
            if (values.TryGetValue("cid", out value))
            {
                var nodeEx = (_siteMapProvider.FindSiteMapNodeFromKey(value as string) as SiteMapNodeEx);
                values.Remove("cid");
                if (nodeEx != null)
                {
                    var func = nodeEx.Get<Func<SiteMapNodeEx, VirtualPathData>>();
                    if (func != null)
                        return func(nodeEx);
                    var route = nodeEx.Get<Route>();
                    if (route != null)
                        return route.GetVirtualPath(requestContext, values);
                }
            }
            return null;
        }
    }
}
