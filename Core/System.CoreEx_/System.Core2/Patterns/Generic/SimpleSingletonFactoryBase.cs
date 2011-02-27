//#region License
///*
//The MIT License

//Copyright (c) 2008 Sky Morey

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.
//*/
//#endregion
//using System.Threading;
//using System.Reflection;
//using System.Collections.Generic;
//namespace System.Patterns.Generic
//{
//    /// <summary>
//    /// SimpleSingletonFactoryBase
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    //+ [Singleton Pattern] http://www.yoda.arachsys.com/csharp/singleton.html
//    public class SimpleSingletonFactoryBase<T> : SimpleFactoryBase<T>
//        where T : class
//    {
//        private static readonly Dictionary<string, T> s_hash = new Dictionary<string, T>();
//        private static readonly MethodInfo s_createMethodInfo = typeof(T).GetMethod("Create", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(string) }, null);

//#if !SqlServer
//        private static readonly ReaderWriterLockSlim s_rwLock = new ReaderWriterLockSlim();

//        /// <summary>
//        /// Gets the specified key.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns></returns>
//        public static T Get(string key)
//        {
//            return s_rwLock.ThreadedGetWithCreate<T, string, Dictionary<string, T>>(s_hash, key, delegate(string key2)
//            {
//                return Create(key2);
//            });
//        }
//        /// <summary>
//        /// Gets this instance.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T2 Get<T2>()
//        {
//            return default(T2);
//        }
//#else
//        /// <summary>
//        /// Gets the specified key.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns></returns>
//        public static T Get(string key)
//        {
//            return null;
//        }
//#endif

//        /// <summary>
//        /// Creates the specified key.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns></returns>
//        private static T Create(string key)
//        {
//            return (T)s_createMethodInfo.Invoke(null, BindingFlags.InvokeMethod, null, new object[] { key }, null);
//        }
//    }
//}
//return ServiceLocatorEx.Resolve<TextProcessBase>(ResolveLifetime.ApplicationUnit, key, new { typeA = "System.Primitives.TextProcesses.{0}TextProcess, " + AssemblyRef.This });