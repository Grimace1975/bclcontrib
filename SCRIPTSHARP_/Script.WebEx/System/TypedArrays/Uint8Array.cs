using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    [IgnoreNamespace, Imported]
    public class Uint8Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 1;

        public Uint8Array(ArrayBuffer buffer) { }
        public Uint8Array(ArrayBuffer buffer, int byteOffset) { }
        public Uint8Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Uint8Array(Uint8Array array) { }
        public Uint8Array(int size) { }
        public Uint8Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public byte this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Uint8Array array) { }
        public void Set(Uint8Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Uint8Array Slice(int offset, int length) { return null; }
    }
}
