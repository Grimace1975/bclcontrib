using System.Collections.Generic;
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

        public Func<Principal, bool> Determiner
        {
            get { return (u => u.StructuralObjectClass.IndexOf("user") > -1); }
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
