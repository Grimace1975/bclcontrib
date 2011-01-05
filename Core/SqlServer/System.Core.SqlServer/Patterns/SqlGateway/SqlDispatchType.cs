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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlClient;
namespace System.Patterns.SqlGateway
{
    /// <summary>
    /// SqlDispatch
    /// </summary>
    [Serializable]
    [SqlUserDefinedType(Format.UserDefined, Name = "SqlDispatch", MaxByteSize = -1, IsByteOrdered = false)]
    public struct SqlDispatchType : ISqlDispatch, INullable, IBinarySerialize, IXmlSerializable
    {
        private static readonly SqlDispatchType s_null = new SqlDispatchType(DBNull.Value);
        private bool _hasPipe;
        private bool _isNull;
        private string _name;
        private int _shard;
        private SqlDispatchSchema _schema;
        // state
        private DispatchState _state;
        // queue
        private Queue<DispatchElement.InstructionBase> _queue;
        private Dictionary<DispatchElement.InstructionBase, object> _queueTag;

        #region Class Types
        /// <summary>
        /// DispatchState
        /// </summary>
        public class DispatchState
        {
            public SqlString LastModifyBy = "System";
            public SqlInt32 Key;
            public SqlXml Xml = SqlXml.Null;

            #region IBinarySerialize
            /// <summary>
            /// Binaries the read.
            /// </summary>
            /// <param name="r">The r.</param>
            public static DispatchState BinaryRead(BinaryReader r)
            {
                return new DispatchState()
                {
                    LastModifyBy = r.ReadSqlType<SqlString>(),
                    Key = r.ReadSqlType<SqlInt32>(),
                    Xml = r.ReadSqlType<SqlXml>(),
                };
            }

            /// <summary>
            /// Binaries the write.
            /// </summary>
            /// <param name="w">The w.</param>
            public static void BinaryWrite(BinaryWriter w, DispatchState state)
            {
                w.WriteSqlType<SqlString>(state.LastModifyBy);
                w.WriteSqlType<SqlInt32>(state.Key);
                w.WriteSqlType<SqlXml>(state.Xml);
            }
            #endregion

            #region IXmlSerialize
            /// <summary>
            /// Reads the XML.
            /// </summary>
            /// <param name="r">The r.</param>
            /// <returns></returns>
            public static DispatchState ReadXml(XmlReader r)
            {
                return new DispatchState()
                {
                    LastModifyBy = r.ReadSqlType<SqlString>("lastModifyBy"),
                    Key = r.ReadSqlType<SqlInt32>("key"),
                    Xml = r.ReadSqlType<SqlXml>(string.Empty),
                };
            }

            /// <summary>
            /// Writes the XML.
            /// </summary>
            /// <param name="w">The w.</param>
            /// <param name="state">The state.</param>
            public static void WriteXml(XmlWriter w, DispatchState state)
            {
                w.WriteSqlType<SqlString>("lastModifyBy", state.LastModifyBy);
                w.WriteSqlType<SqlInt32>("key", state.Key);
                w.WriteSqlType<SqlXml>(string.Empty, state.Xml);
            }
            #endregion
        }
        #endregion

        public SqlDispatchType(string name, int shard)
        {
            _hasPipe = false;
            _isNull = false;
            _name = name;
            _shard = shard;
            _schema = SqlDispatchSchema.Get(_name, _shard);
            // state
            _state = new DispatchState();
            // queue
            _queue = new Queue<DispatchElement.InstructionBase>();
            _queueTag = null;
        }
        public SqlDispatchType(DBNull dbNull)
        {
            _hasPipe = false;
            _isNull = true;
            _name = null;
            _shard = 0;
            _schema = null;
            // state
            _state = null;
            // queue
            _queue = null;
            _queueTag = null;
        }

        [SqlMethod(IsMutator = true)]
        public void AddInstruction(string name)
        {
            AddInstruction2(name, null);
        }
        [SqlMethod(IsMutator = true)]
        public void AddInstruction2(string name, SqlXml tag)
        {
            DispatchElement.InstructionBase instruction;
            if ((_schema.Instructions.TryGetValue(name, out instruction)) && (!_queue.Contains(instruction)))
            {
                if (_hasPipe)
                {
                    SqlContext.Pipe.Send(string.Format("[Add: {0}]", name));
                }
                InternalAddInstruction(instruction, tag);
            }
        }
        [SqlMethod(IsMutator = true)]
        public void AddInstruction3(SqlXml xml)
        {
            if (xml.IsNull)
                throw new ArgumentNullException("xml");
            throw new NotImplementedException();
        }

        [SqlMethod(DataAccess = DataAccessKind.Read, IsMutator = true)]
        public void AddInstructionIf(string name, string condition)
        {
            AddInstructionIf2(name, null, condition);
        }

