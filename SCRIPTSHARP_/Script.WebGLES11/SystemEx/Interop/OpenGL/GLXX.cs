#if !CODE_ANALYSIS
using System.IO;
using System.Collections;
namespace System.Interop.OpenGL
#else
using System.TypedArrays;
using System.Interop.OpenGL;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class GLXX
    {
        /* Item1 */
        public const uint ARRAY_POSITION = 0;
        public const uint ARRAY_COLOR = 1;
        public const uint ARRAY_TEXCOORD_0 = 2;
        public const uint ARRAY_TEXCOORD_1 = 3;

        /* WebGL-transition enums */
        public const uint QUADS = 0x0007;
        public const uint POLYGON = GLES20.TRIANGLE_FAN;
        public const uint SIMPLE_TEXUTRED_QUAD = 0xFFFFFFFF;
    }
}
