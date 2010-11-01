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
using System;
using System.IO;
using System.Linq;
using System.Patterns.ReleaseManagement;
namespace Digital.Nant.Hosting.Tasks
{
	[TaskName("manageIis"), Serializable]
	public partial class ManageIisTask : Task
	{
        [TaskAttribute("deploymentEnvironment", Required = true), StringValidator(AllowEmpty = false)]
        public DeploymentEnvironment DeploymentEnvironment { get; set; }

		[TaskAttribute("applicationId", Required = true), StringValidator(AllowEmpty = false)]
		public string ApplicationId { get; set; }

		[TaskAttribute("remove")]
		public bool Remove { get; set; }

		[BuildElementArray("binding")]
		public Binding[] Bindings { get; set; }

		private string ApplicationPath { get; set; }

		private decimal IisVersion { get; set; }

		private decimal LayoutVersion { get; set; }

		protected override void ExecuteTask()
		{
			LayoutVersion = 2M;
			IisVersion = 7M;
			var iis = (IisVersion != 6M ? (IIis)new Iis7 { Project = Project } : (IIis)new Iis6 { Project = Project });
			ApplicationPath = (!Project.Properties.Contains("applicationPath") ? @"C:\_APPLICATION2" : Project.Properties["applicationPath"]);
			// site
			var bindings = (Bindings != null ? Bindings.Select(c => new IisContext.Binding { Information = c.Information, Protocol = c.Protocol }).ToArray() : null);
			var iisContext = new IisContext
			{
				LayoutVersion = LayoutVersion,
				ApplicationPoolId = ApplicationId,
				ApplicationPoolUserId = DeploymentEnvironment.CreateAccountUserId(ApplicationId, "IUSR"),
				ApplicationPoolPassword = DeploymentEnvironment.CreateAccountPassword(ApplicationId, "IUSR"),
				Bindings = bindings,
				SiteId = ApplicationId,
				Domain = ApplicationId + "." + DeploymentEnvironment.ToShortName() + (DeploymentEnvironment.GetExternalDeployment() ? ".deghosting.com" : ".degdarwin.com"),
				RootPath = ApplicationPath + @"\" + ApplicationId,
			};
			if (!Remove)
				iis.CreateSite(iisContext);
			else
				iis.RemoveSite(iisContext);
		}
	}
}
