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
using NServiceBus;
namespace System.Quality
{
    /// <summary>
    /// INServiceBus
    /// </summary>
    public interface INServiceBus : IPublishingServiceBus
    {
        void Reply<TMessage>(Action<TMessage> messageBuilder) where TMessage : IServiceMessage;
        void Reply(params IServiceMessage[] messages);
        void Return<T>(T value);
        IBus Bus { get; }
    }

    /// <summary>
    /// NServiceBusAbstractor
    /// </summary>
    public class NServiceBusAbstractor : INServiceBus
    {
        private static readonly Type s_domainServiceMessageType = typeof(INServiceMessage);

        public NServiceBusAbstractor()
            : this(new global::NServiceBus.Unicast.UnicastBus()) { }
        //public NServiceBusAbstractor()
        //    : this(Configure.With()
        //        .CreateBus()
        //        .Start()) { }
        public NServiceBusAbstractor(Func<IBus> busBuilder)
            : this(busBuilder()) { }
        public NServiceBusAbstractor(IBus bus)
        {
            if (bus == null)
                throw new ArgumentNullException("bus", "The specified NServiceBus bus cannot be null.");
            Bus = bus;
        }

        public TMessage MakeMessage<TMessage>()
            where TMessage : IServiceMessage, new()
        {
            return MessageWrapper<TMessage>.MakeMessage();
        }

        public void Send<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.SendLocal(Bus, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Send(params IServiceMessage[] messages)
        {
            try
            {
                Bus.SendLocal(MessageWrapper.Wrap(messages));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public IServiceBusCallback SendTo<TMessage>(string destination, Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                if (destination == null)
                    return MessageWrapper<TMessage>.Send(Bus, messageBuilder);
                return MessageWrapper<TMessage>.Send(Bus, destination, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public IServiceBusCallback SendTo(string destination, params IServiceMessage[] messages)
        {
            try
            {
                if (destination == null)
                    return MessageWrapper.Wrap(Bus.Send(MessageWrapper.Wrap(messages)));
                return MessageWrapper.Wrap(Bus.Send(destination, MessageWrapper.Wrap(messages)));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        #region Publishing ServiceBus

        public void Publish<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.Publish(Bus, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Publish<TMessage>(params TMessage[] messages)
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.Publish(Bus, messages);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Subscribe<TMessage>()
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.Subscribe(Bus);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Subscribe<TMessage>(Predicate<TMessage> condition)
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.Subscribe(Bus, condition);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Subscribe(Type messageType)
        {
            try
            {
                Bus.Subscribe(MessageWrapper.Wrap(messageType));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Subscribe(Type messageType, Predicate<IServiceMessage> condition)
        {
            try
            {
                Bus.Subscribe(MessageWrapper.Wrap(messageType), MessageWrapper.Wrap(condition));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Unsubscribe<TMessage>()
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.Unsubscribe(Bus);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Unsubscribe(Type messageType)
        {
            try
            {
                Bus.Unsubscribe(MessageWrapper.Wrap(messageType));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }
        #endregion

        #region Domain-specific

        public IBus Bus { get; private set; }

        public void Reply<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            if (!typeof(TMessage).IsAssignableFrom(s_domainServiceMessageType))
                throw new ArgumentException("TMessage");
            try
            {
                MessageWrapper<TMessage>.Reply(Bus, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Reply(params IServiceMessage[] messages)
        {
            try
            {
                Bus.Reply(MessageWrapper.Wrap(messages));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Return<T>(T value)
        {
            if (typeof(T) != typeof(int))
                throw new NotSupportedException();
            try
            {
                Bus.Return(Convert.ToInt32(value));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        #endregion
    }
}
