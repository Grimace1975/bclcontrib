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
using System.Collections.Generic;
using System.Web;
namespace System.Patterns.Caching
{
    using WebCacheDependency = System.Web.Caching.CacheDependency;
    using WebCacheItemPriority = System.Web.Caching.CacheItemPriority;
    using WebCacheItemRemovedCallback = System.Web.Caching.CacheItemRemovedCallback;
    using WebCacheItemRemovedReason = System.Web.Caching.CacheItemRemovedReason;

    //: might need to make thread safe
    /// <summary>
    /// Provides the core factory method mechanism for generating or accessing a singleton-based Cache Provider.
    /// </summary>

    public class WebEnvironmentCacheProvider : CacheProviderBase, ICache
    {
        public WebEnvironmentCacheProvider() { }

        #region Class Types
        private class CacheItemRemovedTranslator
        {
            private CacheItemRemovedCallback _onRemoveCallback;

            public CacheItemRemovedTranslator(CacheItemRemovedCallback onRemoveCallback)
            {
                _onRemoveCallback = onRemoveCallback;
            }

            public void ItemRemovedCallback(string key, object value, WebCacheItemRemovedReason cacheItemRemovedReason)
            {
                _onRemoveCallback(key, value);
            }
        }
        #endregion

        public override object Add(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            // dependency
            var dependency2 = (dependency != null ? new WebCacheDependency(dependency.FilePaths, dependency.CacheKeys, dependency.StartDate) : null);
            // item priority
            WebCacheItemPriority cacheItemPriority;
            switch (priority)
            {
                case CacheItemPriority.AboveNormal:
                    cacheItemPriority = WebCacheItemPriority.AboveNormal;
                    break;
                case CacheItemPriority.BelowNormal:
                    cacheItemPriority = WebCacheItemPriority.BelowNormal;
                    break;
                case CacheItemPriority.High:
                    cacheItemPriority = WebCacheItemPriority.High;
                    break;
                case CacheItemPriority.Low:
                    cacheItemPriority = WebCacheItemPriority.Low;
                    break;
                case CacheItemPriority.Normal:
                    cacheItemPriority = WebCacheItemPriority.Normal;
                    break;
                case CacheItemPriority.NotRemovable:
                    cacheItemPriority = WebCacheItemPriority.NotRemovable;
                    break;
                default:
                    cacheItemPriority = WebCacheItemPriority.Default;
                    break;
            }
            // item removed callback
            var cacheItemRemovedCallback = (onRemoveCallback != null ? new WebCacheItemRemovedCallback(new CacheItemRemovedTranslator(onRemoveCallback).ItemRemovedCallback) : null);
            return HttpRuntime.Cache.Add(key, value, dependency2, absoluteExpiration, slidingExpiration, cacheItemPriority, cacheItemRemovedCallback);
        }

        public override object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        public override void Insert(string key, object value, CacheDependency dependency, System.DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            // dependency
            var dependency2 = (dependency != null ? new WebCacheDependency(dependency.FilePaths, dependency.CacheKeys, dependency.StartDate) : null);
            // item priority
            WebCacheItemPriority cacheItemPriority;
            switch (priority)
            {
                case CacheItemPriority.AboveNormal:
                    cacheItemPriority = WebCacheItemPriority.AboveNormal;
                    break;
                case CacheItemPriority.BelowNormal:
                    cacheItemPriority = WebCacheItemPriority.BelowNormal;
                    break;
                case CacheItemPriority.High:
                    cacheItemPriority = WebCacheItemPriority.High;
                    break;
                case CacheItemPriority.Low:
                    cacheItemPriority = WebCacheItemPriority.Low;
                    break;
                case CacheItemPriority.Normal:
                    cacheItemPriority = WebCacheItemPriority.Normal;
                    break;
                case CacheItemPriority.NotRemovable:
                    cacheItemPriority = WebCacheItemPriority.NotRemovable;
                    break;
                default:
                    cacheItemPriority = WebCacheItemPriority.Default;
                    break;
            }
            // item removed callback
            var cacheItemRemovedCallback = (onRemoveCallback != null ? new WebCacheItemRemovedCallback(new CacheItemRemovedTranslator(onRemoveCallback).ItemRemovedCallback) : null);
            HttpRuntime.Cache.Insert(key, value, dependency2, absoluteExpiration, slidingExpiration, cacheItemPriority, cacheItemRemovedCallback);
        }

        public override object Remove(string key)
        {
            return HttpRuntime.Cache.Remove(key);
        }

        public override void Touch(string key)
        {
            Insert(key, string.Empty, null, CacheEx.NoAbsoluteExpiration, CacheEx.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
    }
}
