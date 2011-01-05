namespace System.Collections.Frugal
{
    /// <summary>
    /// SingleObjectMap
    /// </summary>
    public sealed class SingleObjectMap : FrugalMapBase
    {
        private FrugalMapBase.Entry _loneEntry;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleObjectMap"/> class.
        /// </summary>
        public SingleObjectMap()
        {
            _loneEntry.Key = 0x7fffffff;
            _loneEntry.Value = Object.BaseProperty.UnsetValue;
        }

        /// <summary>
        /// Gets the key value pair.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public override void GetKeyValuePair(int index, out int key, out object value)
        {
            if (index == 0)
            {
                value = _loneEntry.Value;
                key = _loneEntry.Key;
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
            if ((0x7fffffff != _loneEntry.Key) && (key != _loneEntry.Key))
            {
                return FrugalMapStoreState.ThreeObjectMap;
            }
            _loneEntry.Key = key;
            _loneEntry.Value = value;
            return FrugalMapStoreState.Success;
        }

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public override void Iterate(System.Collections.ArrayList list, FrugalMapIterationCallback callback)
        {
            if (Count == 1)
            {
                callback(list, _loneEntry.Key, _loneEntry.Value);
            }
        }

        /// <summary>
        /// Promotes the specified new map.
        /// </summary>
        /// <param name="newMap">The new map.</param>
        public override void Promote(FrugalMapBase newMap)
        {
            if (newMap.InsertEntry(_loneEntry.Key, _loneEntry.Value) != FrugalMapStoreState.Success)
            {
                throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
            }
        }

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void RemoveEntry(int key)
        {
            if (key == _loneEntry.Key)
            {
                _loneEntry.Key = 0x7fffffff;
                _loneEntry.Value = Object.BaseProperty.UnsetValue;
            }
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Search(int key)
        {
            if (key == _loneEntry.Key)
            {
                return _loneEntry.Value;
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
            get
            {
                if (0x7fffffff != _loneEntry.Key)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}