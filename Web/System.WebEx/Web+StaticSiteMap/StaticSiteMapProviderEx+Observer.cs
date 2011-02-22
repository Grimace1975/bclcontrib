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
namespace System.Web
{
	public partial class StaticSiteMapProviderEx
	{
		public struct NodeToAdd
		{
			public SiteMapNode Node;
			public SiteMapNode ParentNode;
		}

		public abstract class ObservableBase : IObservable<NodeToAdd>
		{
			public abstract IDisposable Subscribe(IObserver<NodeToAdd> observer);
			public abstract SiteMapNode GetRootNode();
		}

		private class Observer : IObserver<NodeToAdd>
		{
			private StaticSiteMapProviderEx _parent;

			public Observer(StaticSiteMapProviderEx parent)
			{
				_parent = parent;
			}

			public void OnCompleted() { }

			public void OnError(Exception ex)
			{
                throw ex.PrepareForRethrow();
			}

			public void OnNext(NodeToAdd nodeToAdd)
			{
				_parent.AddNode(nodeToAdd.Node, nodeToAdd.ParentNode);
			}
		}
	}

	//public const string DependencyId = "SiteMap";
	//private const string _cacheDependencyName = "__SiteMapCacheDependency";
	// ensureDependency
	//HttpRuntime.Cache.Add("Content", string.Empty, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
	//CacheDependency dependency = new CacheDependency((string[])null, new string[] { "Content" });
	// Use the SQL cache dependency
	//HttpRuntime.Cache.Insert(_cacheDependencyName, new object(), dependency,
	//    Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable,
	//    new CacheItemRemovedCallback(OnSiteMapChanged));
	//// touch dependency key for foreign caches
	//HttpContext.Current.Cache.Insert(DependencyId, DateTime.Now, null,
	//      DateTime.MaxValue, TimeSpan.Zero,
	//      CacheItemPriority.NotRemovable,
	//      null);
	//private void OnSiteMapChanged(string key, object item, CacheItemRemovedReason reason)
	//{
	//    Clear();
	//}
}