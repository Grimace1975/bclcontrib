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
    /// DataTypeFormatterBase
    /// </summary>
    public abstract class DataTypeFormatterBase : IDataTypeFormatter
    {
        /// <summary>
        /// Formats the value and returns the appropriate representation.
        /// </summary>
        /// <param name="value">The value to evaluate and convert to a string.</param>
        /// <returns>Returns a string representation of <c>value</c> in a standardized format.</returns>
        /// <remarks>Required to translate object to native data type and handle null for non-nullable types, ie. bool.</remarks>
        public string Format(object value) { return Format(value, string.Empty, null); }
        /// <summary>
        /// Formats the value and returns the appropriate representation.
        /// </summary>
        /// <param name="value">The value to evaluate and convert to a string.</param>
        /// <param name="attrib">The format to use in determining the return value.</param>
        /// <returns>Returns a string representation of <c>value</c> in a standardized format.</returns>
        /// <remarks>Required to translate object to native data type and handle null for non-nullable types, ie. bool.</remarks>
        public string Format(object value, Nattrib attrib) { return Format(value, string.Empty, attrib); }
        /// <summary>
        /// Formats the value and returns the appropriate representation.
        /// </summary>
        /// <param name="value">The value to evaluate and convert to a string.</param>
        /// <param name="defaultValue">The default value to use if <c>value</c> is null.</param>
        /// <returns>Returns a string representation of <c>value</c> in a standardized format.</returns>
        /// <remarks>Required to translate object to native data type and handle null for non-nullable types, ie. bool.</remarks>
        public string Format(object value, string defaultValue) { return Format(value, defaultValue, null); }
        /// <summary>
        /// Formats the value and returns the appropriate representation.
        /// </summary>
        /// <param name="value">The value to evaluate and convert to a string.</param>
        /// <param name="defaultValue">The default value to use if <c>value</c> is null.</param>
        /// <param name="attrib">The format to use in determining the return value.</param>
        /// <returns>
        /// Returns a string representation of <c>value</c> in a standardized format.
        /// </returns>
        /// <remarks>Required to translate object to native data type and handle null for non-nullable types, ie. bool.</remarks>
        public abstract string Format(object value, string defaultValue, Nattrib attrib);

        /// <summary>
        /// Parses the specified <c>value</c> using the default value and formatting attributes
        /// contained within the specific <c>attrib</c> hash collection.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>String representation of the results.</returns>
        public string FormatText(string text) { return FormatText(text, string.Empty, null); }
        /// <summary>
        /// Parses the specified <c>value</c> using the default value and formatting attributes
        /// contained within the specific <c>attrib</c> hash collection.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="attrib">The collection of attributes for formatting value.</param>
        /// <returns>String representation of the results.</returns>
        public string FormatText(string text, Nattrib attrib) { return FormatText(text, string.Empty, attrib); }
        /// <summary>
        /// Parses the specified <c>value</c> using the default value and formatting attributes
        /// contained within the specific <c>attrib</c> hash collection.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Value to use if the initial value is not valid.</param>
        /// <returns>String representation of the results.</returns>
        public string FormatText(string text, string defaultValue) { return FormatText(text, defaultValue, null); }
        /// <summary>
        /// Parses the specified <c>value</c> using the default value and formatting attributes
        /// contained within the specific <c>attrib</c> hash collection.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="defaultValue">Value to use if the initial value is not valid.</param>
        /// <param name="attrib">The collection of attributes for formatting value.</param>
        /// <returns>String representation of the results.</returns>
        public abstract string FormatText(string value, string defaultValue, Nattrib attrib);

        #region Binders
        protected static string FormatBinder<TValue, TFormatAttrib>(Func<TValue, TFormatAttrib, string> format, object value, string defaultValue, Nattrib attrib)
        {
            return (value != null ? format((TValue)value, attrib.Get<TFormatAttrib>()) : defaultValue);
        }

        protected static string FormatTextBinder<TValue, TFormatAttrib, TParseAttrib>(Func<TValue, TFormatAttrib, string> format, TryFunc<string, TParseAttrib, TValue> tryParse, string text, string defaultValue, Nattrib attrib)
        {
            TValue value;
            return (tryParse(text, attrib.Get<TParseAttrib>(), out value) ? format((TValue)value, attrib.Get<TFormatAttrib>()) : defaultValue);
        }
        #endregion
    }
}