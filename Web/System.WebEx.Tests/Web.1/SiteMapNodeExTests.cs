using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Web
{
	[TestClass]
	public class SiteMapNodeExTests
	{
		public class SampleExtent { }

		[TestMethod]
		public void HasExtents()
		{
			var node = new SiteMapNodeEx(new StaticSiteMapProviderEx(), "a");
			Assert.IsFalse(node.HasExtents);
			node.Set<SampleExtent>(new SampleExtent());
			Assert.IsTrue(node.HasExtents);
		}

		[TestMethod]
		public void Set()
		{
			var node = new SiteMapNodeEx(new StaticSiteMapProviderEx(), "a");
			var extent = new SampleExtent();
			node.Set<SampleExtent>(extent);
			Assert.AreSame(extent, node.Get<SampleExtent>());
		}

		[TestMethod]
		public void Get()
		{
			var node = new SiteMapNodeEx(new StaticSiteMapProviderEx(), "a");
			var extent = new SampleExtent();
			node.Set<SampleExtent>(extent);
			Assert.AreSame(extent, node.Get<SampleExtent>());
		}

		[TestMethod]
		public void Clear()
		{
			var node = new SiteMapNodeEx(new StaticSiteMapProviderEx(), "a");
			var extent = new SampleExtent();
			node.Set<SampleExtent>(extent);
			Assert.IsTrue(node.HasExtents);
			node.Clear<SampleExtent>();
			Assert.IsFalse(node.HasExtents);
		}
	}
}
