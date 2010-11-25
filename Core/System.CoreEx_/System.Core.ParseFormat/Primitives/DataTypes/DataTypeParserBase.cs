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
    /// DataTypeParserBase
    /// </summary>
    public abstract class DataTypeParserBase : IDataTypeParser // SimpleFactoryBase<DataTypeParserBase>,
    {
        public DataTypeParserBase()
            : this(string.Empty, string.Empty) { }
        public DataTypeParserBase(object parseDefaultValue, string parseTextDefaultValue)
        {
            ParseDefaultValue = parseDefaultValue;
            ParseTextDefaultValue = parseTextDefaultValue;
        }

        /// <summary>
        /// Gets or sets the parse default value.
        /// </summary>
        /// <value>The parse default value.</value>
        protected object ParseDefaultValue { get; set; }
        /// <summary>
        /// Gets or sets the parse text default value.
        /// </summary>
        /// <value>The parse text default value.</value>
        protected string ParseTextDefaultValue { get; set; }

        /// <summary>
        /// Conversion routine that converts a string to a data value in a standardized format.
        /// </summary>
        /// <param name="text">The string value to evaluate.</param>
        /// <returns>The specified <c>text</c> parameter converted to a standardized format.</returns>
        public object Parse(string text) { return Parse(text, ParseDefaultValue, null); }
        /// <summary>
        /// Conversion routine that converts a string to a data value in a standardized format.
        /// </summary>
        /// <param name="text">The string value to evaluate.</param>
        /// <param name="attrib">The collection of attributes for parsing.</param>
        /// <returns>The specified <c>text</c> parameter converted to a standardized format.</returns>
        public object Parse(string text, Nattrib attrib) { return Parse(text, ParseDefaultValue, attrib); }
        /// <summary>
        /// Conversion routine that converts a string to a data value in a standardized format.
        /// </summary>
        /// <param name="text">The string value to evaluate.</param>
        /// <param name="defaultValue">The default value to use if conversion fails.</param>
        /// <returns>The specified <c>text</c> parameter converted to a standardized format.</returns>
        public object Parse(string text, object defaultValue) { return Parse(text, defaultValue, null); }
        /// <summary>
        /// Conversion routine that converts a "1"|"true" to true and "0"|"false" to false.
        /// </summary>
        /// <param name="text">The string value to evaluate.</param>
        /// <param name="defaultValue">The default value to use if conversion fails.</param>
        /// <param name="attrib">The collection of attributes for parsing.</param>
        /// <returns>
        /// The specified <c>text</c> parameter converted to a true|false.
        /// </returns>
        public abstract object Parse(string text, object defaultValue, Nattrib attrib);

        /// <summary>
        /// Parses the specified <c>text</c> parameter provided.
        /// </summary>
        /// <param name="text">The value to parse.</param>
        /// <returns>String representation of the results.</returns>
        public string ParseText(string text) { return ParseText(text, ParseTextDefaultValue, null); }
        /// <summary>
        /// Parses the specified <c>text</c> parameter provided.
        /// </summary>
        /// <param name="text">The value to parse.</param>
        /// <param name="attrib">The collection of attributes for parsing.</param>
        /// <returns>String representation of the results.</returns>
        public string ParseText(string text, Nattrib attrib) { return ParseText(text, ParseTextDefaultValue, attrib); }
        /// <summary>
        /// Parses the specified <c>text</c> parameter provided.
        /// </summary>
        /// <param name="text">The value to parse.</param>
        /// <param name="defaultValue">Value to use if the initial value is not valid.</param>
        /// <returns>String representation of the results.</returns>
        public string ParseText(string text, string defaultValue) { return ParseText(text, defaultValue, null); }
        /// <summary>
        /// Parses the specified <c>value</c> parameter provided.
        /// </summary>
        /// <param name="text">The value to parse.</param>
        /// <param name="defaultValue">Value to use if the initial value is not valid.</param>
        /// <param name="attrib">The collection of attributes for parsing.</param>
        /// <returns>
        /// String representation ("true"|"false") of the results.
        /// </returns>
        public abstract string ParseText(string text, string defaultValue, Nattrib attrib);

        /// <summary>
        /// Determines whether the specified text is considered a valid data value.
        /// </summary>
        /// <param name="text">The string to evaluate.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is considered a valid data value; otherwise, <c>false</c>.
        /// </returns>
        public bool TryParse(string text, out object value) { return TryParse(text, null, out value); }
        /// <summary>
        /// Determines whether the specified text is considered a valid data value.
        /// </summary>
        /// <param name="text">The string to evaluate.</param>
        /// <param name="attrib">The collection of attributes for parsing.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is considered a valid data value; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool TryParse(string text, Nattrib attrib, out object value);

        #region Binders
        protected static object ParseBinder<TValue, TParseAttrib>(TryFunc<string, TParseAttrib, TValue> tryParse, string text, object defaultValue, Nattrib attrib)
        {
            TValue value;
            return (tryParse(text, attrib.Get<TParseAttrib>(), out value) ? value.ToString() : defaultValue);
        }

        protected static string ParseTextBinder<TValue, TParseAttrib>(TryFunc<string, TParseAttrib, TValue> tryParse, string text, string defaultValue, Nattrib attrib)
        {
            TValue value;
            return (tryParse(text, attrib.Get<TParseAttrib>(), out value) ? value.ToString() : defaultValue);
        }

        protected static bool TryParseBinder<TValue, TParseAttrib>(TryFunc<string, TParseAttrib, TValue> tryParse, string text, Nattrib attrib, out object value)
        {
            TValue parsedValue;
            bool parsed = tryParse(text, attrib.Get<TParseAttrib>(), out parsedValue);
            value = (parsed ? (object)parsedValue : default(TValue));
            return parsed;
        }

        protected static bool TryParseBinder<T, TValue, TParseAttrib>(TryFunc<string, TParseAttrib, TValue> tryParse, string text, Nattrib attrib, out T value)
            where T : TValue
        {
            TValue parsedValue;
            bool parsed = tryParse(text, attrib.Get<TParseAttrib>(), out parsedValue);
            value = (T)parsedValue;
            return parsed;
        }
        #endregion
    }
}