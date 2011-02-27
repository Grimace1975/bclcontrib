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
using System.IO;
using System.Text;
namespace System.Quality
{
    /// <summary>
    /// ISerializerExtensions
    /// </summary>
    public static class ISerializerExtensions
    {
        public static T ReadObject<T>(this ISerializer serializer, Type type, string text)
            where T : class
        {
            using (var s = new MemoryStream(Encoding.Default.GetBytes(text)))
                return serializer.ReadObject<T>(type, s);
        }

        public static string WriteObject<T>(this ISerializer serializer, Type type, T graph)
            where T : class
        {
            using (var s = new MemoryStream())
            {
                serializer.WriteObject<T>(type, s, graph);
                return Encoding.Default.GetString(s.ToArray());
            }
        }
    }
}