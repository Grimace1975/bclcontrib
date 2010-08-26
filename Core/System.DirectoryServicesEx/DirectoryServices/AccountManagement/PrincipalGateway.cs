using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;

namespace System.DirectoryServices.AccountManagement
{
    public class PrincipalGateway
    {
        public static readonly SecurityIdentifier WorldSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
        public static readonly SecurityIdentifier AnonymousSid = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);

        private readonly string _domain;
        private readonly string _userId;
        private readonly string _password;
        private readonly string _userContainer;

        public PrincipalGateway(string domain, string userId, string password, string userContainer)
        {
            _domain = domain;
            _userId = userId;
            _password = password;
            _userContainer = userContainer;
        }

        #region GroupPrincipal

        public static IEnumerable<GroupPrincipal> GetAllGroupPrincipals(PrincipalContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            var searcher = new PrincipalSearcher(new GroupPrincipal(context));
            using (var searchResults = searcher.FindAll())
                return searchResults
                    .Where(g => g.StructuralObjectClass == "group")
                    .OfType<GroupPrincipal>()
                    .ToList();
        }

        public static IEnumerable<GroupPrincipal> GetGroupPrincipalsByUserIdentity(PrincipalContext context, IdentityType identityType, string identity, string container)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            using (var user = UserPrincipal.FindByIdentity(context, identityType, identity))
            {
                if (user == null)
                    return null;
                return GetGroupPrincipalsByUser(user, container);
            }
        }

        public static IEnumerable<GroupPrincipal> GetGroupPrincipalsByUser(UserPrincipal user, string container)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            return user.GetGroups()
                .Where(g => (g.StructuralObjectClass == "group") && (g.DistinguishedName.EndsWith(container)))
                .OfType<GroupPrincipal>()
                .ToList();
        }

        public static GroupPrincipal GetGroupPrincipalByIdentity(PrincipalContext context, IdentityType identityType, string identity)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            return GroupPrincipal.FindByIdentity(context, identityType, identity);
        }

        public static IEnumerable<T> MapUserPrincipalToGroup<T>(string container, string suffix, UserPrincipal userPrincipal, Func<string, T> builder)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            if (userPrincipal == null)
                throw new ArgumentNullException("userPrincipal");
            if (builder == null)
                throw new ArgumentNullException("builder");
            suffix = (string.IsNullOrEmpty(suffix) ? string.Empty : "-" + suffix);
            var entry = (DirectoryEntry)userPrincipal.GetUnderlyingObject();
            var memberOf = entry.Properties["memberOf"].Value;
            string filter = suffix + "," + container;
            //
            IEnumerable<string> set;
            if (memberOf is object[])
                set = ((object[])memberOf).Cast<string>();
            else if (memberOf is string)
                set = new[] { (string)memberOf };
            else
                return null;
            //
            return set
                .Where(c => c.EndsWith(filter) && (c.LastIndexOf(',', c.Length - filter.Length - 1) == -1))
                .Select(c => builder(c.Substring(3, c.Length - filter.Length - 3) + suffix))
                .ToList();
        }

        public TReturn WithGroupPrincipalBySid<TReturn>(string container, string sid, Func<GroupPrincipal, TReturn> action)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(sid))
                throw new ArgumentNullException("sid");
            if (action == null)
                throw new ArgumentNullException("action");
            if (sid == AnonymousSid.Value)
                throw new InvalidOperationException();
            using (var context = GetPrincipalContext(container))
                return action(GroupPrincipal.FindByIdentity(context, IdentityType.Sid, sid));
        }

        public TReturn WithGroupPrincipalByIdentity<TReturn>(string container, IdentityType identityType, string identity, Func<GroupPrincipal, TReturn> action)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (action == null)
                throw new ArgumentNullException("action");
            using (var context = GetPrincipalContext(container))
                return action(GroupPrincipal.FindByIdentity(context, identityType, identity));
        }

        public void SyncGroupPrincipalMembers<T>(PrincipalContext context, UserPrincipal userPrincipal, IdentityType identityType, IEnumerable<T> items, Func<UserPrincipal, IEnumerable<T>> existingItemsAccessor, Func<T, string> identityAccessor)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (userPrincipal == null)
                throw new ArgumentNullException("userPrincipal");
            if (existingItemsAccessor == null)
                throw new ArgumentNullException("existingItemsAccessor");
            if (identityAccessor == null)
                throw new ArgumentNullException("identityAccessor");
            // should have at least one item
            if ((items == null) || (items.Count() == 0))
                throw new ArgumentNullException("items");
            var existingItems = existingItemsAccessor(userPrincipal);
            // delete items
            existingItems.Except(items)
                .Select(c => GetGroupPrincipalByIdentity(context, identityType, identityAccessor(c)))
                .ToList()
                .ForEach(a =>
                {
                    if (a.Members.Contains(userPrincipal))
                    {
                        a.Members.Remove(userPrincipal);
                        a.Save();
                    }
                });
            // add items
            items.Except(existingItems)
                .Select(c => GetGroupPrincipalByIdentity(context, identityType, identityAccessor(c)))
                .ToList()
                .ForEach(a =>
                {
                    if (!a.Members.Contains(userPrincipal))
                    {
                        a.Members.Add(userPrincipal);
                        a.Save();
                    }
                });
        }

        public static bool MergeGroupPrincipalMembersWithUsers(GroupPrincipal groupPrincipal, IEnumerable<UserPrincipal> userPrincipals, bool wantsMembership) { return MergeGroupPrincipalMembersWithUsers(groupPrincipal, userPrincipals, wantsMembership, true); }
        public static bool MergeGroupPrincipalMembersWithUsers(GroupPrincipal groupPrincipal, IEnumerable<UserPrincipal> userPrincipals, bool wantsMembership, bool saveOnChange)
        {
            var members = groupPrincipal.Members;
            bool hasChanged = false;
            foreach (var userPrincipal in userPrincipals)
            {
                var hasMembership = members.Contains(userPrincipal);
                if (hasMembership == wantsMembership)
                    continue;
                if (wantsMembership)
                    members.Add(userPrincipal);
                else
                    members.Remove(userPrincipal);
                if (!hasChanged)
                    hasChanged = true;
            }
            if ((saveOnChange) && (hasChanged))
                groupPrincipal.Save();
            return hasChanged;
        }

        #endregion

        #region UserPrincipal

        public static IEnumerable<UserPrincipal> GetAllUserPrincipals(PrincipalContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            var searcher = new PrincipalSearcher(new UserPrincipal(context));
            using (var searchResults = searcher.FindAll())
                return searchResults
#if DEBUG
.Take(100)
#endif
.Where(user => user.StructuralObjectClass == "user")
                    .OfType<UserPrincipal>()
                    .ToList();
        }

        public static UserPrincipal GetUserPrincipalByIdentity(PrincipalContext context, IdentityType identityType, string identity)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            return UserPrincipal.FindByIdentity(context, identityType, identity);
        }

        public static UserPrincipal GetUserPrincipalByEmail(PrincipalContext context, string email)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");
            var searcher = new PrincipalSearcher(new UserPrincipal(context) { EmailAddress = email });
            using (var users = searcher.FindAll())
                switch (users.Count())
                {
                    case 0:
                        return null;
                    case 1:
                        return (UserPrincipal)users.First();
                    default:
                        throw new MultipleMatchesException();
                }
        }

        public static IEnumerable<UserPrincipal> GetUserPrincipalsBySingleIdentity<TSingle>(PrincipalContext context, IdentityType identityType, string identity)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            using (var groupPrincipal = GroupPrincipal.FindByIdentity(context, identityType, identity))
            {
                if (groupPrincipal == null)
                    return null;
                return groupPrincipal.GetMembers()
                    .Where(user => user.StructuralObjectClass == "user")
                    .OfType<UserPrincipal>()
                    .ToList();
            }
        }

        public static IEnumerable<UserPrincipal> GetUserPrincipalsBySingleIdentity<TSingle>(PrincipalContext context, IdentityType identityType, string identity, Func<GroupPrincipal, TSingle> singleBuilder, out TSingle single)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (singleBuilder == null)
                throw new ArgumentNullException("singleBuilder");
            using (var groupPrincipal = GroupPrincipal.FindByIdentity(context, identityType, identity))
            {
                if (groupPrincipal == null)
                {
                    single = default(TSingle);
                    return null;
                }
                single = (singleBuilder != null ? singleBuilder(groupPrincipal) : default(TSingle));
                //
                return groupPrincipal.GetMembers()
                    .Where(user => user.StructuralObjectClass == "user")
                    .OfType<UserPrincipal>()
                    .ToList();
            }
        }

        public void MoveUserPrincipalTo(UserPrincipal userPrincipal, string newContainer) { MoveUserPrincipalTo(userPrincipal, GetDirectoryEntry(newContainer)); }
        public static void MoveUserPrincipalTo(UserPrincipal userPrincipal, DirectoryEntry newContainer)
        {
            if (userPrincipal == null)
                throw new ArgumentNullException("userPrincipal");
            var directoryEntry = (DirectoryEntry)userPrincipal.GetUnderlyingObject();
            if (newContainer != null)
            {
                directoryEntry.MoveTo(newContainer);
                directoryEntry.CommitChanges();
            }
        }

        public TReturn WithUserPrincipals<TReturn>(Func<IEnumerable<UserPrincipal>, TReturn> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            using (var context = GetPrincipalContext(_userContainer))
                return action(GetAllUserPrincipals(context));
        }

        public TReturn WithUserPrincipalBySid<TReturn>(string sid, Func<UserPrincipal, TReturn> action)
        {
            if (string.IsNullOrEmpty(sid))
                throw new ArgumentNullException("sid");
            if (action == null)
                throw new ArgumentNullException("action");
            if (sid == AnonymousSid.Value)
                throw new InvalidOperationException();
            using (var context = GetPrincipalContext(_userContainer))
                return action(UserPrincipal.FindByIdentity(context, IdentityType.Sid, sid));
        }

        public TReturn WithUserPrincipalByIdentity<TReturn>(IdentityType identityType, string identity, Func<UserPrincipal, TReturn> action)
        {
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (action == null)
                throw new ArgumentNullException("action");
            using (var context = GetPrincipalContext(_userContainer))
                return action(UserPrincipal.FindByIdentity(context, identityType, identity));
        }

        public TReturn WithUserPrincipalByIdentity<TReturn>(string container, IdentityType identityType, string identity, Func<UserPrincipal, TReturn> action)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (action == null)
                throw new ArgumentNullException("action");
            using (var context = GetPrincipalContext(container))
                return action(UserPrincipal.FindByIdentity(context, identityType, identity));
        }
        #endregion

        #region Context

        public PrincipalContext GetPrincipalUsersContext() { return GetPrincipalContext(ContextType.ApplicationDirectory, _userContainer); }
        public PrincipalContext GetPrincipalContext(string container) { return GetPrincipalContext(ContextType.ApplicationDirectory, container); }
        public PrincipalContext GetPrincipalContext(ContextType contextType, string container)
        {
            return new PrincipalContext(contextType, _domain, container, _userId, _password);
        }

        public DirectoryEntry GetDirectoryEntry(string container)
        {
            var authenticationTypes = (_domain.EndsWith(":636") ? AuthenticationTypes.SecureSocketsLayer | AuthenticationTypes.Secure : AuthenticationTypes.Secure);
            return new DirectoryEntry("LDAP://" + _domain + "/" + container, _userId, _password, authenticationTypes);
        }

        #endregion
    }
}
