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
    /// UriIdDataType
    /// </summary>
	public class UriIdDataType : TextDataType
	{
		public readonly static string UriIdPattern = @"^[A-Z0-9\-_]+$";

		public new class ParseAttrib : TextDataType.ParseAttrib { }

		public UriIdDataType()
			: base(new DataTypeFormatter(), new DataTypeParser()) { }

		public new class DataTypeParser : DataTypeParserBase<string, ParseAttrib>
		{
			public DataTypeParser()
				: base(Prime.TryParse) { }
		}

        /// <summary>
        /// Prime
        /// </summary>
		public new static class Prime
		{
			public static bool TryParse(string text, ParseAttrib attrib, out string value)
			{
				if (TextDataType.Prime.TryParse(text, attrib, out value))
					return Regex.IsMatch(text, UriIdPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				return false;
			}

			public static DataTypeFormFieldMeta FormFieldMeta
			{
				get
				{
					return new DataTypeFormFieldMeta()
					{
						GetBinderType = (int applicationType) => "Text",
						GetMaxLength = (int applicationType) => 100,
						GetSize = (int applicationType, int length) => (length <= 50 ? "15" : "25"),
					};
				}
			}
		}
	}
}