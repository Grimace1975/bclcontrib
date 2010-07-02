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
        private static Dictionary<Type, object> s_valueFormatterProviders = null;

        public interface IValueFormatterBuilder
        {
            IValueFormatter<T> Build<T>();
        }

        public interface IValueFormatter<T>
        {
            string Format(T value);
        }

        /// <summary>
        /// ValueFormatterDelegateFactory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static class ValueFormatterDelegateFactory<T>
        {
            private static readonly Type s_type = typeof(T);
            public static readonly Func<T, string> Format = CreateFormatter(s_type);

            /// <summary>
            /// Initializes the <see cref="ValueDelegateFactory&lt;T&gt;"/> class.
            /// </summary>
            static ValueFormatterDelegateFactory() { }

            /// <summary>
            /// Creates the format.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<T, string> CreateFormatter(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_String", BindingFlags.NonPublic | BindingFlags.Static));
                if (s_valueFormatterProviders != null)
                    foreach (var valueFormatter in s_valueFormatterProviders)
                        if (type.IsSubclassOf(valueFormatter.Key))
                            return ((IValueFormatter<T>)valueFormatter.Value).Format;
                //ScanForSubclassParser();
                if (type == DataTypeBase.DataTypeBaseType)
                    throw new NotSupportedException();
                return new Func<T, string>(Formatter_Default);
            }

            #region Default
            /// <summary>
            /// Values the field.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static string Formatter_Default(T value)
            {
                return (value != null ? value.ToString() : string.Empty);
            }
            #endregion

            #region Bool
            /// <summary>
            /// Formats the field_ bool.
            /// </summary>
            /// <param name="value">if set to <c>true</c> [value].</param>
            /// <returns></returns>
            private static string Formatter_Bool(bool value)
            {
                return (value ? BoolDataType.YesString : BoolDataType.NoString);
            }
            #endregion

            #region DateTime
            /// <summary>
            /// Formats the field_ date time.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_DateTime(DateTime value)
            {
                return value.ToString("M/d/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            #endregion

            #region Decimal
            /// <summary>
            /// Formats the field_ decimal.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_Decimal(decimal value)
            {
                return value.ToString("0.0000", CultureInfo.InvariantCulture);
            }
            #endregion

            #region Int32
            /// <summary>
            /// Formats the field_ int32.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_Int32(int value)
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }
            #endregion

            #region String
            /// <summary>
            /// Formats the field_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static string Formatter_String(string value)
            {
                return value;
            }
            #endregion
        }
    }
}
