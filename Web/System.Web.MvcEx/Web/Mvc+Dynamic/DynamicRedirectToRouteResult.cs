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
    public class DynamicRedirectToRouteResult : RedirectToRouteResult
    {
        private RouteCollection _routes;

        public DynamicRedirectToRouteResult(RouteValueDictionary routeValues)
            : this((IDynamicRoutingContext)null, null, routeValues) { }
        public DynamicRedirectToRouteResult(string routeName, RouteValueDictionary routeValues)
            : this((IDynamicRoutingContext)null, routeName, routeValues) { }
        public DynamicRedirectToRouteResult(IDynamicRoutingContext routingContext, RouteValueDictionary routeValues)
            : this(routingContext, null, routeValues) { }
        public DynamicRedirectToRouteResult(IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues)
            : base(routeName, routeValues)
        {
            RoutingContext = routingContext;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (context.IsChildAction)
                throw new InvalidOperationException("MvcResources.RedirectAction_CannotRedirectInChildAction");
            string text = UrlHelperEx.DynamicGenerateUrl(RoutingContext, RouteName, null, null, RouteValues, Routes, context.RequestContext, false);
            if (string.IsNullOrEmpty(text))
                throw new InvalidOperationException("MvcResources.Common_NoRouteMatched");
            context.Controller.TempData.Keep();
            context.HttpContext.Response.Redirect(text, false);
        }

        public IDynamicRoutingContext RoutingContext { get; set; }

        internal RouteCollection Routes
        {
            get
            {
                if (_routes == null)
                    _routes = RouteTable.Routes;
                return _routes;
            }
            set { _routes = value; }
        }
    }
}