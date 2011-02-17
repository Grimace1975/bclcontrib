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
using System.Reflection;
namespace System.Quality.EventSourcing
{
    /// <summary>
    /// RegistryEventDispatcher
    /// </summary>
    public class RegistryEventDispatcher : IAggregateRootEventDispatcher
    {
        private readonly IDictionary<Type, Action<Event>> _handlerRegistry = new Dictionary<Type, Action<Event>>();

        public RegistryEventDispatcher() { }
        public RegistryEventDispatcher(AggregateRoot aggregate)
        {
            RegisterByConvention(aggregate);
        }

        private class HandlerPair
        {
            public Type EventType;
            public Action<Event> Handler;
        }

        public void RegisterByConvention(AggregateRoot aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException("aggregate");
            var eventType = typeof(Event);
            var actionType = typeof(Action<>);
            //var actionEventType = typeof(Action<Event>);
            //var covariantCastMethod = typeof(ExpressionEx).GetMethod("CovariantCast");
            var handlerInfos = aggregate.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => !x.IsGenericMethod && (x.Name == "HandleEvent"))
                .Select(methodInfo =>
                {
                    var parameters = methodInfo.GetParameters();
                    Type derivedEventType;
                    if ((parameters.Length != 1) || !(derivedEventType = parameters[0].ParameterType).IsSubclassOf(eventType))
                        return null;
                    return new HandlerPair { EventType = derivedEventType, Handler = ((Event e) => methodInfo.Invoke(aggregate, new[] { e })) };
                    //var derivedActionDelegate = Delegate.CreateDelegate(actionType.MakeGenericType(derivedEventType), aggregate, methodInfo);
                    //var actionEventDelegate = (Action<Event>)Delegate.CreateDelegate(actionEventType, covariantCastMethod.MakeGenericMethod(eventType, derivedEventType));
                    //return new HandlerPair { EventType = derivedEventType, Handler = actionEventDelegate };
                })
                .Where(x => x != null);
            foreach (var handlerInfo in handlerInfos)
                RegisterHandler(handlerInfo.EventType, (Action<Event>)handlerInfo.Handler);
        }

        public void RegisterHandler<TEvent>(Action<TEvent> handler)
           where TEvent : Event { var castHandler = ExpressionEx.CovariantCast<Event, TEvent>(e => handler(e)); RegisterHandler(typeof(TEvent), castHandler); }
        public void RegisterHandler(Type eventType, Action<Event> handler)
        {
            _handlerRegistry.Add(eventType, handler);
        }

        public IEnumerable<Type> GetEventTypes()
        {
            return _handlerRegistry.Keys;
        }

        public void ApplyEvent(AggregateRoot aggregate, Event e)
        {
            Action<Event> handler;
            if (_handlerRegistry.TryGetValue(e.GetType(), out handler))
                handler(e);
        }
    }
}