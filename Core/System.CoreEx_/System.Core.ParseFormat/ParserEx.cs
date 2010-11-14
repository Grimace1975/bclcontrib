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
using System.Text.RegularExpressions;
namespace System
{
    /// <summary>
    /// ParserEx
    /// </summary>
    public static partial class ParserEx
    {
        public class CanParseAttribute : Attribute { }

        public static bool TryParseRanges<T, TAttrib>(string value, Func<string, bool, string, TAttrib, Range<T>> parser, TAttrib attrib, out ICollection<Range<T>> ranges)
        {
            if (string.IsNullOrEmpty(value))
            {
                ranges = null;
                return false;
            }
            var match = Regex.Match(value, @"(?<=,|^)([^,-]+)(?:\-([^,-]+))?");
            if (match.Success)
            {
                ranges = null;
                return false;
            }
            var rangeList = new List<Range<T>>();
            while (match.Success)
            {
                var groups = match.Groups;
                var endGroup = groups[2];
                var range = (endGroup.Success ? parser(groups[1].Value, true, endGroup.Value, attrib) : parser(groups[1].Value, false, null, attrib));
                if (range == null)
                {
                    ranges = null;
                    return false;
                }
                rangeList.Add(range);
                match = match.NextMatch();
            };
            ranges = rangeList;
            return true;
        }

        #region Enum
        public class EnumParser : Dictionary<string, int> { }
        public static EnumParser MakeEnumParser(Type type)
        {
            var names = Enum.GetNames(type);
            var values = (int[])Enum.GetValues(type);
            var parser = new EnumParser();
            for (int index = 0; index < names.Length; index++)
                parser.Add(names[index], values[index]);
            return parser;
        }
        #endregion

        #region Object
        public static T Parse<T>(object value) { return Parse<T, T>(value, null); }
        public static T Parse<T>(object value, Nattrib attribs) { return Parse<T, T>(value, attribs); }
        public static TResult Parse<T, TResult>(object value) { return Parse<T, TResult>(value, null); }
        public static TResult Parse<T, TResult>(object value, Nattrib attribs)
        {
            return ObjectParserDelegateFactory<T, TResult>.Parse(value, default(TResult), attribs);
        }
        public static T Parse<T>(object value, T defaultValue) { return Parse<T, T>(value, defaultValue, null); }
        public static T Parse<T>(object value, T defaultValue, Nattrib attribs) { return Parse<T, T>(value, defaultValue, attribs); }
        public static TResult Parse<T, TResult>(object value, TResult defaultValue) { return Parse<T, TResult>(value, defaultValue, null); }
        public static TResult Parse<T, TResult>(object value, TResult defaultValue, Nattrib attribs)
        {
            return ObjectParserDelegateFactory<T, TResult>.Parse(value, defaultValue, attribs);
        }
        public static object Parse<T>(object value, object defaultValue) { return Parse<T>(value, defaultValue, null); }
        public static object Parse<T>(object value, object defaultValue, Nattrib attribs)
        {
            return ObjectParserDelegateFactory<T, object>.Parse2(value, defaultValue, attribs);
        }

        public static bool TryParse<T>(object value, out T validValue) { return TryParse<T, T>(value, null, out validValue); }
        public static bool TryParse<T>(object value, Nattrib attribs, out T validValue) { return TryParse<T, T>(value, attribs, out validValue); }
        public static bool TryParse<T, TResult>(object value, out TResult validValue) { return TryParse<T, TResult>(value, null, out validValue); }
        public static bool TryParse<T, TResult>(object value, Nattrib attribs, out TResult validValue)
        {
            return ObjectParserDelegateFactory<T, TResult>.TryParse(value, attribs, out validValue);
        }

        public static bool Validate<T>(object value) { return Validate<T>(value, null); }
        public static bool Validate<T>(object value, Nattrib attrib)
        {
            return ObjectParserDelegateFactory<T, bool>.Validate(value, attrib);
        }

        public static string ParseAndFormat<T>(this object obj) { return ParseAndFormat<T>(obj, null); }
        public static string ParseAndFormat<T>(this object obj, Nattrib attribs)
        {
            var value = ObjectParserDelegateFactory<T, object>.Parse(obj, null, attribs);
            return (value != null ? FormatterEx.ObjectFormatterDelegateFactory<T>.Format(value) : string.Empty);
        }
        #endregion

        #region String
        public static T Parse<T>(string text) { return Parse<T, T>(text, null); }
        public static T Parse<T>(string text, Nattrib attribs) { return Parse<T, T>(text, attribs); }
        public static TResult Parse<T, TResult>(string text) { return Parse<T, TResult>(text, null); }
        public static TResult Parse<T, TResult>(string text, Nattrib attribs)
        {
            return StringParserDelegateFactory<T, TResult>.Parse(text, default(TResult), attribs);
        }
        public static T Parse<T>(string text, T defaultValue) { return Parse<T, T>(text, defaultValue, null); }
        public static T Parse<T>(string text, T defaultValue, Nattrib attribs) { return Parse<T, T>(text, defaultValue, attribs); }
        public static TResult Parse<T, TResult>(string text, TResult defaultValue) { return Parse<T, TResult>(text, defaultValue, null); }
        public static TResult Parse<T, TResult>(string text, TResult defaultValue, Nattrib attribs)
        {
            return StringParserDelegateFactory<T, TResult>.Parse(text, defaultValue, attribs);
        }
        public static object Parse<T>(string text, object defaultValue) { return Parse<T>(text, defaultValue, null); }
        public static object Parse<T>(string text, object defaultValue, Nattrib attrib)
        {
            return StringParserDelegateFactory<T, object>.Parse2(text, defaultValue, attrib);
        }

        public static bool TryParse<T>(string text, out T validValue) { return TryParse<T, T>(text, null, out validValue); }
        public static bool TryParse<T>(string text, Nattrib attribs, out T validValue) { return TryParse<T, T>(text, attribs, out validValue); }
        public static bool TryParse<T, TResult>(string text, out TResult validValue) { return TryParse<T, TResult>(text, null, out validValue); }
        public static bool TryParse<T, TResult>(string text, Nattrib attribs, out TResult validValue)
        {
            return StringParserDelegateFactory<T, TResult>.TryParse(text, attribs, out validValue);
        }

        public static bool Validate<T>(string text) { return Validate<T>(text, null); }
        public static bool Validate<T>(string text, Nattrib attribs)
        {
            return StringParserDelegateFactory<T, bool>.Validate(text, attribs);
        }

        public static string ParseAndFormat<T>(string text) { return ParseAndFormat<T>(text, null); }
        public static string ParseAndFormat<T>(string text, Nattrib attribs)
        {
            var value = StringParserDelegateFactory<T, T>.Parse(text, default(T), attribs);
            return (value != null ? FormatterEx.ValueFormatterDelegateFactory<T>.Format(value) : string.Empty);
        }
        #endregion
    }
}
