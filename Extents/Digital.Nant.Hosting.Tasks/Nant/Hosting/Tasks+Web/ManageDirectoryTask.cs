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
using System.IO;
using System.Patterns.ReleaseManagement;
namespace Digital.Nant.Hosting.Tasks
{
	[TaskName("manageDirectory")]
	public partial class ManageDirectoryTask : Task
	{
        [TaskAttribute("deploymentEnvironment", Required = true), StringValidator(AllowEmpty = false)]
        public DeploymentEnvironment DeploymentEnvironment { get; set; }

		[TaskAttribute("applicationId", Required = true), StringValidator(AllowEmpty = false)]
		public string ApplicationId { get; set; }

		[TaskAttribute("remove")]
		public bool Remove { get; set; }

		private string ApplicationPath { get; set; }

		private decimal LayoutVersion { get; set; }

		protected override void ExecuteTask()
		{
			LayoutVersion = 2M;
			ApplicationPath = (!Project.Properties.Contains("applicationPath") ? @"C:\_APPLICATION2" : Project.Properties["applicationPath"]);
			if (!Remove)
			{
				CreateDirectory();
				SetAcls();
			}
			else
				RemoveDirectory();
		}

		private void CreateDirectory()
		{
			Project.Log(Level.Info, "Creating directory: " + ApplicationId);
			string path = ApplicationPath + @"\" + ApplicationId;
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			if (!Directory.Exists(path + @"\_Secure"))
				Directory.CreateDirectory(path + @"\_Secure");
			if (!Directory.Exists(path + @"\_Virtual"))
				Directory.CreateDirectory(path + @"\_Virtual");
			string resourceName = (LayoutVersion == 2M ? "Resource_" : "_FileLibrary");
			if (!Directory.Exists(path + @"\_Virtual\" + resourceName))
				Directory.CreateDirectory(path + @"\_Virtual\" + resourceName);
			if (!Directory.Exists(path + @"\WebRoot"))
				Directory.CreateDirectory(path + @"\WebRoot");
		}

		private void SetAcls() { }

		private void RemoveDirectory()
		{
			Project.Log(Level.Info, "Removing directory: " + ApplicationId);
		}
	}
}
