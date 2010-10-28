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
[assembly: WebResource("System.Resource_.SwfObject2.1.js", "text/javascript")]
[assembly: WebResource("System.Resource_.SwfObject2_1.expressInstall.swf", "application/x-Shockwave-Flash")]
namespace System.Web.UI.ClientShapes
{
    /// <summary>
    /// ClientScriptRegistrar
    /// </summary>
    public class ClientScriptRegistrarSwfObjectShape
    {
        private static Type s_type = typeof(SwfObjectShape);
        public const string SwfObjectVersion = "2.1";

        [Flags]
        public enum Registrations
        {
            SwfObject = 0x1,
        }

        public static void Register(IClientScriptManager manager, Registrations registrations, Nattrib attrib)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if ((registrations & Registrations.SwfObject) == Registrations.SwfObject)
            {
                //string swfObjectVersion;
                string version = SwfObjectVersion; // ((attrib != null) && attrib.TryGetValue("swfObjectVersion", out swfObjectVersion) ? swfObjectVersion : SwfObjectVersion);
                if (string.IsNullOrEmpty(version))
                    throw new InvalidOperationException("version");
                string versionFolder = "System.Resource_.SwfObject" + version.Replace(".", "_");
                // STATE
                HttpContext.Current.Set<ClientScriptRegistrarSwfObjectShape>(new ClientScriptRegistrarSwfObjectShape
                {
                    SwfObjectExpressInstallFlashUrl = ClientScriptManagerEx.GetWebResourceUrl(s_type, versionFolder + ".expressInstall.swf"),
                });
                // INCLUDES
                manager.EnsureItem<HtmlHead>(null, () => new IncludeForResourceClientScriptItem(s_type, "System.Resource_.SwfObject" + version + ".js"));
            }
        }

        public string SwfObjectExpressInstallFlashUrl { get; private set; }
    }
}
