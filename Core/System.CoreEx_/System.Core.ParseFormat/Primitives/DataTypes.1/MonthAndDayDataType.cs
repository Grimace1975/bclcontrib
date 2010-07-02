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
    /// MonthAndDayDataType
    /// </summary>
	public class MonthAndDayDataType : DataTypeBase
	{
		public class FormatAttrib
		{
			public Formats Formats;
			public string Format;
		}

		public enum Formats
		{
			Pattern,
		}

		public class ParseAttrib { }

		public MonthAndDayDataType()
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
						case Formats.Pattern:
							return value.ToString(attrib.Format, CultureInfo.InvariantCulture);
						default:
							throw new InvalidOperationException();
					}
				return value.ToString("M/d", CultureInfo.InvariantCulture);
			}

			public static bool TryParse(string text, ParseAttrib attrib, out DateTime value)
			{
				if (string.IsNullOrEmpty(text))
				{
					value = DateTime.MinValue;
					return false;
				}
				// static has cached version
				var match = Regex.Match(text, @"^\d{1,2}([/\-])\d{1,2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				if ((!match.Success) || (match.Groups.Count != 2))
				{
					value = DateTime.MinValue;
					return false;
				}
				// fix year
				text += match.Groups[1].Value + "1904";
				return DateTime.TryParse(text, out value);
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
						GetMaxLength = (int applicationType) => 5,
						GetSize = (int applicationType, int length) => "6",
					};
				}
			}
		}
	}
}