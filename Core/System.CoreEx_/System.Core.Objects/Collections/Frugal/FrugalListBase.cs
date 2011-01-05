namespace System.Collections.Frugal
{
    /// <summary>
    /// FrugalListBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FrugalListBase<T>
    {
        protected int _count;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrugalListBase&lt;T&gt;"/> class.
        /// </summary>
        protected FrugalListBase()
        {
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract FrugalListStoreState Add(T value);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public abstract void Clear();
        
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
        
        /// <summary>
        /// Determines whether [contains] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool Contains(T value);
        
        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public abstract void CopyTo(T[] array, int index);
        
        /// <summary>
        /// Entries at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public abstract T EntryAt(int index);
        
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract int IndexOf(T value);
        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public abstract void Insert(int index, T value);
        
        /// <summary>
        /// Promotes the specified new list.
        /// </summary>
        /// <param name="newList">The new list.</param>
        public abstract void Promote(FrugalListBase<T> newList);
        
        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract bool Remove(T value);
        
        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public abstract void RemoveAt(int index);
        
        /// <summary>
        /// Sets at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public abstract void SetAt(int index, T value);
        
        /// <summary>
        /// Toes the array.
        /// </summary>
        /// <returns></returns>
        public abstract T[] ToArray();

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>The capacity.</value>
        public abstract int Capacity { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get { return _count; }
        }
    }
}