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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Xml.Linq;
namespace System.Quality.EventSourcing
{
    public class SqlAggregateRootSnapshotStore : IBatchedAggregateRootSnapshotStore
    {
        private readonly string _connectionString;
        internal readonly string _tableName;
        private readonly Func<string, object> _makeAggregateId;
        private readonly ISerializer _serializer;

        protected class SnapshotOrdinal
        {
            public int AggregateId, Type, Blob;
            public SnapshotOrdinal(IDataReader r, bool hasAggregateId)
            {
                if (hasAggregateId)
                    AggregateId = r.GetOrdinal("AggregateId");
                Type = r.GetOrdinal("Type");
                Blob = r.GetOrdinal("Blob");
            }
        }

        public SqlAggregateRootSnapshotStore(string connectionString)
            : this(connectionString, "AggregateSnapshot", null, new JsonSerializer()) { }
        public SqlAggregateRootSnapshotStore(string connectionString, string tableName)
            : this(connectionString, tableName, null, new JsonSerializer()) { }
        public SqlAggregateRootSnapshotStore(string connectionString, Func<string, object> makeAggregateId)
            : this(connectionString, "AggregateSnapshot", makeAggregateId, new JsonSerializer()) { }
        public SqlAggregateRootSnapshotStore(string connectionString, string tableName, Func<string, object> makeAggregateId)
            : this(connectionString, tableName, makeAggregateId, new JsonSerializer()) { }
        public SqlAggregateRootSnapshotStore(string connectionString, string tableName, Func<string, object> makeAggregateId, ISerializer serializer)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (serializer == null)
                throw new ArgumentNullException("serializer");
            _connectionString = connectionString;
            _tableName = tableName;
            _makeAggregateId = makeAggregateId;
            _serializer = serializer;
        }

