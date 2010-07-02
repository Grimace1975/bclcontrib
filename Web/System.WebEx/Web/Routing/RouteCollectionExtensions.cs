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
    public static class RouteCollectionExtensions
    {
        public static void IgnoreRouteEx(this RouteCollection routes, string url) { routes.IgnoreRouteEx(url, null); }
        public static void IgnoreRouteEx(this RouteCollection routes, string url, object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            routes.Add(new IgnoreRouteExInternal(url) { Constraints = new RouteValueDictionary(constraints) });
        }

        public static void ThrowRouteEx(this RouteCollection routes, Exception exception, string url) { routes.ThrowRouteEx(exception, url, null); }
        public static void ThrowRouteEx(this RouteCollection routes, Exception exception, string url, object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            routes.Add(new ThrowRouteExInternal(url, exception) { Constraints = new RouteValueDictionary(constraints) });
        }

        public static void AliasRouteEx(this RouteCollection routes, string alias, string url) { routes.AliasRouteEx(alias, url, null, (left, right) => left); }
        public static void AliasRouteEx(this RouteCollection routes, string alias, string url, object aliasConstraints) { routes.AliasRouteEx(alias, url, null, (left, right) => left); }
        public static void AliasRouteEx(this RouteCollection routes, string alias, string url, object aliasConstraints, Func<RouteData, RouteData, RouteData> routeDataJoiner)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (alias == null)
                throw new ArgumentNullException("alias");
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            if (routeDataJoiner == null)
                throw new ArgumentNullException("routeDataJoiner");
            routes.Add(new AliasRouteExInternal(alias, () => routes.FindRouteDataByUrl(HttpContext.Current, url), routeDataJoiner) { Constraints = new RouteValueDictionary(aliasConstraints) });
        }

        public static void AliasRouteEx(this RouteCollection routes, string alias, RouteData routeData) { routes.AliasRouteEx(alias, routeData, null, (left, right) => left); }
        public static void AliasRouteEx(this RouteCollection routes, string alias, RouteData routeData, object aliasConstraints) { routes.AliasRouteEx(alias, routeData, null, (left, right) => left); }
        public static void AliasRouteEx(this RouteCollection routes, string alias, RouteData routeData, object aliasConstraints, Func<RouteData, RouteData, RouteData> routeDataJoiner)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException("alias");
            if (routeData == null)
                throw new ArgumentNullException("routeData");
            if (routeDataJoiner == null)
                throw new ArgumentNullException("routeDataJoiner");
            routes.Add(new AliasRouteExInternal(alias, () => routeData, routeDataJoiner) { Constraints = new RouteValueDictionary(aliasConstraints) });
        }

        #region Internal-Routes
        private sealed class IgnoreRouteExInternal : Route
        {
            public IgnoreRouteExInternal(string url)
                : base(url, new StopRoutingHandler()) { }

            public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
            {
                return null;
            }
        }

        private sealed class AliasRouteExInternal : Route
        {
            private Func<RouteData> _routeDataFinder;
            private Func<RouteData, RouteData, RouteData> _routeDataJoiner;

            public AliasRouteExInternal(string alias, Func<RouteData> routeDataFinder, Func<RouteData, RouteData, RouteData> routeDataJoiner)
                : base(alias, null)
            {
                _routeDataFinder = routeDataFinder;
                _routeDataJoiner = routeDataJoiner;
            }

            public override RouteData GetRouteData(HttpContextBase httpContext)
            {
                var aliasRouteData = base.GetRouteData(httpContext);
                if (aliasRouteData == null)
                    return null;
                var routeData = _routeDataFinder();
                return (routeData != null ? _routeDataJoiner(routeData, aliasRouteData) : null);
            }

            public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
            {
                return base.GetVirtualPath(requestContext, values);
            }
        }

        private sealed class ThrowRouteExInternal : Route
        {
            public ThrowRouteExInternal(string url, Exception exception)
                : base(url, new ThrowRouteExHandlerInternal(exception)) { }

            public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
            {
                return null;
            }
        }

        private sealed class ThrowRouteExHandlerInternal : IRouteHandler
        {
            private Exception _exception;

            public ThrowRouteExHandlerInternal(Exception exception)
            {
                if (exception == null)
                    throw new ArgumentNullException("exception");
                _exception = exception;
            }

            public IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                throw _exception;
            }
        }
        #endregion

        #region Find Route-Data
        public static RouteData FindRouteDataByUrl(this RouteCollection routes, HttpContext httpContext, string url)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            var injectedHttpContext = new HttpContextInjector(httpContext, new HttpRequestInjector(httpContext.Request, url));
            return routes.GetRouteData(injectedHttpContext);
        }

        private sealed class HttpContextInjector : HttpContextWrapper
        {
            private HttpRequestBase _request;

            public HttpContextInjector(HttpContext httpContext, HttpRequestBase request)
                : base(httpContext)
            {
                _request = request;
            }

            public override HttpRequestBase Request
            {
                get { return _request; }
            }
        }

        private sealed class HttpRequestInjector : HttpRequestWrapper
        {
            private string _appRelativeCurrentExecutionFilePath;

            public HttpRequestInjector(HttpRequest httpRequest, string appRelativeCurrentExecutionFilePath)
                : base(httpRequest)
            {
                _appRelativeCurrentExecutionFilePath = appRelativeCurrentExecutionFilePath;
            }

            public override string AppRelativeCurrentExecutionFilePath
            {
                get { return _appRelativeCurrentExecutionFilePath; }
            }
        }
        #endregion
    }
}