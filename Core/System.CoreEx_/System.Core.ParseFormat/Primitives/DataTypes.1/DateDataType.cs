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
    /// DateDataType
    /// </summary>
    public class DateDataType : DataTypeBase
    {
        private static readonly DateTime s_minValue = new DateTime(1753, 1, 1);
        private static readonly DateTime s_maxValue = new DateTime(9999, 12, 31);

        public class FormatAttrib
        {
            public Formats Formats;
            public string Format;
        }

        public enum Formats
        {
            Date,
            LongDate,
            LongDate2,
            ShortDate,
            ShorterDate,
            MonthDay,
            MonthYear,
            Pattern,
        }

        public class ParseAttrib
        {
            public bool EndOfMonth { get; set; }
            public DateTime? MaxValue { get; set; }
            public DateTime? MinValue { get; set; }
        }

        public DateDataType()
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

        public static class Prime
        {
            public static string Format(DateTime value, FormatAttrib attrib)
            {
                if (attrib != null)
                    switch (attrib.Formats)
                    {
                        case Formats.Date:
                            return value.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture);
                        case Formats.LongDate:
                            return value.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
                        case Formats.LongDate2:
                            return value.ToString((value.Year == DateTime.Today.Year ? "dddd, MMMM d" : "dddd, MMMM d, yyyy"), CultureInfo.InvariantCulture);
                        case Formats.ShortDate:
                            return value.ToString("d-MMM-yyyy", CultureInfo.InvariantCulture);
                        case Formats.ShorterDate:
                            return CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames[value.Month - 1] + ". " + value.Day + ", " + value.Year;
                        case Formats.MonthDay:
                            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[value.Month - 1] + " " + value.Day;
                        case Formats.MonthYear:
                            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[value.Month - 1] + " " + value.Year;
                        case Formats.Pattern:
                            return value.ToString(attrib.Format, CultureInfo.InvariantCulture);
                        default:
                            throw new InvalidOperationException();
                    }
                return value.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            }

            public static bool TryParse(string text, ParseAttrib attrib, out DateTime value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = DateTime.MinValue;
                    return false;
                }
                if (!DateTime.TryParse(text, out value))
                    return false;
                else if ((value < s_minValue) || (value > s_maxValue))
                    return false;
                value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
                // check attrib
                if (attrib != null)
                {
                    DateTime? minValue = attrib.MinValue;
                    if ((minValue != null) && (value < minValue))
                        return false;
                    DateTime? maxValue = attrib.MaxValue;
                    if ((maxValue != null) && (value > maxValue))
                        return false;
                    // roll date for creditcard
                    bool? endOfMonth = attrib.EndOfMonth;
                    if ((endOfMonth != null) && (endOfMonth == true))
                        value = value.ShiftDate(DateTimeExtensions.ShiftDateMethod.EndOfMonth);
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
                        GetSize = (int applicationType, int length) => "15",
                    };
                }
            }
        }
    }
}