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
using System.Threading;
namespace System.Quality.EventSourcing
{
    /// <summary>
    /// AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAccessAggregateRootState
    {
        private readonly List<Event> _changes = new List<Event>();
        private readonly IAggregateRootEventDispatcher _dispatcher;
        private bool _useStorageSequencing;

        public AggregateRoot()
            : this(new RegistryAggregateRootEventDispatcher()) { }
        public AggregateRoot(Type aggregateType)
            : this(new RegistryAggregateRootEventDispatcher(aggregateType)) { }
        public AggregateRoot(IAggregateRootEventDispatcher dispatcher)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");
            _dispatcher = dispatcher;
        }

        public object AggregateId { get; protected set; }
        protected internal int LastEventSequence { get; private set; }

        protected IAggregateRootEventDispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        protected void ApplyEvent(Event e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            e.AggregateId = AggregateId;
            e.EventDate = DateTime.Now;
            if (!_useStorageSequencing)
                e.Sequence = ++LastEventSequence;
            _dispatcher.ApplyEvent(this, e);
            _changes.Add(e); // trackAsChange
        }

        #region Access State

        void IAccessAggregateRootState.LoadFromHistory(IEnumerable<Event> events)
        {
            if (events == null)
                throw new ArgumentNullException("events");
            int? lastEventSequence = 0;
            foreach (var e in events.OrderBy(x => x.Sequence))
            {
                _dispatcher.ApplyEvent(this, e);
                lastEventSequence = e.Sequence;
            }
            LastEventSequence = (int)lastEventSequence;
        }

        IEnumerable<Event> IAccessAggregateRootState.GetUncommittedChanges()
        {
            return _changes;
        }

        void IAccessAggregateRootState.MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        #endregion
    }
}