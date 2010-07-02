namespace System.Collections.Frugal
{
    /// <summary>
    /// ThreeObjectMap
    /// </summary>
    public sealed class ThreeObjectMap : FrugalMapBase
    {
        private ushort _count;
        private FrugalMapBase.Entry _entry0;
        private FrugalMapBase.Entry _entry1;
        private FrugalMapBase.Entry _entry2;
        private bool _sorted;
        private const int SIZE = 3;

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
                switch (index)
                {
                    case 0:
                        key = _entry0.Key;
                        value = _entry0.Value;
                        return;
                    case 1:
                        key = _entry1.Key;
                        value = _entry1.Value;
                        return;
                    case 2:
                        key = _entry2.Key;
                        value = _entry2.Value;
                        return;
                }
                key = 0x7fffffff;
                value = Object.BaseProperty.UnsetValue;
            }
            else
            {
                key = 0x7fffffff;
                value = Object.BaseProperty.UnsetValue;
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
            switch (_count)
            {
                case 1:
                    if (_entry0.Key != key)
                    {
                        break;
                    }
                    _entry0.Value = value;
                    return FrugalMapStoreState.Success;
                case 2:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key != key)
                        {
                            break;
                        }
                        _entry1.Value = value;
                        return FrugalMapStoreState.Success;
                    }
                    _entry0.Value = value;
                    return FrugalMapStoreState.Success;
                case 3:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key == key)
                        {
                            _entry1.Value = value;
                            return FrugalMapStoreState.Success;
                        }
                        if (_entry2.Key == key)
                        {
                            _entry2.Value = value;
                            return FrugalMapStoreState.Success;
                        }
                        break;
                    }
                    _entry0.Value = value;
                    return FrugalMapStoreState.Success;
            }
            if (3 <= _count)
            {
                return FrugalMapStoreState.SixObjectMap;
            }
            switch (_count)
            {
                case 0:
                    _entry0.Key = key;
                    _entry0.Value = value;
                    _sorted = true;
                    break;
                case 1:
                    _entry1.Key = key;
                    _entry1.Value = value;
                    _sorted = false;
                    break;
                case 2:
                    _entry2.Key = key;
                    _entry2.Value = value;
                    _sorted = false;
                    break;
            }
            _count = (ushort)(_count + 1);
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
                if (_count >= 1)
                {
                    callback(list, _entry0.Key, _entry0.Value);
                }
                if (_count >= 2)
                {
                    callback(list, _entry1.Key, _entry1.Value);
                }
                if (_count == 3)
                {
                    callback(list, _entry2.Key, _entry2.Value);
                }
            }
        }

        /// <summary>
        /// Promotes the specified new map.
        /// </summary>
        /// <param name="newMap">The new map.</param>
        public override void Promote(FrugalMapBase newMap)
        {
            if (newMap.InsertEntry(_entry0.Key, _entry0.Value) != FrugalMapStoreState.Success)
            {
                throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
            }
            if (newMap.InsertEntry(_entry1.Key, _entry1.Value) != FrugalMapStoreState.Success)
            {
                throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
            }
            if (newMap.InsertEntry(_entry2.Key, _entry2.Value) != FrugalMapStoreState.Success)
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
            switch (_count)
            {
                case 1:
                    if (_entry0.Key != key)
                    {
                        break;
                    }
                    _entry0.Key = 0x7fffffff;
                    _entry0.Value = Object.BaseProperty.UnsetValue;
                    _count = (ushort)(_count - 1);
                    return;
                case 2:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key != key)
                        {
                            break;
                        }
                        _entry1.Key = 0x7fffffff;
                        _entry1.Value = Object.BaseProperty.UnsetValue;
                        _count = (ushort)(_count - 1);
                        return;
                    }
                    _entry0 = _entry1;
                    _entry1.Key = 0x7fffffff;
                    _entry1.Value = Object.BaseProperty.UnsetValue;
                    _count = (ushort)(_count - 1);
                    return;
                case 3:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key == key)
                        {
                            _entry1 = _entry2;
                            _entry2.Key = 0x7fffffff;
                            _entry2.Value = Object.BaseProperty.UnsetValue;
                            _count = (ushort)(_count - 1);
                            return;
                        }
                        if (_entry2.Key == key)
                        {
                            _entry2.Key = 0x7fffffff;
                            _entry2.Value = Object.BaseProperty.UnsetValue;
                            _count = (ushort)(_count - 1);
                        }
                        break;
                    }
                    _entry0 = _entry1;
                    _entry1 = _entry2;
                    _entry2.Key = 0x7fffffff;
                    _entry2.Value = Object.BaseProperty.UnsetValue;
                    _count = (ushort)(_count - 1);
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Search(int key)
        {
            if (_count > 0)
            {
                if (_entry0.Key == key)
                {
                    return _entry0.Value;
                }
                if (_count > 1)
                {
                    if (_entry1.Key == key)
                    {
                        return _entry1.Value;
                    }
                    if ((_count > 2) && (_entry2.Key == key))
                    {
                        return _entry2.Value;
                    }
                }
            }
            return Object.BaseProperty.UnsetValue;
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public override void Sort()
        {
            if ((_sorted == false) && (_count > 1))
            {
                FrugalMapBase.Entry entry;
                if (_entry0.Key > _entry1.Key)
                {
                    entry = _entry0;
                    _entry0 = _entry1;
                    _entry1 = entry;
                }
                if ((_count > 2) && (_entry1.Key > _entry2.Key))
                {
                    entry = _entry1;
                    _entry1 = _entry2;
                    _entry2 = entry;
                    if (_entry0.Key > _entry1.Key)
                    {
                        entry = _entry0;
                        _entry0 = _entry1;
                        _entry1 = entry;
                    }
                }
                _sorted = true;
            }
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