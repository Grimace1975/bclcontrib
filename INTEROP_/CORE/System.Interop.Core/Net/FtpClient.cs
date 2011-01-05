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
namespace System.Interop.Core.Net
{
    /// <summary>
    /// FtpClient
    /// </summary>
    public class FtpClient : FileTransferClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpClient"/> class.
        /// </summary>
        /// <param name="remoteHost">The remote host.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="password">The password.</param>
        public FtpClient(string remoteHost, string userId, string password)
        {
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

        /// <summary>
        /// Existses the specified remote file.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public override bool Exists(string remoteFile)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + RemoteHost + "/" + remoteFile));
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = Credentials;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            try
            {
                using (var response = (FtpWebResponse)request.GetResponse()) { };
                return true;
            }
            catch (WebException) { return false; }
        }

        /// <summary>
        /// Tries the delete.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public override bool TryDelete(string remoteFile, out Exception ex)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + RemoteHost + "/" + remoteFile));
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = Credentials;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            try
            {
                using (var response = (FtpWebResponse)request.GetResponse()) { };
                ex = null;
                return true;
            }
            catch (WebException e) { ex = e; return false; }
        }

        /// <summary>
        /// Tries the get.
        /// </summary>
        /// <param name="remoteFile">The remote file.</param>
        /// <param name="localFile">The local file.</param>
        /// <returns></returns>
        public override bool TryGet(string remoteFile, string localFile, out Exception ex)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + RemoteHost + "/" + remoteFile));
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = Credentials;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            if (File.Exists(localFile))
                File.Delete(localFile);
            try
            {
                using (var response = (FtpWebResponse)request.GetResponse())
                using (Stream r = response.GetResponseStream(), w = File.Open(localFile, FileMode.CreateNew))
                {
                    byte[] b = new byte[32768];
                    int read = 0;
                    while ((read = r.Read(b, 0, b.Length)) > 0)
                        w.Write(b, 0, b.Length);
                }
                ex = null;
                return true;
            }
            catch (WebException e) { ex = e; return false; }
        }

        /// <summary>
        /// Tries the put.
        /// </summary>
        /// <param name="localFile">The local file.</param>
        /// <param name="remoteFile">The remote file.</param>
        /// <returns></returns>
        public override bool TryPut(string localFile, string remoteFile, out Exception ex)
        {
            var request = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + RemoteHost + "/" + remoteFile));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = Credentials;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            try
            {
                using (Stream w = request.GetRequestStream(), r = File.Open(localFile, FileMode.Open))
                {
                    byte[] b = new byte[32768];
                    int read = 0;
                    while ((read = r.Read(b, 0, b.Length)) > 0)
                        w.Write(b, 0, b.Length);
                }
                ex = null;
                return true;
            }
            catch (WebException e) { ex = e; return false; }
        }
    }
}
