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
    /// Extends <see cref="T:Instinct.Index.DisposeableDictionaryCollectionIndex">DisposeableDictionaryCollectionIndex</see> with
    /// auto-create functionality when attempt made to access a value with a key that does not exist.
    /// </summary>
    /// <typeparam name="TKey">Generic type of a key</typeparam>
    /// <typeparam name="TValue">Generic type of a value in the collection</typeparam>

    public class DisposeableDictionaryCollectionIndexerWithCreate<TKey, TValue> : DisposeableDictionaryCollectionIndexer<TKey, TValue>
        where TValue : IDisposable, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposeableDictionaryCollectionIndexWithCreate&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="hash">The hash.</param>
        public DisposeableDictionaryCollectionIndexerWithCreate(Dictionary<TKey, TValue> hash)
            : base(hash) { }

        /// <summary>
        /// Gets or sets the value (of generic parameter type TValue) associated with the specified key.
        /// </summary>
        /// <value></value>
        public override TValue this[TKey key]
        {
            get
            {
                if (_hash == null)
                    throw new InvalidOperationException("_hash is null");
                TValue value;
                if (!_hash.TryGetValue(key, out value))
                {
                    // create
                    value = CreateValue(key);
                    _hash.Add(key, value);
                }
                return value;
            }
        }

        /// <summary>
        /// Allows creation of instances of the type originally associated with keys used by this generic collection class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Instance of TValue created.</returns>
        protected virtual TValue CreateValue(TKey key)
        {
            return new TValue();
        }
    }
}
