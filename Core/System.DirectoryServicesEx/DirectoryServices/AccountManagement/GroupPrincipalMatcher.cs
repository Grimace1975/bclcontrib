using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public class GroupPrincipalMatcher : IPrincipalMatcher
    {
        public Func<Principal, bool> IsStructuralObjectClass
        {
            get { return (g => g.StructuralObjectClass == "group"); }
        }

        public Func<DirectoryEntry, bool> IsSchemaClassName
        {
            get { return (g => g.SchemaClassName == "group"); }
        }

        public IEnumerable<Principal> GetQueryFilters(PrincipalContext context)
        {
            yield return new GroupPrincipal(context);
        }

        public IEnumerable<string> GetQueryFilters()
        {
            yield return "(&(objectCategory=group)(objectClass=group){0})";
        }

        public IEnumerable<Type> GetPrincipalTypes()
        {
            yield return typeof(GroupPrincipal);
        }
    }
}
