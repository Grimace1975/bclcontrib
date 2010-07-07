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
using System.Threading;
namespace System.Patterns.Caching
{
    /// <summary>
    /// DataCache
    /// </summary>
    //: no way to seperate values and state. they are all used for a key
    //: could use generics so don't have to typecast result, maybe autofind the T from the registration
    public static class DataCache
    {
        /// <summary>
        /// NoHeaderId
        /// </summary>
        public const string NoHeaderId = "none";
        private static ReaderWriterLockSlim s_rwLock = new ReaderWriterLockSlim();

        #region Class Types
        /// <summary>
        /// Primitive
        /// </summary>
        public class Primitive
        {
            /// <summary>
            /// YesNo
            /// </summary>
            public static readonly DataCacheRegistration YesNo = new DataCacheRegistration("YesNo", delegate(string tag, object[] values)
            {
                var hash = new Dictionary<string, string>(3);
                switch (tag)
                {
                    case "":
                        hash.Add(string.Empty, "--");
                        break;
                    case NoHeaderId:
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                hash.Add(bool.TrueString, "Yes");
                hash.Add(bool.FalseString, "No");
                return hash;
            });
            /// <summary>
            /// Gender
            /// </summary>
            public static readonly DataCacheRegistration Gender = new DataCacheRegistration("Gender", delegate(string tag, object[] values)
            {
                var hash = new Dictionary<string, string>(3);
                switch (tag)
                {
                    case "":
                        hash.Add(string.Empty, "--");
                        break;
                    case NoHeaderId:
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                hash.Add("Male", "Male");
                hash.Add("Female", "Female");
                return hash;
            });
            /// <summary>
            /// Integer
            /// </summary>
            public static readonly DataCacheRegistration Integer = new DataCacheRegistration("Integer", delegate(string tag, object[] values)
            {
                var hash = new Dictionary<string, string>(3);
                switch (tag)
                {
                    case "":
                        hash.Add(string.Empty, "--");
                        break;
                    case NoHeaderId:
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                int startIndex = (int)values[0];
                int endIndex = (int)values[1];
                int indexStep = (int)values[2];
                for (int index = startIndex; index < endIndex; index += indexStep)
                    hash.Add(index.ToString(), index.ToString());
                return hash;
            });
        }
        #endregion

        /// <summary>
        /// Initializes the <see cref="DataCache"/> class.
        /// </summary>
        static DataCache()
        {
            var registrar = GetRegistrar(typeof(DataCache));
            registrar.AddData(Primitive.YesNo);
            registrar.AddData(Primitive.Gender);
            registrar.AddData(Primitive.Integer);
        }

        /// <summary>
        /// Gets the registrar.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static DataCacheRegistrar GetRegistrar(Type type)
        {
            DataCacheRegistrar dataSourceRegistrar;
            DataCacheRegistrar.TryGetInstance(type, out dataSourceRegistrar, true);
            return dataSourceRegistrar;
        }

        
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static object GetData(DataCacheRegistration registration) { return GetData<object>(registration, string.Empty, null); }
        public static T GetData<T>(DataCacheRegistration registration) { return GetData<T>(registration, string.Empty, null); }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="values">The value array.</param>
        /// <returns></returns>
        public static object GetData(DataCacheRegistration registration, object[] values) { return GetData<object>(registration, string.Empty, values); }
        public static T GetData<T>(DataCacheRegistration registration, object[] values) { return GetData<T>(registration, string.Empty, values); }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="headerId">The header id.</param>
        /// <returns></returns>
        public static object GetData(DataCacheRegistration registration, string tag) { return GetData<object>(registration, tag, null); }
        public static T GetData<T>(DataCacheRegistration registration, string tag) { return GetData<T>(registration, tag, null); }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static object GetData(Type type, string registrationId) { return GetData<object>(type, registrationId, string.Empty, null); }
        public static T GetData<T>(Type type, string registrationId) { return GetData<T>(type, registrationId, string.Empty, null); }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="values">The value array.</param>
        /// <returns></returns>
        public static object GetData(Type type, string registrationId, object[] values) { return GetData<object>(type, registrationId, string.Empty, values); }
        public static T GetData<T>(Type type, string registrationId, object[] values) { return GetData<T>(type, registrationId, string.Empty, values); }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="headerId">The header id.</param>
        /// <returns></returns>
        public static object GetData(Type type, string registrationId, string tag) { return GetData<object>(type, registrationId, tag, null); }
        public static T GetData<T>(Type type, string registrationId, string tag) { return GetData<T>(type, registrationId, tag, null); }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="headerId">The header id.</param>
        /// <param name="values">The value array.</param>
        /// <returns></returns>
        //:BIND: GetDataSource(Type, string, string, object[])
        public static object GetData(DataCacheRegistration registration, string tag, object[] values) { return GetData<object>(registration, tag, values); }
        public static T GetData<T>(DataCacheRegistration registration, string tag, object[] values)
        {
            if (registration == null)
                throw new ArgumentNullException("registration");
            if (tag == null)
                throw new ArgumentNullException("headerId");
            tag = tag.ToLowerInvariant();
            // fetch registration
            int recurses = 0;
            DataCacheRegistration foundRegistration;
            if (!DataCacheRegistrar.TryGetValue(registration, ref recurses, out foundRegistration))
                throw new InvalidOperationException(string.Format(Local.UndefinedDataSourceRegistrationAB, (registration.Registrar != null ? registration.Registrar.AnchorType.ToString() : "{unregistered}"), registration.Id));
            // fetch from cache
            var cacheCommand = foundRegistration.CacheCommand;
            string cacheKey = cacheCommand.Key + CoreEx.Scope + tag;
            var cache = foundRegistration.GetCacheSystem(values);
            s_rwLock.EnterUpgradeableReadLock();
            try
            {
                T dataSource;
                if ((dataSource = (T)cache[cacheKey]) == null)
                {
                    s_rwLock.EnterWriteLock();
                    try
                    {
                        if ((dataSource = (T)cache[cacheKey]) == null)
                        {
                            // create
                            dataSource = DataCacheRegistrar.CreateData<T>(foundRegistration, tag, values);
                            cache.AddInternal(cacheKey, cacheCommand, dataSource);
                        }
                    }
                    finally { s_rwLock.ExitWriteLock(); }
                }
                return dataSource;
            }
            finally { s_rwLock.ExitUpgradeableReadLock(); }
        }
        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="headerId">The header id.</param>
        /// <param name="values">The value array.</param>
        /// <returns></returns>
        //:BIND: GetDataSource(DataSourceRegistration, string, object[])
        public static object GetData(Type type, string registrationId, string tag, object[] values) { return GetData<object>(type, registrationId, tag, values); }
        public static T GetData<T>(Type type, string registrationId, string tag, object[] values)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException("registrationId");
            if (tag == null)
                throw new ArgumentNullException("headerId");
            tag = tag.ToLowerInvariant();
            // fetch registration
            int recurses = 0;
            DataCacheRegistration foundRegistration;
            if (!DataCacheRegistrar.TryGetValue(type, registrationId, ref recurses, out foundRegistration))
                throw new InvalidOperationException(string.Format(Local.UndefinedDataSourceRegistrationAB, type.ToString(), registrationId));
            // fetch from cache
            var cacheCommand = foundRegistration.CacheCommand;
            string cacheKey = cacheCommand.Key + CoreEx.Scope + tag;
            var cache = foundRegistration.GetCacheSystem(values);
            s_rwLock.EnterUpgradeableReadLock();
            try
            {
                T dataSource;
                if ((dataSource = (T)cache[cacheKey]) == null)
                {
                    s_rwLock.EnterWriteLock();
                    try
                    {
                        if ((dataSource = (T)cache[cacheKey]) == null)
                        {
                            // create
                            dataSource = DataCacheRegistrar.CreateData<T>(foundRegistration, tag, values);
                            cache.AddInternal(cacheKey, cacheCommand, dataSource);
                        }
                    }
                    finally { s_rwLock.ExitWriteLock(); }
                }
                return dataSource;
            }
            finally { s_rwLock.ExitUpgradeableReadLock(); }
        }

        /// <summary>
        /// Invalidates the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void InvalidateData(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            // fetch registration-hash
            DataCacheRegistrar registrar;
            if (!DataCacheRegistrar.TryGetInstance(type, out registrar, false))
                throw new InvalidOperationException(string.Format(Local.UndefinedDataSourceRegistrationA, type.ToString()));
            foreach (var registration in registrar.Registrations)
                InvalidateData(registration);
        }
        /// <summary>
        /// Invalidates the data source.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        public static void InvalidateData(Type type, string registrationId)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(registrationId))
                throw new ArgumentNullException("registrationId");
            // fetch registration
            int recurses = 0;
            DataCacheRegistration foundRegistration;
            if (!DataCacheRegistrar.TryGetValue(type, registrationId, ref recurses, out foundRegistration))
                throw new InvalidOperationException(string.Format(Local.UndefinedDataSourceRegistrationAB, type.ToString(), registrationId));
            InvalidateData(foundRegistration);
        }
        /// <summary>
        /// Invalidates the data source.
        /// </summary>
        /// <param name="registration">The registration.</param>
        public static void InvalidateData(DataCacheRegistration registration)
        {
            if (registration is DataCacheRegistrationLink)
                throw new InvalidOperationException(Local.InvalidDataSource);
            // remove from cache
            var cache = registration.GetCacheSystem(null);
            foreach (string headerId in registration.Tags)
                cache.Remove(registration.CacheCommand.Key + CoreEx.Scope + headerId);
        }
    }
}
