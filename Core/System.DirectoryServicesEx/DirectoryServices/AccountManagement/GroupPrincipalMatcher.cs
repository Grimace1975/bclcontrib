using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public class GroupPrincipalMatcher : IPrincipalMatcher
    {
        public Func<Principal, bool> Determiner
        {
            get { return (g => g.StructuralObjectClass == "group"); }
        }

        public IEnumerable<Principal> GetQueryFilters(PrincipalContext context)
        {
            yield return new GroupPrincipal(context);
        }

        public IEnumerable<Type> GetPrincipalTypes()
        {
            yield return typeof(GroupPrincipal);
        }
    }
}
