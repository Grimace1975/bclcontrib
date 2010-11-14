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
using System.Data.SqlTypes;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.SqlServer.Server;
using System.IO;
namespace System
{
    /// <summary>
    /// SqlConfig
    /// </summary>
    [Serializable]
    [SqlUserDefinedType(Format.UserDefined, MaxByteSize = -1, IsByteOrdered = false)]
    public class SqlConfigType : INullable, IBinarySerialize
    {
        private ISqlConfig _proxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDispatch"/> struct.
        /// </summary>
        /// <param name="name">The name.</param>
        public SqlConfigType()
        {
            _proxy = null;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDispatchProxy"/> class.
        /// </summary>
        /// <param name="_proxy">The _proxy.</param>
        private SqlConfigType(ISqlConfig proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public SqlDateTime GetDateTime(string name)
        {
            return (SqlDateTime)_proxy.GetValue(name);
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public SqlDecimal GetDecimal(string name)
        {
            return (SqlDecimal)_proxy.GetValue(name);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public SqlInt32 GetInt32(string name)
        {
            return (SqlInt32)_proxy.GetValue(name);
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public SqlString GetText(string name)
        {
            return (SqlString)_proxy.GetValue(name);
        }

        /// <summary>
        /// Gets the type of the proxy.
        /// </summary>
        /// <returns></returns>
        private static Type GetProxyType
        {
            get { return Type.GetType("SqlConfig, " + AssemblyRef.SqlConfig); }
        }

        /// <summary>
        /// Builds the assembly.
        /// </summary>
        //+ [Builder] http://msdn.microsoft.com/en-us/library/system.reflection.emit.assemblybuilder.aspx
        private static void BuildAssembly()
        {
            //AssemblyName assemblyName = new AssemblyName("DynamicAssemblyExample");
            //AssemblyBuilder ab = System.AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            //ILGenerator x = new ILGenerator();
            //x.Emit(OpCodes.Add, 5);
        }

        #region SqlTypes
        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return _proxy.ToString();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        public bool IsNull
        {
            get { return _proxy.IsNull; }
        }

        /// <summary>
        /// Gets the null.
        /// </summary>
        /// <value>The null.</value>
        public static SqlConfigType Null
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlConfigType Parse(SqlString value)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IBinarySerialize
        /// <summary>
        /// Generates a user-defined type (UDT) or user-defined aggregate from its binary form.
        /// </summary>
        /// <param name="r">The <see cref="T:System.IO.BinaryReader"/> stream from which the object is deserialized.</param>
        [SqlMethod(DataAccess = DataAccessKind.Read)]
        void IBinarySerialize.Read(BinaryReader r)
        {
            if (_proxy == null)
                _proxy = (ISqlConfig)Activator.CreateInstance(GetProxyType, DBNull.Value);
            ((IBinarySerialize)_proxy).Read(r);
        }

        /// <summary>
        /// Converts a user-defined type (UDT) or user-defined aggregate into its binary format so that it may be persisted.
        /// </summary>
        /// <param name="w">The <see cref="T:System.IO.BinaryWriter"/> stream to which the UDT or user-defined aggregate is serialized.</param>
        void IBinarySerialize.Write(BinaryWriter w)
        {
            ((IBinarySerialize)_proxy).Write(w);
        }
        #endregion
    }
}