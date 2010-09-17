using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;
using System.Reflection;
namespace System.DirectoryServices.AccountManagement
{
    public class PrincipalGateway : DirectoryGateway
    {
        static PrincipalGateway()
        {
            SetGroupPrincipalMatcher(new GroupPrincipalMatcher());
            SetUserPrincipalMatcher(new UserPrincipalMatcher());
        }
        public PrincipalGateway(string domain, string userId, string password, string userContainer)
            : base(domain, userId, password, userContainer) { }

        public static void SetGroupPrincipalMatcher(IPrincipalMatcher matcher)
        {
            GroupPrincipalMatcher = matcher;
        }

        public static void SetUserPrincipalMatcher(IPrincipalMatcher matcher)
        {
            UserPrincipalMatcher = matcher;
        }

        public static IPrincipalMatcher GroupPrincipalMatcher { get; private set; }
        public static IPrincipalMatcher UserPrincipalMatcher { get; private set; }

        #region GroupPrincipal

        public static bool MergeGroupPrincipalMembersWithUsers<TPrincipal>(TPrincipal groupPrincipal, IEnumerable<Principal> itemPrincipals, bool wantsMembership)
            where TPrincipal : GroupPrincipal { return MergeGroupPrincipalMembersWithUsers<TPrincipal>(groupPrincipal, itemPrincipals, wantsMembership, true); }
        public static bool MergeGroupPrincipalMembersWithUsers<TPrincipal>(TPrincipal groupPrincipal, IEnumerable<Principal> itemPrincipals, bool wantsMembership, bool saveOnChange)
            where TPrincipal : GroupPrincipal
        {
            var members = groupPrincipal.Members;
            bool hasChanged = false;
            foreach (var itemPrincipal in itemPrincipals)
            {
                var hasMembership = members.Contains(itemPrincipal);
                if (hasMembership == wantsMembership)
                    continue;
                if (wantsMembership)
                    members.Add(itemPrincipal);
                else
                    members.Remove(itemPrincipal);
                if (!hasChanged)
                    hasChanged = true;
            }
            if ((saveOnChange) && (hasChanged))
                groupPrincipal.Save();
            return hasChanged;
        }

        public void SyncGroupPrincipalMembersByAccessor<TPrincipal, T>(PrincipalContext context, TPrincipal principal, IdentityType identityType, IEnumerable<T> items, Func<TPrincipal, IEnumerable<T>> existingItemsAccessor, Func<T, string> identityAccessor)
            where TPrincipal : Principal { SyncGroupPrincipalMembersByAccessor<GroupPrincipal, TPrincipal, T>(GroupPrincipalMatcher, context, principal, identityType, items, existingItemsAccessor, identityAccessor); }
        public void SyncGroupPrincipalMembersByAccessor<TGroupPrincipal, TPrincipal, T>(IPrincipalMatcher groupPrincipalMatcher, PrincipalContext context, TPrincipal principal, IdentityType identityType, IEnumerable<T> items, Func<TPrincipal, IEnumerable<T>> existingItemsAccessor, Func<T, string> identityAccessor)
            where TGroupPrincipal : GroupPrincipal
            where TPrincipal : Principal
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (principal == null)
                throw new ArgumentNullException("userPrincipal");
            if (existingItemsAccessor == null)
                throw new ArgumentNullException("existingItemsAccessor");
            if (identityAccessor == null)
                throw new ArgumentNullException("identityAccessor");
            if (items == null)
                throw new ArgumentNullException("items");
            var existingItems = (existingItemsAccessor(principal) ?? new List<T>());
            // delete items
            existingItems.Except(items)
                .Select(c => GetPrincipalByIdentity<TGroupPrincipal>(groupPrincipalMatcher, context, identityType, identityAccessor(c)))
                .ToList()
                .ForEach(g =>
                {
                    if (g.Members.Contains(principal))
                    {
                        g.Members.Remove(principal);
                        g.Save();
                    }
                });
            // add items
            items.Except(existingItems)
                .Select(c => GetPrincipalByIdentity<TGroupPrincipal>(groupPrincipalMatcher, context, identityType, identityAccessor(c)))
                .ToList()
                .ForEach(g =>
                {
                    if (!g.Members.Contains(principal))
                    {
                        g.Members.Add(principal);
                        g.Save();
                    }
                });
        }

        #endregion

        #region UserPrincipal

