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
namespace System.Web.UI
{
    public interface IClientScriptRepository
    {
        Dictionary<string, ClientScriptItemBase> Includes { get; }
        Dictionary<string, ClientScriptItemBase> Items { get; }
        void EnsureItem(string id, Func<ClientScriptItemBase> item);
        void InsertItem(string id, ClientScriptItemBase item);
        void AddRange(ClientScriptItemBase item);
        void AddRange(string literal);
        void AddRange(params object[] items);
    }

    public class ClientScriptRepository : IClientScriptRepository
    {
        public ClientScriptRepository()
        {
            Includes = new Dictionary<string, ClientScriptItemBase>();
            Items = new Dictionary<string, ClientScriptItemBase>();
        }

        public Dictionary<string, ClientScriptItemBase> Includes { get; private set; }
        public Dictionary<string, ClientScriptItemBase> Items { get; private set; }

        public void EnsureItem(string id, Func<ClientScriptItemBase> item)
        {
            if ((!Items.ContainsKey(id)) && (!Includes.ContainsKey(id)))
                InsertItem(id, item());
        }

        public void InsertItem(string id, ClientScriptItemBase item)
        {
            var itemAsInclude = (item as IncludeClientScriptItem);
            if (itemAsInclude == null)
                Items[id] = item;
            else
                Includes[id] = itemAsInclude;
        }

        public void AddRange(ClientScriptItemBase item)
        {
            var itemAsInclude = (item as IncludeClientScriptItem);
            if (itemAsInclude == null)
                Items[Items.Count.ToString()] = item;
            else
                Includes[Includes.Count.ToString()] = itemAsInclude;
        }

        public void AddRange(string literal)
        {
            Items[Items.Count.ToString()] = new LiteralClientScriptItem(literal);
        }

        public void AddRange(params object[] items)
        {
            foreach (object item in items)
            {
                if (item == null)
                    continue;
                string itemAsString = (item as string);
                if (itemAsString != null)
                {
                    Items[Items.Count.ToString()] = new LiteralClientScriptItem(itemAsString);
                    continue;
                }
                var itemAsInclude = (item as IncludeClientScriptItem);
                if (itemAsInclude != null)
                {
                    Includes[Items.Count.ToString()] = itemAsInclude;
                    continue;
                }
                var itemAsItem = (item as ClientScriptItemBase);
                if (itemAsItem != null)
                {
                    Items[Items.Count.ToString()] = itemAsItem;
                    continue;
                }
                throw new InvalidOperationException();
            }
        }
    }
}
