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
//using Tamir.SharpSsh.jsch;
//using System;
//using System.Net;
//namespace System.Interop.Core.Net
//{
//    /// <summary>
//    /// SftpClient
//    /// </summary>
//    public class SftpClient : FtpClient
//    {
//        #region Class Types
//        /// <summary>
//        /// ProgressMonitor
//        /// </summary>
//        public class ProgressMonitor : SftpProgressMonitor
//        {
//            /// <summary>
//            /// Counts the specified count.
//            /// </summary>
//            /// <param name="count">The count.</param>
//            /// <returns></returns>
//            public override bool count(long count)
//            {
//                return true;
//            }

//            /// <summary>
//            /// Ends this instance.
//            /// </summary>
//            public override void end()
//            {
//            }

//            /// <summary>
//            /// Inits the specified op.
//            /// </summary>
//            /// <param name="op">The op.</param>
//            /// <param name="src">The SRC.</param>
//            /// <param name="dest">The dest.</param>
//            /// <param name="max">The max.</param>
//            public override void init(int op, string src, string dest, long max)
//            {
//            }
//        }
//        #endregion Class Types

//        /// <summary>
//        /// Initializes a new instance of the <see cref="SftpClient"/> class.
//        /// </summary>
//        /// <param name="remoteHost">The remote host.</param>
//        /// <param name="userId">The user id.</param>
//        /// <param name="password">The password.</param>
//        public SftpClient(string remoteHost, string userId, string password)
//            : base(remoteHost, userId, password)
//        {
//        }

//        /// <summary>
//        /// Uploads the file.
//        /// </summary>
//        /// <param name="localFile">The local file.</param>
//        /// <param name="remoteFile">The remote file.</param>
//        public override bool TryPut(string localFile, string remoteFile)
//        {
//            return TryExecute(delegate(ChannelSftp sftp)
//            {
//                SftpProgressMonitor monitor = new ProgressMonitor();
//                sftp.put(localFile, remoteFile, monitor, 0);
//            });
//        }

//        /// <summary>
//        /// Downloads the file.
//        /// </summary>
//        /// <param name="remoteFile">The remote file.</param>
//        /// <param name="localFile">The local file.</param>
//        /// <returns></returns>
//        public override bool TryGet(string remoteFile, string localFile)
//        {
//            return TryExecute(delegate(ChannelSftp sftp)
//            {
//                SftpProgressMonitor monitor = new ProgressMonitor();
//                sftp.get(remoteFile, localFile, monitor, 0);
//            });
//        }

//        /// <summary>
//        /// Deletes the file.
//        /// </summary>
//        /// <param name="remotePath">The remote path.</param>
//        public override bool TryDelete(string remoteFile)
//        {
//            return TryExecute(delegate(ChannelSftp sftp)
//            {
//                SftpProgressMonitor monitor = new ProgressMonitor();
//                sftp.rm(remoteFile);
//            });
//        }

//        /// <summary>
//        /// Existses the specified remote file.
//        /// </summary>
//        /// <param name="remoteFile">The remote file.</param>
//        /// <returns></returns>
//        public override bool Exists(string remoteFile)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Executes this instance.
//        /// </summary>
//        private bool TryExecute(System.Action<ChannelSftp> action)
//        {
//            //+ session
//            Session session = null;
//            try
//            {
//                session = new Tamir.SharpSsh.jsch.JSch().getSession(Credentials.UserName, RemoteHost, 0x16);
//                session.setPassword(Credentials.Password);
//                System.Collections.Hashtable foo2 = new System.Collections.Hashtable();
//                foo2.Add("StrictHostKeyChecking", "no");
//                session.setConfig(foo2);
//                session.connect();
//                //+ ftp
//                ChannelSftp sftp = null;
//                try
//                {
//                    sftp = (ChannelSftp)session.openChannel("sftp");
//                    sftp.connect();
//                    action(sftp);
//                    return true;
//                }
//                catch (SftpException ex)
//                {
//                    System.Console.WriteLine(ex.message);
//                    return false;
//                }
//                finally
//                {
//                    sftp.disconnect();
//                }
//            }
//            catch (System.Net.Sockets.SocketException)
//            {
//                return false;
//            }
//            finally
//            {
//                if (session != null)
//                {
//                    session.disconnect();
//                }
//            }
//        }
//    }
//}
