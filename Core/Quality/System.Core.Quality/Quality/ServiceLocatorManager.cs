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
        private static IServiceLocator _locator;

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
                if (_locator == null)
                    lock (_lock)
                        if (_locator == null)
                        {
                            _locator = _provider();
                            if (_locator == null)
                                throw new InvalidOperationException();
                            var registrar = _locator.GetRegistrar();
                            RegisterSelfLocator(registrar, _locator);
                            if (_registration != null)
                                _registration(registrar, _locator);
                        }
                return _locator;
            }
        }

        private static void RegisterSelfLocator(IServiceRegistrar registrar, IServiceLocator locator)
        {
            registrar.Register<IServiceLocator>(locator);
        }

        public static void RegisterFromAssemblies(IServiceRegistrar registrar, IServiceLocator locator, Assembly[] assemblies) { RegisterFromAssemblies(registrar, locator, assemblies, null); }
        public static void RegisterFromAssemblies(IServiceRegistrar registrar, IServiceLocator locator, Assembly[] assemblies, Predicate<Type> predicate)
        {
            var registrationType = typeof(IServiceRegistration);
            assemblies.SelectMany(a => a.GetTypes())
                .Where(t => (!t.IsInterface) && (!t.IsAbstract) && (t.GetInterfaces().Contains(registrationType)))
                .Where(t => (predicate == null) || (predicate(t)))
                .ToList()
                .ForEach(r => ((IServiceRegistration)locator.Resolve(r)).Register(registrar));
        }

        public static void RegisterFromAssembliesByNameConvention(IServiceRegistrar registrar, IServiceLocator locator, Assembly[] assemblies) { RegisterFromAssembliesByNameConvention(registrar, locator, assemblies, null); }
        public static void RegisterFromAssembliesByNameConvention(IServiceRegistrar registrar, IServiceLocator locator, Assembly[] assemblies, Predicate<Type> predicate)
        {
            var interfaceTypes = assemblies.SelectMany(a => a.AsTypesEnumerator(t => t.IsInterface))
                .Where(type => type.Name.StartsWith("I"));
            foreach (var interfaceType in interfaceTypes)
            {
                string concreteName = interfaceType.Name.Substring(1);
                interfaceType.Assembly.AsTypesEnumerator(interfaceType)
                    .Where(t => t.Name == concreteName)
                    .Where(t => (predicate == null) || (predicate(t)))
                    .ToList()
                    .ForEach(t => registrar.Register(interfaceType, t));
            }
        }
    }
}
