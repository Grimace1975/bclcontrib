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
using System.Text.RegularExpressions;
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// EmailDataType
    /// </summary>
    public class EmailDataType : DataTypeBase
    {
        private const string EmailFragment = @"[\x21\x23-\x27\x2A\x2B\x2D\x2F-\x39\x3D\x3F\x41-\x5A\x5E-\x7C]";
        public static readonly string EmailPattern = "^" + EmailFragment + @"+(\." + EmailFragment + @"+)*\@" + EmailFragment + @"+(\." + EmailFragment + @"+)*$";

        public class FormatAttrib { }

        public class ParseAttrib { }

        public EmailDataType()
            : base(Prime.Type, Prime.FormFieldMeta, new DataTypeFormatter(), new DataTypeParser()) { }

        public class DataTypeFormatter : DataTypeFormatterBase<string, FormatAttrib, ParseAttrib>
        {
            public DataTypeFormatter()
                : base(Prime.Format, Prime.TryParse) { }
        }

        public class DataTypeParser : DataTypeParserBase<string, ParseAttrib>
        {
            public DataTypeParser()
                : base(Prime.TryParse) { }
        }

        public static bool IsValidCharacterForTextParsing(char value)
        {
            return (((value >= '\x2F') && (value <= '\x2F'))
            || ((value >= '\x41') && (value <= '\x5A'))
            || ((value >= '\x5E') && (value <= '\x7C'))
            || ("@.\x21\x23\x24\x25\x26\x27\x2A\x2B\x2D\x3D\x3F".IndexOf(value) > -1));
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Prime
        {
            public static string Format(string value, FormatAttrib attrib)
            {
                return value;
            }

            public static bool TryParse(string text, ParseAttrib attrib, out string value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = string.Empty;
                    return false;
                }
                // static has cached version
                if (!Regex.IsMatch(text, EmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline))
                {
                    value = string.Empty;
                    return false;
                }
                value = text;
                return true;
            }

            public static Type Type
            {
                get { return typeof(string); }
            }

            public static DataTypeFormFieldMeta FormFieldMeta
            {
                get
                {
                    return new DataTypeFormFieldMeta()
                    {
                        GetBinderType = (int applicationType) => "Text",
                        GetMaxLength = (int applicationType) => 100,
                        GetSize = (int applicationType, int length) => "25",
                    };
                }
            }
        }
    }
}