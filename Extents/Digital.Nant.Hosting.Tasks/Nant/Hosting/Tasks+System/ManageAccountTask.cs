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
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Diagnostics;
using System.DirectoryServices;
using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Patterns.ReleaseManagement;
using System.Collections;
namespace Digital.Nant.Hosting.Tasks
{
    [TaskName("manageAccount")]
    public class ManageAccountTask : Task
    {
        [TaskAttribute("deploymentEnvironment", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public DeploymentEnvironment DeploymentEnvironment { get; set; }

        [TaskAttribute("applicationId", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ApplicationId { get; set; }

        [TaskAttribute("remove")]
        public bool Remove { get; set; }

        protected override void ExecuteTask()
        {
            // admin user
            string userId = DeploymentEnvironment.CreateAccountUserId(ApplicationId, null);
            if (!Remove)
                InstallUser(userId, DeploymentEnvironment.CreateAccountPassword(ApplicationId, null));
            else
                RemoveUser(userId);
            // iusr user
            userId = DeploymentEnvironment.CreateAccountUserId(ApplicationId, "IUSR");
            if (!Remove)
                InstallUser(userId, DeploymentEnvironment.CreateAccountPassword(ApplicationId, "IUSR"), "IIS_IUSRS");
            else
                RemoveUser(userId);
        }

        private void InstallUser(string userId, string password, params string[] groups)
        {
            //const int ADS_UF_PASSWD_NOTREQD = 0x0020;
            const int ADS_UF_PASSWD_CANT_CHANGE = 0x0040;
            const int ADS_UF_DONT_EXPIRE_PASSWD = 0x10000;
            //const int ADS_UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x0080;
            //const int ADS_UF_PASSWORD_EXPIRED = 0x80000;
            Project.Log(Level.Info, "Creating user: " + userId);
            try
            {
                var directory = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                // add user
                DirectoryEntry userEntry;
                try
                {
                    userEntry = directory.Children.Add(userId, "user");
                    userEntry.Invoke("SetPassword", new object[] { password });
                    userEntry.CommitChanges();
                }
                catch (COMException ex)
                {
                    if ((uint)ex.ErrorCode != 0x800708b0)
                        throw;
                    userEntry = directory.Children.Find(userId, "user");
                }
                // set password not expire
                //userEntry.Properties["userAccountControl"].Value = (int)userEntry.Properties["userAccountControl"].Value | ADS_UF_PASSWD_CANT_CHANGE | ADS_UF_DONT_EXPIRE_PASSWD;
                userEntry.Invoke("Put", new object[] { "userFlags", ADS_UF_PASSWD_CANT_CHANGE | ADS_UF_DONT_EXPIRE_PASSWD });
                userEntry.Invoke("Put", new object[] { "Description", "DEG Autoset" });
                userEntry.CommitChanges();
                // add group
                if (EnumerableEx.IsNullOrEmptyArray(groups))
                    foreach (string group in groups)
                    {
                        var groupEntry = directory.Children.Find(group, "group");
                        if (groupEntry != null)
                            try
                            {
                                groupEntry.Invoke("Add", new object[] { userEntry.Path.ToString() });
                            }
                            catch (TargetInvocationException ex)
                            {
                                var innerException = (ex.InnerException as COMException);
                                if ((innerException == null) || ((uint)innerException.ErrorCode != 0x80070562))
                                    throw;
                            }
                    }
                //Project.Log(Level.Info, "Account created: " + userId);
            }
            catch (Exception ex) { Project.Log(Level.Error, ex.Message); }
        }

        private void RemoveUser(string userId)
        {
            Project.Log(Level.Info, "Removing user: " + userId);
            try
            {
                var directory = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                // remove user
                DirectoryEntry userEntry = null;
                try
                {
                    userEntry = directory.Children.Find(userId, "user");
                }
                catch (COMException ex)
                {
                    if ((uint)ex.ErrorCode == 0x800708ad)
                        return;
                    if (userEntry == null)
                        throw;
                }
                directory.Children.Remove(userEntry);
                //Project.Log(Level.Info, "Account Removed: " + userId);
            }
            catch (Exception ex) { Project.Log(Level.Error, ex.Message); }
        }
    }
}
