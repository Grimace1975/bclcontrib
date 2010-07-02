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
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.IO;
namespace System.Patterns.SqlGateway
{
    /// <summary>
    /// SqlDispatchSchema
    /// </summary>
    public class SqlDispatchSchema
    {
        private static readonly Dictionary<string, SqlDispatchSchema> s_set = new Dictionary<string, SqlDispatchSchema>();
        private const string CartDispatch_Xml = @"<schema>
    <instruction name=""Test"">Exec dbo.[cart2_Test] @oDispatchProxy out;</instruction>
    <instruction name=""+Artwork"">Exec dbo.[cart2_Artwork] @oDispatchProxy out, @cXml;</instruction>
    <instruction name=""+CreateArtworkRequest"">Exec dbo.[cart_CreateArtworkRequest] @cLastModifyBy, @cXml;
<condition>
Select Top 1 Cart.[Key]
From dbo.Cart
    Inner Join @cXml.nodes(N'/root/extern/root/item') _Xml(item)
    On (Cart.[Key] = _Xml.item.value(N'@key', N'int'))
    Where ((Cart.FulfillmentStatusId Between 20010 And 40010) Or (Cart.FulfillmentStatusId = 90030))
</condition></instruction>
    <instruction name=""ForceArtworkAssignment"">Exec dbo.[cart_ForceArtworkAssignment~nKey] @cLastModifyBy, @nKey;</instruction>
    <instruction name=""LineItem"">Exec dbo.[cart_LineItem~nKey,cMethod] @oDispatchProxy out, @nKey, @cMethod, @cDataXml;
<parameter name=""cMethod"" />
<parameter name=""cDataXml"" /></instruction>
    <instruction name=""ManualChange"">Exec dbo.[cart_ManualChange~nKey] @cLastModifyBy, @nKey;</instruction>
    <instruction name=""+Patch"">Exec dbo.[cart_Patch~cXml] @cLastModifyBy, @cXml;</instruction>
    <instruction name=""+Print/Artwork"">Exec dbo.[cart_Print/Artwork~cXml] @cLastModifyBy, @cXml;</instruction>
    <instruction name=""Refresh"">Exec dbo.[cart2_Refresh~nKey] @oDispatchProxy out, @nKey;</instruction>
    <instruction name=""+SetArtworkPaid"">Exec dbo.[cart_SetArtworkPaid~cXml] @cLastModifyBy, @cXml;</instruction>
    <instruction name=""+Summary"">Exec dbo.[cart_Summary~cXml] @cLastModifyBy, @cXml;
<condition provider=""Sql"" include=""@cXml"">
Select Top 1 Cart.[Key]
From dbo.Cart
	Inner Join @cXml.nodes(N'/root/extern/root/item') _Xml(item)
	On (Cart.[Key] = _Xml.item.value(N'@key', N'int'))
	Where (Cart.FulfillmentStatusId Between 20010 And 99999)
</condition></instruction>
    <instruction name=""TeamDesign:Link"">Exec dbo.[cart2_TeamDesign:Link~nKey] @oDispatchProxy out, @nKey;</instruction>
    <instruction name=""TeamNumber:Link"">Exec dbo.[cart2_TeamNumber:Link~nKey] @oDispatchProxy out, @nKey;</instruction>
</schema>";
        private const string AccountDispatch_Xml = @"<schema>
   <instruction name=""+ArtworkFileCache"">Exec dbo.[account_ArtworkFileCache~cXml] @oDispatch out, @cXml;</instruction>
   <instruction name=""+ArtworkSummary"">Exec dbo.[account_ArtworkSummary~cSource,cXml] @oDispatch out, @cXml, @cP0;</instruction>
   <instruction name=""+ArtworkSummary:Print"">Exec dbo.[account_ArtworkSummary:Print~cSource,cXml] @oDispatch out, @cXml;</instruction>
   <instruction name=""+ArtworkValueContext"">Exec dbo.[account_ArtworkValueContext~cXml @oDispatch out, @cXml;</instruction>
</schema>";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDispatchSchema"/> class.
        /// </summary>
        public SqlDispatchSchema() { }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        public Dictionary<string, DispatchElement.InstructionBase> Instructions { get; private set; }

        /// <summary>
        /// Gets the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static SqlDispatchSchema Get(string name, int shard)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            string encodedName = shard.ToString() + "." + name;
            SqlDispatchSchema schema;
            if (s_set.TryGetValue(encodedName, out schema))
                return schema;
            s_set[encodedName] = schema = FetchSchema(name, shard);
            return schema;
        }

        /// <summary>
        /// Fetches the schema.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        //+ [External Access]http://social.msdn.microsoft.com/Forums/en-US/sqlnetfx/thread/5cdadddf-33bb-4c56-9d08-5a69cd9bfa6b/
        private static SqlDispatchSchema FetchSchema(string name, int shard)
        {
            switch (name)
            {
                case "CartDispatch":
                    return ParseSchemaXml(XmlReader.Create(new StringReader(CartDispatch_Xml)));
                case "AccountDispatch":
                    return ParseSchemaXml(XmlReader.Create(new StringReader(AccountDispatch_Xml)));
            }
            SqlDispatchSchema schema;
            // fetch schema
            using (var connection = new SqlConnection(LocalTR.SqlDispatchConnectionString))
            {
                var command = new SqlCommand("Exec dbo.[dp_" + SqlEncoder.EncodeObjectPartialText(name) + "] @cMethod, @nShard;", connection);
                command.Parameters.Add(new SqlParameter("@cMethod", SqlDbType.NVarChar, 50) { Value = "9 - Schema" });
                command.Parameters.Add(new SqlParameter("@nShard", SqlDbType.Int) { Value = shard });
                connection.Open();
                using (var r = command.ExecuteReader())
                {
                    if (r.Read())
                    {
                        SqlXml xml = r.GetSqlXml(0);
                        // parse schema
                        schema = (xml.IsNull == false ? ParseSchemaXml(xml.CreateReader()) : null);
                    }
                    else
                        schema = null;
                }
            }
            //if (schema == null)
            //    SqlContext.Pipe.Send("-- undefined schema");
            return schema;
        }

        /// <summary>
        /// Reads the schema XML.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        private static SqlDispatchSchema ParseSchemaXml(System.Xml.XmlReader r)
        {
            // 1. find root element
            r.ReadToFollowing("schema");
            if (r.EOF == true)
                return null;
            int xmlDepth = r.Depth;
            // 2. parse document
            var instructions = new Dictionary<string, DispatchElement.InstructionBase>();
            while ((r.Read()) && (r.Depth >= xmlDepth))
                if (r.NodeType == XmlNodeType.Element)
                    switch (r.LocalName)
                    {
                        case "instruction":
                            string name;
                            var instruction = DispatchElement.InstructionBase.ParseSchemaXmlInstruction(r, out name);
                            instructions[name] = instruction;
                            break;
                    }
            // create schema
            int instructionCount = instructions.Count;
            if (instructionCount == 0)
                throw new InvalidOperationException("No Instructions");
            // SqlContext.Pipe.Send(string.Format("-- retrieved {0} instructions", instructionCount));
            return new SqlDispatchSchema { Instructions = instructions };
        }
    }
}