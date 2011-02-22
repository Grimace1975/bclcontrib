using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
namespace System.Web
{
	[TestClass]
	public class SiteMapNodeExExtensionsTests
	{
		[TestMethod]
		public void GetVisibleChildNodes_HiddenAndVisibleChildValues_EqualsVisibleNode()
		{
			var provider = new StaticSiteMapProviderEx();
			var hiddenNode = new SiteMapNodeEx(provider, "hidden") { Visible = false };
			var visibleNode = new SiteMapNodeEx(provider, "visible");
			var parentNode = new SiteMapNodeEx(provider, ".") { ChildNodes = new SiteMapNodeCollection(new[] { hiddenNode, visibleNode }) };
			var visibleChildNodes = parentNode.GetVisibleChildNodes();
			Assert.AreEqual(1, visibleChildNodes.Count());
			Assert.AreSame(visibleNode, visibleChildNodes.First());
		}
	}
}
