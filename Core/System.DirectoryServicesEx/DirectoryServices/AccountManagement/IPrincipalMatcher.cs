using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public interface IPrincipalMatcher : IDirectoryEntryMatcher
    {
        Func<Principal, bool> IsStructuralObjectClass { get; }
        IEnumerable<Principal> GetQueryFilters(PrincipalContext context);
        IEnumerable<Type> GetPrincipalTypes();
    }
}
