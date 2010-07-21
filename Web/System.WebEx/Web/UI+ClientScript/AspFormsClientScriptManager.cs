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
using System.Collections.Generic;
namespace System.Web.UI
{
    /// <summary>
    /// AspFormsClientScriptManager
    /// </summary>
    public class AspFormsClientScriptManager : ClientScriptManagerEx
    {
        public void RegisterStartupScript<T>(ClientScriptManager clientScript)
        {
            if (clientScript != null)
                throw new ArgumentNullException("clientScript");
            var repository = GetRepository<T>();
            RenderBlocks(clientScript, repository.Includes, false);
            RenderBlocks(clientScript, repository.Items, true);
        }

        private static void RenderBlocks(ClientScriptManager clientScript, Dictionary<string, ClientScriptItemBase> blocks, bool addScriptTags)
        {
            if (blocks.Count > 0)
            {
                var b = new StringBuilder();
                if (addScriptTags)
                    b.Append(@"<script type=""text/javascript"">
        //<![CDATA[");
                foreach (var block in blocks.Values)
                    block.Render(b);
                blocks.Clear();
                if (addScriptTags)
                    b.AppendLine(@"//]]>");
                clientScript.RegisterStartupScript(typeof(ClientScript), string.Empty, b.ToString(), false);
            }
        }

        //ClientScriptBlocks.SetRenderMethodDelegate((output, container) =>
        //{
        //    typeof(ClientScriptManager).GetMethod("RenderClientScriptBlocks", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this.ClientScript, new object[] { output });
        //});
    }
}
