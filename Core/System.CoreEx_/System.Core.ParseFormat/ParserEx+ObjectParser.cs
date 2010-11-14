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
namespace System
{
    public static partial class ParserEx
    {
        #region Registration
        //private static readonly Type s_objectParserBuilderType = typeof(IObjectParserBuilder);
        private static Dictionary<Type, object> s_objectParserProviders = null;

        public interface IObjectParserBuilder
        {
            IObjectParser<TResult> Build<TResult>();
        }

        public interface IObjectParser<TResult>
        {
            TResult Parser(object value, TResult defaultValue, Nattrib attrib);
            object Parser2(object value, object defaultValue, Nattrib attrib);
            bool TryParser(object value, Nattrib attrib, out TResult validValue);
            bool Validator(object value, Nattrib attrib);
        }

        private static bool TryScanForObjectParser<TResult>(out Type key, out IObjectParser<TResult> objectParser)
        {
            key = null; objectParser = null; return false;
            //var interfaces = type.FindInterfaces(((m, filterCriteria) => m == s_objectParserBuilderType), null);
            //throw new NotImplementedException();
        }

        private static IObjectParser<TResult> ScanForObjectParser<TResult>(Type type)
        {
            if (s_objectParserProviders != null)
                foreach (var objectParser2 in s_objectParserProviders)
                    if (type.IsSubclassOf(objectParser2.Key))
                        return ((IObjectParser<TResult>)objectParser2.Value);
            Type key;
            IObjectParser<TResult> objectParser;
            if (ParserEx.TryScanForObjectParser<TResult>(out key, out objectParser))
            {
                if (s_objectParserProviders == null)
                    s_objectParserProviders = new Dictionary<Type, object>();
                s_objectParserProviders.Add(key, objectParser);
                return objectParser;
            }
            return null;
        }
        #endregion

        internal static class ObjectParserDelegateFactory<T, TResult>
        {
            private static readonly Type s_type = typeof(T);
            private static readonly MethodInfo s_tryParseMethodInfo = s_type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { CoreEx.StringType, s_type.MakeByRefType() }, null);
            public static readonly Func<object, TResult, Nattrib, TResult> Parse = CreateParser(s_type);
            public static readonly Func<object, object, Nattrib, object> Parse2 = CreateParser2(s_type);
            public static readonly TryFunc<object, Nattrib, TResult> TryParse = CreateTryParser(s_type);
            public static readonly Func<object, Nattrib, bool> Validate = CreateValidator(s_type);

            static ObjectParserDelegateFactory() { }

            private static Func<object, TResult, Nattrib, TResult> CreateParser(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NBoolType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_NBool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NDateTimeType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_NDateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NDecimalType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_NDecimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NInt32Type)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_NInt32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (Func<object, TResult, Nattrib, TResult>)Delegate.CreateDelegate(typeof(Func<object, TResult, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("Parser_String", BindingFlags.NonPublic | BindingFlags.Static));
                var parser = ScanForObjectParser<TResult>(type);
                if (parser != null)
                    return parser.Parser;
                EnsureTryParseMethod();
                return new Func<object, TResult, Nattrib, TResult>(Parser_Default);
            }

            private static Func<object, object, Nattrib, object> CreateParser2(Type type)
            {
                if ((type == CoreEx.BoolType) || (type == CoreEx.NBoolType))
                    return new Func<object, object, Nattrib, object>(Parser2_Bool);
                if ((type == CoreEx.DateTimeType) || (type == CoreEx.NDateTimeType))
                    return new Func<object, object, Nattrib, object>(Parser2_DateTime);
                if ((type == CoreEx.DecimalType) || (type == CoreEx.NDecimalType))
                    return new Func<object, object, Nattrib, object>(Parser2_Decimal);
                if ((type == CoreEx.Int32Type) || (type == CoreEx.NInt32Type))
                    return new Func<object, object, Nattrib, object>(Parser2_Int32);
                if (type == CoreEx.StringType)
                    return new Func<object, object, Nattrib, object>(Parser2_String);
                var parser = ScanForObjectParser<TResult>(type);
                if (parser != null)
                    return parser.Parser2;
                EnsureTryParseMethod();
                return new Func<object, object, Nattrib, object>(Parser2_Default);
            }

