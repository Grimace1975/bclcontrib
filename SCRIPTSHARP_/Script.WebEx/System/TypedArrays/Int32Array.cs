using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    // [TypedArrays] https://cvs.khronos.org/svn/repos/registry/trunk/public/webgl/doc/spec/TypedArray-spec.html
    [IgnoreNamespace, Imported]
    public class Int32Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        public Int32Array(ArrayBuffer buffer) { }
        public Int32Array(ArrayBuffer buffer, int byteOffset) { }
        public Int32Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Int32Array(Int32Array array) { }
        public Int32Array(int size) { }
        public Int32Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public int this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Int32Array array) { }
        public void Set(Int32Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Int32Array Slice(int offset, int length) { return null; }
    }
}
