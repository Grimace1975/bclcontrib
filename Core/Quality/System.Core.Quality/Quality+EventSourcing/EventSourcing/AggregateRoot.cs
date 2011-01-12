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
    /// AggregateRoot
    /// </summary>
    public abstract class AggregateRoot : IAccessAggregateRootState
    {
        private readonly List<Event> _changes = new List<Event>();
        //private readonly bool _strictEvents;

        public abstract Guid AggregateId { get; }
        protected void ApplyEvent(Event @event) { ApplyEvent(@event, true); }

        #region Access State

        void IAccessAggregateRootState.LoadFromHistory(IEnumerable<Event> events)
        {
            foreach (var e in events)
                ApplyEvent(e, false);
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

        #region Handlers
        private readonly IDictionary<Type, Action<Event>> _handlerRegistry = new Dictionary<Type, Action<Event>>();

        // Event Handlers
        private void RegisterHandlersByConvention()
        {
            //this.GetType().GetMethods("HandleEvent");
        }

        protected void RegisterHandler<TEvent>(Action<TEvent> handler)
           where TEvent : Event
        {
            var castHandler = ExpressionEx.CastArgument<Event, TEvent>(e => handler(e));
            _handlerRegistry.Add(typeof(TEvent), castHandler);
        }

        private void ApplyEvent(Event @event, bool trackAsChange)
        {
            Action<Event> handler;
            if (_handlerRegistry.TryGetValue(@event.GetType(), out handler))
                handler(@event);
            //else if (_strictEvents)
            //    throw new InvalidOperationException();
            if (trackAsChange)
                _changes.Add(@event);
        }
        #endregion
    }
}