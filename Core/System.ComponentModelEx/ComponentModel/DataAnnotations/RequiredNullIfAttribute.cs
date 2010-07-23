using System.Primitives.DataTypes;
using System.Collections;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredNullIfAttribute : ValidationAttribute
    {
        private static readonly Comparer s_comparer = Comparer.Default;

        private object _nullValue;

        public RequiredNullIfAttribute(object nullValue)
            : base(() => DataAnnotationsResources.RequiredAttribute_ValidationError)
        {
            _nullValue = nullValue;
        }

        public override bool IsValid(object value)
        {
            if ((value == null) || (s_comparer.Compare(_nullValue, value) == 0))
                return false;
            string text = (value as string);
            return (text != null ? (text.Trim().Length != 0) : true);
        }
    }
}
