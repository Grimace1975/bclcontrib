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
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
namespace System.Patterns.SqlGateway.DispatchElement
{
    /// <summary>
    /// InstructionBase
    /// </summary>
    public abstract class InstructionBase
    {
        public virtual bool CheckCondition(ISqlDispatch dispatch, string condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");
            var conditionHash = Conditions;
            if (conditionHash == null)
                return true;
            string conditionId;
            condition = condition.ParseBoundedPrefix("[]", out conditionId);
            DispatchElement.ConditionBase condition2;
            return (conditionHash.TryGetValue(conditionId, out condition2) ? condition2.CheckCondition(dispatch, this, condition) : true);
        }

        /// <summary>
        /// Gets or sets the conditions.
        /// </summary>
        /// <value>The conditions.</value>
        public Dictionary<string, DispatchElement.ConditionBase> Conditions { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Parses the schema XML_ instruction.
        /// </summary>
        /// <param name="r">The r.</param>
        public static InstructionBase ParseSchemaXmlInstruction(XmlReader r, out string name)
        {
            name = (r.GetAttribute("name") ?? string.Empty);
            string provider = (r.GetAttribute("provider") ?? InstructionProviderEnum.Sql);
            // sql type
            switch (provider)
            {
                case InstructionProviderEnum.Sql:
                    return new Instruction.SqlInstruction(name, r);
                case InstructionProviderEnum.Clr:
                    string type = (r.GetAttribute("type") ?? string.Empty);
                    if (type.Length == 0)
                        throw new ArgumentNullException("type");
                    Type instructionType = Type.GetType(type);
                    return (instructionType != null ? new Instruction.ClrInstruction(name, instructionType, r) : null);
                default:
                    throw new InvalidOperationException();
            }
        }

        public virtual object ParseTag(SqlXml tag)
        {
            return tag;
        }

        #region Execute
        public abstract int Run(ref SqlDispatchType dispatch, object tag);
        #endregion

        #region IBinarySerialize
        public virtual object BinaryReadTag(BinaryReader r)
        {
            return r.ReadSqlType<SqlXml>();
        }

        public virtual void BinaryWriteTag(BinaryWriter w, object tag)
        {
            w.WriteSqlType<SqlXml>((SqlXml)tag);
        }
        #endregion

        #region IXmlSerializable
        public virtual object XmlReadTag(XmlReader r)
        {
            return null;
        }

        public virtual void XmlWriteTag(XmlWriter w, object tag)
        {
        }
        #endregion
    }
}