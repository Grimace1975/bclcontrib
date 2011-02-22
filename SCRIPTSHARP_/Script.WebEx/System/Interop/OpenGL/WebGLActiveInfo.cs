using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
    /// <summary>
    /// The WebGLActiveInfo interface represents the information returned from the getActiveAttrib and getActiveUniform calls. 
    /// </summary>
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public class WebGLActiveInfo : WebGLObject
    {
        protected WebGLActiveInfo() { }

        /// <summary>
        /// The size of the requested variable. 
        /// </summary>
#if CODE_ANALYSIS
        [IntrinsicProperty]
#endif
        public virtual long Size
        {
            get { return 0; }
        }

        /// <summary>
        /// The data type of the requested variable.
        /// </summary>
#if CODE_ANALYSIS
        [IntrinsicProperty]
#endif
        public virtual ulong Type
        {
            get { return 0; }
        }

        /// <summary>
        /// The name of the requested variable.
        /// </summary>
#if CODE_ANALYSIS
        [IntrinsicProperty]
#endif
        public virtual string Name
        {
            get { return null; }
        }
    }
}