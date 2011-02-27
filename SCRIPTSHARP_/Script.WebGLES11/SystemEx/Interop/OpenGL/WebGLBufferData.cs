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
    internal sealed class WebGLBufferData : Record
#else
    internal struct WebGLBufferData
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
