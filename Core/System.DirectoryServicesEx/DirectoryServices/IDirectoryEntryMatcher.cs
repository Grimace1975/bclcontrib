using System.Collections.Generic;
namespace System.DirectoryServices
{
    public interface IDirectoryEntryMatcher
    {
        Func<DirectoryEntry, bool> IsSchemaClassName { get; }
        IEnumerable<string> GetQueryFilters();
    }
}
