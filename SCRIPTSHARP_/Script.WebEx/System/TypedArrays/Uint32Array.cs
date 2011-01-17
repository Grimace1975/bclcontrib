using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    [IgnoreNamespace, Imported]
    public class Uint32Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 4;

        public Uint32Array(ArrayBuffer buffer) { }
        public Uint32Array(ArrayBuffer buffer, int byteOffset) { }
        public Uint32Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Uint32Array(Uint32Array array) { }
        public Uint32Array(int size) { }
        public Uint32Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public uint this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Uint32Array array) { }
        public void Set(Uint32Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Uint32Array Slice(int offset, int length) { return null; }
    }
}
