//namespace System.Collections
//{
//    ///// <summary>
//    ///// ValidateValueCallback
//    ///// </summary>
//    //public delegate bool ValidateValueCallback(object value);

//    /// <summary>
//    /// BaseProperty
//    /// </summary>
//    public class BaseProperty
//    {
//        private static int GlobalIndexCount;
//        internal static Frugal.ItemStructList<BaseProperty> RegisteredPropertyList;
//        internal static object Synchronized;
//        public static readonly object UnsetValue = null;
//        private Flags _packedData;
//        //private string _name;
//        //private System.Type _ownerType;
//        //private System.Type _propertyType;
//        //private ValidateValueCallback _validateValueCallback;
 


//        #region Class Types
//        [System.Flags]
//        private enum Flags
//        {
//            GlobalIndexMask = 0xffff,
//            IsDefaultValueChanged = 0x100000,
//            IsFreezableType = 0x20000,
//            IsObjectType = 0x400000,
//            IsPotentiallyInherited = 0x80000,
//            IsPotentiallyUsingDefaultValueFactory = 0x200000,
//            IsStringType = 0x40000,
//            IsValueType = 0x10000
//        }
//        #endregion Class Types


//        //private static void ValidateDefaultValueCommon(object defaultValue, Type propertyType, string propertyName, ValidateValueCallback validateValueCallback, bool checkThreadAffinity)
//        //{
//        //    if (!IsValidType(defaultValue, propertyType))
//        //    {
//        //        throw new ArgumentException(TR.Get("DefaultValuePropertyTypeMismatch", new object[] { propertyName }));
//        //    }
//        //    if (defaultValue is Expression)
//        //    {
//        //        throw new ArgumentException(TR.Get("DefaultValueMayNotBeExpression"));
//        //    }
//        //    if (checkThreadAffinity)
//        //    {
//        //        DispatcherObject obj2 = defaultValue as DispatcherObject;
//        //        if ((obj2 != null) && (obj2.Dispatcher != null))
//        //        {
//        //            ISealable sealable = obj2 as ISealable;
//        //            if ((sealable == null) || !sealable.CanSeal)
//        //            {
//        //                throw new ArgumentException(TR.Get("DefaultValueMustBeFreeThreaded", new object[] { propertyName }));
//        //            }
//        //            Invariant.Assert(!sealable.IsSealed, "A Sealed ISealable must not have dispatcher affinity");
//        //            sealable.Seal();
//        //            Invariant.Assert(obj2.Dispatcher == null, "ISealable.Seal() failed after ISealable.CanSeal returned true");
//        //        }
//        //    }
//        //    if ((validateValueCallback != null) && !validateValueCallback(defaultValue))
//        //    {
//        //        throw new ArgumentException(TR.Get("DefaultValueInvalid", new object[] { propertyName }));
//        //    }
//        //}

//        ///// <summary>
//        ///// Gets the type of the property.
//        ///// </summary>
//        ///// <value>The type of the property.</value>
//        //public System.Type PropertyType
//        //{
//        //    get { return _propertyType; }
//        //}

//        ///// <summary>
//        ///// Gets the name.
//        ///// </summary>
//        ///// <value>The name.</value>
//        //public string Name
//        //{
//        //    get { return _name; }
//        //}

//        ///// <summary>
//        ///// Gets the type of the owner.
//        ///// </summary>
//        ///// <value>The type of the owner.</value>
//        //public System.Type OwnerType
//        //{
//        //    get {return _ownerType; }
//        //}

//        ///// <summary>
//        ///// Gets the validate value callback.
//        ///// </summary>
//        ///// <value>The validate value callback.</value>
//        //public ValidateValueCallback ValidateValueCallback
//        //{
//        //    get { return _validateValueCallback; }
//        //}
 
//        /// <summary>
//        /// Validates the factory default value.
//        /// </summary>
//        /// <param name="defaultValue">The default value.</param>
//        internal void ValidateFactoryDefaultValue(object defaultValue)
//        {
//            //ValidateDefaultValueCommon(defaultValue, PropertyType, Name, ValidateValueCallback, false);
//        }

//        /// <summary>
//        /// Gets the index of the global.
//        /// </summary>
//        /// <value>The index of the global.</value>
//        public int GlobalIndex
//        {
//            get { return (((int)_packedData) & 0xffff); }
//        }

//        /// <summary>
//        /// Gets the index of the unique global.
//        /// </summary>
//        /// <param name="ownerType">Type of the owner.</param>
//        /// <param name="name">The name.</param>
//        /// <returns></returns>
//        internal static int GetUniqueGlobalIndex(System.Type ownerType, string name)
//        {
//            if (GlobalIndexCount < 0xffff)
//            {
//                return GlobalIndexCount++;
//            }
//            if (ownerType != null)
//            {
//                throw new InvalidOperationException(TR.Get("TooManyDependencyProperties", new object[] { ownerType.Name + "." + name }));
//            }
//            throw new InvalidOperationException(TR.Get("TooManyDependencyProperties", new object[] { "ConstantProperty" }));
//        }
//    }
//}
