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
using Ninject;
using Ninject.Modules;
namespace System.Quality
{
    /// <summary>
    /// INinjectServiceRegistrar
    /// </summary>
    public interface INinjectServiceRegistrar : IServiceRegistrar { }

    public class NinjectServiceRegistrar : NinjectModule, INinjectServiceRegistrar, IDisposable
    {
        private NinjectServiceLocator _parent;
        private IKernel _container;
        private Guid _moduleId;

        public NinjectServiceRegistrar(NinjectServiceLocator parent, IKernel kernel)
        {
            _parent = parent;
            _container = kernel;
            _container.Load(new INinjectModule[] { this });
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
            Bind<TService>()
                .ToConstant(instance);
        }

        public void Register<TService>(Func<TService> factoryMethod)
            where TService : class
        {
            Bind<TService>()
                .ToMethod(c => factoryMethod.Invoke());
        }

        public void Register<TService, TImplementation>()
            where TImplementation : class, TService
        {
            Bind<TService>()
                .To<TImplementation>();
        }

        public void Register<TService, TImplementation>(string id)
            where TImplementation : class, TService
        {
            Bind<TService>()
                .To(typeof(TImplementation)).Named(id);
        }

        public void Register<TService>(Type implType)
            where TService : class
        {
            string key = string.Format("{0}-{1}", typeof(TService).Name, implType.FullName);
            Bind<TService>()
                .To(implType)
                .Named(key);
        }

        public void Register(string id, Type type)
        {
            Bind(type)
                .ToSelf()
                .Named(id);
        }

        public void Register(Type serviceType, Type implType)
        {
            Bind(serviceType)
                .To(implType);
        }

        #region Domain specific
        public override void Load()
        {
            _moduleId = Guid.NewGuid();
        }

        public override string Name
        {
            get { return _moduleId.ToString(); }
        }
        #endregion
    }
}
