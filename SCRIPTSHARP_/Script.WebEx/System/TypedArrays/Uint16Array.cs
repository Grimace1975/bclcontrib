using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    [IgnoreNamespace, Imported]
    public class Uint16Array : ArrayBufferView
    {
        public const int BYTES_PER_ELEMENT = 2;

        public Uint16Array(ArrayBuffer buffer) { }
        public Uint16Array(ArrayBuffer buffer, int byteOffset) { }
        public Uint16Array(ArrayBuffer buffer, int byteOffset, int length) { }
        public Uint16Array(Uint16Array array) { }
        public Uint16Array(int size) { }
        public Uint16Array(JSArrayInteger data) { }

        [IntrinsicProperty]
        public ushort this[int index]
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }

        public void Set(Uint16Array array) { }
        public void Set(Uint16Array array, int offset) { }
        public void Set(JSArrayInteger array) { }
        public void Set(JSArrayInteger array, int offset) { }

        public Uint16Array Slice(int offset, int length) { return null; }
    }
}