        [SqlMethod(DataAccess = DataAccessKind.Read, IsMutator = true)]
        public void AddInstructionIf2(string name, SqlXml tag, string condition)
        {
            DispatchElement.InstructionBase instruction;
            if ((_schema.Instructions.TryGetValue(name, out instruction)) && (!_queue.Contains(instruction)))
            {
                if (instruction.CheckCondition(this, condition ?? string.Empty))
                {
                    if (_hasPipe == true)
                    {
                        SqlContext.Pipe.Send(string.Format("[Add: {0}]", name));
                    }
                    InternalAddInstruction(instruction, tag);
                }
            }
        }

        private void InternalAddInstruction(DispatchElement.InstructionBase instruction, SqlXml tag)
        {
            _queue.Enqueue(instruction);
            if (tag != null)
            {
                if (_queueTag == null)
                    _queueTag = new Dictionary<DispatchElement.InstructionBase, object>();
                _queueTag[instruction] = instruction.ParseTag(tag);
            }
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        [SqlMethod(IsMutator = true)]
        public void Clear()
        {
            _queue.Clear();
            if (_queueTag != null)
                _queueTag.Clear();
        }

        [SqlMethod(IsDeterministic = true)]
        public static SqlDispatchType Get(string name)
        {
            return new SqlDispatchType(name, 0);
        }
        [SqlMethod(IsDeterministic = true)]
        public static SqlDispatchType Get2(string name, SqlXml xml)
        {
            var dispatcher = new SqlDispatchType(name, 0);
            dispatcher.AddInstruction3(xml);
            return dispatcher;
        }
        /// <summary>
        /// Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="shard">The shard.</param>
        /// <returns></returns>
        [SqlMethod(IsDeterministic = true)]
        public static SqlDispatchType Get3(string name, int shard)
        {
            return new SqlDispatchType(name, shard);
        }

        public bool HasPipe
        {
            get { return _hasPipe; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int InternalRun()
        {
            _hasPipe = true;
            SqlPipe pipe = SqlContext.Pipe;
            pipe.Send("{" + _name + "}");
            int returnValue = 0;
            if (_queueTag == null)
                while (_queue.Count > 0)
                {
                    returnValue = _queue.Dequeue().Run(ref this, null);
                    if (returnValue > 0)
                        break;
                }
            else
                while (_queue.Count > 0)
                {
                    var instruction = _queue.Dequeue();
                    object tag;
                    if (_queueTag.TryGetValue(instruction, out tag))
                        _queueTag.Remove(instruction);
                    pipe.Send("Instruction: " + instruction.Name);
                    returnValue = instruction.Run(ref this, tag);
                    if (returnValue > 0)
                        break;
                }
            _hasPipe = false;
            return 0;
        }

        public int Shard
        {
            get { return _shard; }
        }

        public SqlInt32 StateKey
        {
            get { return _state.Key; }
            set
            {
                _state.Key = value;
                if (value.IsNull == false)
                {
                    var s = new MemoryStream();
                    var w = new StreamWriter(s);
                    w.Write("<root><item key=\"" + value.Value.ToString() + "\" /></root>");
                    w.Flush();
                    _state.Xml = new SqlXml(s);
                }
                else
                    _state.Xml = SqlXml.Null;
            }
        }

        /// <summary>
        /// Gets or sets the state last modify by.
        /// </summary>
        /// <value>The state last modify by.</value>
        public SqlString StateLastModifyBy
        {
            get { return _state.LastModifyBy; }
            set { _state.LastModifyBy = value; }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public SqlXml StateXml
        {
            get { return _state.Xml; }
            set
            {
                _state.Key = new SqlInt32();
                _state.Xml = value;
            }
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
            var b = new StringBuilder();
            using (var w = XmlWriter.Create(b))
                ((IXmlSerializable)this).WriteXml(w);
            return b.ToString();
        }

        public bool IsNull
        {
            get { return _isNull; }
        }

        public static SqlDispatchType Null
        {
            get { return s_null; }
        }

        [SqlMethod(DataAccess = DataAccessKind.Read, IsDeterministic = false)]
        public static SqlDispatchType Parse(SqlString value)
        {
            if (value.IsNull)
                return Null;
            //LoadState()
            string valueText = value.ToString();
            if (valueText.Length == 0)
                throw new ArgumentNullException("value");
            return new SqlDispatchType(valueText, 0);
        }
        #endregion

        #region IBinarySerialize
        [SqlMethod(DataAccess = DataAccessKind.Read)]
        void IBinarySerialize.Read(BinaryReader r)
        {
            //if (_hasPipe)
            //    SqlContext.Pipe.Send("{IBinarySerialize.Read}");
            _isNull = r.ReadBoolean();
            if (!_isNull)
            {
                //Clear();
                _name = r.ReadString();
                _shard = r.ReadInt32();
                _schema = SqlDispatchSchema.Get(_name, _shard);
                // state
                _state = DispatchState.BinaryRead(r);
                // queue
                var instructions = _schema.Instructions;
                int queueCount = r.ReadInt32();
                _queue = new Queue<DispatchElement.InstructionBase>(queueCount);
                if (r.ReadBoolean())
                {
                    _queueTag = null;
                    for (int queueIndex = 0; queueIndex < queueCount; queueIndex++)
                    {
                        DispatchElement.InstructionBase instruction;
                        if (instructions.TryGetValue(r.ReadString(), out instruction))
                        {
                            _queue.Enqueue(instruction);
                        }
                    }
                }
                else
                {
                    _queueTag = new Dictionary<DispatchElement.InstructionBase, object>();
                    for (int queueIndex = 0; queueIndex < queueCount; queueIndex++)
                    {
                        DispatchElement.InstructionBase instruction;
                        if (instructions.TryGetValue(r.ReadString(), out instruction))
                        {
                            _queue.Enqueue(instruction);
                            if (r.ReadBoolean())
                                _queueTag[instruction] = instruction.BinaryReadTag(r);
                        }
                    }
                }
            }
            else
            {
                _name = null;
                _shard = 0;
                _schema = null;
                // state
                _state = null;
                // queue
                _queue = null;
                _queueTag = null;
            }
        }

        void IBinarySerialize.Write(BinaryWriter w)
        {
            //if (_hasPipe)
            //    SqlContext.Pipe.Send("{IBinarySerialize.Write}");
            w.Write(_isNull);
            if (!_isNull)
            {
                w.Write(_name);
                w.Write(_shard);
                // state
                DispatchState.BinaryWrite(w, _state);
                // queue
                int queueCount = _queue.Count;
                w.Write(_queue.Count);
                w.Write(_queueTag == null);
                if (_queueTag == null)
                    for (int queueIndex = 0; queueIndex < queueCount; queueIndex++)
                    {
                        var instruction = _queue.Dequeue();
                        w.Write(instruction.Name);
                    }
                else
                    for (int queueIndex = 0; queueIndex < queueCount; queueIndex++)
                    {
                        var instruction = _queue.Dequeue();
                        w.Write(instruction.Name);
                        object tag;
                        bool hasTag = _queueTag.TryGetValue(instruction, out tag);
                        w.Write(hasTag);
                        if (hasTag)
                            instruction.BinaryWriteTag(w, tag);
                    }
            }
        }
        #endregion

        #region IXMLSERIALIZABLE
        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader r)
        {
            if (_hasPipe)
                SqlContext.Pipe.Send("{IXmlSerializable.Read}");
            if (!_isNull)
            {
                //Clear();
                // 1. find root element
                r.ReadToFollowing("schema");
                if (r.EOF)
                    return;
                int xmlDepth = r.Depth;
                // 2. parse document
                _name = (r.GetAttribute("name") ?? string.Empty);
                _shard = (r.GetAttribute("shard") ?? string.Empty).Parse<int>();
                _schema = SqlDispatchSchema.Get(_name, _shard);
                // state
                _state = DispatchState.ReadXml(r);
                // queue
                var instructions = new Dictionary<string, DispatchElement.InstructionBase>();
                while ((r.Read()) && (r.Depth >= xmlDepth))
                    if (r.NodeType == XmlNodeType.Element)
                    {
                        string name = r.LocalName;
                        DispatchElement.InstructionBase instruction;
                        if (instructions.TryGetValue(name, out instruction))
                        {
                            _queue.Enqueue(instruction);
                            if (!r.IsEmptyElement)
                            {
                                r.ReadToFollowing("tag");
                                if (_queueTag == null)
                                    _queueTag = new Dictionary<DispatchElement.InstructionBase, object>();
                                _queueTag[instruction] = instruction.XmlReadTag(r);
                            }
                        }
                    }
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter w)
        {
            if (_hasPipe)
                SqlContext.Pipe.Send("{IXmlSerializable.Write}");
            if (!_isNull)
            {
                w.WriteStartElement("dispatch");
                w.WriteAttributeString("name", _name);
                w.WriteAttributeString("shard", _shard.ToString());
                // state
                w.WriteStartElement("state");
                DispatchState.WriteXml(w, _state);
                w.WriteEndElement();
                // queue
                w.WriteStartElement("queue");
                int queueCount = _queue.Count;
                if (_queueTag == null)
                    foreach (var instruction in _queue)
                    {
                        w.WriteStartElement(instruction.Name);
                        w.WriteEndElement();
                    }
                else
                    foreach (var instruction in _queue)
                    {
                        w.WriteStartElement(instruction.Name);
                        object tag;
                        bool hasTag = _queueTag.TryGetValue(instruction, out tag);
                        if (hasTag)
                        {
                            w.WriteStartElement("tag");
                            instruction.XmlWriteTag(w, tag);
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();
                    }
                w.WriteEndElement(); // queue
                w.WriteEndElement();
            }
        }
        #endregion
    }
}