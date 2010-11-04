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
namespace System.Web.UI.ClientShapes
{
    /// <summary>
    /// SwfUploadShapeSwfObjectPlugin
    /// </summary>
    public class SwfUploadShapeSwfObjectPlugin : IClientScriptItemOption
    {
        public SwfUploadShapeSwfObjectPlugin()
        {
            ClientScriptRegistrarSwfObjectShape.AssertRegistered();
        }

        NewAttrib IClientScriptItemOption.MakeOption()
        {
            var options = new NewAttrib();
            if (MinimumFlashVersion != null)
                options["minimum_flash_version"] = ClientScript.EncodeText(MinimumFlashVersion);
            if (SwfUploadPreLoadHandler != null)
                options["swfupload_pre_load_handler"] = ClientScript.EncodeExpression(SwfUploadPreLoadHandler);
            if (SwfUploadLoadFailedHandler != null)
                options["swfupload_load_failed_handler"] = ClientScript.EncodeExpression(SwfUploadLoadFailedHandler);
            return options;
        }

        /// <summary>
        /// Gets or sets the minimum flash version.
        /// </summary>
        /// <value>The minimum flash version.</value>
        public string MinimumFlashVersion { get; set; }

        /// <summary>
        /// Gets or sets the SWF upload pre load handler.
        /// </summary>
        /// <value>The SWF upload pre load handler.</value>
        public string SwfUploadPreLoadHandler { get; set; }

        /// <summary>
        /// Gets or sets the SWF upload load failed handler.
        /// </summary>
        /// <value>The SWF upload load failed handler.</value>
        public string SwfUploadLoadFailedHandler { get; set; }
    }
}