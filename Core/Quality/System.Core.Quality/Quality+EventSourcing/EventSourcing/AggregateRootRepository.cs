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
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
namespace System.Quality.EventSourcing
{
    /// <summary>
    /// IAggregateRootRepository
    /// </summary>
    public interface IAggregateRootRepository
    {
        TAggregateRoot GetById<TAggregateRoot>(Guid aggregateId)
            where TAggregateRoot : AggregateRoot, new();
        void Save(AggregateRoot aggregate);
    }

    /// <summary>
    /// AggregateRootRepository
    /// </summary>
    public class AggregateRootRepository : IAggregateRootRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateRootSnapshotStore _snapshotStore;
        private readonly Action<IEnumerable<Event>> _dispatcher;

        public AggregateRootRepository(IEventStore eventStore, IAggregateRootSnapshotStore snapshotStore)
            : this(eventStore, snapshotStore, null) { }
        public AggregateRootRepository(IEventStore eventStore, IAggregateRootSnapshotStore snapshotStore, Action<IEnumerable<Event>> dispatcher)
        {
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
            _dispatcher = dispatcher;
        }

        public TAggregateRoot GetById<TAggregateRoot>(Guid aggregateId)
             where TAggregateRoot : AggregateRoot, new()
        {
            var aggregate = new TAggregateRoot();
            // find snapshot
            AggregateRootSnapshot snapshot = null;
            var snapshoter = (aggregate as ICanAggregateRootSnapshot);
            if ((snapshoter != null) && ((snapshot = _snapshotStore.GetSnapshot(aggregateId)) != null))
                snapshoter.LoadSnapshot(snapshot);
            //
            var events = _eventStore.GetEventsForAggregate(aggregateId, (snapshot != null ? snapshot.LastEventSequence : 0));
            ((IAccessAggregateRootState)aggregate).LoadFromHistory(events);
            return aggregate;
        }

        public void Save(AggregateRoot aggregate)
        {
            var accessAggregateState = (IAccessAggregateRootState)aggregate;
            var events = accessAggregateState.GetUncommittedChanges();
            _eventStore.SaveEvents(aggregate.AggregateId, events);
            if (_dispatcher != null)
                _dispatcher(events);
            accessAggregateState.MarkChangesAsCommitted();
        }
    }
}