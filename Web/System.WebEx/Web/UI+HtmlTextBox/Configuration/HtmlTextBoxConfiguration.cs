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
using System.Configuration;
namespace System.Web.UI.Configuration
{
    /// <summary>
    /// HtmlTextBoxConfiguration
    /// </summary>
    public class HtmlTextBoxConfiguration : Patterns.Configuration.Configuration
    {
        public HtmlTextBoxConfiguration()
            : base() { }

        [ConfigurationProperty("name", IsRequired = true)]
        public override string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("designModeBodyTagCssClass", DefaultValue = "contentPane")]
        public string DesignModeBodyTagCssClass
        {
            get { return (string)this["designModeBodyTagCssClass"]; }
            set { this["designModeBodyTagCssClass"] = value; }
        }

        [ConfigurationProperty("designModeCssUri", DefaultValue = "/App_/ROOT/PageFrame/HtmlTextEditorStyleSheet.css")]
        public string DesignModeCssUri
        {
            get { return (string)this["designModeCssUri"]; }
            set { this["designModeCssUri"] = value; }
        }

        [ConfigurationProperty("toolbarBreakOn", DefaultValue = "None")]
        public HtmlTextBoxCommands ToolbarBreakOn
        {
            get { return (HtmlTextBoxCommands)this["toolbarBreakOn"]; }
            set { this["toolbarBreakOn"] = value; }
        }

        [ConfigurationProperty("toolbarCommand", DefaultValue = "Format,Align,Insert,Table,Bullet,ElementStyle,Indent")]
        public HtmlTextBoxCommands ToolbarCommands
        {
            get { return (HtmlTextBoxCommands)this["toolbarCommand"]; }
            set { this["toolbarCommand"] = value; }
        }

        [ConfigurationProperty("cssStyles")]
        public HtmlTextBoxCssStyleConfigurationSet CssStyles
        {
            get { return (HtmlTextBoxCssStyleConfigurationSet)this["cssStyles"]; }
        }

        [ConfigurationProperty("elementStyles")]
        public HtmlTextBoxElementStyleConfigurationSet ElementStyles
        {
            get { return (HtmlTextBoxElementStyleConfigurationSet)this["elementStyles"]; }
        }

        [ConfigurationProperty("fonts")]
        public HtmlTextBoxFontConfigurationSet Fonts
        {
            get { return (HtmlTextBoxFontConfigurationSet)this["fonts"]; }
        }

        [ConfigurationProperty("plugins")]
        public HtmlTextBoxPluginConfigurationSet Plugins
        {
            get { return (HtmlTextBoxPluginConfigurationSet)this["plugins"]; }
        }

        [ConfigurationProperty("toolbars")]
        public HtmlTextBoxToolbarConfigurationSet Toolbars
        {
            get { return (HtmlTextBoxToolbarConfigurationSet)this["toolbars"]; }
        }
    }
}