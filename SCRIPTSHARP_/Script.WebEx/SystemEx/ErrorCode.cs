#if !CODE_ANALYSIS
namespace System
#else
namespace SystemEx
#endif
{
    public enum ErrorCode
    {
        ERR_FATAL = 0, // exit the entire game with a popup window 
        ERR_DROP = 1, // print to console and disconnect from game 
        ERR_DISCONNECT = 2, // don't kill server 
        INFO = 3
    }
}