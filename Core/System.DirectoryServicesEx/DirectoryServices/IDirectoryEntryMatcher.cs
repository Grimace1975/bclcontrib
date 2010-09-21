using System.Collections.Generic;
namespace System.DirectoryServices
{
    public interface IDirectoryEntryMatcher
    {
        IEnumerable<string> GetQueryFilters();
    }
}
