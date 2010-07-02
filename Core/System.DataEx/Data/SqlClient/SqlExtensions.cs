#if SqlServer
using System.IO;
using System.Xml;
namespace System.Data.SqlClient
{
    /// <summary>
    /// SqlExtensions class
    /// </summary>
    public static class SqlExtensions
    {
        /// <summary>
        /// Reads the type of the SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        public static T ReadSqlType<T>(this BinaryReader r)
        {
            return SqlParseEx.SqlTypeT<T>.BinaryRead(r);
        }

        /// <summary>
        /// Writes the type of the SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="w">The w.</param>
        /// <param name="value">The value.</param>
        public static void WriteSqlType<T>(this BinaryWriter w, T value)
        {
            SqlParseEx.SqlTypeT<T>.BinaryWrite(w, value);
        }

        /// <summary>
        /// Reads the type of the SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r">The r.</param>
        /// <returns></returns>
        public static T ReadSqlType<T>(this XmlReader r, string id)
        {
            return SqlParseEx.SqlTypeT<T>.XmlRead(r, id);
        }

        /// <summary>
        /// Writes the type of the SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="w">The w.</param>
        /// <param name="value">The value.</param>
        public static void WriteSqlType<T>(this XmlWriter w, string id, T value)
        {
            SqlParseEx.SqlTypeT<T>.XmlWrite(w, id, value);
        }
    }
}
#endif