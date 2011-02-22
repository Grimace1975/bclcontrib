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
//using System.Text;
//namespace System.Web.UI.WebControls
//{
//    // article on a fix: http://it.gps678.com/25/a74ab18e9a53de45.html
//    /// <summary>
//    /// CallbackEndpoint
//    /// </summary>
//    public class CallbackEndpoint : Control, ICallbackEventHandler, ICallbackContainer
//    {
//        private static Type s_type = typeof(CallbackEndpoint);
//        private string _callbackReturn;

//        /// <summary>
//        /// Gets or sets the data source.
//        /// </summary>
//        /// <value>The data source.</value>
//        public Unit.CallbackEndpoints DataSource { get; set; }

//        /// <summary>
//        /// Gets or sets the endpoint id.
//        /// </summary>
//        /// <value>The endpoint id.</value>
//        public string EndpointId { get; set; }

//        /// <summary>
//        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
//        /// </summary>
//        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
//        protected override void OnPreRender(System.EventArgs e)
//        {
//            string endpointId = EndpointId;
//            if (endpointId == null)
//                throw new InvalidOperationException();
//            string encodeIdEncodeText = ClientScript.EncodeText(endpointId);
//            Page.ClientScript.RegisterClientScriptResource(s_type, "Instinct.Resource_.Control.CallbackEndpoint.js");
//            var b = new StringBuilder();
//            // endpoint
//            b.Append(@"
//Control = window.Control || {};
//window.registeredEndpointList = window.registeredEndpointList || [];
//{
//    var endpoint = window.registeredEndpointList[" + encodeIdEncodeText + @"] = new Control.CallbackEndpoint({
//        id: " + encodeIdEncodeText + @"
//    });");
//            // method
//            foreach (string method in DataSource.Keys)
//                b.Append(@"
//    endpoint.addOperation('" + method + @"', function(callback, paramArray) {
//        var paramText = '" + method + @"\2' + paramArray.join('\2');
//        " + GetCallbackScript(null, "paramText") + @"
//    });");
//            b.Append(@"
//}");
//            ClientScript.Instance.AddBlock(b.ToString());
//            base.OnPreRender(e);
//        }

//        #region ICALLBACKEVENTHANDLER
//        /// <summary>
//        /// Returns the results of a callback event that targets a control.
//        /// </summary>
//        /// <returns>The result of the callback.</returns>
//        public string GetCallbackResult()
//        {
//            return _callbackReturn;
//        }

//        /// <summary>
//        /// Processes a callback event that targets a control.
//        /// </summary>
//        /// <param name="eventArgument">A string that represents an event argument to pass to the event handler.</param>
//        public void RaiseCallbackEvent(string eventArgument)
//        {
//            string[] eventArgumentArray = eventArgument.Split(new char[] { '\x02' }, 2);
//            _callbackReturn = DataSource.ExecuteContract(eventArgumentArray[0], eventArgumentArray[1]);
//        }
//        #endregion

//        #region ICALLBACKCONTAINER
//        /// <summary>
//        /// Creates a script for initiating a client callback to a Web server.
//        /// </summary>
//        /// <param name="buttonControl">The control initiating the callback request.</param>
//        /// <param name="argument">The arguments used to build the callback script.</param>
//        /// <returns>
//        /// A script that, when run on a client, will initiate a callback to the Web server.
//        /// </returns>
//        public string GetCallbackScript(System.Web.UI.WebControls.IButtonControl buttonControl, string argument)
//        {
//            return Page.ClientScript.GetCallbackEventReference(this, argument, "callback", string.Empty, null, true);
//        }
//        #endregion
//    }
//}