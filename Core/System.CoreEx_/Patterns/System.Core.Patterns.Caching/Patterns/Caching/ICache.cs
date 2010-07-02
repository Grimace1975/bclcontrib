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
namespace System.Patterns.Caching
{
    /// <summary>
    /// ICache
    /// </summary>
    public interface ICache
    {
        object Add(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);

        /// <summary>
        /// Gets the item from cache associated with the key provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The cached item.</returns>
        object Get(string key);

        /// <summary>
        /// Removes from cache the item associated with the key provided.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The item removed from the Cache. If the value in the key parameter is not found, returns null.</returns>
        object Remove(string key);

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
        void Insert(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);

        /// <summary>
        /// Touches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Touch(string key);
    }
}