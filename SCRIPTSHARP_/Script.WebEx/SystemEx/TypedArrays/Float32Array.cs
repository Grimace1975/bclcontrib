using System;
using System.Runtime.CompilerServices;
using System.TypedArrays;
namespace SystemEx.TypedArrays
{
    public class Float32Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        protected Float32Array() { }

        public static Float32Array Create(ArrayBuffer buffer) { return (Float32Array)Script.Literal("new Float32Array({0})", buffer); }
        public static Float32Array Create2(ArrayBuffer buffer, int byteOffset) { return (Float32Array)Script.Literal("new Float32Array({0}, {1})", buffer, byteOffset); }
        public static Float32Array Create3(ArrayBuffer buffer, int byteOffset, int length) { return (Float32Array)Script.Literal("new Float32Array({0}, {1}, {2})", buffer, byteOffset, length); }
        public static Float32Array CreateA(float[] data) { return Create6(JSConvertEx.SinglesToJSArray(data)); }
        public static Float32Array Create4(Int32Array array) { return (Float32Array)Script.Literal("new Float32Array({0})", array); }
        public static Float32Array Create5(int size) { return (Float32Array)Script.Literal("new Float32Array({0})", size); }
        public static Float32Array Create6(JSArrayNumber data) { return (Float32Array)Script.Literal("new Float32Array({0})", data); }

        public int Get(int index) { return (int)Script.Literal("this[{0}]", index); }

        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }

        [AlternateSignature]
        public extern void SetA(float[] array);
        public void SetA(float[] array, int offset) { Set5(JSConvertEx.SinglesToJSArray(array), offset); }
        public void Set(Float32Array array) { Script.Literal("this.set({0})", array); }
        public void Set2(Float32Array array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }
        public void Set3(int index, float value) { Script.Literal("this[{0}] = {1}", index, value); }
        public void Set4(JSArrayNumber array) { Script.Literal("this.set({0})", array); }
        public void Set5(JSArrayNumber array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }

        public Float32Array Slice(int offset, int length) { return (Float32Array)Script.Literal("this.slice({0}, {1})", offset, length); }
    }
}
