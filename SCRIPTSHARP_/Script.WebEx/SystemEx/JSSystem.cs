#if !CODE_ANALYSIS
namespace System
#else
using System;
namespace SystemEx
#endif
{
#if CODE_ANALYSIS
    public class JSSystem
    {
        public static bool TryCatch(Exception e, string name) { throw e; }
    }
#else
    public class JSSystem
    {
        public static bool TryCatch(Exception e, string name) { throw e; }
    }
#endif
}