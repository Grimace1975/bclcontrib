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
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
namespace System.Quality
{
    [Serializable]
    public class NinjectServiceLocator : IServiceLocator, IDisposable
    {
        private IKernel _container;
        private NinjectServiceRegistrar _registrar;

        public NinjectServiceLocator()
            : this(new StandardKernel(new INinjectModule[0])) { }

        public NinjectServiceLocator(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel", "The specified Ninject IKernel cannot be null.");
            Container = kernel;
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
            Container.Inject(instance, new IParameter[0]);
            return instance;
        }

        [Obsolete("Not used with this implementation of IServiceLocator.")]
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
                return Container.Get<T>(new IParameter[0]);
            }
            //catch (ActivationException activationException)
            //{
            //    return (ResolveTheFirstBindingFromTheContainer(activationException, typeof(T)) as T);
            //}
            catch (Exception ex) { throw new ServiceResolutionException(typeof(T), ex); }
        }

        public T Resolve<T>(string key)
            where T : class
        {
            try
            {
                T value = Container.Get<T>(key, new IParameter[0]);
                if (value == null)
                    throw new ServiceResolutionException(typeof(T));
                return value;
            }
            catch (Exception ex) { throw new ServiceResolutionException(typeof(T), ex); }
        }

        public object Resolve(Type type)
        {
            try
            {
                return Container.Get(type, new IParameter[0]);
            }
            //catch (ActivationException activationException)
            //{
            //    return ResolveTheFirstBindingFromTheContainer(activationException, type);
            //}
            catch (Exception ex) { throw new ServiceResolutionException(type, ex); }
        }

        public IList<T> ResolveAll<T>()
            where T : class
        {
            return new List<T>(Container.GetAll<T>(new IParameter[0]));
        }

        [Obsolete("Not used with this implementation of IServiceLocator.")]
        public void TearDown<TService>(TService instance)
            where TService : class { }

        public IKernel Container
        {
            get { return _container; }
            private set
            {
                _container = value;
                _registrar = new NinjectServiceRegistrar(this, value);
            }
        }

        //#region First Binding
        //private class FirstBindingInfo
        //{
        //    public bool BindingExists { get; set; }
        //    public string Name { get; set; }
        //}

        //private FirstBindingInfo GetNameOfFirstBinding(Type type)
        //{
        //    var binding = Container.GetBindings(type)
        //        .OrderBy(x => x.Metadata.Name)
        //        .FirstOrDefault();
        //    binding.Metadata.Name
        //    return (binding == null ? new FirstBindingInfo() : new FirstBindingInfo());
        //}

        //private object ResolveTheFirstBindingFromTheContainer(Exception activationException, Type type)
        //{
        //    var firstBinding = GetNameOfFirstBinding(type);
        //    if (!firstBinding.BindingExists)
        //        throw new ServiceResolutionException(type, activationException);
        //    return Container.Get(type, firstBinding.Name, new IParameter[0]);
        //}
        //#endregion
    }
}
