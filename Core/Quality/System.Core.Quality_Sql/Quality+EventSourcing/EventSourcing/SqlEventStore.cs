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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;
namespace System.Quality.EventSourcing
{
    public class SqlEventStore : IBatchedEventStore
    {
        internal readonly string _connectionString;
        internal readonly string _tableName;
        internal readonly Func<string, object> _makeAggregateId;
        private readonly ISerializer _serializer;

        protected class EventOrdinal
        {
            public int AggregateId, Type, Blob;
            public EventOrdinal(IDataReader r, bool hasAggregateId)
            {
                if (hasAggregateId)
                    AggregateId = r.GetOrdinal("AggregateId");
                Type = r.GetOrdinal("Type");
                Blob = r.GetOrdinal("Blob");
            }
        }

        public SqlEventStore(string connectionString)
            : this(connectionString, "AggregateEvent", null, new JsonSerializer()) { }
        public SqlEventStore(string connectionString, string tableName)
            : this(connectionString, tableName, null, new JsonSerializer()) { }
        public SqlEventStore(string connectionString, Func<string, object> makeAggregateId)
            : this(connectionString, "AggregateEvent", makeAggregateId, new JsonSerializer()) { }
        public SqlEventStore(string connectionString, string tableName, Func<string, object> makeAggregateId)
            : this(connectionString, tableName, makeAggregateId, new JsonSerializer()) { }
        public SqlEventStore(string connectionString, string tableName, Func<string, object> makeAggregateId, ISerializer serializer)
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

        public IEnumerable<Event> GetEventsById(object aggregateId, int startSequence)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select Type, Blob
From dbo.[{0}]
	Where (AggregateId = @id)
	And (EventSequence > @sequence)
Order By EventSequence;", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.NVarChar, Value = aggregateId },
                    new SqlParameter { ParameterName = "@sequence", SqlDbType = SqlDbType.Int, Value = startSequence } });
                connection.Open();
                var events = new List<Event>();
                using (var r = command.ExecuteReader())
                {
                    var ordinal = new EventOrdinal(r, false);
                    while (r.Read())
                        events.Add(MakeEvent(r, ordinal));
                }
                return events;
            }
        }

        public IEnumerable<AggregateTuple<IEnumerable<Event>>> GetEventsByIds(IEnumerable<AggregateTuple<int>> aggregateIds)
        {
            var xml = new XElement("r", aggregateIds
                .Select(x => new XElement("i",
                    new XAttribute("i", x.AggregateId),
                    new XAttribute("s", x.Item1)))
                );
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Select AggregateId, Type, Blob
From dbo.[{0}]
    Inner Join @xml.nodes(N'/r/i') _xml(item)
    On (AggregateId = _xml.item.value(N'@i', N'nvarchar(100)'))
    And (EventSequence > _xml.item.value(N'@s', N'int'))
Order By AggregateId, EventSequence;", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@xml", SqlDbType = SqlDbType.Xml, Value = (xml != null ? xml.ToString() : string.Empty) } });
                connection.Open();
                var events = new List<AggregateTuple<IEnumerable<Event>>>();
                using (var r = command.ExecuteReader())
                {
                    var eventGroup = new List<Event>();
                    string lastAggregateId = null;
                    var ordinal = new EventOrdinal(r, true);
                    while (r.Read())
                    {
                        var aggregateId = r.Field<string>(ordinal.AggregateId);
                        if (lastAggregateId != aggregateId)
                        {
                            events.Add(new AggregateTuple<IEnumerable<Event>>
                            {
                                AggregateId = lastAggregateId,
                                Item1 = eventGroup,
                            });
                            lastAggregateId = aggregateId;
                            eventGroup = new List<Event>();
                        }
                        eventGroup.Add(MakeEvent(r, ordinal));
                    }
                    if (eventGroup.Count > 0)
                        events.Add(new AggregateTuple<IEnumerable<Event>>
                        {
                            AggregateId = lastAggregateId,
                            Item1 = eventGroup,
                        });
                }
                return events;
            }
        }

        public void SaveEvents(object aggregateId, IEnumerable<Event> events)
        {
            var xml = new XElement("r", events
                .Select(x =>
                {
                    var eventType = x.GetType();
                    return new XElement("e",
                        new XAttribute("s", x.EventSequence),
                        new XAttribute("d", x.EventDate),
                        new XAttribute("t", eventType.AssemblyQualifiedName),
                        _serializer.WriteObject(eventType, x));
                }));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Insert dbo.[{0}] (AggregateId, EventSequence, EventDate, Type, Blob)
Select @id, _xml.item.value(N'@s', N'int'), _xml.item.value(N'@d', N'datetime'), _xml.item.value(N'@t', N'nvarchar(500)'), _xml.item.value(N'.', N'nvarchar(max)')
From @xml.nodes(N'/r/e') _xml(item);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.NVarChar, Value = aggregateId },
                    new SqlParameter { ParameterName = "@xml", SqlDbType = SqlDbType.Xml, Value = (xml != null ? xml.ToString() : string.Empty) } });
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveEvents(IEnumerable<AggregateTuple<IEnumerable<Event>>> events)
        {
            var xml = new XElement("r", events
                .SelectMany(x => x.Item1, (a, x) => new { a, x })
                .Select(m =>
                {
                    var x = m.x;
                    var eventType = x.GetType();
                    return new XElement("e",
                        new XAttribute("i", m.a),
                        new XAttribute("s", x.EventSequence),
                        new XAttribute("d", x.EventDate),
                        new XAttribute("t", eventType.AssemblyQualifiedName),
                        _serializer.WriteObject(eventType, x));
                }));
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = string.Format(@"
Insert dbo.[{0}] (AggregateId, EventSequence, EventDate, Type, Blob)
Select _xml.item.value(N'@i', N'nvarchar(100)'), _xml.item.value(N'@s', N'int'), _xml.item.value(N'@d', N'datetime'), _xml.item.value(N'@t', N'nvarchar(500)'), _xml.item.value(N'.', N'nvarchar(max)')
From @xml.nodes(N'/r/e') _xml(item);", _tableName);
                var command = new SqlCommand(sql, connection) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[] {
                    new SqlParameter { ParameterName = "@xml", SqlDbType = SqlDbType.Xml, Value = (xml != null ? xml.ToString() : string.Empty) } });
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
    }
}