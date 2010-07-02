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
    /// BoolDataType
    /// </summary>
    public class BoolDataType : DataTypeBase
    {
        public const string YesString = "Yes";
        public const string NoString = "No";

        public class FormatAttrib
        {
            public Formats Formats;
            public string[] Values;
        }

        public enum Formats
        {
            Values,
            TrueFalse,
            YesNo,
        }

        public class ParseAttrib { }

        public BoolDataType()
            : this(new DataTypeFormatter(), new DataTypeParser()) { }
        public BoolDataType(DataTypeFormatterBase formatter, DataTypeParserBase parser)
            : base(Prime.Type, Prime.FormFieldMeta, formatter, parser) { }

        public class DataTypeFormatter : DataTypeFormatterBase<bool, FormatAttrib, ParseAttrib>
        {
            public DataTypeFormatter()
                : base(Prime.Format, Prime.TryParse) { }
        }

        public class DataTypeParser : DataTypeParserBase<bool, ParseAttrib>
        {
            public DataTypeParser()
                : base(Prime.TryParse, false, bool.FalseString) { }
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Prime
        {
            public static string Format(bool value, FormatAttrib attrib)
            {
                if (attrib != null)
                    switch (attrib.Formats)
                    {
                        case Formats.TrueFalse:
                            return value.ToString();
                        case Formats.YesNo:
                            return (value ? YesString : NoString);
                        case Formats.Values:
                            var values = attrib.Values;
                            if (values == null)
                                throw new ArgumentNullException("attrib.Values");
                            if (values.Length != 2)
                                throw new ArgumentOutOfRangeException("attrib.Values");
                            return (value ? values[0] : values[1]);
                        default:
                            throw new InvalidOperationException();
                    }
                return (value ? YesString : NoString);
            }

            public static bool TryParse(string text, ParseAttrib attrib, out bool value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = false;
                    return false;
                }
                switch (text.ToLowerInvariant())
                {
                    case "1":
                    case "true":
                    case "yes":
                        value = true;
                        return true;
                    case "0":
                    case "false":
                    case "no":
                        value = false;
                        return true;
                }
                return bool.TryParse(text, out value);
            }

            public static Type Type
            {
                get { return typeof(bool); }
            }

            public static DataTypeFormFieldMeta FormFieldMeta
            {
                get
                {
                    return new DataTypeFormFieldMeta()
                    {
                        GetBinderType = (int applicationType) => "CheckBoxWithLabel",
                        GetMaxLength = (int applicationType) => 1,
                        GetSize = (int applicationType, int length) => string.Empty,
                    };
                }
            }
        }
    }
}