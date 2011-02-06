#if !CODE_ANALYSIS
namespace System.Text
#else
using System;
using System.Runtime.CompilerServices;
namespace SystemEx
#endif
{
    public class StringBuilderEx
    {
#if CODE_ANALYSIS
        static StringBuilderEx()
        {
            Script.Literal(@"ss.StringBuilder.prototype.baseAppend = ss.StringBuilder.prototype.append;
    ss.StringBuilder.prototype.baseClear = ss.StringBuilder.prototype.clear;
    ss.StringBuilder.prototype.append = function(s) { if (!ss.isNullOrUndefined(s)) this.length += s.length; return this.baseAppend(s); }
    ss.StringBuilder.prototype.clear = function(s) { this.length = 0; return this.baseClear(); }
    ss.StringBuilder.prototype.length = 0");
        }
        public static int GetLength(StringBuilder b) { return (int)Script.Literal("{0}.length", b); }
        public static void SetLength(StringBuilder b, int value) { Script.Literal("{0}.length = {1}", b, value); }
#else
        public static int GetLength(StringBuilder b) { return b.Length; }
        public static void SetLength(StringBuilder b, int value) { b.Length = value; }
#endif
    }
}
