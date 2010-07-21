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
using System.Patterns.Form;
namespace System.Web.UI.WebControls
{
    /// <summary>
    /// TextBoxEx
    /// </summary>
    public class TextBoxEx : TextBox, IFormControl
    {
        public TextBoxEx()
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
            w.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            w.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            w.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            w.AddAttribute(HtmlTextWriterAttribute.Value, Text);
            w.RenderBeginTag(HtmlTextWriterTag.Input);
            w.RenderEndTag();
        }

        protected virtual void RenderStaticText(HtmlTextWriter w)
        {
            w.AddAttribute(HtmlTextWriterAttribute.Class, "static");
            w.RenderBeginTag(HtmlTextWriterTag.Span);
            switch (TextMode)
            {
                case TextBoxMode.MultiLine:
                    w.WriteEncodedText(Text);
                    //TODO: split content, encode each line, join with <br />
                    //w.WriteEncodedText((Text != null ? Text.Replace("\n", "<br />");
                    break;
                case TextBoxMode.Password:
                    w.Write("********");
                    break;
                case TextBoxMode.SingleLine:
                    w.WriteEncodedText(Text);
                    break;
            }
            w.RenderEndTag();
        }
    }
}
