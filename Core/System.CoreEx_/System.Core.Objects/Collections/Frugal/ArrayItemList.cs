using System.Collections.Generic;
using System.Collections;
namespace System.Collections.Frugal
{
    /// <summary>
    /// ArrayItemList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ArrayItemList<T> : FrugalListBase<T>
    {
        private T[] _entries;
        private const int GROWTH = 3;
        private const int LARGEGROWTH = 0x12;
        private const int MINSIZE = 9;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayItemList&lt;T&gt;"/> class.
        /// </summary>
        public ArrayItemList()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayItemList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ArrayItemList(ICollection<T> collection)
        {
            if (collection != null)
            {
                _count = collection.Count;
                _entries = new T[_count];
                collection.CopyTo(_entries, 0);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayItemList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public ArrayItemList(ICollection collection)
        {
            if (collection != null)
            {
                _count = collection.Count;
                _entries = new T[_count];
                collection.CopyTo(_entries, 0);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayItemList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public ArrayItemList(int size)
        {
            size += 2;
            size -= size % 3;
            _entries = new T[size];
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override FrugalListStoreState Add(T value)
        {
            if ((_entries != null) && (_count < _entries.Length))
            {
                _entries[_count] = value;
                _count++;
            }
            else
            {
                if (_entries != null)
                {
                    int length = _entries.Length;
                    if (length < 0x12)
                    {
                        length += 3;
                    }
                    else
                    {
                        length += length >> 2;
                    }
                    T[] destinationArray = new T[length];
                    System.Array.Copy(_entries, 0, destinationArray, 0, _entries.Length);
                    _entries = destinationArray;
                }
                else
                {
                    _entries = new T[9];
                }
                _entries[_count] = value;
                _count++;
            }
            return FrugalListStoreState.Success;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear()
        {
            for (int index = 0; index < _count; index++)
            {
                _entries[index] = default(T);
            }
            _count = 0;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ArrayItemList<T> list = new ArrayItemList<T>(Capacity);
            list.Promote((ArrayItemList<T>)this);
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
            for (int index2 = 0; index2 < _count; index2++)
            {
                array[index + index2] = _entries[index2];
            }
        }

        /// <summary>
        /// Entries at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public override T EntryAt(int index)
        {
            return _entries[index];
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override int IndexOf(T value)
        {
            for (int index = 0; index < _count; index++)
            {
                if (_entries[index].Equals(value) == true)
                {
                    return index;
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
            if ((_entries == null) || (_count >= _entries.Length))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            System.Array.Copy(_entries, index, _entries, index + 1, _count - index);
            _entries[index] = value;
            _count++;
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public void Promote(ArrayItemList<T> oldList)
        {
            int count = oldList.Count;
            if (_entries.Length < count)
            {
                throw new ArgumentException(TR.Get("FrugalList_TargetMapCannotHoldAllData", new object[] { oldList.ToString(), ToString() }), "oldList");
            }
            SetCount(oldList.Count);
            for (int index = 0; index < count; index++)
            {
                SetAt(index, oldList.EntryAt(index));
            }
        }

        /// <summary>
        /// Promotes the specified old list.
        /// </summary>
        /// <param name="oldList">The old list.</param>
        public override void Promote(FrugalListBase<T> oldList)
        {
            for (int index = 0; index < oldList.Count; index++)
            {
                if (Add(oldList.EntryAt(index)) != FrugalListStoreState.Success)
                {
                    throw new ArgumentException(TR.Get("FrugalList_TargetMapCannotHoldAllData", new object[] { oldList.ToString(), ToString() }), "oldList");
                }
            }
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
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool Remove(T value)
        {
            for (int index = 0; index < _count; index++)
            {
                if (_entries[index].Equals(value) == true)
                {
                    RemoveAt(index);
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
            int length = (_count - index) - 1;
            if (length > 0)
            {
                System.Array.Copy(_entries, index + 1, _entries, index, length);
            }
            _entries[_count - 1] = default(T);
            _count--;
        }

        /// <summary>
        /// Sets at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public override void SetAt(int index, T value)
        {
            _entries[index] = value;
        }

        /// <summary>
        /// Sets the count.
        /// </summary>
        /// <param name="value">The value.</param>
        private void SetCount(int value)
        {
            if ((value < 0) || (value > _entries.Length))
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
            for (int index = 0; index < _count; index++)
            {
                localArray[index] = _entries[index];
            }
            return localArray;
        }

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>The capacity.</value>
        public override int Capacity
        {
            get
            {
                if (_entries != null)
                {
                    return _entries.Length;
                }
                return 0;
            }
        }
    }
}