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
		public class FormatAttrib { }

		public class ParseAttrib
		{
			public Func<string> CountryId { get; set; }
		}

		public ZipDataType()
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
				// check attrib
				string countryId;
				Func<string> countryIdDelegate;
				if ((attrib != null) && ((countryIdDelegate = attrib.CountryId) != null) && (!string.IsNullOrEmpty(countryId = countryIdDelegate())))
					countryId = countryId.ToLowerInvariant();
				else
					countryId = "us";
				// globalized processing
				int textLength;
				switch (countryId)
				{
					// us/generic parsing
					case "ca":
                        text = StringEx.ExtractString.ExtractAlphaDigit(text);
						textLength = text.Length;
						if ((textLength == 6)
							&& (char.IsLetter(text[0])) && (char.IsDigit(text[1])) && (char.IsLetter(text[2]))
							&& (char.IsDigit(text[3])) && (char.IsLetter(text[4])) && (char.IsDigit(text[5])))
						{
							value = text.Substring(0, 3) + " " + text.Substring(3);
							return true;
						}
						value = string.Empty;
						return false;
					case "us":
						// us/generic parsing
                        text = StringEx.ExtractString.ExtractDigit(text);
						textLength = text.Length;
						if ((textLength >= 7) && (textLength <= 9))
						{
							value = text.Substring(0, 5) + "-" + text.Substring(5).PadLeft(4, '0');
							return true;
						}
						else if ((textLength >= 3) && (textLength <= 5))
						{
							value = text.PadLeft(5, '0');
							return true;
						}
						value = string.Empty;
						return false;
					default:
						// accept all
						value = text;
						return true;
				}
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