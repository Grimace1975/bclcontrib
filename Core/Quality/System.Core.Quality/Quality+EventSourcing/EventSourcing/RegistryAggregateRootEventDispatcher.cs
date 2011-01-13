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
    /// RegistryAggregateRootEventDispatcher
    /// </summary>
    public class RegistryAggregateRootEventDispatcher : IAggregateRootEventDispatcher
    {
        private readonly IDictionary<Type, Action<Event>> _handlerRegistry = new Dictionary<Type, Action<Event>>();

        public RegistryAggregateRootEventDispatcher() { }
        public RegistryAggregateRootEventDispatcher(Type aggregateType)
        {
            RegisterByConvention(aggregateType);
        }

        public void RegisterByConvention(Type aggregateType)
        {
            var eventBaseType = typeof(Event);
            var handlerInfos = aggregateType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => !x.IsGenericMethod && (x.Name == "HandleEvent"))
                .Select(methodInfo =>
                {
                    var parameters = methodInfo.GetParameters();
                    Type eventType;
                    if ((parameters.Length != 1) || !(eventType = parameters[0].ParameterType).IsSubclassOf(eventBaseType))
                        return null;
                    return new { EventType = eventType, Handler = Delegate.CreateDelegate(typeof(Action<Event>), methodInfo) };
                })
                .Where(x => x != null);
            foreach (var handlerInfo in handlerInfos)
                RegisterHandler(handlerInfo.EventType, (Action<Event>)handlerInfo.Handler);
        }

        public void RegisterHandler<TEvent>(Action<TEvent> handler)
           where TEvent : Event { var castHandler = ExpressionEx.CastArgument<Event, TEvent>(e => handler(e)); RegisterHandler(typeof(TEvent), castHandler); }
        public void RegisterHandler(Type eventType, Action<Event> handler)
        {
            _handlerRegistry.Add(eventType, handler);
        }

        public void ApplyEvent(AggregateRoot aggregate, Event e)
        {
            Action<Event> handler;
            if (_handlerRegistry.TryGetValue(e.GetType(), out handler))
                handler(e);
        }
    }
}