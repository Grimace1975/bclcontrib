using System;
using System.Runtime.CompilerServices;
namespace SystemEx.TypedArrays
{
    public class Float64Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        protected Float64Array() { }

        public static Float64Array Create(ArrayBuffer buffer) { return (Float64Array)Script.Literal("new Float64Array({0})", buffer); }
        public static Float64Array Create2(ArrayBuffer buffer, int byteOffset) { return (Float64Array)Script.Literal("new Float64Array({0}, {1})", buffer, byteOffset); }
        public static Float64Array Create3(ArrayBuffer buffer, int byteOffset, int length) { return (Float64Array)Script.Literal("new Float64Array({0}, {1}, {2})", buffer, byteOffset, length); }
        public static Float64Array CreateA(double[] data) { return Create6(JSConvertEx.DoublesToJSArray(data)); }
        public static Float64Array Create4(Int32Array array) { return (Float64Array)Script.Literal("new Float64Array({0})", array); }
        public static Float64Array Create5(int size) { return (Float64Array)Script.Literal("new Float64Array({0})", size); }
        public static Float64Array Create6(JSArrayNumber data) { return (Float64Array)Script.Literal("new Float64Array({0})", data); }

        public int Get(int index) { return (int)Script.Literal("this[{0}]", index); }

        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }

        [AlternateSignature]
        public extern void SetA(double[] array);
        public void SetA(double[] array, int offset) { Set5(JSConvertEx.DoublesToJSArray(array), offset); }
        public void Set(Float64Array array) { Script.Literal("this.set({0})", array); }
        public void Set2(Float64Array array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }
        public void Set3(int index, double value) { Script.Literal("this[{0}] = {1}", index, value); }
        public void Set4(JSArrayNumber array) { Script.Literal("this.set({0})", array); }
        public void Set5(JSArrayNumber array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }

        public Float64Array Slice(int offset, int length) { return (Float64Array)Script.Literal("this.slice({0}, {1})", offset, length); }
    }
}
