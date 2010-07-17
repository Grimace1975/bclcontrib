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
//namespace Instinct.ApplicationUnit_.WebApplicationControl
//{
//    /// <summary>
//    /// SiteCatalyst (Omniture) tracker control
//    /// </summary>
//    public class SiteCatalyst : CachingControl
//    {
//        private static readonly string AppSettingId_OperationMode = "SiteCatalystTracker" + KernelText.Scope + "OperationMode";
//        private static readonly string AppSettingId_RemoteCodeJsUri = "SiteCatalystTracker" + KernelText.Scope + "RemoteCodeJsUri";
//        private static readonly string AppSettingId_TrackerId = "SiteCatalystTracker" + KernelText.Scope + "TrackerId";
//        private static readonly System.Type s_trackerOperationModeType = typeof(GoogleAnalyticsTracker.TrackerOperationMode);

//        //- Main -//
//        public SiteCatalyst()
//            : base()
//        {
//            Hash<string, object> hash = Kernel.Instance.Hash;
//            //+ determine operationMode
//            object operationMode;
//            if (hash.TryGetValue(AppSettingId_OperationMode, out operationMode) == true)
//            {
//                OperationMode = (GoogleAnalyticsTracker.TrackerOperationMode)System.Enum.Parse(s_trackerOperationModeType, (string)operationMode);
//            }
//            else if (EnvironmentBase.DeploymentTarget == KernelDeploymentTarget.Live)
//            {
//                OperationMode = GoogleAnalyticsTracker.TrackerOperationMode.Production;
//            }
//            else
//            {
//                OperationMode = GoogleAnalyticsTracker.TrackerOperationMode.Development;
//            }
//            //+ determine trackerId
//            object remoteCodeJsUri;
//            if (hash.TryGetValue(AppSettingId_RemoteCodeJsUri, out remoteCodeJsUri) == true)
//            {
//                RemoteCodeJsUri = Kernel.ParseText(remoteCodeJsUri, string.Empty);
//            }
//            //+ determine trackerId
//            object trackerId;
//            if (hash.TryGetValue(AppSettingId_TrackerId, out trackerId) == true)
//            {
//                TrackerId = Kernel.ParseText(trackerId, string.Empty);
//            }
//        }

//        //- Render -//
//        protected override void Render(System.Web.UI.HtmlTextWriter writer)
//        {
//            if (string.IsNullOrEmpty(TrackerId) == false)
//            {
//                Property1 = TrackerId;
//                Property2 = TrackerId;
//                Property3 = TrackerId;
//            }
//            string trackerId = Property1;
//            //+
//            GoogleAnalyticsTracker.TrackerOperationMode operationMode = OperationMode;
//            switch (operationMode)
//            {
//                case GoogleAnalyticsTracker.TrackerOperationMode.Production:
//                case GoogleAnalyticsTracker.TrackerOperationMode.Commented:
//                    Emit(writer, (operationMode == GoogleAnalyticsTracker.TrackerOperationMode.Commented));
//                    break;
//                case GoogleAnalyticsTracker.TrackerOperationMode.Development:
//                    writer.WriteLine("<!--SiteCatalyst[" + System.Web.HttpUtility.HtmlEncode(trackerId) + "]-->");
//                    break;
//                default:
//                    throw new System.InvalidOperationException();
//            }
//        }

