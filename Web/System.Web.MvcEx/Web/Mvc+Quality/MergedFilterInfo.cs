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
using System.Collections.Generic;
using System.Quality;
namespace System.Web.Mvc
{
    /// <summary>
    /// IFilterFinder
    /// </summary>
    [Serializable]
    public class MergeableFilterInfo : FilterInfo
    {
        protected virtual void AddActionFilter(IActionFilter filter)
        {
            if (!ActionFilters.Contains(filter))
                ActionFilters.Add(filter);
        }

        protected virtual void AddAuthorizationFilter(IAuthorizationFilter filter)
        {
            if (!AuthorizationFilters.Contains(filter))
                AuthorizationFilters.Add(filter);
        }

        protected virtual void AddExceptionFilter(IExceptionFilter filter)
        {
            if (!ExceptionFilters.Contains(filter))
                ExceptionFilters.Add(filter);
        }

        protected virtual void AddResultFilter(IResultFilter filter)
        {
            if (!ResultFilters.Contains(filter))
                ResultFilters.Add(filter);
        }

        public virtual MergeableFilterInfo Merge(FilterInfo filter)
        {
            if (filter != null)
            {
                filter.ActionFilters
                    .Yield<IActionFilter>(AddActionFilter);
                filter.AuthorizationFilters
                    .Yield<IAuthorizationFilter>(AddAuthorizationFilter);
                filter.ExceptionFilters
                    .Yield<IExceptionFilter>(AddExceptionFilter);
                filter.ResultFilters
                    .Yield<IResultFilter>(AddResultFilter);
            }
            return this;
        }

        public virtual MergeableFilterInfo Merge(IEnumerable<IActionFilter> actionFilters, IEnumerable<IAuthorizationFilter> authorizationFilters, IEnumerable<IExceptionFilter> exceptionFilters, IEnumerable<IResultFilter> resultFilters)
        {
            actionFilters.Yield<IActionFilter>(AddActionFilter);
            authorizationFilters.Yield<IAuthorizationFilter>(AddAuthorizationFilter);
            exceptionFilters.Yield<IExceptionFilter>(AddExceptionFilter);
            resultFilters.Yield<IResultFilter>(AddResultFilter);
            return this;
        }
    }
}