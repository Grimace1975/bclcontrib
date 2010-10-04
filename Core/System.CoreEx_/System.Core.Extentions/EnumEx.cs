#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace System
{
    /// <summary>
    /// EnumEx
    /// </summary>
    public struct EnumEx
    {
        private static readonly Type s_enumNameAttributeType = typeof(EnumNameAttribute);

        public static bool TryParse<T>(string s, out T result)
        {
            try
            {
                result = (T)Enum.Parse(typeof(T), s);
                return true;
            }
            catch { result = default(T); return false; }
        }

        /// <summary>
        /// Toes the name.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToString(Type enumType, object value)
        {
            return InternalGetValueAsString(enumType, value, 0);
        }
        /// <summary>
        /// Toes the name.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToString<TEnum>(TEnum value)
            where TEnum : struct
        {
            return InternalGetValueAsString(typeof(TEnum), value, 0);
        }

        /// <summary>
        /// Toes the name.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToName(Type enumType, object value)
        {
            return InternalGetValueAsString(enumType, value, 1);
        }
        /// <summary>
        /// Toes the name.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToName<TEnum>(TEnum value)
            where TEnum : struct
        {
            return InternalGetValueAsString(typeof(TEnum), value, 1);
        }

        /// <summary>
        /// Internals the get value as string.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static string InternalGetValueAsString(Type enumType, object value, int type)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");
            if (value == null)
                throw new ArgumentNullException("value");
            if (type == 0)
                return value.ToString();
            EnumNameAttribute[] enumNameAttributes;
            var field = enumType.GetField(value.ToString());
            return (((field != null) && ((enumNameAttributes = (EnumNameAttribute[])field.GetCustomAttributes(s_enumNameAttributeType, true)).Length > 0)) ? enumNameAttributes[0].Name : value.ToString());
        }
    }
}

#region RAW
//using System.Reflection;
//using System.Globalization;
//using System.Runtime.InteropServices;
//using System.Runtime.CompilerServices;
//using System.Collections;
//namespace System
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public partial struct EnumEx
//    {
//        /// <summary>
//        /// Internal
//        /// </summary>
//        internal static class Internal2
//        {
//            [MethodImpl(MethodImplOptions.InternalCall)]
//            private static extern Type InternalGetUnderlyingType(Type enumType);
//            [MethodImpl(MethodImplOptions.InternalCall)]
//            private static extern void InternalGetEnumValues(Type enumType, ref ulong[] values, ref string[] names);
//            //+
//            private static Hashtable s_fieldInfoHash = Hashtable.Synchronized(new Hashtable());
//            private static readonly Type s_intType = typeof(int);

//            #region Class Types
//            /// <summary>
//            /// HashEntry
//            /// </summary>
//            private class HashEntry
//            {
//                public string[] Names;
//                public ulong[] Values;
//                public string[] DisplayNames;

//                /// <summary>
//                /// Initializes a new instance of the <see cref="HashEntry"/> class.
//                /// </summary>
//                /// <param name="names">The names.</param>
//                /// <param name="values">The values.</param>
//                /// <param name="displayNames">The display names.</param>
//                public HashEntry(string[] names, ulong[] values, string[] displayNames)
//                {
//                    Names = names;
//                    Values = values;
//                    DisplayNames = displayNames;
//                }
//            }
//            #endregion

//            /// <summary>
//            /// Internals the get value as string.
//            /// </summary>
//            /// <param name="enumType">Type of the enum.</param>
//            /// <param name="value">The value.</param>
//            /// <returns></returns>
//            public static string InternalGetValueAsString(Type enumType, object value, int type)
//            {
//                HashEntry hashEntry = GetHashEntry(enumType);
//                Type underlyingType = InternalGetUnderlyingType(enumType);
//                if ((((underlyingType == s_intType) || (underlyingType == typeof(short))) || ((underlyingType == typeof(long)) || (underlyingType == typeof(ushort)))) || (((underlyingType == typeof(byte)) || (underlyingType == typeof(sbyte))) || ((underlyingType == typeof(uint)) || (underlyingType == typeof(ulong)))))
//                {
//                    ulong num = ToUInt64(value);
//                    int index = BinarySearch(hashEntry.Values, num);
//                    if (index >= 0)
//                    {
//                        return (type == 1 ? hashEntry.DisplayNames[index] : hashEntry.Names[index]);
//                    }
//                }
//                return null;
//            }

//            /// <summary>
//            /// Binaries the search.
//            /// </summary>
//            /// <param name="array">The array.</param>
//            /// <param name="value">The value.</param>
//            /// <returns></returns>
//            private static int BinarySearch(ulong[] array, ulong value)
//            {
//                int num = 0;
//                int num2 = array.Length - 1;
//                while (num <= num2)
//                {
//                    int index = (num + num2) >> 1;
//                    ulong num4 = array[index];
//                    if (value == num4)
//                    {
//                        return index;
//                    }
//                    if (num4 < value)
//                    {
//                        num = index + 1;
//                    }
//                    else
//                    {
//                        num2 = index - 1;
//                    }
//                }
//                return ~num;
//            }

//            /// <summary>
//            /// Gets the hash entry.
//            /// </summary>
//            /// <param name="enumType">Type of the enum.</param>
//            /// <returns></returns>
//            private static HashEntry GetHashEntry(Type enumType)
//            {
//                HashEntry entry = (HashEntry)s_fieldInfoHash[enumType];
//                if (entry == null)
//                {
//                    if (s_fieldInfoHash.Count > 100)
//                    {
//                        s_fieldInfoHash.Clear();
//                    }
//                    ulong[] values = null;
//                    string[] names = null;
//                    if (enumType.BaseType == typeof(System.Enum))
//                    {
//                        InternalGetEnumValues(enumType, ref values, ref names);
//                    }
//                    else
//                    {
//                        FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
//                        values = new ulong[fields.Length];
//                        names = new string[fields.Length];
//                        for (int i = 0; i < fields.Length; i++)
//                        {
//                            names[i] = fields[i].Name;
//                            values[i] = ToUInt64(fields[i].GetValue(null));
//                        }
//                        for (int j = 1; j < values.Length; j++)
//                        {
//                            int index = j;
//                            string str = names[j];
//                            ulong num4 = values[j];
//                            bool flag = false;
//                            while (values[index - 1] > num4)
//                            {
//                                names[index] = names[index - 1];
//                                values[index] = values[index - 1];
//                                index--;
//                                flag = true;
//                                if (index == 0)
//                                {
//                                    break;
//                                }
//                            }
//                            if (flag)
//                            {
//                                names[index] = str;
//                                values[index] = num4;
//                            }
//                        }
//                    }
//                    //+
//                    string[] displayNames = null;
//                    s_fieldInfoHash[enumType] = entry = new HashEntry(names, values, displayNames);
//                }
//                return entry;
//            }

//            /// <summary>
//            /// Toes the U int64.
//            /// </summary>
//            /// <param name="value">The value.</param>
//            /// <returns></returns>
//            private static ulong ToUInt64(object value)
//            {
//                switch (Convert.GetTypeCode(value))
//                {
//                    case TypeCode.SByte:
//                    case TypeCode.Int16:
//                    case TypeCode.Int32:
//                    case TypeCode.Int64:
//                        return (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
//                    case TypeCode.Byte:
//                    case TypeCode.UInt16:
//                    case TypeCode.UInt32:
//                    case TypeCode.UInt64:
//                        return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
//                }
//                throw new InvalidOperationException("InvalidOperation_UnknownEnumType");
//            }
//        }
//    }
//}
#endregion