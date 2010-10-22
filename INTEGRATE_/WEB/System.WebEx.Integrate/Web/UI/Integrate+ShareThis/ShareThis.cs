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
using System.Quality;
using System.Collections.Generic;
using System.Patterns.ReleaseManagement;
namespace System.Web.UI.Integrate
{
    using UIClientScript = ClientScript;

    /// <summary>
    /// ShareThis
    /// </summary>
    public class ShareThis : HtmlContainerControl
    {
        private static Type s_type = typeof(ShareThis);
        private string _scriptBody;

        public ShareThis()
            : base()
        {
            DeploymentTarget = DeploymentEnvironment.Production;
            Include = new ShareThisInclude();
            Inject = true;
        }

        public DeploymentEnvironment DeploymentTarget { get; set; }
        public string AttachToButtonId { get; set; }
        public bool UseClientScript { get; set; }
        public ShareThisInclude Include { get; set; }
        public ShareThisObject Object { get; set; }

        public bool Inject { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        [ServiceDependency]
        public IClientScriptManager ClientScriptManager { get; set; }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        /// <value>The button text.</value>
        public string ButtonText
        {
            get { return Include.ButtonText; }
            set { Include.ButtonText = value; }
        }

        /// <summary>
        /// Gets or sets the color of the header background.
        /// </summary>
        /// <value>The color of the header background.</value>
        public string HeaderBackgroundColor
        {
            get { return Include.HeaderBackgroundColor; }
            set { Include.HeaderBackgroundColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the header foreground.
        /// </summary>
        /// <value>The color of the header foreground.</value>
        public string HeaderForegroundColor
        {
            get { return Include.HeaderForegroundColor; }
            set { Include.HeaderForegroundColor = value; }
        }

        /// <summary>
        /// Gets or sets the header title.
        /// </summary>
        /// <value>The header title.</value>
        public string HeaderTitle
        {
            get { return Include.HeaderTitle; }
            set { Include.HeaderTitle = value; }
        }

        /// <summary>
        /// Gets or sets the post services.
        /// </summary>
        /// <value>The post services.</value>
        public string PostServices
        {
            get { return Include.PostServices; }
            set { Include.PostServices = value; }
        }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>The publisher.</value>
        public string Publisher
        {
            get { return Include.Publisher; }
            set { Include.Publisher = value; }
        }

        /// <summary>
        /// Gets or sets the send services.
        /// </summary>
        /// <value>The send services.</value>
        public string SendServices
        {
            get { return Include.SendServices; }
            set { Include.SendServices = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type
        {
            get { return Include.Type; }
            set { Include.Type = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Inject)
                ServiceLocator.Inject(this);
            if (Include == null)
                throw new ArgumentNullException("Include");
            base.OnPreRender(e);
            // include
            if (ClientScriptManager != null)
                ClientScriptManager.EnsureItem<HtmlHead>(ID, () => new IncludeClientScriptItem(GetIncludeUriString()));
        }

        protected override void Render(HtmlTextWriter w)
        {
            if (Object == null)
                throw new ArgumentNullException("Object");
            // object
            _scriptBody = GetObjectString();
            if (ClientScriptManager != null)
            {
                ClientScriptManager.AddRange(_scriptBody);
                _scriptBody = null;
            }
            //if (EnvironmentEx.DeploymentEnvironment == DeploymentTarget)
            //{
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
            //}
            //else
            //    w.WriteLine("<!-- Share This -->");
        }

        private string GetObjectString()
        {
            var objectLiteral = (Object as ShareThisObjectLiteral);
            if (objectLiteral != null)
                return objectLiteral.Literal;
            //
            var b = new StringBuilder("SHARETHIS.addEntry(");
            var obj = Object;
            bool hasButton = string.IsNullOrEmpty(AttachToButtonId);
            b.Append(SerializeObjectToJsonString(obj));
            b.Append(",");
            b.Append(SerializeObjectMetaToJsonString((obj.Meta == null ? ShareThisObjectMeta.Default : obj.Meta), hasButton));
            b.Append(")");
            //
            b.Append(!hasButton ? ".attachButton(document.getElementById('" + AttachToButtonId + "'));" : ";");
            return b.ToString();
        }

        private string GetIncludeUriString()
        {
            var includeLiteral = (Include as ShareThisIncludeLiteral);
            if (includeLiteral != null)
            {
                var value = includeLiteral.Literal;
                if (value.IndexOf("&amp;button=", StringComparison.OrdinalIgnoreCase) == -1)
                    value += "&amp;button=false";
                return value;
            }
            //
            var include = Include;
            if (string.IsNullOrEmpty(include.Type))
                throw new InvalidOperationException("'Type' cannot be null");
            return SerializeIncludeToUriString(include);
        }

        #region Serializers
        public static string SerializeIncludeToUriString(ShareThisInclude include)
        {
            // todo: build urlserialize here
            var b = new StringBuilder();
            b.Append(string.Format("http://w.sharethis.com/button/sharethis.js#type={0}", Uri.EscapeDataString(include.Type)));
            if (!string.IsNullOrEmpty(include.Publisher))
                b.Append("&amp;publisher=" + Uri.EscapeDataString(include.Publisher));
            if (!string.IsNullOrEmpty(include.ButtonText))
                b.Append("&amp;buttonText=" + Uri.EscapeDataString(include.ButtonText));
            if (!string.IsNullOrEmpty(include.HeaderBackgroundColor))
                b.Append("&amp;headerbg=" + Uri.EscapeDataString(include.HeaderBackgroundColor));
            if (!string.IsNullOrEmpty(include.HeaderForegroundColor))
                b.Append("&amp;headerfg=" + Uri.EscapeDataString(include.HeaderForegroundColor));
            if (!string.IsNullOrEmpty(include.HeaderTitle))
                b.Append("&amp;headerTitle=" + Uri.EscapeDataString(include.HeaderTitle));
            if (!string.IsNullOrEmpty(include.PostServices))
                b.Append("&amp;post_services=" + Uri.EscapeDataString(include.PostServices));
            if (!string.IsNullOrEmpty(include.SendServices))
                b.Append("&amp;send_services=" + Uri.EscapeDataString(include.SendServices));
            //foreach (var keyValue in _javascriptAttribs)
            //    b.Append("&amp;" + keyValue.Key + "=" + Uri.EscapeDataString(keyValue.Value));
            b.Append("&amp;button=false");
            return b.ToString();
        }

        public static string SerializeObjectToJsonString(ShareThisObject obj)
        {
            var b = new StringBuilder("{");
            var objectPropertyList = new List<string>();
            if (!string.IsNullOrEmpty(obj.Author))
                objectPropertyList.Add("author: " + UIClientScript.EncodeText(obj.Author));
            if (!string.IsNullOrEmpty(obj.Category))
                objectPropertyList.Add("category: " + UIClientScript.EncodeText(obj.Category));
            if (!string.IsNullOrEmpty(obj.Content))
                objectPropertyList.Add("content: " + UIClientScript.EncodeText(obj.Content));
            if (!string.IsNullOrEmpty(obj.Icon))
                objectPropertyList.Add("icon: " + UIClientScript.EncodeText(obj.Icon));
            if (!string.IsNullOrEmpty(obj.Published))
                objectPropertyList.Add("published: " + UIClientScript.EncodeText(obj.Published));
            if (!string.IsNullOrEmpty(obj.Summary))
                objectPropertyList.Add("summary: " + UIClientScript.EncodeText(obj.Summary));
            if (!string.IsNullOrEmpty(obj.Title))
                objectPropertyList.Add("title: " + UIClientScript.EncodeText(obj.Title));
            if (!string.IsNullOrEmpty(obj.Updated))
                objectPropertyList.Add("updated: " + UIClientScript.EncodeText(obj.Updated));
            if (!string.IsNullOrEmpty(obj.Url))
                objectPropertyList.Add("url: " + UIClientScript.EncodeText(obj.Url));
            //
            b.Append(string.Join(",", objectPropertyList.ToArray()));
            b.Append("}");
            return b.ToString();
        }

        public static string SerializeObjectMetaToJsonString(ShareThisObjectMeta objMeta, bool hasButton)
        {
            var b = new StringBuilder("{");
            b.Append("button: " + (hasButton ? "true" : "false"));
            b.AppendFormat(", embeds: {0}", UIClientScript.EncodeBool(objMeta.Embeds));
            if (objMeta.OffsetLeft != 0)
                b.AppendFormat(", offsetLeft: {0}", UIClientScript.EncodeInt32(objMeta.OffsetLeft));
            if (objMeta.OffsetTop != 0)
                b.AppendFormat(", offsetTop: {0}", UIClientScript.EncodeInt32(objMeta.OffsetTop));
            if (!string.IsNullOrEmpty(objMeta.OnClientClick))
                b.AppendFormat(", onclick: {0}", objMeta.OnClientClick);
            b.AppendFormat(", popup: {0}", UIClientScript.EncodeBool(objMeta.Popup));
            b.Append("}");
            return b.ToString();
        }
        #endregion
    }
}