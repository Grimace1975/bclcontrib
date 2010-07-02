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
using System.Text;
using System.Collections.Generic;
using System.Patterns.ReleaseManagement;
namespace System.Web.UI.Integrate
{
    public class ShareThis : HtmlContainerControl
    {
        private static Type s_type = typeof(ShareThis);
        private string _scriptBody;

        public ShareThis()
            : base()
        {
            DeploymentTarget = DeploymentEnvironment.Live;
            //Kernel kernel = Kernel.Instance;
            //Type = (string)kernel.Hash["ShareThisType"] ?? "website";
            //Publisher = (string)kernel.Hash["ShareThisPublisher"];
            //Embeds = false;
            //Popup = false;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string rawIncludeUri = RawIncludeUri;
            // client-script
            var manager = Page.ClientScript;
            if (!manager.IsClientScriptIncludeRegistered(s_type, string.Empty))
            {
                var b = new StringBuilder();
                if (string.IsNullOrEmpty(rawIncludeUri))
                {
                    if (string.IsNullOrEmpty(Publisher))
                        throw new InvalidOperationException("'Publisher' cannot be null");
                    if (string.IsNullOrEmpty(Type))
                        throw new InvalidOperationException("'Type' cannot be null");
                    b.Append(string.Format("http://w.sharethis.com/button/sharethis.js#publisher={0}&amp;type={1}&amp;button=false", Uri.EscapeDataString(Publisher), Uri.EscapeDataString(Type)));
                    if (!string.IsNullOrEmpty(ButtonText))
                        b.Append("&amp;buttonText=" + Uri.EscapeDataString(ButtonText));
                    if (!string.IsNullOrEmpty(HeaderBackgroundColor))
                        b.Append("&amp;headerbg=" + Uri.EscapeDataString(HeaderBackgroundColor));
                    if (!string.IsNullOrEmpty(HeaderForegroundColor))
                        b.Append("&amp;headerfg=" + Uri.EscapeDataString(HeaderForegroundColor));
                    if (!string.IsNullOrEmpty(HeaderTitle))
                        b.Append("&amp;headerTitle=" + Uri.EscapeDataString(HeaderTitle));
                    if (!string.IsNullOrEmpty(PostServices))
                        b.Append("&amp;post_services=" + Uri.EscapeDataString(PostServices));
                    if (!string.IsNullOrEmpty(SendServices))
                        b.Append("&amp;send_services=" + Uri.EscapeDataString(SendServices));
                    //foreach (var keyValue in _javascriptAttribs)
                    //    b.Append("&amp;" + keyValue.Key + "=" + Uri.EscapeDataString(keyValue.Value));
                }
                else if (rawIncludeUri.IndexOf(";button=", StringComparison.OrdinalIgnoreCase) == -1)
                    b.Append("&button=false");
                manager.RegisterClientScriptInclude(s_type, string.Empty, b.ToString());
            }
            // client-script
            var clientScript = ServiceLocatorEx.Resolve<IClientScript>();
            if (string.IsNullOrEmpty(rawIncludeUri))
            {
                var scriptBody = new StringBuilder("SHARETHIS.addEntry({");
                // object properties
                var objectPropertyList = new List<string>();
                if (!string.IsNullOrEmpty(Author))
                    objectPropertyList.Add("author: " + ClientScript.EncodeText(Author));
                if (!string.IsNullOrEmpty(Category))
                    objectPropertyList.Add("category: " + ClientScript.EncodeText(Category));
                if (!string.IsNullOrEmpty(Content))
                    objectPropertyList.Add("content: " + ClientScript.EncodeText(Content));
                if (!string.IsNullOrEmpty(Icon))
                    objectPropertyList.Add("icon: " + ClientScript.EncodeText(Icon));
                if (!string.IsNullOrEmpty(Published))
                    objectPropertyList.Add("published: " + ClientScript.EncodeText(Published));
                if (!string.IsNullOrEmpty(Summary))
                    objectPropertyList.Add("summary: " + ClientScript.EncodeText(Summary));
                if (!string.IsNullOrEmpty(Title))
                    objectPropertyList.Add("title: " + ClientScript.EncodeText(Title));
                if (!string.IsNullOrEmpty(Updated))
                    objectPropertyList.Add("updated: " + ClientScript.EncodeText(Updated));
                if (!string.IsNullOrEmpty(Url))
                    objectPropertyList.Add("url: " + ClientScript.EncodeText(Url));
                //
                scriptBody.Append(string.Join(",", objectPropertyList.ToArray()));
                scriptBody.Append("}, {");
                // object flags
                bool renderInline = string.IsNullOrEmpty(AttachToButtonId);
                scriptBody.Append("button: " + (renderInline ? "true" : "false"));
                scriptBody.AppendFormat(", embeds: {0}", ClientScript.EncodeBool(Embeds));
                if (OffsetLeft != 0)
                    scriptBody.AppendFormat(", offsetLeft: {0}", ClientScript.EncodeInt32(OffsetLeft));
                if (OffsetTop != 0)
                    scriptBody.AppendFormat(", offsetTop: {0}", ClientScript.EncodeInt32(OffsetTop));
                if (!string.IsNullOrEmpty(OnClientClick))
                    scriptBody.AppendFormat(", onclick: {0}", OnClientClick);
                scriptBody.AppendFormat(", popup: {0}", ClientScript.EncodeBool(Popup));
                scriptBody.Append("})");
                if (!renderInline)
                {
                    scriptBody.Append(".attachButton(document.getElementById('" + AttachToButtonId + "'));");
                    clientScript.AddBlock(scriptBody.ToString());
                }
                else
                {
                    scriptBody.Append(";");
                    _scriptBody = scriptBody.ToString();
                }
            }
            else
                _scriptBody = RawScriptBody;
        }

