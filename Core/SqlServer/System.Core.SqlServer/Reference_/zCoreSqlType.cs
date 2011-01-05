//namespace SqlServer
//{
//    /// <summary>
//    /// CoreSql
//    /// </summary>
//    [System.Serializable]
//    [Microsoft.SqlServer.Server.SqlUserDefinedType(Microsoft.SqlServer.Server.Format.UserDefined, MaxByteSize = 8000)]
//    public struct CoreSql : System.Data.SqlTypes.INullable, Microsoft.SqlServer.Server.IBinarySerialize
//    {
//        /// <summary>
//        /// Encodes the execute partial text.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns></returns>
//        public static string EncodeExecutePartialText(string value)
//        {
//            return CoreSql.EncodeExecutePartialText(value);
//        }

//        /// <summary>
//        /// Encodes the execute text.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns></returns>
//        public static string EncodeExecuteText(string value)
//        {
//            return CoreSql.EncodeExecuteText(value);
//        }

//        #region SQLTYPE
//        /// <summary>
//        /// Returns the fully qualified type name of this instance.
//        /// </summary>
//        /// <returns>
//        /// A <see cref="T:System.String"/> containing a fully qualified type name.
//        /// </returns>
//        public override string ToString()
//        {
//            throw new NotSupportedException();
//        }

//        /// <summary>
//        /// Gets a value indicating whether this instance is null.
//        /// </summary>
//        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
//        public bool IsNull
//        {
//            get { return false; }
//        }

//        /// <summary>
//        /// Gets the null.
//        /// </summary>
//        /// <value>The null.</value>
//        public static CoreSql Null
//        {
//            get { throw new NotSupportedException(); }
//        }

//        /// <summary>
//        /// Parses the specified value.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        /// <returns></returns>
//        public static CoreSql Parse(System.Data.SqlTypes.SqlString value)
//        {
//            throw new NotSupportedException();
//        }
//        #endregion SQLTYPE

//        #region IBINARYSERIALIZE
//        /// <summary>
//        /// Generates a user-defined type (UDT) or user-defined aggregate from its binary form.
//        /// </summary>
//        /// <param name="r">The <see cref="T:System.IO.BinaryReader"/> stream from which the object is deserialized.</param>
//        void Microsoft.SqlServer.Server.IBinarySerialize.Read(System.IO.BinaryReader r)
//        {
//        }

//        /// <summary>
//        /// Converts a user-defined type (UDT) or user-defined aggregate into its binary format so that it may be persisted.
//        /// </summary>
//        /// <param name="w">The <see cref="T:System.IO.BinaryWriter"/> stream to which the UDT or user-defined aggregate is serialized.</param>
//        void Microsoft.SqlServer.Server.IBinarySerialize.Write(System.IO.BinaryWriter w)
//        {
//        }
//        #endregion IBINARYSERIALIZE
//    }
//}