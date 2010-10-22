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
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Web.Configuration;
using System.Configuration;
namespace System.Web
{
    /// <summary>
    /// HttpErrorTrapModule
    /// </summary>
    //: [CustomerError Transfer]http://www.colincochrane.com/post/2008/01/ASP-NET-Custom-Errors-Preventing-302-Redirects-To-Custom-Error-Pages.aspx
    // defaultUrlRoutingType="System.Web.UI.Page, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    public class HttpErrorTrapModule : Patterns.Disposeable, IHttpModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpErrorTrapModule"/> class.
        /// </summary>
        public HttpErrorTrapModule()
            : base() { }

        public void Init(HttpApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("application");
            application.Error += new EventHandler(OnError);
        }

        protected void OnError(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var exception = application.Server.GetLastError();
            var httpContext = application.Context;
            // log error
            LogException(exception, httpContext);
            //+ server transfer httpexception
            var httpException = (exception as HttpException);
            if (httpException != null)
            {
                int statusId = httpException.GetHttpCode();
                // get the web application configuration.
                // WARNING: "~/web.config" fails/questionable if in a sub-application, because trys to map to the root web.config (should be parameter)
                var configuration = WebConfigurationManager.OpenWebConfiguration("~/web.config");
                var customErrorsSection = (CustomErrorsSection)configuration.GetSection("system.web/customErrors");
                var customErrors = customErrorsSection.Errors;
                var customError = customErrors[statusId.ToString()];
                // find redirect
                string redirect = (customError == null ? customErrorsSection.DefaultRedirect : customError.Redirect);
                // find httpHandlerType
                Type httpHandlerType;
                var customErrorsSectionSyn = new CustomErrorsSectionSyn(customErrorsSection);
                var customErrorSyn = new CustomErrorSyn(customError);
                httpHandlerType = (customErrorsSectionSyn.DefaultUrlRoutingType == null ? (Type)null : customErrorsSectionSyn.DefaultUrlRoutingType);
                if ((customErrors != null) && (customErrorSyn.UrlRoutingType != null))
                    httpHandlerType = customErrorSyn.UrlRoutingType;
                var handlerBuilder = (httpHandlerType == null ? (Func<IHttpHandler>)null : () => (IHttpHandler)Activator.CreateInstance(httpHandlerType));
                // clears existing response headers and sets the desired ones.
                httpContext.ClearError();
                var httpResponse = httpContext.Response;
                httpResponse.ClearHeaders();
                httpResponse.StatusCode = statusId;
                if (!string.IsNullOrEmpty(redirect))
                    if (handlerBuilder == null)
                        httpContext.Server.ModernTransfer(redirect);
                    else
                        httpContext.Server.ModernTransferForUrlRouting(redirect, handlerBuilder);
                else
                    httpResponse.Flush();
            }
            application.CompleteRequest();
        }

        /// <summary>
        /// Logs an exception and its context to the error log.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="httpContext">The HTTP context.</param>
        protected virtual void LogException(Exception e, HttpContext httpContext)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            try
            {
                var b = new StringBuilder();
                using (var w = XmlWriter.Create(b))
                {
                    w.WriteStartElement("error");
                    new HttpError(e, httpContext).ToXml(w);
                    w.WriteEndElement();
                }
                string errorText = b.ToString();
                // log based on status code
                var httpException = (e as HttpException);
                if (httpException != null)
                {
                    int statusId = httpException.GetHttpCode();
                    switch (MathEx.Floor(statusId / 100M))
                    {
                        case 4:
                            Logger.LogEvent(Logger.LogEventType.Information, "HttpErrorTrapModule", errorText);
                            break;
                        default:
                            Logger.LogEvent(Logger.LogEventType.Fatal, "HttpErrorTrapModule", errorText);
                            break;
                    }
                }
                else
                    Logger.LogEvent(Logger.LogEventType.Fatal, "HttpErrorTrapModule", errorText);
            }
            catch (Exception localException)
            {
                Trace.WriteLine(localException);
            }
        }
    }
}