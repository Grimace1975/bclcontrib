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
    internal class WebGLES11BufferData
    {
        public ArrayBufferView ToBind;
        public WebGLBuffer Buffer;
        public int ByteStride;
        public int Size;
        public int Type;
        public int ByteSize;
        public bool Normalize;
    }
}
