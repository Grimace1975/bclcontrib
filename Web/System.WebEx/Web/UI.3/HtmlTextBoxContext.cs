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
using System.Web.UI.Configuration;
namespace System.Web.UI
{
    /// <summary>
    /// IHtmlTextBoxContext
    /// </summary>
    public interface IHtmlTextBoxContext
    {
        Dictionary<string, string> AnchorCssStyles { get; }
        Dictionary<string, string> BodyCssStyles { get; }
        bool InDebugMode { get; set; }
        string DesignModeBodyTagCssClass { get; set; }
        string DesignModeCssUri { get; set; }
        Dictionary<string, string> ElementStyles { get; }
        string ElementCssUri { get; set; }
        Dictionary<string, string> Fonts { get; }
        Dictionary<string, string> ImageCssStyles { get; }
        string ResourceFolder { get; set; }
        Dictionary<string, string> Plugins { get; }
        Dictionary<string, string> TableCssStyles { get; }
        HtmlTextBoxCommands ToolbarBreakOn { get; set; }
        HtmlTextBoxCommands ToolbarCommands { get; set; }
    }

    /// <summary>
    /// HtmlTextBoxContext
    /// </summary>
    public class HtmlTextBoxContext : IHtmlTextBoxContext, ICloneable
    {
        #region Primitives
        public class Primitives
        {
            public static readonly string DesignModeCssUri = "/App_/ROOT/PageFrame/HtmlTextEditorStyleSheet.css";
            public static readonly string DesignModeBodyTagCssClass = "contentPane";
            public static readonly Dictionary<string, string> Fonts = new Dictionary<string, string>();
            public static readonly Dictionary<string, string> ElementStyles = new Dictionary<string, string>();
            public static readonly Dictionary<string, string> BodyCssStyles = new Dictionary<string, string>();
            public static readonly Dictionary<string, string> AnchorCssStyles = new Dictionary<string, string>();
            public static readonly Dictionary<string, string> TableCssStyles = new Dictionary<string, string>();
            public static readonly Dictionary<string, string> ImageCssStyles = new Dictionary<string, string>();
            public static readonly Dictionary<string, string> Plugins = new Dictionary<string, string>();
            public static readonly HtmlTextBoxCommands ToolbarCommands = HtmlTextBoxCommands.Format | HtmlTextBoxCommands.Align | HtmlTextBoxCommands.Insert | HtmlTextBoxCommands.Table | HtmlTextBoxCommands.Bullet | HtmlTextBoxCommands.ElementStyle | HtmlTextBoxCommands.Indent;
            public static readonly HtmlTextBoxCommands ToolbarBreakOn = HtmlTextBoxCommands.None;
            public static readonly HtmlTextBoxContext Context = new HtmlTextBoxContext
            {
                DesignModeCssUri = DesignModeCssUri,
                DesignModeBodyTagCssClass = DesignModeBodyTagCssClass,
                Fonts = Fonts,
                ToolbarCommands = ToolbarCommands,
                ToolbarBreakOn = ToolbarBreakOn,
                ElementStyles = ElementStyles,
                BodyCssStyles = BodyCssStyles,
                AnchorCssStyles = AnchorCssStyles,
                TableCssStyles = TableCssStyles,
                ImageCssStyles = ImageCssStyles,
                Plugins = Plugins,
            };

            static Primitives()
            {
                Fonts.Add("Times New Roman", "times new roman,times,serif");
                Fonts.Add("Trebuchet MS", "trebuchet ms,geneva, sans-serif");
                Fonts.Add("Arial", "arial, sans-serif");
                Fonts.Add("Verdana", "verdana,geneva, sans-serif");
                //
                ElementStyles.Add("Normal", "p");
                ElementStyles.Add("Heading 1", "h1");
                ElementStyles.Add("Heading 2", "h2");
                ElementStyles.Add("Heading 3", "h3");
                ElementStyles.Add("Heading 4", "h4");
                //
                BodyCssStyles.Add("Class 1", "class1");
                BodyCssStyles.Add("Class 2", "class2");
                BodyCssStyles.Add("Class 3", "class3");
                BodyCssStyles.Add("Class 4", "class4");
            }
        }
        #endregion

        public HtmlTextBoxContext() { }

        public Dictionary<string, string> AnchorCssStyles { get; private set; }
        public Dictionary<string, string> BodyCssStyles { get; private set; }
        public bool InDebugMode { get; set; }
        public string DesignModeBodyTagCssClass { get; set; }
        public string DesignModeCssUri { get; set; }
        public Dictionary<string, string> ElementStyles { get; private set; }
        public string ElementCssUri { get; set; }
        public Dictionary<string, string> Fonts { get; private set; }
        public Dictionary<string, string> ImageCssStyles { get; private set; }
        public string ResourceFolder { get; set; }
        public Dictionary<string, string> Plugins { get; private set; }
        public Dictionary<string, string> TableCssStyles { get; private set; }
        public HtmlTextBoxCommands ToolbarBreakOn { get; set; }
        public HtmlTextBoxCommands ToolbarCommands { get; set; }

