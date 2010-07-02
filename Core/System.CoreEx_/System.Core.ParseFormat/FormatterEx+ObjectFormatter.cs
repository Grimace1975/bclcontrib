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
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Primitives;
using System.Primitives.DataTypes;
namespace System
{
    public static partial class FormatterEx
    {
        private static Dictionary<Type, object> s_objectFormatterProviders = null;

        public interface IObjectFormatterBuilder
        {
            IObjectFormatter<T> Build<T>();
        }

        public interface IObjectFormatter<T>
        {
            string Format(object value);
        }

        /// <summary>
        /// ObjectFormatterDelegateFactory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static class ObjectFormatterDelegateFactory<T>
        {
            private static readonly Type s_type = typeof(T);
            public static readonly Func<object, string> Format = CreateFormatter(s_type);

            /// <summary>
            /// Initializes the <see cref="ParseObjectT&lt;T&gt;"/> class.
            /// </summary>
            static ObjectFormatterDelegateFactory() { }

            /// <summary>
            /// Creates the format.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<object, string> CreateFormatter(Type type)
            {
                if (type == CoreEx.BoolType)
                    return new Func<object, string>(Formatter_Bool);
                if (type == CoreEx.DateTimeType)
                    return new Func<object, string>(Formatter_DateTime);
                if (type == CoreEx.DecimalType)
                    return new Func<object, string>(Formatter_Decimal);
                if (type == CoreEx.Int32Type)
                    return new Func<object, string>(Formatter_Int32);
                if (type == CoreEx.StringType)
                    return new Func<object, string>(Formatter_String);
                if (s_objectFormatterProviders != null)
                    foreach (var objectFormatter in s_objectFormatterProviders)
                        if (type.IsSubclassOf(objectFormatter.Key))
                            return ((IObjectFormatter<T>)objectFormatter.Value).Format;
                //ScanForSubclassParser();
                return new Func<object, string>(Formatter_Default);
            }

            //#region Nullable
            //private static TElem? NullableField<TElem>(object value, T defaultValue)
            //    where TElem : struct
            //{
            //    throw new NotSupportedException();
            //}
            //private static TElem? NullableField2<TElem>(object value, object defaultValue)
            //    where TElem : struct
            //{
            //    throw new NotSupportedException();
            //}
            //#endregion

            #region Default
            /// <summary>
            /// Values the field.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static string Formatter_Default(object value)
            {
                T valueAsT = ParserEx.Parse<T>(value);
                return (valueAsT != null ? valueAsT.ToString() : string.Empty);
            }
            #endregion

            #region Bool
            /// <summary>
            /// Formats the field_ bool.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_Bool(object value)
            {
                bool valueAsBool = ParserEx.Parse<bool>(value);
                return (valueAsBool ? BoolDataType.YesString : BoolDataType.NoString);
            }
            #endregion

            #region DateTime
            /// <summary>
            /// Formats the field_ date time.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_DateTime(object value)
            {
                DateTime valueAsDateTime = ParserEx.Parse<DateTime>(value);
                return valueAsDateTime.ToString("M/d/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            #endregion

            #region Decimal
            /// <summary>
            /// Formats the field_ decimal.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_Decimal(object value)
            {
                decimal valueAsDecimal = ParserEx.Parse<decimal>(value);
                return valueAsDecimal.ToString("0.0000", CultureInfo.InvariantCulture);
            }
            #endregion

            #region Int32
            /// <summary>
            /// Formats the field_ int32.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_Int32(object value)
            {
                int valueAsInt32 = ParserEx.Parse<int>(value);
                return valueAsInt32.ToString(CultureInfo.InvariantCulture);
            }
            #endregion

            #region String
            /// <summary>
            /// Formats the field_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_String(object value)
            {
                return ParserEx.Parse<string>(value);
            }
            #endregion
        }
    }
}
