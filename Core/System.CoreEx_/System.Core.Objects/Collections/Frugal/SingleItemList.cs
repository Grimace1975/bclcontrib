namespace System.Collections.Frugal
{
    /// <summary>
    /// SingleItemList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SingleItemList<T> : FrugalListBase<T>
    {
        private T _loneEntry;
        private const int SIZE = 1;

        // Methods
        public override FrugalListStoreState Add(T value)
        {
            if (_count == 0)
            {
                _loneEntry = value;
                _count++;
                return FrugalListStoreState.Success;
            }
            return FrugalListStoreState.ThreeItemList;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear()
        {
            _loneEntry = default(T);
            _count = 0;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            SingleItemList<T> list = new SingleItemList<T>();
            list.Promote((SingleItemList<T>)this);
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
            return _loneEntry.Equals(value);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public override void CopyTo(T[] array, int index)
        {
            array[index] = _loneEntry;
        }

        /// <summary>
        /// Entries at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public override T EntryAt(int index)
        {
            return _loneEntry;
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override int IndexOf(T value)
        {
            if (_loneEntry.Equals(value))
            {
                return 0;
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
            if ((_count >= 1) || (index >= 1))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            _loneEntry = value;
            _count++;
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public override void Promote(FrugalListBase<T> oldList)
        {
            if (1 != oldList.Count)
            {
                throw new ArgumentException(TR.Get("FrugalList_TargetMapCannotHoldAllData", new object[] { oldList.ToString(), ToString() }), "oldList");
            }
            SetCount(1);
            SetAt(0, oldList.EntryAt(0));
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
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool Remove(T value)
        {
            if (_loneEntry.Equals(value))
            {
                _loneEntry = default(T);
                _count--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public override void RemoveAt(int index)
        {
            if (index != 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            _loneEntry = default(T);
            _count--;
        }

        /// <summary>
        /// Sets at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public override void SetAt(int index, T value)
        {
            _loneEntry = value;
        }

        /// <summary>
        /// Sets the count.
        /// </summary>
        /// <param name="value">The value.</param>
        private void SetCount(int value)
        {
            if ((value < 0) || (value > 1))
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
            return new T[] { _loneEntry };
        }

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>The capacity.</value>
        public override int Capacity
        {
            get { return 1; }
        }
    }
}