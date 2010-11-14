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
using System.Globalization;
using System.Collections.Generic;
using System.Primitives.DataTypes;
namespace System
{
    public static partial class FormatterEx
    {
        #region Registration
        private static Dictionary<Type, object> s_objectFormatterProviders = null;

        public interface IObjectFormatterBuilder
        {
            IObjectFormatter<T> Build<T>();
        }

        public interface IObjectFormatter<T>
        {
            string Format(object value);
        }

        private static IObjectFormatter<T> ScanForObjectFormatter<T>(Type type)
        {
            if (s_objectFormatterProviders != null)
                foreach (var objectFormatter2 in s_objectFormatterProviders)
                    if (type.IsSubclassOf(objectFormatter2.Key))
                        return ((IObjectFormatter<T>)objectFormatter2.Value);
            Type key;
            IObjectFormatter<T> objectFormatter;
            if (FormatterEx.TryScanForObjectFormatter<T>(out key, out objectFormatter))
            {
                if (s_objectFormatterProviders == null)
                    s_objectFormatterProviders = new Dictionary<Type, object>();
                s_objectFormatterProviders.Add(key, objectFormatter);
                return objectFormatter;
            }
            return null;
        }

        private static bool TryScanForObjectFormatter<T>(out Type key, out IObjectFormatter<T> objectFormatter)
        {
            //var interfaces = type.FindInterfaces(((m, filterCriteria) => m == s_objectParserBuilderType), null);
            throw new NotImplementedException();
        }
        #endregion

        internal static class ObjectFormatterDelegateFactory<T>
        {
            private static readonly Type s_type = typeof(T);
            public static readonly Func<object, string> Format = CreateFormatter(s_type);

            static ObjectFormatterDelegateFactory() { }

            private static Func<object, string> CreateFormatter(Type type)
            {
                if (type == CoreEx.BoolType)
                    return new Func<object, string>(Formatter_Bool);
                if (type == CoreEx.NBoolType)
                    return new Func<object, string>(Formatter_NBool);
                if (type == CoreEx.DateTimeType)
                    return new Func<object, string>(Formatter_DateTime);
                if (type == CoreEx.NDateTimeType)
                    return new Func<object, string>(Formatter_NDateTime);
                if (type == CoreEx.DecimalType)
                    return new Func<object, string>(Formatter_Decimal);
                if (type == CoreEx.NDecimalType)
                    return new Func<object, string>(Formatter_NDecimal);
                if (type == CoreEx.Int32Type)
                    return new Func<object, string>(Formatter_Int32);
                if (type == CoreEx.NInt32Type)
                    return new Func<object, string>(Formatter_NInt32);
                if (type == CoreEx.StringType)
                    return new Func<object, string>(Formatter_String);
                var formatter = ScanForObjectFormatter<T>(type);
                if (formatter != null)
                    return formatter.Format;
                return new Func<object, string>(Formatter_Default);
            }

            #region Default
            private static string Formatter_Default(object value)
            {
                var valueAsT = ParserEx.Parse<T>(value);
                return (valueAsT != null ? valueAsT.ToString() : string.Empty);
            }
            #endregion

            #region Bool
            private static string Formatter_Bool(object value)
            {
                var valueAsBool = ParserEx.Parse<bool>(value);
                return (valueAsBool ? BoolDataType.YesString : BoolDataType.NoString);
            }
            private static string Formatter_NBool(object value)
            {
                var valueAsBool = ParserEx.Parse<bool?>(value);
                return (valueAsBool.HasValue ? (valueAsBool.Value ? BoolDataType.YesString : BoolDataType.NoString) : string.Empty);
            }
            #endregion

            #region DateTime
            private static string Formatter_DateTime(object value)
            {
                var valueAsDateTime = ParserEx.Parse<DateTime>(value);
                return valueAsDateTime.ToString("M/d/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            }
            private static string Formatter_NDateTime(object value)
            {
                var valueAsDateTime = ParserEx.Parse<DateTime?>(value);
                return (valueAsDateTime.HasValue ? valueAsDateTime.Value.ToString("M/d/yyyy hh:mm tt", CultureInfo.InvariantCulture) : string.Empty);
            }
            #endregion

            #region Decimal
            private static string Formatter_Decimal(object value)
            {
                var valueAsDecimal = ParserEx.Parse<decimal>(value);
                return valueAsDecimal.ToString("0.0000", CultureInfo.InvariantCulture);
            }
            private static string Formatter_NDecimal(object value)
            {
                var valueAsDecimal = ParserEx.Parse<decimal?>(value);
                return (valueAsDecimal.HasValue ? valueAsDecimal.Value.ToString("0.0000", CultureInfo.InvariantCulture) : string.Empty);
            }
            #endregion

            #region Int32
            private static string Formatter_Int32(object value)
            {
                var valueAsInt32 = ParserEx.Parse<int>(value);
                return valueAsInt32.ToString(CultureInfo.InvariantCulture);
            }
            private static string Formatter_NInt32(object value)
            {
                var valueAsInt32 = ParserEx.Parse<int?>(value);
                return (valueAsInt32.HasValue ? valueAsInt32.Value.ToString(CultureInfo.InvariantCulture) : string.Empty);
            }
            #endregion

            #region String
            private static string Formatter_String(object value)
            {
                return ParserEx.Parse<string>(value);
            }
            #endregion
        }
    }
}
