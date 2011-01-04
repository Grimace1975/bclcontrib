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
using System.Linq.Expressions;
namespace System.Linq
{
    /// <summary>
    /// QueryableExtensions
    /// </summary>
    public static partial class QueryableExtensions
    {
        private static IEnumerable<HierarchyNode<TEntity>> CreateHierarchyRecurse<TEntity>(IQueryable<TEntity> source, TEntity parentItem, string propertyNameKey, string propertyNameParentKey, object rootKey, int depth, int maxDepth)
            where TEntity : class
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "e");
            Expression<Func<TEntity, bool>> predicate;
            if (rootKey != null)
            {
                Expression left = Expression.Convert(Expression.Property(parameter, propertyNameKey), rootKey.GetType());
                Expression right = Expression.Constant(rootKey);
                predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right), parameter);
            }
            else
            {
                if (parentItem == null)
                    predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(Expression.Property(parameter, propertyNameParentKey), Expression.Constant(null)), parameter);
                else
                {
                    Expression left = Expression.Convert(Expression.Property(parameter, propertyNameParentKey), parentItem.GetType().GetProperty(propertyNameKey).PropertyType);
                    Expression right = Expression.Constant(parentItem.GetType().GetProperty(propertyNameKey).GetValue(parentItem, null));
                    predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right), parameter);
                }
            }
            IEnumerable<TEntity> childs = source.Where(predicate).ToList();
            if (childs.Count() > 0)
            {
                depth++;
                if ((depth <= maxDepth) || (maxDepth == 0))
                    foreach (var item in childs)
                        yield return new HierarchyNode<TEntity>()
                        {
                            Entity = item,
                            ChildNodes = CreateHierarchyRecurse(source, item, propertyNameKey, propertyNameParentKey, null, depth, maxDepth),
                            Depth = depth,
                            Parent = parentItem
                        };
            }
        }

        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(this IQueryable<TEntity> source, string propertyNameKey, string propertyNameParentKey)
            where TEntity : class { return CreateHierarchyRecurse(source, null, propertyNameKey, propertyNameParentKey, null, 0, 0); }
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(this IQueryable<TEntity> source, string propertyNameKey, string propertyNameParentKey, object rootKey)
            where TEntity : class { return CreateHierarchyRecurse(source, null, propertyNameKey, propertyNameParentKey, rootKey, 0, 0); }
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(this IQueryable<TEntity> source, string propertyNameKey, string propertyNameParentKey, object rootKey, int maxDepth)
            where TEntity : class { return CreateHierarchyRecurse(source, null, propertyNameKey, propertyNameParentKey, rootKey, 0, maxDepth); }
    }
}
