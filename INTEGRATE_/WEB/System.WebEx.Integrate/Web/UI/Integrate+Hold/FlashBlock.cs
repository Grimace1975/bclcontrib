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
//namespace Instinct.ApplicationUnit_.WebApplicationControl
//{
//    //- FlashBlock -//
//    //: todo: set option for target on link
//    public class FlashBlock : System.Web.UI.WebControls.WebControl
//    {
//        private const string RequiredVersionTextA = @"<p>
//<a href=""http://www.adobe.com/go/getflashplayer"" target=""_blank""><img src=""http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"" alt=""Get Adobe Flash player"" style=""float: right""/></a>
//To view the flash video player, you need to have Flash Player {0} or higher installed, and JavaScript enabled. You may <a href=""http://www.adobe.com/go/getflashplayer"" target=""_blank"">download the Flash Player here</a>.</p>";
//        private static System.Type s_type = typeof(FlashBlock);
//        private System.Text.StringBuilder m_textBuilder = new System.Text.StringBuilder();
//        private string m_version = "7.0.0";
//        private string m_bgColor = "#000000";
//        private string m_uri = string.Empty;
//        private string m_alt = string.Empty;
//        private bool m_isHasMenu = false;
//        private bool m_isTransparent = false;
//        private System.TriState m_triFlashOrImage = System.TriState.False;
//        private System.TriState m_triHasFlash = System.TriState.Inherit;
//        private string m_linkUrl = string.Empty;
//        private VariableIndex m_variableIndex;
//        private FlashRenderType m_flashRenderType;
//        private string m_expressInstallUri = string.Empty;
//        private System.Web.UI.ClientScriptManager m_scriptManager;

//        //- FlashRenderType -//
//        public enum FlashRenderType
//        {
//            Kernel,
//            SwfObject1_5,
//            SwfObject2_0,
//            SwfObject2_0Dynamic
//        }

//        //- WMode -//
//        public class WMode
//        {
//            public const string Opaque = "opaque";
//            public const string Transparent = "transparent";
//            public const string Window = "window";
//        }

//        //- Main -//
//        public FlashBlock()
//            : this(FlashRenderType.Kernel)
//        {
//        }
//        public FlashBlock(FlashRenderType flashRenderType)
//            : base()
//        {
//            m_flashRenderType = flashRenderType;
//            m_variableIndex = new VariableIndex(this);
//            Width = new System.Web.UI.WebControls.Unit(300);
//            Height = new System.Web.UI.WebControls.Unit(300);
//        }

//        //- OnPreRender -//
//        protected override void OnPreRender(System.EventArgs e)
//        {
//            base.OnPreRender(e);
//            //+ have script-manager
//            if (Page != null)
//            {
//                m_scriptManager = Page.ClientScript;
//                if (m_expressInstallUri.Length == 0)
//                {
//                    m_expressInstallUri = m_scriptManager.GetWebResourceUrl(s_type, "Instinct.Resource_.Control.FlashExpressInstall.swf");
//                }
//                BuildScriptBlock(true, true);
//            }
//        }

//        //- BuildScriptBlock -//
//        private string BuildScriptBlock(bool isRegister, bool isBuildScriptTag)
//        {
//            string registerKey;
//            string clientId = (ClientID ?? string.Empty);
//            string width = Width.ToString();
//            string height = Height.ToString();
//            //+ build
//            System.Text.StringBuilder textBuilder = new System.Text.StringBuilder();
//            if (isBuildScriptTag == true)
//            {
//                textBuilder.AppendLine(@"<script type=""text/javascript"">
////<![CDATA[");
//            }
//            switch (m_flashRenderType)
//            {
//                case FlashRenderType.Kernel:
//                    registerKey = "FlashBlock";
//                    textBuilder.AppendFormat("Kernel.DetectBrowserFlash({0});\n", Kernel.ParseInt32(m_version.Split('.')[0], 7));
//                    break;
//                case FlashRenderType.SwfObject1_5:
//                    return null;
//                case FlashRenderType.SwfObject2_0:
//                    registerKey = null;
//                    textBuilder.AppendFormat("swfobject.registerObject('{0}','{1}'{2});\n", Instinct.ClientScript.EncodePartialText(clientId), m_version, (m_expressInstallUri.Length > 0 ? "," + Instinct.ClientScript.EncodeText(m_expressInstallUri) : string.Empty));
//                    break;
//                case FlashRenderType.SwfObject2_0Dynamic:
//                    registerKey = null;
//                    textBuilder.AppendFormat("swfobject.embedSWF('{0}','{1}','{2}','{3}','{4}'{5});\n", Instinct.ClientScript.EncodePartialText(m_uri), Instinct.ClientScript.EncodePartialText(clientId), width, height, m_version, (m_expressInstallUri.Length > 0 ? "," + Instinct.ClientScript.EncodeText(m_expressInstallUri) : string.Empty));
//                    break;
//                default:
//                    throw new InvalidOperationException();
//            }
//            if (isBuildScriptTag == true)
//            {
//                textBuilder.Append(@"//]]>
//</script>");
//            }
//            //+ register
//            if (isRegister == true)
//            {
//                if ((registerKey == null) || (m_scriptManager.IsClientScriptBlockRegistered(s_type, registerKey) == false))
//                {
//                    m_scriptManager.RegisterClientScriptBlock(s_type, registerKey, textBuilder.ToString(), false);
//                }
//                return null;
//            }
//            return textBuilder.ToString();
//        }

