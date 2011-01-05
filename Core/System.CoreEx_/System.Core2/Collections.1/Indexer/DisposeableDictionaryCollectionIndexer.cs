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
	public class DisposeableDictionaryCollectionIndexer<TKey, TValue> : DictionaryCollectionIndexer<TKey, TValue>
		where TValue : IDisposable
	{
		private bool _disposed = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="HashCollectionIndex&lt;TKey, TValue&gt;"/> class.
		/// </summary>
		/// <param name="hash">The hash.</param>
		public DisposeableDictionaryCollectionIndexer(Dictionary<TKey, TValue> hash)
			: base(hash) { }
		/// <summary>
		/// Implements the <see cref="System.IDisposable.Dispose"/> method of
		/// <see cref="System.IDisposable"/>.
		/// </summary>
		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				Dispose(true);
			}
		}
		/// <summary>
		/// Disposes this instance of the object.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if ((!disposing) && (_hash != null))
			{
				foreach (TValue value in _hash.Values)
					value.Dispose();
				_hash.Clear();
			}
		}

		/// <summary>
		/// Gets or sets the value (of generic parameter type TValue) associated with the specified key.
		/// </summary>
		/// <value>Value associate with key provided.</value>
		public override TValue this[TKey key]
		{
			set
			{
				if (_hash == null)
					throw new InvalidOperationException("_hash is null");
				TValue lastValue;
				if (_hash.TryGetValue(key, out lastValue))
					lastValue.Dispose();
				_hash[key] = value;
			}
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public override void Clear()
		{
			if (_hash != null)
			{
				foreach (TValue value in _hash.Values)
					value.Dispose();
				_hash.Clear();
			}
		}

		/// <summary>
		/// Removes the item with the specified key.
		/// </summary>
		/// <param name="key">The key to use.</param>
		/// <returns></returns>
		public override bool Remove(TKey key)
		{
			if (_hash != null)
			{
				TValue lastValue;
				if (_hash.TryGetValue(key, out lastValue))
					lastValue.Dispose();
				return _hash.Remove(key);
			}
			return false;
		}
	}
}
