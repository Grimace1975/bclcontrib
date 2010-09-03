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
//
// http://the.earth.li/~sgtatham/putty/0.60/htmldoc/Chapter5.html
// http://tartarus.org/~simon/putty-snapshots/htmldoc/Chapter5.html
//
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
namespace System.Interop.Core.Security
{
    public class SecureCopyInterop
    {
        private const string GetArgumentsXABC = "{0} {1}:{2} {3}"; // pscp [options] [user@]host:source target
        private const string PutArgumentsXABC = "{0} {1} {2}:{3}"; // pscp [options] source [source...] [user@]host:target
        private const string ListArgumentsXAB = "{0} -ls {1}:{2}"; // pscp [options] -ls [user@]host:filespec

        #region Process launcher

        public class ProcessLauncher
        {
            private Thread _outputThread;
            private Thread _errorThread;
            private StreamReader _outputStream;
            private StreamReader _errorStream;

            public ProcessLauncher()
            {
                _outputThread = new Thread(new ThreadStart(() => { OutputString = _outputStream.ReadToEnd(); }));
                _errorThread = new Thread(new ThreadStart(() => { ErrorString = _errorStream.ReadToEnd(); }));
            }

            public string OutputString { get; set; }
            public string ErrorString { get; set; }

            public int Launch(int timeoutInMilliseconds, string executablePath, string arguments, Action<StreamWriter> action)
            {
                var process = Process.Start(new ProcessStartInfo(executablePath)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Arguments = arguments,
                });
                if (process == null)
                    throw new SecureCopyException("Failed to Create Process");
                _outputStream = process.StandardOutput;
                _errorStream = process.StandardError;
                try
                {
                    action(process.StandardInput);
                    // Create two threads to read both output/error streams without creating a deadlock
                    _outputThread.Start();
                    _errorThread.Start();
                    //while (!process.WaitForExit(timeoutInMilliseconds))
                    if (process.WaitForExit(timeoutInMilliseconds))
                    {
                        // Wait for the threads to complete reading output/error (but use a timeout!)
                        if (!_outputThread.Join(timeoutInMilliseconds >> 1))
                            _outputThread.Abort();
                        if (!_errorThread.Join(timeoutInMilliseconds >> 1))
                            _errorThread.Abort();
                        return process.ExitCode;
                    }
                    throw new SecureCopyException("Operation Timed out");
                }
                finally
                {
                    if (!process.HasExited)
                        process.Kill();
                    if (_outputThread.IsAlive)
                        _outputThread.Abort();
                    if (_errorThread.IsAlive)
                        _errorThread.Abort();
                }
            }
        }

        #endregion