//        //- Clear -//
//        public void Clear()
//        {
//            m_textBuilder.Length = 0;
//            m_variableIndex.Clear();
//        }

//        //- Execute -//
//        protected override void Render(System.Web.UI.HtmlTextWriter writer)
//        {
//            HtmlBuilder z = HtmlBuilder.GetBuilder(writer);
//            string style = (Style.Value ?? string.Empty);
//            if ((m_triFlashOrImage == System.TriState.False) || ((m_triFlashOrImage == System.TriState.Inherit) && (m_uri.IndexOf(".swf", System.StringComparison.InvariantCultureIgnoreCase) != -1)))
//            {
//                string clientId = (ClientID ?? string.Empty);
//                string width = Width.ToString();
//                string height = Height.ToString();
//                if ((m_isTransparent == true) && (string.IsNullOrEmpty(Variable["wmode"]) == true))
//                {
//                    Variable["wmode"] = WMode.Transparent;
//                }
//                //string protocol = (System.Web.HttpContext.Current.Request.IsSecureConnection == false ? "http://" : "https://");
//                //+ render
//                switch (m_flashRenderType)
//                {
//                    case FlashRenderType.Kernel:
//                        string windowMode = Variable["wmode"];
//                        //+ render-script
//                        z.Write(string.Format(@"<script type=""text/javascript"">
////<![CDATA[{0}
//if ({1}) {{
//	var textBuilder = new Kernel.TextBuilder();
//	textBuilder.Append('<object{2} type=""application/x-shockwave-flash"" data=""{3}"" width=""{4}"" height=""{5}""{6}>');
//	textBuilder.Append('<param name=""movie"" value=""{3}"" />');
//	textBuilder.Append('<param name=""quality"" value=""high"" />');
//	textBuilder.Append('<param name=""scale"" value=""exactfit"" />');
//	textBuilder.Append('<param name=""bgcolor"" value=""{7}"" />');
//	textBuilder.Append('<param name=""menu"" value=""{8}"" />{9}{10}');
//	textBuilder.Append('</object>');
//	Kernel.Write(textBuilder.ToString());{11}
//}}
////]]>
//</script>"
//                            , BuildScriptBlock(false, false)
//                            , (m_triHasFlash == System.TriState.Inherit ? "Kernel.IsBrowserFlash == true" : Instinct.ClientScript.EncodeBool(m_triHasFlash == System.TriState.True))
//                            , (clientId.Length > 0 ? " id=\"" + Instinct.ClientScript.EncodePartialText(clientId) + "\"" : string.Empty)
//                            , Instinct.ClientScript.EncodePartialText(m_uri)
//                            , width
//                            , height
//                            , (style.Length == 0 ? string.Empty : " style=\"" + Instinct.ClientScript.EncodePartialText(style) + "\"")
//                            , m_bgColor
//                            , (m_isHasMenu == true ? "1" : "0")
//                            , (string.IsNullOrEmpty(windowMode) == true ? string.Empty : string.Format("<param name=\"wmode\" value=\"{0}\" />", windowMode))
//                            , (m_variableIndex.Count == 0 ? string.Empty : "<param name=\"flashvars\" value=\"" + Instinct.ClientScript.EncodePartialText(m_variableIndex.ToString()) + "\" />")
//                            , (m_textBuilder.Length > 0 ? "\n} else {\ndocument.write(unescape('" + Http.Escape(m_textBuilder.ToString()) + "'))" : string.Empty)
//                        ));
//                        break;
//                    case FlashRenderType.SwfObject1_5:
//                        //+ render
//                        z.AddHtmlAttrib(HtmlAttrib.Id, clientId);
//                        z.BeginHtmlTag(HtmlTag.Div);
//                        if (m_textBuilder.Length > 0)
//                        {
//                            z.Write(m_textBuilder.ToString());
//                        }
//                        z.EndHtmlTag();
//                        //+ render-script
//                        string variable = clientId;
//                        z.Write(string.Format(@"<script type=""text/javascript"">
////<![CDATA[
//var {0} = new SWFObject('{2}','{3}','{4}','{5}','{6}','{7}',true);{8}{9}
//{0}.write('{1}');
////]]>
//</script>"
//                            , variable
//                            , clientId
//                            , m_uri
//                            , clientId + "_0"
//                            , width
//                            , height
//                            , m_version
//                            , m_bgColor
//                            , m_variableIndex.ToString()
//                            , (m_expressInstallUri.Length > 0 ? "\n" + variable + ".useExpressInstall('" + m_expressInstallUri + "');" : string.Empty)
//                        ));
//                        break;
//                    case FlashRenderType.SwfObject2_0:
//                        //+ don't have script-manager
//                        if (m_scriptManager == null)
//                        {
//                            z.Write(BuildScriptBlock(false, true));
//                        }
//                        //+ render
//                        z.AddHtmlAttrib(HtmlAttrib.Id, clientId);
//                        z.AddHtmlAttrib(HtmlAttrib.StyleWidth, width);
//                        z.AddHtmlAttrib(HtmlAttrib.StyleHeight, height);
//                        z.AddAttribute("classid", "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000");
//                        z.BeginHtmlTag(HtmlTag.Object);
//                        //+
//                        z.Write(string.Format("<param name=\"movie\" value=\"{0}\"/>{1}\n", Http.HtmlEncode(m_uri), m_variableIndex.ToString()));
//                        z.Write("<!--[if !IE]>-->\n");
//                        z.AddHtmlAttrib(HtmlAttrib.Type, "application/x-shockwave-flash");
//                        z.AddAttribute("data", m_uri);
//                        z.AddHtmlAttrib(HtmlAttrib.StyleWidth, width);
//                        z.AddHtmlAttrib(HtmlAttrib.StyleHeight, height);
//                        z.BeginHtmlTag(HtmlTag.Object);
//                        z.Write("<!--<![endif]-->\n");
//                        z.BeginHtmlTag(HtmlTag.Div);
//                        if (m_textBuilder.Length > 0)
//                        {
//                            z.Write(m_textBuilder.ToString());
//                        }
//                        z.EndHtmlTag();
//                        z.Write("<!--[if !IE]>-->\n");
//                        z.EndHtmlTag();
//                        z.Write("<!--<![endif]-->\n");
//                        //+
//                        z.EndHtmlTag();
//                        break;
//                    case FlashRenderType.SwfObject2_0Dynamic:
//                        //+ don't have script-manager
//                        if (m_scriptManager == null)
//                        {
//                            z.Write(BuildScriptBlock(false, true));
//                        }
//                        //+ render
//                        z.AddHtmlAttrib(HtmlAttrib.Id, clientId);
//                        z.BeginHtmlTag(HtmlTag.Div);
//                        if (m_textBuilder.Length > 0)
//                        {
//                            z.Write(m_textBuilder.ToString());
//                        }
//                        z.EndHtmlTag();
//                        break;
//                }
//            }
//            else
//            {
//                bool isLinkUrl = ((m_linkUrl != null) && (m_linkUrl.Length > 0));
//                if (isLinkUrl == true)
//                {
//                    z.AddHtmlAttrib(HtmlAttrib.Target, "_blank");
//                    z.AddHtmlAttrib(HtmlAttrib.Href, m_linkUrl);
//                    z.BeginHtmlTag(HtmlTag.A);
//                }
//                if ((style != null) && (style.Length > 0))
//                {
//                    z.AddHtmlAttrib(HtmlAttrib.Style, style);
//                }
//                z.AddHtmlAttrib(HtmlAttrib.Src, m_uri);
//                z.AddHtmlAttrib(HtmlAttrib.Alt, m_alt);
//                z.EmptyHtmlTag(HtmlTag.Img);
//                if (isLinkUrl == true)
//                {
//                    z.EndHtmlTag();
//                }
//            }
//        }

