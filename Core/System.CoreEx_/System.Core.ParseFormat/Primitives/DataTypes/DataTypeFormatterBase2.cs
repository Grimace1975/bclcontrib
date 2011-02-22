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
    /// DataTypeFormatterBase
    /// </summary>
    /// <typeparam name="TResult">The type of the value.</typeparam>
    /// <typeparam name="TFormatAttrib">The type of the format attrib.</typeparam>
    /// <typeparam name="TParseAttrib">The type of the parse attrib.</typeparam>
    public abstract class DataTypeFormatterBase<TResult, TFormatAttrib, TParseAttrib> : DataTypeFormatterBase, FormatterEx.IValueFormatter<TResult>
    {
        private Func<TResult, TFormatAttrib, string> _format;
        private TryFunc<string, TParseAttrib, TResult> _tryParse;

        public DataTypeFormatterBase(Func<TResult, TFormatAttrib, string> format, TryFunc<string, TParseAttrib, TResult> tryParse)
        {
            _format = format;
            _tryParse = tryParse;
        }
        public override string Format(object value, string defaultValue, Nattrib attrib)
        {
            return FormatBinder<TResult, TFormatAttrib>(_format, value, defaultValue, attrib);
        }
        public override string FormatText(string value, string defaultValue, Nattrib attrib)
        {
            return FormatTextBinder<TResult, TFormatAttrib, TParseAttrib>(_format, _tryParse, value, defaultValue, attrib);
        }

        #region IValueFormatter
        public string Format(TResult value, Nattrib attrib)
        {
            return _format(value, attrib.Get<TFormatAttrib>());
        }
        #endregion
    }
}