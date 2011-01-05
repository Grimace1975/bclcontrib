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
    public interface IClientScriptShapeRepository
    {
        Dictionary<string, ClientScriptShapeBase> Includes { get; }
        Dictionary<string, ClientScriptShapeBase> Shapes { get; }
        void EnsureItem(string id, Func<ClientScriptShapeBase> shape);
        void InsertItem(string id, ClientScriptShapeBase shape);
        void AddRange(ClientScriptShapeBase shape);
        void AddRange(string literal);
        void AddRange(params object[] shapes);
    }

    public class ClientScriptShapeRepository : IClientScriptShapeRepository
    {
        public ClientScriptShapeRepository()
        {
            Includes = new Dictionary<string, ClientScriptShapeBase>();
            Shapes = new Dictionary<string, ClientScriptShapeBase>();
        }

        public Dictionary<string, ClientScriptShapeBase> Includes { get; private set; }
        public Dictionary<string, ClientScriptShapeBase> Shapes { get; private set; }

        public void EnsureItem(string id, Func<ClientScriptShapeBase> shape)
        {
            if ((!Shapes.ContainsKey(id)) && (!Includes.ContainsKey(id)))
                InsertItem(id, shape());
        }

        public void InsertItem(string id, ClientScriptShapeBase shape)
        {
            var shapeAsInclude = (shape as IncludeClientScriptItem);
            if (shapeAsInclude == null)
                Shapes[id] = shape;
            else
                Includes[id] = shapeAsInclude;
        }

        public void AddRange(ClientScriptShapeBase shape)
        {
            var shapeAsInclude = (shape as IncludeClientScriptItem);
            if (shapeAsInclude == null)
                Shapes[Shapes.Count.ToString()] = shape;
            else
                Includes[Includes.Count.ToString()] = shapeAsInclude;
        }

        public void AddRange(string literal)
        {
            Shapes[Shapes.Count.ToString()] = new LiteralClientScriptItem(literal);
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
                    Shapes[Shapes.Count.ToString()] = new LiteralClientScriptItem(itemAsString);
                    continue;
                }
                var itemAsInclude = (item as IncludeClientScriptItem);
                if (itemAsInclude != null)
                {
                    Includes[Shapes.Count.ToString()] = itemAsInclude;
                    continue;
                }
                var itemAsItem = (item as ClientScriptShapeBase);
                if (itemAsItem != null)
                {
                    Shapes[Shapes.Count.ToString()] = itemAsItem;
                    continue;
                }
                throw new InvalidOperationException();
            }
        }
    }
}
