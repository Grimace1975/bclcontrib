using System.Linq;
using System.Reflection;
using MongoDB;
using MongoDB.Configuration;
namespace System.Quality.EventSourcing
{
    public class MongoAggregateRootSnapshotStore : IAggregateRootSnapshotStore
    {
        private readonly IMongoDatabase _database;
        private readonly Func<object, object, bool> _aggregateKeyEqualityComparer;

        public MongoAggregateRootSnapshotStore(string connectionString, ITypeCatalog snapshotTypeCatalog, Func<object, object, bool> aggregateKeyEqualityComparer)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder(connectionString);
            var mongo = new Mongo(BuildMongoConfiguration(connectionString, snapshotTypeCatalog));
            mongo.Connect();
            _database = mongo.GetDatabase(connectionStringBuilder.Database);
            _aggregateKeyEqualityComparer = aggregateKeyEqualityComparer;
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

        public AggregateRootSnapshot GetLatestSnapshot<TAggregateRoot>(object aggregateId)
            where TAggregateRoot : AggregateRoot
        {
            return _database.GetCollection<AggregateRootSnapshot>("snapshots")
                .Linq()
                .Where(x => x.AggregateId.Equals(aggregateId))
                .SingleOrDefault();
        }

        public void SaveSnapshot(Type aggregateType, AggregateRootSnapshot snapshot)
        {
            var monoSnapshots = _database.GetCollection<AggregateRootSnapshot>("snapshots");
            monoSnapshots.Update(snapshot, UpdateFlags.Upsert);
        }

        public Func<IAggregateRootRepository, AggregateRoot, bool> InlineSnapshotPredicate { get; set; }
    }
}