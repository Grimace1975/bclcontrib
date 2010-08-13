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
using StructureMap.Configuration.DSL;
using StructureMap;
using StructureMap.Graph;
namespace System.Quality
{
    /// <summary>
    /// IStructureMapServiceRegistrar
    /// </summary>
    public interface IStructureMapServiceRegistrar : IServiceRegistrar
    {
        void RegisterAll<Source>();
    }

    /// <summary>
    /// StructureMapServiceRegistrar
    /// </summary>
    public class StructureMapServiceRegistrar : Registry, IStructureMapServiceRegistrar, IDisposable
    {
        private StructureMapServiceLocator _parent;
        private IContainer _container;

        public StructureMapServiceRegistrar(StructureMapServiceLocator parent, IContainer container)
        {
            _parent = parent;
            _container = container;
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

        public void Dispose()
        {
            _container.Configure(x => x.AddRegistry(this));
        }

        public void Register<Source>(Source instance)
            where Source : class
        {
            For<Source>()
                .Use(instance);
        }

        public void Register<Source>(Func<Source> factoryMethod)
            where Source : class
        {
            For<Source>()
                .Use(factoryMethod.Invoke);
            //Container.Configure(configure => configure.For<Source>()
            //    .Use(factoryMethod.Invoke));
        }

        public void Register<Source>(Type implType)
            where Source : class
        {
            ForRequestedType(typeof(Source))
                .AddType(implType);
        }

        public void Register<Source, Implementation>()
            where Implementation : class, Source
        {
            ForRequestedType<Source>()
                .AddConcreteType<Implementation>();
        }

        public void Register<Source, Implementation>(string id)
            where Implementation : class, Source
        {
            ForRequestedType(typeof(Source))
                .AddType(typeof(Implementation))
                .WithName(id);
        }

        public void Register(string id, Type type)
        {
            ForRequestedType(type)
                .AddType(type)
                .WithName(id);
        }

        public void Register(Type serviceType, Type implType)
        {
            ForRequestedType(serviceType)
                .AddType(implType);
        }

        #region Domain extents
        public void RegisterAll<Source>()
        {
            Scan(scanner => scanner.AddAllTypesOf<Source>());
        }
        #endregion
    }
}