            private static TryFunc<object, Nattrib, TResult> CreateTryParser(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NBoolType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_NBool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NDateTimeType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_NDateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NDecimalType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_NDecimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.NInt32Type)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_NInt32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (TryFunc<object, Nattrib, TResult>)Delegate.CreateDelegate(typeof(TryFunc<object, Nattrib, TResult>), typeof(ObjectParserDelegateFactory<T, TResult>).GetMethod("TryParser_String", BindingFlags.NonPublic | BindingFlags.Static));
                var parser = ScanForObjectParser<TResult>(type);
                if (parser != null)
                    return parser.TryParser;
                EnsureTryParseMethod();
                return new TryFunc<object, Nattrib, TResult>(TryParser_Default);
            }

            private static Func<object, Nattrib, bool> CreateValidator(Type type)
            {
                if (type == CoreEx.BoolType)
                    return new Func<object, Nattrib, bool>(Validator_Bool);
                else if (type == CoreEx.DateTimeType)
                    return new Func<object, Nattrib, bool>(Validator_DateTime);
                else if (type == CoreEx.DecimalType)
                    return new Func<object, Nattrib, bool>(Validator_Decimal);
                else if (type == CoreEx.Int32Type)
                    return new Func<object, Nattrib, bool>(Validator_Int32);
                else if (type == CoreEx.StringType)
                    return new Func<object, Nattrib, bool>(Validator_String);
                var parser = ScanForObjectParser<TResult>(type);
                if (parser != null)
                    return parser.Validator;
                return new Func<object, Nattrib, bool>(Validator_Default);
            }

            private static void EnsureTryParseMethod()
            {
                if (s_tryParseMethodInfo == null)
                    throw new InvalidOperationException(string.Format("{0}::TypeParse method required for parsing", typeof(T).ToString()));
            }

