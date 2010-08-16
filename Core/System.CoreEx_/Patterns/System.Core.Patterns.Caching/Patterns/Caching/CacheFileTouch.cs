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
using System;
using System.Linq;
using System.IO;
namespace System.Patterns.Caching
{
    public partial class CacheFileTouch
    {
        private static readonly object s_filesLock = new object();

        /// <summary>
        /// Touches the specified key array.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public static void TouchFiles(string folder, params string[] keys)
        {
            if ((keys == null) || (keys.Length == 0))
                return;
            foreach (var key in keys)
                lock (s_filesLock)
                {
                    var filePath = GetTouchFullPath(folder, key);
                    try
                    {
                        File.WriteAllText(filePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                    }
                    catch { };
                }
        }

        public static string GetTouchFullPath(string folder, string key)
        {
            return folder.EnsureEndsWith("\\") + key + ".txt";
        }

        public static CacheDependency MakeCacheDependency(string folder, params string[] keys)
        {
            if ((keys == null) || (keys.Length == 0))
                throw new ArgumentNullException("keys");
            EnsureCacheDependency(folder, keys);
            return new CacheDependency
            {
                FilePaths = keys.Select(k => GetTouchFullPath(folder, k))
                    .ToArray(),
            };
        }

        private static void EnsureCacheDependency(string folder, string[] keys)
        {
            lock (s_filesLock)
                foreach (var key in keys)
                {
                    var filePath = GetTouchFullPath(folder, key);
                    try
                    {
                        if (!File.Exists(filePath))
                            File.WriteAllText(filePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                    }
                    catch { };
                }
        }
    }
}
