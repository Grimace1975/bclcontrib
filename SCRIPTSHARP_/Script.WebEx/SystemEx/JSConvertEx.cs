using System;
using System.TypedArrays;
namespace SystemEx
{
    public class JSConvertEx
    {
        private static Int8Array _wba = new Int8Array(4);
        private static Int32Array _wia = new Int32Array(_wba.Buffer, 0, 1);
        private static Float32Array _wfa = new Float32Array(_wba.Buffer, 0, 1);

        public static int SingleToIntBits(float v) { _wfa[0] = v; return _wia[0]; }
        public static float IntBitsToSingle(int v) { _wia[0] = v; return _wfa[0]; }

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
    }
}
