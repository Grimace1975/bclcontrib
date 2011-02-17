using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
    /// <summary>
    /// The WebGLObject interface is the parent interface for all GL objects. 
    /// </summary>
    // [W3] https://cvs.khronos.org/svn/repos/registry/trunk/public/webgl/doc/spec/WebGL-spec.html
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public class WebGLObject
    {
        protected WebGLObject() { }
    }
}