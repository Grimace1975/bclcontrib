namespace System.Collections.Frugal
{
    /// <summary>
    /// InsertionSortMap
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct InsertionSortMap
    {
        internal LargeSortedObjectMap _mapStore;

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value></value>
        public object this[int key]
        {
            get
            {
                if (_mapStore != null)
                {
                    return _mapStore.Search(key);
                }
                return Object.BaseProperty.UnsetValue;
            }
            set
            {
                if (value != Object.BaseProperty.UnsetValue)
                {
                    if (_mapStore == null)
                    {
                        _mapStore = new LargeSortedObjectMap();
                    }
                    FrugalMapStoreState state = _mapStore.InsertEntry(key, value);
                    if (state != FrugalMapStoreState.Success)
                    {
                        if (FrugalMapStoreState.SortedArray != state)
                        {
                            throw new InvalidOperationException(TR.Get("FrugalMap_CannotPromoteBeyondHashtable"));
                        }
                        LargeSortedObjectMap newMap = new LargeSortedObjectMap();
                        _mapStore.Promote(newMap);
                        _mapStore = newMap;
                        _mapStore.InsertEntry(key, value);
                    }
                }
                else if (_mapStore != null)
                {
                    _mapStore.RemoveEntry(key);
                    if (_mapStore.Count == 0)
                    {
                        _mapStore = null;
                    }
                }
            }
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public void Sort()
        {
            if (_mapStore != null)
            {
                _mapStore.Sort();
            }
        }

        /// <summary>
        /// Gets the key value pair.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void GetKeyValuePair(int index, out int key, out object value)
        {
            if (_mapStore == null)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            _mapStore.GetKeyValuePair(index, out key, out value);
        }

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public void Iterate(System.Collections.ArrayList list, FrugalMapIterationCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (_mapStore != null)
            {
                _mapStore.Iterate(list, callback);
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                if (_mapStore != null)
                {
                    return _mapStore.Count;
                }
                return 0;
            }
        }
    }
}