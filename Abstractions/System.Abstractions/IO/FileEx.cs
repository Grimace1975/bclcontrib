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
using System.Text;
using System.Security.AccessControl;
namespace System.IO
{
    /// <summary>
    /// FileEx
    /// </summary>
    public static partial class FileEx
    {
        [ThreadStatic]
        private static MockBase s_mock;

        public static MockBase Mock
        {
            get { return s_mock; }
            set { s_mock = value; }
        }

        public static void AppendAllText(string path, string contents) { s_mock.AppendAllText(path, contents); }
        public static void AppendAllText(string path, string contents, Encoding encoding) { s_mock.AppendAllText(path, contents, encoding); }
        public static StreamWriter AppendText(string path) { return s_mock.AppendText(path); }
        public static void Copy(string sourceFileName, string destFileName) { s_mock.Copy(sourceFileName, destFileName); }
        public static void Copy(string sourceFileName, string destFileName, bool overwrite) { s_mock.Copy(sourceFileName, destFileName, overwrite); }
        public static FileStream Create(string path) { return s_mock.Create(path); }
        public static FileStream Create(string path, int bufferSize) { return s_mock.Create(path, bufferSize); }
        public static FileStream Create(string path, int bufferSize, FileOptions options) { return s_mock.Create(path, bufferSize, options); }
        public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity) { return s_mock.Create(path, bufferSize, options, fileSecurity); }
        public static StreamWriter CreateText(string path) { return s_mock.CreateText(path); }
        public static void Decrypt(string path) { s_mock.Decrypt(path); }
        public static void Delete(string path) { s_mock.Delete(path); }
        public static void Encrypt(string path) { s_mock.Encrypt(path); }
        public static bool Exists(string path) { return s_mock.Exists(path); }
        public static FileSecurity GetAccessControl(string path) { return s_mock.GetAccessControl(path); }
        public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections) { return s_mock.GetAccessControl(path, includeSections); }
        public static FileAttributes GetAttributes(string path) { return s_mock.GetAttributes(path); }
        public static DateTime GetCreationTime(string path) { return s_mock.GetCreationTime(path); }
        public static DateTime GetCreationTimeUtc(string path) { return s_mock.GetCreationTimeUtc(path); }
        public static DateTime GetLastAccessTime(string path) { return s_mock.GetLastAccessTime(path); }
        public static DateTime GetLastAccessTimeUtc(string path) { return s_mock.GetLastAccessTimeUtc(path); }
        public static DateTime GetLastWriteTime(string path) { return s_mock.GetLastWriteTime(path); }
        public static DateTime GetLastWriteTimeUtc(string path) { return s_mock.GetLastWriteTimeUtc(path); }
        public static void Move(string sourceFileName, string destFileName) { s_mock.Move(sourceFileName, destFileName); }
        public static FileStream Open(string path, FileMode mode) { return s_mock.Open(path, mode); }
        public static FileStream Open(string path, FileMode mode, FileAccess access) { return s_mock.Open(path, mode, access); }
        public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share) { return s_mock.Open(path, mode, access, share); }
        public static FileStream OpenRead(string path) { return s_mock.OpenRead(path); }
        public static StreamReader OpenText(string path) { return s_mock.OpenText(path); }
        public static FileStream OpenWrite(string path) { return s_mock.OpenWrite(path); }
        public static byte[] ReadAllBytes(string path) { return s_mock.ReadAllBytes(path); }
        public static string[] ReadAllLines(string path) { return s_mock.ReadAllLines(path); }
        public static string[] ReadAllLines(string path, Encoding encoding) { return s_mock.ReadAllLines(path); }
        public static string ReadAllText(string path) { return s_mock.ReadAllText(path); }
        public static string ReadAllText(string path, Encoding encoding) { return s_mock.ReadAllText(path); }
        public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { s_mock.Replace(sourceFileName, destinationFileName, destinationBackupFileName); }
        public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { s_mock.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors); }
        public static void SetAccessControl(string path, FileSecurity fileSecurity) { s_mock.SetAccessControl(path, fileSecurity); }
        public static void SetAttributes(string path, FileAttributes fileAttributes) { s_mock.SetAttributes(path, fileAttributes); }
        public static void SetCreationTime(string path, DateTime creationTime) { s_mock.SetCreationTime(path, creationTime); }
        public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { s_mock.SetCreationTimeUtc(path, creationTimeUtc); }
        public static void SetLastAccessTime(string path, DateTime lastAccessTime) { s_mock.SetLastAccessTime(path, lastAccessTime); }
        public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { s_mock.SetLastAccessTimeUtc(path, lastAccessTimeUtc); }
        public static void SetLastWriteTime(string path, DateTime lastWriteTime) { s_mock.SetLastWriteTime(path, lastWriteTime); }
        public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { s_mock.SetLastWriteTimeUtc(path, lastWriteTimeUtc); }
        public static void WriteAllBytes(string path, byte[] bytes) { s_mock.WriteAllBytes(path, bytes); }
        public static void WriteAllLines(string path, string[] contents) { s_mock.WriteAllLines(path, contents); }
        public static void WriteAllLines(string path, string[] contents, Encoding encoding) { s_mock.WriteAllLines(path, contents, encoding); }
        public static void WriteAllText(string path, string contents) { s_mock.WriteAllText(path, contents); }
        public static void WriteAllText(string path, string contents, Encoding encoding) { s_mock.WriteAllText(path, contents, encoding); }
    }
}
