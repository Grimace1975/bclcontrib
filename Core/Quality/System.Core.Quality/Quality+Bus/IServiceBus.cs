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
namespace System.Quality
{
    /// <summary>
    /// IServiceBus
    /// </summary>
    public interface IServiceBus
    {
        void Publish<TMessage>(Action<TMessage> messageBuilder) where TMessage : IServiceMessage;
        void Publish<TMessage>(params TMessage[] messages) where TMessage : IServiceMessage;
        void Reply<TMessage>(Action<TMessage> messageBuilder) where TMessage : IServiceMessage;
        void Reply(params IServiceMessage[] messages);
        void Return<T>(T value);
        IServiceBusCallback Send<TMessage>(Action<TMessage> messageBuilder) where TMessage : IServiceMessage;
        IServiceBusCallback Send<TMessage>(string destination, Action<TMessage> messageBuilder) where TMessage : IServiceMessage;
        IServiceBusCallback Send(params IServiceMessage[] messages);
        IServiceBusCallback Send(string destination, params IServiceMessage[] messages);
        void SendLocal<TMessage>(Action<TMessage> messageBuilder) where TMessage : IServiceMessage;
        void SendLocal(params IServiceMessage[] messages);
        void Subscribe<TMessage>() where TMessage : IServiceMessage;
        void Subscribe<TMessage>(Predicate<TMessage> condition) where TMessage : IServiceMessage;
        void Subscribe(Type messageType);
        void Subscribe(Type messageType, Predicate<IServiceMessage> condition);
        void Unsubscribe<TMessage>() where TMessage : IServiceMessage;
        void Unsubscribe(Type messageType);
    }
}