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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Security;
[assembly: WebResource("System.Resource_.SwfUpload2.2.0.1.js", "text/javascript")]
[assembly: WebResource("System.Resource_.SwfUpload2_2_0_1.cookies.js", "text/javascript")]
[assembly: WebResource("System.Resource_.SwfUpload2_2_0_1.queue.js", "text/javascript")]
[assembly: WebResource("System.Resource_.SwfUpload2_2_0_1.speed.js", "text/javascript")]
[assembly: WebResource("System.Resource_.SwfUpload2_2_0_1.swfobject.js", "text/javascript")]
[assembly: WebResource("System.Resource_.SwfUpload2_2_0_1.swfupload.swf", "application/x-Shockwave-Flash")]
namespace System.Web.UI.ClientShapes
{
    /// <summary>
    /// ClientScriptRegistrar
    /// </summary>
    public class ClientScriptRegistrarSwfUploadShape
    {
        private static Type s_type = typeof(SwfUploadShape);
        public const string SwfUploadVersion = "2.2.0.1";

        [Flags]
        public enum Registrations
        {
            SwfUpload = 0x1,
            SwfUploadCookiePlugin = SwfUpload | 0x2,
            SwfUploadQueuePlugin = SwfUpload | 0x4,
            SwfUploadSpeedPlugin = SwfUpload | 0x8,
            SwfUploadSwfObjectPlugin = SwfUpload | 0x10,
        }

        public static void Register(IClientScriptManager manager, Registrations registrations, Nattrib attrib)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if ((registrations & Registrations.SwfUpload) == Registrations.SwfUpload)
            {
                //string swfUploadVersion;
                string version = SwfUploadVersion; // ((attrib != null) && attrib.TryGetValue("swfUploadVersion", out swfUploadVersion) ? swfUploadVersion : SwfUploadVersion);
                if (string.IsNullOrEmpty(version))
                    throw new InvalidOperationException("version");
                string versionFolder = "System.Resource_.SwfUpload" + version.Replace(".", "_");
                // STATE
                HttpContext.Current.Set<ClientScriptRegistrarSwfUploadShape>(new ClientScriptRegistrarSwfUploadShape
                {
                    SwfUploadFlashUrl = ClientScriptManagerEx.GetWebResourceUrl(s_type, versionFolder + ".swfupload.swf"),
                });
                // INCLUDES
                manager.EnsureItem<HtmlHead>("SwfUpload", () => new IncludeForResourceClientScriptItem(s_type, "System.Resource_.SwfUpload" + version + ".js"));
                if ((registrations & Registrations.SwfUploadCookiePlugin) == Registrations.SwfUploadCookiePlugin)
                    manager.EnsureItem<HtmlHead>("CookiePlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".cookies.js"));
                if ((registrations & Registrations.SwfUploadQueuePlugin) == Registrations.SwfUploadQueuePlugin)
                    manager.EnsureItem<HtmlHead>("QueuePlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".queue.js"));
                if ((registrations & Registrations.SwfUploadSpeedPlugin) == Registrations.SwfUploadSpeedPlugin)
                    manager.EnsureItem<HtmlHead>("SpeedPlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".speed.js"));
                if ((registrations & Registrations.SwfUploadSwfObjectPlugin) == Registrations.SwfUploadSwfObjectPlugin)
                    manager.EnsureItem<HtmlHead>("SwfObjectPlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".swfobject.js"));
            }
        }

        public string SwfUploadFlashUrl { get; internal set; }

        public static ClientScriptRegistrarSwfUploadShape AssertRegistered()
        {
            var registrar = HttpContext.Current.Get<ClientScriptRegistrarSwfUploadShape>();
            if (registrar == null)
                throw new InvalidOperationException("ClientScriptRegistrarSwfUploadShape.PerRequest must be set first");
            return registrar;
        }


        //Fix for the Flash Player Cookie bug in Non-IE browsers.
        //Since Flash Player always sends the IE cookies even in FireFox we have to bypass the cookies by sending the values as part of the POST or GET and overwrite the cookies with the passed in values.
        //The theory is that at this point (BeginRequest) the cookies have not been read by the Session and Authentication logic and if we update the cookies here we'll get our Session and Authentication restored correctly
        public static void FlashFixInBeginRequest(HttpRequest r)
        {
            try
            {
                TrySetCookie(r, "ASP.NET_SESSIONID", "ASPSESSID");
            }
            catch (Exception) { throw new HttpException("Error Initializing Session").PrepareForRethrow(); }
            try
            {
                TrySetCookie(r, FormsAuthentication.FormsCookieName, "AUTHID");
            }
            catch (Exception) { throw new HttpException("Error Initializing Forms Authentication").PrepareForRethrow(); }
        }

        private static bool TrySetCookie(HttpRequest r, string cookieId, string requestId)
        {
            // find value
            string value;
            if (!string.IsNullOrEmpty(r.Form[requestId]))
                value = r.Form[requestId];
            else if (!string.IsNullOrEmpty(r.QueryString[requestId]))
                value = r.QueryString[requestId];
            else
                return false;
            // set cookie
            var cookie = r.Cookies.Get(cookieId);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieId);
                r.Cookies.Add(cookie);
            }
            cookie.Value = value;
            r.Cookies.Set(cookie);
            return true;
        }
    }
}
