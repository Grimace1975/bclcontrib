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
using System.Xml;
using System.Threading;
using System.Collections.Specialized;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace System.Web
{
    /// <summary>
    /// HttpError
    /// </summary>
    public class HttpError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpError"/> class from a given <see cref="Exception"/> instance and
        /// <see cref="System.Web.HttpContext"/> instance representing the HTTP context during the exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="httpContext">The HTTP context.</param>
        public HttpError(Exception e, HttpContext httpContext)
            : base()
        {
            if (e == null)
                throw new ArgumentNullException("e");
            Exception = e;
            // load the basic information.
            var baseException = e.GetBaseException();
            HostName = Environment.MachineName;
            TypeName = baseException.GetType().FullName;
            Message = baseException.Message;
            Source = baseException.Source;
            Detail = e.ToString();
            UserName = (Thread.CurrentPrincipal.Identity.Name ?? string.Empty);
            Date = DateTime.Now;
            // if this is an http exception, then get the status code and detailed html message provided by the host.
            var httpException = e as HttpException;
            if (httpException != null)
            {
                StatusId = httpException.GetHttpCode();
                HtmlErrorMessage = (httpException.GetHtmlErrorMessage() ?? string.Empty);
            }
            // if the http context is available, then capture the collections that represent the state request.
            if (httpContext != null)
            {
                var httpRequest = httpContext.Request;
                ServerVariable = httpRequest.ServerVariables;
                QueryString = httpRequest.QueryString;
                Form = httpRequest.Form;
                Cookie = httpRequest.Cookies;
            }
        }

        /// <summary>
        /// Gets or sets the name of application in which this error occurred.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets a collection representing the client cookies captured as part of diagnostic data for the error.
        /// </summary>
        /// <value>The cookie.</value>
        public HttpCookieCollection Cookie { get; private set; }

        /// <summary>
        /// Gets or sets the date and time (in local time) at which the error occurred.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a detailed text describing the error, such as a stack trace.
        /// </summary>
        /// <value>The detail.</value>
        public string Detail { get; set; }

        /// <summary>
        /// Get the <see cref="Exception"/> instance used to initialize this instance.
        /// </summary>
        /// <value>The exception.</value>
        /// <remarks>
        /// This is a run-time property only that is not written or read during XML serialization via <see cref="FromXml"/> and <see cref="ToXml"/>.
        /// </remarks>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets a collection representing the form variables captured as part of diagnostic data for the error.
        /// </summary>
        public NameValueCollection Form { get; set; }

        /// <summary>
        /// Gets or sets name of host machine where this error occurred.
        /// </summary>
        /// <value>The name of the host.</value>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the HTML message generated by the web host (ASP.NET) for the given error.
        /// </summary>
        /// <value>The HTML error message.</value>
        public string HtmlErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a brief text describing the error.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets a collection representing the Web query string variables captured as part of diagnostic data for the error.
        /// </summary>
        /// <value>The query string.</value>
        public NameValueCollection QueryString { get; private set; }

        /// <summary>
        /// Gets a collection representing the Web server variables captured as part of diagnostic data for the error.
        /// </summary>
        /// <value>The server variable.</value>
        public NameValueCollection ServerVariable { get; private set; }

        /// <summary>
        /// Gets or sets the source that is the cause of the error.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code of the output returned to the client for the error.
        /// </summary>
        /// <value>The status id.</value>
        /// <remarks>
        /// For cases where this value cannot always be reliably determined, the value may be reported as zero.
        /// </remarks>
        public int StatusId { get; set; }

        /// <summary>
        /// Returns the value of the <see cref="Message"/> property.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Message;
        }

        /// <summary>
        /// Get or sets the type, class or category of the error.
        /// </summary>
        /// <value>The type.</value>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the user logged into the application at the time of the error.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        #region ToXml
        /// <summary>
        /// Writes the error data to its XML representation.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        public void ToXml(XmlWriter w)
        {
            if (w == null)
                throw new ArgumentNullException("w");
            if (w.WriteState != WriteState.Element)
                throw new ArgumentOutOfRangeException("w");
            // write out the basic typed information in attributes followed by collections as inner elements.
            WriteXmlAttribute(w);
            WriteInnerXml(w);
        }

        /// <summary>
        /// Writes the error data that belongs in XML attributes.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        protected void WriteXmlAttribute(XmlWriter w)
        {
            if (w == null)
                throw new ArgumentNullException("w");
            w.WriteAttributeStringIf("applicationName", ApplicationName);
            w.WriteAttributeStringIf("hostName", HostName);
            w.WriteAttributeStringIf("typeName", TypeName);
            w.WriteAttributeStringIf("source", Source);
            w.WriteAttributeStringIf("userName", UserName);
            if (Date != DateTime.MinValue)
                w.WriteAttributeString("date", XmlConvert.ToString(Date, XmlDateTimeSerializationMode.Local));
            if (StatusId != 0)
                w.WriteAttributeString("statusId", XmlConvert.ToString(StatusId));
        }

        /// <summary>
        /// Writes the error data that belongs in child nodes.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        protected void WriteInnerXml(System.Xml.XmlWriter w)
        {
            if (w == null)
                throw new ArgumentNullException("w");
            // message + detail
            if ((!string.IsNullOrEmpty(Message)) || (!string.IsNullOrEmpty(Detail)))
            {
                w.WriteStartElement("message");
                w.WriteAttributeStringIf("text", Message);
                if (!string.IsNullOrEmpty(Detail))
                    w.WriteCData(Detail);
                w.WriteEndElement();
            }
            // htmlErrorMessage
            if (!string.IsNullOrEmpty(HtmlErrorMessage))
            {
                w.WriteStartElement("htmlErrorMessage");
                w.WriteCData(HtmlErrorMessage);
                w.WriteEndElement();
            }
            // collections
            w.WriteCollectionIf("serverVariables", new HttpValuesCollection(ServerVariable));
            w.WriteCollectionIf("queryString", new HttpValuesCollection(QueryString));
            w.WriteCollectionIf("form", new HttpValuesCollection(Form));
            w.WriteCollectionIf("cookies", new HttpValuesCollection(Cookie));
        }
        #endregion

        #region HttpValuesCollection
        /// <summary>
        /// A name-values collection implementation suitable for web-based collections (like server variables, query strings, forms and cookies) that can also be written and read as XML.
        /// </summary>
        private sealed class HttpValuesCollection : NameValueCollection, IXmlSerializable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HttpValuesCollection"/> class.
            /// </summary>
            /// <param name="collection">The collection.</param>
            public HttpValuesCollection(NameValueCollection collection)
                : base(collection) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="HttpValuesCollection"/> class.
            /// </summary>
            /// <param name="collection">The collection.</param>
            public HttpValuesCollection(HttpCookieCollection collection)
                : base(collection != null ? collection.Count : 0)
            {
                if ((collection != null) && (collection.Count > 0))
                    for (int index = 0; index < collection.Count; index++)
                    {
                        var cookie = collection[index];
                        // note: we drop the path and domain properties of the cookie for sake of simplicity.
                        Add(cookie.Name, cookie.Value);
                    }
            }

            /// <summary>
            /// Gets the schema.
            /// </summary>
            /// <returns></returns>
            public XmlSchema GetSchema()
            {
                return null;
            }

            /// <summary>
            /// Reads the XML.
            /// </summary>
            /// <param name="xmlReader">The XML reader.</param>
            public void ReadXml(XmlReader r)
            {
                if (r == null)
                    throw new ArgumentNullException("r");
                if (IsReadOnly)
                    throw new InvalidOperationException("Object is read-only.");
                r.Read();
                // add entries into the collection as <item> elements with child <value> elements are found.
                while (r.IsStartElement("item"))
                {
                    string name = r.GetAttribute("name");
                    bool isEmptyElement = r.IsEmptyElement;
                    // <item>
                    r.Read();
                    if (!isEmptyElement)
                    {
                        // <value ...>
                        while (r.IsStartElement("value"))
                        {
                            Add(name, r.GetAttribute("text"));
                            r.Read();
                        }
                        // </item>
                        r.ReadEndElement();
                    }
                    else
                        Add(name, null);
                }
                r.ReadEndElement();
            }

            /// <summary>
            /// Writes the XML.
            /// </summary>
            /// <param name="xmlWriter">The XML writer.</param>
            /// Write out a named multi-value collection as follows (example here is the ServerVariables collection):
            /// <item name="HTTP_URL">
            /// 	<value string="/myapp/somewhere/page.aspx"/>
            /// </item>
            /// <item name="QUERY_STRING">
            /// 	<value string="a=1&amp;b=2"/>
            /// </item>
            /// ...
            public void WriteXml(XmlWriter w)
            {
                if (w == null)
                    throw new ArgumentNullException("w");
                if (Count == 0)
                    return;
                foreach (string key in Keys)
                {
                    w.WriteStartElement("item");
                    w.WriteAttributeString("name", key);
                    string[] values = GetValues(key);
                    if (values != null)
                    {
                        foreach (string value in values)
                        {
                            w.WriteStartElement("value");
                            w.WriteAttributeString("text", value);
                            w.WriteEndElement();
                        }
                    }
                    w.WriteEndElement();
                }
            }
        }
        #endregion
    }
}