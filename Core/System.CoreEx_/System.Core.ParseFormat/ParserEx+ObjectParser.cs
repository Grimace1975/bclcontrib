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
using System.Text.RegularExpressions;
using System.Text;
namespace System
{
    public static partial class ParserEx
    {
        private static Dictionary<Type, object> s_objectParserProviders = null;
        private static readonly Type s_objectParserBuilderType = typeof(IObjectParserBuilder);

        public interface IObjectParserBuilder
        {
            IObjectParser<T> Build<T>();
        }

        public interface IObjectParser<T>
        {
            T Parser(object value, T defaultValue);
            object Parser2(object value, object defaultValue);
            bool TryParser(object value, out T validValue);
            bool Validator(object value);
        }

        /// <summary>
        /// ObjectParserDelegateFactory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static class ObjectParserDelegateFactory<T>
        {
            private static readonly Type s_type = typeof(T);
            private static readonly MethodInfo s_tryParseMethodInfo = s_type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { CoreEx.StringType, s_type.MakeByRefType() }, null);
            public static readonly Func<object, T, T> Parse = CreateParser(s_type);
            public static readonly Func<object, object, object> Parse2 = CreateParser2(s_type);
            public static readonly TryFunc<object, T> TryParse = CreateTryParser(s_type);
            public static readonly Func<object, bool> Validate = CreateValidator(s_type);

            /// <summary>
            /// Initializes the <see cref="ObjectParserDelegateFactory&lt;T&gt;"/> class.
            /// </summary>
            static ObjectParserDelegateFactory() { }

            /// <summary>
            /// Creates the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<object, T, T> CreateParser(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (Func<object, T, T>)Delegate.CreateDelegate(typeof(Func<object, T, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("Parser_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (Func<object, T, T>)Delegate.CreateDelegate(typeof(Func<object, T, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("Parser_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (Func<object, T, T>)Delegate.CreateDelegate(typeof(Func<object, T, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("Parser_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (Func<object, T, T>)Delegate.CreateDelegate(typeof(Func<object, T, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("Parser_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (Func<object, T, T>)Delegate.CreateDelegate(typeof(Func<object, T, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("Parser_String", BindingFlags.NonPublic | BindingFlags.Static));
                //var interfaces = type.FindInterfaces(((m, filterCriteria) => m == s_objectParserBuilderType), null);
                if (s_objectParserProviders != null)
                    foreach (var objectParser in s_objectParserProviders)
                        if (type.IsSubclassOf(objectParser.Key))
                            return ((IObjectParser<T>)objectParser.Value).Parser;
                //Type key;
                //IObjectParser<T> objectParser;
                //if (TryScanForObjectParser(out key, out objectParser))
                //{
                //    if (s_objectParserProviders == null)
                //        s_objectParserProviders = new Dictionary<Type, object>();
                //    s_objectParserProviders.Add(key, objectParser);
                //    return objectParser.Parser;
                //}
                EnsureTryParseMethod();
                return new Func<object, T, T>(Parser_Default);
            }
            /// <summary>
            /// Create2s the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<object, object, object> CreateParser2(Type type)
            {
                if (type == CoreEx.BoolType)
                    return new Func<object, object, object>(Parser2_Bool);
                if (type == CoreEx.DateTimeType)
                    return new Func<object, object, object>(Parser2_DateTime);
                if (type == CoreEx.DecimalType)
                    return new Func<object, object, object>(Parser2_Decimal);
                if (type == CoreEx.Int32Type)
                    return new Func<object, object, object>(Parser2_Int32);
                if (type == CoreEx.StringType)
                    return new Func<object, object, object>(Parser2_String);
                if (s_objectParserProviders != null)
                    foreach (var objectParser in s_objectParserProviders)
                        if (type.IsSubclassOf(objectParser.Key))
                            return ((IObjectParser<T>)objectParser.Value).Parser2;
                //ScanForSubclassParser();
                EnsureTryParseMethod();
                return new Func<object, object, object>(Parser2_Default);
            }

