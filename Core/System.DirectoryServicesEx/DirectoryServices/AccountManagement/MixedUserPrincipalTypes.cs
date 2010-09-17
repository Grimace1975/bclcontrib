using System.Collections.Generic;
namespace System.DirectoryServices.AccountManagement
{
    [Flags]
    public enum MixedUserPrincipalTypes
    {
        UserPrincipal = 0x1,
        UserProxyPrincipal = 0x2,
        UserProxyFullPrincipal = 0x4,
        All = 0xFF,
    }
}
