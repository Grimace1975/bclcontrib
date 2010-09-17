using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    public interface IPrincipalMatcher
    {
        Func<Principal, bool> Determiner { get; }
        IEnumerable<Principal> GetQueryFilters(PrincipalContext context);
        IEnumerable<Type> GetPrincipalTypes();
    }
}
