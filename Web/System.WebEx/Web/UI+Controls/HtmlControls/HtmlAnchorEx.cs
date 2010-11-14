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
using System.Web.UI.WebControls;
namespace System.Web.UI.HtmlControls
{
    /// <summary>
    /// HtmlAnchorEx
    /// </summary>
    public class HtmlAnchorEx : HtmlAnchor
    {
        private static readonly object s_eventCommandKey = new object();

        public HtmlAnchorEx()
            : base()
        {
            // singhj: added to force a raise event when clicked
            ServerClick += new EventHandler(HtmlButton_ServerClick);
        }

        public string ClientConfirmationText { get; set; }

        public string CommandArgument { get; set; }

        public string CommandName { get; set; }

        protected void HtmlButton_ServerClick(object sender, EventArgs e) { }

        protected override void OnPreRender(EventArgs e)
        {
            if (!string.IsNullOrEmpty(ClientConfirmationText))
                Attributes["onclick"] = "if(!confirm(" + ClientScript.EncodeText(ClientConfirmationText) + "))return(false);";
            base.OnPreRender(e);
        }

        protected override void RaisePostBackEvent(string eventArgument)
        {
            base.RaisePostBackEvent(eventArgument);
            if (!string.IsNullOrEmpty(CommandName))
                OnCommand(new CommandEventArgs(CommandName, CommandArgument));
        }

        public event EventHandler Command
        {
            add { base.Events.AddHandler(s_eventCommandKey, value); }
            remove { base.Events.RemoveHandler(s_eventCommandKey, value); }
        }

        protected virtual void OnCommand(EventArgs e)
        {
            var handler = (EventHandler)base.Events[s_eventCommandKey];
            if (handler != null)
                handler(this, e);
            base.RaiseBubbleEvent(this, e);
        }
    }
}