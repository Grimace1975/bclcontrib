﻿using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;
namespace System.Quality.EventSourcing
{
    public class SqlEventStore : IBatchedEventStore
    {
        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly ISerializer _serializer;

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
            : this(connectionString, "AggregateEvent", new JsonSerializer()) { }
        public SqlEventStore(string connectionString, string tableName)
            : this(connectionString, tableName, new JsonSerializer()) { }
        public SqlEventStore(string connectionString, string tableName, ISerializer serializer)
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

        public IEnumerable<Event> GetEventsById(object aggregateId, int startSequence)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select Type, Blob
From dbo.[{0}]
	Where (AggregateId = @aggregateId)
	And (EventSequence > @startSequence)
Order By EventSequence;", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
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

        public IEnumerable<AggregateTuple<IEnumerable<Event>>> GetEventsByIds(IEnumerable<AggregateTuple<int>> aggregateIds)
        {
            var idsXml = new XElement("r", aggregateIds
                .Select(x => new XElement("i",
                    new XAttribute("aggregateId", x.AggregateId),
                    new XAttribute("startSequence", x.Item1)))
                );
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select Type, Blob
From dbo.[{0}]
	Where (AggregateId = @aggregateId)
	And (EventSequence > @startSequence)
Order By EventSequence;", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@idsXml", SqlDbType = SqlDbType.Xml, Value = (idsXml != null ? idsXml.ToString() : string.Empty) } });
                connection.Open();
                var events = new List<AggregateTuple<IEnumerable<Event>>>();
                var events2 = new List<Event>();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new EventOrdinal(r);
                    while (r.Read())
                        events2.Add(MakeEvent(r, ordinal));
                }
                return events;
            }
        }

        //        public IEnumerable<Event> GetEventsByEventTypes(IEnumerable<Type> eventTypes)
        //        {
        //            var eventTypesXml = new XElement("r", eventTypes
        //                .Select(x => new XElement("e",
        //                    new XAttribute("type", x.AssemblyQualifiedName)
        //                )));
        //            using (var connection = new SqlConnection(_connectionString))
        //            {
        //                var sql = string.Format(@"
        //Select Type, Blob
        //From dbo.[{0}]
        //	Inner Join @eventTypesXml.nodes(N'/r/e') _xml(item)
        //	On (AggregateEvent.Type = _xml.item.value(N'@type', N'nvarchar(500)'))
        //Order By AggregateId, EventSequence;", _tableName);
        //                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
        //                command.Parameters.AddRange(new[] {
        //                    new SqlParameter { ParameterName = "@eventTypesXml", SqlDbType = SqlDbType.Xml, Value = (eventTypesXml != null ? eventTypesXml.ToString() : string.Empty) } });
        //                connection.Open();
        //                var events = new List<Event>();
        //                using (var r = command.ExecuteReader())
        //                {
        //                    var ordinal = new EventOrdinal(r);
        //                    while (r.Read())
        //                        events.Add(MakeEvent(r, ordinal));
        //                }
        //                return events;
        //            }
        //        }

        public void SaveEvents(object aggregateId, IEnumerable<Event> events)
        {
            var eventsXml = new XElement("r", events
                .Select(x =>
                {
                    var eventType = x.GetType();
                    return new XElement("e",
                        new XAttribute("eventSequence", x.EventSequence),
                        new XAttribute("eventDate", x.EventDate),
                        new XAttribute("type", eventType.AssemblyQualifiedName),
                        _serializer.WriteObject(eventType, x));
                }));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Insert dbo.[{0}] (AggregateId, EventSequence, EventDate, Type, Blob)
Select @aggregateId, _xml.item.value(N'@eventSequence', N'int'), _xml.item.value(N'@eventDate', N'datetime'), _xml.item.value(N'@type', N'nvarchar(500)'), _xml.item.value(N'.', N'nvarchar(max)')
From @eventsXml.nodes(N'/r/e') _xml(item);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@aggregateId", SqlDbType = SqlDbType.NVarChar, Value = aggregateId },
                    new SqlParameter { ParameterName = "@eventsXml", SqlDbType = SqlDbType.Xml, Value = (eventsXml != null ? eventsXml.ToString() : string.Empty) } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveEvents(IEnumerable<AggregateTuple<IEnumerable<Event>>> events)
        {
            var eventsXml = ""; //new XElement("r", events
            //.Select(x =>
            //.Select(x =>
            //{
            //    var eventType = x.GetType();
            //    return new XElement("e",
            //        new XAttribute("eventSequence", x.EventSequence),
            //        new XAttribute("eventDate", x.EventDate),
            //        new XAttribute("type", eventType.AssemblyQualifiedName),
            //        _serializer.WriteObject(eventType, x));
            //}));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Insert dbo.[{0}] (AggregateId, EventSequence, EventDate, Type, Blob)
Select @aggregateId, _xml.item.value(N'@eventSequence', N'int'), _xml.item.value(N'@eventDate', N'datetime'), _xml.item.value(N'@type', N'nvarchar(500)'), _xml.item.value(N'.', N'nvarchar(max)')
From @eventsXml.nodes(N'/r/e') _xml(item);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@eventsXml", SqlDbType = SqlDbType.Xml, Value = (eventsXml != null ? eventsXml.ToString() : string.Empty) } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private Event MakeEvent(SqlDataReader r, EventOrdinal ordinal)
        {
            var type = Type.GetType(r.Field<string>(ordinal.Type));
            string blob = r.Field<string>(ordinal.Blob);
            return _serializer.ReadObject<Event>(type, blob);
        }
    }
}