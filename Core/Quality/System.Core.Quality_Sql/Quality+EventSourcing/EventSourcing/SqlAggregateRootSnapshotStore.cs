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
        private readonly string _tableName;
        private readonly ISerializer _serializer;

        protected class SnapshotOrdinal
        {
            public int Type, Blob;
            public SnapshotOrdinal(IDataReader r)
            {
                Type = r.GetOrdinal("Type");
                Blob = r.GetOrdinal("Blob");
            }
        }

        public SqlAggregateRootSnapshotStore(string connectionString)
            : this(connectionString, "AggregateSnapshot", new JsonSerializer()) { }
        public SqlAggregateRootSnapshotStore(string connectionString, string tableName)
            : this(connectionString, tableName, new JsonSerializer()) { }
        public SqlAggregateRootSnapshotStore(string connectionString, string tableName, ISerializer serializer)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (serializer == null)
                throw new ArgumentNullException("serializer");
            _connectionString = connectionString;
            _tableName = tableName;
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
	Where (AggregateId = @aggregateId);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aggregateId", SqlDbType = SqlDbType.NVarChar, Value = aggregateId } });
                connection.Open();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new SnapshotOrdinal(r);
                    return (r.Read() ? MakeSnapshot(r, ordinal) : null);
                }
            }
        }

        public IEnumerable<AggregateRootSnapshot> GetLatestSnapshots<TAggregateRoot>(IEnumerable<object> aggregateIds)
            where TAggregateRoot : AggregateRoot
        {
            var idsXml = new XElement("r", aggregateIds
                .Select(x => new XElement("i", new XAttribute("id", x.ToString()))));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select Top 1 Type, Blob
From dbo.[{0}] _Events
    Inner Join @eventsXml.nodes(N'/r/i') _xml(item)
    On (_Events.AggregateId = _xml.item.value(N'@id', N'nvarchar(100)'));", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@idsXml", SqlDbType = SqlDbType.Xml, Value = (idsXml != null ? idsXml.ToString() : string.Empty) } });
                connection.Open();
                var snapshots = new List<AggregateRootSnapshot>();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new SnapshotOrdinal(r);
                    while (r.Read())
                        snapshots.Add(MakeSnapshot(r, ordinal));
                }
                return snapshots;
            }
        }

        public void SaveSnapshot(AggregateRootSnapshot snapshot)
        {
            var snapshotType = snapshot.GetType();
            var snapshotJson = _serializer.WriteObject(snapshotType, snapshot);
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
With _Target As (
	Select *
	From dbo.[{0}]
		Where (AggregateId = @aggregateId)
)
Merge _Target
Using (
	Select @aggregateId As AggregateId) As _Source
On (_Target.AggregateId = _Source.AggregateId)
When Matched Then
	Update Set Type = @type, Blob = @blob
When Not Matched By Target Then
	Insert (AggregateId, Type, Blob)
	Values (@aggregateId, @type, @blob);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aggregateId", SqlDbType = SqlDbType.NVarChar, Value = snapshot.AggregateId },
                    new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = snapshotType.AssemblyQualifiedName },
                    new SqlParameter { ParameterName = "@blob", SqlDbType = SqlDbType.NVarChar, Value = snapshotJson } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveSnapshots(IEnumerable<AggregateRootSnapshot> snapshots)
        {
            var snapshotsXml = new XElement("r", snapshots
                .Select(x =>
                {
                    var snapshotType = x.GetType();
                    return new XElement("s",
                        new XAttribute("aggregateId", x.AggregateId),
                        new XAttribute("type", snapshotType.AssemblyQualifiedName),
                        _serializer.WriteObject(snapshotType, x));
                }));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
With _Target As (
	Select *
	From dbo.[{0}]
		Where (AggregateId = @aggregateId)
)
Merge _Target
Using (
	Select @aggregateId As AggregateId) As _Source
On (_Target.AggregateId = _Source.AggregateId)
When Matched Then
	Update Set Type = @type, Blob = @blob
When Not Matched By Target Then
	Insert (AggregateId, Type, Blob)
	Values (@aggregateId, @type, @blob);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@snapshotsXml", SqlDbType = SqlDbType.NVarChar, Value = (snapshotsXml != null ? snapshotsXml.ToString() : string.Empty) } });
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

        public bool ShouldSnapshot(AggregateRootRepository repository, AggregateRoot aggregate)
        {
            return false;
        }
    }
}