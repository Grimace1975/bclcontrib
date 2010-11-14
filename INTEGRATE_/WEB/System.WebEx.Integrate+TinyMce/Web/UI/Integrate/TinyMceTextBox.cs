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
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace System.Web.UI.Integrate
{
    /// <summary>
    /// HtmlTextBox
    /// </summary>
    [ParseChildren(false)]
    public partial class TinyMceTextBox : TextBox, IHtmlTextBox
    {
        private static Type s_type = typeof(TinyMceTextBox);
        private IHtmlTextBoxContext _htmlTextBoxContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyMceTextBox"/> class.
        /// </summary>
        /// <param name="htmlTextEditorState">State of the HTML text editor.</param>
        public TinyMceTextBox(IHtmlTextBoxContext htmlTextBoxContext)
            : base()
        {
            if (htmlTextBoxContext != null)
                throw new ArgumentNullException("htmlTextBoxContext");
            ctr_TinyMceTextBox();
            _htmlTextBoxContext = htmlTextBoxContext;
            TextMode = TextBoxMode.MultiLine;
            //
            IsDebugMode = _htmlTextBoxContext.InDebugMode;
        }

        public static string RebaseUrl(string url)
        {
            var applicationPath = HttpContext.Current.Request.ApplicationPath;
            return (applicationPath == "/" ? url : applicationPath + url);
        }

        public bool IsDebugMode { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
            Authenticate();
        }

        protected override void OnPreRender(EventArgs e)
        {
            AddElementCssUri(_htmlTextBoxContext.ElementCssUri);
            settings["theme"] = "advanced";
            settings["skin"] = "o2k7";
            settings["skin_variant"] = "silver";
            settings["plugins"] = "safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,imagemanager,filemanager";
            settings["theme_advanced_buttons1"] = "bold,italic,underline,strikethrough,sub,sup,|,justifyleft,justifycenter,justifyright,justifyfull,|,code,|,forecolor,styleselect,formatselect";
            settings["theme_advanced_buttons2"] = "spellchecker,|,pastetext,|,search,replace,|,bullist,numlist,advhr,|,outdent,indent,|,undo,redo,|,link,unlink,anchor" + (string.IsNullOrEmpty(ResourcePath) == false ? ",|,insertfile,insertimage" : string.Empty);
            settings["theme_advanced_buttons3"] = "tablecontrols,|,removeformat,visualaid,|,charmap,media,|,fullscreen";
            settings["theme_advanced_toolbar_location"] = "top";
            settings["theme_advanced_toolbar_align"] = "left";
            settings["theme_advanced_path_location"] = "bottom";
            settings["theme_advanced_resizing"] = "true";
            settings["debug"] = IsDebugMode.ToString().ToLowerInvariant();
            settings["content_css"] = _htmlTextBoxContext.DesignModeCssUri;
            // file/image manager
            settings["imagemanager_path"] = settings["filemanager_path"] = "deg:" + ResourcePath;
            //
            string elementStyleSeries;
            string bodyCssStyleSeries;
            string anchorCssStyleSeries;
            string tableCssStyleSeries;
            string imageCssStyleSeries;
            string fontSeries;
            BuildStyleSeries(out elementStyleSeries, out bodyCssStyleSeries, out anchorCssStyleSeries, out tableCssStyleSeries, out imageCssStyleSeries, out fontSeries);
            settings["theme_advanced_blockformats"] = elementStyleSeries;
            settings["theme_advanced_styles"] = bodyCssStyleSeries;
            settings["theme_advanced_anchor_styles"] = anchorCssStyleSeries;
            settings["theme_advanced_table_styles"] = tableCssStyleSeries;
            settings["theme_advanced_image_styles"] = imageCssStyleSeries;
            Settings["theme_advanced_fonts"] = fontSeries;
            base.OnPreRender(e);
        }

        public static void OpenFileManagerScript(object manager, out string clientEvent, out string clientScript)
        {
            Authenticate();
            //
            clientEvent = "mcFileManager.browse({});";
            var clientScriptManager = (ClientScriptManager)manager;
            if (!clientScriptManager.IsClientScriptIncludeRegistered(s_type, string.Empty))
            {
                clientScriptManager.RegisterClientScriptInclude(s_type, string.Empty, RebaseUrl("/TinyMCE.ashx?module=GzipModule&themes=advanced&plugins=safari,inlinepopups,filemanager&languages=en"));
                clientScript = @"document.write('<textarea id=""pageResourceStub"" style=""display:none""></textarea>');tinyMCE.init({ elements: 'pageResourceStub', plugins: '-safari,-inlinepopups,-filemanager', mode: 'exact', language: 'en' });";
            }
            else
                clientScript = null;
        }

        /// <summary>
        /// Handles the PreRenderComplete event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(s_type, string.Empty))
                Page.ClientScript.RegisterClientScriptInclude(s_type, string.Empty, ScriptURI);
        }

        public static void Authenticate()
        {
            var context = HttpContext.Current;
            var session = context.Session;
            session["mc_isLoggedIn"] = "true";
            session["filemanager.preview.urlprefix"] = session["imagemanager.preview.urlprefix"] = "{proto}://" + context.Request.ServerVariables["HTTP_HOST"] + "/";
        }

        public string ResourceId { get; set; }

        public string ResourcePath { get; set; }

        #region Render
        protected override void Render(HtmlTextWriter w)
        {
            bool first = true;
            // Write script tag start
            w.WriteBeginTag("script");
            w.WriteAttribute("type", "text/javascript");
            w.Write(HtmlTextWriter.TagRightChar);
            w.Write("tinyMCE.init({\n");
            var settings = Settings;
            foreach (string key in settings.Keys)
            {
                string val = settings[key];
                if (!first)
                    w.Write(",\n");
                else
                    first = false;
                // Is boolean state or string
                w.Write(key + ((val == "true") || (val == "false") ? ":" + val : ":'" + val + "'"));
            }
            w.Write("\n});\n");
            w.WriteEndTag("script");
            //
            int rows = (Rows > 0 ? Rows : 2);
            int columns = (Columns > 0 ? Columns : 20);
            w.AddAttribute(HtmlTextWriterAttribute.Rows, rows.ToString());
            w.AddAttribute(HtmlTextWriterAttribute.Cols, columns.ToString());
            if (Width.Value > 0)
                w.AddStyleAttribute(HtmlTextWriterStyle.Width, Width.ToString());
            if (Height.Value > 0)
                w.AddStyleAttribute(HtmlTextWriterStyle.Height, Height.ToString());
            if (!Wrap)
                w.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Wrap, "off");
            if (ReadOnly)
                w.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.ReadOnly, "readonly");
            //AddAttributesToRender(w);
            base.Render(w);
        }

        private void BuildStyleSeries(out string elementStyleSeries, out string bodyCssStyleSeries, out string anchorCssStyleSeries, out string tableCssStyleSeries, out string imageCssStyleSeries, out string fontSeries)
        {
            elementStyleSeries = (_htmlTextBoxContext.ElementStyles.Count > 0 ? string.Join(",", _htmlTextBoxContext.ElementStyles
                .Select(c => c.Value)
                .ToArray()) : string.Empty);
            bodyCssStyleSeries = (_htmlTextBoxContext.BodyCssStyles.Count > 0 ? string.Join(";", _htmlTextBoxContext.BodyCssStyles
                .Select(c => c.Key + "=" + c.Value)
                .ToArray()) : string.Empty);
            anchorCssStyleSeries = (_htmlTextBoxContext.AnchorCssStyles.Count > 0 ? string.Join(";", _htmlTextBoxContext.AnchorCssStyles
                .Select(c => c.Key + "=" + c.Value)
                .ToArray()) : string.Empty);
            tableCssStyleSeries = (_htmlTextBoxContext.TableCssStyles.Count > 0 ? string.Join(";", _htmlTextBoxContext.TableCssStyles
                .Select(c => c.Key + "=" + c.Value)
                .ToArray()) : string.Empty);
            imageCssStyleSeries = (_htmlTextBoxContext.ImageCssStyles.Count > 0 ? string.Join(";", _htmlTextBoxContext.ImageCssStyles
                .Select(c => c.Key + "=" + c.Value)
                .ToArray()) : string.Empty);
            fontSeries = (_htmlTextBoxContext.Fonts.Count > 0 ? string.Join(";", _htmlTextBoxContext.Fonts
                .Select(c => c.Key + "=" + c.Value)
                .ToArray()) : string.Empty);
        }
        #endregion

        #region Utility Methods
        private void AddElementCssUri(string cssUri)
        {
            if (!string.IsNullOrEmpty(cssUri))
            {
                string linkId = "TinyMceStyleSheet";
                if (!ContainsControlId(Page.Header, linkId))
                {
                    var link = new HtmlControls.HtmlGenericControl("link")
                    {
                        ID = linkId,
                    };
                    link.Attributes.Add("href", cssUri);
                    link.Attributes.Add("rel", "stylesheet");
                    link.Attributes.Add("type", "text/css");
                    Page.Header.Controls.Add(link);
                }
            }
        }

        private bool ContainsControlId(Control parent, string childId)
        {
            if ((parent == null) || (parent.Controls == null) || (string.IsNullOrEmpty(childId)))
                return false;
            for (int controlIndex = 0; controlIndex < Page.Header.Controls.Count; controlIndex++)
                if (parent.Controls[controlIndex].ID == childId)
                    return true;
            return false;
        }
        #endregion

        //internal static void GetRootPathValues(string id, out string encodedResource, out string virtualRootPath)
        //{
        //    HtmlTextBoxResource resource = HtmlTextBoxHelper.GetGlobalResource(id);
        //    HtmlTextBoxResource.ResourceRoot[] resourceRoots = resource.ResourceRoots;
        //    System.Text.StringBuilder resourcePathBuffer = new System.Text.StringBuilder();
        //    foreach (HtmlTextBoxResource.ResourceRoot resourceRoot in resourceRoots)
        //        resourcePathBuffer.AppendFormat("{0}={1};", resourceRoot.Name, resourceRoot.FilePath);
        //    if (resourcePathBuffer.Length > 0)
        //        resourcePathBuffer.Length--;
        //    //
        //    virtualRootPath = resource.VirtualRootFilePath;
        //    encodedResource = resourcePathBuffer.ToString();
        //}

        //#region IDataFieldBinder

        //void IDataFieldBinder.Load(System.Xml.XPath.XPathNavigator viewerConfiguration, object control, DataState dataState, DataField dataField)
        //{
        //    HtmlTextBoxHelper htmlTextBoxHelper = HtmlTextBoxHelper.Instance;
        //    ResourceId = HtmlTextBoxHelper.ResourceId;
        //    HtmlTextBoxResource resource = HtmlTextBoxHelper.GetGlobalResource(ResourceId);
        //    string resourcePath;
        //    HtmlTextBoxResource.ResourceRoot[] resourceRoots;
        //    if ((resource != null) && ((resourceRoots = resource.ResourceRoots).Length > 0) && (string.IsNullOrEmpty(resourcePath = htmlTextBoxHelper.GetResourcePath(dataField)) == false))
        //    {
        //        string resourceIndex;
        //        resourcePath = KernelText.ParseBoundedPrefix("[]", resourcePath, out resourceIndex);
        //        string resourceRootFilePath = resourceRoots[Kernel.ParseInt32(resourceIndex)].FilePath;
        //        ResourcePath = (resourceRootFilePath + "\\" + resourcePath).Replace("\\", "\\\\");
        //    }
        //}

        //#endregion
    }
}