//        #region EMIT
//        /// <summary>
//        /// Emits the specified writer.
//        /// </summary>
//        /// <param name="writer">The writer.</param>
//        private void Emit(System.Web.UI.HtmlTextWriter writer, bool emitCommented)
//        {
//            bool showVersion = string.IsNullOrEmpty(Version) == false;
//            //+ header
//            if (showVersion == true)
//            {
//                writer.Write(@"<!-- SiteCatalyst code version: " + Version + @".
//Copyright 1997-2004 Omniture, Inc. More info available at http://www.omniture.com -->");
//            }
//            //+
//            writer.Write(emitCommented == false ? @"<script language=""JavaScript"">" : @"<!--script language=""JavaScript""-->");
//            writer.Write(@"<!--
//    /* You may give each page an identifying name, server, and channel on the next lines. */
//    var s_pageName = "); writer.Write(Instinct.ClientScript.EncodeText(PageName ?? "")); writer.Write(@";
//    var s_server = "); writer.Write(Instinct.ClientScript.EncodeText(Server ?? "")); writer.Write(@";
//    var s_channel = "); writer.Write(Instinct.ClientScript.EncodeText(Channel ?? "")); writer.Write(@";
//    var s_hier1 = "); writer.Write(Instinct.ClientScript.EncodeText(Hier1 ?? "")); writer.Write(@";
//    var s_pageType = "); writer.Write(Instinct.ClientScript.EncodeText(PageType ?? "")); writer.Write(@";
//    var s_prop1 = "); writer.Write(Instinct.ClientScript.EncodeText(Property1 ?? "")); writer.Write(@";
//    var s_prop2 = "); writer.Write(Instinct.ClientScript.EncodeText(Property2 ?? "")); writer.Write(@";
//    var s_prop3 = "); writer.Write(Instinct.ClientScript.EncodeText(Property3 ?? "")); writer.Write(@";
//    /********* INSERT THE DOMAIN AND PATH TO YOUR CODE BELOW ************/ //-->");
//            writer.Write(emitCommented == false ? "</script>" : @"<!--/script-->");
//            if (string.IsNullOrEmpty(RemoteCodeJsUri) == false)
//            {
//                writer.Write("\n" + (emitCommented == false ? @"<script language=""JavaScript"" src=""" : @"<!--script language=""JavaScript"" src="""));
//                writer.Write(Instinct.ClientScript.EncodePartialText(RemoteCodeJsUri));
//                writer.Write(@""" type=""text/javascript"">");
//                writer.Write(emitCommented == false ? "</script>" : @"<!--/script-->");
//            }
//            //+ footer
//            if (showVersion == true)
//            {
//                writer.Write(@"
//<!-- End SiteCatalyst code version: " + Version + @". -->");
//            }
//        }
//        #endregion EMIT

//        //- TAG -//
//        #region TAG
//        /// <summary>
//        /// Gets or sets the channel.
//        /// </summary>
//        /// <value>The channel.</value>
//        public string Channel { get; set; }

//        /// <summary>
//        /// Gets or sets the hier1.
//        /// </summary>
//        /// <value>The hier1.</value>
//        public string Hier1 { get; set; }

//        /// <summary>
//        /// Gets or sets the operation mode.
//        /// </summary>
//        /// <value>The operation mode.</value>
//        public GoogleAnalyticsTracker.TrackerOperationMode OperationMode { get; set; }

//        /// <summary>
//        /// Gets or sets the name of the page.
//        /// </summary>
//        /// <value>The name of the page.</value>
//        public string PageName { get; set; }

//        /// <summary>
//        /// Gets or sets the type of the page.
//        /// </summary>
//        /// <value>The type of the page.</value>
//        public string PageType { get; set; }

//        /// <summary>
//        /// Gets or sets the s_prop1.
//        /// </summary>
//        /// <value>The s_prop1.</value>
//        public string Property1 { get; set; }

//        /// <summary>
//        /// Gets or sets the s_prop2.
//        /// </summary>
//        /// <value>The s_prop2.</value>
//        public string Property2 { get; set; }

//        /// <summary>
//        /// Gets or sets the s_prop3.
//        /// </summary>
//        /// <value>The s_prop3.</value>
//        public string Property3 { get; set; }

//        /// <summary>
//        /// Gets or sets the remote code Javascript file URI.
//        /// </summary>
//        /// <value>The remote code Javascript file URI.</value>
//        public string RemoteCodeJsUri { get; set; }

//        /// <summary>
//        /// Gets or sets the server.
//        /// </summary>
//        /// <value>The server.</value>
//        public string Server { get; set; }

//        /// <summary>
//        /// Gets or sets the tracker id.
//        /// </summary>
//        /// <value>The tracker id.</value>
//        public string TrackerId { get; set; }

//        /// <summary>
//        /// Gets or sets the version.
//        /// </summary>
//        /// <value>The version.</value>
//        public string Version { get; set; }
//        #endregion TAG
//    }
//}