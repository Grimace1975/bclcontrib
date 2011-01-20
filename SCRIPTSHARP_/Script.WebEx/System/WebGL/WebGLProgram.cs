using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLProgram interface represents an OpenGL Program Object. The underlying object is created as if by calling glCreateProgram (OpenGL ES 2.0 §2.10.3, man page)  , used as if by calling glUseProgram (OpenGL ES 2.0 §2.10.3, man page)  and destroyed as if by calling glDeleteProgram (OpenGL ES 2.0 §2.10.3, man page). 
    /// </summary>
    [IgnoreNamespace, Imported]
    public class WebGLProgram : WebGLObject
    {
        protected WebGLProgram() { }
    }
}