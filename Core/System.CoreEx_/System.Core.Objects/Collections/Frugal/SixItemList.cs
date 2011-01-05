namespace System.Collections.Frugal
{
    /// <summary>
    /// SixItemList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SixItemList<T> : FrugalListBase<T>
    {
        private T _entry0;
        private T _entry1;
        private T _entry2;
        private T _entry3;
        private T _entry4;
        private T _entry5;
        private const int SIZE = 6;

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
                case 3:
                    _entry3 = value;
                    break;
                case 4:
                    _entry4 = value;
                    break;
                case 5:
                    _entry5 = value;
                    break;
                default:
                    return FrugalListStoreState.Array;
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
            _entry3 = default(T);
            _entry4 = default(T);
            _entry5 = default(T);
            _count = 0;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            SixItemList<T> list = new SixItemList<T>();
            list.Promote((SixItemList<T>)this);
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
            if (_count >= 1)
            {
                array[index] = _entry0;
                if (_count >= 2)
                {
                    array[index + 1] = _entry1;
                    if (_count >= 3)
                    {
                        array[index + 2] = _entry2;
                        if (_count >= 4)
                        {
                            array[index + 3] = _entry3;
                            if (_count >= 5)
                            {
                                array[index + 4] = _entry4;
                                if (_count == 6)
                                {
                                    array[index + 5] = _entry5;
                                }
                            }
                        }
                    }
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
                case 3:
                    return _entry3;
                case 4:
                    return _entry4;
                case 5:
                    return _entry5;
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
                if (_count > 2)
                {
                    if (_entry2.Equals(value) == true)
                    {
                        return 2;
                    }
                    if (_count > 3)
                    {
                        if (_entry3.Equals(value) == true)
                        {
                            return 3;
                        }
                        if (_count > 4)
                        {
                            if (_entry4.Equals(value) == true)
                            {
                                return 4;
                            }
                            if ((6 == _count) && (_entry5.Equals(value) == true))
                            {
                                return 5;
                            }
                        }
                    }
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
            if (_count >= 6)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            switch (index)
            {
                case 0:
                    _entry5 = _entry4;
                    _entry4 = _entry3;
                    _entry3 = _entry2;
                    _entry2 = _entry1;
                    _entry1 = _entry0;
                    _entry0 = value;
                    break;
                case 1:
                    _entry5 = _entry4;
                    _entry4 = _entry3;
                    _entry3 = _entry2;
                    _entry2 = _entry1;
                    _entry1 = value;
                    break;
                case 2:
                    _entry5 = _entry4;
                    _entry4 = _entry3;
                    _entry3 = _entry2;
                    _entry2 = value;
                    break;
                case 3:
                    _entry5 = _entry4;
                    _entry4 = _entry3;
                    _entry3 = value;
                    break;
                case 4:
                    _entry5 = _entry4;
                    _entry4 = value;
                    break;
                case 5:
                    _entry5 = value;
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
            if (6 >= count)
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
                    case 4:
                        SetAt(0, oldList.EntryAt(0));
                        SetAt(1, oldList.EntryAt(1));
                        SetAt(2, oldList.EntryAt(2));
                        SetAt(3, oldList.EntryAt(3));
                        return;
                    case 5:
                        SetAt(0, oldList.EntryAt(0));
                        SetAt(1, oldList.EntryAt(1));
                        SetAt(2, oldList.EntryAt(2));
                        SetAt(3, oldList.EntryAt(3));
                        SetAt(4, oldList.EntryAt(4));
                        return;
                    case 6:
                        SetAt(0, oldList.EntryAt(0));
                        SetAt(1, oldList.EntryAt(1));
                        SetAt(2, oldList.EntryAt(2));
                        SetAt(3, oldList.EntryAt(3));
                        SetAt(4, oldList.EntryAt(4));
                        SetAt(5, oldList.EntryAt(5));
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
        public void Promote(SixItemList<T> oldList)
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
                case 4:
                    SetAt(0, oldList.EntryAt(0));
                    SetAt(1, oldList.EntryAt(1));
                    SetAt(2, oldList.EntryAt(2));
                    SetAt(3, oldList.EntryAt(3));
                    return;
                case 5:
                    SetAt(0, oldList.EntryAt(0));
                    SetAt(1, oldList.EntryAt(1));
                    SetAt(2, oldList.EntryAt(2));
                    SetAt(3, oldList.EntryAt(3));
                    SetAt(4, oldList.EntryAt(4));
                    return;
                case 6:
                    SetAt(0, oldList.EntryAt(0));
                    SetAt(1, oldList.EntryAt(1));
                    SetAt(2, oldList.EntryAt(2));
                    SetAt(3, oldList.EntryAt(3));
                    SetAt(4, oldList.EntryAt(4));
                    SetAt(5, oldList.EntryAt(5));
                    return;
            }
            throw new ArgumentOutOfRangeException("index");
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public void Promote(ThreeItemList<T> oldList)
        {
            int count = oldList.Count;
            if (6 <= count)
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
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool Remove(T value)
        {
            if (_entry0.Equals(value) == true)
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
                if (_count > 2)
                {
                    if (_entry2.Equals(value) == true)
                    {
                        RemoveAt(2);
                        return true;
                    }
                    if (_count > 3)
                    {
                        if (_entry3.Equals(value) == true)
                        {
                            RemoveAt(3);
                            return true;
                        }
                        if (_count > 4)
                        {
                            if (_entry4.Equals(value) == true)
                            {
                                RemoveAt(4);
                                return true;
                            }
                            if ((6 == _count) && (_entry5.Equals(value) == true))
                            {
                                RemoveAt(5);
                                return true;
                            }
                        }
                    }
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
                    _entry2 = _entry3;
                    _entry3 = _entry4;
                    _entry4 = _entry5;
                    break;
                case 1:
                    _entry1 = _entry2;
                    _entry2 = _entry3;
                    _entry3 = _entry4;
                    _entry4 = _entry5;
                    break;
                case 2:
                    _entry2 = _entry3;
                    _entry3 = _entry4;
                    _entry4 = _entry5;
                    break;
                case 3:
                    _entry3 = _entry4;
                    _entry4 = _entry5;
                    break;
                case 4:
                    _entry4 = _entry5;
                    break;
                case 5:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index");
            }
            _entry5 = default(T);
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
                case 3:
                    _entry3 = value;
                    return;
                case 4:
                    _entry4 = value;
                    return;
                case 5:
                    _entry5 = value;
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
            if ((value < 0) || (value > 6))
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
            if (_count >= 1)
            {
                localArray[0] = _entry0;
                if (_count >= 2)
                {
                    localArray[1] = _entry1;
                    if (_count >= 3)
                    {
                        localArray[2] = _entry2;
                        if (_count >= 4)
                        {
                            localArray[3] = _entry3;
                            if (_count >= 5)
                            {
                                localArray[4] = _entry4;
                                if (_count == 6)
                                {
                                    localArray[5] = _entry5;
                                }
                            }
                        }
                    }
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
            get { return 6; }
        }
    }
}