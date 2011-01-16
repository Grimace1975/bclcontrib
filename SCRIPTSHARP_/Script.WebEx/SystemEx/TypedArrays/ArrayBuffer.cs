using System;
namespace SystemEx.TypedArrays
{
    /// <summary>
    /// The ArrayBuffer type describes a buffer used to store data for the TypedArray interface and its subclasses.
    /// Taken from the Khronos TypedArrays Draft Spec as of Aug 30, 2010.
    /// </summary>
    public class ArrayBuffer
    {
        protected ArrayBuffer() { }

        /// <summary>
        /// Creates a new ArrayBuffer of the given length in bytes. The contents of the ArrayBuffer are initialized to 0. 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static ArrayBuffer Create(int length) { return (ArrayBuffer)Script.Literal("new ArrayBuffer({0})", length); }

        /// <summary>
        /// The length of the ArrayBuffer in bytes, as fixed at construction time.
        /// </summary>
        /// <returns></returns>
        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
        }
    }
}
