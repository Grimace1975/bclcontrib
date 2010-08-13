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

namespace System.Quality
{
    /// <summary>
    /// INServiceBusServiceBus
    /// </summary>
    public interface INServiceBusServiceBus : IServiceBus
    {
        IBus Bus { get; }
    }

    /// <summary>
    /// NServiceBusServiceBus
    /// </summary>
    public class NServiceBusServiceBus : INServiceBusServiceBus
    {
        public NServiceBusServiceBus()
            : this((IBus)null) { }

        public NServiceBusServiceBus(Func<IBus> busBuilder)
            : this(busBuilder()) { }

        public NServiceBusServiceBus(IBus bus)
        {
            if (bus == null)
                throw new ArgumentNullException("bus", "The specified NServiceBus bus cannot be null.");
            Bus = bus;
        }

        public IBus Bus { get; private set; }

        public void Publish<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            try
            {
                MessageWrapper<TMessage>.Publish(Bus, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Publish<TMessage>(params TMessage[] messages)
            where TMessage : IServiceMessage
        {
            try
            {
                MessageWrapper<TMessage>.Publish(Bus, messages);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Reply<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
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

        public IServiceBusCallback Send<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            try
            {
                return MessageWrapper<TMessage>.Send(Bus, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public IServiceBusCallback Send<TMessage>(string destination, Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            try
            {
                return MessageWrapper<TMessage>.Send(Bus, destination, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public IServiceBusCallback Send(params IServiceMessage[] messages)
        {
            try
            {
                return MessageWrapper.Wrap(Bus.Send(MessageWrapper.Wrap(messages)));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public IServiceBusCallback Send(string destination, params IServiceMessage[] messages)
        {
            try
            {
                return MessageWrapper.Wrap(Bus.Send(destination, MessageWrapper.Wrap(messages)));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void SendLocal<TMessage>(Action<TMessage> messageBuilder)
            where TMessage : IServiceMessage
        {
            try
            {
                MessageWrapper<TMessage>.SendLocal(Bus, messageBuilder);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void SendLocal(params IServiceMessage[] messages)
        {
            try
            {
                Bus.SendLocal(MessageWrapper.Wrap(messages));
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Subscribe<TMessage>()
            where TMessage : IServiceMessage
        {
            try
            {
                MessageWrapper<TMessage>.Subscribe(Bus);
            }
            catch (Exception exception) { throw new ServiceBusException(exception); }
        }

        public void Subscribe<TMessage>(Predicate<TMessage> condition)
            where TMessage : IServiceMessage
        {
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
    }
}
