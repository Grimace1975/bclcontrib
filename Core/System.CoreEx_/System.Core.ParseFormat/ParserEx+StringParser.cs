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
        private static Dictionary<Type, object> s_stringParserProviders = null;

        public interface IStringParserBuilder
        {
            IStringParser<T> Build<T>();
        }

        public interface IStringParser<T>
        {
            T Parser(string value, T defaultValue);
            object Parser2(string value, object defaultValue);
            bool TryParser(string value, out T validValue);
            bool Validator(string value);
        }

        /// <summary>
        /// StringParserDelegateFactory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static class StringParserDelegateFactory<T>
        {
            private static readonly Type s_type = typeof(T);
            private static readonly MethodInfo s_tryParseMethodInfo = s_type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { CoreEx.StringType, s_type.MakeByRefType() }, null);
            public static readonly Func<string, T, T> Parse = CreateParser(s_type);
            public static readonly Func<string, object, object> Parse2 = CreateParser2(s_type);
            public static readonly TryFunc<string, T> TryParse = CreateTryParser(s_type);
            public static readonly Func<string, bool> Validate = CreateValidator(s_type);

            /// <summary>
            /// Initializes the <see cref="StringParserDelegateFactory&lt;T&gt;"/> class.
            /// </summary>
            static StringParserDelegateFactory() { }

            /// <summary>
            /// Creates the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<string, T, T> CreateParser(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (Func<string, T, T>)Delegate.CreateDelegate(typeof(Func<string, T, T>), typeof(StringParserDelegateFactory<T>).GetMethod("Parser_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (Func<string, T, T>)Delegate.CreateDelegate(typeof(Func<string, T, T>), typeof(StringParserDelegateFactory<T>).GetMethod("Parser_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (Func<string, T, T>)Delegate.CreateDelegate(typeof(Func<string, T, T>), typeof(StringParserDelegateFactory<T>).GetMethod("Parser_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (Func<string, T, T>)Delegate.CreateDelegate(typeof(Func<string, T, T>), typeof(StringParserDelegateFactory<T>).GetMethod("Parser_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (Func<string, T, T>)Delegate.CreateDelegate(typeof(Func<string, T, T>), typeof(StringParserDelegateFactory<T>).GetMethod("Parser_String", BindingFlags.NonPublic | BindingFlags.Static));
                if (s_stringParserProviders != null)
                    foreach (var stringParser in s_stringParserProviders)
                        if (type.IsSubclassOf(stringParser.Key))
                            return ((IStringParser<T>)stringParser.Value).Parser;
                //ScanForSubclassParser();
                EnsureTryParseMethod();
                return new Func<string, T, T>(Parser_Default);
            }
            /// <summary>
            /// Create2s the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<string, object, object> CreateParser2(Type type)
            {
                if (s_stringParserProviders != null)
                    foreach (var stringParser in s_stringParserProviders)
                        if (type.IsSubclassOf(stringParser.Key))
                            return ((IStringParser<T>)stringParser.Value).Parser2;
                //ScanForSubclassParser();
                //EnsureTryParseMethod();
                return new Func<string, object, object>(Parser2_Default);
            }

            /// <summary>
            /// Creates the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static TryFunc<string, T> CreateTryParser(Type type)
            {
                if (type == CoreEx.BoolType)
                    return (TryFunc<string, T>)Delegate.CreateDelegate(typeof(TryFunc<string, T>), typeof(StringParserDelegateFactory<T>).GetMethod("TryParser_Bool", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DateTimeType)
                    return (TryFunc<string, T>)Delegate.CreateDelegate(typeof(TryFunc<string, T>), typeof(StringParserDelegateFactory<T>).GetMethod("TryParser_DateTime", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.DecimalType)
                    return (TryFunc<string, T>)Delegate.CreateDelegate(typeof(TryFunc<string, T>), typeof(StringParserDelegateFactory<T>).GetMethod("TryParser_Decimal", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.Int32Type)
                    return (TryFunc<string, T>)Delegate.CreateDelegate(typeof(TryFunc<string, T>), typeof(StringParserDelegateFactory<T>).GetMethod("TryParser_Int32", BindingFlags.NonPublic | BindingFlags.Static));
                if (type == CoreEx.StringType)
                    return (TryFunc<string, T>)Delegate.CreateDelegate(typeof(TryFunc<string, T>), typeof(StringParserDelegateFactory<T>).GetMethod("TryParser_String", BindingFlags.NonPublic | BindingFlags.Static));
                if (s_stringParserProviders != null)
                    foreach (var stringParser in s_stringParserProviders)
                        if (type.IsSubclassOf(stringParser.Key))
                            return ((IStringParser<T>)stringParser.Value).TryParser;
                //ScanForSubclassParser();
                EnsureTryParseMethod();
                return new TryFunc<string, T>(TryParser_Default);
            }

            /// <summary>
            /// Creates the specified type.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            private static Func<string, bool> CreateValidator(Type type)
            {
                if (type == CoreEx.BoolType)
                    return new Func<string, bool>(Validator_Bool);
                if (type == CoreEx.DateTimeType)
                    return new Func<string, bool>(Validator_DateTime);
                if (type == CoreEx.DecimalType)
                    return new Func<string, bool>(Validator_Decimal);
                if (type == CoreEx.Int32Type)
                    return new Func<string, bool>(Validator_Int32);
                if (type == CoreEx.StringType)
                    return new Func<string, bool>(Validator_String);
                if (s_stringParserProviders != null)
                    foreach (var stringParser in s_stringParserProviders)
                        if (type.IsSubclassOf(stringParser.Key))
                            return ((IStringParser<T>)stringParser.Value).Validator;
                //ScanForSubclassParser();
                EnsureTryParseMethod();
                return new Func<string, bool>(Validator_Default);
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
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static T Parser_Default(string text, T defaultValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    var args = new object[] { text, default(T) };
                    if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                        return (T)args[1];
                }
                return defaultValue;
            }
            /// <summary>
            /// Values the field2.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static object Parser2_Default(string text, object defaultValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    var args = new object[] { text, default(T) };
                    if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                        return (T)args[1];
                }
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_Default(string text, out T validValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    var args = new object[] { text, default(T) };
                    if ((bool)s_tryParseMethodInfo.Invoke(null, BindingFlags.Default, null, args, null))
                    {
                        validValue = (T)args[1];
                        return true;
                    }
                }
                validValue = default(T);
                return false;
            }

            /// <summary>
            /// Values the field.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <returns></returns>
            private static bool Validator_Default(string text)
            {
                if ((text != null) && (text.Length > 0))
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
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
            /// <returns></returns>
            private static bool Parser_Bool(string text, bool defaultValue)
            {
                if ((text != null) && (text.Length > 0))
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
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field_ bool.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="validValue">if set to <c>true</c> [valid value].</param>
            /// <returns></returns>
            private static bool TryParser_Bool(string text, out bool validValue)
            {
                if ((text != null) && (text.Length > 0))
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
                validValue = default(bool);
                return false;
            }

            /// <summary>
            /// Values the field_ bool.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_Bool(string text)
            {
                if ((text != null) && (text.Length > 0))
                {
                    bool validValue;
                    return bool.TryParse(text, out validValue);
                }
                return false;
            }
            #endregion

            #region DateTime
            /// <summary>
            /// Values the field_ date time.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static DateTime Parser_DateTime(string text, DateTime defaultValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    DateTime validValue;
                    if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue))
                        return validValue;
                }
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field_ date time.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_DateTime(string text, out DateTime validValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    DateTime validValue2;
                    if (DateTime.TryParse(text, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out validValue2))
                    {
                        validValue = validValue2;
                        return true;
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
            private static bool Validator_DateTime(string text)
            {
                if ((text != null) && (text.Length > 0))
                {
                    DateTime validValue;
                    return DateTime.TryParse(text, out validValue);
                }
                return false;
            }
            #endregion

            #region Decimal
            /// <summary>
            /// Values the field_ decimal.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static decimal Parser_Decimal(string text, decimal defaultValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    decimal validValue;
                    if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue))
                        return validValue;
                }
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field_ decimal.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_Decimal(string text, out decimal validValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    decimal validValue2;
                    if (decimal.TryParse(text, NumberStyles.Currency, null, out validValue2))
                    {
                        validValue = validValue2;
                        return true;
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
            private static bool Validator_Decimal(string text)
            {
                if ((text != null) && (text.Length > 0))
                {
                    decimal validValue;
                    return decimal.TryParse(text, NumberStyles.Currency, null, out validValue);
                }
                return false;
            }
            #endregion

            #region Int32
            /// <summary>
            /// Values the field_ int32.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static int Parser_Int32(string text, int defaultValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    int validValue;
                    if (int.TryParse(text, out validValue))
                        return validValue;
                }
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field_ int32.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_Int32(string text, out int validValue)
            {
                if ((text != null) && (text.Length > 0))
                {
                    int validValue2;
                    if (int.TryParse(text, out validValue2))
                    {
                        validValue = validValue2;
                        return true;
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
            private static bool Validator_Int32(string text)
            {
                if ((text != null) && (text.Length > 0))
                {
                    int validValue;
                    return int.TryParse(text, out validValue);
                }
                return false;
            }
            #endregion

            #region String
            /// <summary>
            /// Values the field_ text.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns></returns>
            private static string Parser_String(string text, string defaultValue)
            {
                string value2;
                if ((text != null) && ((value2 = text.Trim()).Length > 0))
                    return value2;
                return defaultValue;
            }

            /// <summary>
            /// Tries the parse field_ text.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="validValue">The valid value.</param>
            /// <returns></returns>
            private static bool TryParser_String(string text, out string validValue)
            {
                string value2;
                if ((text != null) && ((value2 = text.Trim()).Length > 0))
                {
                    validValue = value2;
                    return true;
                }
                validValue = default(string);
                return false;
            }

            /// <summary>
            /// Values the field_ text.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private static bool Validator_String(string text)
            {
                return ((text != null) && (text.Trim().Length > 0));
            }
            #endregion
        }
    }
}
