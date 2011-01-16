#if CODE_ANALYSIS
using System;
using System.Runtime.CompilerServices;
namespace SystemEx
#else
namespace System.Text
#endif
{
    public class StringBuilderEx
    {
#if CODE_ANALYSIS
        public static int GetLength(StringBuilder b) { return (int)Script.Literal(""); }
#else
        public static int GetLength(StringBuilder b) { return b.Length; }
#endif
    }
}
