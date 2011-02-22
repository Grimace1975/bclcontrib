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
using System.Reflection;
namespace System.Web.Mvc.Html
{
    public static class FormExtensionsEx
    {
        private static readonly PropertyInfo s_formIdGeneratorPropertyInfo = typeof(ViewContext).GetProperty("FormIdGenerator", BindingFlags.NonPublic | BindingFlags.Instance);

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext)
        {
            string rawUrl = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return DynamicFormHelper(htmlHelper, rawUrl, FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, object routeValues) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, null, null, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, object routeValues) { return DynamicBeginForm(htmlHelper, routingContext, null, null, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, null, null, routeValues, FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, RouteValueDictionary routeValues) { return DynamicBeginForm(htmlHelper, routingContext, null, null, routeValues, FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, object routeValues) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, object routeValues) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, FormMethod method) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(), method, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, FormMethod method) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, routeValues, FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, RouteValueDictionary routeValues) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, routeValues, FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, object routeValues, FormMethod method) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(routeValues), method, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, object routeValues, FormMethod method) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, FormMethod method, IDictionary<string, object> htmlAttributes) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(), method, htmlAttributes); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, FormMethod method, IDictionary<string, object> htmlAttributes) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(), method, htmlAttributes); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, FormMethod method, object htmlAttributes) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(), method, new RouteValueDictionary(htmlAttributes)); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, FormMethod method, object htmlAttributes) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary(htmlAttributes)); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues, FormMethod method) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, routeValues, method, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, routeValues, method, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, object routeValues, FormMethod method, object htmlAttributes) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, new RouteValueDictionary(routeValues), method, new RouteValueDictionary(htmlAttributes)); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes) { return DynamicBeginForm(htmlHelper, routingContext, actionName, controllerName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary(htmlAttributes)); }

        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, string actionName, string dynamicId, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes) { return DynamicBeginForm(htmlHelper, (IDynamicRoutingContext)null, actionName, dynamicId, routeValues, method, htmlAttributes); }
        public static MvcForm DynamicBeginForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            IDynamicNode node;
            string formAction = UrlHelperEx.GenerateDynamicUrl(out node, routingContext, null, actionName, controllerName, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true);
            return DynamicFormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, object routeValues) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, null, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, object routeValues) { return DynamicBeginRouteForm(htmlHelper, routingContext, null, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, null, routeValues, FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, RouteValueDictionary routeValues) { return DynamicBeginRouteForm(htmlHelper, routingContext, null, routeValues, FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(), method, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, FormMethod method) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(), method, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, routeValues, FormMethod.Post, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, routeValues, FormMethod.Post, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues, FormMethod method) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(), method, htmlAttributes); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(), method, htmlAttributes); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(), method, new RouteValueDictionary(htmlAttributes)); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, FormMethod method, object htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(), method, new RouteValueDictionary(htmlAttributes)); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, routeValues, method, new RouteValueDictionary()); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues, FormMethod method) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, routeValues, method, new RouteValueDictionary()); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method, object htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary(htmlAttributes)); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, object routeValues, FormMethod method, object htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, routingContext, routeName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary(htmlAttributes)); }

        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes) { return DynamicBeginRouteForm(htmlHelper, (IDynamicRoutingContext)null, routeName, routeValues, method, htmlAttributes); }
        public static MvcForm DynamicBeginRouteForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            IDynamicNode node;
            string formAction = UrlHelperEx.GenerateDynamicUrl(out node, routingContext, routeName, null, null, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, false);
            return DynamicFormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        public static void DynamicEndForm(this HtmlHelper htmlHelper) { DynamicEndForm(htmlHelper, (IDynamicRoutingContext)null); }
        public static void DynamicEndForm(this HtmlHelper htmlHelper, IDynamicRoutingContext routingContext)
        {
            htmlHelper.ViewContext.Writer.Write("</form>");
            htmlHelper.ViewContext.OutputClientValidation();
        }

        private static MvcForm DynamicFormHelper(HtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            var builder = new TagBuilder("form");
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("action", formAction);
            builder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);
            if (htmlHelper.ViewContext.ClientValidationEnabled)
                builder.GenerateId(((Func<string>)s_formIdGeneratorPropertyInfo.GetValue(htmlHelper.ViewContext, null))());
            htmlHelper.ViewContext.Writer.Write(builder.ToString(TagRenderMode.StartTag));
            var form = new MvcForm(htmlHelper.ViewContext);
            if (htmlHelper.ViewContext.ClientValidationEnabled)
                htmlHelper.ViewContext.FormContext.FormId = builder.Attributes["id"];
            return form;
        }
    }
}
