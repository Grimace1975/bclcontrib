//using System;
//namespace Instinct.Http
//{
//    /// <summary>
//    /// HttpContext class
//    /// </summary>
//    public class HttpContext : Pattern.Generic.Singleton<HttpContext>, Collections.IIndexer<string, string>
//    {
//        protected System.Web.HttpResponse m_systemResponse;
//        protected System.Web.HttpRequest m_systemRequest;
//        private System.Collections.Generic.Dictionary<string, string> m_hash = new System.Collections.Generic.Dictionary<string, string>();
//        private System.Collections.Generic.Dictionary<string, string> m_stateHash = new System.Collections.Generic.Dictionary<string, string>();
//        //private IndexBase<string, string> m_peekStateIndex;
//        private System.Collections.Generic.Dictionary<string, string> m_formStateHash = new System.Collections.Generic.Dictionary<string, string>();
//        //private HttpUrl m_httpUrl;
//        //private string m_file;
//        //private bool m_isCookieEnable;
//        private string m_stateUrlCache;
//        //private string m_pageUid = string.Empty;
//        //private string m_pageTreeId = string.Empty;
//        //private string m_pageSectionUid = string.Empty;
//        //private bool m_isPageDefault = false;
//        //private bool m_isRender = true;
//        //private bool m_isForceSsl = false;
//        //private string m_networkResourceUrl;
//        //private string m_sharedNetworkSecureDomain;
//        //private string m_primaryNetworkDomain;
//        //private string m_primaryNetworkSecureDomain;
//        //private string m_networkDomain;
//        //private string m_networkSecureDomain;
//        private Environment.SessionProvider.HttpSessionProviderBase m_sessionProvider;
//        //private System.Web.UI.Page m_page;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="HttpContext"/> class.
//        /// </summary>
//        protected HttpContext()
//            : base()
//        {
//            //m_networkResourceUrl = HttpFactory.s_configSection.NetworkResourceUrl;
//            //m_sharedNetworkSecureDomain = HttpFactory.s_configSection.SharedNetworkSecureDomain;
//            //m_networkDomain = m_primaryNetworkDomain = HttpFactory.s_configSection.NetworkDomain;
//            //m_networkSecureDomain = m_primaryNetworkSecureDomain = HttpFactory.s_configSection.NetworkSecureDomain;
//            //m_systemResponse = System.Web.HttpContext.Current.Response;
//            //m_systemRequest = System.Web.HttpContext.Current.Request;
//            //m_peekStateIndex = new PeekStateIndex(this);
//            //m_file = m_systemRequest.Path;
//            //m_isCookieEnable = (Http.Cookie["DEG"] == "DEG");
//            //if (m_isCookieEnable == false)
//            //{
//            //    Http.Cookie["DEG"] = "DEG";
//            //}
//            if (m_stateUrlCache == string.Empty) { throw new Exception("COMPILER WARNING"); }
//        }

//        /// <summary>
//        /// Gets or sets the <see cref="System.String"/> with the specified key.
//        /// </summary>
//        /// <value></value>
//        public virtual string this[string key]
//        {
//            get
//            {
//                if (key.Length > 0)
//                {
//                    string value;
//                    if (key.StartsWith("#") == true)
//                    {
//                        return (m_hash.TryGetValue(key, out value) == true ? value : string.Empty);
//                    }
//                    else if (key.StartsWith("&") == true)
//                    {
//                        key = key.Substring(1);
//                        if (m_stateHash.TryGetValue(key, out value) == false)
//                        {
//                            m_stateUrlCache = null;
//                            //+ fetch
//                            value = (m_systemRequest.QueryString[key] ?? string.Empty);
//                            m_stateHash.Add(key, value);
//                        }
//                        return value;
//                    }
//                    else if (key.StartsWith("%") == true)
//                    {
//                        key = key.Substring(1);
//                        if (m_formStateHash.TryGetValue(key, out value) == false)
//                        {
//                            //+ fetch
//                            value = (m_systemRequest.Form[key] ?? string.Empty);
//                            m_formStateHash.Add(key, value);
//                        }
//                        return value;
//                    }
//                }
//                return (string)m_sessionProvider.GetValue(key);
//            }
//            set
//            {
//                if (key.Length > 0)
//                {
//                    if (key.StartsWith("#") == true)
//                    {
//                        m_hash[key] = (value ?? string.Empty);
//                        return;
//                    }
//                    else if (key.StartsWith("&") == true)
//                    {
//                        key = key.Substring(1);
//                        m_stateUrlCache = null;
//                        m_stateHash[key] = (value ?? string.Empty);
//                        return;
//                    }
//                    else if (key.StartsWith("%") == true)
//                    {
//                        key = key.Substring(1);
//                        m_formStateHash[key] = (value ?? string.Empty);
//                        return;
//                    }
//                }
//                m_sessionProvider.SetValue(key, (value ?? string.Empty));
//            }
//        }

