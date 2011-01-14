﻿using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;
namespace System.Quality.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly string _connectionString;

        protected class EventOrdinal
        {
            public int Type, Blob;
            public EventOrdinal(IDataReader r)
            {
                Type = r.GetOrdinal("Type");
                Blob = r.GetOrdinal("Blob");
            }
        }

        public SqlEventStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Event> GetEventsForAggregate(object aggregateId, int startSequence)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var b = new StringBuilder();
                b.Append(@"
Select Type, Blob
From dbo.AggregateEvent
	Where (AggregateId = @aggregateId)
	And (EventSequence > @startSequence);");
                var command = new SqlCommand(b.ToString(), connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aggregateId", SqlDbType = SqlDbType.NVarChar, Value = aggregateId },
                    new SqlParameter { ParameterName = "@startSequence", SqlDbType = SqlDbType.Int, Value = startSequence } });
                connection.Open();
                var events = new List<Event>();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new EventOrdinal(r);
                    while (r.Read())
                        events.Add(MakeEvent(r, ordinal));
                }
                return events;
            }
        }

        public IEnumerable<Event> GetEventsByEventTypes(IEnumerable<Type> eventTypes)
        {
            var eventTypesXml = new XElement("r", eventTypes.Select(x => new XElement("e", new XAttribute("type", x.Name))));
            using (var connection = new SqlConnection(_connectionString))
            {
                var b = new StringBuilder();
                b.Append(@"
Select Type, Blob
From dbo.AggregateEvent
	Inner Join @eventTypesXml.nodes(N'/r/e') _xml(item)
	On (AggregateEvent.Type = _xml.item.value(N'@type', N'nvarchar(100)'))
	Order By AggregateEvent.AggregateId, AggregateEvent.EventSequence;");
                var command = new SqlCommand(b.ToString(), connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@eventTypesXml", SqlDbType = SqlDbType.Xml, Value = (eventTypesXml != null ? eventTypesXml.ToString() : string.Empty) } });
                connection.Open();
                var events = new List<Event>();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new EventOrdinal(r);
                    while (r.Read())
                        events.Add(MakeEvent(r, ordinal));
                }
                return events;
            }
        }

        public void SaveEvents(object aggregateId, IEnumerable<Event> events)
        {
            var eventsXml = new XElement("r", events.Select(x => new XElement("e", new XAttribute("sequence", x.Sequence), new XAttribute("eventDate", x.EventDate), new XAttribute("type", x.GetType().Name), SqlBuilder.ToJson(x.GetType(), x))));
            using (var connection = new SqlConnection(_connectionString))
            {
                var b = new StringBuilder();
                b.Append(@"
Insert dbo.AggregateEvent (AggregateId, EventSequence, EventDate, Type, Blob)
Select @aggregateId, _xml.item.value(N'@sequence', N'int'), _xml.item.value(N'@eventDate', N'datetime'), _xml.item.value(N'@type', N'nvarchar(100)'), _xml.item.value(N'.', N'nvarchar(max)')
From @eventsXml.nodes(N'/r/e') _xml(item);");
                var command = new SqlCommand(b.ToString(), connection) { CommandType = CommandType.Text, };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aggregateId", SqlDbType = SqlDbType.NVarChar, Value = aggregateId },
                    new SqlParameter { ParameterName = "@eventsXml", SqlDbType = SqlDbType.Xml, Value = (eventsXml != null ? eventsXml.ToString() : string.Empty) } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private Event MakeEvent(SqlDataReader r, EventOrdinal ordinal)
        {
            var type = Type.GetType(r.Field<string>(ordinal.Type));
            string blob = r.Field<string>(ordinal.Blob);
            return SqlBuilder.FromJson<Event>(type, blob);
        }
    }
}