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
using System.Quality;
namespace System.Web.UI.WebControls
{
    public interface IHtmlTextBox { }

    /// <summary>
    /// HtmlTextBoxEx
    /// </summary>
    public class HtmlTextBoxEx : TextBoxEx
    {
        public HtmlTextBoxEx()
            : base()
        {
            TextMode = TextBoxMode.MultiLine;
        }

        public string HtmlTextEditorId { get; set; }

        public bool InDebugMode { get; set; }

        public string ToolbarId { get; set; }

        public string ResourceFolder { get; set; }

        public WebControl InternalEditor { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (ViewMode == FormFieldViewMode.Static)
                Text = Page.Request.Form[ClientID];
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var htmlTextBoxContext = ServiceLocator.Resolve<IHtmlTextBoxContext>(HtmlTextEditorId, ToolbarId, ResourceFolder);
            if (InDebugMode)
                htmlTextBoxContext.InDebugMode = true;
            InternalEditor = (WebControl)ServiceLocator.Resolve<IHtmlTextBox>(null, htmlTextBoxContext);
            InternalEditor.ID = ID;
            ID += "_Pane";
            InternalEditor.Width = Width;
            InternalEditor.Height = Height;
            Controls.Add(InternalEditor);
        }

        protected override void Render(HtmlTextWriter w)
        {
            switch (ViewMode)
            {
                case FormFieldViewMode.Static:
                case FormFieldViewMode.StaticWithHidden:
                case FormFieldViewMode.Hidden:
                    base.Render(w);
                    break;
                default:
                    InternalEditor.RenderControl(w);
                    break;
            }
        }
    }
}
