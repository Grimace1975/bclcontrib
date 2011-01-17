using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    [IgnoreNamespace, Imported]
    public class Int16Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 2;

        public Int16Array(ArrayBuffer buffer) { }
        public Int16Array(ArrayBuffer buffer, int byteOffset) { }
        public Int16Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Int16Array(Int16Array array) { }
        public Int16Array(int size) { }
        public Int16Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public short this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Int16Array array) { }
        public void Set(Int16Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Int16Array Slice(int offset, int length) { return null; }
    }
}
