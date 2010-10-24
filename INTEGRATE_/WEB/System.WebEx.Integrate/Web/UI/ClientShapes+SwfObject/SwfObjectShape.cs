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
// http://code.google.com/p/swfupload/
using System;
using System.Text;
using System.Collections;
namespace System.Web.UI.ClientShapes
{
    /// <summary>
    /// SwfObjectShape
    /// </summary>
    public class SwfObjectShape : ClientScriptItemBase
    {
        public SwfObjectShape()
        {
            Registrar = ClientScriptRegistrarSwfObjectShape.Current;
            if (Registrar == null)
                throw new InvalidOperationException("ClientScriptRegistrarSwfUploadShape.Current must be set first");
            FlashVersionId = "7.0.0";
            ExpressInstallSwfUrl = Registrar.SwfObjectExpressInstallFlashUrl;
        }

        protected ClientScriptRegistrarSwfObjectShape Registrar { get; set; }

        public override void Render(StringBuilder b)
        {
            if (string.IsNullOrEmpty(Url))
                throw new ArgumentNullException("Url");
            if (string.IsNullOrEmpty(ElementId))
                throw new ArgumentNullException("ElementId");
            if (string.IsNullOrEmpty(Width))
                throw new ArgumentNullException("Width");
            if (string.IsNullOrEmpty(Height))
                throw new ArgumentNullException("Height");
            if (string.IsNullOrEmpty(FlashVersionId))
                throw new ArgumentNullException("FlashVersionId");
            if ((EnumerableEx.IsNullOrEmpty(Variables)) && (EnumerableEx.IsNullOrEmpty(Parameters)) && (EnumerableEx.IsNullOrEmpty(Attributes)))
                b.AppendLine(string.Format("swfobject.embedSWF({0},{1},{2},{3},{4});",
                    ClientScript.EncodeText(Url), ClientScript.EncodeText(ElementId), ClientScript.EncodeText(Width), ClientScript.EncodeText(Height), ClientScript.EncodeText(FlashVersionId)));
            else
            {
                var variables = ClientScript.EncodeDictionary(Variables);
                var parameters = ClientScript.EncodeDictionary(Parameters);
                var attributes = ClientScript.EncodeDictionary(Attributes);
                b.AppendLine(string.Format("swfobject.embedSWF({0},{1},{2},{3},{4},{5},{6},{7},{8});",
                    ClientScript.EncodeText(Url), ClientScript.EncodeText(ElementId), ClientScript.EncodeText(Width), ClientScript.EncodeText(Height), ClientScript.EncodeText(FlashVersionId),
                    ((!UseExpressInstall) || (string.IsNullOrEmpty(ExpressInstallSwfUrl)) ? "false" : ClientScript.EncodeText(ExpressInstallSwfUrl)),
                    (!EnumerableEx.IsNullOrEmpty(Variables) ? variables : ClientScript.EmptyObject),
                    (!EnumerableEx.IsNullOrEmpty(Parameters) ? parameters : ClientScript.EmptyObject),
                    (!EnumerableEx.IsNullOrEmpty(Attributes) ? attributes : ClientScript.EmptyObject)));
            }
        }

        #region Options

        public string Url { get; set; }
        public string ElementId { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string FlashVersionId { get; set; }
        public string ExpressInstallSwfUrl { get; set; }
        public bool UseExpressInstall { get; set; }
        public NewAttrib Variables { get; set; }
        public NewAttrib Parameters { get; set; }
        public NewAttrib Attributes { get; set; }

        #endregion
    }
}