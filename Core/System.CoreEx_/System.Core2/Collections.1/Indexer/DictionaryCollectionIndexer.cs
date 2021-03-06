#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Collections.Generic;
namespace System.Collections.Index
{
    /// <summary>
    /// Provides hash-based collection and indexer functionality using generic types.
    /// </summary>
    /// <typeparam name="TKey">Generic type of a key</typeparam>
    /// <typeparam name="TValue">Generic type of a value in the collection</typeparam>
    public class DictionaryCollectionIndexer<TKey, TValue> : CollectionIndexer<TKey, TValue>
    {
        /// <summary>
        /// Protected Member variable exposing to derived classes the underlying Hash instance. Allows write ability to subclasses.
        /// </summary>
        protected Dictionary<TKey, TValue> _hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCollectionIndex&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="hash">The hash.</param>
        public DictionaryCollectionIndexer(Dictionary<TKey, TValue> hash)
            : base()
        {
            _hash = hash;
        }

        /// <summary>
        /// Gets or sets the value (of generic parameter type TValue) associated with the specified key.
        /// </summary>
        /// <value>Value associate with key provided.</value>
        public override TValue this[TKey key]
        {
            get
            {
                if (_hash == null)
                    throw new InvalidOperationException("_hash is null");
                TValue value;
                if (_hash.TryGetValue(key, out value))
                    return value;
                throw new ArgumentException(string.Format(Local.UndefinedItemAB, "Dictionary", key), "key");
            }
            set
            {
                if (_hash == null)
                    throw new InvalidOperationException("_hash is null");
                _hash[key] = value;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear()
        {
            if (_hash != null)
                _hash.Clear();
        }

        /// <summary>
        /// Gets the count of the items in collection.
        /// </summary>
        /// <value>The count.</value>
        public override int Count
        {
            get { return (_hash != null ? _hash.Count : 0); }
        }

        /// <summary>
        /// Gets the underlying Hash instance being used.
        /// </summary>
        /// <value>The hash.</value>
        public Dictionary<TKey, TValue> Hash
        {
            get { return _hash; }
        }

        /// <summary>
        /// Determines whether the item in collection with specified key exists.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>
        /// 	<c>true</c> if the specified item exists; otherwise, <c>false</c>.
        /// </returns>
        public override bool ContainsKey(TKey key)
        {
            return (_hash != null ? _hash.ContainsKey(key) : false);
        }

        /// <summary>
        /// Removes the item with the specified key.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <returns></returns>
        public override bool Remove(TKey key)
        {
            return (_hash != null ? _hash.Remove(key) : false);
        }

        /// <summary>
        /// Return an instance of <see cref="System.Collections.Generic.ICollection{TKey}"/> representing the collection of
        /// keys in the indexed collection.
        /// </summary>
        /// <value>
        /// The <see cref="System.Collections.Generic.ICollection{TKey}"/> instance containing the collection of keys.
        /// </value>
        public override ICollection<TKey> Keys
        {
            get { return (_hash != null ? _hash.Keys : null); }
        }

        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (_hash != null)
                return _hash.TryGetValue(key, out value);
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Tries the get value at.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool TryGetValueAt(int index, out TValue value)
        {
            if (_hash != null)
            {
                throw new NotSupportedException();
                //throw new InvalidOperationException("_hash is null");
                //if (index >= _hash.Count)
                //    throw new ArgumentOutOfRangeException("key");
                //return _hash.TryGetValue(index, out value);
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Return an instance of <see cref="System.Collections.Generic.ICollection{TValue}"/> representing the collection of
        /// values in the indexed collection.
        /// </summary>
        /// <value>
        /// The <see cref="System.Collections.Generic.ICollection{TValue}"/> instance containing the collection of values.
        /// </value>
        public override ICollection<TValue> Values
        {
            get { return (_hash != null ? _hash.Values : null); }
        }
    }
}
