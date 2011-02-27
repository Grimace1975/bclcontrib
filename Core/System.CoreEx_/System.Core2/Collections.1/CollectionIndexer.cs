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
namespace System.Collections
{
	/// <summary>
	/// A generic interface providing a generically typed indexer. Allows passing of object instances implementing interface
	/// without explicit knowledge of the object implementation specifics.
	/// </summary>
	/// <typeparam name="TKey">Generic type of the key for the indexed object instance.</typeparam>
	/// <typeparam name="TValue">Generic type for the value in the indexed object instance.</typeparam>
	public interface ICollectionIndexer<TKey, TValue> : IIndexer<TKey, TValue>
	{
		/// <summary>
		/// Clears this instance.
		/// </summary>
		void Clear();
		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		int Count { get; }
		/// <summary>
		/// Determines whether the specified key exists.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		/// 	<c>true</c> if the specified key exists; otherwise, <c>false</c>.
		/// </returns>
		bool ContainsKey(TKey key);
		/// <summary>
		/// Removes the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		bool Remove(TKey key);
		/// <summary>
		/// Gets the ICollection instance containing the Keys for the collection.
		/// </summary>
		/// <value>The key enum.</value>
		ICollection<TKey> Keys { get; }
		/// <summary>
		/// Gets the ICollection instance containing the Values for the collection.
		/// </summary>
		/// <value>The value enum.</value>
		ICollection<TValue> Values { get; }
	}

	/// <summary>
	/// Provides a standardized, abstract, generic base class for collection classes. Provides no implementation details by itself.
	/// </summary>
	/// <typeparam name="TKey">Generic type of the key for the collection.</typeparam>
	/// <typeparam name="TValue">Generic type for the values in the collection.</typeparam>
	public abstract class CollectionIndexer<TKey, TValue> : Indexer<TKey, TValue>, ICollectionIndexer<TKey, TValue>
	{
		/// <summary>
		/// Clears this instance.
		/// </summary>
		public abstract void Clear();

		/// <summary>
		/// Gets the count of the items in collection.
		/// </summary>
		/// <value>The count.</value>
		public abstract int Count { get; }

        ///// <summary>
        ///// Gets the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.
        ///// </summary>
        ///// <param name="key">The key whose value to get or set.</param>
        ///// <param name="defaultValue">The default value to return if no value is found associated with <c>key</c>.</param>
        ///// <returns></returns>
        ///// <value>Returns the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.</value>
        //public abstract TValue GetValue(TKey key, TValue defaultValue);
        ///// <summary>
        ///// Gets the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.
        ///// </summary>
        ///// <param name="index">The index whose value to get or set.</param>
        ///// <returns></returns>
        ///// <value>Returns the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.</value>
        //public abstract TValue GetValueAt(int index);

		/// <summary>
		/// Determines whether the item in collection with specified key exists.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>
		/// 	<c>true</c> if the specified item exists; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool ContainsKey(TKey key);

		/// <summary>
		/// Removes the item with the specified key.
		/// </summary>
		/// <param name="key">The key to use.</param>
		/// <returns></returns>
		public abstract bool Remove(TKey key);

		/// <summary>
		/// Return an instance of <see cref="System.Collections.Generic.ICollection{TKey}"/> representing the collection of 
		/// keys in the indexed collection.
		/// </summary>
		/// <value>The <see cref="System.Collections.Generic.ICollection{TKey}"/> instance containing the collection of keys.</value>
		public abstract ICollection<TKey> Keys { get; }

		/// <summary>
		/// Tries the get value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public abstract bool TryGetValue(TKey key, out TValue value);

		/// <summary>
		/// Tries the get value at.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public abstract bool TryGetValueAt(int index, out TValue value);

		/// <summary>
		/// Return an instance of <see cref="System.Collections.Generic.ICollection{TValue}"/> representing the collection of 
		/// values in the indexed collection.
		/// </summary>
		/// <value>The <see cref="System.Collections.Generic.ICollection{TValue}"/> instance containing the collection of values.</value>
		public abstract ICollection<TValue> Values { get; }
	}
}
