using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLActiveInfo interface represents the information returned from the getActiveAttrib and getActiveUniform calls. 
    /// </summary>
    [IgnoreNamespace, Imported]
    public class WebGLActiveInfo : WebGLObject
    {
        protected WebGLActiveInfo() { }

        /// <summary>
        /// The size of the requested variable. 
        /// </summary>
        [IntrinsicProperty]
        public long Size
        {
            get { return 0; }
        }

        /// <summary>
        /// The data type of the requested variable.
        /// </summary>
        [IntrinsicProperty]
        public ulong Type
        {
            get { return 0; }
        }

        /// <summary>
        /// The name of the requested variable.
        /// </summary>
        [IntrinsicProperty]
        public string Name
        {
            get { return null; }
        }
    }
}