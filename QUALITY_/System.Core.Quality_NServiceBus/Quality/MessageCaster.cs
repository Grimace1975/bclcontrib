//#region License
///*
//The MIT License

//Copyright (c) 2008 Sky Morey

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.
//*/
//#endregion
//using System.Linq;
//using System.Reflection;
//using NServiceBus;
//namespace System.Quality
//{
//    internal class MessageCaster
//    {
//        public static readonly MethodInfo PublishMessageBuilderMethod = typeof(IBus).GetGenericMethod("Publish", new[] { typeof(IMessage) }, new[] { typeof(Action<IMessage>) });
//        public static readonly MethodInfo PublishMessagesMethod = typeof(IBus).GetGenericMethod("Publish", new[] { typeof(IMessage) }, new[] { typeof(IMessage[]) });
//        public static readonly MethodInfo ReplyMessageBuilderMethod = typeof(IBus).GetGenericMethod("Reply", new[] { typeof(IMessage) }, new[] { typeof(Action<IMessage>) });
//        public static readonly MethodInfo SendMessageBuilderMethod = typeof(IBus).GetGenericMethod("Send", new[] { typeof(IMessage) }, new[] { typeof(Action<IMessage>) });
//        public static readonly MethodInfo SendMessagesMethod = typeof(IBus).GetGenericMethod("Send", new[] { typeof(IMessage) }, new[] { typeof(string), typeof(Action<IMessage>) });
//        public static readonly MethodInfo SendLocalMessageBuilderMethod = typeof(IBus).GetGenericMethod("SendLocal", new[] { typeof(IMessage) }, new[] { typeof(Action<IMessage>) });
//        public static readonly MethodInfo SubscribeMethod = typeof(IBus).GetGenericMethod("Subscribe", new[] { typeof(IMessage) }, null);
//        public static readonly MethodInfo SubscribeConditionMethod = typeof(IBus).GetGenericMethod("Subscribe", new[] { typeof(IMessage) }, new[] { typeof(Predicate<IMessage>) });
//        public static readonly MethodInfo UnsubscribeMethod = typeof(IBus).GetGenericMethod("Unsubscribe", new[] { typeof(IMessage) }, null);

//        public class CallbackWrapper : IServiceBusCallback
//        {
//            private readonly ICallback _callback;

//            public CallbackWrapper(ICallback callback)
//            {
//                _callback = callback;
//            }

//            public void Register<T>(Action<T> callback) { _callback.Register(callback); }
//            public IAsyncResult Register(AsyncCallback callback, object state) { return _callback.Register(callback, state); }
//        }

//        public static Type Cast(Type messageType)
//        {
//            return messageType;
//        }

//        public static Predicate<IMessage> Cast(Predicate<IServiceMessage> condition)
//        {
//            return (c => condition((INServiceMessage)c));
//        }

//        public static IMessage[] Cast(IServiceMessage[] messages)
//        {
//            return messages.Cast<INServiceMessage>().ToArray();
//        }

//        public static IServiceBusCallback Cast(ICallback callback)
//        {
//            return new CallbackWrapper(callback);
//        }
//    }
//}
