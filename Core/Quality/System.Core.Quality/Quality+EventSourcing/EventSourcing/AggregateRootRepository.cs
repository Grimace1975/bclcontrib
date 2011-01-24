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
using System.Linq.Expressions;
using System.Collections.ObjectModel;
namespace System.Quality.EventSourcing
{
    /// <summary>
    /// IAggregateRootRepository
    /// </summary>
    public interface IAggregateRootRepository
    {
        TAggregateRoot GetById<TAggregateRoot>(object aggregateId, AggregateRootQueryOptions queryOptions)
            where TAggregateRoot : AggregateRoot;
        IEnumerable<TAggregateRoot> GetManyByIds<TAggregateRoot>(IEnumerable<object> aggregateIds, AggregateRootQueryOptions queryOptions)
            where TAggregateRoot : AggregateRoot;
        IEnumerable<Event> GetEventsById(object aggregateId);
        void Save(AggregateRoot aggregate);
    }

    public static class IAggregateRootRepositoryExtensions
    {
        public static TAggregateRoot GetById<TAggregateRoot>(this IAggregateRootRepository repository, object aggregateId)
            where TAggregateRoot : AggregateRoot { return repository.GetById<TAggregateRoot>(aggregateId, 0); }
    }

    /// <summary>
    /// AggregateRootRepository
    /// </summary>
    public class AggregateRootRepository : IAggregateRootRepository
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateRootSnapshotStore _snapshotStore;
        private readonly Action<IEnumerable<Event>> _eventDispatcher;
        private readonly Func<Type, AggregateRoot> _factory;

        public static class DefaultFactory
        {
            public static Func<Type, AggregateRoot> Factory = (t => (AggregateRoot)Activator.CreateInstance(t));
        }

        public AggregateRootRepository(IEventStore eventStore, IAggregateRootSnapshotStore snapshotStore)
            : this(eventStore, snapshotStore, null, DefaultFactory.Factory) { }
        public AggregateRootRepository(IEventStore eventStore, IAggregateRootSnapshotStore snapshotStore, Action<IEnumerable<Event>> eventDispatcher)
            : this(eventStore, snapshotStore, eventDispatcher, DefaultFactory.Factory) { }
        public AggregateRootRepository(IEventStore eventStore, IAggregateRootSnapshotStore snapshotStore, Action<IEnumerable<Event>> eventDispatcher, Func<Type, AggregateRoot> factory)
        {
            if (eventStore == null)
                throw new ArgumentNullException("eventStore");
            _eventStore = eventStore;
            _snapshotStore = snapshotStore;
            _eventDispatcher = eventDispatcher;
            _factory = (factory ?? DefaultFactory.Factory);
        }

        public IEnumerable<Event> GetEventsById(object aggregateId)
        {
            return _eventStore.GetEventsForAggregate(aggregateId, 0);
        }

        public TAggregateRoot GetById<TAggregateRoot>(object aggregateId, AggregateRootQueryOptions queryOptions)
             where TAggregateRoot : AggregateRoot
        {
            if (aggregateId == null)
                throw new ArgumentNullException("aggreaggregateIdgateIds");
            var aggregate = (_factory(typeof(TAggregateRoot)) as TAggregateRoot);
            if (aggregate == null)
                throw new InvalidOperationException("Factory");
            // find snapshot
            bool loaded = false;
            AggregateRootSnapshot snapshot = null;
            if (_snapshotStore != null)
            {
                var snapshoter = (aggregate as ICanAggregateRootSnapshot);
                if ((snapshoter != null) && ((snapshot = _snapshotStore.GetLatestSnapshot<TAggregateRoot>(aggregateId)) != null))
                {
                    loaded = true;
                    snapshoter.LoadSnapshot(snapshot);
                }
            }
            // load events
            var events = _eventStore.GetEventsForAggregate(aggregateId, (snapshot != null ? snapshot.LastEventSequence : 0));
            loaded |= ((IAccessAggregateRootState)aggregate).LoadFromHistory(events);
            return ((queryOptions & AggregateRootQueryOptions.UseNullAggregates) == 0 ? aggregate : (loaded ? aggregate : null));
        }

        public IEnumerable<TAggregateRoot> GetManyByIds<TAggregateRoot>(IEnumerable<object> aggregateIds, AggregateRootQueryOptions queryOptions)
            where TAggregateRoot : AggregateRoot
        {
            if (aggregateIds == null)
                throw new ArgumentNullException("aggregateIds");
            return aggregateIds.Select(x => GetById<TAggregateRoot>(x, queryOptions)).ToList();
        }

        public void Save(AggregateRoot aggregate)
        {
            var accessAggregateState = (IAccessAggregateRootState)aggregate;
            var events = accessAggregateState.GetUncommittedChanges();
            _eventStore.SaveEvents(aggregate.AggregateId, events);
            if (_eventDispatcher != null)
                _eventDispatcher(events);
            accessAggregateState.MarkChangesAsCommitted();
            // make snapshot
            if (_snapshotStore != null)
            {
                var snapshoter = (aggregate as ICanAggregateRootSnapshot);
                if ((snapshoter != null) && (_snapshotStore.ShouldSnapshot(aggregate)))
                    _snapshotStore.SaveSnapshot(snapshoter.GetSnapshot());
            }
        }
    }
}