namespace System.Collections.Frugal
{
    /// <summary>
    /// FrugalMapBase
    /// </summary>
    public abstract class FrugalMapBase
    {
        protected const int INVALIDKEY = 0x7fffffff;

        #region Entry
        /// <summary>
        /// Entry
        /// </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        internal struct Entry
        {
            public int Key;
            public object Value;
        }
        #endregion Entry

        /// <summary>
        /// Initializes a new instance of the <see cref="FrugalMapBase"/> class.
        /// </summary>
        protected FrugalMapBase()
        {
        }

        /// <summary>
        /// Gets the key value pair.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public abstract void GetKeyValuePair(int index, out int key, out object value);

        /// <summary>
        /// Inserts the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract FrugalMapStoreState InsertEntry(int key, object value);

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public abstract void Iterate(System.Collections.ArrayList list, FrugalMapIterationCallback callback);

        /// <summary>
        /// Promotes the specified new map.
        /// </summary>
        /// <param name="newMap">The new map.</param>
        public abstract void Promote(FrugalMapBase newMap);

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        public abstract void RemoveEntry(int key);

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public abstract object Search(int key);

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public abstract void Sort();

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public abstract int Count { get; }
    }
}