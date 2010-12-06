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
using System.Collections.Generic;
using System.Diagnostics;
namespace System.Quality
{
    /// <summary>
    /// ServiceLocatorManager
    /// </summary>
    public static class ServiceLocatorManager
    {
        private static readonly Type s_wantToSkipServiceLocatorType = typeof(IWantToSkipServiceLocator);
        private static readonly object _lock = new object();
        private static Func<IServiceLocator> _provider;
        private static Action<IServiceRegistrar, IServiceLocator> _registration;
        private static IServiceLocator _locator;

        public static void SetLocatorProvider(Func<IServiceLocator> provider) { SetLocatorProvider(provider, (Action<IServiceRegistrar, IServiceLocator>)null); }
        public static void SetLocatorProvider(Func<IServiceLocator> provider, params Assembly[] assemblies) { SetLocatorProvider(provider, (registrar, locator) => RegisterFromAssemblies(registrar, assemblies)); }
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
                            RegisterSelfInLocator(registrar, _locator);
                            if (_registration != null)
                                _registration(registrar, _locator);
                        }
                return _locator;
            }
        }

        private static void RegisterSelfInLocator(IServiceRegistrar registrar, IServiceLocator locator)
        {
            registrar.Register<IServiceLocator>(locator);
        }

        public static void RegisterFromAssemblies(IServiceRegistrar registrar, Assembly[] assemblies) { RegisterFromAssemblies(registrar, assemblies, null); }
        public static void RegisterFromAssemblies(IServiceRegistrar registrar, Assembly[] assemblies, Predicate<Type> predicate)
        {
            var locator = registrar.GetLocator();
            var registrationType = typeof(IServiceRegistration);
            assemblies.SelectMany(a => a.GetTypes())
                .Where(t => (!t.IsInterface) && (!t.IsAbstract) && (t.GetInterfaces().Contains(registrationType)))
                .Where(t => (predicate == null) || (predicate(t)))
                .ToList()
                .ForEach(r => ((IServiceRegistration)locator.Resolve(r)).Register(registrar));
        }

        public static void RegisterFromAssembliesByNameConvention(IServiceRegistrar registrar) { RegisterFromAssembliesByNameConvention(registrar, new[] { GetPreviousCallingMethodsAssembly() }, null); }
        public static void RegisterFromAssembliesByNameConvention(IServiceRegistrar registrar, Assembly[] assemblies) { RegisterFromAssembliesByNameConvention(registrar, assemblies, null); }
        public static void RegisterFromAssembliesByNameConvention(IServiceRegistrar registrar, Predicate<Type> predicate) { RegisterFromAssembliesByNameConvention(registrar, new[] { GetPreviousCallingMethodsAssembly() }, predicate); }
        public static void RegisterFromAssembliesByNameConvention(IServiceRegistrar registrar, Assembly[] assemblies, Predicate<Type> predicate)
        {
            var locator = registrar.GetLocator();
            var registrationType = typeof(IServiceRegistrationByNameConvention);
            var nameConventionTypes = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => (!t.IsInterface) && (!t.IsAbstract) && (t.GetInterfaces().Contains(registrationType)))
                .ToList();
            if (nameConventionTypes.Count > 0)
                foreach (var nameConventionType in nameConventionTypes)
                    ((IServiceRegistrationByNameConvention)locator.Resolve(nameConventionType)).RegisterByNameConvention(registrar);
            // default registation
            var remainingAssemblies = assemblies.Where(a => !nameConventionTypes.Any(y => y.Assembly == a));
            DefaultNameConvention(remainingAssemblies, predicate, (interfaceType, type) => registrar.Register(interfaceType, type));
        }

        public static void DefaultNameConvention(IEnumerable<Assembly> assemblies, Predicate<Type> predicate, Action<Type, Type> action)
        {
            if (assemblies.Count() == 0)
                return;
            var interfaceTypes = assemblies.SelectMany(a => a.AsTypesEnumerator(t => t.IsInterface))
                .Where(t => t.Name.StartsWith("I"))
                .Where(t => (predicate == null) || (predicate(t)));
            foreach (var interfaceType in interfaceTypes)
            {
                string concreteName = interfaceType.Name.Substring(1);
                var types = interfaceType.Assembly.AsTypesEnumerator(interfaceType)
                    .Where(t => t.Name == concreteName)
                    .Where(t => (predicate == null) || (predicate(t)))
                    .ToList();
                if (types.Count == 1)
                    action(interfaceType, types.First());
            }
        }

        public static bool GetWantsToSkipLocator<TService>(object service) { return ((service == null) || (GetWantsToSkipLocator(service.GetType()))); }
        public static bool GetWantsToSkipLocator<TService>() { return GetWantsToSkipLocator(typeof(TService)); }
        public static bool GetWantsToSkipLocator(Type type)
        {
            return ((type == null) || (s_wantToSkipServiceLocatorType.IsAssignableFrom(type)));
        }

        private static Assembly GetPreviousCallingMethodsAssembly()
        {
            var frame = new StackTrace().GetFrame(2);
            var method = frame.GetMethod();
            return (method != null ? method.ReflectedType.Assembly : null);
        }
    }
}
