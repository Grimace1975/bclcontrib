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
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Web.Routing;
namespace System.Web.Mvc.Html
{
    public static partial class SelectExtensionsEx
    {
        public static MvcHtmlString DropDownListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList) { return DropDownListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString DropDownListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return DropDownListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, htmlAttributes); }
        public static MvcHtmlString DropDownListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return DropDownListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString DropDownListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel) { return DropDownListForEx<TModel, TProperty>(htmlHelper, expression, selectList, optionLabel, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString DropDownListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes) { return DropDownListForEx<TModel, TProperty>(htmlHelper, expression, selectList, optionLabel, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString DropDownListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            var metadataEx = (ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData) as IModelMetadataEx);
            if (metadataEx != null)
                metadataEx.ViewModifiers.MapToHtmlAttributes(ref htmlAttributes);
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, selectList, optionLabel, htmlAttributes);
        }

        public static MvcHtmlString ListBoxForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList) { return ListBoxForEx<TModel, TProperty>(htmlHelper, expression, selectList, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString ListBoxForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return ListBoxForEx<TModel, TProperty>(htmlHelper, expression, selectList, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString ListBoxForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            var metadataEx = (ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData) as IModelMetadataEx);
            if (metadataEx != null)
                metadataEx.ViewModifiers.MapToHtmlAttributes(ref htmlAttributes);
            return htmlHelper.ListBoxFor<TModel, TProperty>(expression, selectList, htmlAttributes);
        }

        public static MvcHtmlString RadioButtonListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList) { return RadioButtonListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return RadioButtonListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, htmlAttributes); }
        public static MvcHtmlString RadioButtonListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return RadioButtonListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString RadioButtonListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, SelectListLayout layout) { return RadioButtonListForEx<TModel, TProperty>(htmlHelper, expression, selectList, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, SelectListLayout layout, IDictionary<string, object> htmlAttributes)
        {
            var metadataEx = (ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData) as IModelMetadataEx);
            if (metadataEx != null)
                metadataEx.ViewModifiers.MapToHtmlAttributes(ref htmlAttributes);
            return htmlHelper.RadioButtonListFor<TModel, TProperty>(expression, selectList, layout, htmlAttributes);
        }

        public static MvcHtmlString CheckBoxListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList) { return CheckBoxListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return CheckBoxListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, htmlAttributes); }
        public static MvcHtmlString CheckBoxListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return CheckBoxListForEx<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString CheckBoxListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, SelectListLayout layout) { return CheckBoxListForEx<TModel, TProperty>(htmlHelper, expression, selectList, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxListForEx<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, SelectListLayout layout, IDictionary<string, object> htmlAttributes)
        {
            var metadataEx = (ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData) as IModelMetadataEx);
            if (metadataEx != null)
                metadataEx.ViewModifiers.MapToHtmlAttributes(ref htmlAttributes);
            return htmlHelper.CheckBoxListFor<TModel, TProperty>(expression, selectList, layout, htmlAttributes);
        }
    }
}
