using System;
using System.Runtime.CompilerServices;
namespace SystemEx.TypedArrays
{
    /// <summary>
    ///  The typed array view types represent a view of an ArrayBuffer that allows for indexing and manipulation. The length of each of these is fixed.
    ///  Taken from the Khronos TypedArrays Draft Spec as of Aug 30, 2010.
    /// </summary>
    public class Int8Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 1;

        protected Int8Array() { }

        public static Int8Array Create(ArrayBuffer buffer) { return (Int8Array)Script.Literal("new Int8Array({0})", buffer); }
        public static Int8Array Create2(ArrayBuffer buffer, int byteOffset) { return (Int8Array)Script.Literal("new Int8Array({0}, {1})", buffer, byteOffset); }
        public static Int8Array Create3(ArrayBuffer buffer, int byteOffset, int length) { return (Int8Array)Script.Literal("new Int8Array({0}, {1}, {2})", buffer, byteOffset, length); }
        public static Int8Array CreateA(int[] data) { return Create6(JSConvertEx.Ints32ToJSArray(data)); }
        public static Int8Array Create4(Int8Array array) { return (Int8Array)Script.Literal("new Int8Array({0})", array); }
        public static Int8Array Create5(int size) { return (Int8Array)Script.Literal("new Int8Array({0})", size); }
        public static Int8Array Create6(JSArrayInteger data) { return (Int8Array)Script.Literal("new Int8Array({0})", data); }

        public byte Get(int index) { return (byte)Script.Literal("this[{0}]", index); }

        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }

        [AlternateSignature]
        public extern void SetA(int[] array);
        public void SetA(int[] array, int offset) { Set5(JSConvertEx.Ints32ToJSArray(array), offset); }
        public void Set(Int8Array array) { Script.Literal("this.set({0})", array); }
        public void Set2(Int8Array array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }
        public void Set3(int index, int value) { Script.Literal("this[{0}] = {1}", index, value); }
        public void Set4(JSArrayInteger array) { Script.Literal("this.set({0})", array); }
        public void Set5(JSArrayInteger array, int offset) { Script.Literal("this.set({0}, {1})", array, offset); }

        public Int8Array Slice(int offset, int length) { return (Int8Array)Script.Literal("this.slice({0}, {1})", offset, length); }
    }
}
