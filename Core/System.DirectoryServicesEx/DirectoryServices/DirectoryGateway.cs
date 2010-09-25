﻿using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;
using System.Collections;
using System.Text;
namespace System.DirectoryServices
{
    public class DirectoryGateway
    {
        private const int BatchSize = 1000;
        public static readonly SecurityIdentifier WorldSid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
        public static readonly SecurityIdentifier AnonymousSid = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);

        private readonly string _domain;
        private readonly string _userId;
        private readonly string _password;

        public DirectoryGateway(string domain, string userId, string password, string userContainer)
        {
            _domain = domain;
            _userId = userId;
            _password = password;
            UserContainer = userContainer;
        }

        public string UserContainer { get; private set; }

        protected T MakeAsAuthenticated<T, TTag>(Func<string, string, string, TTag, T> builder, TTag tag)
        {
            return builder(_domain, _userId, _password, tag);
        }

        public static string ParseUserId(IPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return (user.Identity != null ? ParseUserId(user.Identity.Name) : null);
        }
        public static string ParseUserId(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var index = path.IndexOf("\\");
            return (index > 0 ? path.Substring(index + 1) : path);
            //var userPath = path.Split(new char[] { '\\' });
            //return userPath[userPath.Length - 1];
        }

        // SINGLE
        //public static DirectoryEntry GetDirectoryEntryBySid(IDirectoryEntryMatcher directoryEntryMatcher, DirectoryEntry directoryEntry, string[] propertiesToLoad, string sid) { return GetDirectoryEntryByIdentity(directoryEntryMatcher, directoryEntry, "objectSid", propertiesToLoad, sid); }
        //public static DirectoryEntry GetDirectoryEntryByIdentity(IDirectoryEntryMatcher directoryEntryMatcher, DirectoryEntry directoryEntry, string identityProperty, string[] propertiesToLoad, string identity)
        //{
        //    if (directoryEntryMatcher == null)
        //        throw new ArgumentNullException("directoryEntryMatcher");
        //    if (directoryEntry == null)
        //        throw new ArgumentNullException("directoryEntry");
        //    if (string.IsNullOrEmpty(identityProperty))
        //        throw new ArgumentNullException("identityProperty");
        //    //foreach (var queryFilter in directoryEntryMatcher.GetQueryFilters())
        //    //{
        //    //    var filterPart = "(" + identityProperty + "=" + identity + ")";
        //    //    var directorySearcher = new DirectorySearcher(directoryEntry, string.Format(queryFilter, "(|" + filterPart + ")"), propertiesToLoad);
        //    //}
        //    return null;
        //}

        //MANY
        public static IEnumerable<T> GetDirectoryEntriesByIdentity<T>(IDirectoryEntryMatcher directoryEntryMatcher, DirectoryEntry directoryEntry, string identityProperty, string[] propertiesToLoad, IEnumerable<string> identities, Func<SearchResult, T> selector) { return GetDirectoryEntriesByIdentity(directoryEntryMatcher, directoryEntry, identityProperty, propertiesToLoad, identities, selector, null); }
        public static IEnumerable<T> GetDirectoryEntriesByIdentity<T>(IDirectoryEntryMatcher directoryEntryMatcher, DirectoryEntry directoryEntry, string identityProperty, string[] propertiesToLoad, IEnumerable<string> identities, Func<SearchResult, T> selector, Action<DirectorySearcher> limiter)
        {
            if (directoryEntryMatcher == null)
                throw new ArgumentNullException("directoryEntryMatcher");
            if (directoryEntry == null)
                throw new ArgumentNullException("directoryEntry");
            if (string.IsNullOrEmpty(identityProperty))
                throw new ArgumentNullException("identityProperty");
            if (EnumerableEx.IsNullOrEmptyArray(propertiesToLoad))
                throw new ArgumentNullException("propertiesToLoad");
            if (selector == null)
                throw new ArgumentNullException("selector");
            //
            if (EnumerableEx.IsNullOrEmpty(identities))
                return new List<T>();
            var list = new List<T>();
            foreach (var queryFilter in directoryEntryMatcher.GetQueryFilters())
                foreach (var batch in identities.GroupAt(BatchSize))
                {
                    var filterPart = "(" + identityProperty + "=" + string.Join(")(" + identityProperty + "=", batch.ToArray()) + ")";
                    var directorySearcher = new DirectorySearcher(directoryEntry, string.Format(queryFilter, "(|" + filterPart + ")"), propertiesToLoad);
                    if (limiter != null)
                        limiter(directorySearcher);
                    list.AddRange(
                        directorySearcher.FindAll()
                        .Cast<SearchResult>()
                        .Select(selector)
                        .OfType<T>());
                }
            return list;
        }

        public static IEnumerable<T> GetDirectoryEntryMemberOf<T>(string container, DirectoryEntry directoryEntry, Func<string, T> selector) { return GetDirectoryEntryMemberOf<T>(container, null, directoryEntry, selector); }
        public static IEnumerable<T> GetDirectoryEntryMemberOf<T>(string container, string suffix, DirectoryEntry directoryEntry, Func<string, T> selector)
        {
            if (string.IsNullOrEmpty(container))
                throw new ArgumentNullException("container");
            suffix = (suffix ?? string.Empty);
            if (directoryEntry == null)
                throw new ArgumentNullException("directoryEntry");
            if (selector == null)
                throw new ArgumentNullException("builder");
            var memberOf = directoryEntry.Properties["memberOf"].Value;
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
                .Select(c => selector(c.Substring(3, c.Length - filter.Length - 3) + suffix))
                .Where(c => c != null)
                .ToList();
        }

