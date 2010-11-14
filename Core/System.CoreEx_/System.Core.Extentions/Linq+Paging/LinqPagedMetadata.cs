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
using System.Collections.Generic;
namespace System.Linq
{
    public class LinqPagedMetadata<TSource> : IPagedMetadata
    {
        public int TotalItems { get; private set; }
        public int Pages { get; private set; }
        public int Items { get; private set; }
        public int Index { get; private set; }
        public LinqPagedCriteria Criteria { get; private set; }
        //
        public bool HasOverflowedShowAll { get; private set; }

        public LinqPagedMetadata(IEnumerable<TSource> items, LinqPagedCriteria criteria)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (criteria == null)
                throw new ArgumentNullException("criteria");
            Criteria = criteria;
            Items = items.Count();
            var totalItemsAccessor = criteria.TotalItemsAccessor;
            TotalItems = (totalItemsAccessor == null ? Items : totalItemsAccessor());
            Index = criteria.PageIndex;
            Pages = (TotalItems > 0 ? (int)Math.Ceiling(TotalItems / (decimal)Criteria.PageSize) : 1);
            HasOverflowedShowAll = ((Criteria.ShowAll) && (Items < TotalItems));
            EnsureVisiblity();
        }

        //private static int ThrowAwayPages(int totalItems, int pageSize)
        //{
        //    int pages = 1;
        //    if ((pageSize > 0) && (totalItems > pageSize))
        //    {
        //        pages = totalItems / pageSize;
        //        if (totalItems % pageSize > 0)
        //            pages++;
        //    }
        //    return pages;
        //}

        public bool EnsureVisiblity()
        {
            if (Index > Pages)
            {
                Index = Pages;
                return false;
            }
            return true;
        }

        public bool HasPreviousPage
        {
            get { return (Criteria.PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return Index < (Pages - 1); }
        }

        public bool IsFirstPage
        {
            get { return Index <= 0; }
        }

        public bool IsLastPage
        {
            get { return Index >= (Pages - 1); }
        }
    }
}
