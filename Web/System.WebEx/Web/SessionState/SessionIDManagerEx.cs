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
using System.Web.Configuration;
using System.Configuration;
namespace System.Web.SessionState
{
    /// <summary>
    /// SessionIDManagerEx
    /// </summary>
    public static class SessionIDManagerEx
    {
        // should base.SaveSessionID be used instead
        public static void ForceSessionId(string newId)
        {
            if (newId == null)
                throw new ArgumentNullException("newId");
            var httpContext = HttpContext.Current;
            //GuardInitializeRequestNotCalled(httpContext);
            var sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
            if (sessionStateSection.Cookieless != HttpCookieMode.UseCookies)
                throw new HttpException("Must be cookieless");
            string cookieName = sessionStateSection.CookieName;
            var httpRequest = httpContext.Request;
            var requestCookieHash = httpRequest.Cookies;
            // should this call base.CreateSessionCookie(newId)
            var cookie = new HttpCookie(cookieName, newId) { Path = "/", HttpOnly = true };
            //NOTE: adding to Response.Cookies resets Request.Cookies and adds the cookie to Request too
            httpContext.Response.Cookies.Set(cookie);
            if (requestCookieHash.Get(cookieName) != null)
                requestCookieHash.Remove(cookieName);
            requestCookieHash.Set(cookie);
        }

        //private static void GuardInitializeRequestNotCalled(HttpContext httpContext)
        //{
        //    if (httpContext.Items["AspSessionIDManagerInitializeRequestCalled"] == null)
        //        throw new HttpException(SR.Get("SessionIDManager_InitializeRequest_not_called"));
        //}

    }
}