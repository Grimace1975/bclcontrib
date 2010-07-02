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
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
namespace System
{
    /// <summary>
    /// ParserEx
    /// </summary>
    public static partial class ParserEx
    {
        /// <summary>
        /// EnumInt32Parser
        /// </summary>
        public class EnumInt32Parser : Dictionary<string, int> { }

        /// <summary>
        /// Tries the parse ranges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAttrib">The type of the attrib.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="attrib">The attrib.</param>
        /// <param name="ranges">The ranges.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the enum int32 parser.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static EnumInt32Parser CreateEnumInt32Parser(Type type)
        {
            string[] names = Enum.GetNames(type);
            int[] values = (int[])Enum.GetValues(type);
            var parser = new EnumInt32Parser();
            for (int index = 0; index < names.Length; index++)
                parser.Add(names[index], values[index]);
            return parser;
        }

        /// <summary>
        /// Parses the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T Parse<T>(object value) { return Parse<T>(value, null); }
        public static T Parse<T>(object value, Nattrib attribs)
        {
            return ObjectParserDelegateFactory<T>.Parse(value, default(T));
        }
        /// <summary>
        /// Parses the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T Parse<T>(object value, T defaultValue)
        {
            return ObjectParserDelegateFactory<T>.Parse(value, defaultValue);
        }
        /// <summary>
        /// Parses the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static object Parse<T>(object value, object defaultValue)
        {
            return ObjectParserDelegateFactory<T>.Parse2(value, defaultValue);
        }
        /// <summary>
        /// Parses the specified @string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static T Parse<T>(string text) { return Parse<T>(text, null); }
        public static T Parse<T>(string text, Nattrib attribs)
        {
            return StringParserDelegateFactory<T>.Parse(text, default(T));
        }
        /// <summary>
        /// Parses the specified @string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T Parse<T>(string text, T defaultValue)
        {
            return StringParserDelegateFactory<T>.Parse(text, defaultValue);
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns></returns>
        public static bool TryParse<T>(object value, out T validValue)
        {
            return ObjectParserDelegateFactory<T>.TryParse(value, out validValue);
        }
        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <param name="validValue">The valid value.</param>
        /// <returns></returns>
        public static bool TryParse<T>(string text, out T validValue)
        {
            return StringParserDelegateFactory<T>.TryParse(text, out validValue);
        }

        /// <summary>
        /// Validates the specified @object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool Validate<T>(object value) { return Validate<T>(value, null); }
        public static bool Validate<T>(object value, Nattrib attrib)
        {
            return ObjectParserDelegateFactory<T>.Validate(value);
        }
        /// <summary>
        /// Validates the specified @string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static bool Validate<T>(string text) { return Validate<T>(text, null); }
        public static bool Validate<T>(string text, Nattrib attribs)
        {
            return StringParserDelegateFactory<T>.Validate(text);
        }
    }
}
