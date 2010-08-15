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
    /// <summary>
    /// Core static class
    /// </summary>
    public static partial class CoreEx
    {
        /// <summary>
        /// String value "::"
        /// </summary>
        public const string Scope = "::";
        public const string EmptyHtml = "\u00A0";
        /// <summary>
        /// Integer value of Scope Length
        /// </summary>
        public const int ScopeLength = 2;
        internal static readonly Type BoolType = typeof(bool);
        internal static readonly Type DateTimeType = typeof(DateTime);
        internal static readonly Type DecimalType = typeof(decimal);
        internal static readonly Type Int32Type = typeof(int);
        internal static readonly Type StringType = typeof(string);
        internal static readonly Type ObjectType = typeof(object);
        internal static readonly Type XmlReaderType = typeof(XmlReader);
        internal static readonly Type XmlWriterType = typeof(XmlWriter);

        [ThreadStatic]
        private static MockBase s_mock;

        public static MockBase Mock
        {
            get { return s_mock; }
            set { s_mock = value; }
        }

#if !SqlServer
        private static readonly object s_nextIdLock = new object();
        private static int s_nextId;

        /// <summary>
        /// Gets the next id in the sequence.
        /// </summary>
        /// <returns></returns>
        public static int GetNextId() { return (s_mock == null ? GetNextIdInternal() : s_mock.GetNextId()); }

        private static int GetNextIdInternal()
        {
            int nextId;
            lock (s_nextIdLock)
            {
                nextId = ++s_nextId;
                if (nextId == (1 >> 30))
                    nextId = s_nextId = 0;
            }
            return nextId;
        }
#endif
    }
}
