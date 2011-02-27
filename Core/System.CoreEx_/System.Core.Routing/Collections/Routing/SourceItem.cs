namespace System.Collections.Routing
{
    /// <summary>
    /// SourceItem
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct SourceItem
    {
        private int _startIndex;
        private object _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceItem"/> struct.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="source">The source.</param>
        internal SourceItem(int startIndex, object source)
        {
            _startIndex = startIndex;
            _source = source;
        }

        /// <summary>
        /// Gets the start index.
        /// </summary>
        /// <value>The start index.</value>
        internal int StartIndex
        {
            get { return _startIndex; }
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        internal object Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Equalses the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            return Equals((SourceItem)o);
        }

        /// <summary>
        /// Equalses the specified source item.
        /// </summary>
        /// <param name="sourceItem">The source item.</param>
        /// <returns></returns>
        public bool Equals(SourceItem sourceItem)
        {
            return ((sourceItem._startIndex == _startIndex) && (sourceItem._source == _source));
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="sourceItem1">The source item1.</param>
        /// <param name="sourceItem2">The source item2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SourceItem sourceItem1, SourceItem sourceItem2)
        {
            return sourceItem1.Equals(sourceItem2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="sourceItem1">The source item1.</param>
        /// <param name="sourceItem2">The source item2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SourceItem sourceItem1, SourceItem sourceItem2)
        {
            return !sourceItem1.Equals(sourceItem2);
        }
    }
}