using System;
using System.Runtime.CompilerServices;
using System.TypedArrays;
namespace SystemEx.TypedArrays
{
    public class Uint16Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 2;

        protected Uint16Array() { }

        public static Uint16Array Create(ArrayBuffer buffer) { return (Uint16Array)Script.Literal("new Uint16Array({0})", buffer); }
        public static Uint16Array Create2(ArrayBuffer buffer, int byteOffset) { return (Uint16Array)Script.Literal("new Uint16Array({0}, {1})", buffer, byteOffset); }
        public static Uint16Array Create3(ArrayBuffer buffer, int byteOffset, int length) { return (Uint16Array)Script.Literal("new Uint16Array({0}, {1}, {2})", buffer, byteOffset, length); }
        public static Uint16Array CreateA(int[] data) { return Create6(JSConvertEx.Ints32ToJSArray(data)); }
        public static Uint16Array Create4(Int32Array array) { return (Uint16Array)Script.Literal("new Uint16Array({0})", array); }
        public static Uint16Array Create5(int size) { return (Uint16Array)Script.Literal("new Uint16Array({0})", size); }
        public static Uint16Array Create6(JSArrayInteger data) { return (Uint16Array)Script.Literal("new Uint16Array({0})", data); }

        public int Get(int index) { return (int)Script.Literal("this[{0}]", index); }

        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }

        [AlternateSignature]
        public extern void SetA(int[] array);
        public void SetA(int[] array, int offset) { Set5(JSConvertEx.Ints32ToJSArray(array), offset); }
        public void Set(Uint16Array array) { Script.Literal("this.set({0})", array); }
        public void Set2(Uint16Array array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }
        public void Set3(int index, int value) { Script.Literal("this[{0}] = {1}", index, value); }
        public void Set4(JSArrayInteger array) { Script.Literal("this.set({0})", array); }
        public void Set5(JSArrayInteger array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }

        public Uint16Array Slice(int offset, int length) { return (Uint16Array)Script.Literal("this.slice({0}, {1})", offset, length); }
    }
}
