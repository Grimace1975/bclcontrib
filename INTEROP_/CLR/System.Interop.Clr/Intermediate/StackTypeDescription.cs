using System.Diagnostics;
namespace System.Interop.Intermediate
{
    /// <summary>
    /// Convenient description of a value on the stack; it can express managed pointers etc.
    /// <para>TODO: Express non-basic types.</para>
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal struct StackTypeDescription : IEquatable<StackTypeDescription>
    {
        public static readonly StackTypeDescription None = new StackTypeDescription();
        public static readonly StackTypeDescription Int32 = new StackTypeDescription(CliType.Int32, CliNumericSize.FourBytes, true);
        public static readonly StackTypeDescription Int64 = new StackTypeDescription(CliType.Int64, CliNumericSize.EightBytes, true);
        public static readonly StackTypeDescription Float32 = new StackTypeDescription(CliType.Float32, CliNumericSize.FourBytes, true);
        public static readonly StackTypeDescription Float64 = new StackTypeDescription(CliType.Float64, CliNumericSize.EightBytes, true);
        public static readonly StackTypeDescription VectorI4 = new StackTypeDescription(CliType.VectorI4, CliNumericSize.SixteenBytes, true);
        public static readonly StackTypeDescription VectorF4 = new StackTypeDescription(CliType.VectorF4, CliNumericSize.SixteenBytes, true);
        public static readonly StackTypeDescription VectorD2 = new StackTypeDescription(CliType.VectorD2, CliNumericSize.SixteenBytes, true);
        public static readonly StackTypeDescription ObjectType = new StackTypeDescription(CliType.ObjectType, CliNumericSize.None, false);
        public static readonly StackTypeDescription NativeInt = new StackTypeDescription(CliType.NativeInt, CliNumericSize.None, true);
        private CliType _cliType;
        private readonly bool _isSigned;
        private readonly CliNumericSize _numericSize;
        private byte _indirectionLevel;
        private bool _isManaged;
        private bool _isArray;
        private TypeDescription _complexType;

        /// <summary>
        /// For simple types.
        /// </summary>
        /// <param name="cliType">Type of the cli.</param>
        /// <param name="numericSize">Size of the numeric.</param>
        /// <param name="isSigned">if set to <c>true</c> [is signed].</param>
        public StackTypeDescription(CliType cliType, CliNumericSize numericSize, bool isSigned)
        {
            _cliType = cliType;
            _isSigned = isSigned;
            _numericSize = numericSize;
            _indirectionLevel = 0;
            _isManaged = false;
            _complexType = null;
            _isArray = false;
        }

        public static StackTypeDescription FromCliType(CliType clitype)
        {
            switch (clitype)
            {
                case CliType.Int32:
                    return Int32;
                case CliType.Int64:
                    return Int64;
                case CliType.NativeInt:
                    return NativeInt;
                case CliType.Float32:
                    return Float32;
                case CliType.Float64:
                    return Float64;
                case CliType.ValueType:
                case CliType.ObjectType:
                case CliType.ManagedPointer:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// For complex types.
        /// </summary>
        /// <param name="complexType"></param>
        public StackTypeDescription(TypeDescription complexType)
        {
            if (complexType.ReflectionType.IsValueType)
            {
                _cliType = CliType.ValueType;
                _indirectionLevel = 0;
            }
            else
            {
                _cliType = CliType.ObjectType;
                _indirectionLevel = 1;
            }
            _isSigned = false;
            _numericSize = CliNumericSize.None;
            _isManaged = false;
            _complexType = complexType;
            _isArray = complexType.ReflectionType.IsArray;
            if (_isArray)
                _indirectionLevel++;
            //Utilities.PretendVariableIsUsed(DebuggerDisplay);
        }

        public TypeDescription ComplexType
        {
            get { return _complexType; }
        }

        public bool IsSigned
        {
            get
            {
                AssertSimple();
                return _isSigned;
            }
        }

        /// <summary>
        /// When this returns true, we can perform some operations in registers. The CLI basic types are not 
        /// covered by this property.
        /// </summary>
        public bool IsImmutableSingleRegisterType
        {
            get { return (this == VectorI4) || (this == VectorF4) || ((CliType == CliType.ValueType) && (ComplexType.IsImmutableSingleRegisterStruct)); }
        }

        /// <summary>
        /// A shorcut for CliType == CliType.ValueType &amp;&amp; !<see cref="IsImmutableSingleRegisterType"/>.
        /// Custom structs always go on the stack.
        /// </summary>
        public bool IsStackValueType
        {
            get { return ((CliType == CliType.ValueType) && (!IsImmutableSingleRegisterType)); }
        }

        internal CliType CliType
        {
            get
            {
                if (IsPointerType)
                {
                    if (_isManaged)
                        return CliType.ManagedPointer;
                    else
                    {
                        if (IndirectionLevel != 1)
                            throw new Exception("IndirectionLevel == 1 for object type.");
                        return CliType.ObjectType;
                    }
                }
                return _cliType;
            }
        }

        private string DebuggerDisplay
        {
            get { return ToString(); }
        }

        /// <summary>
        /// Size of a numeric value in bytes.
        /// </summary>
        public CliNumericSize NumericSize
        {
            get
            {
                if (_isArray)
                    return CliNumericSize.FourBytes;
                AssertSimple();
                return _numericSize;
            }
        }

        /// <summary>
        /// Indicates whether this is an 1D array.
        /// </summary>
        public bool IsArray
        {
            get { return (!_isManaged) && (_isArray); }
        }

        /// <summary>
        /// Both managed and unmanaged pointers.
        /// <para>
        /// Value types have a value of zero, although they currently (20070929) are almost always located on the stack.
        /// </para>
        /// <para>
        /// Reference types have a value of one.
        /// </para>
        /// </summary>
        public int IndirectionLevel
        {
            get { return _indirectionLevel; }
        }

        public bool IsPointerType
        {
            get { return IndirectionLevel != 0; }
        }

        public StackTypeDescription GetManagedPointer()
        {
            if (_isManaged)
                throw new InvalidOperationException();
            var rv = this;
            rv._isManaged = true;
            rv._indirectionLevel++;
            return rv;
        }

        /// <summary>
        /// Asserts that this is a simple type; that is, that this is a value type and 
        /// not a pointer or an array.
        /// </summary>
        private void AssertSimple()
        {
            if ((IsArray) || (IsPointerType))
                throw new InvalidOperationException("Invalid operation since this is not a simple type (" + this + ").");
        }

        /// <summary>
        /// Returns a type that repre
        /// </summary>
        /// <returns></returns>
        public StackTypeDescription GetArrayType()
        {
            if ((_isArray) || (_cliType == CliType.ManagedPointer))
                throw new NotSupportedException("This array type not suported.");
            var arr = this;
            arr._isArray = true;
            arr._indirectionLevel++;
            return arr;
        }

        public StackTypeDescription GetArrayElementType()
        {
            if ((!_isArray) || (_isManaged))
                throw new InvalidOperationException("This type is not an array type.");
            var std = this;
            std._isArray = false;
            std._indirectionLevel--;
            return std;
        }

        /// <summary>
        /// Return the <see cref="Type"/> representation of the <paramref name="clitype"/> argument.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// If the cli type is not a concrete type, ie. ValueType, ObjectType ManagedPointer.</exception>
        /// <param name="clitype"></param>
        /// <returns></returns>
        public static Type GetReflectionType(CliType clitype)
        {
            switch (clitype)
            {
                case CliType.Int32:
                    return typeof(int);
                case CliType.Int64:
                    return typeof(long);
                case CliType.NativeInt:
                    return typeof(IntPtr);
                case CliType.Float32:
                    return typeof(float);
                case CliType.Float64:
                    return typeof(double);
                case CliType.VectorI4:
                    return typeof(VectorI4);
                case CliType.VectorF4:
                    return typeof(VectorF4);
                default:
                    throw new ArgumentException();
            }
        }

        public int GetSizeWithPadding()
        {
            switch (CliType)
            {
                case CliType.Int32:
                case CliType.Float32:
                    return 4;
                case CliType.VectorI4:
                case CliType.VectorF4:
                    return 16;
                case CliType.Int64:
                case CliType.Float64:
                    return 8;
                case CliType.None:
                case CliType.NativeInt:
                case CliType.ValueType:
                case CliType.ObjectType:
                case CliType.ManagedPointer:
                default:
                    throw new InvalidOperationException();
            }
        }

        public StackTypeDescription Dereference()
        {
            if ((!IsPointerType) || (_isArray))
                throw new InvalidOperationException("ReflectionType is not byref.");
            var e = this;
            e._indirectionLevel--;
            if (!((_indirectionLevel >= 0) && ((_indirectionLevel >= 1) || (_cliType != CliType.ObjectType))))
                throw new Exception("Low indirection level.");
            e._isManaged = false;
            return e;
        }

        public CliType DereferencedCliType
        {
            get { return _cliType; }
        }

        private StackTypeDescription DereferenceFully()
        {
            if ((!IsPointerType) || (_isArray))
                throw new InvalidOperationException("ReflectionType is not byref.");
            var e = this;
            e._indirectionLevel = 0;
            e._isManaged = false;
            return e;
        }

        public override string ToString()
        {
            string s;
            int lvl = IndirectionLevel;
            if (IsArray)
                s = GetArrayElementType().CliType.ToString();
            else if (lvl > 0)
                s = DereferenceFully().CliType.ToString();
            else
                s = CliType.ToString();
            if (lvl > 0)
            {
                // I guess this is not entirely correct if you got an unmanaged pointer to a managed pointer...
                if (_isManaged)
                {
                    if (CliType == CliType.ObjectType)
                        lvl--;
                    s += new string('&', lvl);
                }
                else
                    s += new string('*', lvl - 1);
            }
            if (IsArray)
                s += "[]";
            return s;
        }

        #region Equality Stuff

        public static bool operator ==(StackTypeDescription x, StackTypeDescription y)
        {
            return ((x._cliType == y._cliType) &&
                   (x._complexType == y._complexType) &&
                   (x._indirectionLevel == y._indirectionLevel) &&
                   (x._isManaged == y._isManaged) &&
                   (x._isSigned == y._isSigned) &&
                   (x._isArray == y._isArray) &&
                   (x._numericSize == y._numericSize));
        }

        public static bool operator !=(StackTypeDescription x, StackTypeDescription y)
        {
            return (!(x == y));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StackTypeDescription))
                return false;
            var stackTypeDescription = (StackTypeDescription)obj;
            return (this == stackTypeDescription);
        }

        public override int GetHashCode()
        {
            int result = _cliType.GetHashCode();
            result = 29 * result + _isSigned.GetHashCode();
            result = 29 * result + _numericSize.GetHashCode();
            result = 29 * result + _indirectionLevel;
            result = 29 * result + _isManaged.GetHashCode();
            result = 29 * result + _isArray.GetHashCode();
            result = 29 * result + (_complexType != null ? _complexType.GetHashCode() : 0);
            return result;
        }

        public bool Equals(StackTypeDescription other)
        {
            return (this == other);
        }

        #endregion
    }
}