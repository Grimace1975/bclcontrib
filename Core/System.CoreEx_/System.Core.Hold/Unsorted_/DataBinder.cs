using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
namespace System
{
    /// <summary>
    /// DataBinder
    /// </summary>
    public class DataBinder
    {
        private static readonly char[] s_expressionPartSeparator = new char[] { '.' };
        private static readonly char[] s_indexExprEndChars = new char[] { ']', ')' };
        private static readonly char[] s_indexExprStartChars = new char[] { '[', '(' };

        //+ copied from DataSourceHelper 
        /// <summary>
        /// Gets the resolved data source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="dataMember">The data member.</param>
        /// <returns></returns>
        public static IEnumerable GetResolvedDataSource(object dataSource, string dataMember)
        {
            if (dataSource != null)
            {
                var source = (dataSource as IListSource);
                if (source != null)
                {
                    var list = source.GetList();
                    if (!source.ContainsListCollection)
                        return list;
                    if ((list != null) && (list is ITypedList))
                    {
                        var itemProperties = ((ITypedList)list).GetItemProperties(new PropertyDescriptor[0]);
                        if ((itemProperties == null) || (itemProperties.Count == 0))
                            throw new Exception("ListSource_Without_DataMembers");
                        PropertyDescriptor descriptor = (string.IsNullOrEmpty(dataMember) ? itemProperties[0] : itemProperties.Find(dataMember, true));
                        if (descriptor != null)
                        {
                            object component = list[0];
                            object obj3 = descriptor.GetValue(component);
                            if ((obj3 != null) && (obj3 is IEnumerable))
                                return (IEnumerable)obj3;
                        }
                        throw new Exception(string.Format("ListSource_Missing_DataMemberA[{0}]", dataMember));
                    }
                }
                if (dataSource is IEnumerable)
                    return (IEnumerable)dataSource;
            }
            return null;
        }

        /// <summary>
        /// Evals the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static object Eval(object container, string expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            expression = expression.Trim();
            if (expression.Length == 0)
                throw new ArgumentNullException("expression");
            if (container == null)
                return null;
            string[] expressionParts = expression.Split(s_expressionPartSeparator);
            return Eval(container, expressionParts);
        }
        /// <summary>
        /// Evals the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="expressionParts">The expression parts.</param>
        /// <returns></returns>
        private static object Eval(object container, string[] expressionParts)
        {
            object propertyValue = container;
            for (int i = 0; (i < expressionParts.Length) && (propertyValue != null); i++)
            {
                string propName = expressionParts[i];
                propertyValue = (propName.IndexOfAny(s_indexExprStartChars) < 0 ? GetPropertyValue(propertyValue, propName) : GetIndexedPropertyValue(propertyValue, propName));
            }
            return propertyValue;
        }
        /// <summary>
        /// Evals the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string Eval(object container, string expression, string format)
        {
            object obj2 = Eval(container, expression);
            if ((obj2 == null) || (obj2 == DBNull.Value))
                return string.Empty;
            return (string.IsNullOrEmpty(format) ? obj2.ToString() : string.Format(format, obj2));
        }

        //public static object GetDataItem(object container)
        //{
        //    bool flag;
        //    return GetDataItem(container, out flag);
        //}
        //public static object GetDataItem(object container, out bool foundDataItem)
        //{
        //    if (container == null)
        //    {
        //        foundDataItem = false;
        //        return null;
        //    }
        //    var container2 = (container as IDataItemContainer);
        //    if (container2 != null)
        //    {
        //        foundDataItem = true;
        //        return container2.DataItem;
        //    }
        //    string name = "DataItem";
        //    var property = container.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        //    if (property == null)
        //    {
        //        foundDataItem = false;
        //        return null;
        //    }
        //    foundDataItem = true;
        //    return property.GetValue(container, null);
        //}

        /// <summary>
        /// Gets the indexed property value.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="expr">The expr.</param>
        /// <returns></returns>
        public static object GetIndexedPropertyValue(object container, string expr)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(expr))
                throw new ArgumentNullException("expr");
            object obj2 = null;
            bool flag = false;
            int length = expr.IndexOfAny(s_indexExprStartChars);
            int num2 = expr.IndexOfAny(s_indexExprEndChars, length + 1);
            if (((length < 0) || (num2 < 0)) || (num2 == (length + 1)))
                throw new ArgumentException(string.Format("DataBinder_Invalid_Indexed_ExprA[{0}]", expr));
            string propName = null;
            object obj3 = null;
            string s = expr.Substring(length + 1, (num2 - length) - 1).Trim();
            if (length != 0)
                propName = expr.Substring(0, length);
            if (s.Length != 0)
            {
                if (((s[0] == '"') && (s[s.Length - 1] == '"')) || ((s[0] == '\'') && (s[s.Length - 1] == '\'')))
                    obj3 = s.Substring(1, s.Length - 2);
                else if (char.IsDigit(s[0]))
                {
                    int num3;
                    flag = int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3);
                    if (flag)
                        obj3 = num3;
                    else
                        obj3 = s;
                }
                else
                    obj3 = s;
            }
            if (obj3 == null)
                throw new ArgumentException(string.Format("DataBinder_Invalid_Indexed_ExprA[{0}]", expr));
            object propertyValue = null;
            if ((propName != null) && (propName.Length != 0))
                propertyValue = GetPropertyValue(container, propName);
            else
                propertyValue = container;
            if (propertyValue == null)
                return obj2;
            Array array = (propertyValue as System.Array);
            if ((array != null) && flag)
                return array.GetValue((int)obj3);
            if ((propertyValue is IList) && flag)
                return ((IList)propertyValue)[(int)obj3];
            var info = propertyValue.GetType().GetProperty("Item", BindingFlags.Public | BindingFlags.Instance, null, null, new Type[] { obj3.GetType() }, null);
            if (info == null)
                throw new ArgumentException(string.Format("DataBinder_No_Indexed_AccessorA[{0}]", propertyValue.GetType().FullName));
            return info.GetValue(propertyValue, new object[] { obj3 });
        }
        /// <summary>
        /// Gets the indexed property value.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string GetIndexedPropertyValue(object container, string propName, string format)
        {
            object indexedPropertyValue = GetIndexedPropertyValue(container, propName);
            if ((indexedPropertyValue == null) || (indexedPropertyValue == DBNull.Value))
                return string.Empty;
            return (string.IsNullOrEmpty(format) ? indexedPropertyValue.ToString() : string.Format(format, indexedPropertyValue));
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <returns></returns>
        public static object GetPropertyValue(object container, string propName)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentNullException("propName");
            var descriptor = TypeDescriptor.GetProperties(container).Find(propName, true);
            if (descriptor == null)
                throw new Exception(string.Format("DataBinder_Prop_Not_FoundAB[{0},{1}]", container.GetType().FullName, propName));
            return descriptor.GetValue(container);
        }
        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string GetPropertyValue(object container, string propName, string format)
        {
            object propertyValue = GetPropertyValue(container, propName);
            if ((propertyValue == null) || (propertyValue == DBNull.Value))
                return string.Empty;
            return (string.IsNullOrEmpty(format) ? propertyValue.ToString() : string.Format(format, propertyValue));
        }

        /// <summary>
        /// Determines whether the specified value is null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is null; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsNull(object value)
        {
            return (!((value != null) && (!Convert.IsDBNull(value))));
        }
    }
}