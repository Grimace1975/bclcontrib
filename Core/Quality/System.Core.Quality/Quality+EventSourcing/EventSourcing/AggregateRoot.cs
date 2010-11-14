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
    public abstract class AggregateRoot
    {
        private readonly IDictionary<Type, Action<Event>> _handlerRegistry = new Dictionary<Type, Action<Event>>();
        private readonly ICollection<Event> _changes = new Collection<Event>();

        public abstract class Event { }

        //public delegate void AppliesEvent<TEvent>(TEvent @event)
        //    where TEvent : Event;

        protected void RegisterHandler<TEvent>(Action<TEvent> handler)
           where TEvent : Event
        {
            var castHandler = DelegateAdjuster.CastArgument<Event, TEvent>(e => handler(e));
            _handlerRegistry.Add(typeof(TEvent), castHandler);
        }

        protected void ApplyEvent(Event @event, bool trackAsChange)
        {
            Action<Event> handler;
            if (!_handlerRegistry.TryGetValue(@event.GetType(), out handler))
                throw new InvalidOperationException();
            handler(@event);
            if (trackAsChange)
                _changes.Add(@event);
        }

        public void LoadFromHistory(IEnumerable<Event> events)
        {
            foreach (var @event in events)
                ApplyEvent(@event, false);
        }

        public IEnumerable<Event> GetChanges()
        {
            return _changes;
        }
    }

    public class DelegateAdjuster
    {
        public static Action<TBase> CastArgument<TBase, TDerived>(Expression<Action<TDerived>> source)
            where TDerived : TBase
        {
            if (typeof(TDerived) == typeof(TBase))
                return (Action<TBase>)((Delegate)source.Compile());
            var sourceParameter = Expression.Parameter(typeof(TBase), "source");
            var result = Expression.Lambda<Action<TBase>>(
                Expression.Invoke(
                    source,
                    Expression.Convert(sourceParameter, typeof(TDerived))),
                sourceParameter);
            return result.Compile();
        }
    }
}