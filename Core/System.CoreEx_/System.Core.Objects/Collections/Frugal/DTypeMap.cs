namespace System.Collections.Frugal
{
    /// <summary>
    /// DTypeMap
    /// </summary>
    public class DTypeMap
    {
        private ItemStructList<Object.BaseObjectType> _activeDTypes;
        private object[] _entries;
        private int _entryCount;
        private System.Collections.Hashtable m_overFlow;

        /// <summary>
        /// Initializes a new instance of the <see cref="DTypeMap"/> class.
        /// </summary>
        /// <param name="entryCount">The entry count.</param>
        public DTypeMap(int entryCount)
        {
            _entryCount = entryCount;
            _entries = new object[_entryCount];
            _activeDTypes = new ItemStructList<Object.BaseObjectType>(0x80);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _entryCount; i++)
            {
                _entries[i] = null;
            }
            for (int j = 0; j < _activeDTypes.Count; j++)
            {
                _activeDTypes.List[j] = null;
            }
            if (m_overFlow != null)
            {
                m_overFlow.Clear();
            }
        }

        /// <summary>
        /// Gets the active D types.
        /// </summary>
        /// <value>The active D types.</value>
        public ItemStructList<Object.BaseObjectType> ActiveDTypes
        {
            get { return _activeDTypes; }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified d type.
        /// </summary>
        /// <value></value>
        public object this[Object.BaseObjectType dType]
        {
            get
            {
                if (dType.Id < _entryCount)
                {
                    return _entries[dType.Id];
                }
                if (m_overFlow != null)
                {
                    return m_overFlow[dType];
                }
                return null;
            }
            set
            {
                if (dType.Id < _entryCount)
                {
                    _entries[dType.Id] = value;
                }
                else
                {
                    if (m_overFlow == null)
                    {
                        m_overFlow = new System.Collections.Hashtable();
                    }
                    m_overFlow[dType] = value;
                }
                _activeDTypes.Add(dType);
            }
        }
    }
}