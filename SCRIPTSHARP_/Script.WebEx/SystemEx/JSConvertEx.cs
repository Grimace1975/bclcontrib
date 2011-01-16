using System;
using SystemEx.TypedArrays;
namespace SystemEx
{
    public class JSConvertEx
    {
        private static Int8Array _wba = Int8Array.Create5(4);
        private static Int32Array _wia = Int32Array.Create3(_wba.Buffer, 0, 1);
        private static Float32Array _wfa = Float32Array.Create3(_wba.Buffer, 0, 1);

        public static int SingleToIntBits(float v)
        {
            _wfa.Set3(0, v);
            return _wia.Get(0);
        }

        public static float IntBitsToSingle(int v)
        {
            _wia.Set3(0, v);
            return _wfa.Get(0);
        }

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
