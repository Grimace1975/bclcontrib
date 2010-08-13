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
using System.Reflection;
using StructureMap;
namespace System.Quality
{
    /// <summary>
    /// IStructureMapServiceLocator
    /// </summary>
    public interface IStructureMapServiceLocator : IServiceLocator
    {
        IContainer Container { get; }
    }

    /// <summary>
    /// StructureMapServiceLocator
    /// </summary>
    [Serializable]
    public class StructureMapServiceLocator : IStructureMapServiceLocator, IDisposable
    {
        private IContainer _container;
        private StructureMapServiceRegistrar _registrar;

        public StructureMapServiceLocator()
            : this(new Container()) { }

        public StructureMapServiceLocator(IContainer container)
        {
            Container = container;
        }

        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
                _registrar = null;
            }
        }

        public IServiceRegistrar GetRegistrar()
        {
            return _registrar;
        }

        public TServiceRegistrar GetRegistrar<TServiceRegistrar>()
            where TServiceRegistrar : class, IServiceRegistrar
        {
            return (_registrar as TServiceRegistrar);
        }

        public TService Inject<TService>(TService instance)
            where TService : class
        {
            if (instance == null)
                return default(TService);
            Container.BuildUp(instance);
            instance.GetType()
                .GetProperties()
                .Where(property => property.CanWrite && Container.Model.HasImplementationsFor(property.PropertyType))
                .ToList()
                .ForEach(property => property.SetValue(instance, Container.GetInstance(property.PropertyType), null));
            return instance;
        }

        [Obsolete("Not used for any real purposes.")]
        public void Release(object instance) { }

        public void Reset()
        {
            Dispose();
        }

        public T Resolve<T>()
            where T : class
        {
            try
            {
                return Container.GetInstance<T>();
            }
            catch (Exception ex) { throw new ServiceResolutionException(typeof(T), ex); }
        }

        public T Resolve<T>(string key)
            where T : class
        {
            try
            {
                return Container.GetInstance<T>(key);
            }
            catch (Exception ex) { throw new ServiceResolutionException(typeof(T), ex); }
        }

        public object Resolve(Type type)
        {
            try
            {
                return Container.GetInstance(type);
            }
            catch (Exception ex) { throw new ServiceResolutionException(type, ex); }
        }

        public IList<T> ResolveAll<T>()
            where T : class
        {
            return Container.GetAllInstances<T>();
        }

        [Obsolete("Not used for any real purposes.")]
        public void TearDown<TService>(TService instance)
            where TService : class { }

        public IContainer Container
        {
            get { return _container; }
            private set
            {
                _container = value;
                _registrar = new StructureMapServiceRegistrar(this, value);
            }
        }
    }
}
