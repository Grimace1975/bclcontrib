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
using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.UI;
using System.Quality;
namespace System.Web.Mvc.Html
{
    /// <summary>
    /// TextAreaExtensionsEx
    /// </summary>
    public static partial class TextAreaExtensionsEx
    {
        internal static Dictionary<string, object> _implicitRowsAndColumns;
        private const int TextAreaColumns = 20;
        private const int TextAreaRows = 2;

        static TextAreaExtensionsEx()
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("rows", 2.ToString(CultureInfo.InvariantCulture));
            dictionary.Add("cols", 20.ToString(CultureInfo.InvariantCulture));
            _implicitRowsAndColumns = dictionary;
        }

        private static Dictionary<string, object> GetRowsAndColumnsDictionary(int rows, int columns)
        {
            if (rows < 0)
                throw new ArgumentOutOfRangeException("rows");
            if (columns < 0)
                throw new ArgumentOutOfRangeException("columns");
            var dictionary = new Dictionary<string, object>();
            if (rows > 0)
                dictionary.Add("rows", rows.ToString(CultureInfo.InvariantCulture));
            if (columns > 0)
                dictionary.Add("cols", columns.ToString(CultureInfo.InvariantCulture));
            return dictionary;
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.HtmlTextArea(name, null, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.HtmlTextArea(name, null, htmlAttributes);
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return htmlHelper.HtmlTextArea(name, null, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, string value)
        {
            return htmlHelper.HtmlTextArea(name, value, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, string value, IDictionary<string, object> htmlAttributes)
        {
            var modelMetadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewContext.ViewData);
            if (value != null)
                modelMetadata.Model = value;
            var editor = GetEditor(htmlAttributes);
            return editor.HtmlTextAreaHelper(htmlHelper, modelMetadata, name, _implicitRowsAndColumns, htmlAttributes);
        }

        private static IHtmlTextBox GetEditor(IDictionary<string, object> htmlAttributes)
        {
            object value;
            bool inDebugMode = (!htmlAttributes.TryGetValue("resourceFolder", out value) ? (bool)value : false);
            string htmlTextEditorId = (!htmlAttributes.TryGetValue("htmlTextEditorId", out value) ? (string)value : string.Empty);
            string toolbarId = (!htmlAttributes.TryGetValue("toolbarId", out value) ? (string)value : string.Empty);
            string resourceFolder = (!htmlAttributes.TryGetValue("resourceFolder", out value) ? (string)value : string.Empty);
            //
            var htmlTextBoxContext = ServiceLocator.Resolve<IHtmlTextBoxContext>(htmlTextEditorId, toolbarId, resourceFolder);
            if (inDebugMode)
                htmlTextBoxContext.InDebugMode = true;
            return ServiceLocator.Resolve<IHtmlTextBox>(null, htmlTextBoxContext);
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
        {
            return htmlHelper.HtmlTextArea(name, value, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, IDictionary<string, object> htmlAttributes)
        {
            var modelMetadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewContext.ViewData);
            if (value != null)
                modelMetadata.Model = value;
            var editor = GetEditor(htmlAttributes);
            return editor.HtmlTextAreaHelper(htmlHelper, modelMetadata, name, GetRowsAndColumnsDictionary(rows, columns), htmlAttributes);
        }

        public static MvcHtmlString HtmlTextArea(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, object htmlAttributes)
        {
            return htmlHelper.HtmlTextArea(name, value, rows, columns, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString HtmlTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.HtmlTextAreaFor<TModel, TProperty>(expression, ((IDictionary<string, object>)null));
        }

        public static MvcHtmlString HtmlTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            var editor = GetEditor(htmlAttributes);
            return editor.HtmlTextAreaHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), _implicitRowsAndColumns, htmlAttributes);
        }

        public static MvcHtmlString HtmlTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.HtmlTextAreaFor<TModel, TProperty>(expression, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }

        public static MvcHtmlString HtmlTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            var editor = GetEditor(htmlAttributes);
            return editor.HtmlTextAreaHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), GetRowsAndColumnsDictionary(rows, columns), htmlAttributes);
        }

        public static MvcHtmlString HtmlTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
        {
            return htmlHelper.HtmlTextAreaFor<TModel, TProperty>(expression, rows, columns, ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
        }
    }
}
