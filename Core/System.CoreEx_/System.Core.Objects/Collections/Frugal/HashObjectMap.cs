namespace System.Collections.Frugal
{
    /// <summary>
    /// HashObjectMap
    /// </summary>
    public sealed class HashObjectMap : FrugalMapBase
    {
        internal System.Collections.Hashtable _entries;
        internal const int MINSIZE = 0xa3;
        private static object NullValue = new object();

        /// <summary>
        /// Gets the key value pair.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public override void GetKeyValuePair(int index, out int key, out object value)
        {
            if (index < _entries.Count)
            {
                System.Collections.IDictionaryEnumerator enumerator = _entries.GetEnumerator();
                enumerator.MoveNext();
                for (int index2 = 0; index2 < index; index2++)
                {
                    enumerator.MoveNext();
                }
                key = (int)enumerator.Key;
                if ((enumerator.Value != NullValue) && (enumerator.Value != null))
                {
                    value = enumerator.Value;
                }
                else
                {
                    value = Object.BaseProperty.UnsetValue;
                }
            }
            else
            {
                value = Object.BaseProperty.UnsetValue;
                key = 0x7fffffff;
                throw new ArgumentOutOfRangeException("index");
            }
        }

        /// <summary>
        /// Inserts the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override FrugalMapStoreState InsertEntry(int key, object value)
        {
            if (_entries == null)
            {
                _entries = new System.Collections.Hashtable(0xa3);
            }
            _entries[key] = ((value != NullValue) && (value != null) ? value : NullValue);
            return FrugalMapStoreState.Success;
        }

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public override void Iterate(System.Collections.ArrayList list, FrugalMapIterationCallback callback)
        {
            System.Collections.IDictionaryEnumerator enumerator = _entries.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object unsetValue;
                int key = (int)enumerator.Key;
                if ((enumerator.Value != NullValue) && (enumerator.Value != null))
                {
                    unsetValue = enumerator.Value;
                }
                else
                {
                    unsetValue = Object.BaseProperty.UnsetValue;
                }
                callback(list, key, unsetValue);
            }
        }

        /// <summary>
        /// Promotes the specified new map.
        /// </summary>
        /// <param name="newMap">The new map.</param>
        public override void Promote(FrugalMapBase newMap)
        {
            throw new InvalidOperationException(TR.Get("FrugalMap_CannotPromoteBeyondHashtable"));
        }

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void RemoveEntry(int key)
        {
            _entries.Remove(key);
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Search(int key)
        {
            object obj2 = _entries[key];
            if ((obj2 != NullValue) && (obj2 != null))
            {
                return obj2;
            }
            return Object.BaseProperty.UnsetValue;
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public override void Sort()
        {
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public override int Count
        {
            get { return _entries.Count; }
        }
    }
}