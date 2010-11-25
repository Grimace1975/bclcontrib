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
using System.Xml;
namespace System
{
    internal static partial class CoreExInternal
    {
        internal static readonly Type BoolType = typeof(bool);
        internal static readonly Type NBoolType = typeof(bool?);
        internal static readonly Type DateTimeType = typeof(DateTime);
        internal static readonly Type NDateTimeType = typeof(DateTime?);
        internal static readonly Type DecimalType = typeof(decimal);
        internal static readonly Type NDecimalType = typeof(decimal?);
        internal static readonly Type Int32Type = typeof(int);
        internal static readonly Type NInt32Type = typeof(int?);
        internal static readonly Type StringType = typeof(string);
        internal static readonly Type ObjectType = typeof(object);
        internal static readonly Type XmlReaderType = typeof(XmlReader);
        internal static readonly Type XmlWriterType = typeof(XmlWriter);
    }
}
