using System;
using System.Runtime.CompilerServices;
namespace SystemEx.Html
{
    // [w3] http://www.w3.org/TR/websockets/
    // [W3] http://dev.w3.org/html5/websockets/
    public class WebSocket
    {
        protected WebSocket() { }

        [AlternateSignature]
        public extern static WebSocket Create(string url);
        public static WebSocket Create(string url, object protocol)
        {
            return (protocol == null ? (WebSocket)Script.Literal("new WebSocket({0})", url) : (WebSocket)Script.Literal("new WebSocket({0}, {1})", url, protocol));
        }

        public string Url
        {
            get { return (string)Script.Literal("this.url"); }
        }

        // ready state
        public const short CONNECTING = 0;
        public const short OPEN = 1;
        public const short CLOSING = 2;
        public const short CLOSED = 3;

        public short ReadyState
        {
            get { return (short)Script.Literal("this.readyState"); }
        }

        public int BufferedAmount
        {
            get { return (int)Script.Literal("this.bufferedAmount"); }
        }

        // networking
        public EventHandler OnOpen
        {
            get { return (EventHandler)Script.Literal("this.onopen"); }
            set { Script.Literal("this.onopen = {0}", value); }
        }

        public MessageEventHandler OnMessage
        {
            get { return (MessageEventHandler)Script.Literal("this.onmessage"); }
            set { Script.Literal("this.onmessage = {0}", value); }
        }

        public EventHandler OnError
        {
            get { return (EventHandler)Script.Literal("this.onerror"); }
            set { Script.Literal("this.onerror = {0}", value); }
        }

        public CloseEventHandler OnClose
        {
            get { return (CloseEventHandler)Script.Literal("this.onclose"); }
            set { Script.Literal("this.onclose = {0}", value); }
        }

        public string Protocol
        {
            get { return (string)Script.Literal("this.protocol"); }
        }

        public bool Send(string data) { return (bool)Script.Literal("this.send(data)"); }

        public void Close() { Script.Literal("this.close()"); }
    }
}


//public interface IWebSocketListener
//{
//    void OnOpen(WebSocket socket, EventArgs e);
//    void OnMessage(WebSocket socket, EventArgs e);
//    void OnError(WebSocket socket, EventArgs e);
//    void OnClose(WebSocket socket, EventArgs e);
//}

//        public void SetListener(IWebSocketListener listener)
//        {
////            Script.Literal(@"
////if (!{0}) {
////    this.onopen = null;
////    this.onmessage = null;
////    this.onerror = null;
////    this.onclose = null;
////    return;
////}
////var self = this;
////this.onopen = function(e) {
////    {0}.OnOpen(self, EventArgs.Empty);
////};
////this.onmessage = function(e) {
////    {0}.OnMessage(self, EventArgs.Empty);
////};
////this.onerror = function(e) {
////    {0}.OnError(self, EventArgs.Empty);
////};
////this.onclose = function(e) {
////    {0}.OnClose(self, EventArgs.Empty);
////};
////", listener);
//        }
