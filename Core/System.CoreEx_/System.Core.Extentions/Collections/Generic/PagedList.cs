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
namespace System.Collections.Generic
{
    public interface IPagedList<T> : IList<T>, IPagedMetadata { }

    public class PagedList<T> : List<T>, IPagedList<T>
    {
        private readonly IPagedMetadata _metadata;

        public PagedList(IPagedMetadata metadata)
        {
            _metadata = metadata;
        }
        public PagedList(IEnumerable<T> collection, IPagedMetadata metadata)
            : base(collection)
        {
            _metadata = metadata;
        }

        public int Pages
        {
            get { return _metadata.Pages; }
        }

        public int TotalItems
        {
            get { return _metadata.TotalItems; }
        }

        public int Items
        {
            get { return _metadata.Items; }
        }

        public int Index
        {
            get { return _metadata.Index; }
        }

        public bool HasPreviousPage
        {
            get { return _metadata.HasPreviousPage; }
        }

        public bool HasNextPage
        {
            get { return _metadata.HasNextPage; }
        }

        public bool IsFirstPage
        {
            get { return _metadata.IsFirstPage; }
        }

        public bool IsLastPage
        {
            get { return _metadata.IsLastPage; }
        }
    }
}