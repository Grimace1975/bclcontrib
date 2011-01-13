using System.Linq;
using System.Reflection;
using MongoDB;
using MongoDB.Configuration;
namespace System.Quality.EventSourcing
{
    public class MongoAggregateRootSnapshotStore : IAggregateRootSnapshotStore
    {
        private readonly IMongoDatabase _database;

        public MongoAggregateRootSnapshotStore(string connectionString, ITypeCatalog snapshotTypeCatalog)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder(connectionString);
            var mongo = new Mongo(BuildMongoConfiguration(connectionString, snapshotTypeCatalog));
            mongo.Connect();
            _database = mongo.GetDatabase(connectionStringBuilder.Database);
        }

        private static MongoConfiguration BuildMongoConfiguration(string connectionString, ITypeCatalog snapshotTypeCatalog)
        {
            var configurationBuilder = new MongoConfigurationBuilder();
            configurationBuilder.ConnectionString(connectionString);
            configurationBuilder.Mapping(mapping =>
            {
                mapping.DefaultProfile(profile => profile.SubClassesAre(type => type.IsSubclassOf(typeof(AggregateRootSnapshot))));
                snapshotTypeCatalog.GetDerivedTypes(typeof(AggregateRootSnapshot), true)
                    .Yield(type => MongoBuilder.MapType(type, mapping));
            });
            return configurationBuilder.BuildConfiguration();
        }

        public AggregateRootSnapshot GetSnapshot(Guid aggregateId)
        {
            return _database.GetCollection<AggregateRootSnapshot>("snapshots")
                .Linq()
                .Where(x => x.AggregateId == aggregateId)
                .SingleOrDefault();
        }

        public void SaveSnapshot<TSnapshot>(TSnapshot snapshot)
            where TSnapshot : AggregateRootSnapshot
        {
            var monoSnapshots = _database.GetCollection<TSnapshot>("snapshots");
            monoSnapshots.Save(snapshot);
        }
    }
}