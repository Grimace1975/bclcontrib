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
namespace System.Web.UI.WebControls
{
    /// <summary>
    /// JscriptAndCookieChecker
    /// </summary>
    public class JscriptAndCookieChecker : Control
    {
        public JscriptAndCookieChecker()
            : base()
        {
            CheckCookies = (HttpContext.Current.Request.Cookies.Count == 0);
            JscriptText = "JavaScript is currently not supported or is disabled by this browser. Please enable JavaScript for full functionality.";
            CookiesText = "Please enable cookies in your browser to experience all the full functionality of our site.";
            CookieValue = "hasCookie=1";
        }

        protected override void Render(HtmlTextWriter w)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Noscript);
            w.AddAttribute(HtmlTextWriterAttribute.Id, "noscript");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.Write(JscriptText);
            w.RenderEndTag();
            w.RenderEndTag();
            //
            if (CheckCookies)
            {
                w.AddAttribute(HtmlTextWriterAttribute.Id, "nocookies");
                w.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                w.RenderBeginTag(HtmlTextWriterTag.Div);
                w.Write(CookiesText);
                w.RenderEndTag();
                //
                w.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                w.RenderBeginTag(HtmlTextWriterTag.Script);
                w.Write("document.cookie='"); w.Write(CookieValue); w.Write("';if(!document.cookie.length)document.getElementById('nocookies').style.display='';");
                w.RenderEndTag();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check cookies].
        /// </summary>
        /// <value><c>true</c> if [check cookies]; otherwise, <c>false</c>.</value>
        public bool CheckCookies { get; set; }

        /// <summary>
        /// Gets or sets the jscript text.
        /// </summary>
        /// <value>The jscript text.</value>
        public string JscriptText { get; set; }

        /// <summary>
        /// Gets or sets the cookies text.
        /// </summary>
        /// <value>The cookies text.</value>
        public string CookiesText { get; set; }

        /// <summary>
        /// Gets or sets the cookie value.
        /// </summary>
        /// <value>The cookie value.</value>
        public string CookieValue { get; set; }
    }
}
