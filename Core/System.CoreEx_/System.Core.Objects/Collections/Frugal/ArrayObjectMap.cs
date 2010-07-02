using System.Collections;
namespace System.Collections.Frugal
{
    /// <summary>
    /// ArrayObjectMap
    /// </summary>
    public sealed class ArrayObjectMap : FrugalMapBase
    {
        private ushort _count;
        private FrugalMapBase.Entry[] m_entries;
        private bool m_sorted;
        private const int GROWTH = 3;
        private const int MAXSIZE = 15;
        private const int MINSIZE = 9;

        /// <summary>
        /// Compares the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        private int Compare(int left, int right)
        {
            return (m_entries[left].Key - m_entries[right].Key);
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
                value = m_entries[index].Value;
                key = m_entries[index].Key;
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
            for (int index = 0; index < _count; index++)
            {
                if (m_entries[index].Key == key)
                {
                    m_entries[index].Value = value;
                    return FrugalMapStoreState.Success;
                }
            }
            if (15 <= _count)
            {
                return FrugalMapStoreState.SortedArray;
            }
            if (m_entries != null)
            {
                m_sorted = false;
                if (m_entries.Length <= _count)
                {
                    FrugalMapBase.Entry[] destinationArray = new FrugalMapBase.Entry[m_entries.Length + 3];
                    System.Array.Copy(m_entries, 0, destinationArray, 0, m_entries.Length);
                    m_entries = destinationArray;
                }
            }
            else
            {
                m_entries = new FrugalMapBase.Entry[9];
                m_sorted = true;
            }
            m_entries[_count].Key = key;
            m_entries[_count].Value = value;
            _count = (ushort)(_count + 1);
            return FrugalMapStoreState.Success;
        }

        /// <summary>
        /// Iterates the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="callback">The callback.</param>
        public override void Iterate(ArrayList list, FrugalMapIterationCallback callback)
        {
            if (_count > 0)
            {
                for (int index = 0; index < _count; index++)
                {
                    callback(list, m_entries[index].Key, m_entries[index].Value);
                }
            }
        }

        /// <summary>
        /// Partitions the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        private int Partition(int left, int right)
        {
            FrugalMapBase.Entry entry;
            int num = right;
            int index = left - 1;
            int num3 = right;
        Label_0008:
            while (Compare(++index, num) < 0) ;
            while (Compare(num, --num3) < 0)
            {
                if (num3 == left)
                {
                    break;
                }
            }
            if (index < num3)
            {
                entry = m_entries[num3];
                m_entries[num3] = m_entries[index];
                m_entries[index] = entry;
                goto Label_0008;
            }
            entry = m_entries[right];
            m_entries[right] = m_entries[index];
            m_entries[index] = entry;
            return index;
        }

        /// <summary>
        /// Promotes the specified new map.
        /// </summary>
        /// <param name="newMap">The new map.</param>
        public override void Promote(FrugalMapBase newMap)
        {
            for (int index = 0; index < m_entries.Length; index++)
            {
                if (newMap.InsertEntry(m_entries[index].Key, m_entries[index].Value) != FrugalMapStoreState.Success)
                {
                    throw new ArgumentException(TR.Get("FrugalMap_TargetMapCannotHoldAllData", new object[] { ToString(), newMap.ToString() }), "newMap");
                }
            }
        }

        /// <summary>
        /// Qs the sort.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        private void QSort(int left, int right)
        {
            if (left < right)
            {
                int num = Partition(left, right);
                QSort(left, num - 1);
                QSort(num + 1, right);
            }
        }

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void RemoveEntry(int key)
        {
            for (int index = 0; index < _count; index++)
            {
                if (m_entries[index].Key == key)
                {
                    int length = (_count - index) - 1;
                    if (length > 0)
                    {
                        System.Array.Copy(m_entries, index + 1, m_entries, index, length);
                    }
                    m_entries[_count - 1].Key = 0x7fffffff;
                    m_entries[_count - 1].Value = Object.BaseProperty.UnsetValue;
                    _count = (ushort)(_count - 1);
                    return;
                }
            }
        }

        /// <summary>
        /// Searches the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override object Search(int key)
        {
            for (int index = 0; index < _count; index++)
            {
                if (key == m_entries[index].Key)
                {
                    return m_entries[index].Value;
                }
            }
            return Object.BaseProperty.UnsetValue;
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        public override void Sort()
        {
            if ((m_sorted == false) && (_count > 1))
            {
                QSort(0, _count - 1);
                m_sorted = true;
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