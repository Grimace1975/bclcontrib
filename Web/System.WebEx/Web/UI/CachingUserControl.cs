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
using System.Web.Caching;
namespace System.Web.UI
{
    /// <summary>
    /// CachingUserControl
    /// </summary>
    [PartialCaching(3600)]
    public class CachingUserControl : UserControl
    {
        public CachingUserControl()
        {
            CacheDuration = new TimeSpan(1, 0, 0);
            EnableCaching = false; // Http.EnableCaching;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var parentControl = (Parent as BasePartialCachingControl);
            if (parentControl == null)
                return;
            if (EnableCaching)
            {
                parentControl.CachePolicy.Duration = CacheDuration;
                if (!string.IsNullOrEmpty(CacheVaryByControl))
                    parentControl.CachePolicy.VaryByControl = CacheVaryByControl;
                if (!string.IsNullOrEmpty(CacheVaryByCustom))
                    parentControl.CachePolicy.SetVaryByCustom(CacheVaryByCustom);
                if (!string.IsNullOrEmpty(CacheKeyDependency))
                {
                    string[] cacheKeys = CacheKeyDependency.Split(',');
                    // add cache dependency keys if they aren't already in cache
                    foreach (string cacheKey in cacheKeys)
                        if (Cache.Get(cacheKey) == null)
                            Cache.Add(cacheKey, string.Empty, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    // add key dependency
                    parentControl.CachePolicy.Dependency = new CacheDependency(null, cacheKeys);
                }
            }
            else
                parentControl.CachePolicy.Cached = false;
        }

        /// <summary>
        /// Gets or sets the duration of the cache.
        /// </summary>
        /// <value>The duration of the cache.</value>
        public TimeSpan CacheDuration { get; set; }

        /// <summary>
        /// Gets or sets the cache key dependency.
        /// </summary>
        /// <value>The cache key dependency.</value>
        public string CacheKeyDependency { get; set; }

        /// <summary>
        /// Gets or sets the cache vary by control.
        /// </summary>
        /// <value>The cache vary by control.</value>
        public string CacheVaryByControl { get; set; }

        /// <summary>
        /// Gets or sets the cache vary by custom.
        /// </summary>
        /// <value>The cache vary by custom.</value>
        public string CacheVaryByCustom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable caching].
        /// </summary>
        /// <value><c>true</c> if [enable caching]; otherwise, <c>false</c>.</value>
        public bool EnableCaching { get; set; }
    }
}