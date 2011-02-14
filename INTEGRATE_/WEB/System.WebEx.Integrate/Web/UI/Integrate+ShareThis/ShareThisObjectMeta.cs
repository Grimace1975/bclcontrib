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
namespace System.Web.UI.Integrate
{
    /// <summary>
    /// ShareThisObjectMeta
    /// </summary>
    public class ShareThisObjectMeta
    {
        public static readonly ShareThisObjectMeta Default = new ShareThisObjectMeta();

        public ShareThisObjectMeta()
        {
            Embeds = false;
            Popup = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ShareThis"/> allows embedded elements to be seen while iFrame is loading.
        /// </summary>
        /// <value><c>true</c> if embedded elements can be seen while iFrame is loading; otherwise, <c>false</c>.</value>
        public bool Embeds { get; set; }

        /// <summary>
        /// Gets or sets the offset left.
        /// </summary>
        /// <value>The offset left.</value>
        public int OffsetLeft { get; set; }

        /// <summary>
        /// Gets or sets the offset top.
        /// </summary>
        /// <value>The offset top.</value>
        public int OffsetTop { get; set; }

        /// <summary>
        /// Gets or sets the on client click.
        /// </summary>
        /// <value>The on client click.</value>
        public string OnClientClick { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ShareThis"/> widget launches in a new window rather than an iFrame.
        /// </summary>
        /// <value><c>true</c> if widget launches in a new window rather than an iFrame; otherwise, <c>false</c>.</value>
        public bool Popup { get; set; }
    }
}