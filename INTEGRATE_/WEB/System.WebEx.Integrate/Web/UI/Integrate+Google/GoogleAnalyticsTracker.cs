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
using System.Patterns.ReleaseManagement;
namespace System.Web.UI.Integrate
{
    /// <summary>
    /// GoogleAnalyticsTracker
    /// </summary>
    // http://code.google.com/apis/analytics/docs/gaJS/gaJSApiEcommerce.html
    public class GoogleAnalyticsTracker : Control
    {
        private static readonly string AppSettingId_OperationMode = "GoogleAnalyticsTracker" + CoreEx.Scope + "OperationMode";
        private static readonly string AppSettingId_TrackerId = "GoogleAnalyticsTracker" + CoreEx.Scope + "TrackerId";
        private static readonly Type s_trackerOperationModeType = typeof(TrackerOperationMode);

        #region Class Types
        /// <summary>
        /// TrackerOperationMode
        /// </summary>
        public enum TrackerOperationMode
        {
            /// <summary>
            /// Production
            /// </summary>
            Production,
            /// <summary>
            /// Development
            /// </summary>
            Development,
            /// <summary>
            /// Commented
            /// </summary>
            Commented,
        }

        /// <summary>
        /// AnalyticsCommerceTransaction
        /// </summary>
        public class AnalyticsCommerceTransaction
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AnalyticsCommerceTransaction"/> class.
            /// </summary>
            public AnalyticsCommerceTransaction()
            {
                Country = "USA";
            }

            /// <summary>
            /// OrderId
            /// </summary>
            public string OrderId;

            /// <summary>
            /// AffiliationId
            /// </summary>
            public string AffiliationId;

            /// <summary>
            /// TotalAmount
            /// </summary>
            public decimal TotalAmount;

            /// <summary>
            /// TaxAmount
            /// </summary>
            public decimal TaxAmount;

            /// <summary>
            /// ShippingAmount
            /// </summary>
            public decimal ShippingAmount;

            /// <summary>
            /// City
            /// </summary>
            public string City;

            /// <summary>
            /// State
            /// </summary>
            public string State;

            /// <summary>
            /// Country
            /// </summary>
            public string Country;
        }

        /// <summary>
        /// AnalyticsCommerceItem
        /// </summary>
        public class AnalyticsCommerceItem
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AnalyticsCommerceItem"/> class.
            /// </summary>
            public AnalyticsCommerceItem() { }

            /// <summary>
            /// OrderId
            /// </summary>
            public string OrderId;

            /// <summary>
            /// SkuId
            /// </summary>
            public string SkuId;

            /// <summary>
            /// ProductName
            /// </summary>
            public string ProductName;

            /// <summary>
            /// CategoryName
            /// </summary>
            public string CategoryName;

            /// <summary>
            /// Price
            /// </summary>
            public decimal Price;

