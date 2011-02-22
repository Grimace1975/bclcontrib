#if !CODE_ANALYSIS
using System.IO;
using System.Collections;
namespace System.Interop.OpenGL
#else
using System.TypedArrays;
using System.Interop.OpenGL;
using System;
namespace SystemEx.Interop.OpenGL
#endif
{
    internal sealed class WebGLES11BufferData : Record
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
