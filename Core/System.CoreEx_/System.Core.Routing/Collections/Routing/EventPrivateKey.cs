namespace System.Collections.Routing
{
    /// <summary>
    /// EventPrivateKey
    /// </summary>
    public class EventPrivateKey
    {
        private int _globalIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventPrivateKey"/> class.
        /// </summary>
        public EventPrivateKey()
        {
            _globalIndex = GlobalEventManager.GetNextAvailableGlobalIndex(this);
        }

        /// <summary>
        /// Gets the index of the global.
        /// </summary>
        /// <value>The index of the global.</value>
        internal int GlobalIndex
        {
            get { return _globalIndex; }
        }
    }
}