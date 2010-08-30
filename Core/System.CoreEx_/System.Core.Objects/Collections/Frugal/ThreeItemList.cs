namespace System.Collections.Frugal
{
    /// <summary>
    /// ThreeItemList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ThreeItemList<T> : FrugalListBase<T>
    {
        private T _entry0;
        private T _entry1;
        private T _entry2;
        private const int SIZE = 3;

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override FrugalListStoreState Add(T value)
        {
            switch (_count)
            {
                case 0:
                    _entry0 = value;
                    break;
                case 1:
                    _entry1 = value;
                    break;
                case 2:
                    _entry2 = value;
                    break;
                default:
                    return FrugalListStoreState.SixItemList;
            }
            _count++;
            return FrugalListStoreState.Success;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear()
        {
            _entry0 = default(T);
            _entry1 = default(T);
            _entry2 = default(T);
            _count = 0;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ThreeItemList<T> list = new ThreeItemList<T>();
            list.Promote((ThreeItemList<T>)this);
            return list;
        }

        /// <summary>
        /// Determines whether [contains] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(T value)
        {
            return (-1 != IndexOf(value));
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public override void CopyTo(T[] array, int index)
        {
            array[index] = _entry0;
            if (_count >= 2)
            {
                array[index + 1] = _entry1;
                if (_count == 3)
                {
                    array[index + 2] = _entry2;
                }
            }
        }

        /// <summary>
        /// Entries at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public override T EntryAt(int index)
        {
            switch (index)
            {
                case 0:
                    return _entry0;
                case 1:
                    return _entry1;
                case 2:
                    return _entry2;
            }
            throw new ArgumentOutOfRangeException("index");
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override int IndexOf(T value)
        {
            if (_entry0.Equals(value) == true)
            {
                return 0;
            }
            if (_count > 1)
            {
                if (_entry1.Equals(value) == true)
                {
                    return 1;
                }
                if ((3 == _count) && (_entry2.Equals(value) == true))
                {
                    return 2;
                }
            }
            return -1;
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public override void Insert(int index, T value)
        {
            if (_count >= 3)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            switch (index)
            {
                case 0:
                    _entry2 = _entry1;
                    _entry1 = _entry0;
                    _entry0 = value;
                    break;
                case 1:
                    _entry2 = _entry1;
                    _entry1 = value;
                    break;
                case 2:
                    _entry2 = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index");
            }
            _count++;
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public override void Promote(FrugalListBase<T> oldList)
        {
            int count = oldList.Count;
            if (3 >= count)
            {
                SetCount(oldList.Count);
                switch (count)
                {
                    case 0:
                        return;
                    case 1:
                        SetAt(0, oldList.EntryAt(0));
                        return;
                    case 2:
                        SetAt(0, oldList.EntryAt(0));
                        SetAt(1, oldList.EntryAt(1));
                        return;
                    case 3:
                        SetAt(0, oldList.EntryAt(0));
                        SetAt(1, oldList.EntryAt(1));
                        SetAt(2, oldList.EntryAt(2));
                        return;
                }
                throw new ArgumentOutOfRangeException("index");
            }
            throw new ArgumentException(TR.Get("FrugalList_TargetMapCannotHoldAllData", new object[] { oldList.ToString(), ToString() }), "oldList");
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public void Promote(SingleItemList<T> oldList)
        {
            SetCount(oldList.Count);
            SetAt(0, oldList.EntryAt(0));
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public void Promote(ThreeItemList<T> oldList)
        {
            int count = oldList.Count;
            SetCount(oldList.Count);
            switch (count)
            {
                case 0:
                    return;
                case 1:
                    SetAt(0, oldList.EntryAt(0));
                    return;
                case 2:
                    SetAt(0, oldList.EntryAt(0));
                    SetAt(1, oldList.EntryAt(1));
                    return;
                case 3:
                    SetAt(0, oldList.EntryAt(0));
                    SetAt(1, oldList.EntryAt(1));
                    SetAt(2, oldList.EntryAt(2));
                    return;
            }
            throw new ArgumentOutOfRangeException("index");
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool Remove(T value)
        {
            if (_entry0.Equals(value))
            {
                RemoveAt(0);
                return true;
            }
            if (_count > 1)
            {
                if (_entry1.Equals(value) == true)
                {
                    RemoveAt(1);
                    return true;
                }
                if ((3 == _count) && (_entry2.Equals(value) == true))
                {
                    RemoveAt(2);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public override void RemoveAt(int index)
        {
            switch (index)
            {
                case 0:
                    _entry0 = _entry1;
                    _entry1 = _entry2;
                    break;
                case 1:
                    _entry1 = _entry2;
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index");
            }
            _entry2 = default(T);
            _count--;
        }

        /// <summary>
        /// Sets at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public override void SetAt(int index, T value)
        {
            switch (index)
            {
                case 0:
                    _entry0 = value;
                    return;
                case 1:
                    _entry1 = value;
                    return;
                case 2:
                    _entry2 = value;
                    return;
            }
            throw new ArgumentOutOfRangeException("index");
        }

        /// <summary>
        /// Sets the count.
        /// </summary>
        /// <param name="value">The value.</param>
        private void SetCount(int value)
        {
            if ((value < 0) || (value > 3))
            {
                throw new ArgumentOutOfRangeException("Count");
            }
            _count = value;
        }

        /// <summary>
        /// Toes the array.
        /// </summary>
        /// <returns></returns>
        public override T[] ToArray()
        {
            T[] localArray = new T[_count];
            localArray[0] = _entry0;
            if (_count >= 2)
            {
                localArray[1] = _entry1;
                if (_count == 3)
                {
                    localArray[2] = _entry2;
                }
            }
            return localArray;
        }

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>The capacity.</value>
        public override int Capacity
        {
            get { return 3; }
        }
    }
}