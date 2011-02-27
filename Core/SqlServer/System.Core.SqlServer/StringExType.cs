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
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.IO;
namespace System
{
    /// <summary>
    /// StringExType
    /// </summary>
    [Serializable]
    [SqlUserDefinedType(Format.UserDefined, Name = "StringEx", MaxByteSize = 8000)]
    public struct StringExType : INullable, IBinarySerialize
    {
        public static string Axb(string a, string x, string b) { return StringEx.Axb(a, x, b); }
        public static string Ay(string a, string y) { return StringEx.Ay(a, y); }
        public static string Ayb(string a, string y, string b) { return StringEx.Axb(a, y, b); }
        public static string Coalesce(params string[] values) { return StringEx.Coalesce(values); }

        #region ExtractText
        public static string ExtractStringAsDigit(string text)
        {
            return StringEx.ExtractString.ExtractDigit(text);
        }
        public static string ExtractStringAsNonDigit(string text)
        {
            return StringEx.ExtractString.ExtractNonDigit(text);
        }
        public static string ExtractStringAsAlpha(string text)
        {
            return StringEx.ExtractString.ExtractAlpha(text);
        }
        public static string ExtractTextAsAlphaDigit(string text)
        {
            return StringEx.ExtractString.ExtractAlphaDigit(text);
        }
        public static string ExtractTextAsAllButLastWord(string text)
        {
            return StringEx.ExtractString.ExtractAllButLastWord(text);
        }
        #endregion

        //public static string Space(int length) { return StringEx.Space(length); }
        public static string Xa(string x, string a) { return StringEx.Xa(x, a); }
        public static string Xay(string x, string a, string y) { return StringEx.Xay(x, a, y); }

        #region StringExtentions
        public static bool ExistsIgnoreCase(string[] array, string value) { return array.ExistsIgnoreCase(value); }
        public static string Camelize(string text) { return text.Camelize(); }
        public static int CountOf(string text, string countOfText) { return text.CountOf(countOfText); }
        public static bool EndsWithSlim(string text, string value) { return text.EndsWithSlim(value); }
        public static bool EndsWithSlim2(string text, string value, bool isIgnoreCase) { return text.EndsWithSlim(value, isIgnoreCase); }
        public static string EnsureEndsWith(string text, string suffix) { return text.EnsureEndsWith(suffix); }
        public static string Left(string text, int maxLength) { return text.Left(maxLength); }
        public static string NullIf(string text) { return text.NullIf(); }
        public static string NullIf2(string text, string nullIfText) { return text.NullIf(nullIfText); }
        public static string Mid(string text, int startIndex) { return text.Mid(startIndex); }
        public static string Mid2(string text, int startIndex, int maxLength) { return text.Mid(startIndex, maxLength); }
        //public static string Format<T>(string text) {}
        //public static T Parse<T>(string text) {}
        public static string ParseBoundedPrefix(string text, string bindingSet, out string prefix) { return text.ParseBoundedPrefix(bindingSet, out prefix); }
        public static string ParseBoundedPostfix(string text, string bindingSet, out string postfix) { return text.ParseBoundedPostfix(bindingSet, out postfix); }
        public static string Right(string text, int maxLength) { return text.Right(maxLength); }
        public static bool SingleSplit(string text, string split, out string a, out string b) { return text.SingleSplit(split, out a, out b); }
        public static bool StartsWithSlim(string text, string value) { return text.StartsWithSlim(value); }
        public static bool StartsWithSlim2(string text, string value, bool isIgnoreCase) { return text.StartsWithSlim(value, isIgnoreCase); }
        public static string Truncate(string text, int maxLength, string textTruncateType) { return Truncate2(text, maxLength, textTruncateType, "..."); }
        public static string Truncate2(string text, int maxLength, string textTruncateType, string trailerText)
        {
            return text.Truncate(maxLength, (StringExtensions.TextTruncateType)Enum.Parse(typeof(StringExtensions.TextTruncateType), textTruncateType), trailerText);
        }
        #endregion

        #region SqlType
        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        public bool IsNull
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the null.
        /// </summary>
        /// <value>The null.</value>
        public static StringExType Null
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static StringExType Parse(SqlString value)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IBinarySerialize
        /// <summary>
        /// Generates a user-defined type (UDT) or user-defined aggregate from its binary form.
        /// </summary>
        /// <param name="r">The <see cref="T:System.IO.BinaryReader"/> stream from which the object is deserialized.</param>
        void IBinarySerialize.Read(BinaryReader r) { }

        /// <summary>
        /// Converts a user-defined type (UDT) or user-defined aggregate into its binary format so that it may be persisted.
        /// </summary>
        /// <param name="w">The <see cref="T:System.IO.BinaryWriter"/> stream to which the UDT or user-defined aggregate is serialized.</param>
        void IBinarySerialize.Write(BinaryWriter w) { }
        #endregion
    }
}