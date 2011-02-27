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
using System.Runtime.Serialization;
using System.Collections.Generic;
namespace System.Patterns.Caching
{
    /// <summary>
    /// DataStore
    /// </summary>
    [DataContract(Name = "dataStore")]
    public class DataStore
    {
        private object _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStore"/> class.
        /// </summary>
        public DataStore()
        {
            Channel = new ChannelIndex(this);
        }

        #region Channel
        /// <summary>
        /// Gets the index base class instance associated with the current instance of the Channel class.
        /// </summary>
        /// <value>The channel.</value>
        public Collections.CollectionIndexer<string, DataStore> Channel { get; private set; }

        /// <summary>
        /// ChannelIndex
        /// </summary>
        private class ChannelIndex : Collections.CollectionIndexer<string, DataStore>
        {
            private DataStore _parent;
            private Dictionary<string, DataStore> _channelHash;

            /// <summary>
            /// Initializes a new instance of the <see cref="ChannelIndex"/> class.
            /// </summary>
            /// <param name="parent">The parent.</param>
            public ChannelIndex(DataStore parent)
                : base()
            {
                _parent = parent;
            }

            /// <summary>
            /// Gets or sets the <see cref="TChannel"/> with the specified key.
            /// </summary>
            /// <value></value>
            public override DataStore this[string key]
            {
                get
                {
                    if ((string.IsNullOrEmpty(key)) || (key == CoreEx.Scope))
                        return _parent;
                    if (key.StartsWith(CoreEx.Scope))
                        key = key.Substring(2);
                    int scopeIndex = key.IndexOf(CoreEx.Scope);
                    string channelKey = (scopeIndex == -1 ? key : key.Substring(0, scopeIndex));
                    if (_channelHash == null)
                        _channelHash = new Dictionary<string, DataStore>();
                    DataStore channel2;
                    if (!_channelHash.TryGetValue(key, out channel2))
                        _channelHash[key] = channel2 = new DataStore();
                    return (scopeIndex == -1 ? channel2 : channel2.Channel[key.Substring(scopeIndex + CoreEx.ScopeLength)]);
                }
                set { throw new NotSupportedException(); }
            }

            /// <summary>
            /// Clears this instance.
            /// </summary>
            public override void Clear()
            {
                if (_channelHash != null)
                    _channelHash.Clear();
            }

            /// <summary>
            /// Gets the count of the items in collection.
            /// </summary>
            /// <value>The count.</value>
            public override int Count
            {
                get { return (_channelHash == null ? 0 : _channelHash.Count); }
            }

			///// <summary>
			///// Gets the generic <paramref name="TChannel"/> value associated with the specified <c>key</c>.
			///// </summary>
			///// <param name="key">The key whose value to get or set.</param>
			///// <param name="defaultValue">The default value to return if no value is found associated with <c>key</c>.</param>
			///// <returns></returns>
			///// <value>Returns the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.</value>
			//public override DataStore GetValue(string key, DataStore defaultValue)
			//{
			//    if ((string.IsNullOrEmpty(key)) || (key == CoreEx.Scope))
			//        return _parent;
			//    if (key.StartsWith(CoreEx.Scope))
			//        key = key.Substring(CoreEx.ScopeLength);
			//    int scopeIndex = key.IndexOf(CoreEx.Scope);
			//    string channelKey = (scopeIndex == -1 ? key : key.Substring(0, scopeIndex));
			//    if (_channelHash == null)
			//        _channelHash = new Dictionary<string, DataStore>();
			//    DataStore channel2;
			//    if (!_channelHash.TryGetValue(key, out channel2))
			//        _channelHash[key] = channel2 = new DataStore();
			//    return (scopeIndex == -1 ? channel2 : channel2.Channel[key.Substring(scopeIndex + CoreEx.ScopeLength)]);
			//}
			///// <summary>
			///// Gets the generic <paramref name="TChannel"/> value associated with the specified <c>key</c>.
			///// </summary>
			///// <param name="index">The index whose value to get or set.</param>
			///// <returns></returns>
			///// <value>Returns the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.</value>
			//public override DataStore GetValueAt(int index)
			//{
			//    throw new NotSupportedException();
			//    //if ((_channelHash == null) || (index >= _channelHash.Count))
			//    //    throw new ArgumentOutOfRangeException("key");
			//    //return _channelHash.GetValue(index);
			//}

            /// <summary>
            /// Determines whether the item in collection with specified key exists.
            /// </summary>
            /// <param name="key">The key to check.</param>
            /// <returns>
            /// 	<c>true</c> if the specified item exists; otherwise, <c>false</c>.
            /// </returns>
            public override bool ContainsKey(string key)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                return (_channelHash == null ? false : _channelHash.ContainsKey(key));
            }

            /// <summary>
            /// Return an instance of <see cref="System.Collections.Generic.ICollection{TKey}"/> representing the collection of
            /// keys in the indexed collection.
            /// </summary>
            /// <value>
            /// The <see cref="System.Collections.Generic.ICollection{TKey}"/> instance containing the collection of keys.
            /// </value>
            public override ICollection<string> Keys
            {
                get { return (_channelHash == null ? null : _channelHash.Keys); }
            }

            /// <summary>
            /// Removes the item with the specified key.
            /// </summary>
            /// <param name="key">The key to use.</param>
            /// <returns></returns>
            public override bool Remove(string key)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                return (_channelHash == null ? false : _channelHash.Remove(key));
            }

            /// <summary>
            /// Tries the get value.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public override bool TryGetValue(string key, out DataStore value)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                if (_channelHash == null)
                {
                    value = null;
                    return false;
                }
                return _channelHash.TryGetValue(key, out value);
            }

			/// <summary>
			/// Tries the get value.
			/// </summary>
			/// <param name="key">The key.</param>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			public override bool TryGetValueAt(int index, out DataStore value)
			{
				value = null;
				return false;
			}

            /// <summary>
            /// Return an instance of <see cref="System.Collections.Generic.ICollection{TValue}"/> representing the collection of
            /// values in the indexed collection.
            /// </summary>
            /// <value>
            /// The <see cref="System.Collections.Generic.ICollection{TValue}"/> instance containing the collection of values.
            /// </value>
            public override ICollection<DataStore> Values
            {
                get { return (_channelHash == null ? null : _channelHash.Values); }
            }
        }
        #endregion Channel

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data
        {
            get
            {
                var builder = (_data as DataStoreBuilder);
                if (builder != null)
                {
                    bool replaceData;
                    object data = builder(out replaceData);
                    if (replaceData)
                        _data = data;
                    return data;
                }
                return _data;
            }
            set { _data = value; }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        [DataMember(Name = "key")]
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; protected set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        [DataMember(Name = "tag")]
        public object Tag { get; set; }
    }
}
