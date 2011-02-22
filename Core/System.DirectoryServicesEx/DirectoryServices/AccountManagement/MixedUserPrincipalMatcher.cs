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
using System.Text;
namespace System.DirectoryServices.AccountManagement
{
    public class MixedUserPrincipalMatcher : IPrincipalMatcher
    {
        private MixedUserPrincipalTypes _types;

        public MixedUserPrincipalMatcher()
            : this(MixedUserPrincipalTypes.Normal_) { }
        public MixedUserPrincipalMatcher(MixedUserPrincipalTypes types)
        {
            _types = types;
        }

        public Func<Principal, bool> IsStructuralObjectClass
        {
            get
            {
                return (u => (((_types & MixedUserPrincipalTypes.OfUserClass_) == MixedUserPrincipalTypes.OfUserClass_) && (u.StructuralObjectClass.IndexOf("user") > -1))
                    || (((_types & MixedUserPrincipalTypes.InetOrgPersonPrincipal) == MixedUserPrincipalTypes.InetOrgPersonPrincipal) && (u.StructuralObjectClass == "inetOrgPerson")));
            }
        }

        public Func<DirectoryEntry, bool> IsSchemaClassName
        {

            get
            {
                return (u => (((_types & MixedUserPrincipalTypes.OfUserClass_) == MixedUserPrincipalTypes.OfUserClass_) && (u.SchemaClassName.IndexOf("user") > -1))
                    || (((_types & MixedUserPrincipalTypes.InetOrgPersonPrincipal) == MixedUserPrincipalTypes.InetOrgPersonPrincipal) && (u.SchemaClassName == "inetOrgPerson")));
            }
        }

        public IEnumerable<Principal> GetQueryFilters(PrincipalContext context)
        {
            if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                yield return new UserPrincipal(context);
            if ((_types & MixedUserPrincipalTypes.UserPrincipalEx) == MixedUserPrincipalTypes.UserPrincipalEx)
                yield return new UserPrincipalEx(context);
            if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                yield return new UserProxyPrincipal(context);
            if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                yield return new UserProxyFullPrincipal(context);
            if ((_types & MixedUserPrincipalTypes.InetOrgPersonPrincipal) == MixedUserPrincipalTypes.InetOrgPersonPrincipal)
                yield return new InetOrgPersonPrincipal(context);
        }

        public IEnumerable<string> GetQueryFilters()
        {
            if ((_types & MixedUserPrincipalTypes.UseWildcard) == MixedUserPrincipalTypes.UseWildcard)
            {
                var b = new StringBuilder();
                if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                    b.Append("(&(objectCategory=user)(objectClass=user))");
                if ((_types & MixedUserPrincipalTypes.UserPrincipalEx) == MixedUserPrincipalTypes.UserPrincipalEx)
                    b.Append("(objectClass=user)");
                if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                    b.Append("(objectClass=userProxy)");
                if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                    b.Append("(objectClass=userProxyFull)");
                if ((_types & MixedUserPrincipalTypes.InetOrgPersonPrincipal) == MixedUserPrincipalTypes.InetOrgPersonPrincipal)
                    b.Append("(objectClass=inetOrgPerson)");
                if (b.Length > 0)
                    yield return "(&(|" + b.ToString() + "){0})";
            }
            else
            {
                if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                    yield return "(&(objectCategory=user)(objectClass=user){0})";
                if ((_types & MixedUserPrincipalTypes.UserPrincipalEx) == MixedUserPrincipalTypes.UserPrincipalEx)
                    yield return "(&(objectClass=user){0})";
                if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                    yield return "(&(objectClass=userProxy){0})";
                if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                    yield return "(&(objectClass=userProxyFull){0})";
                if ((_types & MixedUserPrincipalTypes.InetOrgPersonPrincipal) == MixedUserPrincipalTypes.InetOrgPersonPrincipal)
                    yield return "(&(objectClass=inetOrgPerson){0})";

            }
        }
        public IEnumerable<Type> GetPrincipalTypes()
        {
            if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                yield return typeof(UserPrincipal);
            if ((_types & MixedUserPrincipalTypes.UserPrincipalEx) == MixedUserPrincipalTypes.UserPrincipalEx)
                yield return typeof(UserPrincipalEx);
            if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                yield return typeof(UserProxyPrincipal);
            if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                yield return typeof(UserProxyFullPrincipal);
            if ((_types & MixedUserPrincipalTypes.InetOrgPersonPrincipal) == MixedUserPrincipalTypes.InetOrgPersonPrincipal)
                yield return typeof(InetOrgPersonPrincipal);
        }
    }
}
