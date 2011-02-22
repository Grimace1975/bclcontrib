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
    /// SwfUploadShapeQueuePlugin
    /// </summary>
    public class SwfUploadShapeQueuePlugin : IClientScriptItemOption
    {
        NewAttrib IClientScriptItemOption.MakeOption()
        {
            var options = new NewAttrib();
            if (UploadCompleteHandler != null)
                options["upload_complete_handler"] = ClientScript.EncodeExpression(UploadCompleteHandler);
            if (UploadStartHandler != null)
                options["upload_start_handler"] = ClientScript.EncodeExpression(UploadStartHandler);
            if (QueueCompleteHandler != null)
                options["queue_complete_handler"] = ClientScript.EncodeExpression(QueueCompleteHandler);
            return options;
        }

        /// <summary>
        /// Gets or sets the upload complete handler.
        /// </summary>
        /// <value>The upload complete handler.</value>
        public string UploadCompleteHandler { get; set; }

        /// <summary>
        /// Gets or sets the upload start handler.
        /// </summary>
        /// <value>The upload start handler.</value>
        public string UploadStartHandler { get; set; }

        /// <summary>
        /// Gets or sets the queue complete handler.
        /// </summary>
        /// <value>The queue complete handler.</value>
        public string QueueCompleteHandler { get; set; }
    }
}