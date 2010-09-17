namespace System.DirectoryServices.AccountManagement
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("userProxyFull")]
    public class UserProxyFullPrincipal : UserPrincipal
    {
        public UserProxyFullPrincipal(PrincipalContext context)
            : base(context) { }
        public UserProxyFullPrincipal(PrincipalContext context, string samAccountName, string password, bool enabled)
            : base(context, samAccountName, password, enabled) { }

        public static new UserProxyFullPrincipal FindByIdentity(PrincipalContext context, string identityValue) { return (UserProxyFullPrincipal)FindByIdentityWithType(context, typeof(UserProxyFullPrincipal), identityValue); }
        public static new UserProxyFullPrincipal FindByIdentity(PrincipalContext context, IdentityType identityType, string identityValue) { return (UserProxyFullPrincipal)FindByIdentityWithType(context, typeof(UserProxyFullPrincipal), identityType, identityValue); }

        [DirectoryProperty("objectSid")]
        public string ObjectSid
        {
            get
            {
                var values = ExtensionGet("objectSid");
                return (values != null ? values[0].ToString() : null);
            }
            set { ExtensionSet("objectSid", value); }
        }
    }
}
