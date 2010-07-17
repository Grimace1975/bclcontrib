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
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
namespace System.Patterns.Generic
{
    /// <summary>
    /// SimpleFactoryBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SimpleFactoryBase<TBase>
        where TBase : class
    {
        private static readonly MethodInfo s_getMethodInfo = typeof(TBase).GetGenericMethod("Get", new[] { typeof(TBase) }, null);
        private static readonly MethodInfo s_get2MethodInfo = typeof(TBase).GetGenericMethod("Get", new[] { typeof(TBase), typeof(IAppUnit) }, new[] { typeof(string) });
        private static readonly MethodInfo s_createMethodInfo = typeof(TBase).GetMethod("Create", BindingFlags.NonPublic | BindingFlags.Static, null, new[] { typeof(Type) }, null);

        protected class DelegateFactoryByAppUnit<T, TAppUnit>
        {
            public static readonly T Value = Create(typeof(T));

            private static T Create(Type type)
            {
                return (T)s_createMethodInfo.Invoke(null, BindingFlags.InvokeMethod, null, new object[] { type }, null);
            }
        }

        public static T Get<T>()
            where T : TBase { return DelegateFactoryByAppUnit<T, DefaultAppUnit>.Value; }
        public static T Get<T, TAppUnit>()
            where T : TBase
            where TAppUnit : IAppUnit { return DelegateFactoryByAppUnit<T, TAppUnit>.Value; }
        public static TBase Get<TAppUnit>(string id)
            where TAppUnit : IAppUnit
        {
            throw new NotSupportedException();
        }

        public static TBase Get<TAppUnit>(Type type)
            where TAppUnit : IAppUnit { return (TBase)s_getMethodInfo.MakeGenericMethod(type, typeof(TAppUnit)).Invoke(null, null); }
        public static TBase Get(string id) { return (TBase)s_get2MethodInfo.MakeGenericMethod(DefaultAppUnit.Type).Invoke(null, null); }
        public static TBase Get(Type type) { return (TBase)s_getMethodInfo.MakeGenericMethod(type, DefaultAppUnit.Type).Invoke(null, null); }
    }
}
