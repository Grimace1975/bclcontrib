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
namespace System.Web.UI
{
    /// <summary>
    /// IClientScriptManager
    /// </summary>
    public interface IClientScriptManager
    {
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
    }

    /// <summary>
    /// ClientScriptManagerEx
    /// </summary>
    public class ClientScriptManagerEx : IClientScriptManager
    {
        private static readonly MethodInfo s_getRepositoryMethodInfo = typeof(ClientScriptManagerEx).GetGenericMethod("GetRepository");

        public ClientScriptManagerEx() { }

        protected class Repositories<TShard>
        {
            public static IClientScriptRepository Repository = new ClientScriptRepository();
        }

        public void EnsureItem<TShard>(string id, Func<ClientScriptItemBase> item) { Repositories<IClientScriptManager>.Repository.EnsureItem(id, item); }
        public void AddRange(ClientScriptItemBase item) { Repositories<IClientScriptManager>.Repository.AddRange(item); }
        public void AddRange(string literal) { Repositories<IClientScriptManager>.Repository.AddRange(literal); }
        public void AddRange(params object[] items) { Repositories<IClientScriptManager>.Repository.AddRange(items); }
        public void AddRange<TShard>(ClientScriptItemBase item) { Repositories<TShard>.Repository.AddRange(item); }
        public void AddRange<TShard>(string literal) { Repositories<TShard>.Repository.AddRange(literal); }
        public void AddRange<TShard>(params object[] items) { Repositories<TShard>.Repository.AddRange(items); }

        public IClientScriptRepository GetRepository<TShard>()
        {
            return Repositories<TShard>.Repository;
        }

        public IClientScriptRepository GetRepository(Type shard) { return (IClientScriptRepository)s_getRepositoryMethodInfo.MakeGenericMethod(shard).Invoke(this, null); }

        public void SetRepository<TShard>(IClientScriptRepository repository)
        {
            Repositories<TShard>.Repository = repository;
        }
    }
}
