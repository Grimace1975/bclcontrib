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
using Microsoft.SqlServer.Server;
using System.IO;
namespace System.Patterns.SqlGateway
{
    /// <summary>
    /// SqlDispatchProxyType
    /// </summary>
    [Serializable]
    [SqlUserDefinedType(Format.UserDefined, Name="SqlDispatchProxy", MaxByteSize = -1, IsByteOrdered = false)]
    public class SqlDispatchProxyType : INullable, IBinarySerialize
    {
        private ISqlDispatch _proxy;

        public SqlDispatchProxyType()
        {
            _proxy = null;
        }
        private SqlDispatchProxyType(ISqlDispatch proxy)
        {
            _proxy = proxy;
        }

        [SqlMethod(IsMutator = true)]
        public void AddInstruction(string name)
        {
            _proxy.AddInstruction(name);
        }
        
        [SqlMethod(IsMutator = true)]
        public void AddInstruction2(string name, SqlXml tag)
        {
            _proxy.AddInstruction2(name, tag);
        }
        [SqlMethod(IsMutator = true)]
        public void AddInstruction3(SqlXml xml)
        {
            _proxy.AddInstruction3(xml);
        }

        [SqlMethod(DataAccess = DataAccessKind.Read, IsMutator = true)]
        public void AddInstructionIf(string name, string condition)
        {
            _proxy.AddInstructionIf(name, condition);
        }
        [SqlMethod(DataAccess = DataAccessKind.Read, IsMutator = true)]
        public void AddInstructionIf2(string name, SqlXml tag, string condition)
        {
            _proxy.AddInstructionIf2(name, tag, condition);
        }

        public int Count
        {
            get { return _proxy.Count; }
        }

        [SqlMethod(IsMutator = true)]
        public void Clear()
        {
            _proxy.Clear();
        }

        [SqlMethod(IsDeterministic = true)]
        public static SqlDispatchProxyType Get(string name)
        {
            return new SqlDispatchProxyType((ISqlDispatch)Activator.CreateInstance(ProxyType, name, 0));
        }
        [SqlMethod(IsDeterministic = true)]
        public static SqlDispatchProxyType Get2(string name, SqlXml xml)
        {
            SqlDispatchProxyType dispatcher = new SqlDispatchProxyType((ISqlDispatch)Activator.CreateInstance(ProxyType, name, 0));
            dispatcher.AddInstruction3(xml);
            return dispatcher;
        }
        [SqlMethod(IsDeterministic = true)]
        public static SqlDispatchProxyType Get3(string name, int shard)
        {
            return new SqlDispatchProxyType((ISqlDispatch)Activator.CreateInstance(ProxyType, name, shard));
        }

        public ISqlDispatch InternalProxy
        {
            get { return _proxy; }
        }

        public string Name
        {
            get { return _proxy.Name; }
        }

        private static Type ProxyType
        {
            get { return Type.GetType("SqlServer.SqlDispatch, " + AssemblyRef.SqlServer); }
        }

        public int Shard
        {
            get { return _proxy.Shard; }
        }

        public SqlInt32 StateKey
        {
            get { return _proxy.StateKey; }
            set { _proxy.StateKey = value; }
        }

        public SqlString StateLastModifyBy
        {
            get { return _proxy.StateLastModifyBy; }
            set { _proxy.StateLastModifyBy = value; }
        }

        public SqlXml StateXml
        {
            get { return _proxy.StateXml; }
            set { _proxy.StateXml = value; }
        }

        #region SqlType
        public override string ToString()
        {
            return _proxy.ToString();
        }

        public bool IsNull
        {
            get { return _proxy.IsNull; }
        }

        public static SqlDispatchProxyType Null
        {
            get { throw new NotSupportedException(); }
        }

        public static SqlDispatchProxyType Parse(SqlString value)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IBinarySerialize
        [SqlMethod(DataAccess = DataAccessKind.Read)]
        void IBinarySerialize.Read(BinaryReader r)
        {
            if (_proxy == null)
                _proxy = (ISqlDispatch)Activator.CreateInstance(ProxyType, DBNull.Value);
            ((IBinarySerialize)_proxy).Read(r);
        }

        void IBinarySerialize.Write(BinaryWriter w)
        {
            ((Microsoft.SqlServer.Server.IBinarySerialize)_proxy).Write(w);
        }
        #endregion
    }
}