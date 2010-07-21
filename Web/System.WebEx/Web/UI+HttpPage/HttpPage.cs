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
namespace System.Web.UI
{
    /// <summary>
    /// HttpPage
    /// </summary>
    public class HttpPage
    {
        public enum WebSyndicationFormat
        {
            Atom,
            Rss,
        }

        public struct WebSyndication
        {
            public string Uri { get; set; }
            public string Title { get; set; }
            public WebSyndicationFormat Format { get; set; }
        }

        public class HttpHead : IHttpPageMetatag
        {
            public bool NoIndex { get; set; }
            public string Title { get; set; }
            public string Keywords { get; set; }
            public string Tag { get; set; }
            public string Author { get; set; }
            public string Developer { get; set; }
            public string Copyright { get; set; }
            public string Description { get; set; }
            public string IconUri { get; set; }
            public WebSyndication[] Syndications { get; set; }
            public string Search { get; set; }
            public string SearchTitle { get; set; }
            public HttpHead()
            {
                Developer = "Local.DeveloperName";
            }
        }

        /// <summary>
        /// HttpPageCachingApproach
        /// </summary>
        public enum HttpPageCachingApproach
        {
            /// <summary>
            /// Default
            /// </summary>
            Default,
            /// <summary>
            /// LastModifyDateWithoutCaching
            /// </summary>
            LastModifyDateWithoutCaching,
            /// <summary>
            /// NoCache
            /// </summary>
            NoCache,
        }

        public HttpPage()
        {
            Head = new HttpHead();
        }

        public HttpHead Head { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public HttpPageCachingApproach CachingApproach { get; set; }
    }
}