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
    /// ControllerExtensions
    /// </summary>
    public static class ControllerExtensions
    {
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, string actionName) { return DynamicRedirectToAction(controller, (IDynamicRoutingContext)null, actionName, (RouteValueDictionary)null); }
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, IDynamicRoutingContext routingContext, string actionName) { return DynamicRedirectToAction(controller, routingContext, actionName, (RouteValueDictionary)null); }

        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, string actionName, object routeValues) { return DynamicRedirectToAction(controller, (IDynamicRoutingContext)null, actionName, new RouteValueDictionary(routeValues)); }
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, IDynamicRoutingContext routingContext, string actionName, object routeValues) { return DynamicRedirectToAction(controller, routingContext, actionName, new RouteValueDictionary(routeValues)); }

        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, string actionName, string dynamicId) { return DynamicRedirectToAction(controller, (IDynamicRoutingContext)null, actionName, dynamicId, (RouteValueDictionary)null); }
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, IDynamicRoutingContext routingContext, string actionName, string dynamicId) { return DynamicRedirectToAction(controller, routingContext, actionName, dynamicId, (RouteValueDictionary)null); }

        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, string actionName, RouteValueDictionary routeValues) { return DynamicRedirectToAction(controller, (IDynamicRoutingContext)null, actionName, null, routeValues); }
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, IDynamicRoutingContext routingContext, string actionName, RouteValueDictionary routeValues) { return DynamicRedirectToAction(controller, routingContext, actionName, null, routeValues); }

        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, string actionName, string dynamicId, object routeValues) { return DynamicRedirectToAction(controller, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(routeValues)); }
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, IDynamicRoutingContext routingContext, string actionName, string dynamicId, object routeValues) { return DynamicRedirectToAction(controller, routingContext, actionName, dynamicId, new RouteValueDictionary(routeValues)); }

        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, string actionName, string dynamicId, RouteValueDictionary routeValues) { return DynamicRedirectToAction(controller, actionName, dynamicId, routeValues); }
        public static RedirectToRouteResult DynamicRedirectToAction(this Controller controller, IDynamicRoutingContext routingContext, string actionName, string dynamicId, RouteValueDictionary routeValues)
        {
            RouteValueDictionary dictionary;
            if (controller.RouteData == null)
                dictionary = RouteValuesHelpers.DynamicMergeRouteValues((IDynamicRoutingContext)null, actionName, dynamicId, null, routeValues, true);
            else
                dictionary = RouteValuesHelpers.DynamicMergeRouteValues((IDynamicRoutingContext)null, actionName, dynamicId, controller.RouteData.Values, routeValues, true);
            return new DynamicRedirectToRouteResult((IDynamicRoutingContext)null, dictionary);
        }

        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, object routeValues) { return DynamicRedirectToRoute(controller, (IDynamicRoutingContext)null, new RouteValueDictionary(routeValues)); }
        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, IDynamicRoutingContext routingContext, object routeValues) { return DynamicRedirectToRoute(controller, routingContext, new RouteValueDictionary(routeValues)); }

        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, string routeName) { return DynamicRedirectToRoute(controller, (IDynamicRoutingContext)null, routeName, (RouteValueDictionary)null); }
        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, IDynamicRoutingContext routingContext, string routeName) { return DynamicRedirectToRoute(controller, routingContext, routeName, (RouteValueDictionary)null); }

        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, RouteValueDictionary routeValues) { return DynamicRedirectToRoute(controller, (IDynamicRoutingContext)null, null, routeValues); }
        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, IDynamicRoutingContext routingContext, RouteValueDictionary routeValues) { return DynamicRedirectToRoute(controller, routingContext, null, routeValues); }

        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, string routeName, object routeValues) { return DynamicRedirectToRoute(controller, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(routeValues)); }
        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, IDynamicRoutingContext routingContext, string routeName, object routeValues) { return DynamicRedirectToRoute(controller, routingContext, routeName, new RouteValueDictionary(routeValues)); }

        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, string routeName, RouteValueDictionary routeValues) { return DynamicRedirectToRoute(controller, (IDynamicRoutingContext)null, routeName, routeValues); }
        public static RedirectToRouteResult DynamicRedirectToRoute(this Controller controller, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues)
        {
            return new DynamicRedirectToRouteResult((IDynamicRoutingContext)null, routeName, RouteValuesHelpers.GetRouteValues(routeValues));
        }
    }
}