//namespace System.Patterns.Schema
//{
//    //- HttpUrl -//
//    public class HttpUrl : Hash<string, HttpUrlStateBase>
//    {
//        private string m_httpUrl;
//        private int m_httpUrlIndex;
//        private string m_urlRoot;
//        private bool m_isAllowUrlCache = true;
//        private string m_urlCache;
//        private ApplicationType m_applicationType = ApplicationType.WebApplication;
//        private HttpUrlSubType m_subType = HttpUrlSubType.None;
//        private bool m_isAllowVirtualize = true;
//        internal string[] m_stateArray;
//        private string m_defaultCulture = string.Empty;

//        //- HttpUrlSubType -//
//        public enum HttpUrlSubType
//        {
//            None,
//            Rss
//        }

//        //- Main -//
//        public HttpUrl()
//            : base()
//        {
//            System.Web.HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
//            m_urlRoot = httpRequest.ApplicationPath;
//            if (m_urlRoot != "/")
//            {
//                m_urlRoot += "/";
//            }
//            m_httpUrl = httpRequest.Path;
//            if (m_httpUrl == "/")
//            {
//                m_httpUrl = "/Index_";
//            }
//            m_httpUrlIndex = m_urlRoot.Length - 1;
//        }
//        public HttpUrl(string urlRoot, string httpUrl)
//            : base()
//        {
//            m_urlRoot = urlRoot;
//            if (m_urlRoot != "/")
//            {
//                m_urlRoot += "/";
//            }
//            m_httpUrl = httpUrl;
//            if (m_httpUrl == "/")
//            {
//                m_httpUrl = "/Index_";
//            }
//            m_httpUrlIndex = m_urlRoot.Length - 1;
//        }

//        //- ApplicationType -//
//        public ApplicationType ApplicationType
//        {
//            get
//            {
//                return m_applicationType;
//            }
//            set
//            {
//                m_applicationType = value;
//            }
//        }

//        //- ApplicationUnitSegment -//
//        public string ApplicationUnitSegment
//        {
//            get
//            {
//                return m_urlRoot;
//            }
//            set
//            {
//                if (!value.EndsWith("/"))
//                {
//                    throw new ArgumentException(string.Format(Local.InvalidValueA, value), "value");
//                }
//                m_urlRoot = value;
//                m_httpUrlIndex = m_urlRoot.Length - 1;
//            }
//        }

//        /// <summary>
//        /// CreateUrlAttrib
//        /// </summary>
//        public class CreateUrlAttrib
//        {
//            internal const string _NoUrlRoot = "noUrlRoot";
//            internal const string _NoUrlState = "noUrlState";
//            public const string NoUrlRoot = "noUrlRoot";
//            public const string NoUrlState = "noUrlState";
//        }

//        //- CreateUrl -//
//        public string CreateUrl(string url, Attrib attrib)
//        {
//            if (url == null)
//            {
//                throw new ArgumentNullException(url);
//            }
//            if (!url.StartsWith("/"))
//            {
//                throw new ArgumentException(Local.InvalidUrl);
//            }
//            bool isAttrib = ((attrib != null) && (attrib.Count > 0));
//            bool isAllowUrlCache = ((m_isAllowUrlCache) && (!isAttrib));
//            if (isAllowUrlCache)
//            {
//                if (m_isKeyListDirty)
//                {
//                    m_isKeyListDirty = false;
//                    m_urlCache = null;
//                }
//                else if (m_urlCache != null)
//                {
//                    return m_urlCache + url.Substring(1);
//                }
//            }
//            //+ create url
//            bool isUrlRoot;
//            bool isUrlState;
//            if (isAttrib)
//            {
//                isUrlRoot = !attrib.SliceBit(CreateUrlAttrib._NoUrlRoot);
//                isUrlState = !attrib.SliceBit(CreateUrlAttrib._NoUrlState);
//            }
//            else
//            {
//                isUrlRoot = true;
//                isUrlState = true;
//            }
//            System.Text.StringBuilder textBuilder = new System.Text.StringBuilder(isUrlRoot ? m_urlRoot : "/");
//            if (isUrlState)
//            {
//                foreach (string httpUrlStateKey in KeyEnum)
//                {
//                    HttpUrlStateBase httpUrlState = this[httpUrlStateKey];
//                    textBuilder.Append(httpUrlState.CreateUrlFragment((!isAttrib) || (!attrib.IsExist(httpUrlStateKey)) ? null : attrib[httpUrlStateKey]));
//                }
//            }
//            string createdUrl = textBuilder.ToString();
//            if (isAllowUrlCache)
//            {
//                m_urlCache = createdUrl;
//            }
//            return createdUrl + url.Substring(1);
//        }

//        //- DefaultCulture -//
//        public string DefaultCulture
//        {
//            get
//            {
//                return m_defaultCulture;
//            }
//            set
//            {
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                m_defaultCulture = value;
//            }
//        }

//        //- DisallowUrlCache -//
//        public void DisallowUrlCache()
//        {
//            m_isAllowUrlCache = false;
//        }

//        //- IncreaseUrlKey -//
//        public void IncreaseUrl(int value)
//        {
//            m_httpUrlIndex += value;
//        }

//        //- IsAllowVirtualize -//
//        public bool IsAllowVirtualize
//        {
//            get
//            {
//                return m_isAllowVirtualize;
//            }
//            set
//            {
//                m_isAllowVirtualize = value;
//            }
//        }

//        //- LocalUrl -//
//        public string LocalUrl
//        {
//            get
//            {
//                return m_httpUrl.Substring(m_httpUrlIndex);
//            }
//        }

//        //- RedirectIfUrlOverflow -//
//        public void RedirectIfUrlOverflow()
//        {
//            int overflow = m_httpUrlIndex - m_httpUrl.Length + 1;
//            if (overflow > 0)
//            {
//                string normalizedLocalUrl = m_httpUrl + new string('/', overflow);
//                //+
//                System.Web.HttpContext httpContext = System.Web.HttpContext.Current;
//                Uri requestUrl = httpContext.Request.Url;
//                //string redirectUrl = requestUrl.Scheme + Uri.SchemeDelimiter + requestUrl.Authority + normalizedLocalUrl + requestUrl.Query;
//                string redirectUrl = normalizedLocalUrl + requestUrl.Query;
//                Http.PermanentRedirect(redirectUrl);
//            }
//        }

//        //- SetLocalUrl -//
//        public void SetLocalUrl(string url)
//        {
//            if (url == null)
//            {
//                throw new ArgumentNullException("url");
//            }
//            m_httpUrl = m_httpUrl.Substring(0, m_httpUrlIndex) + url;
//        }

//        //- StateArray -//
//        //: should not be here. just temporary. finally should rest in HttpEngineUrlState
//        public string[] StateArray
//        {
//            get
//            {
//                return m_stateArray;
//            }
//        }

//        //- SubType -//
//        public HttpUrlSubType SubType
//        {
//            get
//            {
//                return m_subType;
//            }
//            set
//            {
//                m_subType = value;
//            }
//        }

//        //- VirtualUrl -//
//        public string VirtualUrl
//        {
//            get
//            {
//                return m_urlRoot + m_httpUrl.Substring(m_httpUrlIndex + 1);
//            }
//        }
//    }
//}