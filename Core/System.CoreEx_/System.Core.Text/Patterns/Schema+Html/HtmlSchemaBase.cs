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
using System.Globalization;
using System.Collections.Generic;
namespace System.Patterns.Schema
{
    /// <summary>
    /// HtmlSchemaBase
    /// </summary>
    public abstract class HtmlSchemaBase
    {
        [Flags]
        public enum DecodeFlags
        {
            CrLfToBr = 0x1,
        }

        public abstract Dictionary<Type, HtmlDecoderBase> Decoders { get; protected set; }
        public virtual string DecodeHtml(string html, Nattrib attrib) { return DecodeHtml(html, (attrib == null ? (uint)DecodeFlags.CrLfToBr : ParseDecodeFlag(attrib.Value<string>("format", "Full")))); }
        public abstract string DecodeHtml(string html, uint defaultDecodeFlags);

        public static uint ParseDecodeFlag(string id)
        {
            throw new NotImplementedException();
        }

        #region FluentConfig
        public abstract HtmlSchemaBase AddDecoder<T>(T decoder)
             where T : HtmlDecoderBase;
        public abstract HtmlSchemaBase MakeReadOnly();
        #endregion
    }
}
