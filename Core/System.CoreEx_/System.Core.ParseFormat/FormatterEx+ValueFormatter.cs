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
using System.Primitives.DataTypes;
namespace System
{
    public static partial class FormatterEx
    {
        #region Registration
        private static Dictionary<Type, object> s_valueFormatterProviders = null;

        public interface IValueFormatterBuilder
        {
            IValueFormatter<T> Build<T>();
        }

        public interface IValueFormatter<T>
        {
            string Format(T value);
        }

        private static IValueFormatter<T> ScanForValueFormatter<T>(Type type)
        {
            if (s_valueFormatterProviders != null)
                foreach (var valueFormatter2 in s_valueFormatterProviders)
                    if (type.IsSubclassOf(valueFormatter2.Key))
                        return ((IValueFormatter<T>)valueFormatter2.Value);
            Type key;
            IValueFormatter<T> valueFormatter;
            if (FormatterEx.TryScanForValueFormatter<T>(out key, out valueFormatter))
            {
                if (s_valueFormatterProviders == null)
                    s_valueFormatterProviders = new Dictionary<Type, object>();
                s_valueFormatterProviders.Add(key, valueFormatter);
                return valueFormatter;
            }
            return null;
        }

        private static bool TryScanForValueFormatter<T>(out Type key, out IValueFormatter<T> valueFormatter)
        {
            //var interfaces = type.FindInterfaces(((m, filterCriteria) => m == s_objectParserBuilderType), null);
            throw new NotImplementedException();
        }
        #endregion

        internal static class ValueFormatterDelegateFactory<T>
        {
            private static readonly Type s_type = typeof(T);
            public static readonly Func<T, string> Format = CreateFormatter(s_type);

            static ValueFormatterDelegateFactory() { }

            private static Func<T, string> CreateFormatter(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NBoolType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_NBool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NDateTimeType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_NDateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NDecimalType)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_NDecimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NInt32Type)
                    return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), typeof(ValueFormatterDelegateFactory<T>).GetMethod("Formatter_NInt32", BindingFlags.NonPublic | BindingFlags.Static));
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
            private static string Formatter_Default(T value)
            {
                return (value != null ? value.ToString() : string.Empty);
            }
            #endregion

            #region Bool
            private static string Formatter_Bool(bool value)
            {
                return (value ? BoolDataType.YesString : BoolDataType.NoString);
            }
            private static string Formatter_NBool(bool? value)
            {
                return (value.HasValue ? (value.Value ? BoolDataType.YesString : BoolDataType.NoString) : string.Empty);
            }
            #endregion

            #region DateTime
            private static string Formatter_DateTime(DateTime value)
            {
                return value.ToString("M/d/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            private static string Formatter_NDateTime(DateTime? value)
            {
                return (value.HasValue ? value.Value.ToString("M/d/yyyy hh:mm tt", CultureInfo.InvariantCulture) : string.Empty);
            }
            #endregion

            #region Decimal
            private static string Formatter_Decimal(decimal value)
            {
                return value.ToString("0.0000", CultureInfo.InvariantCulture);
            }
            private static string Formatter_NDecimal(decimal? value)
            {
                return (value.HasValue ? value.Value.ToString("0.0000", CultureInfo.InvariantCulture) : string.Empty);
            }
            #endregion

            #region Int32
            private static string Formatter_Int32(int value)
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }
            private static string Formatter_NInt32(int? value)
            {
                return (value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty);
            }
            #endregion

            #region String
            private static string Formatter_String(string value)
            {
                return value;
            }
            #endregion
        }
    }
}
