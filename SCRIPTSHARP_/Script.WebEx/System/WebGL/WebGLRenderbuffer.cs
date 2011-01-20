using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLRenderbuffer interface represents an OpenGL Renderbuffer Object. The underlying object is created as if by calling glGenRenderbuffers (OpenGL ES 2.0 §4.4.3, man page)  , bound as if by calling glBindRenderbuffer (OpenGL ES 2.0 §4.4.3, man page)  and destroyed as if by calling glDeleteRenderbuffers (OpenGL ES 2.0 §4.4.3, man page).
    /// </summary>
    [IgnoreNamespace, Imported]
    public class WebGLRenderbuffer : WebGLObject
    {
        protected WebGLRenderbuffer() { }
    }
}