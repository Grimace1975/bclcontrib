#if SqlServer
using System.Data.SqlTypes;
namespace System.Data.SqlClient
{
	public static class SqlConvert
	{
        public static object ConvertFromSqlType(object value)
        {
            if ((value == null) || (value is DBNull))
                return null;
            else if (value is SqlBoolean)
                return ((SqlBoolean)value).Value;
            else if (value is SqlChars)
                return ((SqlChars)value).Value;
            else if (value is SqlDateTime)
                return ((SqlDateTime)value).Value;
            else if (value is SqlDecimal)
                return ((SqlDecimal)value).Value;
            else if (value is SqlDouble)
                return ((SqlDouble)value).Value;
            else if (value is SqlInt32)
                return ((SqlInt32)value).Value;
            else if (value is SqlMoney)
                return ((SqlMoney)value).Value;
            else if (value is SqlSingle)
                return ((SqlSingle)value).Value;
            else if (value is SqlString)
                return ((SqlString)value).Value;
            throw new InvalidOperationException("type was: " + value.GetType().ToString());
        }	
	}
}
#endif