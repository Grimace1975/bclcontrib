#region Foreign-License
//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Modified: Sky Morey <moreys@digitalev.com>
//
#endregion
using System.Linq;
using System.Collections.Generic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
namespace System.Quality
{
    /// <summary>
    /// IWindsorServiceRegistrar
    /// </summary>
    public interface IWindsorServiceRegistrar : IServiceRegistrar
    {
        void RegisterAll<Source>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class WindsorServiceRegistrar : IWindsorServiceRegistrar, IDisposable
    {
        private WindsorServiceLocator _parent;
        private readonly IList<IRegistration> _registrations = new List<IRegistration>();
        private IWindsorContainer _container;

        public WindsorServiceRegistrar(WindsorServiceLocator parent, IWindsorContainer container)
        {
            _parent = parent;
            _container = container;
        }

        public void Dispose()
        {
            _container.Register(_registrations.ToArray());
        }

        public IServiceLocator GetLocator()
        {
            return _parent;
        }

        public TServiceLocator GetLocator<TServiceLocator>()
            where TServiceLocator : class, IServiceLocator
        {
            return (_parent as TServiceLocator);
        }

        public void Register<TService>(TService instance)
            where TService : class
        {
            var registration = Component.For<TService>()
                .Instance(instance);
            _registrations.Add(registration);
        }

        public void Register<TService>(Func<TService> factoryMethod)
            where TService : class
        {
            var registration = Component.For<TService>()
                .UsingFactoryMethod<TService>(factoryMethod.Invoke);
            _registrations.Add(registration);
        }

        public void Register<TService>(Type implType)
             where TService : class
        {
            string key = GetKey(typeof(TService), implType);
            var registration = Component.For<TService>()
                .Named(key)
                .ImplementedBy(implType)
                .LifeStyle
                .Transient;
            _registrations.Add(registration);
        }

        public void Register<TService, TImplementation>()
            where TImplementation : class, TService
        {
            var key = GetKey(typeof(TService), typeof(TImplementation));
            Register<TService, TImplementation>(key);
        }

        public void Register<TService, TImplementation>(string id)
            where TImplementation : class, TService
        {
            var registration = Component.For<TService>()
                .Named(id)
                .ImplementedBy<TImplementation>()
                .LifeStyle
                .Transient;
            _registrations.Add(registration);
        }

        public void Register(string id, Type type)
        {
            var registration = Component.For(type)
                .Named(id)
                .LifeStyle
                .Transient;
            _registrations.Add(registration);
        }

        public void Register(Type serviceType, Type implType)
        {
            var registration = Component.For(serviceType)
                .ImplementedBy(implType)
                .LifeStyle
                .Transient;
            _registrations.Add(registration);
        }

        #region Domain extents
        public void RegisterAll<TService>()
        {
            AllTypes.Of<TService>();
        }
        #endregion

        #region Domain specific
        private static string GetKey(Type service, Type impl)
        {
            return string.Format("{0}-{1}", service.Name, impl.FullName);
        }
        #endregion
    }
}
