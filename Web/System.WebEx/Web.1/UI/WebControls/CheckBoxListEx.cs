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
using System.Text;
using System.Patterns.Form;
namespace System.Web.UI.WebControls
{
    /// <summary>
    /// CheckBoxListEx
    /// </summary>
    public class CheckBoxListEx : CheckBoxList, IFormControl
    {
        private string _staticTextSeparator = "<br />";

        public CheckBoxListEx()
            : base() { }

        public FormFieldViewMode ViewMode { get; set; }

        protected override void Render(HtmlTextWriter w)
        {
            switch (ViewMode)
            {
                case FormFieldViewMode.Static:
                    RenderStaticText(w);
                    break;
                case FormFieldViewMode.StaticWithHidden:
                    RenderStaticText(w);
                    RenderHidden(w);
                    break;
                case FormFieldViewMode.Hidden:
                    RenderHidden(w);
                    break;
                default:
                    base.Render(w);
                    break;
            }
        }

        protected void RenderHidden(HtmlTextWriter w)
        {
            var b = new StringBuilder();
            foreach (ListItem item in Items)
                if (item.Selected)
                    b.Append(item.Value + ",");
            if (b.Length > 0)
                b.Length--;
            w.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            w.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            w.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            w.AddAttribute(HtmlTextWriterAttribute.Value, b.ToString());
            w.RenderBeginTag(HtmlTextWriterTag.Input);
            w.RenderEndTag();
        }

        protected virtual void RenderStaticText(HtmlTextWriter w)
        {
            string staticTextSeparator = StaticTextSeparator;
            var b = new StringBuilder();
            foreach (ListItem item in Items)
                b.Append(HttpUtility.HtmlEncode(item.Text) + staticTextSeparator);
            int staticTextSeparatorLength = staticTextSeparator.Length;
            if (b.Length > staticTextSeparatorLength)
                b.Length -= staticTextSeparatorLength;
            w.AddAttribute(HtmlTextWriterAttribute.Class, "static");
            w.RenderBeginTag(HtmlTextWriterTag.Span);
            w.Write(b.ToString());
            w.RenderEndTag();
        }

        public string StaticTextSeparator
        {
            get { return _staticTextSeparator; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _staticTextSeparator = value;
            }
        }
    }
}
