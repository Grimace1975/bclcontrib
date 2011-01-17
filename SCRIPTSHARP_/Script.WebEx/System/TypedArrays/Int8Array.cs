using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    /// <summary>
    ///  The typed array view types represent a view of an ArrayBuffer that allows for indexing and manipulation. The length of each of these is fixed.
    ///  Taken from the Khronos TypedArrays Draft Spec as of Aug 30, 2010.
    /// </summary>
    [IgnoreNamespace, Imported]
    public class Int8Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 1;

        public Int8Array(ArrayBuffer buffer) { }
        public Int8Array(ArrayBuffer buffer, int byteOffset) { }
        public Int8Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Int8Array(Int8Array array) { }
        public Int8Array(int size) { }
        public Int8Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public sbyte this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Int8Array array) {  }
        public void Set(Int8Array array, int offset) {  }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Int8Array Slice(int offset, int length) { return null; }
    }
}
