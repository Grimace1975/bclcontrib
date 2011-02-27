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
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Xml;
namespace System.Patterns.SqlGateway.DispatchElement.Instruction
{
    /// <summary>
    /// SqlInstruction
    /// </summary>
    public class SqlInstruction : InstructionBase
    {
        private string _sql;
        private ParameterDelegate[] _parameters;
        private int _dispatchParameterIndex;

        #region Class Types
        public delegate SqlParameter ParameterDelegate(ISqlDispatch dispatch, object instructionTag);
        #endregion

        public SqlInstruction(string name, XmlReader r)
        {
            Name = name;
            string sql = Sql = r.ReadString();
            var parameters = Parameters = new Dictionary<string, Parameter>();
            _parameters = ParseSqlParameters(parameters, sql, out _dispatchParameterIndex);
            if (!r.IsEmptyElement)
                ParseSchemaXml(r);
            Sql = sql;
        }

        public Dictionary<string, Parameter> Parameters { get; protected set; }

        private void ParseSchemaXml(XmlReader r)
        {
            // 1. find root element
            int xmlDepth = r.Depth;
            // 2. parse document
            var parameters = Parameters;
            Dictionary<string, ConditionBase> conditions = null;
            while ((r.Read()) && (r.Depth >= xmlDepth))
                if (r.NodeType == XmlNodeType.Element)
                {
                    string name;
                    switch (r.LocalName)
                    {
                        case "parameter":
                            var parameter = Parameter.ParseSchemaXmlParameter(r, out name);
                            parameters[name] = parameter;
                            break;
                        case "condition":
                            if (conditions != null)
                                conditions = new Dictionary<string, ConditionBase>();
                            var condition = ConditionBase.ParseSchemaXmlCondition(r, out name);
                            conditions[name] = condition;
                            break;
                    }
            }
            Conditions = conditions;
        }

        public string Sql
        {
            get { return _sql; }
            private set { _sql = value; }
        }

        #region Build Parameters
        //+ [Working with UDT Parameters] http://msdn.microsoft.com/en-us/library/ms131080.aspx
        private static ParameterDelegate[] ParseSqlParameters(Dictionary<string, Parameter> parameters, string sql, out int dispatchParameterIndex)
        {
            dispatchParameterIndex = -1;
            var listOfParameters = new List<ParameterDelegate>();
            if (sql.Contains("@oDispatchProxy"))
            {
                dispatchParameterIndex = listOfParameters.Count;
                listOfParameters.Add(CreateSqlParameter_DispatchProxy);
            }
            else if (sql.Contains("@oDispatch"))
            {
                dispatchParameterIndex = listOfParameters.Count;
                listOfParameters.Add(CreateSqlParameter_Dispatch);
            }
            if (sql.Contains("@cLastModifyBy"))
                listOfParameters.Add(CreateSqlParameter_LastModifyBy);
            if (sql.Contains("@nKey"))
                listOfParameters.Add(CreateSqlParameter_Key);
            if (sql.Contains("@cXml"))
                listOfParameters.Add(CreateSqlParameter_Xml);
            if (sql.Contains("@cDataXml"))
                listOfParameters.Add(CreateSqlParameter_DataXml);
            //// dynamic tag
            //if (parameterSet != null)
            //{
            //    var parameters3 = new SqlParameter[parameters.Count];
            //    foreach (var parameter in parameters.Values)
            //        switch (parameter.DataType)
            //        {
            //            case "xml":
            //                listOfParameters.Add(CreateSqlParameter_Xml);
            //                break;
            //        }
            //}
            return (listOfParameters.Count > 0 ? listOfParameters.ToArray() : null);
        }

        //+ [UDT] http://msdn.microsoft.com/en-us/library/7kwf5d78(VS.80).aspx
        private static SqlParameter CreateSqlParameter_DispatchProxy(ISqlDispatch dispatch, object instructionTag)
        {
            //if (dispatch._hasPipe)
            //    SqlContext.Pipe.Send("{CreateSqlParameter_Dispatch}");
            return new SqlParameter("@oDispatchProxy", SqlDbType.Udt)
            {
                UdtTypeName = "dbo.SqlDispatchProxy",
                Direction = ParameterDirection.InputOutput,
                Value = dispatch,
            };
        }

