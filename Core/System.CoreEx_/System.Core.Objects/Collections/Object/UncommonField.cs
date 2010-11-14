namespace System.Collections.Object
{
    /// <summary>
    /// UncommonField
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UncommonField<T>
    {
        private T _defaultValue;
        private int _globalIndex;
        private bool _hasBeenSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="UncommonField&lt;T&gt;"/> class.
        /// </summary>
        public UncommonField()
            : this(default(T))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UncommonField&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        public UncommonField(T defaultValue)
        {
            _defaultValue = defaultValue;
            _hasBeenSet = false;
            lock (BaseProperty.Synchronized)
            {
                _globalIndex = BaseProperty.GetUniqueGlobalIndex(null, null);
                BaseProperty.RegisteredPropertyList.Add();
            }
        }

        /// <summary>
        /// Clears the value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void ClearValue(BaseObject instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            Simple.EntryIndex entry = instance.LookupEntry(_globalIndex);
            instance.UnsetEffectiveValue(entry, null, null);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public T GetValue(BaseObject instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (_hasBeenSet == true)
            {
                Simple.EntryIndex entry = instance.LookupEntry(_globalIndex);
                if (entry.Found == true)
                {
                    object localValue = instance.EffectiveValues[entry.Index].LocalValue;
                    if (localValue != BaseProperty.UnsetValue)
                    {
                        return (T)localValue;
                    }
                }
            }
            return _defaultValue;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="value">The value.</param>
        public void SetValue(BaseObject instance, T value)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            Simple.EntryIndex entry = instance.LookupEntry(_globalIndex);
            if (object.ReferenceEquals(value, _defaultValue) == false)
            {
                instance.SetEffectiveValue(entry, null, _globalIndex, null, value, Simple.ValueSource.Base.Local);
                _hasBeenSet = true;
            }
            else
            {
                instance.UnsetEffectiveValue(entry, null, null);
            }
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