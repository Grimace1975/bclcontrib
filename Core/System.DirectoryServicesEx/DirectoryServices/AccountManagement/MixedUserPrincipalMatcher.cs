using System.Collections.Generic;
using System.Text;
namespace System.DirectoryServices.AccountManagement
{
    public class MixedUserPrincipalMatcher : IPrincipalMatcher
    {
        private MixedUserPrincipalTypes _types;

        public MixedUserPrincipalMatcher()
            : this(MixedUserPrincipalTypes.All) { }
        public MixedUserPrincipalMatcher(MixedUserPrincipalTypes types)
        {
            _types = types;
        }

        public Func<Principal, bool> IsStructuralObjectClass
        {
            get { return (u => u.StructuralObjectClass.IndexOf("user") > -1); }
        }

        public Func<DirectoryEntry, bool> IsSchemaClassName
        {
            get { return (u => u.SchemaClassName.IndexOf("user") > -1); }
        }

        public IEnumerable<Principal> GetQueryFilters(PrincipalContext context)
        {
            if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                yield return new UserPrincipal(context);
            if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                yield return new UserProxyPrincipal(context);
            if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                yield return new UserProxyFullPrincipal(context);
        }

        public IEnumerable<string> GetQueryFilters()
        {
            if ((_types & MixedUserPrincipalTypes.UseWildcard) == MixedUserPrincipalTypes.UseWildcard)
            {
                var b = new StringBuilder();
                if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                    b.Append("(&(objectCategory=user)(objectClass=user))");
                if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                    b.Append("(objectClass=userProxy)");
                if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                    b.Append("(objectClass=userProxyFull)");
                if (b.Length > 0)
                    yield return "(&(|" + b.ToString() + "){0})";
            }
            else
            {
                if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                    yield return "(&(objectCategory=user)(objectClass=user){0})";
                if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                    yield return "(&(objectClass=userProxy){0})";
                if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                    yield return "(&(objectClass=userProxyFull){0})";
            }
        }
        public IEnumerable<Type> GetPrincipalTypes()
        {
            if ((_types & MixedUserPrincipalTypes.UserPrincipal) == MixedUserPrincipalTypes.UserPrincipal)
                yield return typeof(UserPrincipal);
            if ((_types & MixedUserPrincipalTypes.UserProxyPrincipal) == MixedUserPrincipalTypes.UserProxyPrincipal)
                yield return typeof(UserProxyPrincipal);
            if ((_types & MixedUserPrincipalTypes.UserProxyFullPrincipal) == MixedUserPrincipalTypes.UserProxyFullPrincipal)
                yield return typeof(UserProxyFullPrincipal);
        }
    }
}
