using System;
using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    /// <summary>
    /// The ArrayBufferView type holds information shared among all of the types of views of ArrayBuffers.
    /// Taken from the Khronos TypedArrays Draft Spec as of Aug 30, 2010.
    /// </summary>
    [IgnoreNamespace, Imported]
    public class ArrayBufferView
    {
        protected ArrayBufferView() { }

        /// <summary>
        /// The ArrayBuffer that this ArrayBufferView references.
        /// </summary>
        /// <returns></returns>
        [IntrinsicProperty]
        public ArrayBuffer Buffer
        {
            get { return null; }
        }

        /// <summary>
        /// The offset of this ArrayBufferView from the start of its ArrayBuffer, in bytes, as fixed at construction time.
        /// </summary>
        /// <returns></returns>
        [IntrinsicProperty]
        public int ByteLength
        {
            get { return 0; }
        }

        /// <summary>
        /// The length of the ArrayBufferView in bytes, as fixed at construction time.
        /// </summary>
        /// <returns></returns>
        [IntrinsicProperty]
        public int ByteOffset
        {
            get { return 0; }
        }
    }
}
