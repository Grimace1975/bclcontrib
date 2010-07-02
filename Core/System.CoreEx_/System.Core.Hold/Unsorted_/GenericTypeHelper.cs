using System.Collections.Generic;
namespace System.Utility
{
    /// <summary>
    /// GenericTypeHelper
    /// </summary>
    public static class GenericTypeHelper
    {
        /// <summary> 
        /// Gets the type of the elements in a <see cref="IEnumerable{TElement}"/> derived type. 
        /// </summary> 
        /// <param name="seqType">Type of the seq.</param> 
        /// <returns></returns> 
        public static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            return (ienum == null ? seqType : ienum.GetGenericArguments()[0]);
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if ((seqType == null) || (seqType == CoreEx.StringType))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                        return ienum;
                }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces.Length > 0)
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null)
                        return ienum;
                }
            return ((seqType.BaseType != null) && (seqType.BaseType != CoreEx.ObjectType) ? FindIEnumerable(seqType.BaseType) : null);
        }
    }
}
