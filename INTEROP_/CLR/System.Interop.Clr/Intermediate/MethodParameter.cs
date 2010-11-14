using System.Reflection;
namespace System.Interop.Intermediate
{
    /// <summary>
    /// Represents a parameter to a method in <see cref="MethodCompiler"/>.
    /// </summary>
    class MethodParameter : MethodVariable
    {
        private bool _isInstanceMethod;
        private ParameterInfo _parameterInfo;

        /// <summary>
        /// Instantiate a <code>MethodParameter</code> representing a this parameter to a instance method.
        /// </summary>
        /// <param name="stackType"></param>
        public MethodParameter(StackTypeDescription stackType)
            : base(stackType)
        {
            _isInstanceMethod = true;
        }
        public MethodParameter(ParameterInfo parameterInfo, StackTypeDescription stackType)
            : base(stackType)
        {
            if (parameterInfo == null)
                throw new ArgumentNullException("parameterInfo");
            _parameterInfo = parameterInfo;
        }

        public override string Name
        {
            get { return _parameterInfo != null ? _parameterInfo.Name : "this"; }
        }

        public override int Index
        {
            get { return (_parameterInfo == null ? 0 : (_isInstanceMethod ? _parameterInfo.Position + 1 : _parameterInfo.Position)); }
        }

        public override void SetType(StackTypeDescription stackType)
        {
            throw new InvalidOperationException("Can't change parameter type.");
        }

        public override Type ReflectionType
        {
            get { return (_parameterInfo != null ? _parameterInfo.ParameterType : StackType.ComplexType.ReflectionType); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
