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
using System.Quality;
using System.Collections.Generic;
namespace System.Web.UI.WebControls
{
    /// <summary>
    /// ClientScriptEmitter
    /// </summary>
    public class ClientScriptEmitter : Control
    {
        public ClientScriptEmitter()
        {
            Inject = true;
        }

        public bool Inject { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        [ServiceDependency]
        public IClientScriptManager ClientScriptManager { get; set; }

        public Type Shard { get; set; }

        public bool InHtmlHead { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            if (InHtmlHead)
                if (Shard == null)
                    Shard = typeof(System.Web.UI.HtmlControls.HtmlHead);
                else
                    throw new InvalidOperationException("Cant set shard and InHtmlHead");
            if (Inject)
            {
                ServiceLocator.Inject(this);
                if (ClientScriptManager == null)
                    throw new InvalidOperationException("ClientScriptManager required");
            }
        }

        protected override void Render(HtmlTextWriter w)
        {
            if (ClientScriptManager == null)
                throw new InvalidOperationException("ClientScriptManager required");
            var repository = ClientScriptManager.GetRepository(Shard ?? typeof(IClientScriptManager));
            RenderItems(w, repository.Includes, false);
            RenderItems(w, repository.Items, true);
        }

        private static void RenderItems(HtmlTextWriter w, Dictionary<string, ClientScriptItemBase> items, bool addScriptTags)
        {
            if (items.Count > 0)
            {
                var b = new StringBuilder();
                foreach (var block in items.Values)
                    block.Render(b);
                items.Clear();
                //
                if (addScriptTags)
                {
                    w.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                    w.RenderBeginTag(HtmlTextWriterTag.Script);
                    w.WriteLine("//<![CDATA[");
                    w.Write(b.ToString());
                    w.WriteLine("//]]>");
                    w.RenderEndTag();
                }
                else
                    w.Write(b.ToString());
            }
        }
    }
}
