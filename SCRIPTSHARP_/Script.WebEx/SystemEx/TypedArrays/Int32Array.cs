using System;
using System.Runtime.CompilerServices;
using System.TypedArrays;
namespace SystemEx.TypedArrays
{
    // [TypedArrays] https://cvs.khronos.org/svn/repos/registry/trunk/public/webgl/doc/spec/TypedArray-spec.html
    public class Int32Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        protected Int32Array() { }

        public static Int32Array Create(ArrayBuffer buffer) { return (Int32Array)Script.Literal("new Int32Array({0})", buffer); }
        public static Int32Array Create2(ArrayBuffer buffer, int byteOffset) { return (Int32Array)Script.Literal("new Int32Array({0}, {1})", buffer, byteOffset); }
        public static Int32Array Create3(ArrayBuffer buffer, int byteOffset, int length) { return (Int32Array)Script.Literal("new Int32Array({0}, {1}, {2})", buffer, byteOffset, length); }
        public static Int32Array CreateA(int[] data) { return Create6(JSConvertEx.Ints32ToJSArray(data)); }
        public static Int32Array Create4(Int32Array array) { return (Int32Array)Script.Literal("new Int32Array({0})", array); }
        public static Int32Array Create5(int size) { return (Int32Array)Script.Literal("new Int32Array({0})", size); }
        public static Int32Array Create6(JSArrayInteger data) { return (Int32Array)Script.Literal("new Int32Array({0})", data); }

        public int Get(int index) { return (int)Script.Literal("this[{0}]", index); }

        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }

        [AlternateSignature]
        public extern void SetA(int[] array);
        public void SetA(int[] array, int offset) { Set5(JSConvertEx.Ints32ToJSArray(array), offset); }
        public void Set(Int32Array array) { Script.Literal("this.set({0})", array); }
        public void Set2(Int32Array array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }
        public void Set3(int index, int value) { Script.Literal("this[{0}] = {1}", index, value); }
        public void Set4(JSArrayInteger array) { Script.Literal("this.set({0})", array); }
        public void Set5(JSArrayInteger array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }

        public Int32Array Slice(int offset, int length) { return (Int32Array)Script.Literal("this.slice({0}, {1})", offset, length); }
    }
}
