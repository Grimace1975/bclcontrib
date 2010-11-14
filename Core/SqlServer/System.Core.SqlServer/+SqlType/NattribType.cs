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
    /// NattribType
    /// </summary>
    [Serializable]
    [SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000)]
    public struct NattribType : INullable, IBinarySerialize
    {
        private static readonly NattribType s_null = new NattribType(DBNull.Value);
        private bool _isNull;
        private Nattrib _attrib;

        private NattribType(string[] values)
        {
            _isNull = false;
            _attrib = new Nattrib(); //values);
        }
        private NattribType(DBNull dbNull)
        {
            _isNull = true;
            _attrib = null;
        }

        public static NattribType New(string value)
        {
            return new NattribType(value.Split(';'));
        }

        /// <summary>
        /// Gets the _ attrib.
        /// </summary>
        /// <value>The _ attrib.</value>
        internal Nattrib Attrib
        {
            get { return _attrib; }
        }

        #region SqlType
        public override string ToString()
        {
            throw new NotSupportedException();
        }

        public bool IsNull
        {
            get { return _isNull; }
        }

        public static NattribType Null
        {
            get { return s_null; }
        }

        public static NattribType Parse(SqlString value)
        {
            if (value.IsNull)
                return Null;
            string valueText = value.ToString();
            return new NattribType(valueText.Length != 0 ? valueText.Split(';') : null);
        }
        #endregion

        #region IBinarySerialize
        void IBinarySerialize.Read(BinaryReader r)
        {
            _isNull = r.ReadBoolean();
            if (!_isNull)
            {
                string[] values = new string[r.ReadInt32()];
                for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
                    values[valueIndex] = r.ReadString();
                _attrib = new Nattrib(); //values);
            }
            else
                _attrib = null;
        }

        void IBinarySerialize.Write(BinaryWriter w)
        {
            w.Write(_isNull);
            if (!_isNull)
            {
                string[] values = _attrib.ToStringArray();
                w.Write(values.Length);
                for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
                    w.Write(values[valueIndex]);
            }
        }
        #endregion
    }
}