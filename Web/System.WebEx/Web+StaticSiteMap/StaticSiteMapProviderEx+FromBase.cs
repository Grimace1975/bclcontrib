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
using System.Reflection;
using System.Collections;
namespace System.Web
{
    public partial class StaticSiteMapProviderEx
    {
        public void RebaseNodesRecurse(SiteMapNode node, string rebaseUrl)
        {
            string nodeUrl = node.Url;
            string newNodeUrl = rebaseUrl + nodeUrl;
            node.Url = newNodeUrl;
            _providerUrlTable.Remove(nodeUrl);
            _providerUrlTable.Add(newNodeUrl, node);
            // duplicate root node
            if (nodeUrl == "/")
                _providerUrlTable.Add(rebaseUrl, node);
            if (node.HasChildNodes)
                foreach (SiteMapNode childNode in node.ChildNodes)
                    RebaseNodesRecurse(childNode, rebaseUrl);
        }

        public virtual SiteMapNode FindSiteMapNodeFromKeyEx(string key, bool useBase)
        {
            SiteMapNode node;
            if (!useBase)
                node = (SiteMapNode)_providerKeyTable[key];
            else
            {
                node = base.FindSiteMapNodeFromKey(key);
                if (node == null)
                    node = (SiteMapNode)_providerKeyTable[key];
            }
            return ReturnNodeIfAccessibleEx(node);
        }

        protected SiteMapNode ReturnNodeIfAccessibleEx(SiteMapNode node)
        {
            return ((node != null) && node.IsAccessibleToUser(HttpContext.Current) ? node : null);
        }
    }
}