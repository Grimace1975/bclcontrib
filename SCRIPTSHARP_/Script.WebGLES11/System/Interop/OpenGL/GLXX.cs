using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public abstract class GLXX
    {
        /* WebGL-transition enums */
        public const uint QUADS = 0x0007;
        public const uint POLYGON = GLES20.TRIANGLE_FAN;
        public const uint SIMPLE_TEXUTRED_QUAD = 0xFFFFFFFF;
    }
}
