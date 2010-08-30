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
        private static readonly Type s_wrappedType = new DynamicProxyBuilder().CreateProxiedType(typeof(TMessage), new[] { typeof(INServiceMessage) });
        private static readonly MethodInfo s_publishMessageBuilderMethod = NServiceBusHelper.SPublishMessageBuilderMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_publishMessagesMethod = NServiceBusHelper.SPublishMessagesMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_replyMessageBuilderMethod = NServiceBusHelper.SReplyMessageBuilderMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_sendMessageBuilderMethod = NServiceBusHelper.SSendMessageBuilderMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_sendMessagesMethod = NServiceBusHelper.SSendMessagesMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_sendLocalMessageBuilderMethod = NServiceBusHelper.SSendLocalMessageBuilderMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_subscribeMethod = NServiceBusHelper.SSubscribeMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_subscribeConditionMethod = NServiceBusHelper.SSubscribeConditionMethod.MakeGenericMethod(s_wrappedType);
        private static readonly MethodInfo s_unsubscribeMethod = NServiceBusHelper.SUnsubscribeMethod.MakeGenericMethod(s_wrappedType);

        public static void Publish(IBus bus, Action<TMessage> messageBuilder) { s_publishMessageBuilderMethod.Invoke(bus, new object[] { Wrap(messageBuilder) }); }
        public static void Publish(IBus bus, TMessage[] messages) { s_publishMessagesMethod.Invoke(bus, new object[] { Wrap(messages) }); }
        public static void Reply(IBus bus, Action<TMessage> messageBuilder) { s_replyMessageBuilderMethod.Invoke(bus, new object[] { Wrap(messageBuilder) }); }
        public static IServiceBusCallback Send(IBus bus, Action<TMessage> messageBuilder) { return MessageWrapper.Wrap((ICallback)s_sendMessageBuilderMethod.Invoke(bus, new object[] { Wrap(messageBuilder) })); }
        public static IServiceBusCallback Send(IBus bus, string destination, Action<TMessage> messageBuilder) { return MessageWrapper.Wrap((ICallback)s_sendMessagesMethod.Invoke(bus, new object[] { destination, Wrap(messageBuilder) })); }
        public static void SendLocal(IBus bus, Action<TMessage> messageBuilder) { s_sendLocalMessageBuilderMethod.Invoke(bus, new object[] { Wrap(messageBuilder) }); }
        public static void Subscribe(IBus bus) { s_subscribeMethod.Invoke(bus, null); }
        public static void Subscribe(IBus bus, Predicate<TMessage> condition) { s_subscribeConditionMethod.Invoke(bus, new object[] { Wrap(condition) }); }
        public static void Unsubscribe(IBus bus) { s_unsubscribeMethod.Invoke(bus, null); }

        public static Action<IMessage> Wrap(Action<TMessage> messageBuilder)
        {
            return (c => messageBuilder((TMessage)((object)c)));
        }

        private static Predicate<IMessage> Wrap(Predicate<TMessage> condition)
        {
            return (c => condition((TMessage)((object)c)));
        }

        public static IMessage[] Wrap(TMessage[] messages)
        {
            return messages.Cast<INServiceMessage>().ToArray();
        }

        public static TMessage MakeMessage()
        {
            return (TMessage)Activator.CreateInstance(s_wrappedType);
        }
    }
}
