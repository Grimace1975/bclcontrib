using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using Moxiecode.TinyMCE;
namespace System.Web.UI.Integrate
{
    /// <summary>
    ///  A webcontrol for the TinyMCE editor
    /// </summary>
    public partial class TinyMceTextBox
    {
        #region private
        private NameValueCollection settings;
        ///+ CHANGED
        //private static readonly object TextChangedEvent = new object();
        //private int rows = 10;
        //private int cols = 70;
        ///+ CHANGED2
        private bool isRendered;
        private bool gzipEnabled, merged;
        private string installPath, mode;
        #endregion

        /// <summary>
        ///  Constructor
        /// </summary>
        ///+ CHANGED
        public void ctr_TinyMceTextBox()
        {
            ConfigSection configSection = (ConfigSection)System.Web.HttpContext.Current.GetSection("TinyMCE");

            this.settings = new NameValueCollection();

            if (configSection != null)
            {
                this.installPath = configSection.InstallPath;
                this.mode = configSection.Mode;
                this.gzipEnabled = configSection.GzipEnabled;

                // Copy items into local settings collection
                foreach (string key in configSection.GlobalSettings.Keys)
                    this.settings[key] = configSection.GlobalSettings[key];
            }
        }

        ///+ CHANGED
        ///// <summary>
        ///// The contents of the editor, can be HTML format or something else depending on plugins.
        ///// </summary>
        //public string Value
        //{
        //    get { return (string)ViewState["value"]; }
        //    set { ViewState["value"] = value; }
        //}

        ///+ CHANGED
        ///// <summary>
        ///// The number of rows in the textarea that gets converted to the editor.
        ///// This affects the size of the editor. Default is 10
        ///// </summary>
        //public int Rows
        //{
        //    get { return this.rows; }
        //    set { this.rows = value; }
        //}

        ///+ CHANGED
        ///// <summary>
        ///// The number of columns in the textarea that gets converted to the editor.
        ///// This affects the size of the editor. Default is 40.
        ///// </summary>
        //public int Cols
        //{
        //    get { return this.cols; }
        //    set { this.cols = value; }
        //}

        /// <summary>Installation path where the TinyMCE script directory is.</summary>
        public string InstallPath
        {
            get
            {
                if (installPath == null)
                    installPath = this.Settings["InstallPath"];

                return installPath;
            }
            set { installPath = value; }
        }

        //+ CHANGED2
        /// <summary>
        ///  State if the control has been rendered or not.
        /// </summary>
        public bool IsRendered
        {
            get { return this.isRendered; }
        }

        /// <summary>
        ///  Changes the id to match the new client id.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnLoad(EventArgs args)
        {
            this.settings["elements"] = this.ClientID;
        }

        /// <summary>
        ///  Collection of init settings for the TinyMCE instance.
        /// </summary>
        public NameValueCollection Settings
        {
            get
            {
                if (!this.merged)
                {
                    // Override local settings with attributes
                    foreach (string key in this.Attributes.Keys)
                        this.settings[key] = this.Attributes[key];

                    if (this.settings["plugins"] != null && this.settings["spellchecker_rpc_url"] == null)
                    {
                        if (Array.IndexOf(((string)this.settings["plugins"]).Split(','), "spellchecker") != -1)
                            ///+ CHANGED
                            this.settings["spellchecker_rpc_url"] = RebaseUrl("/TinyMCE.ashx?module=SpellChecker");
                    }

                    this.settings["elements"] = this.ClientID;
                    this.settings["mode"] = "exact";

                    this.merged = true;
                }

                return this.settings;
            }
        }

        ///+ CHANGED2
        /// <summary>
        ///  Returns true or false if any TinyMCE instance has been rendered or not.
        /// </summary>
        /// <param name="control">Control to start the search at.</param>
        /// <returns>True or false if any TinyMCE instance has been rendered or not</returns>
        public bool HasRenderedTextArea(Control control)
        {
            if (control is TinyMceTextBox && ((TinyMceTextBox)control).IsRendered)
                return true;

            foreach (Control ctrl in control.Controls)
            {
                if (this.HasRenderedTextArea(ctrl))
                    return true;
            }

            return false;
        }

        ///+ CHANGED
        ///// <summary>
        /////  Renders the TinyMCE textarea control HTML to page.
        ///// </summary>
        ///// <param name="outWriter">The writer to render them HTML to.</param>
        //protected override void Render(HtmlTextWriter outWriter)
        //{
        //    bool first = true;

        //    // Render HTML for TinyMCE instance
        //    if (!this.HasRenderedTextArea(this.Page))
        //    {
        //        outWriter.WriteBeginTag("script");
        //        outWriter.WriteAttribute("type", "text/javascript");
        //        outWriter.WriteAttribute("src", this.ScriptURI);
        //        outWriter.Write(HtmlTextWriter.TagRightChar);
        //        outWriter.WriteEndTag("script");
        //        this.isRendered = true;
        //    }

        //    // Write script tag start
        //    outWriter.WriteBeginTag("script");
        //    outWriter.WriteAttribute("type", "text/javascript");
        //    outWriter.Write(HtmlTextWriter.TagRightChar);
        //    outWriter.Write("tinyMCE.init({\n");

        //    // Write options
        //    foreach (string key in this.Settings.Keys)
        //    {
        //        string val = this.Settings[key];

        //        if (!first)
        //            outWriter.Write(",\n");
        //        else
        //            first = false;

        //        // Is boolean state or string
        //        if (val == "true" || val == "false")
        //            outWriter.Write(key + ":" + this.Settings[key]);
        //        else
        //            outWriter.Write(key + ":'" + this.Settings[key] + "'");
        //    }

        //    // Write script tag end
        //    outWriter.Write("\n});\n");
        //    outWriter.WriteEndTag("script");

