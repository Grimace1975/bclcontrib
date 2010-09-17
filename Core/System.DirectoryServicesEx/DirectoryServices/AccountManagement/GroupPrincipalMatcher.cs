using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public class GroupPrincipalMatcher : IPrincipalMatcher
    {
        public Func<Principal, bool> Determiner
        {
            get { return (g => g.StructuralObjectClass == "group"); }
        }

        public IEnumerable<Principal> MakeQueryFilters(PrincipalContext context)
        {
            yield return new GroupPrincipal(context);
        }
    }
}