//        /// <summary>
//        /// Sets the session provider.
//        /// </summary>
//        /// <param name="sessionProvider">The session provider.</param>
//        internal void SetSessionProvider(Environment.SessionProvider.HttpSessionProviderBase sessionProvider)
//        {
//            //if (m_sessionProvider != null)
//            //{
//            //    m_sessionProvider.Dispose();
//            //}
//            m_sessionProvider = sessionProvider;
//        }

//        //#region VIRTUALIZE
//        ////- Tag -//
//        //public class Tag
//        //{
//        //    public bool IsEnable;
//        //    public string Url;
//        //    public string PathTranslated;
//        //    public string RestoreRewritePath;
//        //}

//        ////- Virtualize -//
//        //public static void Virtualize(string url)
//        //{
//        //    Virtualize(url, url, null);
//        //}
//        //public static void Virtualize(string url, string actualUrl)
//        //{
//        //    Virtualize(url, actualUrl, null);
//        //}
//        //public static void Virtualize(string url, string actualUrl, string pathTranslated)
//        //{
//        //    System.Web.HttpContext context = System.Web.HttpContext.Current;
//        //    System.Collections.IDictionary hash = context.Items;
//        //    //+ check for redefined virtualize
//        //    if (hash.Contains(s_type) == true)
//        //    {
//        //        throw new InvalidOperationException(Local.RedefineVirtualize);
//        //    }
//        //    //+ check url parameter
//        //    if ((url == null) || (url.Length == 0))
//        //    {
//        //        throw new ArgumentException("url");
//        //    }
//        //    //+ check pathTranslated parameter
//        //    if (pathTranslated == null)
//        //    {
//        //        if ((actualUrl == null) || (actualUrl.Length <= 0) || (actualUrl.StartsWith("/") == false))
//        //        {
//        //            throw new ArgumentException("actualUrl");
//        //        }
//        //        pathTranslated = context.Request.PhysicalApplicationPath + actualUrl.Substring(1).Replace("/", "\\");
//        //    }
//        //    if ((pathTranslated == null) || (pathTranslated.Length == 0))
//        //    {
//        //        throw new ArgumentException("pathTranslated");
//        //    }
//        //    //+ extension check
//        //    Tag tag = new Tag();
//        //    tag.IsEnable = false;
//        //    tag.RestoreRewritePath = url;
//        //    context.RewritePath(actualUrl);
//        //    //+
//        //    //int pathTranslatedLength = pathTranslated.Length;
//        //    //string extension = (pathTranslatedLength >= 5 ? pathTranslated.Substring(pathTranslatedLength - 5, 5).ToLowerInvariant() : string.Empty);
//        //    //switch (extension)
//        //    //{
//        //    //    case ".aspx":
//        //    //    case ".ashx":
//        //    //    case ".asmx":
//        //    //        //if (actualUrl.EndsWith("/") == true) {
//        //    //        //   actualUrl = actualUrl.Substring(0, actualUrl.Length - 1);
//        //    //        //}
//        //    //        context.RewritePath(actualUrl);
//        //    //        ////+
//        //    //        //string queryString = null;
//        //    //        //int urlQueryIndex = actualUrl.IndexOf('?');
//        //    //        //if (urlQueryIndex >= 0) {
//        //    //        //   queryString = (urlQueryIndex < (actualUrl.Length - 1) ? actualUrl.Substring(urlQueryIndex + 1) : string.Empty);
//        //    //        //   actualUrl = actualUrl.Substring(0, urlQueryIndex);
//        //    //        //}
//        //    //        //context.RewritePath(actualUrl, string.Empty, queryString, true);
//        //    //        break;
//        //    //    default:
//        //    //        //if (url.EndsWith("/") == true) {
//        //    //        //   url = url.Substring(0, url.Length - 1);
//        //    //        //}
//        //    //        context.RewritePath(url);
//        //    //        break;
//        //    //}
//        //    hash[s_type] = tag;
//        //}

//        //////- SetRequestFilePath -//
//        ////public static void SetRequestFilePath(System.Web.HttpContext context, string path) {
//        ////   object virtualPath = s_virtualPathType.InvokeMember(s_virtualPathType.Name, System.Reflection.BindingFlags.CreateInstance | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, null, new object[] { path });
//        ////   s_httpRequestType.GetField("_filePath", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(context.Request, virtualPath);
//        ////}

//        ////- CheckVirtualizeRestore -//
//        //public static void CheckVirtualizeRestore()
//        //{
//        //    System.Web.HttpContext context = System.Web.HttpContext.Current;
//        //    //+ check for virtualize
//        //    System.Collections.IDictionary hash = context.Items;
//        //    if (hash.Contains(s_type) == true)
//        //    {
//        //        Tag tag = (Tag)hash[s_type];
//        //        string restoreRewritePath = tag.RestoreRewritePath;
//        //        if (restoreRewritePath != null)
//        //        {
//        //            //if (restoreRewritePath.EndsWith("/") == true) {
//        //            //   restoreRewritePath = restoreRewritePath.Substring(0, restoreRewritePath.Length - 1);
//        //            //}
//        //            context.RewritePath(restoreRewritePath);
//        //        }
//        //    }
//        //}
//        //#endregion VIRTUALIZE
//    }
//}
