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
namespace System.Quality
{
    /// <summary>
    /// ServiceLocator
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Type s_wantToSkipServiceLocatorType = typeof(IWantToSkipServiceLocator);

        public static IServiceRegistrar GetRegistrar() { return ServiceLocatorManager.Current.GetRegistrar(); }
        public static TServiceRegistrar GetRegistrar<TServiceRegistrar>()
            where TServiceRegistrar : class, IServiceRegistrar { return ServiceLocatorManager.Current.GetRegistrar<TServiceRegistrar>(); }
        //
        public static T Resolve<T>()
            where T : class { return ServiceLocatorManager.Current.Resolve<T>(); }
        public static T Resolve<T>(string id, params object[] args)
            where T : class { return ServiceLocatorManager.Current.Resolve<T>(id); }
        public static object Resolve(Type type) { return ServiceLocatorManager.Current.Resolve(type); }
        //
        public static IEnumerable<T> ResolveAll<T>()
            where T : class { return ServiceLocatorManager.Current.ResolveAll<T>(); }
        public static TService Inject<TService>(TService instance)
            where TService : class { return ServiceLocatorManager.Current.Inject<TService>(instance); }

        public static bool GetWantsToSkipLocator<T>() { return GetWantsToSkipLocator(typeof(T)); }
        public static bool GetWantsToSkipLocator(Type type)
        {
            return ((type == null) || (type.IsAssignableFrom(s_wantToSkipServiceLocatorType)));
        }
    }
}