//        //- Write -//
//        public void Write(string text)
//        {
//            if ((text != null) && (text.Length > 0))
//            {
//                m_textBuilder.Append(text);
//            }
//        }
//        public void Write(System.Web.UI.Control control)
//        {
//            if (control != null)
//            {
//                System.IO.StringWriter textWriter = new System.IO.StringWriter();
//                System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(textWriter);
//                control.RenderControl(htmlTextWriter);
//                htmlTextWriter.Close();
//                m_textBuilder.Append(textWriter.ToString());
//            }
//        }

//        //- WriteExpressLink -//
//        public virtual void WriteExpressLink()
//        {
//            m_textBuilder.Length = 0;
//            m_textBuilder.Append(string.Format(RequiredVersionTextA, m_version.ToString()));
//        }

//        //- VARIABLE -//
//        #region VARIABLE
//        //- Variable -//
//        public CollectionIndexBase<string, string> Variable
//        {
//            get
//            {
//                return m_variableIndex;
//            }
//        }
//        private sealed class VariableIndex : Index.HashCollectionIndex<string, string>
//        {
//            private FlashBlock m_flashBlock;

//            //- Main -//
//            public VariableIndex(FlashBlock flashBlock)
//                : base(new Hash<string, string>())
//            {
//                m_flashBlock = flashBlock;
//            }

