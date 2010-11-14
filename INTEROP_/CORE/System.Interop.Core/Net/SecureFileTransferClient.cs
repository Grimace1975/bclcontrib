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
using System.Net;
using System.IO;
using System.Interop.Core.Security;
namespace System.Interop.Core.Net
{
    /// <summary>
    /// SecureFileTransferClient
    /// </summary>
    public class SecureFileTransferClient : FileTransferClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpClient"/> class.
        /// </summary>
        /// <param name="remoteHost">The remote host.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="password">The password.</param>
        public SecureFileTransferClient(string puTtyPath, string remoteHost, string userId, string password)
        {
            SecureCopySettings = new SecureCopySettings
            {
                PuTtyPath = puTtyPath,
                UserId = userId,
                Password = password,
            };
            Credentials = new NetworkCredential(userId, password);
            RemoteHost = remoteHost;
        }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        public override NetworkCredential Credentials { get; protected set; }

        /// <summary>
        /// Gets or sets the remote host.
        /// </summary>
        /// <value>The remote host.</value>
        public override string RemoteHost { get; protected set; }

        public SecureCopySettings SecureCopySettings { get; protected set; }

        /// <summary>
        /// Existses the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public override bool Exists(string remoteFile)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Tries the delete.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public override bool TryDelete(string remoteFile, out Exception ex)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Tries the get.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        /// <returns></returns>
        public override bool TryGet(string remoteFile, string localFile, out Exception ex)
        {
            return SecureCopyInterop.TryGet(SecureCopySettings, RemoteHost, null, remoteFile, localFile, out ex);
        }

        /// <summary>
        /// Tries the put.
        /// </summary>
        /// <param name="localFile">The local file.</param>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public override bool TryPut(string localFile, string remoteFile, out Exception ex)
        {
            return SecureCopyInterop.TryPut(SecureCopySettings, RemoteHost, null, new[] { localFile }, remoteFile, out ex);
        }
    }
}