            #region Default
            private static TResult Parser_Default(object value, TResult defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is TResult)
                        return (TResult)value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        var args = new object[] { text, default(TResult) };
                        if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                            return (TResult)args[1];
                    }
                }
                return defaultValue;
            }
            private static object Parser2_Default(object value, object defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is TResult)
                        return value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        var args = new object[] { text, default(TResult) };
                        if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                            return (TResult)args[1];
                    }
                }
                return defaultValue;
            }

            private static bool TryParser_Default(object value, Nattrib attrib, out TResult validValue)
            {
                if (value != null)
                {
                    if (value is TResult)
                    {
                        validValue = (TResult)value; return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        var args = new object[] { text, default(TResult) };
                        if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                        {
                            validValue = (TResult)args[1]; return true;
                        }
                    }
                }
                validValue = default(TResult); return false;
            }

            private static bool Validator_Default(object value, Nattrib attrib)
            {
                if (value == null)
                    return false;
                if (value is TResult)
                    return true;
                string text;
                if (((text = (value as string)) != null) && (text.Length > 0))
                {
                    var args = new object[] { text, default(TResult) };
                    if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                        return true;
                }
                return false;
            }
            #endregion

            #region Bool
            private static bool Parser_Bool(object value, bool defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is bool)
                        return (bool)value;
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                                return true;
                            case 0:
                                return false;
                        }
                        return defaultValue;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "y":
                            case "true":
                            case "yes":
                            case "on":
                                return true;
                            case "0":
                            case "n":
                            case "false":
                            case "no":
                            case "off":
                                return false;
                        }
                        bool validValue;
                        if (bool.TryParse(text, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static bool? Parser_NBool(object value, bool? defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is bool) || (value is bool?))
                        return (bool?)value;
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                                return true;
                            case 0:
                                return false;
                        }
                        return defaultValue;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "y":
                            case "true":
                            case "yes":
                            case "on":
                                return true;
                            case "0":
                            case "n":
                            case "false":
                            case "no":
                            case "off":
                                return false;
                        }
                        bool validValue;
                        if (bool.TryParse(text, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static object Parser2_Bool(object value, object defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is bool) || (value is bool?))
                        return value;
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                                return true;
                            case 0:
                                return false;
                        }
                        return defaultValue;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "y":
                            case "true":
                            case "yes":
                            case "on":
                                return true;
                            case "0":
                            case "n":
                            case "false":
                            case "no":
                            case "off":
                                return false;
                        }
                        bool validValue;
                        if (bool.TryParse(text, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }

            private static bool TryParser_Bool(object value, Nattrib attrib, out bool validValue)
            {
                if (value != null)
                {
                    if (value is bool)
                    {
                        validValue = (bool)value; return true;
                    }
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                                validValue = true; return true;
                            case 0:
                                validValue = false; return true;
                        }
                        validValue = default(bool); return false;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "y":
                            case "true":
                            case "yes":
                            case "on":
                                validValue = true; return true;
                            case "0":
                            case "n":
                            case "false":
                            case "no":
                            case "off":
                                validValue = false; return true;
                        }
                        bool validValue2;
                        if (bool.TryParse(text, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = default(bool);
                return false;
            }
            private static bool TryParser_NBool(object value, Nattrib attrib, out bool? validValue)
            {
                if (value != null)
                {
                    if ((value is bool) || (value is bool?))
                    {
                        validValue = (bool?)value; return true;
                    }
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                                validValue = true; return true;
                            case 0:
                                validValue = false; return true;
                        }
                        validValue = null; return false;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "y":
                            case "true":
                            case "yes":
                            case "on":
                                validValue = true; return true;
                            case "0":
                            case "n":
                            case "false":
                            case "no":
                            case "off":
                                validValue = false; return true;
                        }
                        bool validValue2;
                        if (bool.TryParse(text, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = null; return false;
            }

            private static bool Validator_Bool(object value, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is bool)
                        return true;
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                            case 0:
                                return true;
                        }
                        return false;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "y":
                            case "true":
                            case "yes":
                            case "on":
                                return true;
                            case "0":
                            case "n":
                            case "false":
                            case "no":
                            case "off":
                                return true;
                        }
                        bool validValue;
                        return bool.TryParse(text, out validValue);
                    }
                }
                return false;
            }
            #endregion

            #region DateTime
            private static DateTime Parser_DateTime(object value, DateTime defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is DateTime)
                        return (DateTime)value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue;
                        if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static DateTime? Parser_DateTime(object value, DateTime? defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is DateTime) || (value is DateTime?))
                        return (DateTime?)value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue;
                        if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }

            private static object Parser2_DateTime(object value, object defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is DateTime) || (value is DateTime?))
                        return value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue;
                        if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }

            private static bool TryParser_DateTime(object value, Nattrib attrib, out DateTime validValue)
            {
                if (value != null)
                {
                    if (value is DateTime)
                    {
                        validValue = (DateTime)value; return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue2;
                        if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = default(DateTime); return false;
            }
            private static bool TryParser_DateTime(object value, Nattrib attrib, out DateTime? validValue)
            {
                if (value != null)
                {
                    if ((value is DateTime) || (value is DateTime?))
                    {
                        validValue = (DateTime?)value; return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue2;
                        if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = null; return false;
            }

            private static bool Validator_DateTime(object value, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is DateTime)
                        return true;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue;
                        return DateTime.TryParse(text, out validValue);
                    }
                }
                return false;
            }
            #endregion

            #region Decimal
            private static decimal Parser_Decimal(object value, decimal defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is decimal)
                        return (decimal)value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue;
                        if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static decimal? Parser_NDecimal(object value, decimal? defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is decimal) || (value is decimal?))
                        return (decimal?)value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue;
                        if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static object Parser2_Decimal(object value, object defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is decimal) || (value is decimal?))
                        return value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue;
                        if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }

            private static bool TryParser_Decimal(object value, Nattrib attrib, out decimal validValue)
            {
                if (value != null)
                {
                    if (value is decimal)
                    {
                        validValue = (decimal)value; return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue2;
                        if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = default(decimal); return false;
            }
            private static bool TryParser_Decimal(object value, Nattrib attrib, out decimal? validValue)
            {
                if (value != null)
                {
                    if ((value is decimal) || (value is decimal?))
                    {
                        validValue = (decimal?)value; return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue2;
                        if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = null; return false;
            }

            private static bool Validator_Decimal(object value, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is decimal)
                        return true;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue;
                        return decimal.TryParse(text, NumberStyles.Currency, null, out validValue);
                    }
                }
                return false;
            }
            #endregion

            #region Int32
            private static int Parser_Int32(object value, int defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is int)
                        return (int)value;
                    if (value is bool)
                        return (!(bool)value ? 0 : 1);
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue;
                        if (int.TryParse(text, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static int? Parser_NInt32(object value, int? defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    if ((value is int) || (value is int?))
                        return (int?)value;
                    if (value is bool)
                        return (!(bool)value ? 0 : 1);
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue;
                        if (int.TryParse(text, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }
            private static object Parser2_Int32(object value, object defaultValue, Nattrib attrib)
            {
                if (value == null)
                {
                    if ((value is int) || (value is int?))
                        return value;
                    if (value is bool)
                        return (!(bool)value ? 0 : 1);
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue;
                        if (int.TryParse(text, out validValue))
                            return validValue;
                    }
                }
                return defaultValue;
            }

            private static bool TryParser_Int32(object value, Nattrib attrib, out int validValue)
            {
                if (value != null)
                {
                    if (value is int)
                    {
                        validValue = (int)value; return true;
                    }
                    if (value is bool)
                    {
                        validValue = (!(bool)value ? 0 : 1); return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue2;
                        if (int.TryParse(text, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = default(int); return false;
            }
            private static bool TryParser_NInt32(object value, Nattrib attrib, out int? validValue)
            {
                if (value != null)
                {
                    if ((value is int) || (value is int?))
                    {
                        validValue = (int?)value; return true;
                    }
                    if (value is bool)
                    {
                        validValue = (!(bool)value ? 0 : 1); return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue2;
                        if (int.TryParse(text, out validValue2))
                        {
                            validValue = validValue2; return true;
                        }
                    }
                }
                validValue = null; return false;
            }

            private static bool Validator_Int32(object value, Nattrib attrib)
            {
                if (value != null)
                {
                    if (value is int)
                        return true;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue;
                        return int.TryParse(text, out validValue);
                    }
                }
                return false;
            }
            #endregion

            #region String
            private static string Parser_String(object value, string defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    string text;
                    if (((text = (value as string)) != null) && ((text = text.Trim()).Length > 0))
                        return text;
                }
                return defaultValue;
            }
            private static object Parser2_String(object value, object defaultValue, Nattrib attrib)
            {
                if (value != null)
                {
                    string text;
                    if (((text = (value as string)) != null) && ((text = text.Trim()).Length > 0))
                        return text;
                }
                return defaultValue;
            }

            private static bool TryParser_String(object value, Nattrib attrib, out string validValue)
            {
                if (value != null)
                {
                    string text;
                    if (((text = (value as string)) != null) && ((text = text.Trim()).Length > 0))
                    {
                        validValue = text; return true;
                    }
                }
                validValue = default(string); return false;
            }

            private static bool Validator_String(object value, Nattrib attrib)
            {
                string text = (value as string);
                return ((text != null) && (text.Trim().Length > 0));
            }
            #endregion
        }
    }
}
