#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace System.DirectoryServices.AccountManagement
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("userProxyFull")]
    public class UserProxyFullPrincipal : UserPrincipalEx
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
