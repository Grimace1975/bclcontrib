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
using System;
using System.Patterns.ReleaseManagement;
namespace Digital.Nant.Hosting.Tasks
{
	[TaskName("manageScheduledTasks")]
	public partial class ManageScheduledTasksTask : Task
	{
        [TaskAttribute("deploymentEnvironment", Required = true), StringValidator(AllowEmpty = false)]
        public DeploymentEnvironment DeploymentEnvironment { get; set; }

		[TaskAttribute("applicationId", Required = true), StringValidator(AllowEmpty = false)]
		public string ApplicationId { get; set; }

		[TaskAttribute("remove")]
		public bool Remove { get; set; }

		protected override void ExecuteTask()
		{
			if (!Remove)
				InstallSchTasks();
			else
				RemoveWinTasks();
		}

		private void InstallSchTasks()
		{
			Project.Log(Level.Info, "Creating wintasks: " + ApplicationId);
			//Environment.MachineName + @"\" + Stage."UserId"
			//    /create /ru %COMPUTERNAME%\%_USERID% /rp %_PASS_IUSR% /tn "DEG\OperationQueueService\%_ENVIRONMENT%" /sc DAILY /st 00:00 /ri %_RI% /du 0023:59 /k /tr "cscript.exe //B D:\_PROGRAM\_LIBRARY\OperationQueueService.wsf %_ENVIRONMENT%
			Process.Start(new ProcessStartInfo
			{
				FileName = "schtasks.exe",
				Arguments = "",	
			});
		}

		private void RemoveWinTasks()
		{
			Project.Log(Level.Info, "Removing wintasks: " + ApplicationId);
		}
	}
}
