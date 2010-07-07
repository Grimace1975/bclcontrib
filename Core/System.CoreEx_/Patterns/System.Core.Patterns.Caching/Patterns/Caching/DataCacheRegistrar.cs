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
using System.Reflection;
using System.Threading;
namespace System.Patterns.Caching
{
    /// <summary>
    /// DataCacheRegistrar
    /// </summary>
    public class DataCacheRegistrar
    {
        private static ReaderWriterLockSlim s_rwLock = new ReaderWriterLockSlim();
        private static Dictionary<Type, DataCacheRegistrar> s_hash = new Dictionary<Type, DataCacheRegistrar>();
        private ReaderWriterLockSlim _setRwLock = new ReaderWriterLockSlim();
        private HashSet<DataCacheRegistration> _set = new HashSet<DataCacheRegistration>();
        private Dictionary<string, DataCacheRegistration> _setAsId = new Dictionary<string, DataCacheRegistration>();
        private string _cacheKeyPrefix;

        /// <summary>
        /// Tries the get instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="registrar">The registrar.</param>
        /// <returns></returns>
        public static bool TryGetInstance(Type anchorType, out DataCacheRegistrar registrar, bool createIfRequired)
        {
            if (anchorType == null)
                throw new ArgumentNullException("anchorType");
            s_rwLock.EnterUpgradeableReadLock();
            try
            {
                bool exists = s_hash.TryGetValue(anchorType, out registrar);
                if ((exists) || (!createIfRequired))
                    return exists;
                s_rwLock.EnterWriteLock();
                try
                {
                    if (!s_hash.TryGetValue(anchorType, out registrar))
                    {
                        // create
                        registrar = new DataCacheRegistrar(anchorType);
                        s_hash.Add(anchorType, registrar);
                    }
                }
                finally { s_rwLock.ExitWriteLock(); }
                return true;
            }
            finally { s_rwLock.ExitUpgradeableReadLock(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceRegistrar"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        internal DataCacheRegistrar(Type anchorType)
        {
            _cacheKeyPrefix = "ds" + CoreEx.Scope + anchorType.ToString() + CoreEx.Scope;
            AnchorType = anchorType;
        }

        /// <summary>
        /// Gets or sets the type of the anchor.
        /// </summary>
        /// <value>The type of the anchor.</value>
        public Type AnchorType { get; private set; }

        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="dependencies">The dependencies.</param>
        public void AddData(string registrationId, DataCacheBuilder builder, params string[] dependencies)
        {
            AddData(new DataCacheRegistration(registrationId, new CacheCommand(null, 60), builder, dependencies));
        }
        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="minuteTimeout">The minute timeout.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="dependencies">The dependencies.</param>
        public void AddData(string registrationId, int minuteTimeout, DataCacheBuilder builder, params string[] dependencies)
        {
            AddData(new DataCacheRegistration(registrationId, new CacheCommand(null, minuteTimeout), builder, dependencies));
        }
        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="cacheCommand">The cache command.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="dependencies">The dependencies.</param>
        public void AddData(string registrationId, CacheCommand cacheCommand, DataCacheBuilder builder, params string[] dependencies)
        {
            AddData(new DataCacheRegistration(registrationId, cacheCommand, builder, dependencies));
        }
        /// <summary>
        /// Adds the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        public void AddData(DataCacheRegistration registration)
        {
            if (registration == null)
                throw new ArgumentNullException("registration");
            _setRwLock.EnterWriteLock();
            try
            {
                if (_set.Contains(registration))
                    throw new InvalidOperationException(string.Format(Local.RedefineDataSourceCacheAB, AnchorType.ToString(), registration.Id));
                // add
                string registrationId = registration.Id;
                if (string.IsNullOrEmpty(registrationId))
                    throw new ArgumentNullException("registration.Id");
                if (registrationId.IndexOf(CoreEx.Scope) > -1)
                    throw new ArgumentException(string.Format(Local.ScopeCharacterNotAllowedA, registrationId), "registration");
                if (_setAsId.ContainsKey(registrationId))
                    throw new ArgumentException(string.Format("RedefinedKey{0}", registrationId), "registration");
                _setAsId.Add(registrationId, registration);
                _set.Add(registration);
                registration.Registrar = this;
                // adjust cache-command
                registration.CacheCommand.Key = _cacheKeyPrefix + registrationId;
            }
            finally { _setRwLock.ExitWriteLock(); }
        }

        /// <summary>
        /// Clears this the underlying Hash class instance.
        /// </summary>
        public void Clear()
        {
            _setRwLock.EnterWriteLock();
            try
            {
                _setAsId.Clear();
                _set.Clear();
            }
            finally { _setRwLock.ExitWriteLock(); }
        }

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(DataCacheRegistration key)
        {
            _setRwLock.EnterReadLock();
            try
            {
                return _set.Contains(key);
            }
            finally { _setRwLock.ExitReadLock(); }
        }
        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string key)
        {
            _setRwLock.EnterReadLock();
            try
            {
                return _setAsId.ContainsKey(key);
            }
            finally { _setRwLock.ExitReadLock(); }
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(DataCacheRegistration registration)
        {
            _setRwLock.EnterWriteLock();
            try
            {
                _setAsId.Remove(registration.Id);
                return _set.Remove(registration);
            }
            finally { _setRwLock.ExitWriteLock(); }
        }
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(string registrationId)
        {
            _setRwLock.EnterWriteLock();
            try
            {
                var registration = _setAsId[registrationId];
                _setAsId.Remove(registrationId);
                return _set.Remove(registration);
            }
            finally { _setRwLock.ExitWriteLock(); }
        }

        /// <summary>
        /// Creates the data source.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="headerId">The header id.</param>
        /// <param name="values">The value array.</param>
        /// <returns></returns>
        internal static T CreateData<T>(DataCacheRegistration registration, string tag, object[] values)
        {
            if (registration is DataCacheRegistrationLink)
                throw new InvalidOperationException(Local.InvalidDataSource);
            // append header-list
            var tags = registration.Tags;
            if (!tags.Contains(tag))
                tags.Add(tag);
            // build data-source
            return (T)registration.Builder(tag, values);
        }

        /// <summary>
        /// Gets the registrations.
        /// </summary>
        /// <value>The registrations.</value>
        internal HashSet<DataCacheRegistration> Registrations
        {
            get { return _set; }
        }

        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="recurses">The recurses.</param>
        /// <param name="registration">The registration.</param>
        /// <returns></returns>
        //: BIND: TryGetValue(System.Type, string, int, DataSourceRegistration)
        internal static bool TryGetValue(DataCacheRegistration registration, ref int recurses, out DataCacheRegistration foundRegistration)
        {
            s_rwLock.EnterReadLock();
            try
            {
                var registrar = registration.Registrar;
                if (registrar != null)
                {
                    // local check
                    var registrationLink = (registration as DataCacheRegistrationLink);
                    if (registrationLink == null)
                    {
                        foundRegistration = registration;
                        return true;
                    }
                    // foreign recurse
                    if (recurses++ > 4)
                        throw new InvalidOperationException(Local.ExceedRecurseCount);
                    // touch - starts foreign static constructor
                    var foreignType = registrationLink.ForeignType;
                    foreignType.InvokeMember("Touch", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static, null, null, null);
                    return TryGetValue(foreignType, registrationLink.ForeignId, ref recurses, out foundRegistration);
                }
                foundRegistration = null;
                return false;
            }
            finally { s_rwLock.ExitReadLock(); }
        }
        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="recurses">The recurses.</param>
        /// <param name="registration">The registration.</param>
        /// <returns></returns>
        //:BIND: TryGetValue(DataSourceRegistration, int, DataSourceRegistration)
        internal static bool TryGetValue(Type type, string registrationId, ref int recurses, out DataCacheRegistration foundRegistration)
        {
            s_rwLock.EnterReadLock();
            try
            {
                DataCacheRegistrar registrar;
                if (s_hash.TryGetValue(type, out registrar))
                {
                    //+ registration locals
                    var setRwLock = registrar._setRwLock;
                    var setAsId = registrar._setAsId;
                    setRwLock.EnterReadLock();
                    try
                    {
                        DataCacheRegistration registration;
                        if (setAsId.TryGetValue(registrationId, out registration))
                        {
                            // local check
                            var registrationLink = (registration as DataCacheRegistrationLink);
                            if (registrationLink == null)
                            {
                                foundRegistration = registration;
                                return true;
                            }
                            // foreign recurse
                            if (recurses++ > 4)
                                throw new InvalidOperationException(Local.ExceedRecurseCount);
                            // touch - starts foreign static constructor
                            var foreignType = registrationLink.ForeignType;
                            foreignType.InvokeMember("Touch", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static, null, null, null);
                            return TryGetValue(foreignType, registrationLink.ForeignId, ref recurses, out foundRegistration);
                        }
                    }
                    finally { setRwLock.ExitReadLock(); }
                }
                foundRegistration = null;
                return false;
            }
            finally { s_rwLock.ExitReadLock(); }
        }
    }
}
