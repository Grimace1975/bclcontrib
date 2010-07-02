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
using System.Text;
namespace System.Patterns.Schema
{
    /// <summary>
    /// HtmlSchema
    /// </summary>
    public class HtmlSchema : HtmlSchemaBase
    {
        public const uint BrFlag = 0x1;
        private bool _isBound;

        public HtmlSchema()
        {
            Decoders = new Dictionary<Type, HtmlDecoderBase>();
        }
        public HtmlSchema(Configuration.HtmlSchemaConfiguration c)
            : this() { }

        public override string DecodeHtml(string html, uint defaultDecodeFlags)
        {
            var decodeFlagsStack = new Stack<uint>();
            var b = new StringBuilder();
            int index;
            int startIndex = 0;
            int htmlLength = html.Length;
            uint decodeFlags = defaultDecodeFlags;
            while ((index = html.IndexOf("[format=\"", startIndex, StringComparison.OrdinalIgnoreCase)) > -1)
            {
                string html2 = html.Substring(startIndex, index - startIndex);
                b.Append(decodeFlags != 0 ? DecodeHtmlBlock(html2, decodeFlags) : html2);
                startIndex = index + 9;
                if ((index = html.IndexOf("\"]", startIndex)) > -1)
                {
                    string commandId = html.Substring(startIndex, index - startIndex);
                    if (string.Compare(commandId, "Full", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        decodeFlagsStack.Push(decodeFlags);
                        decodeFlags = uint.MaxValue;
                    }
                    else if (string.Compare(commandId, "None", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        decodeFlagsStack.Push(decodeFlags);
                        decodeFlags = 0;
                    }
                    else if (string.Compare(commandId, "Restore", StringComparison.OrdinalIgnoreCase) == 0)
                        decodeFlags = (decodeFlagsStack.Count > 0 ? decodeFlagsStack.Pop() : defaultDecodeFlags);
                    else
                    {
                        decodeFlagsStack.Push(decodeFlags);
                        decodeFlags = ParseDecodeFlag(commandId);
                    }
                    startIndex = index + 2;
                }
            }
            if (startIndex < htmlLength)
            {
                string html2 = html.Substring(startIndex);
                b.Append(decodeFlags != 0 ? DecodeHtmlBlock(html2, decodeFlags) : html2);
            }
            return b.ToString();
        }

        private string DecodeHtmlBlock(string html, uint decodeFlags)
        {
            var b = new StringBuilder();
            foreach (var decoder in Decoders.Values)
            {
                if ((decodeFlags & decoder.Flag) != decoder.Flag) { continue; }
                decoder.Decode(b, decoder.Formatter, html);
                html = b.ToString();
                b.Length = 0;
            }
            return ((decodeFlags & BrFlag) != BrFlag ? html : html.Replace("\r\n", "<br />"));
        }

        public override Dictionary<Type, HtmlDecoderBase> Decoders { get; protected set; }

        #region FluentConfig
        public override HtmlSchemaBase AddDecoder<T>(T decoder)
        {
            if (_isBound)
                throw new InvalidOperationException("_isBound");
            Decoders.Add(typeof(T), decoder);
            return this;
        }

        public override HtmlSchemaBase MakeReadOnly()
        {
            _isBound = true;
            foreach (var decoder in Decoders.Values)
                decoder.IsBound = true;
            return this;
        }
        #endregion
    }
}
