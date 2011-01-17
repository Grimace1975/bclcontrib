using System;
using System.Runtime.CompilerServices;
using System.TypedArrays;
namespace SystemEx.TypedArrays
{
    public class Uint32Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        protected Uint32Array() { }

        public static Uint32Array Create(ArrayBuffer buffer) { return (Uint32Array)Script.Literal("new Uint32Array({0})", buffer); }
        public static Uint32Array Create2(ArrayBuffer buffer, int byteOffset) { return (Uint32Array)Script.Literal("new Uint32Array({0}, {1})", buffer, byteOffset); }
        public static Uint32Array Create3(ArrayBuffer buffer, int byteOffset, int length) { return (Uint32Array)Script.Literal("new Uint32Array({0}, {1}, {2})", buffer, byteOffset, length); }
        public static Uint32Array CreateA(int[] data) { return Create6(JSConvertEx.Ints32ToJSArray(data)); }
        public static Uint32Array Create4(Int32Array array) { return (Uint32Array)Script.Literal("new Uint32Array({0})", array); }
        public static Uint32Array Create5(int size) { return (Uint32Array)Script.Literal("new Uint32Array({0})", size); }
        public static Uint32Array Create6(JSArrayInteger data) { return (Uint32Array)Script.Literal("new Uint32Array({0})", data); }

        public int Get(int index) { return (int)Script.Literal("this[{0}]", index); }

        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }

        [AlternateSignature]
        public extern void SetA(int[] array);
        public void SetA(int[] array, int offset) { Set5(JSConvertEx.Ints32ToJSArray(array), offset); }
        public void Set(Uint32Array array) { Script.Literal("this.set({0})", array); }
        public void Set2(Uint32Array array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }
        public void Set3(int index, int value) { Script.Literal("this[{0}] = {1}", index, value); }
        public void Set4(JSArrayInteger array) { Script.Literal("this.set({0})", array); }
        public void Set5(JSArrayInteger array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }

        public Uint32Array Slice(int offset, int length) { return (Uint32Array)Script.Literal("this.slice({0}, {1})", offset, length); }
    }
}
