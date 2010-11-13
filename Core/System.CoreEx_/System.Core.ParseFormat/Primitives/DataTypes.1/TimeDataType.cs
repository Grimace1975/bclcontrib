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
    /// TimeDataType
    /// </summary>
    public class TimeDataType : DataTypeBase
    {
        public static readonly DateTime MinValue = new DateTime(1900, 1, 1, 0, 0, 0);

        public class FormatAttrib
        {
            public Formats Formats;
            public string Format;
        }

        public enum Formats
        {
            LongTime,
            ShortTime,
            Pattern,
        }

        public class ParseAttrib
        {
            public DateTime? MaxValue { get; set; }
            public DateTime? MinValue { get; set; }
        }

        public TimeDataType()
            : base(Prime.Type, Prime.FormFieldMeta, new DataTypeFormatter(), new DataTypeParser()) { }

        public class DataTypeFormatter : DataTypeFormatterBase<DateTime, FormatAttrib, ParseAttrib>
        {
            public DataTypeFormatter()
                : base(Prime.Format, Prime.TryParse) { }
        }

        public class DataTypeParser : DataTypeParserBase<DateTime, ParseAttrib>
        {
            public DataTypeParser()
                : base(Prime.TryParse, DateTime.MinValue, DateTime.MinValue.ToString()) { }
        }

        //public string FormatValueRange(object value, object value2, string defaultValue, params string[] parameterArray)
        //{
        //    return FormatValueRange(value, value2, defaultValue, ((parameterArray != null) || (parameterArray.Length > 0) ? new Attrib(parameterArray) : null));
        //}
        //public string FormatValueRange(object value, object value2, string defaultValue, Attrib attrib)
        //{
        //    if ((value != null) && (value2 != null))
        //    {
        //        System.DateTime validValue = (System.DateTime)value;
        //        string text = FormatValue(validValue, attrib);
        //        System.DateTime validValue2 = (System.DateTime)value2;
        //        if ((validValue.Hour != validValue2.Hour) || (validValue.Minute != validValue2.Minute) || (validValue.Second != validValue2.Second))
        //            text += " - " + FormatValue(validValue2, attrib);
        //        return text;
        //    }
        //    else if (value != null)
        //        return FormatValue((System.DateTime)value, attrib);
        //    else if (value2 != null)
        //        return FormatValue((System.DateTime)value2, attrib);
        //    return defaultValue;
        //}

        /// <summary>
        /// Prime
        /// </summary>
        public static class Prime
        {
            public static string Format(DateTime value, FormatAttrib attrib)
            {
                if (attrib != null)
                    switch (attrib.Formats)
                    {
                        case Formats.LongTime:
                            return value.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
                        case Formats.ShortTime:
                            return value.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                        case Formats.Pattern:
                            return value.ToString(attrib.Format, CultureInfo.InvariantCulture);
                        default:
                            throw new InvalidOperationException();
                    }
                return value.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }

            public static bool TryParse(string text, ParseAttrib attrib, out DateTime value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = MinValue; return false;
                }
                if (!DateTime.TryParse(text, null, DateTimeStyles.NoCurrentDateDefault, out value))
                    return false;
                value = new DateTime(1900, 1, 1, value.Hour, value.Minute, value.Second);
                // check attrib
                if (attrib != null)
                {
                    DateTime? minValue = attrib.MinValue;
                    if (minValue != null)
                    {
                        var validMinValue = minValue.Value;
                        validMinValue = new DateTime(1900, 1, 1, validMinValue.Hour, validMinValue.Minute, validMinValue.Second);
                        if (value < validMinValue)
                            return false;
                    }
                    DateTime? maxValue = attrib.MaxValue;
                    if (maxValue != null)
                    {
                        var validMaxValue = maxValue.Value;
                        validMaxValue = new DateTime(1900, 1, 1, validMaxValue.Hour, validMaxValue.Minute, validMaxValue.Second);
                        if (value > validMaxValue)
                            return false;
                    }
                }
                return true;
            }

            public static Type Type
            {
                get { return typeof(DateTime); }
            }

            public static DataTypeFormFieldMeta FormFieldMeta
            {
                get
                {
                    return new DataTypeFormFieldMeta()
                    {
                        GetBinderType = (int applicationType) => "Text",
                        GetMaxLength = (int applicationType) => 10,
                        GetSize = (int applicationType, int length) => "7",
                    };
                }
            }
        }
    }
}