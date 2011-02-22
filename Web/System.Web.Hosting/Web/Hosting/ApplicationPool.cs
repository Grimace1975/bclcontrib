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
using System.DirectoryServices;
namespace System.Web.Hosting
{
    /// <summary>
    /// ApplicationPool
    /// </summary>
    public static class ApplicationPool
    {
        /// <summary>
        /// Attempts to recycle current application pool
        /// </summary>
        /// <returns>
        /// Boolean indicating if application pool was successfully recycled
        /// </returns>
        public static bool RecycleCurrentApplicationPool()
        {
            try
            {
                var appDomain = AppDomain.CurrentDomain;                
                if (IsApplicationRunningOnAppPool(appDomain))
                {
                    RecycleApplicationPool(GetCurrentApplicationPoolId(appDomain));
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Determines whether Application hosted on IIS that supports App Pools, like 6.0 and 7.0
        /// </summary>
        /// <param name="appDomain">The app domain.</param>
        /// <returns>
        /// 	<c>true</c> if [is application running on app pool] [the specified app domain]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsApplicationRunningOnAppPool(AppDomain appDomain)
        {
            // Application is not hosted on IIS && on IIS that doesn't support App Pools, like 5.1
            return ((!appDomain.FriendlyName.StartsWith("/LM/")) && (!DirectoryEntry.Exists("IIS://Localhost/W3SVC/AppPools")));
        }

        public static string GetCurrentApplicationPoolId(AppDomain appDomain)
        {
            string virtualDirPath = appDomain.FriendlyName;
            virtualDirPath = virtualDirPath.Substring(4);
            int index = virtualDirPath.Length + 1;
            index = virtualDirPath.LastIndexOf("-", index - 1, index - 1);
            index = virtualDirPath.LastIndexOf("-", index - 1, index - 1);
            virtualDirPath = "IIS://localhost/" + virtualDirPath.Remove(index);
            var virtualDirEntry = new DirectoryEntry(virtualDirPath);
            return virtualDirEntry.Properties["AppPoolId"].Value.ToString();
        }

        public static void RecycleApplicationPool(string appPoolId)
        {
            string appPoolPath = "IIS://localhost/W3SVC/AppPools/" + appPoolId;
            var appPoolEntry = new DirectoryEntry(appPoolPath);
            appPoolEntry.Invoke("Recycle");
        }
    }
}