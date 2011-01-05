namespace System.Collections.Frugal
{
    /// <summary>
    /// SortedObjectMap
    /// </summary>
    public sealed class SortedObjectMap : FrugalMapBase
    {
        internal int _count;
        private FrugalMapBase.Entry[] _entries;
        private int _lastKey = 0x7fffffff;
        private const int GROWTH = 8;
        private const int MAXSIZE = 0x80;
        private const int MINSIZE = 0x10;

        /// <summary>
        /// Finds the index of the insert.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="found">if set to <c>true</c> [found].</param>
        /// <returns></returns>
        private int FindInsertIndex(int key, out bool found)
        {
            int index = 0;
            if ((_count > 0) && (key <= _lastKey))
            {
                int num2 = _count - 1;
                do
                {
                    int num3 = (num2 + index) / 2;
                    if (key <= _entries[num3].Key)
                    {
                        num2 = num3;
                    }
                    else
                    {
                        index = num3 + 1;
                    }
                }
                while (index < num2);
                found = key == _entries[index].Key;
                return index;
            }
            index = _count;
            found = false;
            return index;
        }

        /// <summary>
        /// Gets the key value pair.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public override void GetKeyValuePair(int index, out int key, out object value)
        {
            if (index < _count)
            {
                value = _entries[index].Value;
                key = _entries[index].Key;
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
            bool flag;
            int index = FindInsertIndex(key, out flag);
            if (flag)
            {
                _entries[index].Value = value;
                return FrugalMapStoreState.Success;
            }
            if (0x80 <= _count)
            {
                return FrugalMapStoreState.Hashtable;
            }
            if (_entries != null)
            {
                if (_entries.Length <= _count)
                {
                    FrugalMapBase.Entry[] destinationArray = new FrugalMapBase.Entry[_entries.Length + 8];
                    System.Array.Copy(_entries, 0, destinationArray, 0, _entries.Length);
                    _entries = destinationArray;
                }
            }
            else
            {
                _entries = new FrugalMapBase.Entry[0x10];
            }
            if (index < _count)
            {
                System.Array.Copy(_entries, index, _entries, index + 1, _count - index);
            }
            else
            {
                _lastKey = key;
            }
            _entries[index].Key = key;
            _entries[index].Value = value;
            _count++;
            return FrugalMapStoreState.Success;
        }

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public override void Iterate(System.Collections.ArrayList list, FrugalMapIterationCallback callback)
        {
            if (_count > 0)
            {
                for (int index = 0; index < _count; index++)
                {
                    callback(list, _entries[index].Key, _entries[index].Value);
                }
            }
        }

        /// <summary>
        /// Promotes the specified new map.
        /// </summary>
        /// <param name="newMap">The new map.</param>
        public override void Promote(FrugalMapBase newMap)
        {
            for (int index = 0; index < _entries.Length; index++)
            {
                if (newMap.InsertEntry(_entries[index].Key, _entries[index].Value) != FrugalMapStoreState.Success)
                {
                    throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
                }
            }
        }

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void RemoveEntry(int key)
        {
            bool flag;
            int destinationIndex = FindInsertIndex(key, out flag);
            if (flag == true)
            {
                int length = (_count - destinationIndex) - 1;
                if (length > 0)
                {
                    System.Array.Copy(_entries, destinationIndex + 1, _entries, destinationIndex, length);
                }
                else if (_count > 1)
                {
                    _lastKey = _entries[_count - 2].Key;
                }
                else
                {
                    _lastKey = 0x7fffffff;
                }
                _entries[_count - 1].Key = 0x7fffffff;
                _entries[_count - 1].Value = Object.BaseProperty.UnsetValue;
                _count--;
            }
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Search(int key)
        {
            bool flag;
            int index = FindInsertIndex(key, out flag);
            if (flag == true)
            {
                return _entries[index].Value;
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
            get { return _count; }
        }
    }
}