using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLFramebuffer interface represents an OpenGL Framebuffer Object. The underlying object is created as if by calling glGenFramebuffers (OpenGL ES 2.0 §4.4.1, man page)  , bound as if by calling glBindFramebuffer (OpenGL ES 2.0 §4.4.1, man page)  and destroyed as if by calling glDeleteFramebuffers (OpenGL ES 2.0 §4.4.1, man page). 
    /// </summary>
    [IgnoreNamespace, Imported]
    public class WebGLFramebuffer : WebGLObject
    {
        protected WebGLFramebuffer() { }
    }
}