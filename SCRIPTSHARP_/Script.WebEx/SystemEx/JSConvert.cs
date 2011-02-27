#if !CODE_ANALYSIS
namespace System
#else
using System;
using System.TypedArrays;
using System.Runtime.CompilerServices;
namespace SystemEx
#endif
{
#if CODE_ANALYSIS
    public class JSConvert
    {
        public const short Short_MinValue = -32768;
        public const int Int_MinValue = -2147483648;
        public const long Long_MinValue = -9223372036854775808;
        private static Int8Array _wba = new Int8Array(4);
        private static Int32Array _wia = new Int32Array(_wba.Buffer, 0, 1);
        private static Float32Array _wfa = new Float32Array(_wba.Buffer, 0, 1);

        public static bool Char_IsDigit(char x)
        {
            int y = int.Parse((string)x);
            return (Number.IsNaN(y) ? false : ((x == y) && (x.ToString() == y.ToString())));
        }

        public static int SingleToInt32Bits(float v) { _wfa[0] = v; return _wia[0]; }
        public static float Int32BitsToSingle(int v) { _wia[0] = v; return _wfa[0]; }

        [AlternateSignature]
        public extern static string BytesToString(byte[] b);
        public static string BytesToString(byte[] b, int offset, int length)
        {
            if (length == 0)
                length = b.Length;
            int[] chars = new int[length];
            for (int index = 0; index < length; index++)
                chars[index] = b[offset + index];
            return string.FromCharCode(chars);
        }

        [AlternateSignature]
        public extern static byte[] StringToBytes(string s);
        public static byte[] StringToBytes(string s, byte[] b, int offset)
        {
            if (b == null)
                b = new byte[s.Length];
            for (int index = 0; index < s.Length; index++)
                b[offset + index] = (byte)s.CharCodeAt(index);
            return b;
        }

        public static string Int16ToString(short value, int toBase)
        {
            return value.ToString();
        }

        public static string Int32ToString(int value, int toBase)
        {
            return value.ToString();
        }

        public static string Int64ToString(long value, int toBase)
        {
            return value.ToString();
        }

    #region JSArray

        public static JSArrayInteger BytesToJSArray(byte[] data)
        {
            JSArrayInteger jsan = (JSArrayInteger)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index]);
            return jsan;
        }

        public static JSArrayInteger UBytesToJSArray(byte[] data)
        {
            JSArrayInteger jsan = (JSArrayInteger)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index] & 255);
            return jsan;
        }

        public static JSArrayNumber SinglesToJSArray(float[] data)
        {
            JSArrayNumber jsan = (JSArrayNumber)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index]);
            return jsan;
        }

        public static JSArrayNumber DoublesToJSArray(double[] data)
        {
            JSArrayNumber jsan = (JSArrayNumber)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index]);
            return jsan;
        }

        public static JSArrayInteger Ints16ToJSArray(short[] data)
        {
            JSArrayInteger jsan = (JSArrayInteger)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index]);
            return jsan;
        }
        public static JSArrayInteger UInt16ToJSArray(short[] data)
        {
            JSArrayInteger jsan = (JSArrayInteger)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index] & 65535);
            return jsan;
        }

        public static JSArrayInteger Ints32ToJSArray(int[] data)
        {
            JSArrayInteger jsan = (JSArrayInteger)Script.Literal("[]");
            int length = data.Length;
            for (int index = length - 1; index >= 0; index--)
                jsan.Set(index, data[index]);
            return jsan;
        }

        #endregion
    }
#else
    public class JSConvert
    {
        public const short Short_MinValue = short.MinValue;
        public const int Int_MinValue = int.MinValue;
        public const long Long_MinValue = long.MinValue;

        public static bool Char_IsDigit(char x) { return char.IsDigit(x); }

        public static int SingleToInt32Bits(float v) { return 0; }
        public static float Int32BitsToSingle(int v) { return 0; }

        public static string BytesToString(byte[] b) { return BytesToString(b, 0, b.Length); }
        public static string BytesToString(byte[] b, int offset, int length)
        {
            char[] chars = new char[length];
            for (int index = 0; index < length; index++)
                chars[index] = (char)b[index];
            return new String(chars);
        }

        public static byte[] StringToBytes(string s) { byte[] b = new byte[s.Length]; return StringToBytes(s, b, 0); }
        public static byte[] StringToBytes(string s, byte[] b, int offset)
        {
            for (int index = 0; index < s.Length; index++)
                b[offset + index] = (byte)s[index];
            return b;
        }

        public static string Int16ToString(short value, int toBase) { return Convert.ToString(value, toBase); }
        public static string Int32ToString(int value, int toBase) { return Convert.ToString(value, toBase); }
        public static string Int64ToString(long value, int toBase) { return Convert.ToString(value, toBase); }
    }
#endif
}
