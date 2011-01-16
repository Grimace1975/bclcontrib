using System;
namespace SystemEx.Html
{
    public class WindowEx
    {
        public static string Btoa(string s)
        {
            return (string)Script.Literal("window.btoa({0})", s);
        }

        public static string Atob(string s)
        {
            return (string)Script.Literal("window.atob({0})", s);
        }
    }
}
