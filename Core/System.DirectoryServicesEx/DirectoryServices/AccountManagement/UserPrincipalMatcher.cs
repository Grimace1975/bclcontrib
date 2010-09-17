using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public class UserPrincipalMatcher : IPrincipalMatcher
    {
        public Func<Principal, bool> Determiner
        {
            get { return (u => u.StructuralObjectClass == "user"); }
        }

        public IEnumerable<Principal> GetQueryFilters(PrincipalContext context)
        {
            yield return new UserPrincipal(context);
        }

        public IEnumerable<Type> GetPrincipalTypes()
        {
            yield return typeof(UserPrincipal);
        }
    }
}