        public static HtmlTextBoxContext Create(HtmlTextBoxConfigurationSet configurationSet, string id, string toolbarId, string resourceFolder)
        {
            if (string.IsNullOrEmpty(resourceFolder))
                resourceFolder = "/App_/Resource_/TinyMce";
            HtmlTextBoxContext context;
            HtmlTextBoxConfiguration configuration;
            if ((configurationSet == null) || (!configurationSet.TryGetValue(id, out configuration)))
            {
                context = (HtmlTextBoxContext)Primitives.Context.Clone();
                context.ResourceFolder = resourceFolder;
                return context;
            }
            context = new HtmlTextBoxContext { ResourceFolder = resourceFolder };
            context.DesignModeCssUri = configuration.DesignModeCssUri;
            context.DesignModeBodyTagCssClass = configuration.DesignModeBodyTagCssClass;
            // fonts
            if (configuration.Fonts.Count > 0)
                context.Fonts = configuration.Fonts
                    .Cast<HtmlTextBoxFontConfiguration>()
                    .ToDictionary(k => k.Name, e => e.Value);
            else
                context.Fonts = new Dictionary<string, string>(Primitives.Fonts);
            // toolbar
            if (!string.IsNullOrEmpty(toolbarId))
            {
                HtmlTextBoxToolbarConfiguration toolbarConfiguration;
                if (!configuration.Toolbars.TryGetValue(toolbarId, out toolbarConfiguration))
                {
                    context.ToolbarCommands = toolbarConfiguration.Commands;
                    context.ToolbarBreakOn = toolbarConfiguration.BreakOn;
                }
                else
                    throw new ArgumentException(string.Format("Local.UndefinedHtmlTextBoxToolbar", toolbarId), "toolbarId");
            }
            else
            {
                context.ToolbarCommands = configuration.ToolbarCommands;
                context.ToolbarBreakOn = configuration.ToolbarBreakOn;
            }
            // elementStyles
            context.ElementCssUri = ((HtmlTextBoxElementStyleConfigurationSet)configuration.ElementStyles).CssUri;
            if (configuration.ElementStyles.Count > 0)
                context.ElementStyles = configuration.ElementStyles
                    .Cast<HtmlTextBoxElementStyleConfiguration>()
                    .ToDictionary(k => k.Name, e => (string.IsNullOrEmpty(e.CssClass) ? e.Value : string.Format("{0};{1}", e.Value, e.CssClass)));
            else
                context.ElementStyles = new Dictionary<string, string>(Primitives.ElementStyles);
            // cssStyle
            var bodyCssStyles = context.BodyCssStyles = new Dictionary<string, string>();
            var anchorCssStyles = context.AnchorCssStyles = new Dictionary<string, string>();
            var tableCssStyles = context.TableCssStyles = new Dictionary<string, string>();
            var imageCssStyles = context.ImageCssStyles = new Dictionary<string, string>();
            if (configuration.CssStyles.Count > 0)
                configuration.CssStyles
                    .Cast<HtmlTextBoxCssStyleConfiguration>()
                    .SelectMany(c => c.Element.ToLowerInvariant().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries), (e, k) => new { Element = k, Name = e.Name, Value = e.Value })
                    .ForEachSlim(e =>
                    {
                        switch (e.Element)
                        {
                            case "body":
                                bodyCssStyles[e.Name] = e.Value;
                                break;
                            case "anchor":
                                anchorCssStyles[e.Name] = e.Value;
                                break;
                            case "table":
                                tableCssStyles[e.Name] = e.Value;
                                break;
                            case "image":
                                imageCssStyles[e.Name] = e.Value;
                                break;
                        }
                    });
            else
                bodyCssStyles.Insert(Primitives.BodyCssStyles);
            // plugins
            if (configuration.Plugins.Count > 0)
                context.Plugins = configuration.Plugins
                    .Cast<HtmlTextBoxPluginConfiguration>()
                    .ToDictionary(k => k.Name, e => e.Path);
            else
                context.Plugins = new Dictionary<string, string>(Primitives.Plugins);
            return context;
        }

        public object Clone()
        {
            var context = (HtmlTextBoxContext)MemberwiseClone();
            context.Fonts = new Dictionary<string, string>(context.Fonts);
            context.ElementStyles = new Dictionary<string, string>(context.ElementStyles);
            context.BodyCssStyles = new Dictionary<string, string>(context.BodyCssStyles);
            context.AnchorCssStyles = new Dictionary<string, string>(context.AnchorCssStyles);
            context.TableCssStyles = new Dictionary<string, string>(context.TableCssStyles);
            context.ImageCssStyles = new Dictionary<string, string>(context.ImageCssStyles);
            context.Plugins = new Dictionary<string, string>(context.Plugins);
            return context;
        }
    }
}
