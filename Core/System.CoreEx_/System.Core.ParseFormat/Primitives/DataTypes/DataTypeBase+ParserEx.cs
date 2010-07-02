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
namespace System.Primitives.DataTypes
{
    //[ParserEx.CanParse()]
    public abstract partial class DataTypeBase : ParserEx.IObjectParserBuilder, ParserEx.IStringParserBuilder
    {
        private static Func<DataTypeBase> s_dataTypeGetter = null; //(Func<DataTypeBase>)Delegate.CreateDelegate(typeof(Func<DataTypeBase>), typeof(SingletonFactoryWithCreate<DataTypeBase>).GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null).MakeGenericMethod(s_type));

        //public object X<T>()
        //{
        //    //var x = new ParserEx.ObjectParserProvider<T>();
        //    return null;
        //}
        //if (type.IsSubclassOf(DataTypeBase.DataTypeBaseType))
        //{
        //    s_dataTypeGetter = (Func<DataTypeBase>)Delegate.CreateDelegate(typeof(Func<DataTypeBase>), typeof(SingletonFactoryWithCreate<DataTypeBase>).GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null).MakeGenericMethod(s_type));
        //    return new Func<object, T, T>(Parser_DataTypeBase);
        //}
        //if (type.IsSubclassOf(DataTypeBase.DataTypeBaseType))
        //{
        //    s_dataTypeGetter = (Func<DataTypeBase>)Delegate.CreateDelegate(typeof(Func<DataTypeBase>), typeof(SingletonFactoryWithCreate<DataTypeBase>).GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null).MakeGenericMethod(s_type));
        //    return new Func<string, T, T>(Parser_DataTypeBase);
        //}

        public ParserEx.IObjectParser<T> Build<T>()
        {
            throw new NotImplementedException();
        }

        internal static class ObjectDelegateFactory<T>
        {
            private static T Parser_DataTypeBase(object value, T defaultValue)
            {
                var dataType = s_dataTypeGetter();
                object value2;
                if (dataType.Parser.TryParse(value as string, out value2))
                    return (T)value2;
                return defaultValue;
            }
            private static object Parser2_DataTypeBase(object value, object defaultValue)
            {
                var dataType = s_dataTypeGetter();
                object value2;
                if (dataType.Parser.TryParse(value as string, out value2))
                    return (T)value2;
                return defaultValue;
            }

            private static bool TryParser_DataTypeBase(object value, out T validValue)
            {
                var dataType = s_dataTypeGetter();
                object value2;
                var returnValue = dataType.Parser.TryParse(value as string, out value2);
                validValue = (T)value2;
                return returnValue;
            }

            private static bool Validator_DataTypeBase(object value)
            {
                var dataType = s_dataTypeGetter();
                object value2;
                return dataType.Parser.TryParse(value as string, out value2);
            }
        }

        ParserEx.IStringParser<T> ParserEx.IStringParserBuilder.Build<T>()
        {
            throw new NotImplementedException();
        }

        internal static class StringDelegateFactory<T>
        {
            private static T Parser_DataTypeBase(string text, T defaultValue)
            {
                var dataType = s_dataTypeGetter();
                object value;
                return (dataType.Parser.TryParse(text, null, out value) ? (T)value : defaultValue);
            }
            private static object Parser2_DataTypeBase(string text, object defaultValue)
            {
                var dataType = s_dataTypeGetter();
                object value;
                return (dataType.Parser.TryParse(text, null, out value) ? (T)value : defaultValue);
            }

            private static bool TryParser_DataTypeBase(string text, out T validValue)
            {
                var dataType = s_dataTypeGetter();
                object value;
                var returnValue = dataType.Parser.TryParse(text, null, out value);
                validValue = (T)value;
                return returnValue;
            }

            private static bool Validator_DataTypeBase(string text)
            {
                var dataType = s_dataTypeGetter();
                object value;
                return dataType.Parser.TryParse(text, null, out value);
            }
        }
    }
}