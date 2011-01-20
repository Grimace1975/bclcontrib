using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLShader interface represents an OpenGL Shader Object. The underlying object is created as if by calling glCreateShader (OpenGL ES 2.0 §2.10.1, man page)  , attached to a Program as if by calling glAttachShader (OpenGL ES 2.0 §2.10.3, man page)  and destroyed as if by calling glDeleteShader (OpenGL ES 2.0 §2.10.1, man page). 
    /// </summary>
    [IgnoreNamespace, Imported]
    public class WebGLShader : WebGLObject
    {
        protected WebGLShader() { }
    }
}