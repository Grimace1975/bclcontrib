using System.Runtime.CompilerServices;
namespace System.TypedArrays
{
    /// <summary>
    /// The ArrayBuffer type describes a buffer used to store data for the TypedArray interface and its subclasses.
    /// Taken from the Khronos TypedArrays Draft Spec as of Aug 30, 2010.
    /// </summary>
    [IgnoreNamespace, Imported]
    public class ArrayBuffer
    {
        /// <summary>
        /// Creates a new ArrayBuffer of the given length in bytes. The contents of the ArrayBuffer are initialized to 0. 
        /// </summary>
        /// <param name="length"></param>
        public ArrayBuffer(int length) { }

        /// <summary>
        /// The length of the ArrayBuffer in bytes, as fixed at construction time.
        /// </summary>
        /// <returns></returns>
        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
        }
    }
}
