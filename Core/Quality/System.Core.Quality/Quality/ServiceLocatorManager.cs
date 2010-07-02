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
using System.Linq;
using System.Reflection;
namespace System.Quality
{
    /// <summary>
    /// ServiceLocatorManager
    /// </summary>
    public static class ServiceLocatorManager
    {
        private static readonly object _lock = new object();
        private static Func<IServiceLocator> _provider;
        private static Action<IServiceRegistrar, IServiceLocator> _registration;
        private static IServiceLocator _serviceLocator;

        public static void SetLocatorProvider(Func<IServiceLocator> provider) { SetLocatorProvider(provider, (Action<IServiceRegistrar, IServiceLocator>)null); }
        public static void SetLocatorProvider(Func<IServiceLocator> provider, Assembly[] assemblies) { SetLocatorProvider(provider, (registrar, locator) => RegisterFromAssemblies(registrar, locator, assemblies)); }
        public static void SetLocatorProvider(Func<IServiceLocator> provider, Action<IServiceRegistrar, IServiceLocator> registration)
        {
            _provider = provider;
            _registration = registration;
        }

        public static IServiceLocator Current
        {
            get
            {
                if (_provider == null)
                    throw new InvalidOperationException(Local.UndefinedServiceLocatorProvider);
                if (_serviceLocator == null)
                    lock (_lock)
                        if (_serviceLocator == null)
                        {
                            _serviceLocator = _provider();
                            if (_registration != null)
                                _registration(_serviceLocator.GetRegistrar(), _serviceLocator);
                        }
                return _serviceLocator;
            }
        }

        public static void RegisterFromAssemblies(IServiceRegistrar registrar, IServiceLocator locator, Assembly[] assemblies)
        {
            var registrationType = typeof(IServiceRegistration);
            assemblies.SelectMany(a => a.GetTypes())
                .Where(t => (!t.IsInterface) && (!t.IsAbstract) && (t.GetInterfaces().Contains(registrationType)))
                .ToList()
                .ForEach(r => ((IServiceRegistration)locator.Resolve(r)).Register(registrar));
        }
    }
}