        public static bool TryGet(SecureCopySettings settings, string remoteHost, string userId, string remoteFile, string localFile, out Exception ex)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrEmpty(remoteHost))
                throw new ArgumentNullException("remoteHost");
            if (string.IsNullOrEmpty(remoteFile))
                throw new ArgumentNullException("remoteFile");
            if (string.IsNullOrEmpty(localFile))
                throw new ArgumentNullException("localFile");
            //
            if (File.Exists(localFile))
                File.Delete(localFile);
            if (!string.IsNullOrEmpty(userId))
                remoteHost = userId + "@" + remoteHost;
            string executablePath;
            var arguments = string.Format(GetArgumentsXABC, Get(settings, out executablePath), remoteHost, remoteFile, localFile);
            try
            {
                var launcher = new ProcessLauncher();
                launcher.Launch(settings.ProcessTimeoutInMilliseconds, executablePath, arguments, (w) =>
                {
                    // Send passphrase, if any
                    if (!string.IsNullOrEmpty(settings.PrivateKeyPassphase))
                    {
                        w.WriteLine(settings.PrivateKeyPassphase);
                        w.Flush();
                    }
                });
                ex = null;
                return true;
            }
            catch (SecureCopyException e) { ex = e; return false; }
        }

        public static bool TryPut(SecureCopySettings settings, string remoteHost, string userId, string localFiles, string remoteFile, out Exception ex) { return TryPut(settings, remoteHost, userId, new[] { localFiles }, remoteFile, out ex); }
        public static bool TryPut(SecureCopySettings settings, string remoteHost, string userId, string[] localFiles, string remoteFile, out Exception ex)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrEmpty(remoteHost))
                throw new ArgumentNullException("remoteHost");
            if (EnumerableEx.IsNullOrEmptyArray(localFiles))
                throw new ArgumentNullException("localFiles");
            if (string.IsNullOrEmpty(remoteFile))
                throw new ArgumentNullException("remoteFile");
            //
            if (!string.IsNullOrEmpty(userId))
                remoteHost = userId + "@" + remoteHost;
            string executablePath;
            var arguments = string.Format(PutArgumentsXABC, Get(settings, out executablePath), string.Join(" ", localFiles), remoteHost, remoteFile);
            try
            {
                var launcher = new ProcessLauncher();
                launcher.Launch(settings.ProcessTimeoutInMilliseconds, executablePath, arguments, (w) =>
                {
                    // Send passphrase, if any
                    if (!string.IsNullOrEmpty(settings.PrivateKeyPassphase))
                    {
                        w.WriteLine(settings.PrivateKeyPassphase);
                        w.Flush();
                    }
                });
                ex = null;
                return true;
            }
            catch (SecureCopyException e) { ex = e; return false; }
        }

        //public static bool TryList<TItem>(SecureCopySettings settings, string remoteHost, string userId, string fileSpecification, out IEnumerable<TItem> items, out Exception ex)
        //{
        //    string itemsAsText;
        //    if (TryList(settings, remoteHost, userId, fileSpecification, out itemsAsText, out ex))
        //    {
        //        items = null;
        //        return true;
        //    };
        //    items = null;
        //    return false;
        //}
        public static bool TryList(SecureCopySettings settings, string remoteHost, string userId, string fileSpecification, out string items, out Exception ex)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (string.IsNullOrEmpty(remoteHost))
                throw new ArgumentNullException("remoteHost");
            if (string.IsNullOrEmpty(fileSpecification))
                throw new ArgumentNullException("fileSpecification");
            //
            if (!string.IsNullOrEmpty(userId))
                remoteHost = userId + "@" + remoteHost;
            string executablePath;
            var arguments = string.Format(ListArgumentsXAB, Get(settings, out executablePath), remoteHost, fileSpecification);
            try
            {
                var launcher = new ProcessLauncher();
                launcher.Launch(settings.ProcessTimeoutInMilliseconds, executablePath, arguments, (w) =>
                {
                    // Send passphrase, if any
                    if (!string.IsNullOrEmpty(settings.PrivateKeyPassphase))
                    {
                        w.WriteLine(settings.PrivateKeyPassphase);
                        w.Flush();
                    }
                });
                items = launcher.OutputString;
                ex = null;
                return true;
            }
            catch (SecureCopyException e) { items = null; ex = e; return false; }
        }

        private static string Get(SecureCopySettings settings, out string executablePath)
        {
            var options = settings.Options;
            var bits = (SecureCopySettingsOptions.ForceSshProtocol1 | SecureCopySettingsOptions.ForceSshProtocol2);
            if ((options & bits) == bits)
                throw new ArgumentException("settings", "ForceSshProtocol1 and ForceSshProtocol2 are mutualy exclusive");
            bits = (SecureCopySettingsOptions.ForceIPv4 | SecureCopySettingsOptions.ForceIPv6);
            if ((options & bits) == bits)
                throw new ArgumentException("settings", "ForceIPv4 and ForceIPv6 are mutualy exclusive");
            // build executablePath
            executablePath = settings.PuTtyPath.EnsureEndsWith("\\") + "pscp.exe";
            if (!File.Exists(executablePath))
                throw new InvalidOperationException(string.Format("'pscp.exe' not found at '{0}'.", executablePath));
            // build arguments
            var b = new StringBuilder();
            if ((options & SecureCopySettingsOptions.PreserveFileAttributes) == SecureCopySettingsOptions.PreserveFileAttributes)
                b.Append("-p ");
            if ((options & SecureCopySettingsOptions.Recursively) == SecureCopySettingsOptions.Recursively)
                b.Append("-r ");
            if (!string.IsNullOrEmpty(settings.SessionName))
                b.AppendFormat("-load \"{0}\" ", settings.SessionName);
            if (settings.Port.HasValue)
                b.AppendFormat("-P {0} ", settings.Port);
            if (!string.IsNullOrEmpty(settings.UserId))
                b.AppendFormat("-l \"{0}\" ", settings.UserId);
            if (!string.IsNullOrEmpty(settings.Password))
                b.AppendFormat("-pw \"{0}\" ", settings.Password);
            if ((options & SecureCopySettingsOptions.ForceSshProtocol1) == SecureCopySettingsOptions.ForceSshProtocol1)
                b.Append("-1 ");
            if ((options & SecureCopySettingsOptions.ForceSshProtocol2) == SecureCopySettingsOptions.ForceSshProtocol2)
                b.Append("-2 ");
            if ((options & SecureCopySettingsOptions.ForceIPv4) == SecureCopySettingsOptions.ForceIPv4)
                b.Append("-4 ");
            if ((options & SecureCopySettingsOptions.ForceIPv6) == SecureCopySettingsOptions.ForceIPv6)
                b.Append("-6 ");
            if ((options & SecureCopySettingsOptions.EnableCompression) == SecureCopySettingsOptions.EnableCompression)
                b.Append("-C ");
            if (!string.IsNullOrEmpty(settings.PrivateKeyPath))
                b.AppendFormat("-i \"{0}\" ", settings.PrivateKeyPath);
            if ((options & SecureCopySettingsOptions.DisableAgent) == SecureCopySettingsOptions.DisableAgent)
                b.Append("-noagent ");
            if ((options & SecureCopySettingsOptions.EnableAgent) == SecureCopySettingsOptions.EnableAgent)
                b.Append("-agent ");
            if ((options & SecureCopySettingsOptions.DisableInteractivePrompts) == SecureCopySettingsOptions.DisableInteractivePrompts)
                b.Append("-batch ");
            if ((options & SecureCopySettingsOptions.Unsafe) == SecureCopySettingsOptions.Unsafe)
                b.Append("-unsafe ");
            if ((options & SecureCopySettingsOptions.ForceSftp) == SecureCopySettingsOptions.ForceSftp)
                b.Append("-sftp ");
            if ((options & SecureCopySettingsOptions.ForceScp) == SecureCopySettingsOptions.ForceScp)
                b.Append("-scp ");
            if (b.Length > 0)
                b.Length--;
            return b.ToString();
        }
    }
}