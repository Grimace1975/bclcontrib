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
        public static string Format<T>(this object obj)
        {
            return FormatterEx.ObjectFormatterDelegateFactory<T>.Format(obj);
        }

        public static T Parse<T>(this string obj) { return Parse<T, T>(obj, null); }
        public static T Parse<T>(this string obj, Nattrib attrib) { return Parse<T, T>(obj, attrib); }
        public static TResult Parse<T, TResult>(this string obj) { return Parse<T, TResult>(obj, null); }
        public static TResult Parse<T, TResult>(this object obj, Nattrib attrib)
        {
            return ParserEx.ObjectParserDelegateFactory<T, TResult>.Parse(obj, default(TResult), attrib);
        }
        public static T Parse<T>(this object obj, T defaultValue) { return Parse<T, T>(obj, defaultValue, null); }
        public static T Parse<T>(this object obj, T defaultValue, Nattrib attrib) { return Parse<T, T>(obj, defaultValue, attrib); }
        public static TResult Parse<T, TResult>(this object obj, TResult defaultValue) { return Parse<T, TResult>(obj, defaultValue, null); }
        public static TResult Parse<T, TResult>(this object obj, TResult defaultValue, Nattrib attrib)
        {
            return ParserEx.ObjectParserDelegateFactory<T, TResult>.Parse(obj, defaultValue, attrib);
        }

        public static object Parse<T>(this object obj, object defaultValue) { return Parse<T>(obj, defaultValue, null); }
        public static object Parse<T>(this object obj, object defaultValue, Nattrib attrib)
        {
            return ParserEx.ObjectParserDelegateFactory<T, object>.Parse2(obj, defaultValue, attrib);
        }

        public static string ParseAndFormat<T>(this object obj) { return ParseAndFormat<T>(obj, null); }
        public static string ParseAndFormat<T>(this object obj, Nattrib attribs)
        {
            return null;
        }
    }
}