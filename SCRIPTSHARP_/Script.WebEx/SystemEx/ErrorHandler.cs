#if !CODE_ANALYSIS
namespace System
#else
namespace SystemEx
#endif
{
    public delegate void ErrorHandler(ErrorCode code, string message, object[] args);
}