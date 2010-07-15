using System.Primitives.DataTypes;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DataTypeExAttribute : ValidationAttribute
    {
        private DataTypeParserBase _parser;

        public DataTypeExAttribute(Type parserType)
            : this(parserType, null) { }
        public DataTypeExAttribute(Type parserType, object attrib)
        {
            if (!parserType.IsAssignableFrom(typeof(DataTypeParserBase)))
                throw new ArgumentException("parserType");
            //_parser = DataTypeBase.Get(parserType);
            Attrib = Nattrib.Parse(attrib);
        }

        public Nattrib Attrib { get; private set; }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
