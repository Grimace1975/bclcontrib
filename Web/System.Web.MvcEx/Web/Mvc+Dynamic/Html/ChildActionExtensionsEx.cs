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
using System.IO;
using System.Web.Routing;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;
namespace System.Web.Mvc.Html
{
    public static class ChildActionExtensionsEx
    {
        private static readonly MethodInfo s_wrapForServerExecuteMethod = Type.GetType("System.Web.Mvc.HttpHandlerUtil").GetMethod("WrapForServerExecute");

        public static MvcHtmlString DynamicAction(this HtmlHelper htmlHelper, string actionName) { return DynamicAction(htmlHelper, actionName, null, ((RouteValueDictionary)null)); }
        public static MvcHtmlString DynamicAction(this HtmlHelper htmlHelper, string actionName, object routeValues) { return DynamicAction(htmlHelper, actionName, null, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicAction(this HtmlHelper htmlHelper, string actionName, string dynamicId) { return DynamicAction(htmlHelper, actionName, dynamicId, ((RouteValueDictionary)null)); }
        public static MvcHtmlString DynamicAction(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues) { return DynamicAction(htmlHelper, actionName, null, routeValues); }
        public static MvcHtmlString DynamicAction(this HtmlHelper htmlHelper, string actionName, string dynamicId, object routeValues) { return DynamicAction(htmlHelper, actionName, dynamicId, new RouteValueDictionary(routeValues)); }
        public static MvcHtmlString DynamicAction(this HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues)
        {
            var textWriter = new StringWriter(CultureInfo.CurrentCulture);
            DynamicActionHelper(htmlHelper, actionName, dynamicId, routeValues, textWriter);
            return MvcHtmlString.Create(textWriter.ToString());
        }

        public static void RenderDynamicAction(this HtmlHelper htmlHelper, string actionName) { RenderDynamicAction(htmlHelper, actionName, null, (RouteValueDictionary)null); }
        public static void RenderDynamicAction(this HtmlHelper htmlHelper, string actionName, object routeValues) { RenderDynamicAction(htmlHelper, actionName, null, new RouteValueDictionary(routeValues)); }
        public static void RenderDynamicAction(this HtmlHelper htmlHelper, string actionName, string dynamicId) { RenderDynamicAction(htmlHelper, actionName, dynamicId, (RouteValueDictionary)null); }
        public static void RenderDynamicAction(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues) { RenderDynamicAction(htmlHelper, actionName, null, routeValues); }
        public static void RenderDynamicAction(this HtmlHelper htmlHelper, string actionName, string dynamicId, object routeValues) { RenderDynamicAction(htmlHelper, actionName, dynamicId, new RouteValueDictionary(routeValues)); }
        public static void RenderDynamicAction(this HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues)
        {
            DynamicActionHelper(htmlHelper, actionName, dynamicId, routeValues, htmlHelper.ViewContext.Writer);
        }

        internal static void DynamicActionHelper(HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues, TextWriter textWriter)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");
            if (string.IsNullOrEmpty(actionName))
                throw new ArgumentException("Common_NullOrEmpty", "actionName");
            routeValues = MergeDictionaries(new RouteValueDictionary[] { routeValues, htmlHelper.ViewContext.RouteData.Values });
            routeValues["action"] = actionName;
            if (!string.IsNullOrEmpty(dynamicId))
                routeValues["dynamicId"] = dynamicId;
            //bool flag;
            var data = htmlHelper.RouteCollection.GetVirtualPathForArea(htmlHelper.ViewContext.RequestContext, null, routeValues); // out flag);
            if (data == null)
                throw new InvalidOperationException("Common_NoRouteMatched");
            //if (flag)
            //    routeValues.Remove("area");
            var data2 = CreateRouteData(data.Route, routeValues, data.DataTokens, htmlHelper.ViewContext);
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var context = new RequestContext(httpContext, data2);
            var httpHandler = new ChildActionMvcHandler(context);
            httpContext.Server.Execute((IHttpHandler)s_wrapForServerExecuteMethod.Invoke(null, new object[] { httpHandler }), textWriter, true);
        }

        private static RouteData CreateRouteData(RouteBase route, RouteValueDictionary routeValues, RouteValueDictionary dataTokens, ViewContext parentViewContext)
        {
            var data = new RouteData();
            foreach (KeyValuePair<string, object> pair in routeValues)
                data.Values.Add(pair.Key, pair.Value);
            foreach (KeyValuePair<string, object> pair2 in dataTokens)
                data.DataTokens.Add(pair2.Key, pair2.Value);
            data.Route = route;
            data.DataTokens["ParentActionViewContext"] = parentViewContext;
            return data;
        }

        private static RouteValueDictionary MergeDictionaries(params RouteValueDictionary[] dictionaries)
        {
            var dictionary = new RouteValueDictionary();
            foreach (var dictionary2 in dictionaries.Where(d => d != null))
                foreach (var pair in dictionary2)
                    if (!dictionary.ContainsKey(pair.Key))
                        dictionary.Add(pair.Key, pair.Value);
            return dictionary;
        }

        internal class ChildActionMvcHandler : MvcHandler
        {
            public ChildActionMvcHandler(RequestContext context)
                : base(context) { }
            protected override void AddVersionHeader(HttpContextBase httpContext) { }
        }
    }
}
