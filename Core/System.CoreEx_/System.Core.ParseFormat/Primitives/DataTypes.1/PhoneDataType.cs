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
    /// PhoneDataType
    /// </summary>
	public class PhoneDataType : DataTypeBase
	{
		public class FormatAttrib { }

		public class ParseAttrib
		{
			public Func<string> CountryId { get; set; }
		}

		public PhoneDataType()
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
					//+ us/generic parsing
					case "ca":
					case "us":
                        text = StringEx.ExtractString.ExtractDigit(text);
						textLength = text.Length;
						if (textLength > 10)
						{
							value = text.Substring(0, 3) + '-' + text.Substring(3, 3) + '-' + text.Substring(6, 4) + " x" + text.Substring(10);
							return true;
						}
						else if (textLength == 10)
						{
							value = text.Substring(0, 3) + '-' + text.Substring(3, 3) + '-' + text.Substring(6, 4);
							return true;
						}
						else if (textLength == 7)
						{
							value = text.Substring(0, 3) + '-' + text.Substring(3, 4);
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
						GetMaxLength = (int applicationType) => 30,
						GetSize = (int applicationType, int length) => "15",
					};
				}
			}
		}
	}
}