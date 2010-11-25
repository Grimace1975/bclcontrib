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
using System.Primitives.DataTypes;
using System.Data.SqlClient;
namespace System
{
    /// <summary>
    /// DataType
    /// </summary>
    [Serializable]
    [SqlUserDefinedType(Microsoft.SqlServer.Server.Format.UserDefined, MaxByteSize = 8000)]
    public struct DataType : INullable, IBinarySerialize
    {
        private static readonly DataType s_null = new DataType(DBNull.Value);
        private bool _isNull;
        private string _key;
        private DataTypeBase _dataType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataType"/> struct.
        /// </summary>
        /// <param name="isNull">if set to <c>true</c> [is null].</param>
        /// <param name="key">The key.</param>
        private DataType(string key)
        {
            throw new NotSupportedException();
            //_isNull = false;
            //_key = (key ?? string.Empty);
            //_dataType = DataTypeBase.Get(_key);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataType"/> struct.
        /// </summary>
        /// <param name="isNull">if set to <c>true</c> [is null].</param>
        private DataType(DBNull dbNull)
        {
            _isNull = true;
            _key = null;
            _dataType = null;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static DataType Get(string key)
        {
            return new DataType(key);
        }

        /// <summary>
        /// Formats the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string FormatText(string value)
        {
            return _dataType.Formatter.FormatText(value);
        }
        /// <summary>
        /// Format2s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns></returns>
        public string FormatText2(string value, NattribType attrib)
        {
            return _dataType.Formatter.FormatText(value, attrib.Attrib);
        }
        /// <summary>
        /// Format3s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string FormatText3(string value, string defaultValue)
        {
            return _dataType.Formatter.FormatText(value, defaultValue);
        }
        /// <summary>
        /// Format4s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns></returns>
        public string FormatText4(string value, string defaultValue, NattribType attrib)
        {
            return _dataType.Formatter.FormatText(value, defaultValue, attrib.Attrib);
        }

        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string Format(object value)
        {
            return _dataType.Formatter.Format(SqlConvert.ConvertFromSqlType(value));
        }
        /// <summary>
        /// Formats the value2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns></returns>
        public string FormatValue2(object value, NattribType attrib)
        {
            return _dataType.Formatter.Format(SqlConvert.ConvertFromSqlType(value), attrib.Attrib);
        }
        /// <summary>
        /// Formats the value3.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string FormatValue3(object value, string defaultValue)
        {
            return _dataType.Formatter.Format(SqlConvert.ConvertFromSqlType(value), defaultValue);
        }
        /// <summary>
        /// Formats the value4.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns></returns>
        public string FormatValue4(object value, string defaultValue, NattribType attrib)
        {
            return _dataType.Formatter.Format(SqlConvert.ConvertFromSqlType(value), defaultValue, attrib.Attrib);
        }

        /// <summary>
        /// Determines whether the specified text is valid.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool CanParse(string text)
        {
            object value;
            return _dataType.Parser.TryParse(text, out value);
        }
        /// <summary>
        /// Determines whether the specified text is valid2.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns>
        /// 	<c>true</c> if the specified text is valid2; otherwise, <c>false</c>.
        /// </returns>
        public bool CanParse2(string text, NattribType attrib)
        {
            object value;
            return _dataType.Parser.TryParse(text, attrib.Attrib, out value);
        }

        /// <summary>
        /// Parse_s the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string Parse_(string text)
        {
            return _dataType.Parser.ParseText(text);
        }
        /// <summary>
        /// Parse2s the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns></returns>
        public string Parse2(string text, NattribType attrib)
        {
            return _dataType.Parser.ParseText(text, attrib.Attrib);
        }
        /// <summary>
        /// Parse3s the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string Parse3(string text, string defaultValue)
        {
            return _dataType.Parser.ParseText(text, defaultValue);
        }
        /// <summary>
        /// Parse4s the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="attrib">The attrib.</param>
        /// <returns></returns>
        public string Parse4(string text, string defaultValue, NattribType attrib)
        {
            return _dataType.Parser.ParseText(text, defaultValue, attrib.Attrib);
        }

        #region SqlType
        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return (_isNull ? "Null" : _key);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        public bool IsNull
        {
            get { return _isNull; }
        }

        /// <summary>
        /// Gets the null.
        /// </summary>
        /// <value>The null.</value>
        public static DataType Null
        {
            get { return s_null; }
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DataType Parse(SqlString value)
        {
            if (value.IsNull)
                return Null;
            string valueAsText = value.ToString();
            return new DataType(valueAsText);
        }
        #endregion

        #region IBinarySerialize
        /// <summary>
        /// Generates a user-defined type (UDT) or user-defined aggregate from its binary form.
        /// </summary>
        /// <param name="r">The <see cref="T:System.IO.BinaryReader"/> stream from which the object is deserialized.</param>
        void IBinarySerialize.Read(BinaryReader r)
        {
            throw new NotSupportedException();
            //_isNull = r.ReadBoolean();
            //if (!_isNull)
            //{
            //    _key = r.ReadString();
            //    _dataType = DataTypeBase.Get(_key);
            //}
            //else
            //{
            //    _key = string.Empty;
            //    _dataType = null;
            //}
        }

        /// <summary>
        /// Converts a user-defined type (UDT) or user-defined aggregate into its binary format so that it may be persisted.
        /// </summary>
        /// <param name="w">The <see cref="T:System.IO.BinaryWriter"/> stream to which the UDT or user-defined aggregate is serialized.</param>
        void IBinarySerialize.Write(BinaryWriter w)
        {
            w.Write(_isNull);
            if (!_isNull)
                w.Write(_key);
        }
        #endregion
    }
}
