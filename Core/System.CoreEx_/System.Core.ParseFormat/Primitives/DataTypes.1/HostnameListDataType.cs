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
using System.Text;
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// HostnameListDataType
    /// </summary>
	public class HostnameListDataType : DataTypeBase
	{
		public class FormatAttrib { }

		public class ParseAttrib
		{
			public int? MaxCount { get; set; }
			public string Separator { get; set; }
		}

		public HostnameListDataType()
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
        /// Prime
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
					value = string.Empty; return false;
				}
				var valueStream = new StringBuilder();
				string[] hostnameArray = text.Replace("\r", "").Replace('\n', ';').Replace(',', ';').Split(';');
				int hostnameCount = 0;
				string separator = ((attrib == null) || (attrib.Separator == null) ? "\n" : attrib.Separator);
				int separatorLength = separator.Length;
				foreach (string hostname in hostnameArray)
				{
					string hostname2 = hostname.Trim();
					if (hostname2.Length == 0)
						continue;
					// static has cached version
					if (!Regex.IsMatch(text, HostnameDataType.HostnamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline))
					{
						value = string.Empty; return false;
					}
					hostnameCount++;
					valueStream.Append(hostname2 + separator);
				}
				if (valueStream.Length > 1)
					valueStream.Length -= separatorLength;
				value = valueStream.ToString();
				// check attrib
				if (attrib != null)
				{
					int? maxCount = attrib.MaxCount;
					if ((maxCount != null) && (hostnameCount > maxCount))
						return false;
				}
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
						GetBinderType = (int applicationType) => "TextArea",
						GetMaxLength = (int applicationType) => -1,
						GetSize = (int applicationType, int length) => "60x15",
					};
				}
			}
		}
	}
}