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
using System.Web.UI.HtmlControls;
namespace System.Web.UI
{
    /// <summary>
    /// HttpPageToHtmlHeadMapper
    /// </summary>
    public class HttpPageToHtmlHeadMapper
    {
        public class HeaderControlId
        {
            public const string Icon = "HtmlHeadIcon";
            public const string ShortcutIcon = "HtmlHeadShortcutIcon";
            public const string Search = "HtmlHeadSearch";
            public const string Syndication = "HtmlHeadRss";
            public const string Title = "HtmlHeadTitle";
            public const string Keyword = "HtmlHeadKeyword";
            public const string Description = "HtmlHeadDescription";
        }

        public static void Map(HttpPage httpPage, HttpResponse response)
        {
            switch (httpPage.CachingApproach)
            {
                case HttpPage.HttpPageCachingApproach.Default:
                    break;
                case HttpPage.HttpPageCachingApproach.LastModifyDateWithoutCaching:
                    DateTime? lastModifyDate = httpPage.LastModifyDate;
                    if (lastModifyDate != null)
                    {
                        //NOTE: adding headers instead of Response.Cache because issues with NoCache and Last-Modified header
                        response.AddHeader("Cache-Control", "no-cache");
                        response.AddHeader("Pragma", "no-cache");
                        response.AddHeader("Expires", "-1");
                        response.AddHeader("Last-Modified", lastModifyDate.Value.ToUniversalTime().ToString("R"));
                    }
                    else
                        response.Cache.SetCacheability(HttpCacheability.NoCache);
                    break;
                case HttpPage.HttpPageCachingApproach.NoCache:
                    response.Cache.SetCacheability(HttpCacheability.NoCache);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static void Map(HttpPage.HttpHead httpHead, HtmlHead htmlHead)
        {
            var htmlHeadControls = htmlHead.Controls;
            HtmlLink htmlLink;
            string text;
            // no-index
            if (httpHead.NoIndex)
                htmlHeadControls.Add(new HtmlMeta { Name = "robots", Content = "noindex" });
            // icon
            if (!string.IsNullOrEmpty(text = httpHead.IconUri))
            {
                if (string.Compare(text, "/favicon.ico", StringComparison.InvariantCultureIgnoreCase) != 0)
                    throw new InvalidOperationException("InvalidHttpHeadIcon");
                // icon (browser type A)
                htmlLink = new HtmlLink { ID = HeaderControlId.Icon, Href = text };
                htmlLink.Attributes["rel"] = "icon";
                htmlLink.Attributes["type"] = "image/x-icon";
                htmlHeadControls.Add(htmlLink);
                // shortcut icon (browser type B)
                htmlLink = new HtmlLink { ID = HeaderControlId.ShortcutIcon, Href = text };
                htmlLink.Attributes["rel"] = "shortcut icon";
                htmlLink.Attributes["type"] = "image/x-icon";
                htmlHeadControls.Add(htmlLink);
            }
            // search
            if (!string.IsNullOrEmpty(text = httpHead.Search))
            {
                htmlLink = new HtmlLink { ID = HeaderControlId.Search, Href = text };
                string title = httpHead.SearchTitle;
                if (!string.IsNullOrEmpty(title))
                    htmlLink.Attributes["title"] = title;
                htmlLink.Attributes["rel"] = "search";
                htmlLink.Attributes["type"] = "application/opensearchdescription+xml";
                htmlHeadControls.Add(htmlLink);
            }
            // rss/atom
            var syndications = httpHead.Syndications;
            if (syndications != null)
            {
                string id = string.Empty;
                for (int syndicationIndex = 1; syndicationIndex < syndications.Length; syndicationIndex++)
                {
                    var syndication = syndications[syndicationIndex];
                    htmlLink = new HtmlLink { ID = HeaderControlId.Syndication + id, Href = syndication.Uri };
                    string title = syndication.Title;
                    if (!string.IsNullOrEmpty(title))
                        htmlLink.Attributes["title"] = title;
                    htmlLink.Attributes["rel"] = "alternate";
                    htmlLink.Attributes["type"] = (syndication.Format == HttpPage.WebSyndicationFormat.Atom ? "application/atom+xml" : "application/rss+xml");
                    htmlHeadControls.Add(htmlLink);
                    id = (syndicationIndex++).ToString();
                }
            }
            // page title
            if (!string.IsNullOrEmpty(text = httpHead.Title))
                htmlHeadControls.Add(new HtmlTitle { ID = HeaderControlId.Title, Text = text });
            // page keyword
            if (!string.IsNullOrEmpty(text = httpHead.Keywords))
                htmlHeadControls.Add(new HtmlMeta { ID = HeaderControlId.Keyword, Name = "keywords", Content = text });
            // page description
            if (!string.IsNullOrEmpty(text = httpHead.Description))
                htmlHeadControls.Add(new HtmlMeta() { ID = HeaderControlId.Description, Name = "description", Content = text });
            // add search tag
            if (!string.IsNullOrEmpty(text = httpHead.Tag))
                htmlHeadControls.Add(new HtmlMeta { ID = "Tag", Name = "tag", Content = text });
            // author
            if (!string.IsNullOrEmpty(text = httpHead.Author))
                htmlHeadControls.Add(new HtmlMeta { ID = "Author", Name = "author", Content = text });
            // copyright
            if (!string.IsNullOrEmpty(text = httpHead.Copyright))
                htmlHeadControls.Add(new HtmlMeta { ID = "Copyright", Name = "copyright", Content = text });
            // developer
            if (!string.IsNullOrEmpty(text = httpHead.Developer))
                htmlHeadControls.Add(new HtmlMeta { ID = "Developer", Name = "developer", Content = text });
        }
    }
}