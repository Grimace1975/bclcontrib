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
//using System.Data.SqlTypes;
//using Microsoft.SqlServer.Server;
//using System.IO;
//using System.Security;
//namespace System
//{
//    /// <summary>
//    /// CryptType
//    /// </summary>
//    [Serializable]
//    [SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000)]
//    public struct CryptType : INullable, IBinarySerialize
//    {
//        private static readonly CryptType s_null = new CryptType(DBNull.Value);
//        private bool _isNull;
//        private string _cryptConfigPath;
//        private string _cryptPublicKey;
//        private Crypt _crypt;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="Crypt"/> struct.
//        /// </summary>
//        /// <param name="isNull">if set to <c>true</c> [is null].</param>
//        /// <param name="configPath">The config path.</param>
//        /// <param name="publicKey">The public key.</param>
//        private CryptType(string configPath, string publicKey)
//        {
//            _isNull = false;
//            _cryptConfigPath = (configPath.Length > 0 ? configPath : "X");
//            _cryptPublicKey = (publicKey.Length > 0 ? publicKey : "XX");
//            _crypt = new Crypt(_cryptConfigPath, _cryptPublicKey);
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="Crypt"/> struct.
//        /// </summary>
//        /// <param name="isNull">if set to <c>true</c> [is null].</param>
//        private CryptType(DBNull dbNull)
//        {
//            _isNull = true;
//            _cryptConfigPath = null;
//            _cryptPublicKey = null;
//            _crypt = null;
//        }

//        /// <summary>
//        /// News this instance.
//        /// </summary>
//        /// <returns></returns>
//        public static CryptType New()
//        {
//            return new CryptType(string.Empty, string.Empty);
//        }
//        /// <summary>
//        /// New2s the specified config path.
//        /// </summary>
//        /// <param name="configPath">The config path.</param>
//        /// <returns></returns>
//        public static CryptType New2(string configPath)
//        {
//            return new CryptType(configPath ?? string.Empty, string.Empty);
//        }
//        /// <summary>
//        /// New3s the specified config path.
//        /// </summary>
//        /// <param name="configPath">The config path.</param>
//        /// <param name="publicKey">The public key.</param>
//        /// <returns></returns>
//        public static CryptType New3(string configPath, string publicKey)
//        {
//            return new CryptType(configPath ?? string.Empty, publicKey ?? string.Empty);
//        }

//        /// <summary>
//        /// Decodes the specified text.
//        /// </summary>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Decode(string text)
//        {
//            return ((_isNull) || (text == null) || (text.Trim().Length == 0) ? null : _crypt.Decode(text));
//        }

//        /// <summary>
//        /// Encodes the specified text.
//        /// </summary>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Encode(string text)
//        {
//            return ((_isNull) || (text == null) || (text.Trim().Length == 0) ? null : _crypt.Encode(text));
//        }

//        #region SqlType
//        /// <summary>
//        /// Returns the fully qualified type name of this instance.
//        /// </summary>
//        /// <returns>
//        /// A <see cref="T:System.String"/> containing a fully qualified type name.
//        /// </returns>
//        public override string ToString()
//        {
//            return (_isNull ? "Null" : _cryptConfigPath + ":" + _cryptPublicKey);
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is null.
//        /// </summary>
//        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
//        public bool IsNull
//        {
//            get { return _isNull; }
//        }

//        /// <summary>
//        /// Gets the null.
//        /// </summary>
//        /// <value>The null.</value>
//        public static CryptType Null
//        {
//            get { return s_null; }
//        }

//        /// <summary>
//        /// Parses the specified value.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns></returns>
//        public static CryptType Parse(SqlString value)
//        {
//            if (value.IsNull)
//                return Null;
//            string valueAsText = value.ToString();
//            if (valueAsText.Length == 0)
//                return new CryptType(string.Empty, string.Empty);
//            string[] array = valueAsText.Split(new char[] { ':' }, 2);
//            return new CryptType(array[0], (array.Length > 1 ? array[1] : string.Empty));
//        }
//        #endregion

//        #region IBinarySerialize
//        /// <summary>
//        /// Generates a user-defined type (UDT) or user-defined aggregate from its binary form.
//        /// </summary>
//        /// <param name="r">The <see cref="T:System.IO.BinaryReader"/> stream from which the object is deserialized.</param>
//        void IBinarySerialize.Read(BinaryReader r)
//        {
//            _isNull = r.ReadBoolean();
//            if (!_isNull)
//            {
//                _cryptConfigPath = r.ReadString();
//                _cryptPublicKey = r.ReadString();
//                _crypt = new Crypt(_cryptConfigPath, _cryptPublicKey);
//            }
//            else
//            {
//                _cryptConfigPath = string.Empty;
//                _cryptPublicKey = string.Empty;
//                _crypt = null;
//            }
//        }

//        /// <summary>
//        /// Converts a user-defined type (UDT) or user-defined aggregate into its binary format so that it may be persisted.
//        /// </summary>
//        /// <param name="w">The <see cref="T:System.IO.BinaryWriter"/> stream to which the UDT or user-defined aggregate is serialized.</param>
//        void IBinarySerialize.Write(BinaryWriter w)
//        {
//            w.Write(_isNull);
//            if (!_isNull)
//            {
//                w.Write(_cryptConfigPath);
//                w.Write(_cryptPublicKey);
//            }
//        }
//        #endregion
//    }
//}