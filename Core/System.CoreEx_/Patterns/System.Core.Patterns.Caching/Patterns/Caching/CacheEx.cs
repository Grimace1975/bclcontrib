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
    /// Represents a generic cache-based storage object. Provides an abstraction from the inherent caching mechanism
    /// provides by a given Application Type (WebApplication, etc) and the generic implementation used. Also
    /// provides a basic facade pattern over the implicit ASP.NET Caching mechanism.
    /// </summary>
    public partial class CacheEx
    {
        /// <summary>
        /// Provides <see cref="System.DateTime"/> instance to be used when no absolute expiration value to be set.
        /// </summary>
        public static readonly DateTime NoAbsoluteExpiration = DateTime.MaxValue;
        /// <summary>
        /// Provides <see cref="System.TimeSpan"/> instance to be used when no sliding expiration value to be set.
        /// </summary>
        public static readonly TimeSpan NoSlidingExpiration = TimeSpan.Zero;
        private static CacheProviderBase s_cacheProvider = new StaticCacheProvider(); //KernelFactory.s_configSection.CacheProvider;
        private string _salt;
        public static readonly CacheEx Default = new CacheEx();
        public static readonly object s_lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache"/> class.
        /// </summary>
        internal CacheEx()
            : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Cache"/> class.
        /// </summary>
        /// <param name="salt">The salt.</param>
        internal CacheEx(string salt)
            : base()
        {
            _salt = salt;
        }

        public static void SetCacheProvider(Func<CacheProviderBase> cacheProvider)
        {
            lock (s_lock)
            {
                if (s_cacheProvider != null)
                    s_cacheProvider.Dispose();
                s_cacheProvider = (CacheProviderBase)cacheProvider();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value></value>
        public object this[string key]
        {
            get { return s_cacheProvider.Get(_salt == null ? key : _salt + key); }
            set
            {
                if (s_cacheProvider == null)
                    throw new ArgumentNullException();
                if (_salt != null)
                    key = _salt + key;
                s_cacheProvider.Insert(key, value, null, DateTime.Now.AddMinutes(60), CacheEx.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="value">The value to add to cache.</param>
        /// <param name="dependency">The dependency object to use.</param>
        /// <param name="absoluteExpiration">The absolute expiration to use to define when the cache entry becomes invalid.</param>
        /// <param name="slidingExpiration">The sliding expiration to use to determine when an unused cache entry becomes invalid.</param>
        /// <param name="priority">The cache item priority to apply.</param>
        /// <param name="onRemoveCallback">The delegate to invoke when the item is removed from cache.</param>
        /// <returns></returns>
        public object Add(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            // ensure-dependency
            if (dependency != null)
                EnsureDependency(dependency);
            // add item
            return s_cacheProvider.Add((_salt == null ? key : _salt + key), value, dependency, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// Adds the value provided to cache using the information provided by the CacheCommand instance provided.
        /// </summary>
        /// <param name="cacheCommand">The cache command.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Add(CacheCommand cacheCommand, object value)
        {
            if (cacheCommand == null)
                throw new ArgumentNullException("cacheCommand");
            return AddInternal(cacheCommand.Key, cacheCommand, value);
        }
        /// <summary>
        /// Adds the value provided to cache using the information provided by the CacheCommand instance provided.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="cacheCommand">The cache command.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal object AddInternal(string key, CacheCommand cacheCommand, object value)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            if (cacheCommand == null)
                throw new ArgumentNullException("cacheCommand");
            // ensure-dependency
            var dependency = cacheCommand.Dependency;
            if (dependency != null)
                EnsureDependency(dependency);
            // add item
            var itemAddedCallback = cacheCommand.ItemAddedCallback;
            if (itemAddedCallback != null)
                itemAddedCallback(key, value);
            return s_cacheProvider.Add((_salt == null ? key : _salt + key), value, dependency, cacheCommand.AbsoluteExpiration, cacheCommand.SlidingExpiration, cacheCommand.Priority, cacheCommand.ItemRemovedCallback);
        }

        /// <summary>
        /// Ensures the dependency.
        /// </summary>
        /// <param name="dependency">The dependency.</param>
        private void EnsureDependency(CacheDependency dependency)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            string[] cacheKeys = dependency.CacheKeys;
            if (cacheKeys != null)
                foreach (string cacheKey in cacheKeys)
                    s_cacheProvider.Add(cacheKey, string.Empty, null, CacheEx.NoAbsoluteExpiration, CacheEx.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="value">The value to add to cache.</param>
        /// <param name="dependency">The dependency object to use.</param>
        /// <param name="absoluteExpiration">The absolute expiration to use to define when the cache entry becomes invalid.</param>
        /// <param name="slidingExpiration">The sliding expiration to use to determine when an unused cache entry becomes invalid.</param>
        /// <param name="priority">The cache item priority to apply.</param>
        /// <param name="onRemoveCallback">The delegate to invoke when the item is removed from cache.</param>
        public void Insert(string key, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            // ensure-dependency
            if (dependency != null)
                EnsureDependency(dependency);
            // add item
            s_cacheProvider.Insert((_salt == null ? key : _salt + key), value, dependency, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// Adds the value provided to cache using the information provided by the CacheCommand instance provided.
        /// </summary>
        /// <param name="cacheCommand">The cache command.</param>
        /// <param name="value">The value.</param>
        public void Insert(CacheCommand cacheCommand, object value)
        {
            if (cacheCommand == null)
                throw new ArgumentNullException("cacheCommand");
            InsertInternal(cacheCommand.Key, cacheCommand, value);
        }
        /// <summary>
        /// Insert the value provided to cache using the information provided by the CacheCommand instance provided.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="cacheCommand">The cache command.</param>
        /// <param name="value">The value.</param>
        internal void InsertInternal(string key, CacheCommand cacheCommand, object value)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            if (cacheCommand == null)
                throw new ArgumentNullException("cacheCommand");
            // ensure-dependency
            var dependency = cacheCommand.Dependency;
            if (dependency != null)
                EnsureDependency(dependency);
            // add item
            var itemAddedCallback = cacheCommand.ItemAddedCallback;
            if (itemAddedCallback != null)
                itemAddedCallback(key, value);
            s_cacheProvider.Insert((_salt == null ? key : _salt + key), value, dependency, cacheCommand.AbsoluteExpiration, cacheCommand.SlidingExpiration, cacheCommand.Priority, cacheCommand.ItemRemovedCallback);
        }

        /// <summary>
        /// Removes the item from cache with the specific key.
        /// </summary>
        /// <param name="key">The cache item key.</param>
        /// <returns></returns>
        public object Remove(string key)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            return s_cacheProvider.Remove(_salt == null ? key : _salt + key);
        }
        /// <summary>
        /// Removes the item from cached associated with the information contained within the provide CacheCommand instance.
        /// </summary>
        /// <param name="cacheCommand">The cache command.</param>
        /// <returns></returns>
        public object Remove(CacheCommand cacheCommand)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            if (cacheCommand == null)
                throw new ArgumentNullException("cacheCommand");
            return s_cacheProvider.Remove(_salt == null ? cacheCommand.Key : _salt + cacheCommand.Key);
        }

        public static CacheEx GetNamespace(object[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");
            // add one additional item, so join ends with scope character.
            string[] valuesAsText = new string[values.Length + 1];
            for (int valueIndex = 0; valueIndex < values.Length; valueIndex++)
            {
                object value = values[valueIndex];
                valuesAsText[valueIndex] = (value != null ? "." + value.ToString() : string.Empty);
            }
            // set additional item to null incase declaration doesnt clear value
            valuesAsText[valuesAsText.Length - 1] = null;
            return new CacheEx(string.Join(CoreEx.Scope, valuesAsText));
        }

        /// <summary>
        /// Touches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Touch(string key)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            s_cacheProvider.Touch(key);
        }
        /// <summary>
        /// Touches the specified key array.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void Touch(params string[] keys)
        {
            if (s_cacheProvider == null)
                throw new ArgumentNullException();
            foreach (string key in keys)
                s_cacheProvider.Touch(key);
        }
    }
}
