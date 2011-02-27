using System.Reflection;
//using CellDotNet.Spe;
namespace System.Interop.Intermediate
{
    /// <summary>
    /// A variable in a method. Can either be a local variable or a parameter; <see cref="MethodParameter"/>
    /// inherits from this class since they have much in common: Escaping, stack position, type, virtual register.
    /// </summary>
    class MethodVariable
    {
        public StackTypeDescription StackType { get; private set; }
        public LocalVariableInfo LocalVariableInfo { get; private set; }
        private readonly int _index;
        private Type _reflectionType;

        protected MethodVariable(StackTypeDescription stackType)
        {
            if (stackType != StackTypeDescription.None)
                throw new ArgumentException("stackType != StackTypeDescription.None");
            StackType = stackType;
        }
        /// <summary>
        /// For stack variables.
        /// </summary>
        /// <param name="variableIndex"></param>
        /// <param name="stackType"></param>
        public MethodVariable(int variableIndex, StackTypeDescription stackType)
        {
            if (variableIndex >= 1000)
                throw new ArgumentException("variableIndex", "Stack varibles indices should be >= 1000.");
            if (stackType != StackTypeDescription.None)
                throw new ArgumentException("stackType", "stackType != StackTypeDescription.None");
            _index = variableIndex;
            StackType = stackType;
        }
        /// <summary>
        /// For CIL variables.
        /// </summary>
        /// <param name="localVariableInfo"></param>
        /// <param name="stackType"></param>
        public MethodVariable(LocalVariableInfo localVariableInfo, StackTypeDescription stackType)
        {
            if (localVariableInfo == null)
                throw new ArgumentNullException("localVariableInfo");
            if (stackType != StackTypeDescription.None)
                throw new ArgumentException("stackType", "stackType != StackTypeDescription.None");
            _reflectionType = localVariableInfo.LocalType;
            LocalVariableInfo = localVariableInfo;
            _index = localVariableInfo.LocalIndex;
            StackType = stackType;
        }

        /// <summary>
        /// This is true if the variable was created during IR tree construction.
        /// </summary>
        public bool IsStackVariable
        {
            get { return LocalVariableInfo == null; }
        }

        /// <summary>
        /// Variable number.
        /// </summary>
        public virtual int Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Position of the variable on the stack, relative to the stack pointer. Measured in quadwords.
        /// </summary>
        public int StackLocation { get; set; }

        public bool? Escapes { get; set; }

        public virtual string Name
        {
            get { return (LocalVariableInfo != null ? LocalVariableInfo.ToString() : "StackVar_" + Index); }
        }

        public virtual void SetType(StackTypeDescription stackType)
        {
            if (LocalVariableInfo != null)
                throw new InvalidOperationException("Can't change variable type.");
            StackType = stackType;
            if (stackType.ComplexType != null)
                _reflectionType = stackType.ComplexType.ReflectionType;
        }

        /// <summary>
        /// This will currently (20070812) not be set for complex stack variables, so
        /// try to use <see cref="StackType"/> instead.
        /// </summary>
        public virtual Type ReflectionType
        {
            get
            {
                if (_reflectionType == null)
                {
                    if (!IsStackVariable)
                        throw new Exception("IsStackVariable");
                    throw new InvalidOperationException("Stack variable type has not yet been determined.");
                }
                return _reflectionType;
            }
            set
            {
                if (_reflectionType != null)
                    throw new InvalidOperationException("Variable already has a type.");
                _reflectionType = value;
            }
        }

        //public VirtualRegister VirtualRegister { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
