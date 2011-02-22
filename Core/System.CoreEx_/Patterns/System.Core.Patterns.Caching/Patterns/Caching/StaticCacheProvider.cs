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
namespace System.Patterns.Caching
{
    //: might need to make thread safe
    /// <summary>
    /// Provides the core factory method mechanism for generating or accessing a singleton-based Cache Provider.
    /// </summary>
    public class StaticCacheProvider : CacheProviderBase
    {
        protected static Dictionary<string, object> Hash = new Dictionary<string, object>();

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticCacheProvider"/> class.
		/// </summary>
		public StaticCacheProvider() { }

        /// <summary>
        /// Adds an object into cache based on the parameters provided.
        /// </summary>
        /// <param name="key">The key used to identify the item in cache.</param>
        /// <param name="value">The value to store in cache.</param>
        /// <param name="dependency">The dependency object defining caching validity dependencies.</param>
        /// <param name="absoluteExpiration">The absolute expiration value used to determine when a cache item must be considerd invalid.</param>
        /// <param name="slidingExpiration">The sliding expiration value used to determine when a cache item is considered invalid due to lack of use.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="onRemoveCallback">The delegate to invoke when the item is removed from cache.</param>
        /// <returns></returns>
        public override object Add(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            // TODO: Throw on dependency or other stuff not supported by this simple system
            object lastValue;
			if (!Hash.TryGetValue(key, out lastValue))
            {
				Hash[key] = value;
                return null;
            }
            return lastValue;
        }

        /// <summary>
        /// Gets the item from cache associated with the key provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The cached item.</returns>
        public override object Get(string key)
        {
            object value;
			return (Hash.TryGetValue(key, out value) ? value : null);
        }

        /// <summary>
        /// Adds an object into cache based on the parameters provided.
        /// </summary>
        /// <param name="key">The key used to identify the item in cache.</param>
        /// <param name="value">The value to store in cache.</param>
        /// <param name="dependency">The dependency object defining caching validity dependencies.</param>
        /// <param name="absoluteExpiration">The absolute expiration value used to determine when a cache item must be considerd invalid.</param>
        /// <param name="slidingExpiration">The sliding expiration value used to determine when a cache item is considered invalid due to lack of use.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="onRemoveCallback">The delegate to invoke when the item is removed from cache.</param>
        public override void Insert(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
			Hash[key] = value;
        }

        /// <summary>
        /// Removes from cache the item associated with the key provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The item removed from the Cache. If the value in the key parameter is not found, returns null.
        /// </returns>
        public override object Remove(string key)
        {
            object value;
			if (Hash.TryGetValue(key, out value))
            {
				Hash.Remove(key);
                return value;
            }
            return null;
        }

        /// <summary>
        /// Touches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void Touch(string key)
        {
			Hash.Clear();
        }
    }
}
