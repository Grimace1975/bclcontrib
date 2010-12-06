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
namespace System
{
    /// <summary>
    /// StringExtensions
    /// </summary>
    public static partial class StringExtensions
    {
        //public static string Format<T>(this T value) { return Format<T, T>(value, null); }
        //public static string Format<T>(this T value, Nattrib attrib) { return Format<T, T>(value, attrib); }
        //public static string Format<T, TValue>(this TValue value) { return Format<T, TValue>(value, null); }
        //public static string Format<T, TValue>(this TValue value, Nattrib attrib)
        //{
        //    return FormatterEx.ValueFormatterDelegateFactory<T, TValue>.Format(value, attrib);
        //}
        //public static string Format<TValue>(this TValue value, FormatterEx.IValueFormatter<TValue> formatter) { return Format<TValue>(value, formatter, null); }
        //public static string Format<TValue>(this TValue value, FormatterEx.IValueFormatter<TValue> formatter, Nattrib attrib)
        //{
        //    return formatter.Format(value, attrib);
        //}

        public static T Parse<T>(this string text) { return Parse<T, T>(text, default(T), null); }
        public static T Parse<T>(this string text, Nattrib attrib) { return Parse<T, T>(text, default(T), attrib); }
        public static T Parse<T>(this string text, T defaultValue) { return Parse<T, T>(text, defaultValue, null); }
        public static T Parse<T>(this string text, T defaultValue, Nattrib attrib) { return Parse<T, T>(text, defaultValue, attrib); }
        public static TResult Parse<T, TResult>(this string text) { return Parse<T, TResult>(text, default(TResult), null); }
        public static TResult Parse<T, TResult>(this string text, Nattrib attrib) { return Parse<T, TResult>(text, default(TResult), null); }
        public static TResult Parse<T, TResult>(this string text, TResult defaultValue) { return Parse<T, TResult>(text, defaultValue, null); }
        public static TResult Parse<T, TResult>(this string text, TResult defaultValue, Nattrib attrib)
        {
            return ParserEx.StringParserDelegateFactory<T, TResult>.Parse(text, defaultValue, attrib);
        }
        public static TResult Parse<TResult>(this string text, ParserEx.IStringParser<TResult> parser) { return Parse<TResult>(text, parser, default(TResult), null); }
        public static TResult Parse<TResult>(this string text, ParserEx.IStringParser<TResult> parser, Nattrib attrib) { return Parse<TResult>(text, parser, default(TResult), attrib); }
        public static TResult Parse<TResult>(this string text, ParserEx.IStringParser<TResult> parser, TResult defaultValue) { return Parse<TResult>(text, parser, defaultValue, null); }
        public static TResult Parse<TResult>(this string text, ParserEx.IStringParser<TResult> parser, TResult defaultValue, Nattrib attrib)
        {
            return parser.Parse(text, defaultValue, attrib);
        }

        public static object Parse<T>(this string text, object defaultValue) { return Parse<T>(text, defaultValue, null); }
        public static object Parse<T>(this string text, object defaultValue, Nattrib attrib)
        {
            return ParserEx.StringParserDelegateFactory<T, object>.Parse2(text, defaultValue, attrib);
        }
        public static object Parse<TResult>(this string text, ParserEx.IStringParser<TResult> parser, object defaultValue) { return Parse<TResult>(text, parser, defaultValue, null); }
        public static object Parse<TResult>(this string text, ParserEx.IStringParser<TResult> parser, object defaultValue, Nattrib attrib)
        {
            return parser.Parse2(text, defaultValue, attrib);
        }

        public static string ParseAndFormat<T, TResult>(this string text) { return ParseAndFormat<T, TResult>(text, null); }
        public static string ParseAndFormat<T, TResult>(this string text, Nattrib attrib)
        {
            var value = ParserEx.StringParserDelegateFactory<T, TResult>.Parse(text, default(TResult), attrib);
            return (value != null ? FormatterEx.ValueFormatterDelegateFactory<T, TResult>.Format(value, attrib) : string.Empty);
        }
        public static string ParseAndFormat<TResult>(this string text, ParserEx.IStringParser<TResult> parser, FormatterEx.IValueFormatter<TResult> formatter) { return ParseAndFormat<TResult>(text, parser, formatter, null); }
        public static string ParseAndFormat<TResult>(this string text, ParserEx.IStringParser<TResult> parser, FormatterEx.IValueFormatter<TResult> formatter, Nattrib attrib)
        {
            var value = parser.Parse(text, default(TResult), attrib);
            return (value != null ? formatter.Format(value, attrib) : string.Empty);
        }
    }
}
