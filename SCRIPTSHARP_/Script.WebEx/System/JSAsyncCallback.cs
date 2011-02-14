using System.Runtime.CompilerServices;
namespace System
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public delegate void JSAsyncCallback(object ar);
}