            /// <summary>
            /// Count
            /// </summary>
            public int Count;
        }
        #endregion

        public GoogleAnalyticsTracker()
            : base()
        {
            DeploymentTarget = DeploymentEnvironment.Live;
            Version = 2;
            //var hash = Kernel.Instance.Hash;
            //// determine operationMode
            //object operationMode;
            //if (hash.TryGetValue(AppSettingId_OperationMode, out operationMode))
            //    OperationMode = (TrackerOperationMode)Enum.Parse(s_trackerOperationModeType, (string)operationMode);
            //else if (EnvironmentEx.DeploymentEnvironment == DeploymentTarget)
            //    OperationMode = TrackerOperationMode.Production;
            //else
            //    OperationMode = TrackerOperationMode.Development;
            //// determine trackerId
            //object trackerId;
            //if (hash.TryGetValue(AppSettingId_TrackerId, out trackerId))
            //    TrackerId = trackerId.Parse<string>();
        }

        /// <summary>
        /// Gets or sets the operation mode.
        /// </summary>
        /// <value>The operation mode.</value>
        public TrackerOperationMode OperationMode { get; set; }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter w)
        {
            if (string.IsNullOrEmpty(TrackerId))
                return;
            OnPreEmit();
            var operationMode = OperationMode;
            switch (operationMode)
            {
                case TrackerOperationMode.Production:
                case TrackerOperationMode.Commented:
                    bool emitCommented = (operationMode == TrackerOperationMode.Commented);
                    switch (Version)
                    {
                        case 2:
                            EmitVersion2(w, emitCommented);
                            break;
                        case 1:
                            EmitVersion1(w, emitCommented);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                    break;
                case TrackerOperationMode.Development:
                    w.WriteLine("<!--GoogleAnalyticsTracker[" + HttpUtility.HtmlEncode(TrackerId) + "]-->");
                    var commerceTransaction = CommerceTransaction;
                    if (commerceTransaction != null)
                        w.WriteLine("<!--GoogleAnalyticsTracker::CommerceTransaction[" + commerceTransaction.OrderId + "]-->");
                    var commerceItemArray = CommerceItems;
                    if (commerceItemArray != null)
                        w.WriteLine("<!--GoogleAnalyticsTracker::CommerceItemArray[" + commerceItemArray.Length.ToString() + "]-->");
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public DeploymentEnvironment DeploymentTarget { get; set; }

        #region Emit
        /// <summary>
        /// Emits the version2.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void EmitVersion2(HtmlTextWriter w, bool emitCommented)
        {
            w.Write(!emitCommented ? "<script type=\"text/javascript\">\n" : "<!--script type=\"text/javascript\">\n");
            w.Write(@"//<![CDATA[
var gaJsHost = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");
document.write(unescape(""%3Cscript src='"" + gaJsHost + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));
//]]>");
            w.Write(!emitCommented ? "</script><script type=\"text/javascript\">\n" : "</script--><!--script type=\"text/javascript\">\n");
            w.Write(@"//<![CDATA[
try {
var pageTracker = _gat._getTracker("""); w.Write(TrackerId); w.Write("\");\n");
            w.Write("pageTracker._trackPageview();\n");
            //
            var commerceTransaction = CommerceTransaction;
            var commerceItems = CommerceItems;
            if ((commerceTransaction != null) || (commerceItems != null))
            {
                if (commerceTransaction != null)
                {
                    w.Write("pageTracker._addTrans(");
                    w.Write(ClientScript.EncodeText(commerceTransaction.OrderId ?? string.Empty));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.AffiliationId ?? string.Empty));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.TotalAmount.ToString()));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.TaxAmount.ToString()));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.ShippingAmount.ToString()));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.City ?? string.Empty));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.State ?? string.Empty));
                    w.Write(","); w.Write(ClientScript.EncodeText(commerceTransaction.Country ?? string.Empty));
                    w.Write(");\n");
                }
                //+
                if (commerceItems != null)
                {
                    foreach (var commerceItem in commerceItems)
                    {
                        if (commerceItem != null)
                        {
                            w.Write("pageTracker._addItem(");
                            w.Write(ClientScript.EncodeText(commerceItem.OrderId ?? string.Empty));
                            w.Write(","); w.Write(ClientScript.EncodeText(commerceItem.SkuId ?? string.Empty));
                            w.Write(","); w.Write(ClientScript.EncodeText(commerceItem.ProductName ?? string.Empty));
                            w.Write(","); w.Write(ClientScript.EncodeText(commerceItem.CategoryName ?? string.Empty));
                            w.Write(","); w.Write(ClientScript.EncodeText(commerceItem.Price.ToString()));
                            w.Write(","); w.Write(ClientScript.EncodeText(commerceItem.Count.ToString()));
                            w.Write(");\n");
                        }
                    }
                }
                w.Write("pageTracker._trackTrans();\n");
            }
            w.Write(@"} catch(err) {}
//]]>");
            w.Write(!emitCommented ? "</script>\n" : "</script-->");
        }

        /// <summary>
        /// Emits the version1.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void EmitVersion1(HtmlTextWriter w, bool emitCommented)
        {
            w.Write(!emitCommented ? "<script src=\"http://www.google-analytics.com/urchin.js\" type=\"text/javascript\"></script><script type=\"text/javascript\">" : "<!--script src=\"http://www.google-analytics.com/urchin.js\" type=\"text/javascript\"></script--><!--script type=\"text/javascript\">");
            w.Write(@"//<![CDATA[
_uacct = """); w.Write(TrackerId); w.Write(@""";
urchinTracker();
//]]>");
            w.Write(!emitCommented ? "</script>" : "</script-->");
        }
        #endregion

        /// <summary>
        /// Gets or sets the commerce transaction.
        /// </summary>
        /// <value>The commerce transaction.</value>
        public AnalyticsCommerceTransaction CommerceTransaction { get; set; }

        /// <summary>
        /// Gets or sets the commerce item array.
        /// </summary>
        /// <value>The commerce item array.</value>
        public AnalyticsCommerceItem[] CommerceItems { get; set; }

        /// <summary>
        /// Gets or sets the tracker id.
        /// </summary>
        /// <value>The tracker id.</value>
        public string TrackerId { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        //+ should be: VersionId
        public int Version { get; set; }

        /// <summary>
        /// Called when [pre emit].
        /// </summary>
        protected virtual void OnPreEmit()
        {
            var preEmit = PreEmit;
            if (preEmit != null)
                preEmit(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when [pre emit].
        /// </summary>
        public event EventHandler PreEmit;
    }
}
