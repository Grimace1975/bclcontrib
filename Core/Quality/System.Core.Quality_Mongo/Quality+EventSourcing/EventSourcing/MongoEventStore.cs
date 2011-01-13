using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using MongoDB;
using MongoDB.Configuration;
namespace System.Quality.EventSourcing
{
    public class MongoEventStore : IEventStore
    {
        private readonly IMongoDatabase _database;

        public MongoEventStore(string connectionString, ITypeCatalog eventTypeCatalog)
        {
            var connectionStringBuilder = new MongoConnectionStringBuilder(connectionString);
            var mongo = new Mongo(BuildMongoConfiguration(connectionString, eventTypeCatalog));
            mongo.Connect();
            _database = mongo.GetDatabase(connectionStringBuilder.Database);
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

        public IEnumerable<Event> GetEventsForAggregate(Guid aggregateId, int startSequence)
        {
            return _database.GetCollection<Event>("events")
                .Linq()
                .Where(e => (e.AggregateId == aggregateId) && (e.Sequence > startSequence))
                .ToList();
        }

        public IEnumerable<Event> GetEventsByEventTypes(IEnumerable<Type> eventTypes)
        {
            var document = new Document { { "_t", new Document { { "$in", eventTypes.Select(t => t.Name).ToArray() } } } };
            var cursor = _database.GetCollection<Event>("events").Find(document);
            return cursor.Documents;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            var mogoEvents = _database.GetCollection<Event>("events");
            mogoEvents.Insert(events);
        }
    }
}