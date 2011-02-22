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
namespace System.IO
{
    /// <summary>
    /// PathEx
    /// </summary>
    public static partial class PathEx
    {
        [ThreadStatic]
        private static MockBase s_mock;

        public static MockBase Mock
        {
            get { return s_mock; }
            set { s_mock = value; }
        }

        public static string ChangeExtension(string path, string extension) { return s_mock.ChangeExtension(path, extension); }
        public static string Combine(string path1, string path2) { return s_mock.Combine(path1, path2); }
        public static string GetDirectoryName(string path) { return s_mock.GetDirectoryName(path); }
        public static string GetExtension(string path) { return s_mock.GetExtension(path); }
        public static string GetFileName(string path) { return s_mock.GetFileName(path); }
        public static string GetFileNameWithoutExtension(string path) { return s_mock.GetFileNameWithoutExtension(path); }
        public static string GetFullPath(string path) { return s_mock.GetFullPath(path); }
        public static char[] GetInvalidFileNameChars() { return s_mock.GetInvalidFileNameChars(); }
        public static char[] GetInvalidPathChars() { return s_mock.GetInvalidPathChars(); }
        public static string GetPathRoot(string path) { return s_mock.GetPathRoot(path); }
        public static string GetRandomFileName() { return s_mock.GetRandomFileName(); }
        public static string GetTempFileName() { return s_mock.GetTempFileName(); }
        public static string GetTempPath() { return s_mock.GetTempPath(); }
        public static bool HasExtension(string path) { return s_mock.HasExtension(path); }
        public static bool IsPathRooted(string path) { return s_mock.IsPathRooted(path); }
    }
}
