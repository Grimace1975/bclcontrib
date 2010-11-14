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
    /// <summary>
    /// DataCacheRegistration
    /// </summary>
    public class DataCacheRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataCacheRegistration"/> class.
        /// </summary>
        internal DataCacheRegistration(string id)
        {
            // used for registration-links only
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");
            Id = id;
            CacheCommand = new CacheCommand("DataCacheRegistration", -1);
        }
        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="dependencyArray">The dependency array.</param>
        public DataCacheRegistration(string id, DataCacheBuilder builder, params string[] dependencies)
            : this(id, new CacheCommand("DataCacheRegistration"), builder, dependencies) { }
        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="minuteTimeout">The minute timeout.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="dependencyArray">The dependency array.</param>
        public DataCacheRegistration(string id, int minuteTimeout, DataCacheBuilder builder, params string[] dependencies)
            : this(id, new CacheCommand("DataCacheRegistration", minuteTimeout), builder, dependencies) { }
        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cacheCommand">The cache command.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="dependencyArray">The dependency array.</param>
        public DataCacheRegistration(string id, CacheCommand cacheCommand, DataCacheBuilder builder, params string[] dependencies)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");
            if (cacheCommand == null)
                throw new ArgumentNullException("cacheCommand");
            if (builder == null)
                throw new ArgumentNullException("builder");
            Id = id;
            Builder = builder;
            if ((dependencies != null) && (dependencies.Length > 0))
            {
                if (cacheCommand.Dependency != null)
                    throw new InvalidOperationException(Local.RedefineCacheDependency);
                cacheCommand.Dependency = new CacheDependency { CacheKeys = dependencies };
            }
            CacheCommand = cacheCommand;
            Tags = new List<string>();
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the cache command.
        /// </summary>
        /// <value>The cache command.</value>
        public CacheCommand CacheCommand { get; set; }

        /// <summary>
        /// Gets or sets the builder.
        /// </summary>
        /// <value>The builder.</value>
        public DataCacheBuilder Builder { get; set; }

        /// <summary>
        /// Tags
        /// </summary>
        internal List<string> Tags;

        /// <summary>
        /// Registrar
        /// </summary>
        internal DataCacheRegistrar Registrar;

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        internal CacheEx GetCacheSystem(object[] values)
        {
            return (values == null ? CacheEx.Default : CacheEx.GetNamespace(values));
        }
    }
}
