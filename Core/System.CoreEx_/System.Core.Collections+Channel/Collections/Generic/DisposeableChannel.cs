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
//namespace System.Collections.Generic
//{
//    /// <summary>
//    /// Base classes used for all channel type classes. Provide core hierarchical and collection type functionality
//    /// indicative of a channel class. Derived instances can contain instances of its own type within the collection
//    /// and hierarchy.
//    /// </summary>
//    /// <typeparam name="TValue">The type of the value.</typeparam>
//    /// <typeparam name="TChannel">The type of the channel.</typeparam>
//    public abstract class DisposeableChannelBase<TChannel, TValue> : ChannelBase<TChannel, TValue>
//        where TChannel : ChannelBase<TChannel, TValue>
//        where TValue : System.IDisposable
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="ChannelBase{TValue, TChannel}"/> class.
//        /// </summary>
//        protected DisposeableChannelBase()
//            : base()
//        {
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="ChannelBase&lt;TValue, TChannel&gt;"/> class.
//        /// </summary>
//        /// <param name="channelKey">The channel key.</param>
//        /// <param name="channelHead">The channel head.</param>
//        protected DisposeableChannelBase(string channelKey, TChannel channelParent, TChannel channelHead)
//            : base(channelKey, channelParent, channelHead)
//        {
//        }
//        /// <summary>
//        /// Implements the virtual <see cref="Instinct.Base.Dispose(bool)"/> method of
//        /// <see cref="Instinct.Base"/>.
//        /// </summary>
//        /// <param name="disposing">If set to <c>true</c> release any unmanaged resources
//        /// used by <see cref="Instinct.ChannelBase{TValue, TChannel}"/>.</param>
//        /// <remarks>Calls <c>Dispose()</c> on the underlying item hash and channel index collection instances.</remarks>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing == true)
//            {
//                m_itemHash.Dispose();
//                m_channelIndex.Dispose();
//            }
//        }

//        /// <summary>
//        /// Clears this the underlying Hash class instance.
//        /// </summary>
//        public virtual void Clear()
//        {
//            m_itemHash.Clear();
//            //+ propagate clear
//            m_channelIndex.Clear();
//        }

//        #region CHANNEL

//        /// <summary>
//        /// Gets the index base class instance associated with the current instance of the Channel class.
//        /// </summary>
//        /// <value>The channel.</value>
//        public ChannelIndexBase<string, TChannel> Channel
//        {
//            get { return m_channelIndex; }
//        }
//        /// <summary>
//        /// ChannelIndex
//        /// </summary>
//        private class ChannelIndex : ChannelIndexBase<string, TChannel>
//        {
//            private ChannelBase<TValue, TChannel> m_channel;
//            private Dictionary<string, TChannel> m_channelHash;

//            /// <summary>
//            /// Initializes a new instance of the <see cref="ChannelBase&lt;TValue, TChannel&gt;.ChannelIndex"/> class.
//            /// </summary>
//            /// <param name="channel">The channel.</param>
//            public ChannelIndex(ChannelBase<TValue, TChannel> channel)
//                : base()
//            {
//                m_channel = channel;
//            }
//            ///// <summary>
//            ///// Releases unmanaged and - optionally - managed resources
//            ///// </summary>
//            //public override void Dispose()
//            //{
//            //    if (m_channelHash != null)
//            //    {
//            //        m_channelHash.Dispose();
//            //    }
//            //    base.Dispose();
//            //}

//            /// <summary>
//            /// Clears this instance.
//            /// </summary>
//            public override void Clear()
//            {
//                if (m_channelHash != null)
//                {
//                    //foreach (string channelKey in m_channelHash.Keys)
//                    //{
//                    //    m_channelHash[channelKey].Dispose();
//                    //}
//                    m_channelHash.Clear();
//                }
//            }

//            /// <summary>
//            /// Removes the item with the specified key.
//            /// </summary>
//            /// <param name="key">The key to use.</param>
//            /// <returns></returns>
//            public override bool Remove(string key)
//            {
//                if (key == null)
//                {
//                    throw new ArgumentNullException("key");
//                }
//                return (m_channelHash == null ? false : m_channelHash.Remove(key));
//            }
//        }
//        #endregion CHANNEL
//    }
//}