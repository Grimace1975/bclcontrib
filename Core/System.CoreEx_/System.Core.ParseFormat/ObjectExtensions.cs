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
namespace System.Ext
{
    /// <summary>
    /// ObjectExtensions
    /// </summary>
    public static partial class ObjectExtensions
    {
        public static string Format<T>(this object obj)
        {
            return FormatterEx.ObjectFormatterDelegateFactory<T>.Format(obj);
        }

        public static T Parse<T>(this object obj)
        {
            return ParserEx.ObjectParserDelegateFactory<T>.Parse(obj, default(T));
        }

        public static T Parse<T>(this object obj, T defaultValue)
        {
            return ParserEx.ObjectParserDelegateFactory<T>.Parse(obj, defaultValue);
        }

        public static object Parse<T>(this object obj, object defaultValue)
        {
            return ParserEx.ObjectParserDelegateFactory<T>.Parse2(obj, defaultValue);
        }
    }
}