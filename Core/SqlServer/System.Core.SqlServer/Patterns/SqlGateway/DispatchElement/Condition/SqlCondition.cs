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
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Xml;
using System.Collections.Generic;
namespace System.Patterns.SqlGateway.DispatchElement.Condition
{
    /// <summary>
    /// SqlCondition
    /// </summary>
    public class SqlCondition : ConditionBase
    {
        private string _sql;
        private ParameterDelegate[] _parameterArray;

        #region Class Types
        public delegate SqlParameter ParameterDelegate(ISqlDispatch dispatch);
        #endregion

        public SqlCondition(string name, XmlReader r)
        {
            Name = name;
            Sql = r.ReadString();
        }

        public string Sql
        {
            get { return _sql; }
            private set
            {
                _sql = value;
                _parameterArray = ParseSqlParameters(value);
            }
        }

        #region Execute
        /// <summary>
        /// Checks the specified instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public override bool CheckCondition(ISqlDispatch dispatch, InstructionBase instruction, string condition)
        {
            if (string.IsNullOrEmpty(_sql))
                throw new NullReferenceException("Sql is Null");
            using (var connection = new SqlConnection("context connection=true"))
            {
                connection.InfoMessage += new SqlInfoMessageEventHandler(Connection_InfoMessage);
                //
                var command = new SqlCommand(_sql, connection);
                if (_parameterArray != null)
                {
                    SqlParameter[] parameter2Array = new SqlParameter[_parameterArray.Length];
                    for (int parameter2Index = 0; parameter2Index < parameter2Array.Length; parameter2Index++)
                        parameter2Array[parameter2Index] = _parameterArray[parameter2Index](dispatch);
                    command.Parameters.AddRange(parameter2Array);
                }
                connection.Open();
                var r = command.ExecuteReader();
                return (r.HasRows ? r.GetBoolean(0) : false);
            }
        }

        /// <summary>
        /// Handles the InfoMessage event of the Connection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Data.SqlClient.SqlInfoMessageEventArgs"/> instance containing the event data.</param>
        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            SqlContext.Pipe.Send(" " + e.Errors[0].Message);
        }
        #endregion Execute

        #region Build Parameters
        //+ [Working with UDT Parameters] http://msdn.microsoft.com/en-us/library/ms131080.aspx
        private static ParameterDelegate[] ParseSqlParameters(string sql)
        {
            var listOfParameters = new List<ParameterDelegate>();
            if (sql.Contains("@nKey"))
            {
                listOfParameters.Add(CreateSqlParameter_Key);
            }
            if (sql.Contains("@cXml"))
            {
                listOfParameters.Add(CreateSqlParameter_Xml);
            }
            return (listOfParameters.Count > 0 ? listOfParameters.ToArray() : null);
        }

        /// <summary>
        /// Creates the SQL parameter_ key.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns></returns>
        private static SqlParameter CreateSqlParameter_Key(ISqlDispatch dispatch)
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
        private static SqlParameter CreateSqlParameter_Xml(ISqlDispatch dispatch)
        {
            //if (dispatch.HasPipe)
            //    SqlContext.Pipe.Send("{CreateSqlParameter_Xml}");
            return new SqlParameter("@cXml", SqlDbType.NVarChar) { Value = dispatch.StateXml };
        }
        #endregion
    }
}