        //+ [UDT] http://msdn.microsoft.com/en-us/library/7kwf5d78(VS.80).aspx
        private static SqlParameter CreateSqlParameter_Dispatch(ISqlDispatch dispatch, object instructionTag)
        {
            if (dispatch.HasPipe)
                SqlContext.Pipe.Send("{CreateSqlParameter_Dispatch}");
            return new SqlParameter("@oDispatch", SqlDbType.Udt)
            {
                UdtTypeName = "dbo.SqlDispatch",
                Direction = ParameterDirection.InputOutput,
                Value = dispatch,
            };
        }

        private static SqlParameter CreateSqlParameter_LastModifyBy(ISqlDispatch dispatch, object instructionTag)
        {
            //if (dispatch.HasPipe)
            //    SqlContext.Pipe.Send("{CreateSqlParameter_LastModifyBy}");
            return new SqlParameter("@cLastModifyBy", SqlDbType.NVarChar, 100) { Value = dispatch.StateLastModifyBy };
        }

        /// <summary>
        /// Creates the SQL parameter_ key.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns></returns>
        private static SqlParameter CreateSqlParameter_Key(ISqlDispatch dispatch, object instructionTag)
        {
            //if (dispatch.HasPipe)
            //    SqlContext.Pipe.Send("{CreateSqlParameter_Key}");
            return new SqlParameter("@nKey", SqlDbType.Int) { Value = dispatch.StateKey };
        }

        /// <summary>
        /// Creates the SQL parameter_ XML.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns></returns>
        private static SqlParameter CreateSqlParameter_Xml(ISqlDispatch dispatch, object instructionTag)
        {
            //if (dispatch.HasPipe)
            //    SqlContext.Pipe.Send("{CreateSqlParameter_Xml}");
            return new SqlParameter("@cXml", SqlDbType.NVarChar) { Value = dispatch.StateXml };
        }

        /// <summary>
        /// Creates the SQL parameter_ data XML.
        /// </summary>
        /// <param name="dispatch">The dispatch.</param>
        /// <returns></returns>
        private static SqlParameter CreateSqlParameter_DataXml(ISqlDispatch dispatch, object instructionTag)
        {
            //if (dispatch.HasPipe)
            //    SqlContext.Pipe.Send("{CreateSqlParameter_DataXml}");
            return new SqlParameter("@cDataXml", SqlDbType.NVarChar) { Value = (SqlXml)instructionTag };
        }
        #endregion

        #region Execute
        /// <summary>
        /// Runs this instance.
        /// </summary>
        public override int Run(ref SqlDispatchType dispatch, object instructionTag)
        {
            if (string.IsNullOrEmpty(_sql))
                throw new NullReferenceException("Sql is Null");
            object returnValue = null;
            using (var connection = new SqlConnection("context connection=true"))
            {
                connection.InfoMessage += new SqlInfoMessageEventHandler(Connection_InfoMessage);
                var command = new SqlCommand(_sql, connection);
                var parameters = command.Parameters;
                var commandParameters = new SqlParameter[(_parameters != null ? _parameters.Length : 0) + 1];
                // return parameter
                commandParameters[0] = new SqlParameter("@cReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                if (_parameters != null)
                    // instruction parameters
                    for (int parameterIndex = 0; parameterIndex < _parameters.Length; parameterIndex++)
                        commandParameters[parameterIndex + 1] = _parameters[parameterIndex](dispatch, instructionTag);
                // add parameters
                parameters.AddRange(commandParameters);
                connection.Open();
                var r = command.ExecuteReader();
                returnValue = parameters[0].Value;
                if (_dispatchParameterIndex > -1)
                {
                    object commandDispatch = parameters[_dispatchParameterIndex + 1].Value;
                    var sqlDispatch = (commandDispatch as ISqlDispatch);
                    if (sqlDispatch == null)
                        sqlDispatch = ((SqlDispatchProxyType)commandDispatch).InternalProxy;
                    dispatch = (SqlDispatchType)sqlDispatch;
                }
                SqlContext.Pipe.Send(r);
            }
            return (returnValue is int ? (int)returnValue : 0);
        }

        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            SqlContext.Pipe.Send(" " + e.Errors[0].Message);
        }
        #endregion
    }
}