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
using System.Text;
namespace System.IO
{
    public static partial class FileEx
    {
        /// <summary>
        /// MockBase
        /// </summary>
        public abstract class MockBase
        {
            protected MockBase() { }

            public virtual void AppendAllText(string path, string contents) { throw new NotImplementedException(); }
            public virtual void AppendAllText(string path, string contents, Encoding encoding) { throw new NotImplementedException(); }
            public virtual StreamWriter AppendText(string path) { throw new NotImplementedException(); }
            public virtual void Copy(string sourceFileName, string destFileName) { throw new NotImplementedException(); }
            public virtual void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException(); }
            public virtual FileStream Create(string path) { throw new NotImplementedException(); }
            public virtual FileStream Create(string path, int bufferSize) { throw new NotImplementedException(); }
            public virtual FileStream Create(string path, int bufferSize, FileOptions options) { throw new NotImplementedException(); }
            public virtual FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity) { throw new NotImplementedException(); }
            public virtual StreamWriter CreateText(string path) { throw new NotImplementedException(); }
            public virtual void Decrypt(string path) { throw new NotImplementedException(); }
            public virtual void Delete(string path) { throw new NotImplementedException(); }
            public virtual void Encrypt(string path) { throw new NotImplementedException(); }
            public virtual bool Exists(string path) { throw new NotImplementedException(); }
            public virtual FileSecurity GetAccessControl(string path) { throw new NotImplementedException(); }
            public virtual FileSecurity GetAccessControl(string path, AccessControlSections includeSections) { throw new NotImplementedException(); }
            public virtual FileAttributes GetAttributes(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetCreationTime(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetCreationTimeUtc(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastAccessTime(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastAccessTimeUtc(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastWriteTime(string path) { throw new NotImplementedException(); }
            public virtual DateTime GetLastWriteTimeUtc(string path) { throw new NotImplementedException(); }
            public virtual void Move(string sourceFileName, string destFileName) { throw new NotImplementedException(); }
            public virtual FileStream Open(string path, FileMode mode) { throw new NotImplementedException(); }
            public virtual FileStream Open(string path, FileMode mode, FileAccess access) { throw new NotImplementedException(); }
            public virtual FileStream Open(string path, FileMode mode, FileAccess access, FileShare share) { throw new NotImplementedException(); }
            public virtual FileStream OpenRead(string path) { throw new NotImplementedException(); }
            public virtual StreamReader OpenText(string path) { throw new NotImplementedException(); }
            public virtual FileStream OpenWrite(string path) { throw new NotImplementedException(); }
            public virtual byte[] ReadAllBytes(string path) { throw new NotImplementedException(); }
            public virtual string[] ReadAllLines(string path) { throw new NotImplementedException(); }
            public virtual string[] ReadAllLines(string path, Encoding encoding) { throw new NotImplementedException(); }
            public virtual string ReadAllText(string path) { throw new NotImplementedException(); }
            public virtual string ReadAllText(string path, Encoding encoding) { throw new NotImplementedException(); }
            public virtual void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException(); }
            public virtual void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException(); }
            public virtual void SetAccessControl(string path, FileSecurity fileSecurity) { throw new NotImplementedException(); }
            public virtual void SetAttributes(string path, FileAttributes fileAttributes) { throw new NotImplementedException(); }
            public virtual void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException(); }
            public virtual void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException(); }
            public virtual void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException(); }
            public virtual void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException(); }
            public virtual void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException(); }
            public virtual void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException(); }
            public virtual void WriteAllBytes(string path, byte[] bytes) { throw new NotImplementedException(); }
            public virtual void WriteAllLines(string path, string[] contents) { throw new NotImplementedException(); }
            public virtual void WriteAllLines(string path, string[] contents, Encoding encoding) { throw new NotImplementedException(); }
            public virtual void WriteAllText(string path, string contents) { throw new NotImplementedException(); }
            public virtual void WriteAllText(string path, string contents, Encoding encoding) { throw new NotImplementedException(); }
        }
    }
}
