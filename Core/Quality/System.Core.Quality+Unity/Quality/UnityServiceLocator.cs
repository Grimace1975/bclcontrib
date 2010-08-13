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
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Quality.Internal;
namespace System.Quality
{
    /// <summary>
    /// IUnityServiceLocator
    /// </summary>
    public interface IUnityServiceLocator : IServiceLocator
    {
        IUnityContainer Container { get; }
    }

    /// <summary>
    /// UnityServiceLocator
    /// </summary>
    [Serializable]
    public class UnityServiceLocator : IUnityServiceLocator, IDisposable
    {
        private IUnityContainer _container;
        private UnityServiceRegistrar _registrar;

        public UnityServiceLocator()
            : this(new UnityContainer()) { }

        public UnityServiceLocator(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container", "The specified Unity container cannot be null.");
            Container = container;
            Container.AddNewExtension<UnityStrategiesExtension>();
        }

        public void Dispose()
        {
            if (_container != null)
            {
                //_container.Dispose();
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
            return (instance == null ? instance : (TService)Container.BuildUp(instance.GetType(), instance));
        }

        public void Release(object instance)
        {
            if (instance != null)
                Container.Teardown(instance);
        }

        public void Reset()
        {
            Dispose();
        }

        public T Resolve<T>()
            where T : class
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch (Exception exception) { throw new ServiceResolutionException(typeof(T), exception); }
        }

        public T Resolve<T>(string key)
            where T : class
        {
            try
            {
                return Container.Resolve<T>(key);
            }
            catch (Exception exception) { throw new ServiceResolutionException(typeof(T), exception); }
        }

        public object Resolve(Type type)
        {
            try
            {
                return Container.Resolve(type);
            }
            catch (Exception exception) { throw new ServiceResolutionException(type, exception); }
        }

        public IList<T> ResolveAll<T>()
            where T : class
        {
            return new List<T>(Container.ResolveAll<T>());
        }

        public void TearDown<TService>(TService instance)
            where TService : class
        {
            if (instance != null)
                Container.Teardown(instance);
        }

        public IUnityContainer Container
        {
            get { return _container; }
            private set
            {
                _container = value;
                _registrar = new UnityServiceRegistrar(this, value);
            }
        }
    }
}