        //    // Write text area
        //    outWriter.AddAttribute("id", this.ClientID);
        //    outWriter.AddAttribute("name", this.UniqueID);

        //    if (this.Rows > 0)
        //        outWriter.AddAttribute("rows", "" + this.Rows);

        //    if (this.Cols > 0)
        //        outWriter.AddAttribute("cols", "" + this.Cols);

        //    if (this.CssClass.Length > 0)
        //        outWriter.AddAttribute("class", this.CssClass);

        //    if (this.Width.Value > 0)
        //        outWriter.AddStyleAttribute("width", this.Width.ToString());

        //    if (this.Height.Value > 0)
        //        outWriter.AddStyleAttribute("height", this.Height.ToString());

        //    outWriter.RenderBeginTag("textarea");
        //    outWriter.Write(this.Context.Server.HtmlEncode(this.Value));
        //    outWriter.WriteEndTag("textarea");
        //}

        ///+ CHANGED
        ///// <summary>
        /////  Raises an event when the text in the editor changes.
        ///// </summary>
        //public event EventHandler TextChanged
        //{
        //    add { Events.AddHandler(TextChangedEvent, value); }
        //    remove { Events.RemoveHandler(TextChangedEvent, value); }
        //}

        ///+ CHANGED
        ///// <summary>
        /////  Event for text change.
        ///// </summary>
        ///// <param name="e"></param>
        //protected virtual void OnTextChanged(EventArgs e)
        //{
        //    if (Events != null)
        //    {
        //        EventHandler eventHandler = (EventHandler)Events[TextChangedEvent];

        //        if (eventHandler != null)
        //            eventHandler(this, e);
        //    }
        //}

        #region internal

        string ScriptURI
        {
            get
            {
                string suffix = "", outURI;

                if (this.InstallPath == null)
                    throw new Exception("Required installPath setting is missing, add it to your web.config. You can also add it directly to your tinymce:TextArea element using InstallPath but the web.config method is recommended since it allows you to switch over to gzip compression.");

                if (this.mode != null)
                    suffix = "_" + this.mode;

                outURI = this.InstallPath + "/tiny_mce" + suffix + ".js";
                if (!File.Exists(this.Context.Server.MapPath(outURI)))
                    throw new Exception("Could not locate TinyMCE by URI:" + outURI + ", Physical path:" + this.Context.Server.MapPath(outURI) + ". Make sure that you configured the installPath to a valid location in your web.config. This path should be an relative or site absolute URI to where TinyMCE is located.");

                // Collect themes, languages and plugins and build gzip URI
                if (this.gzipEnabled)
                {
                    ArrayList themes = new ArrayList(), plugins = new ArrayList(), languages = new ArrayList();

                    foreach (TinyMceTextBox area in this.FindAllTinyMCEAreas(this.Page, new ArrayList()))
                    {
                        // Add themes
                        if (area.Settings["theme"] == null)
                            area.Settings["theme"] = "simple";

                        foreach (string theme in area.Settings["theme"].Split(','))
                        {
                            string themeVal = theme.Trim();

                            if (themes.IndexOf(themeVal) == -1)
                                themes.Add(themeVal);
                        }

                        // Add plugins
                        if (area.Settings["plugins"] != null)
                        {
                            foreach (string plugin in area.Settings["plugins"].Split(','))
                            {
                                string pluginVal = plugin.Trim();

                                if (plugins.IndexOf(pluginVal) == -1)
                                    plugins.Add(pluginVal);
                            }
                        }

                        // Add language
                        if (area.Settings["language"] == null)
                            area.Settings["language"] = "en";

                        if (languages.IndexOf(area.Settings["language"]) == -1)
                            languages.Add(area.Settings["language"]);

                        // Skip loading of themes and plugins
                        area.Settings["theme"] = "-" + area.Settings["theme"];

                        if (area.Settings["plugins"] != null)
                            area.Settings["plugins"] = "-" + String.Join(",-", area.Settings["plugins"].Split(','));
                    }

                    // Build output URI
                    ///+ CHANGED
                    outURI = RebaseUrl("/TinyMCE.ashx?module=GzipModule");

                    if (themes.Count > 0)
                        outURI += "&themes=" + String.Join(",", ((string[])themes.ToArray(typeof(string))));

                    if (plugins.Count > 0)
                        outURI += "&plugins=" + String.Join(",", ((string[])plugins.ToArray(typeof(string))));

                    if (languages.Count > 0)
                        outURI += "&languages=" + String.Join(",", ((string[])languages.ToArray(typeof(string))));
                }

                return outURI;
            }
        }

        /// <summary>
        ///  Loops though the page to find all instances of the TinyMCE control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public ArrayList FindAllTinyMCEAreas(Control control, ArrayList items)
        {
            if (control is TinyMceTextBox)
                items.Add(control);

            foreach (Control ctrl in control.Controls)
                this.FindAllTinyMCEAreas(ctrl, items);

            return items;
        }

        ///+ CHANGED
        ///// <summary>
        /////  Called when a postback occurs on the page the control is placed at.
        ///// </summary>
        ///// <param name="postDataKey">The key of the editor data</param>
        ///// <param name="postCollection">All the posted data</param>
        ///// <returns></returns>
        //bool IPostBackDataHandler.LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        //{
        //    string newContent = postCollection[postDataKey];

        //    if (newContent != this.Value)
        //    {
        //        this.Value = newContent;
        //        return true;
        //    }

        //    return false;
        //}

        ///// <summary>
        /////  Raises an event when postback occurs.
        ///// </summary>
        //void IPostBackDataHandler.RaisePostDataChangedEvent()
        //{
        //    OnTextChanged(EventArgs.Empty);
        //}

        #endregion
    }
}
