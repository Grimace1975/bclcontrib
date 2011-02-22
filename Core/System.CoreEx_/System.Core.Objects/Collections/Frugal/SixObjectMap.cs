namespace System.Collections.Frugal
{
    /// <summary>
    /// SixObjectMap
    /// </summary>
    public sealed class SixObjectMap : FrugalMapBase
    {
        private ushort m_count;
        private FrugalMapBase.Entry _entry0;
        private FrugalMapBase.Entry _entry1;
        private FrugalMapBase.Entry _entry2;
        private FrugalMapBase.Entry _entry3;
        private FrugalMapBase.Entry _entry4;
        private FrugalMapBase.Entry _entry5;
        private bool _sorted;
        private const int SIZE = 6;

        /// <summary>
        /// Gets the key value pair.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public override void GetKeyValuePair(int index, out int key, out object value)
        {
            if (index < m_count)
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
                    case 3:
                        key = _entry3.Key;
                        value = _entry3.Value;
                        return;
                    case 4:
                        key = _entry4.Key;
                        value = _entry4.Value;
                        return;
                    case 5:
                        key = _entry5.Key;
                        value = _entry5.Value;
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
            if (m_count > 0)
            {
                if (_entry0.Key == key)
                {
                    _entry0.Value = value;
                    return FrugalMapStoreState.Success;
                }
                if (m_count > 1)
                {
                    if (_entry1.Key == key)
                    {
                        _entry1.Value = value;
                        return FrugalMapStoreState.Success;
                    }
                    if (m_count > 2)
                    {
                        if (_entry2.Key == key)
                        {
                            _entry2.Value = value;
                            return FrugalMapStoreState.Success;
                        }
                        if (m_count > 3)
                        {
                            if (_entry3.Key == key)
                            {
                                _entry3.Value = value;
                                return FrugalMapStoreState.Success;
                            }
                            if (m_count > 4)
                            {
                                if (_entry4.Key == key)
                                {
                                    _entry4.Value = value;
                                    return FrugalMapStoreState.Success;
                                }
                                if ((m_count > 5) && (_entry5.Key == key))
                                {
                                    _entry5.Value = value;
                                    return FrugalMapStoreState.Success;
                                }
                            }
                        }
                    }
                }
            }
            if (6 <= m_count)
            {
                return FrugalMapStoreState.Array;
            }
            _sorted = false;
            switch (m_count)
            {
                case 0:
                    _entry0.Key = key;
                    _entry0.Value = value;
                    _sorted = true;
                    break;
                case 1:
                    _entry1.Key = key;
                    _entry1.Value = value;
                    break;
                case 2:
                    _entry2.Key = key;
                    _entry2.Value = value;
                    break;
                case 3:
                    _entry3.Key = key;
                    _entry3.Value = value;
                    break;
                case 4:
                    _entry4.Key = key;
                    _entry4.Value = value;
                    break;
                case 5:
                    _entry5.Key = key;
                    _entry5.Value = value;
                    break;
            }
            m_count = (ushort)(m_count + 1);
            return FrugalMapStoreState.Success;
        }

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public override void Iterate(System.Collections.ArrayList list, FrugalMapIterationCallback callback)
        {
            if (m_count > 0)
            {
                if (m_count >= 1)
                {
                    callback(list, _entry0.Key, _entry0.Value);
                }
                if (m_count >= 2)
                {
                    callback(list, _entry1.Key, _entry1.Value);
                }
                if (m_count >= 3)
                {
                    callback(list, _entry2.Key, _entry2.Value);
                }
                if (m_count >= 4)
                {
                    callback(list, _entry3.Key, _entry3.Value);
                }
                if (m_count >= 5)
                {
                    callback(list, _entry4.Key, _entry4.Value);
                }
                if (m_count == 6)
                {
                    callback(list, _entry5.Key, _entry5.Value);
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
            if (newMap.InsertEntry(_entry3.Key, _entry3.Value) != FrugalMapStoreState.Success)
            {
                throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
            }
            if (newMap.InsertEntry(_entry4.Key, _entry4.Value) != FrugalMapStoreState.Success)
            {
                throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
            }
            if (newMap.InsertEntry(_entry5.Key, _entry5.Value) != FrugalMapStoreState.Success)
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
            switch (m_count)
            {
                case 1:
                    if (_entry0.Key != key)
                    {
                        break;
                    }
                    _entry0.Key = 0x7fffffff;
                    _entry0.Value = Object.BaseProperty.UnsetValue;
                    m_count = (ushort)(m_count - 1);
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
                        m_count = (ushort)(m_count - 1);
                        return;
                    }
                    _entry0 = _entry1;
                    _entry1.Key = 0x7fffffff;
                    _entry1.Value = Object.BaseProperty.UnsetValue;
                    m_count = (ushort)(m_count - 1);
                    return;
                case 3:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key == key)
                        {
                            _entry1 = _entry2;
                            _entry2.Key = 0x7fffffff;
                            _entry2.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry2.Key != key)
                        {
                            break;
                        }
                        _entry2.Key = 0x7fffffff;
                        _entry2.Value = Object.BaseProperty.UnsetValue;
                        m_count = (ushort)(m_count - 1);
                        return;
                    }
                    _entry0 = _entry1;
                    _entry1 = _entry2;
                    _entry2.Key = 0x7fffffff;
                    _entry2.Value = Object.BaseProperty.UnsetValue;
                    m_count = (ushort)(m_count - 1);
                    return;
                case 4:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key == key)
                        {
                            _entry1 = _entry2;
                            _entry2 = _entry3;
                            _entry3.Key = 0x7fffffff;
                            _entry3.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry2.Key == key)
                        {
                            _entry2 = _entry3;
                            _entry3.Key = 0x7fffffff;
                            _entry3.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry3.Key != key)
                        {
                            break;
                        }
                        _entry3.Key = 0x7fffffff;
                        _entry3.Value = Object.BaseProperty.UnsetValue;
                        m_count = (ushort)(m_count - 1);
                        return;
                    }
                    _entry0 = _entry1;
                    _entry1 = _entry2;
                    _entry2 = _entry3;
                    _entry3.Key = 0x7fffffff;
                    _entry3.Value = Object.BaseProperty.UnsetValue;
                    m_count = (ushort)(m_count - 1);
                    return;
                case 5:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key == key)
                        {
                            _entry1 = _entry2;
                            _entry2 = _entry3;
                            _entry3 = _entry4;
                            _entry4.Key = 0x7fffffff;
                            _entry4.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry2.Key == key)
                        {
                            _entry2 = _entry3;
                            _entry3 = _entry4;
                            _entry4.Key = 0x7fffffff;
                            _entry4.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry3.Key == key)
                        {
                            _entry3 = _entry4;
                            _entry4.Key = 0x7fffffff;
                            _entry4.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry4.Key != key)
                        {
                            break;
                        }
                        _entry4.Key = 0x7fffffff;
                        _entry4.Value = Object.BaseProperty.UnsetValue;
                        m_count = (ushort)(m_count - 1);
                        return;
                    }
                    _entry0 = _entry1;
                    _entry1 = _entry2;
                    _entry2 = _entry3;
                    _entry3 = _entry4;
                    _entry4.Key = 0x7fffffff;
                    _entry4.Value = Object.BaseProperty.UnsetValue;
                    m_count = (ushort)(m_count - 1);
                    return;
                case 6:
                    if (_entry0.Key != key)
                    {
                        if (_entry1.Key == key)
                        {
                            _entry1 = _entry2;
                            _entry2 = _entry3;
                            _entry3 = _entry4;
                            _entry4 = _entry5;
                            _entry5.Key = 0x7fffffff;
                            _entry5.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry2.Key == key)
                        {
                            _entry2 = _entry3;
                            _entry3 = _entry4;
                            _entry4 = _entry5;
                            _entry5.Key = 0x7fffffff;
                            _entry5.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry3.Key == key)
                        {
                            _entry3 = _entry4;
                            _entry4 = _entry5;
                            _entry5.Key = 0x7fffffff;
                            _entry5.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry4.Key == key)
                        {
                            _entry4 = _entry5;
                            _entry5.Key = 0x7fffffff;
                            _entry5.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                            return;
                        }
                        if (_entry5.Key == key)
                        {
                            _entry5.Key = 0x7fffffff;
                            _entry5.Value = Object.BaseProperty.UnsetValue;
                            m_count = (ushort)(m_count - 1);
                        }
                        break;
                    }
                    _entry0 = _entry1;
                    _entry1 = _entry2;
                    _entry2 = _entry3;
                    _entry3 = _entry4;
                    _entry4 = _entry5;
                    _entry5.Key = 0x7fffffff;
                    _entry5.Value = Object.BaseProperty.UnsetValue;
                    m_count = (ushort)(m_count - 1);
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
            if (m_count > 0)
            {
                if (_entry0.Key == key)
                {
                    return _entry0.Value;
                }
                if (m_count > 1)
                {
                    if (_entry1.Key == key)
                    {
                        return _entry1.Value;
                    }
                    if (m_count > 2)
                    {
                        if (_entry2.Key == key)
                        {
                            return _entry2.Value;
                        }
                        if (m_count > 3)
                        {
                            if (_entry3.Key == key)
                            {
                                return _entry3.Value;
                            }
                            if (m_count > 4)
                            {
                                if (_entry4.Key == key)
                                {
                                    return _entry4.Value;
                                }
                                if ((m_count > 5) && (_entry5.Key == key))
                                {
                                    return _entry5.Value;
                                }
                            }
                        }
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
            if ((_sorted == false) && (m_count > 1))
            {
                bool flag;
                do
                {
                    FrugalMapBase.Entry entry;
                    flag = false;
                    if (_entry0.Key > _entry1.Key)
                    {
                        entry = _entry0;
                        _entry0 = _entry1;
                        _entry1 = entry;
                        flag = true;
                    }
                    if (m_count > 2)
                    {
                        if (_entry1.Key > _entry2.Key)
                        {
                            entry = _entry1;
                            _entry1 = _entry2;
                            _entry2 = entry;
                            flag = true;
                        }
                        if (m_count > 3)
                        {
                            if (_entry2.Key > _entry3.Key)
                            {
                                entry = _entry2;
                                _entry2 = _entry3;
                                _entry3 = entry;
                                flag = true;
                            }
                            if (m_count > 4)
                            {
                                if (_entry3.Key > _entry4.Key)
                                {
                                    entry = _entry3;
                                    _entry3 = _entry4;
                                    _entry4 = entry;
                                    flag = true;
                                }
                                if ((m_count > 5) && (_entry4.Key > _entry5.Key))
                                {
                                    entry = _entry4;
                                    _entry4 = _entry5;
                                    _entry5 = entry;
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                while (flag);
                _sorted = true;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public override int Count
        {
            get { return m_count; }
        }
    }
}