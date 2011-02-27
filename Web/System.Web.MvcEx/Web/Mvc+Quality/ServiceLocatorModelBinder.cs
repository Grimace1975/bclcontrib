#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Linq;
using System.Quality;
using System.Collections;
using System.Collections.Generic;
namespace System.Web.Mvc
{
    /// <summary>
    /// ServiceLocatorModelBinder
    /// </summary>
    public class ServiceLocatorModelBinder : DefaultModelBinder
    {
        private static readonly object s_lock = new object();
        private static IList<IInjectableModelBinder> s_modelBinders;

        public ServiceLocatorModelBinder()
            : this(ServiceLocatorManager.Current) { }
        public ServiceLocatorModelBinder(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
                throw new ArgumentNullException("serviceLocator");
            ServiceLocator = serviceLocator;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var controllerType = controllerContext.Controller.GetType();
            if (!ServiceLocatorManager.GetWantsToSkipLocator(controllerType))
            {
                var modelBinders = GetRegisteredModelBinders();
                if (EnumerableEx.IsNullOrEmpty(modelBinders))
                    foreach (var modelBinder in modelBinders.Where(modelBinder => modelBinder.InjectForModelType(bindingContext.ModelType)))
                        return modelBinder.BindModel(controllerContext, bindingContext);
            }
            return base.BindModel(controllerContext, bindingContext);
        }

        protected virtual IList<IInjectableModelBinder> GetRegisteredModelBinders()
        {
            if (s_modelBinders == null)
                lock (s_lock)
                    if (s_modelBinders == null)
                        s_modelBinders = ServiceLocator.ResolveAll<IInjectableModelBinder>();
            return s_modelBinders;
        }

        public IServiceLocator ServiceLocator { get; private set; }
    }
}