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
using System.Web.UI.HtmlControls;
namespace System.Web.UI.Integrate
{
    /// <summary>
    /// FacebookFeature
    /// </summary>
    public class FacebookFeature : HtmlContainerControl
    {
        private static Type s_type = typeof(FacebookFeature);

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookFeature"/> class.
        /// </summary>
        public FacebookFeature()
            : base("div") { }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var page = (Page != null ? Page : (HttpContext.Current.Handler as Page));
            if (page != null)
            {
                var clientScript = page.ClientScript;
                clientScript.RegisterClientScriptInclude("fbFeatureLoader", "http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php/en_US");
                clientScript.RegisterClientScriptBlock(s_type, "init", "<script type=\"text/javascript\">FB.init(\"" + FeatureUid + "\");</script>", false);
            }
        }

        /// <summary>
        /// Gets or sets the feature uid.
        /// </summary>
        /// <value>The feature uid.</value>
        public string FeatureUid { get; set; }
    }
}
