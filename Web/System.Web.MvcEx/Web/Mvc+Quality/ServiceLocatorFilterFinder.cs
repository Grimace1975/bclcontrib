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
using System.Collections;
using System.Reflection;
namespace System.Web.Mvc
{
    /// <summary>
    /// ServiceLocatorFilterFinder
    /// </summary>
    public class ServiceLocatorFilterFinder : IFilterInfoFinder
    {
        public ServiceLocatorFilterFinder(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
                throw new ArgumentNullException("serviceLocator");
            ServiceLocator = serviceLocator;
        }

        protected virtual MergeableFilterInfo MakeMergeableFilterInfo() { return new MergeableFilterInfo(); }

        public FilterInfo FindFilters(ActionDescriptor actionDescriptor)
        {
            return (actionDescriptor != null ? MakeMergeableFilterInfo()
                .Merge(GetAllFilters())
                .Merge(GetInjectableFilters(actionDescriptor)) : null);
        }

        private static InjectableFilterAttribute[] GetAttributes(ICustomAttributeProvider actionDescriptor)
        {
            return (actionDescriptor.GetCustomAttributes(typeof(InjectableFilterAttribute), true) as InjectableFilterAttribute[]);
        }

        private FilterInfo GetAllFilters()
        {
            return MakeMergeableFilterInfo().Merge(LocateAllFilters<IActionFilter>()
                , LocateAllFilters<IAuthorizationFilter>()
                , LocateAllFilters<IExceptionFilter>()
                , LocateAllFilters<IResultFilter>());
        }

        private FilterInfo GetInjectableFilters(ICustomAttributeProvider actionDescriptor)
        {
            var attributes = GetAttributes(actionDescriptor);
            if (EnumerableEx.IsNullOrEmpty(attributes))
                return null;
            return MakeMergeableFilterInfo().Merge(LocateInjectableFilters<IActionFilter>(attributes)
                , LocateInjectableFilters<IAuthorizationFilter>(attributes)
                , LocateInjectableFilters<IExceptionFilter>(attributes)
                , LocateInjectableFilters<IResultFilter>(attributes));
        }

        protected virtual IList<TFilter> LocateAllFilters<TFilter>()
            where TFilter : class
        {
            return ServiceLocator.ResolveAll<TFilter>()
                .Where(x => !(x is IController))
                .ToDictionary(k => k.GetType())
                .Values
                .ToList();
        }

        protected virtual IList<TFilter> LocateInjectableFilters<TFilter>(InjectableFilterAttribute[] filterAttributes)
            where TFilter : class
        {
            return ServiceLocator.ResolveAll<TFilter>()
                .SelectMany(x => filterAttributes, (svc, filter) => new { svc, filter })
                .Where(x => x.filter.FilterType is TFilter)
                .Select(x => x.svc)
                .ToList();
        }

        public IServiceLocator ServiceLocator { get; private set; }
    }
}