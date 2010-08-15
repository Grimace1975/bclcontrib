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
namespace System.Quality
{
    /// <summary>
    /// IStructureMapServiceRegistrar
    /// </summary>
    public interface IStructureMapServiceRegistrar : IServiceRegistrar
    {
        void RegisterAll<TSource>();
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

        public new void Register<TSource>(TSource instance)
            where TSource : class
        {
            For<TSource>()
                .Use(instance);
        }

        public void Register<TSource>(Func<TSource> factoryMethod)
            where TSource : class
        {
            For<TSource>()
                .Use(factoryMethod.Invoke);
            //Container.Configure(configure => configure.For<Source>()
            //    .Use(factoryMethod.Invoke));
        }

        public void Register<TSource>(Type implType)
            where TSource : class
        {
            For(typeof(TSource))
                .Add(implType);
        }

        public void Register<TSource, TImplementation>()
            where TImplementation : class, TSource
        {
            For<TSource>()
                .Add<TImplementation>();
        }

        public void Register<TSource, TImplementation>(string id)
            where TImplementation : class, TSource
        {
            For(typeof(TSource))
                .Add(typeof(TImplementation))
                .Named(id);
        }

        public void Register(string id, Type type)
        {
            For(type)
                .Add(type)
                .Named(id);
        }

        public void Register(Type serviceType, Type implType)
        {
            For(serviceType)
                .Add(implType);
        }

        #region Domain extents
        public void RegisterAll<TSource>()
        {
            Scan(scanner => scanner.AddAllTypesOf<TSource>());
        }
        #endregion
    }
}
