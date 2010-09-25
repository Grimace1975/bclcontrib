namespace System.DirectoryServices.AccountManagement
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("userProxy")]
    public class UserProxyPrincipal : UserPrincipal
    {
        public UserProxyPrincipal(PrincipalContext context)
            : base(context) { }
        public UserProxyPrincipal(PrincipalContext context, string samAccountName, string password, bool enabled)
            : base(context, samAccountName, password, enabled) { }

        public static new UserProxyPrincipal FindByIdentity(PrincipalContext context, string identityValue) { return (UserProxyPrincipal)FindByIdentityWithType(context, typeof(UserProxyPrincipal), identityValue); }
        public static new UserProxyPrincipal FindByIdentity(PrincipalContext context, IdentityType identityType, string identityValue) { return (UserProxyPrincipal)FindByIdentityWithType(context, typeof(UserProxyPrincipal), identityType, identityValue); }

        [DirectoryProperty("objectSid")]
        public string ObjectSid
        {
            get
            {
                var values = ExtensionGet("objectSid");
                return ((values != null) && (values.Length > 0) ? values[0].ToString() : null);
            }
            set { ExtensionSet("objectSid", value); }
        }

        [DirectoryProperty("name")]
        public new string Name
        {
            get
            {
                var values = ExtensionGet("name");
                return ((values != null) && (values.Length > 0) ? values[0].ToString() : null);
            }
            set { ExtensionSet("name", value); }
        }
    }
}