//            //- ToString -//
//            public override string ToString()
//            {
//                System.Text.StringBuilder textBuilder = new System.Text.StringBuilder();
//                //+ render
//                switch (m_flashBlock.m_flashRenderType)
//                {
//                    case FlashRenderType.Kernel:
//                        foreach (string key in m_hash.KeyEnum)
//                        {
//                            textBuilder.Append(key + "=");
//                            textBuilder.Append(Http.HtmlEncode(m_hash[key]));
//                            textBuilder.Append("&");
//                        }
//                        if (textBuilder.Length > 0)
//                        {
//                            textBuilder.Length--;
//                        }
//                        break;
//                    case FlashRenderType.SwfObject1_5:
//                        string variable = m_flashBlock.ClientID;
//                        foreach (string key in m_hash.KeyEnum)
//                        {
//                            textBuilder.AppendFormat("\n{0}.addVariable('{1}','{2}');", variable, key, Instinct.ClientScript.EncodePartialText(m_hash[key]));
//                        }
//                        break;
//                    case FlashRenderType.SwfObject2_0:
//                        foreach (string key in m_hash.KeyEnum)
//                        {
//                            textBuilder.AppendFormat("<param name=\"{0}\" value=\"{1}\"/>", Http.HtmlEncode(key), Http.HtmlEncode(m_hash[key]));
//                        }
//                        break;
//                }
//                return textBuilder.ToString();
//            }
//        }
//        #endregion VARIABLE

//        //- PROPERTY -//
//        #region PROPERTY
//        //- Alt -//
//        public string Alt
//        {
//            get
//            {
//                return m_alt;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_alt = value;
//            }
//        }

//        //- BgColor -//
//        public string BgColor
//        {
//            get
//            {
//                return m_bgColor;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_bgColor = value;
//            }
//        }

//        //- ExpressInstallUri -//
//        public string ExpressInstallUri
//        {
//            get
//            {
//                return m_expressInstallUri;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_expressInstallUri = value;
//            }
//        }

//        //- IsHasMenu -//
//        public bool IsHasMenu
//        {
//            get
//            {
//                return m_isHasMenu;
//            }
//            set
//            {
//                m_isHasMenu = value;
//            }
//        }

//        //- IsTransparent -//
//        public bool IsTransparent
//        {
//            get
//            {
//                return m_isTransparent;
//            }
//            set
//            {
//                m_isTransparent = value;
//            }
//        }

//        //- LinkUrl -//
//        public string LinkUrl
//        {
//            get
//            {
//                return m_linkUrl;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_linkUrl = value;
//            }
//        }

//        //- TriFlashOrImage -//
//        public System.TriState TriFlashOrImage
//        {
//            get
//            {
//                return m_triFlashOrImage;
//            }
//            set
//            {
//                m_triFlashOrImage = value;
//            }
//        }

//        //- TriHasFlash -//
//        public System.TriState TriHasFlash
//        {
//            get
//            {
//                return m_triHasFlash;
//            }
//            set
//            {
//                m_triHasFlash = value;
//            }
//        }

//        //- Uri -//
//        public string Uri
//        {
//            get
//            {
//                return m_uri;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_uri = value;
//            }
//        }

//        //- Version -//
//        public string Version
//        {
//            get
//            {
//                return m_version;
//            }
//            set
//            {
//                if ((value == null) || (value.Length == 0))
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_version = value;
//            }
//        }

//        //- WindowMode -//
//        public string WindowMode
//        {
//            get
//            {
//                return Variable["wmode"];
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                Variable["wmode"] = value;
//            }
//        }
//        #endregion PROPERTY
//    }
//}
