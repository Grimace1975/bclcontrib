using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    [IgnoreNamespace, Imported]
    public class Float32Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        public Float32Array(ArrayBuffer buffer) { }
        public Float32Array(ArrayBuffer buffer, int byteOffset) { }
        public Float32Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Float32Array(Float32Array array) { }
        public Float32Array(int size) { }
        public Float32Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public float this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Float32Array array) { }
        public void Set(Float32Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Float32Array Slice(int offset, int length) { return null; }
    }
}
