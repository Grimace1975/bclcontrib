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
using System.Collections.Generic;
using System.Web.UI;
namespace System.Web.Mvc.Html.Integrate
{
    /// <summary>
    /// TinyMceTextAreaHelper
    /// </summary>
    public class TinyMceTextAreaHelper : IHtmlTextBox
    {
        private IHtmlTextBoxContext _htmlTextBoxContext;

        public TinyMceTextAreaHelper(IHtmlTextBoxContext htmlTextBoxContext)
        {
            if (htmlTextBoxContext != null)
                throw new ArgumentNullException("htmlTextBoxContext");
            _htmlTextBoxContext = htmlTextBoxContext;
        }

        public MvcHtmlString HtmlTextAreaHelper(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes)
        {
            throw new NotSupportedException();
            //ModelState state;
            //string attemptedValue;
            //string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
            //if (string.IsNullOrEmpty(fullHtmlFieldName))
            //    throw new ArgumentException("name");
            //var b = new TagBuilder("textarea");
            //b.GenerateId(fullHtmlFieldName);
            //b.MergeAttributes<string, object>(htmlAttributes, true);
            //b.MergeAttributes<string, object>(rowsAndColumns, rowsAndColumns != TextAreaExtensionsEx._implicitRowsAndColumns);
            //b.MergeAttribute("name", fullHtmlFieldName, true);
            //if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out state) && (state.Errors.Count > 0))
            //    b.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            //if ((state != null) && (state.Value != null))
            //    attemptedValue = state.Value.AttemptedValue;
            //else if (modelMetadata.Model != null)
            //    attemptedValue = modelMetadata.Model.ToString();
            //else
            //    attemptedValue = string.Empty;
            //b.SetInnerText(Environment.NewLine + attemptedValue);
            //return MvcHtmlString.Create(b.ToString(TagRenderMode.Normal));
        }
    }
}