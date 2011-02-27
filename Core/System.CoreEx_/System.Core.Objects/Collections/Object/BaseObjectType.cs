namespace System.Collections.Object
{
    /// <summary>
    /// BaseObjectType
    /// </summary>
    public class BaseObjectType
    {
        private BaseObjectType _baseDType;
        private int _id;
        private static object _lock = new object();
        private System.Type _systemType;
        private static int DTypeCount = 0;
        private static System.Collections.Hashtable DTypeFromCLRType = new System.Collections.Hashtable();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseObjectType"/> class.
        /// </summary>
        private BaseObjectType()
        {
        }

        /// <summary>
        /// Froms the type of the system.
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <returns></returns>
        public static BaseObjectType FromSystemType(System.Type systemType)
        {
            if (systemType == null)
            {
                throw new ArgumentNullException("systemType");
            }
            if (typeof(BaseObject).IsAssignableFrom(systemType) == false)
            {
                throw new ArgumentException(TR.Get("DTypeNotSupportForSystemType", new object[] { systemType.Name }));
            }
            return FromSystemTypeInternal(systemType);
        }


        /// <summary>
        /// Froms the system type internal.
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <returns></returns>
        internal static BaseObjectType FromSystemTypeInternal(System.Type systemType)
        {
            lock (_lock)
            {
                return FromSystemTypeRecursive(systemType);
            }
        }

        /// <summary>
        /// Froms the system type recursive.
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <returns></returns>
        private static BaseObjectType FromSystemTypeRecursive(System.Type systemType)
        {
            BaseObjectType type = (BaseObjectType)DTypeFromCLRType[systemType];
            if (type == null)
            {
                type = new BaseObjectType
                {
                    _systemType = systemType
                };
                DTypeFromCLRType[systemType] = type;
                if (systemType != typeof(BaseObject))
                {
                    type._baseDType = FromSystemTypeRecursive(systemType.BaseType);
                }
                type._id = DTypeCount++;
            }
            return type;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return _id;
        }

        /// <summary>
        /// Determines whether [is instance of type] [the specified base object].
        /// </summary>
        /// <param name="baseObject">The base object.</param>
        /// <returns>
        /// 	<c>true</c> if [is instance of type] [the specified base object]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInstanceOfType(BaseObject baseObject)
        {
            if (baseObject != null)
            {
                BaseObjectType baseObjectType = baseObject.BaseObjectType;
                do
                {
                    if (baseObjectType.Id == Id)
                    {
                        return true;
                    }
                    baseObjectType = baseObjectType._baseDType;
                }
                while (baseObjectType != null);
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is subclass of] [the specified dependency object type].
        /// </summary>
        /// <param name="dependencyObjectType">Type of the dependency object.</param>
        /// <returns>
        /// 	<c>true</c> if [is subclass of] [the specified dependency object type]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSubclassOf(BaseObjectType baseObjectType)
        {
            if (baseObjectType != null)
            {
                for (BaseObjectType type = _baseDType; type != null; type = type._baseDType)
                {
                    if (type.Id == baseObjectType.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the type of the base.
        /// </summary>
        /// <value>The type of the base.</value>
        public BaseObjectType BaseType
        {
            get { return _baseDType; }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return SystemType.Name; }
        }

        /// <summary>
        /// Gets the type of the system.
        /// </summary>
        /// <value>The type of the system.</value>
        public System.Type SystemType
        {
            get { return _systemType; }
        }
    }
}