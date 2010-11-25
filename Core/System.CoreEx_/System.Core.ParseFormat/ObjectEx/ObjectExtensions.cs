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
namespace System.ObjectEx
{
    /// <summary>
    /// ObjectExtensions
    /// </summary>
    public static partial class ObjectExtensions
    {
        public static string Format<T>(this object obj) { return Format<T, object>(obj, null); }
        public static string Format<T>(this object obj, Nattrib attrib) { return Format<T, object>(obj, attrib); }
        public static string Format<T, TValue>(this object obj) { return Format<T, TValue>(obj, null); }
        public static string Format<T, TValue>(this object obj, Nattrib attrib)
        {
            return FormatterEx.ObjectFormatterDelegateFactory<T, TValue>.Format(obj, attrib);
        }
        public static string Format<TValue>(this object obj, FormatterEx.IObjectFormatter<TValue> formatter) { return Format<TValue>(obj, formatter, null); }
        public static string Format<TValue>(this object obj, FormatterEx.IObjectFormatter<TValue> formatter, Nattrib attrib)
        {
            return formatter.Format(obj, attrib);
        }

        public static T Parse<T>(this string obj) { return Parse<T, T>(obj, default(T), null); }
        public static T Parse<T>(this string obj, Nattrib attrib) { return Parse<T, T>(obj, default(T), attrib); }
        public static T Parse<T>(this object obj, T defaultValue) { return Parse<T, T>(obj, defaultValue, null); }
        public static T Parse<T>(this object obj, T defaultValue, Nattrib attrib) { return Parse<T, T>(obj, defaultValue, attrib); }
        public static TResult Parse<T, TResult>(this string obj) { return Parse<T, TResult>(obj, default(TResult), null); }
        public static TResult Parse<T, TResult>(this object obj, Nattrib attrib) { return Parse<T, TResult>(obj, default(TResult), null); }
        public static TResult Parse<T, TResult>(this object obj, TResult defaultValue) { return Parse<T, TResult>(obj, defaultValue, null); }
        public static TResult Parse<T, TResult>(this object obj, TResult defaultValue, Nattrib attrib)
        {
            return ParserEx.ObjectParserDelegateFactory<T, TResult>.Parse(obj, defaultValue, attrib);
        }
        public static TResult Parse<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser) { return Parse<TResult>(obj, parser, default(TResult), null); }
        public static TResult Parse<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, Nattrib attrib) { return Parse<TResult>(obj, parser, default(TResult), attrib); }
        public static TResult Parse<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, TResult defaultValue) { return Parse<TResult>(obj, parser, defaultValue, null); }
        public static TResult Parse<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, TResult defaultValue, Nattrib attrib)
        {
            return parser.Parse(obj, defaultValue, attrib);
        }

        public static object Parse<T>(this object obj, object defaultValue) { return Parse<T>(obj, defaultValue, null); }
        public static object Parse<T>(this object obj, object defaultValue, Nattrib attrib)
        {
            return ParserEx.ObjectParserDelegateFactory<T, object>.Parse2(obj, defaultValue, attrib);
        }
        public static object Parse<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, object defaultValue) { return Parse<TResult>(obj, parser, defaultValue, null); }
        public static object Parse<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, object defaultValue, Nattrib attrib)
        {
            return parser.Parse2(obj, defaultValue, attrib);
        }

        public static string ParseAndFormat<T>(this object obj) { return ParseAndFormat<T>(obj, null); }
        public static string ParseAndFormat<T>(this object obj, Nattrib attrib)
        {
            var value = ParserEx.ObjectParserDelegateFactory<T, object>.Parse(obj, null, attrib);
            return (value != null ? FormatterEx.ObjectFormatterDelegateFactory<T, object>.Format(value, attrib) : string.Empty);
        }
        public static string ParseAndFormat<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, FormatterEx.IValueFormatter<TResult> formatter) { return ParseAndFormat<TResult>(obj, parser, formatter, null); }
        public static string ParseAndFormat<TResult>(this object obj, ParserEx.IObjectParser<TResult> parser, FormatterEx.IValueFormatter<TResult> formatter, Nattrib attrib)
        {
            var value = parser.Parse(obj, default(TResult), attrib);
            return (value != null ? formatter.Format(value, attrib) : string.Empty);
        }
    }
}