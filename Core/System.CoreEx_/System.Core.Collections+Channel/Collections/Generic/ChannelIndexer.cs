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
	/// Abstract class used as base for indexed channel classes.
	/// </summary>
	/// <typeparam name="TKey">The generic type of the key.</typeparam>
	/// <typeparam name="TValue">The generic type of the indexed channel value.</typeparam>
	public abstract class ChannelIndexer<TKey, TValue> : CollectionIndexer<TKey, TValue>
	{
        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <value>The channels.</value>
		public abstract Dictionary<TKey, TValue> Channels { get; }

        /// <summary>
        /// Gets or sets the channel key.
        /// </summary>
        /// <value>The channel key.</value>
		public abstract string ChannelKey { get; set; }

        /// <summary>
        /// Gets the channel head.
        /// </summary>
        /// <value>The channel head.</value>
		public abstract TValue ChannelHead { get; }
	}
}
