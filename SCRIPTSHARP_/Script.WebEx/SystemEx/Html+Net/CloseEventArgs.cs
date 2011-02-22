using System;
using System.Html;
namespace SystemEx.Html
{
    public delegate void CloseEventHandler(WebSocket socket, CloseEventArgs e);

    public class CloseEventArgs
    {
        protected CloseEventArgs() { }

        public bool WasClean
        {
            get { return (bool)Script.Literal("this.wasClean"); }
        }

        public void InitCloseEvent(string typeArg, bool canBubbleArg, bool cancelableArg, bool wasCleanArg) { Script.Literal("this.initCloseEvent({0}, {1}, {2}, {3})", typeArg, canBubbleArg, cancelableArg, wasCleanArg); }
    }
}
