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
using System.Collections.Generic;
using System.Reflection;
using System.Web.Handlers;
namespace System.Web.UI
{
    internal delegate string GetWebResourceUrlDelegate(Type type, string resourceName, bool htmlEncoded);

    /// <summary>
    /// IClientScriptManager
    /// </summary>
    public interface IClientScriptManager
    {
        void EnsureItem(string id, Func<ClientScriptItemBase> item);
        void EnsureItem<TShard>(string id, Func<ClientScriptItemBase> item);
        void AddRange(ClientScriptItemBase item);
        void AddRange(string literal);
        void AddRange(params object[] items);
        void AddRange<TShard>(ClientScriptItemBase item);
        void AddRange<TShard>(string literal);
        void AddRange<TShard>(params object[] items);
        IClientScriptRepository GetRepository<TShard>();
        IClientScriptRepository GetRepository(Type shard);
        void SetRepository<TShard>(IClientScriptRepository repository);
        void SetRepository(Type type, IClientScriptRepository repository);
    }

    /// <summary>
    /// ClientScriptManagerEx
    /// </summary>
    public class ClientScriptManagerEx : IClientScriptManager
    {
        private static readonly GetWebResourceUrlDelegate s_getWebResourceUrl = (GetWebResourceUrlDelegate)Delegate.CreateDelegate(typeof(GetWebResourceUrlDelegate), typeof(AssemblyResourceLoader).GetMethod("GetWebResourceUrl", BindingFlags.NonPublic | BindingFlags.Static, null, new[] { typeof(Type), typeof(string), typeof(bool) }, null));
        private static readonly Type s_type = typeof(IClientScriptManager);
        private static readonly Type s_htmlHeadType = typeof(System.Web.UI.HtmlControls.HtmlHead);
        private IClientScriptRepository _defaultRepository = new ClientScriptRepository();
        private IClientScriptRepository _htmlHeadRepository = new ClientScriptRepository();
        private Dictionary<Type, IClientScriptRepository> _repositories;

        public ClientScriptManagerEx() { }

        public void EnsureItem(string id, Func<ClientScriptItemBase> item) { _defaultRepository.EnsureItem(id, item); }
        public void EnsureItem<TShard>(string id, Func<ClientScriptItemBase> item) { GetRepository(typeof(TShard)).EnsureItem(id, item); }
        public void AddRange(ClientScriptItemBase item) { _defaultRepository.AddRange(item); }
        public void AddRange(string literal) { _defaultRepository.AddRange(literal); }
        public void AddRange(params object[] items) { _defaultRepository.AddRange(items); }
        public void AddRange<TShard>(ClientScriptItemBase item) { GetRepository(typeof(TShard)).AddRange(item); }
        public void AddRange<TShard>(string literal) { GetRepository(typeof(TShard)).AddRange(literal); }
        public void AddRange<TShard>(params object[] items) { GetRepository(typeof(TShard)).AddRange(items); }

        public IClientScriptRepository GetRepository<TShard>() { return GetRepository(typeof(TShard)); }
        public IClientScriptRepository GetRepository(Type shard)
        {
            if (shard == s_type)
                return _defaultRepository;
            if (shard == s_htmlHeadType)
                return _htmlHeadRepository;
            // repositories
            if (_repositories == null)
                _repositories = new Dictionary<Type, IClientScriptRepository>();
            IClientScriptRepository repository;
            if (!_repositories.TryGetValue(shard, out repository))
                _repositories.Add(shard, (repository = new ClientScriptRepository()));
            return repository;
        }
        public void SetRepository<TShard>(IClientScriptRepository repository) { SetRepository(typeof(TShard), repository); }
        public void SetRepository(Type shard, IClientScriptRepository repository)
        {
            if (shard == s_type)
                _defaultRepository = repository;
            if (shard == s_htmlHeadType)
                _htmlHeadRepository = repository;
            // repositories
            if (_repositories == null)
                _repositories = new Dictionary<Type, IClientScriptRepository>();
            _repositories[shard] = repository;
        }

        public static string GetWebResourceUrl(Type type, string resourceName) { return GetWebResourceUrl(type, resourceName, false); }
        public static string GetWebResourceUrl(Type type, string resourceName, bool htmlEncoded)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentNullException("resourceName");
            return s_getWebResourceUrl(type, resourceName, htmlEncoded);
        }
    }
}
