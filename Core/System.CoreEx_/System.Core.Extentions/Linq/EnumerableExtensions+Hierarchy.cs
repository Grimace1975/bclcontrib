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
    /// <summary>
    /// EnumerableExtensions
    /// </summary>
    public static partial class EnumerableExtensions
    {
        private static IEnumerable<HierarchyNode<TEntity>> CreateHierarchyRecurse<TEntity, TKey>(IEnumerable<TEntity> source, TEntity parentItem, Func<TEntity, TKey> keySelector, Func<TEntity, TKey> parentKeySelector, object rootKey, int depth, int maxDepth)
            where TEntity : class
        {
            IEnumerable<TEntity> childs;
            if (rootKey != null)
                childs = source.Where(x => keySelector(x).Equals(rootKey));
            else
                if (parentItem == null)
                    childs = source.Where(x => parentKeySelector(x).Equals(default(TKey)));
                else
                    childs = source.Where(x => parentKeySelector(x).Equals(keySelector(parentItem)));
            if (childs.Count() > 0)
            {
                depth++;
                if ((depth <= maxDepth) || (maxDepth == 0))
                    foreach (var item in childs)
                        yield return new HierarchyNode<TEntity>()
                        {
                            Entity = item,
                            ChildNodes = CreateHierarchyRecurse(source.AsEnumerable(), item, keySelector, parentKeySelector, null, depth, maxDepth),
                            Depth = depth,
                            Parent = parentItem
                        };
            }
        }

        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TKey>(this IEnumerable<TEntity> source, Func<TEntity, TKey> keySelector, Func<TEntity, TKey> parentKeySelector)
            where TEntity : class { return CreateHierarchyRecurse(source, default(TEntity), keySelector, parentKeySelector, null, 0, 0); }
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TKey>(this IEnumerable<TEntity> source, Func<TEntity, TKey> keySelector, Func<TEntity, TKey> parentKeySelector, object rootKey)
            where TEntity : class { return CreateHierarchyRecurse(source, default(TEntity), keySelector, parentKeySelector, rootKey, 0, 0); }
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TKey>(this IEnumerable<TEntity> source, Func<TEntity, TKey> keySelector, Func<TEntity, TKey> parentKeySelector, object rootKey, int maxDepth)
            where TEntity : class { return CreateHierarchyRecurse(source, default(TEntity), keySelector, parentKeySelector, rootKey, 0, maxDepth); }
    }
}
