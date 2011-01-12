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
    public interface IAggregateRootRepository<T>
        where T : AggregateRoot, new()
    {
        void Save(T aggregate, int expectedSequence);
        T GetById(Guid aggregateId);
    }

    /// <summary>
    /// AggregateRootRepository
    /// </summary>
    public class AggregateRootRepository<T> : IAggregateRootRepository<T>
        where T : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateRootSnapshotRepository _snapshotStore;

        public AggregateRootRepository(IEventStore eventStore, IAggregateRootSnapshotRepository snapshotStore)
        {
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
        }

        public void Save(T aggregate, int expectedSequence)
        {
            _eventStore.SaveEvents(aggregate.AggregateId, ((IAggregateRootBacking)aggregate).GetUncommittedChanges(), expectedSequence);
        }

        public T GetById(Guid aggregateId)
        {
            var aggregate = new T(); // { AggregateId = aggregateId };
            // find snapshot
            AggregateRootSnapshot snapshot = null;
            var snapshoter = (aggregate as ICanAggregateRootSnapshot);
            if ((snapshoter != null) && ((snapshot = _snapshotStore.GetSnapshot(aggregateId)) != null))
                snapshoter.LoadSnapshot(snapshot);
            var events = _eventStore.GetEventsForAggregate(aggregateId, (snapshot != null ? snapshot.LastSequence : 0));
            ((IAggregateRootBacking)aggregate).LoadFromHistory(events);
            return aggregate;
        }
    }
}