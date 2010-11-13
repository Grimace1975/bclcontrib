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
namespace System.Primitives.DataTypes
{
    public abstract partial class DataTypeBase : FormatterEx.IObjectFormatterBuilder, FormatterEx.IValueFormatterBuilder
    {
        //private static Func<DataTypeBase> s_dataTypeGetter = null; //(Func<DataTypeBase>)Delegate.CreateDelegate(typeof(Func<DataTypeBase>), typeof(SingletonFactoryWithCreate<DataTypeBase>).GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null).MakeGenericMethod(s_type));

        //if (type.IsSubclassOf(DataTypeBase.DataTypeBaseType))
        //{
        //    s_dataTypeGetter = (Func<DataTypeBase>)Delegate.CreateDelegate(typeof(Func<DataTypeBase>), typeof(SingletonFactoryWithCreate<DataTypeBase>).GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null).MakeGenericMethod(s_type));
        //    return new Func<object, string>(Formatter_DataTypeBase);
        //}

        #region Object
        FormatterEx.IObjectFormatter<T> FormatterEx.IObjectFormatterBuilder.Build<T>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Value
        FormatterEx.IValueFormatter<T> FormatterEx.IValueFormatterBuilder.Build<T>()
        {
            throw new NotImplementedException();
        }

        private static string Formatter_DataTypeBase(object value)
        {
            var dataType = s_dataTypeGetter();
            return dataType.Formatter.Format(value);
        }
        #endregion
    }
}