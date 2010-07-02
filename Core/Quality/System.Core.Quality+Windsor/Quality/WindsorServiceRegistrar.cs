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
    public class WindsorServiceRegistrar : IServiceRegistrar, IDisposable
    {
        private readonly IList<IRegistration> _registrations = new List<IRegistration>();
        private IWindsorContainer _container;

        public WindsorServiceRegistrar(IWindsorContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Register(_registrations.ToArray());
        }

        public void Register<Source>(Source instance)
            where Source : class
        {
            var registration = Component.For<Source>()
                .Instance(instance);
            _registrations.Add(registration);
        }

        public void Register<Source>(Func<Source> factoryMethod)
            where Source : class
        {
            var registration = Component.For<Source>()
                .UsingFactoryMethod<Source>(factoryMethod.Invoke);
            _registrations.Add(registration);
        }

        public void Register<Source>(Type implType)
             where Source : class
        {
            string key = GetKey(typeof(Source), implType);
            var registration = Component.For<Source>()
                .Named(key)
                .ImplementedBy(implType)
                .LifeStyle
                .Transient;
            _registrations.Add(registration);
        }

        public void Register<Source, Implementation>()
            where Implementation : class, Source
        {
            var key = GetKey(typeof(Source), typeof(Implementation));
            Register<Source, Implementation>(key);
        }

        public void Register<Source, Implementation>(string id)
            where Implementation : class, Source
        {
            var registration = Component.For<Source>()
                .Named(id)
                .ImplementedBy<Implementation>()
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
        public void RegisterAll<Source>()
        {
            AllTypes.Of<Source>();
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
