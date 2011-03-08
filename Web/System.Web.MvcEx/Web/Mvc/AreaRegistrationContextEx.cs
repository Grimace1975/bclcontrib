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
using System.Web.Routing;
namespace System.Web.Mvc
{
    public static class AreaRegistrationContextEx
    {
        public static void AddRoute(this AreaRegistrationContext context, Route route) { AddRoute(context, null, route); }
        public static void AddRoute(this AreaRegistrationContext context, string name, Route route)
        {
            var dataTokens = route.DataTokens;
            if (dataTokens == null)
                dataTokens = route.DataTokens = new RouteValueDictionary();
            dataTokens["area"] = context.AreaName;
            // disabling the namespace lookup fallback mechanism keeps this areas from accidentally picking up controllers belonging to other areas
            object namespacesAsObject;
            string[] namespaces = (dataTokens.TryGetValue("namespace", out namespacesAsObject) ? (string[])namespacesAsObject : null);
            dataTokens["UseNamespaceFallback"] = ((namespaces == null) || (namespaces.Length == 0));
            if (string.IsNullOrEmpty(name))
                context.Routes.Add(route);
            else
                context.Routes.Add(name, route);
        }
    }
}