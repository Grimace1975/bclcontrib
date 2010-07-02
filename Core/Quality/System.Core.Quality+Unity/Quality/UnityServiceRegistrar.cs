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
using Microsoft.Practices.Unity;
namespace System.Quality
{
    internal sealed class UnityServiceRegistrar : IUnityServiceRegistrar, IDisposable
    {
        private UnityServiceLocator _parent;
        private IUnityContainer _container;

        public UnityServiceRegistrar(UnityServiceLocator parent, IUnityContainer container)
        {
            _parent = parent;
            _container = container;
        }

        public void Dispose() { }

        public IServiceLocator GetLocator()
        {
            return _parent;
        }

        public TServiceLocator GetLocator<TServiceLocator>()
            where TServiceLocator : class, IServiceLocator
        {
            return (_parent as TServiceLocator);
        }

        public void Register<Source>(Source instance)
            where Source : class
        {
            _container.RegisterInstance<Source>(instance);
        }

        public void Register<Source>(Func<Source> factoryMethod)
            where Source : class
        {
            Func<IUnityContainer, object> factory = (c => factoryMethod.Invoke());
            _container.RegisterType<Source>(new InjectionFactory(factory));
        }

        public void Register<Source>(Type implType)
           where Source : class
        {
            var type = typeof(Source);
            string name = string.Format("{0}-{1}", type.Name, implType.FullName);
            _container.RegisterType(type, implType, name, new InjectionMember[0]);
        }

        public void Register<Source, Implementation>()
            where Implementation : class, Source
        {
            _container.RegisterType<Source, Implementation>(new InjectionMember[0]);
        }

        public void Register<Source, Implementation>(string id)
            where Implementation : class, Source
        {
            _container.RegisterType<Source, Implementation>(id, new InjectionMember[0]);
        }

        public void Register(string id, Type type)
        {
            _container.RegisterType(type, id, new InjectionMember[0]);
        }

        public void Register(Type serviceType, Type implType)
        {
            _container.RegisterType(serviceType, implType, new InjectionMember[0]);
        }
    }
}