            /// <summary>
            /// Creates the try parse.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static TryFunc<object, T> CreateTryParser(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (TryFunc<object, T>)Delegate.CreateDelegate(typeof(TryFunc<object, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("TryParser_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (TryFunc<object, T>)Delegate.CreateDelegate(typeof(TryFunc<object, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("TryParser_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (TryFunc<object, T>)Delegate.CreateDelegate(typeof(TryFunc<object, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("TryParser_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (TryFunc<object, T>)Delegate.CreateDelegate(typeof(TryFunc<object, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("TryParser_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (TryFunc<object, T>)Delegate.CreateDelegate(typeof(TryFunc<object, T>), typeof(ObjectParserDelegateFactory<T>).GetMethod("TryParser_String", BindingFlags.NonPublic | BindingFlags.Static));
                if (s_objectParserProviders != null)
                    foreach (var objectParser in s_objectParserProviders)
                        if (type.IsSubclassOf(objectParser.Key))
                            return ((IObjectParser<T>)objectParser.Value).TryParser;
                //ScanForSubclassParser();
                EnsureTryParseMethod();
                return new TryFunc<object, T>(TryParser_Default);
            }

            /// <summary>
            /// Creates the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<object, bool> CreateValidator(Type type)
            {
                if (type == CoreEx.BoolType)
                    return new Func<object, bool>(Validator_Bool);
                else if (type == CoreEx.DateTimeType)
                    return new Func<object, bool>(Validator_DateTime);
                else if (type == CoreEx.DecimalType)
                    return new Func<object, bool>(Validator_Decimal);
                else if (type == CoreEx.Int32Type)
                    return new Func<object, bool>(Validator_Int32);
                else if (type == CoreEx.StringType)
                    return new Func<object, bool>(Validator_String);
                if (s_objectParserProviders != null)
                    foreach (var objectParser in s_objectParserProviders)
                        if (type.IsSubclassOf(objectParser.Key))
                            return ((IObjectParser<T>)objectParser.Value).Validator;
                //ScanForSubclassParser();
                return new Func<object, bool>(Validator_Default);
            }

            /// <summary>
            /// Ensures the try parse method.
            /// </summary>
            private static void EnsureTryParseMethod()
            {
                if (s_tryParseMethodInfo == null)
                    throw new InvalidOperationException(string.Format("{0}::TypeParse method required for parsing", typeof(T).ToString()));
            }

            #region Default
            /// <summary>
            /// Values the field.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static T Parser_Default(object value, T defaultValue)
            {
                if (value != null)
                {
                    if (value is T)
                        return (T)value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        var args = new object[] { text, default(T) };
                        if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                            return (T)args[1];
                    }
                }
                return defaultValue;
            }
            /// <summary>
            /// Values the field.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static object Parser2_Default(object value, object defaultValue)
            {
                if (value != null)
                {
                    if (value is T)
                        return value;
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        var args = new object[] { text, default(T) };
                        if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                            return (T)args[1];
                    }
                }
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_Default(object value, out T validValue)
            {
                if (value != null)
                {
                    if (value is T)
                    {
                        validValue = (T)value;
                        return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        var args = new object[] { text, default(T) };
                        if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                        {
                            validValue = (T)args[1];
                            return true;
                        }
                    }
                }
                validValue = default(T);
                return false;
            }

            /// <summary>
            /// Values the field.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_Default(object value)
            {
                if (value == null)
                    return false;
                if (value is T)
                    return true;
                string text;
                if (((text = (value as string)) != null) && (text.Length > 0))
                {
                    var args = new object[] { text, default(T) };
                    if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                        return true;
                }
                return false;
            }
            #endregion

            #region Bool
            /// <summary>
            /// Values the field_ bool.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
            /// <returns></returns>
            private static bool Parser_Bool(object value, bool defaultValue)
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
                            case "true":
                            case "yes":
                            case "on":
                                return true;
                            case "0":
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
            /// <summary>
            /// Values the field_ bool.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static object Parser2_Bool(object value, object defaultValue)
            {
                if (value != null)
                {
                    if (value is bool)
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
                            case "true":
                            case "yes":
                            case "on":
                                return true;
                            case "0":
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

            /// <summary>
            /// Tries the parse field_ bool.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="validValue">if set to <c>true</c> [valid value].</param>
            /// <returns></returns>
            private static bool TryParser_Bool(object value, out bool validValue)
            {
                if (value != null)
                {
                    if (value is bool)
                    {
                        validValue = (bool)value;
                        return true;
                    }
                    if (value is int)
                    {
                        switch ((int)value)
                        {
                            case 1:
                                validValue = true;
                                return true;
                            case 0:
                                validValue = false;
                                return true;
                        }
                        validValue = default(bool);
                        return false;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        switch (text.ToLowerInvariant())
                        {
                            case "1":
                            case "true":
                            case "yes":
                            case "on":
                                validValue = true;
                                return true;
                            case "0":
                            case "false":
                            case "no":
                            case "off":
                                validValue = false;
                                return true;
                        }
                        bool validValue2;
                        if (bool.TryParse(text, out validValue2))
                        {
                            validValue = validValue2;
                            return true;
                        }
                    }
                }
                validValue = default(bool);
                return false;
            }

            /// <summary>
            /// Values the field_ bool.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_Bool(object value)
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
                            case "true":
                            case "yes":
                            case "on":
                            case "0":
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
            /// <summary>
            /// Values the field_ date time.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static DateTime Parser_DateTime(object value, DateTime defaultValue)
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
            /// <summary>
            /// Values the field2_ date time.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static object Parser2_DateTime(object value, object defaultValue)
            {
                if (value != null)
                {
                    if (value is DateTime)
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

            /// <summary>
            /// Tries the parse field_ date time.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_DateTime(object value, out DateTime validValue)
            {
                if (value != null)
                {
                    if (value is DateTime)
                    {
                        validValue = (DateTime)value;
                        return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        DateTime validValue2;
                        if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue2))
                        {
                            validValue = validValue2;
                            return true;
                        }
                    }
                }
                validValue = default(DateTime);
                return false;
            }

            /// <summary>
            /// Values the field_ date time.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_DateTime(object value)
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
            /// <summary>
            /// Values the field_ decimal.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static decimal Parser_Decimal(object value, decimal defaultValue)
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
            /// <summary>
            /// Values the field2_ decimal.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static object Parser2_Decimal(object value, object defaultValue)
            {
                if (value != null)
                {
                    if (value is decimal)
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

            /// <summary>
            /// Tries the parse field_ decimal.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_Decimal(object value, out decimal validValue)
            {
                if (value != null)
                {
                    if (value is decimal)
                    {
                        validValue = (decimal)value;
                        return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        decimal validValue2;
                        if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue2))
                        {
                            validValue = validValue2;
                            return true;
                        }
                    }
                }
                validValue = default(decimal);
                return false;
            }


            /// <summary>
            /// Values the field_ decimal.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_Decimal(object value)
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
            /// <summary>
            /// Values the field_ int32.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static int Parser_Int32(object value, int defaultValue)
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
            /// <summary>
            /// Values the field2_ int32.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static object Parser2_Int32(object value, object defaultValue)
            {
                if (value == null)
                {
                    if (value is int)
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

            /// <summary>
            /// Tries the parse field_ int32.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_Int32(object value, out int validValue)
            {
                if (value != null)
                {
                    if (value is int)
                    {
                        validValue = (int)value;
                        return true;
                    }
                    if (value is bool)
                    {
                        validValue = (!(bool)value ? 0 : 1);
                        return true;
                    }
                    string text;
                    if (((text = (value as string)) != null) && (text.Length > 0))
                    {
                        int validValue2;
                        if (int.TryParse(text, out validValue2))
                        {
                            validValue = validValue2;
                            return true;
                        }
                    }
                }
                validValue = default(int);
                return false;
            }

            /// <summary>
            /// Values the field_ int32.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_Int32(object value)
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
            /// <summary>
            /// Values the field_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static string Parser_String(object value, string defaultValue)
            {
                if (value != null)
                {
                    string text;
                    if (((text = (value as string)) != null) && ((text = text.Trim()).Length > 0))
                        return text;
                }
                return defaultValue;
            }
            /// <summary>
            /// Values the field2_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static object Parser2_String(object value, object defaultValue)
            {
                if (value != null)
                {
                    string text;
                    if (((text = (value as string)) != null) && ((text = text.Trim()).Length > 0))
                        return text;
                }
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_String(object value, out string validValue)
            {
                if (value != null)
                {
                    string text;
                    if (((text = (value as string)) != null) && ((text = text.Trim()).Length > 0))
                    {
                        validValue = text;
                        return true;
                    }
                }
                validValue = default(string);
                return false;
            }

            /// <summary>
            /// Values the field_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_String(object value)
            {
                string text = (value as string);
                return ((text != null) && (text.Trim().Length > 0));
            }
            #endregion
        }
    }
}
