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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Text;
using System.Globalization;
using System.Collections;
namespace System.Web.Mvc.Html
{
    public static partial class SelectExtensionsEx
    {
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name) { return RadioButtonList(htmlHelper, name, null, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList) { return RadioButtonList(htmlHelper, name, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, ButtonListLayout layout) { return RadioButtonList(htmlHelper, name, null, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return RadioButtonList(htmlHelper, name, selectList, null, htmlAttributes); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return RadioButtonList(htmlHelper, name, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, ButtonListLayout layout) { return RadioButtonList(htmlHelper, name, selectList, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, IDictionary<string, object> htmlAttributes) { return RadioButtonList(htmlHelper, name, selectList, layout, htmlAttributes); }
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, object htmlAttributes) { return RadioButtonList(htmlHelper, name, selectList, layout, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList) { return RadioButtonListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return RadioButtonListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, htmlAttributes); }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return RadioButtonListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, ButtonListLayout layout) { return RadioButtonListFor<TModel, TProperty>(htmlHelper, expression, selectList, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            return RadioButtonListHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList, layout, htmlAttributes);
        }

        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string layout, object htmlAttributes) { return RadioButtonListFor<TModel, TProperty>(htmlHelper, expression, selectList, layout, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        private static MvcHtmlString RadioButtonListHelper(HtmlHelper htmlHelper, string expression, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, expression, selectList, false, htmlAttributes);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name) { return CheckBoxList(htmlHelper, name, null, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList) { return CheckBoxList(htmlHelper, name, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, ButtonListLayout layout) { return CheckBoxList(htmlHelper, name, null, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return CheckBoxList(htmlHelper, name, selectList, null, htmlAttributes); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return CheckBoxList(htmlHelper, name, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, ButtonListLayout layout) { return CheckBoxList(htmlHelper, name, selectList, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, IDictionary<string, object> htmlAttributes) { return CheckBoxList(htmlHelper, name, selectList, layout, htmlAttributes); }
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, object htmlAttributes) { return CheckBoxList(htmlHelper, name, selectList, layout, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList) { return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes) { return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, htmlAttributes); }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes) { return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, ButtonListLayout layout) { return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, layout, ((IDictionary<string, object>)null)); }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            return CheckBoxListHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList, layout, htmlAttributes);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string layout, object htmlAttributes) { return CheckBoxListFor<TModel, TProperty>(htmlHelper, expression, selectList, layout, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes))); }
        private static MvcHtmlString CheckBoxListHelper(HtmlHelper htmlHelper, string expression, IEnumerable<SelectListItem> selectList, ButtonListLayout layout, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, expression, selectList, true, htmlAttributes);
        }

        #region Select Renderer
        private static MvcHtmlString SelectInternal(HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            ModelState state;
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("MvcResources.Common_NullOrEmpty", "name");
            bool hasSelectList = false;
            if (selectList == null)
            {
                selectList = GetSelectData(htmlHelper, name);
                hasSelectList = true;
            }
            object data = (allowMultiple ? htmlHelper.GetModelStateValue(name, typeof(string[])) : htmlHelper.GetModelStateValue(name, typeof(string)));
            if ((!hasSelectList) && (data == null))
                data = htmlHelper.ViewData.Eval(name);
            if (data != null)
            {
                var source = (allowMultiple ? (data as IEnumerable) : ((IEnumerable)new object[] { data }));
                var set = new HashSet<string>(source.Cast<object>().Select<object, string>(value => Convert.ToString(value, CultureInfo.CurrentCulture)), StringComparer.OrdinalIgnoreCase);
                foreach (var item in selectList)
                    item.Selected = (item.Value != null ? set.Contains(item.Value) : set.Contains(item.Text));
            }
            var b = new StringBuilder();
            foreach (var selectItem in selectList)
                b.AppendLine(ListItemToOption(name, selectItem));
            var b2 = new TagBuilder("div");
            b2.InnerHtml = b.ToString();
            b2.MergeAttributes<string, object>(htmlAttributes);
            b2.GenerateId(name);
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out state) && (state.Errors.Count > 0))
                b2.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            return b2.ToMvcHtmlString(TagRenderMode.Normal);
        }

        private static IEnumerable<SelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object obj2 = null;
            if (htmlHelper.ViewData != null)
                obj2 = htmlHelper.ViewData.Eval(name);
            if (obj2 == null)
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, "MvcResources.HtmlHelper_MissingSelectData", new object[] { name, "IEnumerable<SelectListItem>" }));
            var enumerable = obj2 as IEnumerable<SelectListItem>;
            if (enumerable == null)
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, "MvcResources.HtmlHelper_WrongSelectDataType", new object[] { name, obj2.GetType().FullName, "IEnumerable<SelectListItem>" }));
            return enumerable;
        }

        internal static string ListItemToOption(string name, SelectListItem item)
        {
            var inputTag = new TagBuilder("input");
            inputTag.Attributes.Add("type", "radio");
            inputTag.Attributes.Add("name", name);
            if (item.Value != null)
                inputTag.Attributes["value"] = item.Value;
            if (item.Selected)
                inputTag.Attributes["checked"] = "checked";
            //
            var labelTag = new TagBuilder("label");
            labelTag.Attributes.Add("for", name);
            if (item.Text != null)
                labelTag.SetInnerText(item.Text);
            //
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("radioButton");
            divTag.InnerHtml = inputTag.ToString(TagRenderMode.Normal) + labelTag.ToString(TagRenderMode.Normal);
            return divTag.ToString(TagRenderMode.Normal);
        }
        #endregion
    }
}
