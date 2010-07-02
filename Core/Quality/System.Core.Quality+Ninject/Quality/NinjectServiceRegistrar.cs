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
    public class NinjectServiceRegistrar : NinjectModule, IServiceRegistrar, IDisposable
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

        public void Register<Source>(Source instance)
            where Source : class
        {
            Bind<Source>()
                .ToConstant(instance);
        }

        public void Register<Source>(Func<Source> factoryMethod)
            where Source : class
        {
            Bind<Source>()
                .ToMethod(c => factoryMethod.Invoke());
        }

        public void Register<Source, Implementation>()
            where Implementation : class, Source
        {
            Bind<Source>()
                .To<Implementation>();
        }

        public void Register<Source, Implementation>(string id)
            where Implementation : class, Source
        {
            Bind<Source>()
                .To(typeof(Implementation)).Named(id);
        }

        public void Register<Source>(Type implType)
            where Source : class
        {
            string key = string.Format("{0}-{1}", typeof(Source).Name, implType.FullName);
            Bind<Source>()
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
