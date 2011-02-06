#if !CODE_ANALYSIS
namespace System
#else
using System;
using System.Diagnostics;
using System.Specialized;
namespace SystemEx
#endif
{
#if CODE_ANALYSIS
    public class JSSystem
    {
        public static bool TryCatch(Exception e, string name)
        {
            MozConsole.Warn(e.Message);
            throw e;
        }
        public static long CurrentMSecond
        {
            get { return (long)Script.Literal("(new Date()).getTime()"); }
        }

    }
#else
    public class JSSystem
    {
        public static bool TryCatch(Exception e, string name) { throw e; }
    }
#endif
}