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
    /// UriDataType
    /// </summary>
	public class UriDataType : DataTypeBase
	{
		public class FormatAttrib { }

		public class ParseAttrib
		{
			public UriKind? UriKind { get; set; }
		}

		public UriDataType()
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
		/// Determines whether [is valid character for text parsing] [the specified value].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if [is valid character for text parsing] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsValidCharacterForTextParsing(char value)
		{
			return ((value >= '!') && (value <= '~'));
			//return (("-./0123456789:" + "?@ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" + ":%=?&+_#").IndexOf(value) > -1);
			//return (((value >= '-') && (value <= ':'))
			//|| ((value >= '@') && (value <= 'Z'))
			//|| ((value >= 'a') && (value <= 'z'))
			//|| ("+?".IndexOf(value) > -1));
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
				// pre-check attrib
				UriKind uriKind = ((attrib == null) || (attrib.UriKind == null) ? UriKind.RelativeOrAbsolute : attrib.UriKind.Value);
				if ((Uri.IsWellFormedUriString(text, uriKind))
					|| ((uriKind != UriKind.Absolute) && (Uri.IsWellFormedUriString((text.Contains("://") ? text : "http://domain.com" + text), UriKind.RelativeOrAbsolute))))
				{
					value = text;
					return true;
				}
				value = string.Empty;
				return false;
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
						GetMaxLength = (int applicationType) => 300,
						GetSize = (int applicationType, int length) => "30",
					};
				}
			}
		}
	}
}