using System.Security.Principal;
namespace System.DirectoryServices
{
    public static class DirectoryEntryExtensions
    {
        public static SecurityIdentifier GetSid(this DirectoryEntry directoryEntry)
        {
            var objectSid = (byte[])directoryEntry.Properties["objectSid"].Value;
            return (objectSid != null ? new SecurityIdentifier(objectSid, 0) : null);
        }
    }
}
