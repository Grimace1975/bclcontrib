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
using System.Collections.Generic;
using System.IO;
using System.Patterns.Form;
namespace System.Web.UI
{
    /// <summary>
    /// HtmlBuilder
    /// </summary>
    public partial class HtmlBuilder : Patterns.Disposeable
    {
        private HtmlTextWriterEx _textWriter;
        private int _writeCount = 0;

        public HtmlBuilder(HtmlTextWriterEx w)
        {
            _textWriter = w;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _elementStack.Clear();
        }

        public void Close()
        {
            _textWriter.Close();
        }

        public HtmlTextWriter InnerWriter
        {
            get
            {
                _writeCount++;
                return _textWriter;
            }
        }

        public void RenderControl(object control)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            _writeCount++;
            ((Control)control).RenderControl(_textWriter);
        }
        public void RenderControl(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            _writeCount++;
            control.RenderControl(_textWriter);
        }

        public override string ToString()
        {
            _textWriter.Flush();
            _writeCount++;
            var innerStringWriter = (_textWriter.InnerWriter as StringWriter);
            return (innerStringWriter != null ? innerStringWriter.ToString() : null);
        }

        #region Class-Factory
        public virtual HtmlBuilderDivTag CreateHtmlBuilderDivTag(HtmlBuilder b, Nattrib attrib)
        {
            return new HtmlBuilderDivTag(b, attrib);
        }
        public virtual HtmlBuilderFormTag CreateHtmlBuilderFormTag(HtmlBuilder b, IFormContext formContext, Nattrib attrib)
        {
            return new HtmlBuilderFormTag(b, formContext, attrib);
        }
        public virtual HtmlBuilderTableTag CreateHtmlBuilderTableTag(HtmlBuilder b, Nattrib attrib)
        {
            return new HtmlBuilderTableTag(b, attrib.Get<HtmlBuilderTableTag.TableAttrib>());
        }
        #endregion
    }
}