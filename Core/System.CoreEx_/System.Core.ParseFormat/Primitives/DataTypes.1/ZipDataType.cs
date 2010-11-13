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
    /// ZipDataType
    /// </summary>
    public class ZipDataType : DataTypeBase
    {
        public readonly static ZipStruct EmptyZipStruct = new ZipStruct { Text = string.Empty };

        public class FormatAttrib { }

        public class ZipStruct
        {
            public string Text { get; set; }
        }
        public class CanadaZipStruct : ZipStruct
        {
            public string Zip { get; set; }
            public string Zip2 { get; set; }
        }
        public class UsaZipStruct : ZipStruct
        {
            public string Zip5 { get; set; }
            public string Zip4 { get; set; }
        }

        public class ParseAttrib
        {
            public Func<CountryId> CountryId { get; set; }
        }

        public ZipDataType()
            : base(Prime.Type, Prime.FormFieldMeta, new DataTypeFormatter(), new DataTypeParser()) { }

        public class DataTypeFormatter : DataTypeFormatterBase<ZipStruct, FormatAttrib, ParseAttrib>
        {
            public DataTypeFormatter()
                : base(Prime.Format, Prime.TryParse) { }
        }

        public class DataTypeParser : DataTypeParserBase<ZipStruct, ParseAttrib>
        {
            public DataTypeParser()
                : base(Prime.TryParse) { }
        }

        /// <summary>
        /// Prime
        /// </summary>
        public static class Prime
        {
            public static string Format(ZipStruct value, FormatAttrib attrib)
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                return value.Text;
            }

            public static bool TryParse(string text, ParseAttrib attrib, out ZipStruct value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = EmptyZipStruct; return false;
                }
                // check attrib
                Func<CountryId> countryIdDelegate;
                var countryId = ((attrib != null) && ((countryIdDelegate = attrib.CountryId) != null) ? countryIdDelegate() : CountryId.Usa);
                int textLength;
                if ((countryId & CountryId.Canada) == CountryId.Canada)
                {
                    // canada/generic parsing
                    text = StringEx.ExtractString.ExtractAlphaDigit(text);
                    textLength = text.Length;
                    if ((textLength == 6)
                        && (char.IsLetter(text[0])) && (char.IsDigit(text[1])) && (char.IsLetter(text[2]))
                        && (char.IsDigit(text[3])) && (char.IsLetter(text[4])) && (char.IsDigit(text[5])))
                    {
                        var zip = text.Substring(0, 3);
                        var zip2 = text.Substring(3);
                        value = new CanadaZipStruct { Text = zip + " " + zip2, Zip = zip, Zip2 = zip2 }; return true;
                    }
                }
                if ((countryId & CountryId.Usa) == CountryId.Usa)
                {
                    // usa/generic parsing
                    text = StringEx.ExtractString.ExtractDigit(text);
                    textLength = text.Length;
                    if ((textLength >= 7) && (textLength <= 9))
                    {
                        var zip5 = text.Substring(0, 5);
                        var zip4 = text.Substring(5).PadLeft(4, '0');
                        value = new UsaZipStruct { Text = zip5 + "-" + zip4, Zip5 = zip5, Zip4 = zip4 }; return true;
                    }
                    else if ((textLength >= 3) && (textLength <= 5))
                    {
                        var zip5 = text.PadLeft(5, '0');
                        value = new UsaZipStruct { Text = zip5, Zip5 = zip5 }; return true;
                    }
                }
                if ((countryId & CountryId.None) == CountryId.None)
                {
                    // accept all
                    value = new ZipStruct { Text = text }; return true;
                }
                value = EmptyZipStruct; return false;
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
                        GetMaxLength = (int applicationType) => 15,
                        GetSize = (int applicationType, int length) => "10",
                    };
                }
            }
        }
    }
}