        public static IEnumerable<TPrincipal> GetPrincipalMembersBySingleIdentity<TPrincipal>(IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity)
            where TPrincipal : Principal { object single; return GetPrincipalMembersBySingleIdentity<GroupPrincipal, TPrincipal, object>(GroupPrincipalMatcher, principalMatcher, context, identityType, identity, null, out single); }
        public static IEnumerable<TPrincipal> GetPrincipalMembersBySingleIdentity<TGroupPrincipal, TPrincipal>(IPrincipalMatcher groupPrincipalMatcher, IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity)
            where TGroupPrincipal : GroupPrincipal
            where TPrincipal : Principal { object single; return GetPrincipalMembersBySingleIdentity<TGroupPrincipal, TPrincipal, object>(groupPrincipalMatcher, principalMatcher, context, identityType, identity, null, out single); }
        public static IEnumerable<TPrincipal> GetPrincipalMembersBySingleIdentity<TPrincipal, TSingle>(IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity, Func<GroupPrincipal, TSingle> singleBuilder, out TSingle single)
            where TPrincipal : Principal { return GetPrincipalMembersBySingleIdentity<GroupPrincipal, TPrincipal, TSingle>(GroupPrincipalMatcher, principalMatcher, context, identityType, identity, singleBuilder, out single); }
        public static IEnumerable<TPrincipal> GetPrincipalMembersBySingleIdentity<TGroupPrincipal, TPrincipal, TSingle>(IPrincipalMatcher groupPrincipalMatcher, IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity, Func<TGroupPrincipal, TSingle> singleBuilder, out TSingle single)
            where TGroupPrincipal : GroupPrincipal
            where TPrincipal : Principal
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (singleBuilder == null)
                throw new ArgumentNullException("singleBuilder");
            using (var groupPrincipal = GetPrincipalByIdentity<TGroupPrincipal>(groupPrincipalMatcher, context, identityType, identity))
            {
                if (groupPrincipal == null)
                {
                    single = default(TSingle);
                    return null;
                }
                single = (singleBuilder != null ? singleBuilder(groupPrincipal) : default(TSingle));
                //
                return groupPrincipal.GetMembers()
                    .Where(principalMatcher.Determiner)
                    .OfType<TPrincipal>()
                    .ToList();
            }
        }

        public static TPrincipal GetUserPrincipalByEmail<TPrincipal>(PrincipalContext context, string email)
            where TPrincipal : UserPrincipal
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
                        return (TPrincipal)users.First();
                    default:
                        throw new MultipleMatchesException();
                }
        }

        #endregion

        #region Generic

        // ALL
        public static IEnumerable<TPrincipal> GetAllPrincipals<TPrincipal>(IPrincipalMatcher principalMatcher, PrincipalContext context)
            where TPrincipal : Principal { return GetAllPrincipals<TPrincipal>(principalMatcher, context, null); }
        public static IEnumerable<TPrincipal> GetAllPrincipals<TPrincipal>(IPrincipalMatcher principalMatcher, PrincipalContext context, int? maximumItems)
            where TPrincipal : Principal
        {
            if (principalMatcher == null)
                throw new ArgumentNullException("principalMatcher");
            if (context == null)
                throw new ArgumentNullException("context");
            var list = new List<TPrincipal>();
            foreach (var queryFilter in principalMatcher.GetQueryFilters(context))
            {
                var searcher = new PrincipalSearcher(queryFilter);
                using (var searchResults = searcher.FindAll())
                    list.AddRange((!maximumItems.HasValue ? searchResults : searchResults.Take(maximumItems.Value)) // - list.Count))
                        .Where(principalMatcher.Determiner)
                        .OfType<TPrincipal>());
                //if ((maximumItems.HasValue) && (list.Count >= maximumItems.Value))
                //    break;
            }
            return list;
        }

        private class PrincipalProtectedAccessor : Principal
        {
            public static Principal FindByIdentityWithTypeWrapper(PrincipalContext context, Type principalType, IdentityType identityType, string identityValue) { return FindByIdentityWithType(context, principalType, identityType, identityValue); }
        }

        // SINGLE
        public static TPrincipal GetPrincipalBySid<TPrincipal>(IPrincipalMatcher principalMatcher, PrincipalContext context, string sid)
            where TPrincipal : Principal { return GetPrincipalByIdentity<TPrincipal>(principalMatcher, context, IdentityType.Sid, sid); }
        public static TPrincipal GetPrincipalByIdentity<TPrincipal>(IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity)
            where TPrincipal : Principal
        {
            if (principalMatcher == null)
                throw new ArgumentNullException("principalMatcher");
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            foreach (var principalType in principalMatcher.GetPrincipalTypes())
            {
                var principal = (PrincipalProtectedAccessor.FindByIdentityWithTypeWrapper(context, principalType, identityType, identity) as TPrincipal);
                if (principal != null)
                    return principal;
            }
            return null;
        }

        public TResult WithPrincipalBySid<TPrincipal, TResult>(IPrincipalMatcher principalMatcher, string container, string sid, Func<TPrincipal, TResult> action)
            where TPrincipal : Principal { return WithPrincipalByIdentity<TPrincipal, TResult>(principalMatcher, container, IdentityType.Sid, sid, action); }
        public TResult WithPrincipalByIdentity<TPrincipal, TResult>(IPrincipalMatcher principalMatcher, string container, IdentityType identityType, string identity, Func<TPrincipal, TResult> action)
            where TPrincipal : Principal
        {
            if (principalMatcher == null)
                throw new ArgumentNullException("principalMatcher");
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (action == null)
                throw new ArgumentNullException("action");
            using (var context = GetPrincipalContext(container))
                return action(GetPrincipalByIdentity<TPrincipal>(principalMatcher, context, identityType, identity));
        }

        // MANY
        public static IEnumerable<TResult> GetPrincipalsByIdentities<TPrincipal, TResult>(IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, IEnumerable<string> identities, Func<TPrincipal, TResult> selector)
            where TPrincipal : Principal
        {
            if (principalMatcher == null)
                throw new ArgumentNullException("principalMatcher");
            if (context == null)
                throw new ArgumentNullException("context");
            if (selector == null)
                throw new ArgumentNullException("selector");
            if ((identities == null) || (!identities.GetEnumerator().MoveNext()))
                return new List<TResult>();
            return identities
                .Select(identity => GetPrincipalByIdentity<TPrincipal>(principalMatcher, context, identityType, identity))
                .Where(p => p != null)
                .Select(selector)
                .ToList();
        }

        public static IEnumerable<TGroupPrincipal> GetPrincipalGroups<TGroupPrincipal, TPrincipal>(IPrincipalMatcher groupPrincipalMatcher, IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity, string container)
            where TGroupPrincipal : Principal
            where TPrincipal : Principal
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(identity))
                throw new ArgumentNullException("identity");
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            using (var principal = GetPrincipalByIdentity<TPrincipal>(principalMatcher, context, identityType, identity))
            {
                if (principal == null)
                    return null;
                return GetPrincipalGroups<TGroupPrincipal>(groupPrincipalMatcher, principal, container);
            }
        }
        public static IEnumerable<TGroupPrincipal> GetPrincipalGroups<TGroupPrincipal>(IPrincipalMatcher groupPrincipalMatcher, Principal principal, string container)
            where TGroupPrincipal : Principal
        {
            if (groupPrincipalMatcher == null)
                throw new ArgumentNullException("groupPrincipalMatcher");
            if (principal == null)
                throw new ArgumentNullException("principal");
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            return principal.GetGroups()
                .Where(g => (groupPrincipalMatcher.Determiner(g)) && (g.DistinguishedName.EndsWith(container)))
                .OfType<TGroupPrincipal>()
                .ToList();
        }

        #endregion

        #region DirectoryEntry

        public static IEnumerable<T> GetPrincipalMemberOf<T>(string container, Principal principal, Func<string, T> selector) { return GetPrincipalMemberOf<T>(container, null, principal, selector); }
        public static IEnumerable<T> GetPrincipalMemberOf<T>(string container, string suffix, Principal principal, Func<string, T> selector)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");
            var directoryEntry = (DirectoryEntry)principal.GetUnderlyingObject();
            return GetDirectoryEntryMemberOf(container, suffix, directoryEntry, selector);
        }

        public void MovePrincipalTo(Principal principal, string newContainer) { MovePrincipalTo(principal, GetDirectoryEntry(newContainer)); }
        public static void MovePrincipalTo(Principal principal, DirectoryEntry newContainer)
        {
            if (principal == null)
                throw new ArgumentNullException("principal");
            MoveDirectoryEntryTo((DirectoryEntry)principal.GetUnderlyingObject(), newContainer);
        }

        #endregion

        public PrincipalContext GetPrincipalUsersContext() { return GetPrincipalContext(ContextType.ApplicationDirectory, UserContainer); }
        public PrincipalContext GetPrincipalContext(string container) { return GetPrincipalContext(ContextType.ApplicationDirectory, container); }
        public PrincipalContext GetPrincipalContext(ContextType contextType, string container)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            return MakeAsAuthenticated<PrincipalContext, object>((domain, userId, password, tag) => new PrincipalContext(contextType, domain, container, userId, password), null);
        }
    }
}
