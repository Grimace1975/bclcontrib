#if !CODE_ANALYSIS
namespace System.Interop.OpenGL
#else
using System.TypedArrays;
using System.Interop.OpenGL;
using System;
namespace SystemEx.Interop.OpenGL
#endif
{
#if CODE_ANALYSIS
    internal sealed class WebGLES11BufferData : Record
#else
    internal struct WebGLES11BufferData
#endif
    {
        public ArrayBufferView ToBind;
        public WebGLBuffer Buffer;
        public int Stride;
        public int Size;
        public uint Type;
        public int ByteSize;
        public bool Normalized;
    }
}
