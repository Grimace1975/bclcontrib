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
namespace System.Web.Mvc
{
    /// <summary>
    /// UrlHelperExtensions
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static string SiteMapAction(this UrlHelper urlHelper, string cid, string actionName) { return SiteMapAction(urlHelper, (SiteMapProvider)null, cid, actionName); }
        public static string SiteMapAction(this UrlHelper urlHelper, SiteMapProvider siteMap, string cid, string actionName)
        {
            if (string.IsNullOrEmpty(cid))
                throw new ArgumentNullException("cid");
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            //
            var routeValues2 = new RouteValueDictionary();
            routeValues2.Add("cid", cid);
            return UrlHelper.GenerateUrl(null, actionName, null, routeValues2, urlHelper.RouteCollection, urlHelper.RequestContext, true);
        }

        public static string SiteMapAction(this UrlHelper urlHelper, string cid, string actionName, object routeValues) { return SiteMapAction(urlHelper, (SiteMapProvider)null, cid, actionName, routeValues); }
        public static string SiteMapAction(this UrlHelper urlHelper, SiteMapProvider siteMap, string cid, string actionName, object routeValues)
        {
            if (string.IsNullOrEmpty(cid))
                throw new ArgumentNullException("cid");
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            //
            var routeValues2 = new RouteValueDictionary(routeValues);
            routeValues2.Add("cid", cid);
            return UrlHelper.GenerateUrl(null, actionName, null, routeValues2, urlHelper.RouteCollection, urlHelper.RequestContext, true);
        }

        public static string SiteMapAction(this UrlHelper urlHelper, string cid, string actionName, RouteValueDictionary routeValues) { return SiteMapAction(urlHelper, (SiteMapProvider)null, cid, actionName, routeValues); }
        public static string SiteMapAction(this UrlHelper urlHelper, SiteMapProvider siteMap, string cid, string actionName, RouteValueDictionary routeValues)
        {
            if (string.IsNullOrEmpty(cid))
                throw new ArgumentNullException("cid");
            if (siteMap == null)
                siteMap = SiteMap.Provider;
            //
            var routeValues2 = new RouteValueDictionary(routeValues);
            routeValues2.Add("cid", cid);
            return UrlHelper.GenerateUrl(null, actionName, null, routeValues2, urlHelper.RouteCollection, urlHelper.RequestContext, true);
        }
    }
}
