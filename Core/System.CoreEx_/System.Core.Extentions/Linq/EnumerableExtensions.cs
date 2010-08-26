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
using System.Collections;
namespace System.Linq
{
    /// <summary>
    /// EnumerableExtensions
    /// </summary>
    public static partial class EnumerableExtensions
    {
        private static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
        {
            return (x => selector2(selector1(x)));
        }

        private static Func<TSource, bool> CombinePredicates<TSource>(Func<TSource, bool> predicate1, Func<TSource, bool> predicate2)
        {
            return (x => (predicate1(x) ? predicate2(x) : false));
        }

        //private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        //{
        //    return new WrappedSelectIterator<TSource, TResult>(-2)
        //    {
        //        OriginalSource = source,
        //        OriginalSelector = selector,
        //    };
        //}

        public static IEnumerable<TSource> AsEnumerableYield<TSource>(this IEnumerable source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var item in source)
                yield return (TSource)item;
        }

        // Name = ForEachYield | Yield
        public static void Yield<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (TSource item in source)
                action(item);
        }

        public static IEnumerable<TSource> ForYield<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource> initialize, Predicate<TSource> predicate, Func<TSource, TSource> next)
        {
            foreach (TSource item in source)
            {
                var value = initialize(item);
                while (predicate(value))
                {
                    yield return value;
                    value = next(value);
                }
            }
        }

        /// <summary>
        /// Coalesces with the specified null value.
        /// </summary>
        /// <param name="nullValue">The null value.</param>
        /// <param name="parameterArray">The parameter array.</param>
        /// <returns>
        /// First null value as defined by parameter nullValue.
        /// </returns>
        public static TSource Coalesce<TSource>(this IEnumerable<TSource> source, TSource nullValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (TSource value in source)
                if ((value != null) && (!value.Equals(nullValue)))
                    return value;
            return nullValue;
        }

        public static int MaxSkipNull(this IEnumerable<int> source, int nullValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int minValue = nullValue;
            foreach (int value in source)
                if ((value != nullValue) && ((minValue == nullValue) || (value > minValue)))
                    minValue = value;
            return minValue;
        }

        public static TSource MaxSkipNull<TSource>(this IEnumerable<TSource> source, TSource nullValue)
            where TSource : IComparable<TSource>
        {
            if (source == null)
                throw new ArgumentNullException("source");
            TSource minValue = nullValue;
            foreach (TSource value in source)
                if ((value != null) && (!value.Equals(nullValue)) && ((minValue.Equals(nullValue)) || (value.CompareTo(minValue) > 0)))
                    minValue = value;
            return minValue;
        }

        /// <summary>
        /// Mins the skip null.
        /// </summary>
        /// <param name="nullValue">The null value.</param>
        /// <param name="parameterArray">The parameter array.</param>
        /// <returns>
        /// minumum value excluding null's as defined by parameter nullValue.
        /// </returns>
        public static int MinSkipNull(this IEnumerable<int> source, int nullValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            int minValue = nullValue;
            foreach (int value in source)
                if ((value != nullValue) && ((minValue == nullValue) || (value < minValue)))
                    minValue = value;
            return minValue;
        }

        public static TSource MinSkipNull<TSource>(this IEnumerable<TSource> source, TSource nullValue)
            where TSource : IComparable<TSource>
        {
            if (source == null)
                throw new ArgumentNullException("source");
            TSource minValue = nullValue;
            foreach (TSource value in source)
                if ((value != null) && (!value.Equals(nullValue)) && ((minValue.Equals(nullValue)) || (value.CompareTo(minValue) < 0)))
                    minValue = value;
            return minValue;
        }

        public static TSource FindWhileSkipNull<TSource>(this IEnumerable<TSource> source, TSource nullValue, Func<TSource, TSource, bool> finder)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            TSource seedValue = nullValue;
            foreach (TSource value in source)
                if ((!value.Equals(nullValue)) && ((seedValue.Equals(nullValue)) || (finder(seedValue, value))))
                    seedValue = value;
            return seedValue;
        }
    }
}
