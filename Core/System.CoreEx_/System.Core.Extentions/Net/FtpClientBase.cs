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
namespace System.Net
{
    /// <summary>
    /// FtpClientBase
    /// </summary>
    public abstract class FtpClientBase
    {
        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public abstract NetworkCredential Credentials { get; protected set; }
        /// <summary>
        /// Gets the remote host.
        /// </summary>
        /// <value>The remote host.</value>
        public abstract string RemoteHost { get; protected set; }
        /// <summary>
        /// Existses the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public abstract bool Exists(string remoteFile);
        /// <summary>
        /// Tries the delete.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public abstract bool TryDelete(string remoteFile);
        /// <summary>
        /// Tries the get.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        /// <returns></returns>
        public abstract bool TryGet(string remoteFile, string localFile);
        /// <summary>
        /// Tries the put.
        /// </summary>
        /// <param name="localFile">The local file.</param>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public abstract bool TryPut(string localFile, string remoteFile);
    }
}
