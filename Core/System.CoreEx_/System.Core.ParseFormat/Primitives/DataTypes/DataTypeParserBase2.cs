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
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// DataTypeParserBase
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TParseAttrib">The type of the parse attrib.</typeparam>
    public abstract class DataTypeParserBase<TValue, TParseAttrib> : DataTypeParserBase
    {
        private TryFunc<string, TParseAttrib, TValue> _tryParse;

        public DataTypeParserBase(TryFunc<string, TParseAttrib, TValue> tryParse)
            : this(tryParse, string.Empty, string.Empty) { }
        public DataTypeParserBase(TryFunc<string, TParseAttrib, TValue> tryParse, object parseDefaultValue, string parseTextDefaultValue)
            : base(parseDefaultValue, parseTextDefaultValue)
        {
            _tryParse = tryParse;
        }
        public override object Parse(string text, object defaultValue, Nattrib attrib)
        {
            return ParseBinder<TValue, TParseAttrib>(_tryParse, text, defaultValue, attrib);
        }
        public override string ParseText(string text, string defaultValue, Nattrib attrib)
        {
            return ParseTextBinder<TValue, TParseAttrib>(_tryParse, text, defaultValue, attrib);
        }
        public override bool TryParse(string text, Nattrib attrib, out object value)
        {
            return TryParseBinder<TValue, TParseAttrib>(_tryParse, text, attrib, out value);
        }
        public bool TryParse<T>(string text, out T value)
            where T : TValue { return TryParse<T>(text, null, out value); }
        public bool TryParse<T>(string text, Nattrib attrib, out T value)
            where T : TValue
        {
            return TryParseBinder<T, TValue, TParseAttrib>(_tryParse, text, attrib, out value);
        }
    }
}