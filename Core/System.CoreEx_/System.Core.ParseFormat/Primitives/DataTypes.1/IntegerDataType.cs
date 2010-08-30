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
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// IntegerDataType
    /// </summary>
    public class IntegerDataType : DataTypeBase
    {
        public class FormatAttrib
        {
            public Formats Formats;
            public string Format;
        }

        public enum Formats
        {
            Comma,
            Byte,
            Pattern,
        }

        public class ParseAttrib
        {
            public int? MaxValue { get; set; }
            public int? MinValue { get; set; }
        }

        public IntegerDataType()
            : base(Prime.Type, Prime.FormFieldMeta, new DataTypeFormatter(), new DataTypeParser()) { }

        public class DataTypeFormatter : DataTypeFormatterBase<int, FormatAttrib, ParseAttrib>
        {
            public DataTypeFormatter()
                : base(Prime.Format, Prime.TryParse) { }
        }

        public class DataTypeParser : DataTypeParserBase<int, ParseAttrib>
        {
            public DataTypeParser()
                : base(Prime.TryParse, 0, "0") { }
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Prime
        {
            public static string Format(int value, FormatAttrib attrib)
            {
                if (attrib != null)
                    switch (attrib.Formats)
                    {
                        case Formats.Comma:
                            return value.ToString("#,##0", CultureInfo.InvariantCulture);
                        case Formats.Byte:
                            int length = (int)Math.Floor((double)(value.ToString(CultureInfo.InvariantCulture).Length - 1) / 3);
                            if (length > 0)
                                return Math.Round((double)value / (2 << (10 * length)), 2).ToString(CultureInfo.InvariantCulture) + " " + "  KBMBGB".Substring(length * 2, 2);
                            if (value == 1)
                                return "1 byte";
                            return value.ToString(CultureInfo.InvariantCulture) + " bytes";
                        case Formats.Pattern:
                            return value.ToString(attrib.Format, CultureInfo.InvariantCulture);
                        default:
                            throw new InvalidOperationException();
                    }
                return value.ToString(CultureInfo.InvariantCulture);
            }

            public static bool TryParse(string text, ParseAttrib attrib, out int value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = 0;
                    return false;
                }
                if (!int.TryParse(text, out value))
                    return false;
                // check attrib
                if (attrib != null)
                {
                    int? minValue = attrib.MinValue;
                    if ((minValue != null) && (value < minValue))
                        return false;
                    int? maxValue = attrib.MaxValue;
                    if ((maxValue != null) && (value > maxValue))
                        return false;
                }
                return true;
            }

            public static Type Type
            {
                get { return typeof(int); }
            }

            public static DataTypeFormFieldMeta FormFieldMeta
            {
                get
                {
                    return new DataTypeFormFieldMeta()
                    {
                        GetBinderType = (int applicationType) => "Text",
                        GetMaxLength = (int applicationType) => 10,
                        GetSize = (int applicationType, int length) => "10",
                    };
                }
            }
        }
    }
}