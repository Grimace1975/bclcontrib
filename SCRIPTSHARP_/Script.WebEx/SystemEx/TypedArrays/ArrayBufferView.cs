using System;
namespace SystemEx.TypedArrays
{
    /// <summary>
    /// The ArrayBufferView type holds information shared among all of the types of views of ArrayBuffers.
    /// Taken from the Khronos TypedArrays Draft Spec as of Aug 30, 2010.
    /// </summary>
    public class ArrayBufferView
    {
        protected ArrayBufferView() { }

        /// <summary>
        /// The ArrayBuffer that this ArrayBufferView references.
        /// </summary>
        /// <returns></returns>
        public ArrayBuffer Buffer
        {
            get { return (ArrayBuffer)Script.Literal("this.buffer"); }
        }

        /// <summary>
        /// The offset of this ArrayBufferView from the start of its ArrayBuffer, in bytes, as fixed at construction time.
        /// </summary>
        /// <returns></returns>
        public int ByteLength
        {
            get { return (int)Script.Literal("this.byteLength"); }
        }

        /// <summary>
        /// The length of the ArrayBufferView in bytes, as fixed at construction time.
        /// </summary>
        /// <returns></returns>
        public int ByteOffset
        {
            get { return (int)Script.Literal("this.byteOffset"); }
        }
    }
}
