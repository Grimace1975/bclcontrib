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
namespace System.Web.UI.Integrate
{
    /// <summary>
    /// ShareThisInclude
    /// </summary>
    public class ShareThisInclude
    {
        public ShareThisInclude()
        {
            Type = "website";
        }

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
    }
}