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
        private Action<IPerRequest> _perRequest;

        [Flags]
        public enum Registrations
        {
            SwfUpload = 0x1,
            SwfUploadCookiePlugin = SwfUpload | 0x2,
            SwfUploadQueuePlugin = SwfUpload | 0x4,
            SwfUploadSpeedPlugin = SwfUpload | 0x8,
            SwfUploadSwfObjectPlugin = SwfUpload | 0x10,
        }

        public interface IPerRequest
        {
            void Register(Registrations registrations, Nattrib attrib);
        }

        public class State : IPerRequest
        {
            public string SwfUploadFlashUrl { get; internal set; }

            void IPerRequest.Register(Registrations registrations, Nattrib attrib)
            {
                var clientScriptManager = (IClientScriptManager)HttpContext.Current.Get<IClientScriptManager>(); // ClientScriptManagerAccessor();
                if (clientScriptManager == null)
                    throw new ArgumentNullException("clientScriptManager");
                if ((registrations & Registrations.SwfUpload) == Registrations.SwfUpload)
                {
                    //string swfUploadVersion;
                    string version = SwfUploadVersion; // ((attrib != null) && attrib.TryGetValue("swfUploadVersion", out swfUploadVersion) ? swfUploadVersion : SwfUploadVersion);
                    if (string.IsNullOrEmpty(version))
                        throw new InvalidOperationException("version");
                    string versionFolder = "System.Resource_.SwfUpload" + version.Replace(".", "_");
                    // STATE
                    HttpContext.Current.Set<State>(new State
                    {
                        SwfUploadFlashUrl = ClientScriptManagerEx.GetWebResourceUrl(s_type, versionFolder + ".swfupload.swf"),
                    });
                    // INCLUDES
                    clientScriptManager.EnsureItem<HtmlHead>(null, () => new IncludeForResourceClientScriptItem(s_type, "System.Resource_.SwfUpload" + version + ".js"));
                    if ((registrations & Registrations.SwfUploadCookiePlugin) == Registrations.SwfUploadCookiePlugin)
                        clientScriptManager.EnsureItem<HtmlHead>("CookiePlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".cookies.js"));
                    if ((registrations & Registrations.SwfUploadQueuePlugin) == Registrations.SwfUploadQueuePlugin)
                        clientScriptManager.EnsureItem<HtmlHead>("QueuePlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".queue.js"));
                    if ((registrations & Registrations.SwfUploadSpeedPlugin) == Registrations.SwfUploadSpeedPlugin)
                        clientScriptManager.EnsureItem<HtmlHead>("SpeedPlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".speed.js"));
                    if ((registrations & Registrations.SwfUploadSwfObjectPlugin) == Registrations.SwfUploadSwfObjectPlugin)
                        clientScriptManager.EnsureItem<HtmlHead>("SwfObjectPlugin", () => new IncludeForResourceClientScriptItem(s_type, versionFolder + ".swfobject.js"));
                }
            }
        }

        static ClientScriptRegistrarSwfUploadShape()
        {
            Current = new ClientScriptRegistrarSwfUploadShape
            {
                ClientScriptManagerAccessor = (() => HttpContext.Current.Get<IClientScriptManager>()),
            };
        }

        public static ClientScriptRegistrarSwfUploadShape Current { get; set; }
        public Func<IClientScriptManager> ClientScriptManagerAccessor { get; set; }

        public void SetPerRequest(Action<IPerRequest> value)
        {
            _perRequest = value;
        }
    }
}
