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
using System;
using NServiceBus;
using System.Reflection;

namespace System.Quality
{
    internal class NServiceBusHelper
    {
        private abstract class TypeForQuery
        {
            public static readonly Type MessagesType = typeof(TypeForQuery).GetTypeForQuery("Messages"); // typeof(Action<TMessage>)
            public static readonly Type MessageBuilderType = typeof(TypeForQuery).GetTypeForQuery("MessageBuilder"); // typeof(Action<TMessage>)
            public static readonly Type ConditionType = typeof(TypeForQuery).GetTypeForQuery("Condition"); // typeof(Action<TMessage>)
            public abstract T[] Messages<T>() where T : IMessage;
            public abstract Action<T> MessageBuilder<T>() where T : IMessage;
            public abstract Predicate<T> Condition<T>() where T : IMessage;
        }

        public static readonly MethodInfo SPublishMessageBuilderMethod = typeof(IBus).GetGenericMethod("Publish", new[] { typeof(IMessage) }, new[] { TypeForQuery.MessageBuilderType });
        public static readonly MethodInfo SPublishMessagesMethod = typeof(IBus).GetGenericMethod("Publish", new[] { typeof(IMessage) }, new[] { TypeForQuery.MessagesType });
        public static readonly MethodInfo SReplyMessageBuilderMethod = typeof(IBus).GetGenericMethod("Reply", new[] { typeof(IMessage) }, new[] { TypeForQuery.MessageBuilderType });
        public static readonly MethodInfo SSendMessageBuilderMethod = typeof(IBus).GetGenericMethod("Send", new[] { typeof(IMessage) }, new[] { TypeForQuery.MessageBuilderType });
        public static readonly MethodInfo SSendMessagesMethod = typeof(IBus).GetGenericMethod("Send", new[] { typeof(IMessage) }, new[] { typeof(string), TypeForQuery.MessageBuilderType });
        public static readonly MethodInfo SSendLocalMessageBuilderMethod = typeof(IBus).GetGenericMethod("SendLocal", new[] { typeof(IMessage) }, new[] { TypeForQuery.MessageBuilderType });
        public static readonly MethodInfo SSubscribeMethod = typeof(IBus).GetGenericMethod("Subscribe", new[] { typeof(IMessage) }, null);
        public static readonly MethodInfo SSubscribeConditionMethod = typeof(IBus).GetGenericMethod("Subscribe", new[] { typeof(IMessage) }, new[] { TypeForQuery.ConditionType });
        public static readonly MethodInfo SUnsubscribeMethod = typeof(IBus).GetGenericMethod("Unsubscribe", new[] { typeof(IMessage) }, null);
    }
}
