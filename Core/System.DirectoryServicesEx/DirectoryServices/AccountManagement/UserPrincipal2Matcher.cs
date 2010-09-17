using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public class UserPrincipal2Matcher : IPrincipalMatcher
    {
        public Func<Principal, bool> Determiner
        {
            get { return (u => u.StructuralObjectClass.IndexOf("user") > -1); }
        }

        public IEnumerable<Principal> MakeQueryFilters(PrincipalContext context)
        {
            yield return new UserPrincipal(context);
            yield return new UserProxyPrincipal(context);
            yield return new UserProxyFullPrincipal(context);
        }
    }
}
