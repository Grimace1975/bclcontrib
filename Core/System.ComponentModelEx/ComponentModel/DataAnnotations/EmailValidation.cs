using System.Primitives.DataTypes;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DataTypeValidation : ValidationAttribute
    {
        public DataTypeValidation(DataTypeParserBase parser)
        {
        }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
