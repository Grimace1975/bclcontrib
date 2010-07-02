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
using System.Security.AccessControl;
namespace System.IO
{
    public static partial class DirectoryEx
    {
        /// <summary>
        /// MockBase
        /// </summary>
        public abstract class MockBase
        {
            protected MockBase() { }

            public virtual DirectoryInfo CreateDirectory(string path) { throw new NotImplementedException(); }
            public virtual DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity) { throw new NotImplementedException(); }
            public virtual void Delete(string path) { throw new NotImplementedException(); }
            public virtual void Delete(string path, bool recursive) { throw new NotImplementedException(); }
            public virtual bool Exists(string path) { throw new NotImplementedException(); }
            public virtual DirectorySecurity GetAccessControl(string path) { throw new NotImplementedException(); }
            public virtual DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections) { throw new NotImplementedException(); }
            public virtual DateTime GetCreationTime(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetCreationTimeUtc(string path) { throw new NotImplementedException(); }
            public virtual string GetCurrentDirectory() { throw new NotImplementedException(); }
            public virtual string[] GetDirectories(string path) { throw new NotImplementedException(); }
            public virtual string[] GetDirectories(string path, string searchPattern) { throw new NotImplementedException(); }
            public virtual string[] GetDirectories(string path, string searchPattern, SearchOption searchOption) { throw new NotImplementedException(); }
            public virtual string GetDirectoryRoot(string path) { throw new NotImplementedException(); }
            public virtual string[] GetFiles(string path) { throw new NotImplementedException(); }
            public virtual string[] GetFiles(string path, string searchPattern) { throw new NotImplementedException(); }
            public virtual string[] GetFiles(string path, string searchPattern, SearchOption searchOption) { throw new NotImplementedException(); }
            public virtual string[] GetFileSystemEntries(string path) { throw new NotImplementedException(); }
            public virtual string[] GetFileSystemEntries(string path, string searchPattern) { throw new NotImplementedException(); }
            public virtual DateTime GetLastAccessTime(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastAccessTimeUtc(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastWriteTime(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastWriteTimeUtc(string path) { throw new NotImplementedException(); }
            public virtual string[] GetLogicalDrives() { throw new NotImplementedException(); }
            public virtual DirectoryInfo GetParent(string path) { throw new NotImplementedException(); }
            public virtual void Move(string sourceDirName, string destDirName) { throw new NotImplementedException(); }
            public virtual void SetAccessControl(string path, DirectorySecurity directorySecurity) { throw new NotImplementedException(); }
            public virtual void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException(); }
            public virtual void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException(); }
            public virtual void SetCurrentDirectory(string path) { throw new NotImplementedException(); }
            public virtual void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException(); }
            public virtual void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException(); }
            public virtual void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException(); }
            public virtual void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException(); }
        }
    }
}
