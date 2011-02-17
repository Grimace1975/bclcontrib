using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
    /// <summary>
    /// The WebGLBuffer interface represents an OpenGL Buffer Object. The underlying object is created as if by calling glGenBuffers (OpenGL ES 2.0 §2.9, man page)  , bound as if by calling glBindBuffer (OpenGL ES 2.0 §2.9, man page)  and destroyed as if by calling glDeleteBuffers (OpenGL ES 2.0 §2.9, man page).
    /// </summary>
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public class WebGLBuffer : WebGLObject
    {
        protected WebGLBuffer() { }
    }
}