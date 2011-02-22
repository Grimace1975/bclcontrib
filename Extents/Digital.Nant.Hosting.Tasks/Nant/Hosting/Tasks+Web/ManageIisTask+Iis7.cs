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
using System;
using Microsoft.Web.Administration;
using NAnt.Core;
//[IIS7] http://blogs.msdn.com/carlosag/archive/2006/04/17/MicrosoftWebAdministration.aspx
namespace Digital.Nant.Hosting.Tasks
{
	public partial class ManageIisTask
	{
		public class Iis7 : IIis
		{
			public Project Project { get; set; }

			public void CreateSite(IisContext iisContext)
			{
				Project.Log(Level.Info, "Creating site: " + iisContext.SiteId);
				using (var iisManager = new ServerManager())
				{
					// application pools
					var applicationPools = iisManager.ApplicationPools;
					var applicationPool = applicationPools[iisContext.ApplicationPoolId];
					if (applicationPool == null)
						applicationPool = applicationPools.Add(iisContext.ApplicationPoolId);
					applicationPool.AutoStart = true;
					var processModel = applicationPool.ProcessModel;
					processModel.IdentityType = ProcessModelIdentityType.SpecificUser;
					processModel.UserName = iisContext.ApplicationPoolUserId;
					processModel.Password = iisContext.ApplicationPoolPassword;
					// sites
					var primaryBinding = new IisContext.Binding { Information = "*:80:" + iisContext.Domain, Protocol = "http" };
					var sites = iisManager.Sites;
					var site = sites[iisContext.SiteId];
					if (site == null)
						site = sites.Add(iisContext.SiteId, primaryBinding.Protocol, primaryBinding.Information, iisContext.RootPath + @"\WebRoot");
					site.ApplicationDefaults.ApplicationPoolName = iisContext.ApplicationPoolId;
					// bindings { remove all but primary, add new bindings }
					var bindings = site.Bindings;
					bindings.Clear();
					bindings.Add(primaryBinding.Information, primaryBinding.Protocol);
					if (iisContext.Bindings != null)
						foreach (var binding in iisContext.Bindings)
							bindings.Add(binding.Information, binding.Protocol);
					// applications
					string resourceName = (iisContext.LayoutVersion == 2M ? "Resource_" : "_FileLibrary");
					var siteApplications = site.Applications;
					var resourceApplication = siteApplications["/" + resourceName];
					if (resourceApplication == null)
						resourceApplication = siteApplications.Add("/" + resourceName, iisContext.RootPath + @"\_Virtual\" + resourceName);
					resourceApplication.VirtualDirectoryDefaults.SetAttributeValue("allowSubDirConfig", false);
					//
					iisManager.CommitChanges();
					//applicationPool.Start();
					//site.Start();
				}
			}

			public void RemoveSite(IisContext iisContext)
			{
				Project.Log(Level.Info, "Removing site: " + iisContext.SiteId);
				using (var iisManager = new ServerManager())
				{
					// application pools
					var applicationPools = iisManager.ApplicationPools;
					var applicationPool = applicationPools[iisContext.ApplicationPoolId];
					if (applicationPool != null)
						applicationPools.Remove(applicationPool);
					// sites
					var sites = iisManager.Sites;
					var site = sites[iisContext.SiteId];
					if (site != null)
						sites.Remove(site);
					//
					iisManager.CommitChanges();
				}
			}
			//    var config = serverManager.GetWebConfiguration(null);
			//    var appSettingsSection = config.GetSection("appSettings");
			//    var appSettingsCollection = appSettingsSection.GetCollection();
		}
	}
}