        //public static IEnumerable<TPrincipal> GetDirectoryEntryMembersBySingleIdentity<TPrincipal>(IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity)
        //    where TPrincipal : Principal { object single; return GetPrincipalMembersBySingleIdentity<GroupPrincipal, TPrincipal, object>(GroupPrincipalMatcher, principalMatcher, context, identityType, identity, null, out single); }
        //public static IEnumerable<TPrincipal> GetDirectoryEntryMembersBySingleIdentity<TGroupPrincipal, TPrincipal>(IPrincipalMatcher groupPrincipalMatcher, IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity)
        //    where TGroupPrincipal : GroupPrincipal
        //    where TPrincipal : Principal { object single; return GetPrincipalMembersBySingleIdentity<TGroupPrincipal, TPrincipal, object>(groupPrincipalMatcher, principalMatcher, context, identityType, identity, null, out single); }
        //public static IEnumerable<TPrincipal> GetDirectoryEntryMembersBySingleIdentity<TPrincipal, TSingle>(IPrincipalMatcher principalMatcher, PrincipalContext context, IdentityType identityType, string identity, Func<GroupPrincipal, TSingle> singleBuilder, out TSingle single)
        //    where TPrincipal : Principal { return GetPrincipalMembersBySingleIdentity<GroupPrincipal, TPrincipal, TSingle>(GroupPrincipalMatcher, principalMatcher, context, identityType, identity, singleBuilder, out single); }
        //public static IEnumerable<DirectoryEntry> GetDirectoryEntryMembersBySingleIdentity<TSingle>(IDirectoryEntryMatcher groupDirectoryEntryMatcher, IDirectoryEntryMatcher directoryEntryMatcher, DirectoryEntry directoryEntry, string identityProperty, string identity, Func<DirectoryEntry, TSingle> singleBuilder, out TSingle single)
        //{
        //    if (directoryEntry == null)
        //        throw new ArgumentNullException("directoryEntry");
        //    if (string.IsNullOrEmpty(identity))
        //        throw new ArgumentNullException("identity");
        //    if (singleBuilder == null)
        //        throw new ArgumentNullException("singleBuilder");
        //    using (var groupDirectoryEntry = GetDirectoryEntryByIdentity(groupDirectoryEntryMatcher, directoryEntry, identityProperty, identity))
        //    {
        //        if (groupDirectoryEntry == null)
        //        {
        //            single = default(TSingle);
        //            return null;
        //        }
        //        single = (singleBuilder != null ? singleBuilder(groupDirectoryEntry) : default(TSingle));
        //        //
        //        var members = (IEnumerable)groupDirectoryEntry.Invoke("Members", null);
        //        var list = new List<DirectoryEntry>();
        //        foreach (object member in (IEnumerable)members)
        //        {
        //            var d = new DirectoryEntry(member);
        //            if (directoryEntryMatcher.Test())
        //                list.Add(new DirectoryEntry(member));
        //        }
        //        return list;
        //    }
        //}

        public void MoveDirectoryEntryTo(DirectoryEntry directoryEntry, string newContainer) { MoveDirectoryEntryTo(directoryEntry, GetDirectoryEntry(newContainer)); }
        public static void MoveDirectoryEntryTo(DirectoryEntry directoryEntry, DirectoryEntry newContainer)
        {
            if (directoryEntry == null)
                throw new ArgumentNullException("directoryEntry");
            if (newContainer != null)
            {
                directoryEntry.MoveTo(newContainer);
                directoryEntry.CommitChanges();
            }
        }

        public DirectoryEntry GetDirectoryEntry(string container)
        {
            var authenticationTypes = (_domain.EndsWith(":636") ? AuthenticationTypes.SecureSocketsLayer | AuthenticationTypes.Secure : AuthenticationTypes.Secure);
            return new DirectoryEntry("LDAP://" + _domain + "/" + container, _userId, _password, authenticationTypes);
        }

        private string GetPrimaryGroup(DirectoryEntry aEntry, DirectoryEntry aDomainEntry)
        {
            int primaryGroupID = (int)aEntry.Properties["primaryGroupID"].Value;
            byte[] objectSid = (byte[])aEntry.Properties["objectSid"].Value;

            var escapedGroupSid = new StringBuilder();

            // Copy over everything but the last four bytes(sub-authority)
            // Doing so gives us the RID of the domain
            for (uint i = 0; i < objectSid.Length - 4; i++)
            {
                escapedGroupSid.AppendFormat("\\{0:x2}", objectSid[i]);
            }

            //Add the primaryGroupID to the escape string to build the SID of the primaryGroup
            for (uint i = 0; i < 4; i++)
            {
                escapedGroupSid.AppendFormat("\\{0:x2}", (primaryGroupID & 0xFF));
                primaryGroupID >>= 8;
            }

            //Search the directory for a group with this SID
            DirectorySearcher searcher = new DirectorySearcher();
            if (aDomainEntry != null)
            {
                searcher.SearchRoot = aDomainEntry;
            }

            searcher.Filter = "(&(objectCategory=Group)(objectSID=" + escapedGroupSid.ToString() + "))";
            searcher.PropertiesToLoad.Add("distinguishedName");

            return searcher.FindOne().Properties["distinguishedName"][0].ToString();
        }
    }
}