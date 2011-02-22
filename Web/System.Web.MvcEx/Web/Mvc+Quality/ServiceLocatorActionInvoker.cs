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
using System.Collections.Generic;
namespace System.Web.Mvc
{
    /// <summary>
    /// ServiceLocatorActionInvoker
    /// </summary>
    public class ServiceLocatorActionInvoker : ControllerActionInvoker
    {
        private static readonly object s_lock = new object();
        private static IFilterInfoFinder s_filterInfoFinder;

        public ServiceLocatorActionInvoker(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
                throw new ArgumentNullException("serviceLocator");
            ServiceLocator = serviceLocator;
        }

        protected virtual MergeableFilterInfo MakeMergeableFilterInfo() { return new MergeableFilterInfo(); }

        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            ActionDescriptor descriptor;
            try
            {
                descriptor = base.FindAction(controllerContext, controllerDescriptor, actionName);
            }
            catch { descriptor = null; }
            return (descriptor ?? new InferredActionDescriptor(actionName, controllerDescriptor));
        }

        protected virtual IFilterInfoFinder GetFilterInfoFinder()
        {
            if (s_filterInfoFinder == null)
                lock (s_lock)
                    if (s_filterInfoFinder == null)
                        try
                        {
                            s_filterInfoFinder = ServiceLocator.Resolve<IFilterInfoFinder>();
                        }
                        catch (ServiceResolutionException) { s_filterInfoFinder = new ServiceLocatorFilterFinder(ServiceLocator); }
            return s_filterInfoFinder;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filter = (base.GetFilters(controllerContext, actionDescriptor) ?? new FilterInfo());
            InjectDependencies(filter);
            return MakeMergeableFilterInfo()
                .Merge(filter)
                .Merge(GetFilterInfoFinder().FindFilters(actionDescriptor));
        }

        public virtual void InjectDependencies(FilterInfo filter)
        {
            var serviceLocator = ServiceLocator;
            filter.ActionFilters.Where(f => !(f is IController))
                .Yield<IActionFilter>(f => serviceLocator.Inject<IActionFilter>(f));
            filter.AuthorizationFilters.Where(f => !(f is IController))
                .Yield<IAuthorizationFilter>(f => serviceLocator.Inject<IAuthorizationFilter>(f));
            filter.ResultFilters.Where(f => !(f is IController))
                .Yield<IResultFilter>(f => serviceLocator.Inject<IResultFilter>(f));
            filter.ExceptionFilters.Where(f => !(f is IController))
                .Yield<IExceptionFilter>(f => serviceLocator.Inject<IExceptionFilter>(f));
        }

        public IServiceLocator ServiceLocator { get; private set; }
    }
}