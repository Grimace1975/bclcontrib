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
using System.Text.RegularExpressions;
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// MemoDataType
    /// </summary>
	public class MemoDataType : DataTypeBase
	{
		public class FormatAttrib { }

		public class ParseAttrib
		{
			public int? MaxLength { get; set; }
			public int? MaxLines { get; set; }
			public int? MaxNonWhiteSpaceLength { get; set; }
			public int? MinLength { get; set; }
		}

		public MemoDataType()
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
				value = text;
				// check attrib
				if (attrib != null)
				{
					int? minLength = attrib.MinLength;
					if ((minLength != null) && (value.Length < minLength))
						return false;
					int? maxLength = attrib.MaxLength;
					if ((maxLength != null) && (value.Length > maxLength))
						return false;
					int? maxNonWhiteSpaceLength = attrib.MaxNonWhiteSpaceLength;
					if ((maxNonWhiteSpaceLength != null) && (Regex.Replace(value, @"\s", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline).Length > maxNonWhiteSpaceLength))
						return false;
					int? maxLines = attrib.MaxLines;
					if ((maxLines != null) && (value.Split(new string[] { "\r\n" }, StringSplitOptions.None).Length > maxLines))
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