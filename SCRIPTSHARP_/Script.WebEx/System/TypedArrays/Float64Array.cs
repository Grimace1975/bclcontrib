using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    [IgnoreNamespace, Imported]
    public class Float64Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        public Float64Array(ArrayBuffer buffer) { }
        public Float64Array(ArrayBuffer buffer, int byteOffset) { }
        public Float64Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Float64Array(Float64Array array) { }
        public Float64Array(int size) { }
        public Float64Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public double this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Float64Array array) { }
        public void Set(Float64Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Float64Array Slice(int offset, int length) { return null; }
    }
}
