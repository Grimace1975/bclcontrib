#if !CODE_ANALYSIS
namespace System
#else
using System;
using System.Runtime.CompilerServices;
namespace SystemEx
#endif
{
#if CODE_ANALYSIS
    public class JSString
    {
        [AlternateSignature]
        public extern static string CharsToString(char[] b);
        public static string CharsToString(char[] b, int startIndex, int length)
        {
            if (length == 0)
                length = b.Length;
            int[] chars = new int[length];
            for (int index = 0; index < length; index++)
                chars[index] = b[startIndex + index];
            return string.FromCharCode(chars);
        }

        public static char[] StringToChars(string s)
        {
            char[] b = new char[s.Length];
            for (int index = 0; index < s.Length; index++)
                b[index] = (char)s.CharCodeAt(index);
            return b;
        }
    }
#else
    public class JSString
    {
        public static string CharsToString(char[] b) { return new String(b); }
        public static string CharsToString(char[] b, int startIndex, int length) { return new String(b, startIndex, length); }
        public static char[] StringToChars(string s) { return s.ToCharArray(); }
    }
#endif
}