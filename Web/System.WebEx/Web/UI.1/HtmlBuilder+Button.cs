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
namespace System.Web.UI
{
    public partial class HtmlBuilder
    {
        public HtmlBuilder RenderButton(string text, string commandEvent, params string[] args) { return RenderButton(text, commandEvent, Nattrib.Parse(args)); }
        public HtmlBuilder RenderButton(string text, string commandEvent, Nattrib attrib)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            _writeCount++;
            if (attrib != null)
                AddHtmlAttrib(attrib, new[] { "onclick" });
            if (!string.IsNullOrEmpty(commandEvent))
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Onclick, commandEvent);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Button);
            _textWriter.Write(text);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder RenderInputButton(string text, string commandEvent, params string[] args) { return RenderInputButton(text, commandEvent, Nattrib.Parse(args)); }
        public HtmlBuilder RenderInputButton(string text, string commandEvent, Nattrib attrib)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            _writeCount++;
            if (attrib != null)
                AddHtmlAttrib(attrib, new[] { "onclick", "value" });
            if (!string.IsNullOrEmpty(commandEvent))
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Onclick, commandEvent);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Value, text);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder RenderImageButton(string url, string alt, string commandEvent, params string[] args) { return RenderImageButton(url, alt, commandEvent, Nattrib.Parse(args)); }
        public HtmlBuilder RenderImageButton(string url, string alt, string commandEvent, Nattrib attrib)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            _writeCount++;
            string onClickEvent = null;
            if (attrib != null)
            {
                Nattrib imageAttrib = attrib.Slice<Nattrib>("image");
                if (attrib.Count > 0)
                {
                    onClickEvent = attrib.Slice<string>("onclick");
                    if (attrib.Count > 0)
                        AddHtmlAttrib(attrib, null);
                }
                if (imageAttrib != null)
                    AddHtmlAttrib(imageAttrib, null);
            }
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Alt, (alt ?? string.Empty));
            if (!string.IsNullOrEmpty(alt))
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Title, alt);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Src, url);
            if ((!string.IsNullOrEmpty(commandEvent)) || (!string.IsNullOrEmpty(onClickEvent)))
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Onclick, StringEx.Axb(onClickEvent, ";", commandEvent) + ";return(false);");
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Type, "image");
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder RenderImageLink(string url, string alt, string command, params string[] args) { return RenderImageLink(url, alt, command, Nattrib.Parse(args)); }
        public HtmlBuilder RenderImageLink(string url, string alt, string command, Nattrib attrib)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            _writeCount++;
            Nattrib imageAttrib;
            if (attrib != null)
            {
                imageAttrib = attrib.Slice<Nattrib>("image");
                if (attrib.Count > 0)
                    AddHtmlAttrib(attrib, null);
            }
            else
                imageAttrib = null;
            if (!string.IsNullOrEmpty(command))
                _textWriter.AddAttributeIfUndefined((true ? HtmlTextWriterAttribute.Onclick : HtmlTextWriterAttribute.Href), command);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.A);
            if (imageAttrib != null)
                AddHtmlAttrib(imageAttrib, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Src, url);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Alt, (alt ?? string.Empty));
            if (!string.IsNullOrEmpty(alt))
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Title, alt);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Img);
            _textWriter.RenderEndTag();
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder RenderLink(string text, string command, params string[] args) { return RenderLink(text, command, Nattrib.Parse(args)); }
        public HtmlBuilder RenderLink(string text, string command, Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddHtmlAttrib(attrib, null);
            if (!string.IsNullOrEmpty(command))
                _textWriter.AddAttributeIfUndefined((true ? HtmlTextWriterAttribute.Onclick : HtmlTextWriterAttribute.Href), command);
            if (!string.IsNullOrEmpty(text))
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Title, text);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.A);
            _textWriter.Write(text);
            _textWriter.RenderEndTag();
            return this;
        }
    }
}