        public AggregateRootSnapshot GetLatestSnapshot<TAggregateRoot>(object aggregateId)
            where TAggregateRoot : AggregateRoot
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select Top 1 Type, Blob
From dbo.[{0}]
	Where (AggregateId = @id)
    And (AggregateType = @aType);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.NVarChar, Value = aggregateId },
                    new SqlParameter { ParameterName = "@aType", SqlDbType = SqlDbType.NVarChar, Value = typeof(TAggregateRoot).AssemblyQualifiedName } });
                connection.Open();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new SnapshotOrdinal(r, false);
                    return (r.Read() ? MakeSnapshot(r, ordinal) : null);
                }
            }
        }

        public IEnumerable<AggregateTuple<AggregateRootSnapshot>> GetLatestSnapshots<TAggregateRoot>(IEnumerable<object> aggregateIds)
            where TAggregateRoot : AggregateRoot
        {
            var xml = new XElement("r", aggregateIds
                .Select(x => new XElement("s",
                    new XAttribute("i", x.ToString())))
                );
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select Type, Blob
From dbo.[{0}]
    Inner Join @xml.nodes(N'/r/s') _xml(item)
    On (AggregateId = _xml.item.value(N'@i', N'nvarchar(100)'))
    And (AggregateType = @aType);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aType", SqlDbType = SqlDbType.NVarChar, Value = typeof(TAggregateRoot).AssemblyQualifiedName },
                    new SqlParameter { ParameterName = "@xml", SqlDbType = SqlDbType.Xml, Value = (xml != null ? xml.ToString() : string.Empty) } });
                connection.Open();
                var snapshots = new List<AggregateTuple<AggregateRootSnapshot>>();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new SnapshotOrdinal(r, true);
                    while (r.Read())
                        snapshots.Add(MakeSnapshotTuple(r, ordinal));
                }
                return snapshots;
            }
        }

        public void SaveSnapshot(Type aggregateType, AggregateRootSnapshot snapshot)
        {
            var snapshotType = snapshot.GetType();
            var snapshotJson = _serializer.WriteObject(snapshotType, snapshot);
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
With _Target As (
	Select *
	From dbo.[{0}]
		Where (AggregateId = @id)
        And (AggregateType = @aType)
)
Merge _Target
Using (
	Select @id As AggregateId, @aType as AggregateType, @sequence as LastEventSequence,
    @type As Type, @blob As Blob) As _Source
On (_Target.AggregateId = _Source.AggregateId)
And (_Target.AggregateType = _Source.AggregateType)
When Matched Then
	Update Set Type = _Source.Type, Blob = _Source.Blob
When Not Matched By Target Then
	Insert (AggregateId, AggregateType, Type, Blob)
	Values (_Source.AggregateId, _Source.AggregateType, _Source.Type, _Source.Blob);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.NVarChar, Value = snapshot.AggregateId },
                    new SqlParameter { ParameterName = "@aType", SqlDbType = SqlDbType.NVarChar, Value = aggregateType.AssemblyQualifiedName },
                    new SqlParameter { ParameterName = "@sequence", SqlDbType = SqlDbType.Int, Value = snapshot.LastEventSequence },
                    new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = snapshotType.AssemblyQualifiedName },
                    new SqlParameter { ParameterName = "@blob", SqlDbType = SqlDbType.NVarChar, Value = snapshotJson } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveSnapshots(Type aggregateType, IEnumerable<AggregateRootSnapshot> snapshots)
        {
            var xml = new XElement("r", snapshots
                .Select(x =>
                {
                    var snapshotType = x.GetType();
                    return new XElement("s",
                        new XAttribute("i", x.AggregateId),
                        new XAttribute("s", x.LastEventSequence),
                        new XAttribute("t", snapshotType.AssemblyQualifiedName),
                        _serializer.WriteObject(snapshotType, x));
                }));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
With _Target As (
	Select *
	From dbo.[{0}]
		Where (AggregateType = @aType)
)
Merge _Target
Using (	
	Select _xml.item.value(N'@i', N'nvarchar(100)') As AggregateId, @aType as AggregateType, _xml.item.value(N'@s', N'int') As LastEventSequence
	, _xml.item.value(N'@t', N'nvarchar(400)') As Type, _xml.item.value(N'.', N'nvarchar(max)') As Blob
	From @xml.nodes(N'/r/s') _xml(item) ) As _Source
On (_Target.AggregateId = _Source.AggregateId)
And (_Target.AggregateType = _Source.AggregateType)
When Matched Then
	Update Set LastEventSequence = _Source.LastEventSequence, Type = _Source.Type, Blob = _Source.Blob
When Not Matched By Target Then
	Insert (AggregateId, AggregateType, LastEventSequence, Type, Blob)
	Values (_Source.AggregateId, _Source.AggregateType, _Source.LastEventSequence, _Source.Type, _Source.Blob);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aType", SqlDbType = SqlDbType.NVarChar, Value = aggregateType.AssemblyQualifiedName },
                    new SqlParameter { ParameterName = "@xml", SqlDbType = SqlDbType.NVarChar, Value = (xml != null ? xml.ToString() : string.Empty) } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private AggregateRootSnapshot MakeSnapshot(SqlDataReader r, SnapshotOrdinal ordinal)
        {
            var type = Type.GetType(r.Field<string>(ordinal.Type));
            string blob = r.Field<string>(ordinal.Blob);
            return _serializer.ReadObject<AggregateRootSnapshot>(type, blob);
        }

        private AggregateTuple<AggregateRootSnapshot> MakeSnapshotTuple(SqlDataReader r, SnapshotOrdinal ordinal)
        {
            var aggregateId = r.Field<string>(ordinal.AggregateId);
            return new AggregateTuple<AggregateRootSnapshot>
            {
                AggregateId = (_makeAggregateId == null ? aggregateId : _makeAggregateId(aggregateId)),
                Item1 = MakeSnapshot(r, ordinal),
            };
        }

        public Func<IAggregateRootRepository, AggregateRoot, bool> InlineSnapshotPredicate { get; set; }
    }
}