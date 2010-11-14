namespace System.Collections.Object
{
    /// <summary>
    /// BaseProperty
    /// </summary>
    public class BaseProperty
    {
        private static int GlobalIndexCount;
        internal static Frugal.ItemStructList<BaseProperty> RegisteredPropertyList = new Frugal.ItemStructList<BaseProperty>(0x300);
        internal static object Synchronized = new object();
        public static readonly object UnsetValue = new NamedObject("BaseProperty.UnsetValue");
        private static System.Collections.Hashtable PropertyFromName = new System.Collections.Hashtable();
        private Flags _packedData;
        private string _name;
        private System.Type _ownerType;
        private System.Type _propertyType;

        #region Class Types
        /// <summary>
        /// Flags
        /// </summary>
        [System.Flags]
        private enum Flags
        {
            /// <summary>
            /// GlobalIndexMask
            /// </summary>
            GlobalIndexMask = 0xffff,
            /// <summary>
            /// IsDefaultValueChanged
            /// </summary>
            IsDefaultValueChanged = 0x100000,
            /// <summary>
            /// IsFreezableType
            /// </summary>
            IsFreezableType = 0x20000,
            /// <summary>
            /// IsObjectType
            /// </summary>
            IsObjectType = 0x400000,
            /// <summary>
            /// IsPotentiallyInherited
            /// </summary>
            IsPotentiallyInherited = 0x80000,
            /// <summary>
            /// IsPotentiallyUsingDefaultValueFactory
            /// </summary>
            IsPotentiallyUsingDefaultValueFactory = 0x200000,
            /// <summary>
            /// IsStringType
            /// </summary>
            IsStringType = 0x40000,
            /// <summary>
            /// IsValueType
            /// </summary>
            IsValueType = 0x10000
        }

        /// <summary>
        /// FromNameKey
        /// </summary>
        private class FromNameKey
        {
            private int _hashCode;
            private string _name;
            private System.Type _ownerType;

            /// <summary>
            /// Initializes a new instance of the <see cref="FromNameKey"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="ownerType">Type of the owner.</param>
            public FromNameKey(string name, System.Type ownerType)
            {
                _name = name;
                _ownerType = ownerType;
                _hashCode = _name.GetHashCode() ^ _ownerType.GetHashCode();
            }

            /// <summary>
            /// Equalses the specified o.
            /// </summary>
            /// <param name="o">The o.</param>
            /// <returns></returns>
            public override bool Equals(object o)
            {
                return (((o != null) && (o is FromNameKey)) && (Equals((FromNameKey)o) == true));
            }

            /// <summary>
            /// Equalses the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns></returns>
            public bool Equals(FromNameKey key)
            {
                return ((_name.Equals(key._name) == true) && (_ownerType == key._ownerType));
            }

            /// <summary>
            /// Serves as a hash function for a particular type.
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object"/>.
            /// </returns>
            public override int GetHashCode()
            {
                return _hashCode;
            }

            /// <summary>
            /// Updates the name key.
            /// </summary>
            /// <param name="ownerType">Type of the owner.</param>
            public void UpdateNameKey(System.Type ownerType)
            {
                _ownerType = ownerType;
                _hashCode = _name.GetHashCode() ^ _ownerType.GetHashCode();
            }
        }
        #endregion Class Types

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseProperty"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="ownerType">Type of the owner.</param>
        private BaseProperty(string name, System.Type propertyType, System.Type ownerType)
        {
            Flags uniqueGlobalIndex;
            _name = name;
            _propertyType = propertyType;
            _ownerType = ownerType;
            lock (Synchronized)
            {
                uniqueGlobalIndex = (Flags)GetUniqueGlobalIndex(ownerType, name);
                RegisteredPropertyList.Add(this);
            }
            if (propertyType.IsValueType == true)
            {
                uniqueGlobalIndex |= Flags.IsValueType;
            }
            if (propertyType == typeof(object))
            {
                uniqueGlobalIndex |= Flags.IsObjectType;
            }
            if (propertyType == typeof(string))
            {
                uniqueGlobalIndex |= Flags.IsStringType;
            }
            _packedData = uniqueGlobalIndex;
        }

        /// <summary>
        /// Adds the owner.
        /// </summary>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        public BaseProperty AddOwner(System.Type ownerType)
        {
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            FromNameKey key = new FromNameKey(Name, ownerType);
            lock (Synchronized)
            {
                if (PropertyFromName.Contains(key) == true)
                {
                    throw new ArgumentException(TR.Get("PropertyAlreadyRegistered", new object[] { Name, ownerType.Name }));
                }
            }
            lock (Synchronized)
            {
                PropertyFromName[key] = this;
            }
            return this;
        }

        /// <summary>
        /// Registers the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="ownerType">Type of the owner.</param>
        /// <returns></returns>
        public static BaseProperty Register(string name, System.Type propertyType, System.Type ownerType)
        {
            return new BaseProperty(name, propertyType, ownerType);
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        public System.Type PropertyType
        {
            get { return _propertyType; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the type of the owner.
        /// </summary>
        /// <value>The type of the owner.</value>
        public System.Type OwnerType
        {
            get { return _ownerType; }
        }

        /// <summary>
        /// Gets the index of the global.
        /// </summary>
        /// <value>The index of the global.</value>
        public int GlobalIndex
        {
            get { return (((int)_packedData) & 0xffff); }
        }

        /// <summary>
        /// Gets the index of the unique global.
        /// </summary>
        /// <param name="ownerType">Type of the owner.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static int GetUniqueGlobalIndex(System.Type ownerType, string name)
        {
            if (GlobalIndexCount < 0xffff)
            {
                return GlobalIndexCount++;
            }
            if (ownerType != null)
            {
                throw new InvalidOperationException(TR.Get("TooManyDependencyProperties", new object[] { ownerType.Name + "." + name }));
            }
            throw new InvalidOperationException(TR.Get("TooManyDependencyProperties", new object[] { "ConstantProperty" }));
        }
    }
}
