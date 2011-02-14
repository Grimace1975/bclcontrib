using System;
using System.Runtime.CompilerServices;
namespace System.Specialized
{
    [IgnoreNamespace, Imported, ScriptName("console")]
    public class MozConsole
    {
        public static void Log(params object[] args) {  }
        public static void Info(params object[] args) { }
        public static void Warn(params object[] args) { }
        public static void Error(params object[] args) { }
    }
}
