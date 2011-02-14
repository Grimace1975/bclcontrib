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
namespace System.Collections.Generic
{
    /// <summary>
    /// Base classes used for all channel type classes. Provide core hierarchical and collection type functionality
    /// indicative of a channel class. Derived instances can contain instances of its own type within the collection
    /// and hierarchy.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TChannel">The type of the channel.</typeparam>
    public abstract class ChannelBase<TChannel, TValue> : Dictionary<string, TValue>, ICollectionKey<string>, IIndexer<string, TValue>
        where TChannel : ChannelBase<TChannel, TValue>
    {
        /// <summary>
        /// Member variable containing the type of the second type parameter.
        /// </summary>
        private TChannel _channelHead;
        /// <summary>
        /// Member variable containing of the underlying ChannelIndexBase collection class used.
        /// </summary>
        private ChannelIndex _channelIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelBase{TValue, TChannel}"/> class.
        /// </summary>
        protected ChannelBase()
            : base()
        {
            _channelHead = (TChannel)this;
            _channelIndex = new ChannelIndex(this, string.Empty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelBase&lt;TValue, TChannel&gt;"/> class.
        /// </summary>
        /// <param name="channelKey">The channel key.</param>
        /// <param name="channelHead">The channel head.</param>
        protected ChannelBase(string channelKey, TChannel channelParent, TChannel channelHead)
            : base()
        {
            if (channelKey == null)
                throw new ArgumentNullException("channelKey");
            if (channelHead == null)
                throw new ArgumentNullException("channelHead");
            _channelHead = channelHead;
            _channelIndex = new ChannelIndex(this, channelKey + CoreEx.Scope);
        }

        /// <summary>
        /// Gets or sets the item in the collection of type TValue that is associted with the specified key.
        /// </summary>
        /// <value></value>
        public new virtual TValue this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("key");
                int scopeIndex = key.IndexOf(CoreEx.Scope);
                if (scopeIndex > 0)
                    return _channelIndex[key.Substring(0, scopeIndex)][key.Substring(scopeIndex + 2)];
                TValue value;
                if (TryGetValue(key, out value))
                    return value;
                throw new ArgumentException(string.Format("Core_.Local.UndefinedKeyA", key), "key");
            }
            set
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("key");
                int scopeIndex = key.IndexOf(CoreEx.Scope);
                if (scopeIndex > 0)
                    _channelIndex[key.Substring(0, scopeIndex)][key.Substring(scopeIndex + 2)] = value;
                else
                    base[key] = value;
            }
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </exception>
        public new void Add(string key, TValue value)
        {
            this[key] = value;
        }

        /// <summary>
        /// Clears this the underlying Hash class instance.
        /// </summary>
        public new virtual void Clear()
        {
            base.Clear();
            // propagate clear
            _channelIndex.Clear();
        }

        /// <summary>
        /// Abstract class specifying the ability to create channel instances associated with the key specific and of
        /// the type provided.
        /// </summary>
        /// <param name="channelKey">The channel key.</param>
        /// <param name="channelHead">The channel head.</param>
        /// <returns></returns>
        protected abstract TChannel CreateChannel(string channelKey, TChannel channelHead);

        /// <summary>
        /// Determines whether the channel is the head.
        /// </summary>
        /// <value><c>true</c> if the channel is the head; otherwise, <c>false</c>.</value>
        public bool IsChannelHead
        {
            get { return (this == _channelHead); }
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key"/> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> is null.
        /// </exception>
        public new bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            int scopeIndex = key.IndexOf(CoreEx.Scope);
            if (scopeIndex > 0)
                return _channelIndex[key.Substring(0, scopeIndex)].Remove(key.Substring(scopeIndex + 2));
            if (ContainsKey(key))
            {
                base.Remove(key);
                return true;
            }
            return false;
        }

        #region Channels
        /// <summary>
        /// Adds a channel associated with the specific key and of the type provided to the ChannelIndex instance used.
        /// </summary>
        /// <param name="channelKey">The channel key.</param>
        /// <param name="channel">The channel.</param>
        /// <returns></returns>
        public virtual TChannel AddChannel(string channelKey, TChannel channel)
        {
            if (channelKey == null)
                throw new ArgumentNullException("channelKey");
            if (channel == null)
                throw new ArgumentNullException("channel");
            if (channel._channelHead != channel)
                throw new ArgumentException("Core_.Local.ChannelHeadRequired", "channel");
            return _channelIndex.Add(channelKey, channel, true);
        }

        /// <summary>
        /// Gets the key associated with the next leaf node.
        /// </summary>
        /// <value>The leaf key.</value>
        public string Key
        {
            get { return _channelIndex.LeafKey; }
        }

        /// <summary>
        /// Gets the index base class instance associated with the current instance of the Channel class.
        /// </summary>
        /// <value>The channel.</value>
        public ChannelIndexer<string, TChannel> Channel
        {
            get { return _channelIndex; }
        }

        /// <summary>
        /// ChannelIndex
        /// </summary>
        private class ChannelIndex : ChannelIndexer<string, TChannel>
        {
            /// <summary>
            /// Member variable containing key for instance of the class.
            /// </summary>
            protected string _channelKey;
            private ChannelBase<TChannel, TValue> _parent;
            private Dictionary<string, TChannel> _channels;

            /// <summary>
            /// Initializes a new instance of the <see cref="ChannelBase&lt;TValue, TChannel&gt;.ChannelIndex"/> class.
            /// </summary>
            /// <param name="channel">The channel.</param>
            public ChannelIndex(ChannelBase<TChannel, TValue> parent, string channelKey)
                : base()
            {
                _parent = parent;
                _channelKey = channelKey;
            }

            /// <summary>
            /// Adds the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="channel">The channel.</param>
            /// <returns></returns>
            internal TChannel Add(string key, TChannel channel, bool propogate)
            {
                if (propogate)
                    ChannelPropagateKey(_channelKey + key, channel);
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("key");
                if (key.IndexOf(CoreEx.Scope) > -1)
                    throw new ArgumentException(string.Format("Core_.Local.ScopeCharacterNotAllowedA", key), "key");
                if (channel == null)
                    throw new ArgumentNullException("channel");
                if (_channels == null)
                    _channels = new Dictionary<string, TChannel>();
                if (_channels.ContainsKey(key))
                    throw new ArgumentException(string.Format("Core_.Local.RedefineChannelA", key), "key");
                _channels[key] = channel;
                return channel;
            }

            /// <summary>
            /// Gets or sets the <see cref="TChannel"/> with the specified key.
            /// </summary>
            /// <value></value>
            public override TChannel this[string key]
            {
                get
                {
                    if (string.IsNullOrEmpty(key))
                        throw new ArgumentNullException("key");
                    if (key.IndexOf(CoreEx.Scope) > -1)
                        throw new ArgumentException(string.Format("Core_.Local.ScopeCharacterNotAllowedA", key), "key");
                    if (_channels == null)
                        _channels = new Dictionary<string, TChannel>();
                    TChannel value;
                    if (_channels.TryGetValue(key, out value))
                        return value;
                    TChannel channel = _parent.CreateChannel(_channelKey + key, _parent._channelHead);
                    _channels[key] = channel;
                    return channel;
                }
                set { throw new NotSupportedException(); }
            }

            /// <summary>
            /// Gets the <see cref="Instinct.Hash"/> instance containing the channel instances.
            /// </summary>
            /// <value>The channel hash.</value>
            public override Dictionary<string, TChannel> Channels
            {
                get { return _channels; }
            }

            /// <summary>
            /// Gets the head of the channel.
            /// </summary>
            /// <value>The channel hash.</value>
            public override TChannel ChannelHead
            {
                get { return _parent._channelHead; }
            }

            /// <summary>
            /// Clears this instance.
            /// </summary>
            public override void Clear()
            {
                if (_channels != null)
                    _channels.Clear();
            }

            /// <summary>
            /// Gets or sets the value of the key associated with this instance of the Channel.
            /// </summary>
            /// <value>The channel key.</value>
            public override string ChannelKey
            {
                get { return _channelKey; }
                set { _channelKey = value; }
            }

            /// <summary>
            /// Gets the key associated with the next leaf node.
            /// </summary>
            /// <value>The leaf key.</value>
            public string LeafKey
            {
                get
                {
                    int leafIndex;
                    if ((_channelKey.Length > 1) && ((leafIndex = _channelKey.LastIndexOf(CoreEx.Scope, _channelKey.Length - CoreEx.ScopeLength)) > -1))
                        return _channelKey.Substring(leafIndex + CoreEx.ScopeLength, _channelKey.Length - leafIndex - (CoreEx.ScopeLength << 1));
                    return _channelKey.Replace(CoreEx.Scope, string.Empty);
                }
            }

            /// <summary>
            /// Gets the count of the items in collection.
            /// </summary>
            /// <value>The count.</value>
            public override int Count
            {
                get { return (_channels == null ? 0 : _channels.Count); }
            }

            //+ recursive
            /// <summary>
            /// Propagates the specified key down throughout the collection of child channels.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="channel">The channel.</param>
            protected void ChannelPropagateKey(string key, TChannel channel)
            {
                if (!key.EndsWith(CoreEx.Scope))
                    key += CoreEx.Scope;
                // set
                channel._channelHead = _parent._channelHead;
                _channelKey = key;
                if ((_channels != null) && (_channels.Count > 0))
                    foreach (TChannel childChannel in _channels.Values)
                        ChannelPropagateKey(key + childChannel.Key, childChannel);
            }

            ///// <summary>
            ///// Gets the generic <paramref name="TChannel"/> value associated with the specified <c>key</c>.
            ///// </summary>
            ///// <param name="index">The index whose value to get or set.</param>
            ///// <returns></returns>
            ///// <value>Returns the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.</value>
            //public override TChannel GetValueAt(int index)
            //{
            //    if (m_channelHash == null)
            //        m_channelHash = new Dictionary<string, TChannel>();
            //    if (index >= m_channelHash.Count)
            //        throw new ArgumentOutOfRangeException("key");
            //    //return m_channelHash.GetValue(index);
            //    throw new NotSupportedException();
            //}
            ///// <summary>
            ///// Gets the generic <paramref name="TChannel"/> value associated with the specified <c>key</c>.
            ///// </summary>
            ///// <param name="key">The key whose value to get or set.</param>
            ///// <param name="defaultValue">The default value to return if no value is found associated with <c>key</c>.</param>
            ///// <returns></returns>
            ///// <value>Returns the generic <paramref name="TValue"/> value associated with the specified <c>key</c>.</value>
            //public override TChannel GetValue(string key, TChannel defaultValue)
            //{
            //    if (string.IsNullOrEmpty(key))
            //        throw new ArgumentNullException("key");
            //    if (key.IndexOf(CoreEx.Scope) > -1)
            //        throw new ArgumentException(string.Format("Core_.Local.ScopeCharacterNotAllowedA", key), "key");
            //    if (m_channelHash == null)
            //        m_channelHash = new Dictionary<string, TChannel>();
            //    TChannel value;
            //    if (m_channelHash.TryGetValue(key, out value) == true)
            //        return value;
            //    TChannel channel = m_channel.CreateChannel(m_channelKey + key, m_channel._channelHead);
            //    m_channelHash[key] = channel;
            //    return channel;
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
                return (_channels == null ? false : _channels.ContainsKey(key));
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
                get { return (_channels == null ? null : _channels.Keys); }
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
                return (_channels == null ? false : _channels.Remove(key));
            }

            /// <summary>
            /// Tries the get value.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public override bool TryGetValue(string key, out TChannel value)
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                if (_channels == null)
                {
                    value = null;
                    return false;
                }
                return _channels.TryGetValue(key, out value);
            }

            /// <summary>
            /// Tries the get value at.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public override bool TryGetValueAt(int index, out TChannel value)
            {
                if (_channels != null)
                {
                    throw new NotSupportedException();
                    //throw new InvalidOperationException("_hash is null");
                    //if (index >= _hash.Count)
                    //    throw new ArgumentOutOfRangeException("key");
                    //return _hash.TryGetValue(index, out value);
                }
                value = default(TChannel);
                return false;
            }

            /// <summary>
            /// Return an instance of <see cref="System.Collections.Generic.ICollection{TValue}"/> representing the collection of
            /// values in the indexed collection.
            /// </summary>
            /// <value>
            /// The <see cref="System.Collections.Generic.ICollection{TValue}"/> instance containing the collection of values.
            /// </value>
            public override ICollection<TChannel> Values
            {
                get { return (_channels == null ? null : _channels.Values); }
            }
        }
        #endregion
    }
}