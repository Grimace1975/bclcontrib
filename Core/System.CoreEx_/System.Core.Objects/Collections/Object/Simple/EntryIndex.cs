namespace System.Collections.Object.Simple
{
    /// <summary>
    /// EntryIndex
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct EntryIndex
    {
        private uint _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryIndex"/> struct.
        /// </summary>
        /// <param name="index">The index.</param>
        public EntryIndex(uint index)
        {
            _store = index | 0x80000000;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryIndex"/> struct.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="found">if set to <c>true</c> [found].</param>
        public EntryIndex(uint index, bool found)
        {
            _store = index & 0x7fffffff;
            if (found == true)
            {
                _store |= 0x80000000;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="EntryIndex"/> is found.
        /// </summary>
        /// <value><c>true</c> if found; otherwise, <c>false</c>.</value>
        public bool Found
        {
            get { return ((_store & 0x80000000) != 0); }
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        public uint Index
        {
            get { return (_store & 0x7fffffff); }
        }
    }

 

}