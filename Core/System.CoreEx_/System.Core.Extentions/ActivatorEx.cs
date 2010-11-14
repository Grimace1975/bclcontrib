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
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
namespace System
{
    /// <summary>
    /// ActivatorEx
    /// </summary>
    public static class ActivatorEx
    {
        public static object CreateInstance(string type, params object[] args)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type");
            return Activator.CreateInstance(Type.GetType(type, true), args);
        }

        public static object CreateInstanceWithCompositeArguments(Type type, object[] prefixArgs, params object[] args)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (prefixArgs == null)
                throw new ArgumentNullException("prefixArgs");
            int prefixArgsLength;
            if ((args != null) && (args.Length > 0) && ((prefixArgsLength = prefixArgs.Length) > 0))
            {
                object[] compositeArgs = new object[args.Length + prefixArgsLength];
                prefixArgs.CopyTo(compositeArgs, 0);
                args.CopyTo(compositeArgs, prefixArgsLength);
                return Activator.CreateInstance(type, compositeArgs);
            }
            return Activator.CreateInstance(type, prefixArgs);
        }

        public static object CreateInstanceFromReader(Type type, XmlReader r, params object[] args)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (r == null)
                return Activator.CreateInstance(type, args);
            var serializer = new XmlSerializer(type, (string)null);
            return serializer.Deserialize(r, (string)null);
        }

        public static object CreateInstanceIndirect(Type type, params object[] args)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return type.InvokeMember("Create" + type.Name, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static, null, null, args);
        }
    }
}
