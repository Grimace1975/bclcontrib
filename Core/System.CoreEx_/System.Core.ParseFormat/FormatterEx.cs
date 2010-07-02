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
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Primitives;
namespace System
{
    /// <summary>
    /// FormatterEx
    /// </summary>
    public static partial class FormatterEx
    {
        /// <summary>
        /// Formats the ranges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ranges">The ranges.</param>
        /// <param name="formater">The formater.</param>
        /// <returns></returns>
        public static string FormatRanges<T, TAttrib>(ICollection<Range<T>> ranges, Func<T, TAttrib, string> formater, TAttrib attrib)
        {
            if (ranges == null)
                throw new ArgumentNullException("ranges");
            if (ranges.Count == 0)
                return string.Empty;
            var b = new StringBuilder();
            foreach (Range<T> range in ranges)
            {
                b.Append(formater(range.BeginValue, attrib));
                if (range.HasEndValue)
                {
                    b.Append(" - ");
                    b.Append(formater(range.EndValue, attrib));
                }
                b.Append(", ");
            }
            b.Length -= 2;
            return b.ToString();
        }

        /// <summary>
        /// Formats the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Format<T>(object value) { return Format<T>(value, null); }
        public static string Format<T>(object value, Nattrib attribs)
        {
            return ObjectFormatterDelegateFactory<T>.Format(value);
        }
        /// <summary>
        /// Formats the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Format<T>(T value) { return Format<T>(value, null); }
        public static string Format<T>(T value, Nattrib attribs)
        {
            return ValueFormatterDelegateFactory<T>.Format(value);
        }
    }
}
