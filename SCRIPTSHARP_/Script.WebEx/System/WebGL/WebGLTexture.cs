using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLTexture interface represents an OpenGL Texture Object. The underlying object is created as if by calling glGenTextures (OpenGL ES 2.0 §3.7.13, man page)  , bound as if by calling glBindTexture (OpenGL ES 2.0 §3.7.13, man page)  and destroyed as if by calling glDeleteTextures (OpenGL ES 2.0 §3.7.13, man page). 
    /// </summary>
    [IgnoreNamespace, Imported]
    public class WebGLTexture : WebGLObject
    {
        protected WebGLTexture() { }
    }
}