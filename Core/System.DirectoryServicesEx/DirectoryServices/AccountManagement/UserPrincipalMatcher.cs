using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public class UserPrincipalMatcher : IPrincipalMatcher
    {
        public Func<Principal, bool> IsStructuralObjectClass
        {
            get { return (u => u.StructuralObjectClass == "user"); }
        }

        public IEnumerable<Principal> GetQueryFilters(PrincipalContext context)
        {
            yield return new UserPrincipal(context);
        }

        public IEnumerable<string> GetQueryFilters()
        {
            yield return "(&(objectCategory=user)(objectClass=user){0})";
        }

        public IEnumerable<Type> GetPrincipalTypes()
        {
            yield return typeof(UserPrincipal);
        }
    }
}
