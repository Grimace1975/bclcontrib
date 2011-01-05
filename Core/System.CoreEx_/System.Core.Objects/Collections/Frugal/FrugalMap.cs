using System.Runtime.InteropServices;
using System.Collections;
namespace System.Collections.Frugal
{
    /// <summary>
    /// FrugalMap
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FrugalMap
    {
        internal FrugalMapBase _mapStore;

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
                        _mapStore = new SingleObjectMap();
                    }
                    FrugalMapStoreState state = _mapStore.InsertEntry(key, value);
                    if (state != FrugalMapStoreState.Success)
                    {
                        FrugalMapBase base2;
                        if (FrugalMapStoreState.ThreeObjectMap == state)
                        {
                            base2 = new ThreeObjectMap();
                        }
                        else if (FrugalMapStoreState.SixObjectMap == state)
                        {
                            base2 = new SixObjectMap();
                        }
                        else if (FrugalMapStoreState.Array == state)
                        {
                            base2 = new ArrayObjectMap();
                        }
                        else if (FrugalMapStoreState.SortedArray == state)
                        {
                            base2 = new SortedObjectMap();
                        }
                        else
                        {
                            if (FrugalMapStoreState.Hashtable != state)
                            {
                                throw new InvalidOperationException(TR.Get("FrugalMap_CannotPromoteBeyondHashtable"));
                            }
                            base2 = new HashObjectMap();
                        }
                        _mapStore.Promote(base2);
                        _mapStore = base2;
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
        public void Iterate(ArrayList list, FrugalMapIterationCallback callback)
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