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
        public void AddAttribute(string attribute, string value)
        {
            if (string.IsNullOrEmpty(attribute))
                throw new ArgumentNullException("attribute");
            _writeCount++;
            if (!attribute.StartsWith("style", StringComparison.OrdinalIgnoreCase))
                _textWriter.AddAttribute(attribute, value);
            else
                _textWriter.AddStyleAttribute(attribute.Substring(5), value);
        }
        public void AddAttribute(HtmlAttribute attribute, string value)
        {
            _writeCount++;
            int attribute2 = (int)attribute;
            if (attribute2 < HtmlTextWriterEx.HtmlAttributeSplit)
                _textWriter.AddAttribute((HtmlTextWriterAttribute)attribute2, value);
            else if (attribute2 > HtmlTextWriterEx.HtmlAttributeSplit)
                _textWriter.AddStyleAttribute((HtmlTextWriterStyle)attribute2 - HtmlTextWriterEx.HtmlAttributeSplit - 1, value);
            else
                throw new ArgumentException(string.Format("Local.InvalidHtmlAttribA", attribute.ToString()), "attribute");
        }

        //public void AddAttribute(params string[] args) { AddAttribute(Nattrib.Parse(args), null); }
        public void AddAttribute(Nattrib attrib, string[] throwOnAttributes)
        {
            if (attrib == null)
                throw new ArgumentNullException("attrib");
            //foreach (string key in attrib.KeyEnum)
            //{
            //    string value = attrib[key];
            //    int htmlAttrib;
            //    if (s_htmlAttribEnumInt32Parser.TryGetValue(key, out htmlAttrib) == true)
            //    {
            //        AddAttribute((HtmlAttrib)htmlAttrib, value);
            //        continue;
            //    }
            //    else if (key.Length > 0)
            //    {
            //        m_writeCount++;
            //        if (key.StartsWith("style", System.StringComparison.InvariantCultureIgnoreCase) == false)
            //        {
            //            m_textWriter.AddAttribute(key, value);
            //            continue;
            //        }
            //        m_textWriter.AddStyleAttribute(key.Substring(5), value);
            //        continue;
            //    }
            //    throw new ArgumentException(string.Format(Local.InvalidHtmlAttribA, key), "attrib");
            //}
        }

        public void AddTextWriterAttribute(string name, string value)
        {
            _writeCount++;
            _textWriter.AddAttribute(name, value);
        }
        public void AddTextWriterAttribute(HtmlTextWriterAttribute attribute, string value)
        {
            _writeCount++;
            _textWriter.AddAttribute(attribute, value);
        }

        public void AddTextWriterStyleAttribute(string name, string value)
        {
            _writeCount++;
            _textWriter.AddStyleAttribute(name, value);
        }
        public void AddTextWriterStyleAttribute(HtmlTextWriterStyle attribute, string value)
        {
            _writeCount++;
            _textWriter.AddStyleAttribute(attribute, value);
        }

        public HtmlBuilder BeginHtmlTag(HtmlTag tag, params string[] args) { return BeginHtmlTag(tag, Nattrib.Parse(args)); }
        public HtmlBuilder BeginHtmlTag(HtmlTag tag, Nattrib attrib)
        {
            if ((tag == HtmlTag.Unknown) || (tag >= HtmlTag._FormReference))
                throw new ArgumentException(string.Format("Local.InvalidHtmlTagA", tag.ToString()), "tag");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag((HtmlTextWriterTag)tag);
            return this;
        }
        public HtmlBuilder BeginHtmlTag(string tag, params string[] args) { return BeginHtmlTag(tag, Nattrib.Parse(args)); }
        public HtmlBuilder BeginHtmlTag(string tag, Nattrib attrib)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException("tag");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(tag);
            return this;
        }

        public HtmlBuilder EmptyHtmlTag(HtmlTag tag, params string[] args) { return EmptyHtmlTag(tag, Nattrib.Parse(args)); }
        public HtmlBuilder EmptyHtmlTag(HtmlTag tag, Nattrib attrib)
        {
            if ((tag == HtmlTag.Unknown) || (tag >= HtmlTag._FormReference))
                throw new ArgumentException(string.Format("Local.InvalidHtmlTagA", tag.ToString()), "tag");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag((HtmlTextWriterTag)tag);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder EmptyHtmlTag(string tag, params string[] args) { return EmptyHtmlTag(tag, Nattrib.Parse(args)); }
        public HtmlBuilder EmptyHtmlTag(string tag, Nattrib attrib)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException("tag");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(tag);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder EndHtmlTag()
        {
            _writeCount++;
            _textWriter.RenderEndTag();
            return this;
        }
    }
}