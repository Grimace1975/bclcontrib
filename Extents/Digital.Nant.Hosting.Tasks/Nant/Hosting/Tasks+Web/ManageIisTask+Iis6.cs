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
using System.Management;
using NAnt.Core;
//[IIS6] http://www.gafvert.info/iis/article/cs_create_website_iis6.htm
namespace Digital.Nant.Hosting.Tasks
{
	public partial class ManageIisTask
	{
		public class Iis6 : IIis
		{
			public Project Project { get; set; }

			public void CreateSite(IisContext iisContext)
			{
				Project.Log(Level.Info, "Creating site: " + iisContext.SiteId);
				var connectionOptions = new ConnectionOptions { }; //Username = "webAdministrator", Password = "adminpwd";
				var path = new ManagementPath { Server = ".", NamespacePath = @"root\MicrosoftIISv2", RelativePath = "IIsWebService='W3SVC'" };
				var scope = new ManagementScope(path, connectionOptions);
				using (var entity = new ManagementObject(scope, path, null))
				{
					// create a ServerBinding WMI object
					var bindings = CreateInstance(connectionOptions);
					bindings["IP"] = "";
					bindings["Hostname"] = iisContext.Domain;
					bindings["Port"] = "80";
					bindings.Put(); // to commit the new instance.
					string sitePath = CreateNewSite(entity, iisContext.RootPath + @"\WebRoot", new[] { bindings }, iisContext.SiteId);
					StartSite(sitePath);
				}
			}

			public void RemoveSite(IisContext iisContext)
			{
				Project.Log(Level.Info, "Removing site: " + iisContext.SiteId);
			}

			private static void StartSite(string sitePath)
			{
				//try
				//{
				var iisSite = new ManagementObject(new ManagementScope(@"root\MicrosoftIISv2"), new ManagementPath(sitePath), null);
				iisSite.InvokeMethod("Start", new object[] { 1 });
				//}
				//catch (Exception ex) { }
			}

			// Create bindings object in WMI repository
			private static ManagementObject CreateInstance(ConnectionOptions connectionOptions)
			{
				var path = new ManagementPath { ClassName = "ServerBinding", NamespacePath = @"root\MicrosoftIISv2" };
				var scope = new ManagementScope(path, connectionOptions);
				scope.Path.Server = "";
				return new ManagementClass(scope, path, null).CreateInstance();
			}

			// Create new site
			private static string CreateNewSite(ManagementObject entity, string pathOfRootVirtualDir, ManagementBaseObject[] serverBindings, string serverComment) //, int serverId
			{
				var parameters = entity.GetMethodParameters("CreateNewSite");
				parameters["PathOfRootVirtualDir"] = pathOfRootVirtualDir;
				parameters["ServerBindings"] = serverBindings;
				parameters["ServerComment"] = serverComment;
				//parameters["ServerId"] = serverId; // Let the system create an ID (calculated hash of ManagementBaseObject outP = nac.InvokeMethod("CreateNewSite", inP, null);
				var newSite = entity.InvokeMethod("CreateNewSite", parameters, null);
				return Convert.ToString(newSite.Properties["ReturnValue"].Value); //IIsWebServer='W3SVC/1273343373'
			}
		}
	}
}


