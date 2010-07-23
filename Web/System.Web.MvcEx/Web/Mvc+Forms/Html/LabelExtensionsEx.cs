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
using System.Linq.Expressions;
namespace System.Web.Mvc.Html
{
    public static class LabelExtensionsEx
    {
        public static MvcHtmlString LabelEx(this HtmlHelper html, string expression) { return LabelHelperEx(html, ModelMetadata.FromStringExpression(expression, html.ViewData), expression); }

        public static MvcHtmlString LabelForEx<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) { return LabelHelperEx(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression)); }

        public static MvcHtmlString LabelForModelEx(this HtmlHelper html) { return LabelHelperEx(html, html.ViewData.ModelMetadata, string.Empty); }

        internal static MvcHtmlString LabelHelperEx(HtmlHelper html, ModelMetadata metadata, string htmlFieldName)
        {
            string text = (metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>()));
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Empty;
            var labelTag = new TagBuilder("label");
            var name = metadata.PropertyName;
            ModelState state;
            if ((!string.IsNullOrEmpty(name)) && (html.ViewData.ModelState.TryGetValue(name, out state)) && (state.Errors.Count > 0))
                labelTag.AddCssClass(HtmlHelperExtensions.ValidationLabelCssClassName);
            labelTag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            labelTag.SetInnerText(text);
            return labelTag.ToMvcHtmlString(TagRenderMode.Normal);
        }
    }
}
