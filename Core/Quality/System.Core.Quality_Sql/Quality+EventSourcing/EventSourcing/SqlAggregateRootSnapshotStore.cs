using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace System.Quality.EventSourcing
{
    public class SqlAggregateRootSnapshotStore : IAggregateRootSnapshotStore
    {
        private readonly string _connectionString;

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
        {
            _connectionString = connectionString;
        }

        public AggregateRootSnapshot GetSnapshot(object aggregateId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var b = new StringBuilder();
                b.Append(@"
Select Type, Blob
From dbo.AggregateSnapshot
	Where (AggregateId = @aggregateId);");
                var command = new SqlCommand(b.ToString(), connection) { CommandType = CommandType.Text };
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

        public void SaveSnapshot<TSnapshot>(TSnapshot snapshot)
            where TSnapshot : AggregateRootSnapshot
        {
            var snapshotType = snapshot.GetType();
            var snapshotJson = SqlBuilder.ToJson(snapshotType, snapshot);
            using (var connection = new SqlConnection(_connectionString))
            {
                var b = new StringBuilder();
                b.Append(@"
With _Target As (
	Select *
	From dbo.AggregateSnapshot
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
	Values (@aggregateId, @type, @blob);");
                var command = new SqlCommand(b.ToString(), connection) { CommandType = CommandType.Text, };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aggregateId", SqlDbType = SqlDbType.NVarChar, Value = snapshot.AggregateId },
                    new SqlParameter { ParameterName = "@type", SqlDbType = SqlDbType.NVarChar, Value = snapshotType.Name },
                    new SqlParameter { ParameterName = "@blob", SqlDbType = SqlDbType.NVarChar, Value = snapshotJson } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private AggregateRootSnapshot MakeSnapshot(SqlDataReader r, SnapshotOrdinal ordinal)
        {
            var type = Type.GetType(r.Field<string>(ordinal.Type));
            string blob = r.Field<string>(ordinal.Blob);
            return SqlBuilder.FromJson<AggregateRootSnapshot>(type, blob);
        }
    }
}