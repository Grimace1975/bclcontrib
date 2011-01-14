using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MongoDB;
using MongoDB.Configuration;
namespace System.Quality.EventSourcing
{
    public class MongoEventStore : IEventStore
    {
        private readonly IMongoDatabase _database;
        private readonly Func<object, object, bool> _aggregateKeyEqualityComparer;

        public MongoEventStore(string connectionString, ITypeCatalog eventTypeCatalog, Func<object, object, bool> aggregateKeyEqualityComparer)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder(connectionString);
            var mongo = new Mongo(BuildMongoConfiguration(connectionString, eventTypeCatalog));
            mongo.Connect();
            _database = mongo.GetDatabase(connectionStringBuilder.Database);
            _aggregateKeyEqualityComparer = aggregateKeyEqualityComparer;
        }

        private static MongoConfiguration BuildMongoConfiguration(string connectionString, ITypeCatalog eventTypeCatalog)
        {
            var configurationBuilder = new MongoConfigurationBuilder();
            configurationBuilder.ConnectionString(connectionString);
            configurationBuilder.Mapping(mapping =>
            {
                mapping.DefaultProfile(profile => profile.SubClassesAre(type => type.IsSubclassOf(typeof(Event))));
                eventTypeCatalog.GetDerivedTypes(typeof(Event), true)
                    .Yield(type => MongoBuilder.MapType(type, mapping));
            });
            return configurationBuilder.BuildConfiguration();
        }

        public IEnumerable<Event> GetEventsForAggregate(object aggregateId, int startSequence)
        {
            return _database.GetCollection<Event>("events")
                .Linq()
                .Where(e => _aggregateKeyEqualityComparer(e.AggregateId, aggregateId))
                .Where(e => e.Sequence > startSequence)
                .ToList();
        }

        public IEnumerable<Event> GetEventsByEventTypes(IEnumerable<Type> eventTypes)
        {
            var document = new Document { { "_t", new Document { { "$in", eventTypes.Select(t => t.Name).ToArray() } } } };
            var cursor = _database.GetCollection<Event>("events").Find(document);
            return cursor.Documents;
        }

        public void SaveEvents(object aggregateId, IEnumerable<Event> events)
        {
            var mogoEvents = _database.GetCollection<Event>("events");
            mogoEvents.Insert(events);
        }
    }
}