        protected override void Render(HtmlTextWriter w)
        {
            if (EnvironmentEx.DeploymentEnvironment == DeploymentTarget)
            {
                if (!string.IsNullOrEmpty(_scriptBody))
                {
                    w.Write(@"<script type=""text/javascript"">
// <![CDATA[
");
                    w.Write(_scriptBody);
                    w.Write(@"
// ]]>
</script>");
                }
            }
            else
                w.WriteLine("<!-- Share This -->");
        }

        public DeploymentEnvironment DeploymentTarget { get; set; }

        public string AttachToButtonId { get; set; }

        public string RawScriptBody { get; set; }

        public string RawIncludeUri { get; set; }

        #region Url Parameters
        ///// <summary>
        ///// Gets or sets a value indicating whether this <see cref="ShareThis"/> is button.
        ///// </summary>
        ///// <value><c>true</c> if button; otherwise, <c>false</c>.</value>
        //public bool Button { get; set; }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        /// <value>The button text.</value>
        public string ButtonText { get; set; }

        /// <summary>
        /// Gets or sets the color of the header background.
        /// </summary>
        /// <value>The color of the header background.</value>
        public string HeaderBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the header foreground.
        /// </summary>
        /// <value>The color of the header foreground.</value>
        public string HeaderForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the header title.
        /// </summary>
        /// <value>The header title.</value>
        public string HeaderTitle { get; set; }

        /// <summary>
        /// Gets or sets the post services.
        /// </summary>
        /// <value>The post services.</value>
        public string PostServices { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>The publisher.</value>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the send services.
        /// </summary>
        /// <value>The send services.</value>
        public string SendServices { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }
        #endregion

        #region Object Flags
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ShareThis"/> allows embedded elements to be seen while iFrame is loading.
        /// </summary>
        /// <value><c>true</c> if embedded elements can be seen while iFrame is loading; otherwise, <c>false</c>.</value>
        public bool Embeds { get; set; }

        /// <summary>
        /// Gets or sets the offset left.
        /// </summary>
        /// <value>The offset left.</value>
        public int OffsetLeft { get; set; }

        /// <summary>
        /// Gets or sets the offset top.
        /// </summary>
        /// <value>The offset top.</value>
        public int OffsetTop { get; set; }

        /// <summary>
        /// Gets or sets the on client click.
        /// </summary>
        /// <value>The on client click.</value>
        public string OnClientClick { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ShareThis"/> widget launches in a new window rather than an iFrame.
        /// </summary>
        /// <value><c>true</c> if widget launches in a new window rather than an iFrame; otherwise, <c>false</c>.</value>
        public bool Popup { get; set; }
        #endregion

        #region Object Propertys
        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the published.
        /// </summary>
        /// <value>The published.</value>
        public string Published { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public string Updated { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }
        #endregion

        private void SetValue(Dictionary<string, string> attrib, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
                attrib[key] = value;
            else if (attrib.ContainsKey(key))
                attrib.Remove(key);
        }
    }
}