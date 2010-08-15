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
using NServiceBus;
using System.Reflection;

namespace System.Quality
{
    internal class MessageWrapper<TMessage>
        where TMessage : IServiceMessage
    {
        private static readonly MethodInfo s_a1 = typeof(IBus).GetGenericMethod("Publish", new[] { typeof(IMessage) }, new[] { typeof(Action<TMessage>) });
        private static readonly MethodInfo s_a2 = typeof(IBus).GetGenericMethod("Publish", new[] { typeof(IMessage) }, new[] { typeof(TMessage[]) });
        private static readonly MethodInfo s_a3 = typeof(IBus).GetGenericMethod("Reply", new[] { typeof(IMessage) }, new[] { typeof(Action<TMessage>) });
        private static readonly MethodInfo s_a4 = typeof(IBus).GetGenericMethod("Send", new[] { typeof(IMessage) }, new[] { typeof(Action<TMessage>) });
        private static readonly MethodInfo s_a5 = typeof(IBus).GetGenericMethod("Send", new[] { typeof(IMessage) }, new[] { typeof(string), typeof(Action<TMessage>) });
        private static readonly MethodInfo s_a6 = typeof(IBus).GetGenericMethod("SendLocal", new[] { typeof(IMessage) }, new[] { typeof(Action<TMessage>) });
        private static readonly MethodInfo s_a7 = typeof(IBus).GetGenericMethod("Subscribe", new[] { typeof(IMessage) }, null);
        private static readonly MethodInfo s_a8 = typeof(IBus).GetGenericMethod("Subscribe", new[] { typeof(IMessage) }, new[] { typeof(Predicate<TMessage>) });
        private static readonly MethodInfo s_a9 = typeof(IBus).GetGenericMethod("Unsubscribe", new[] { typeof(IMessage) }, null);

        //private static readonly Type s_wrappedType = new DynamicProxyBuilder().CreateProxiedType(typeof(TMessage), new[] { typeof(IMessage) });
        //public static TBase Get(Type type) { return (TBase)s_getMethodInfo.MakeGenericMethod(type, DefaultAppUnit.Type).Invoke(null, null); }

        public static void Publish(IBus bus, Action<TMessage> messageBuilder) { bus.Publish<IMessage>(Wrap(messageBuilder)); }
        public static void Publish(IBus bus, TMessage[] messages) { bus.Publish<IMessage>(Wrap(messages)); }
        public static void Reply(IBus bus, Action<TMessage> messageBuilder) { bus.Reply<IMessage>(Wrap(messageBuilder)); }
        public static IServiceBusCallback Send(IBus bus, Action<TMessage> messageBuilder) { return MessageWrapper.Wrap(bus.Send<IMessage>(Wrap(messageBuilder))); }
        public static IServiceBusCallback Send(IBus bus, string destination, Action<TMessage> messageBuilder) { return MessageWrapper.Wrap(bus.Send<IMessage>(destination, Wrap(messageBuilder))); }
        public static void SendLocal(IBus bus, Action<TMessage> messageBuilder) { bus.SendLocal<IMessage>(Wrap(messageBuilder)); }
        public static void Subscribe(IBus bus) { bus.Subscribe<IMessage>(); }
        public static void Subscribe(IBus bus, Predicate<TMessage> predicate) { bus.Subscribe<IMessage>(Wrap(predicate)); }
        public static void Unsubscribe(IBus bus) { bus.Unsubscribe<IMessage>(); }

        public static Action<IMessage> Wrap(Action<TMessage> messageBuilder)
        {
            return (c => messageBuilder(default(TMessage)));
        }

        private static Predicate<IMessage> Wrap(Predicate<TMessage> condition)
        {
            return (c => condition(default(TMessage)));
        }

        public static IMessage[] Wrap(TMessage[] messages)
        {
            return messages.Select(c => default(IMessage))
                .ToArray();
        }
    }
}
