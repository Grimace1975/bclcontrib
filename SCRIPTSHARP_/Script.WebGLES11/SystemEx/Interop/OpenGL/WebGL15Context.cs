#if !CODE_ANALYSIS
namespace System.Interop.OpenGL
#else
using System;
using SystemEx.IO;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class WebGL15Context
    {
        // GL_VERSION_1_0
        public void CullFace(uint mode);
        public void FrontFace(uint mode);
        public void Hint(uint target, uint mode);
        public void LineWidth(float width);
        public void PointSize(float size);
        public void PolygonMode(uint face, uint mode);
        public void Scissor(int x, int y, int width, int height);
        public void TexParameterf(uint target, uint pname, float param);
        public void TexParameterfv(uint target, uint pname, Stream @params);
        public void TexParameteri(uint target, uint pname, int param);
        public void TexParameteriv(uint target, uint pname, Stream @params);
        public void TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, Stream pixels);
        public void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, Stream pixels);
        public void DrawBuffer(uint mode);
        public void Clear(uint mask);
        public void ClearColor(float red, float green, float blue, float alpha);
        public void ClearStencil(int s);
        public void ClearDepth(double depth);
        public void StencilMask(uint mask);
        public void ColorMask(bool red, bool green, bool blue, bool alpha);
        public void DepthMask(bool flag);
        public void Disable(uint cap);
        public void Enable(uint cap);
        public void Finish();
        public void Flush();
        public void BlendFunc(uint sfactor, uint dfactor);
        public void LogicOp(uint opcode);
        public void StencilFunc(uint func, int @ref, uint mask);
        public void StencilOp(uint fail, uint zfail, uint zpass);
        public void DepthFunc(uint func);
        public void PixelStoref(uint pname, float param);
        public void PixelStorei(uint pname, int param);
        public void ReadBuffer(uint mode);
        public void ReadPixels(int x, int y, int width, int height, uint format, uint type, Stream pixels);
        public void GetBooleanv(uint pname, Stream @params);
        public void GetDoublev(uint pname, Stream @params);
        public uint GetError();
        public void GetFloatv(uint pname, Stream @params);
        public void GetIntegerv(uint pname, Stream @params);
        public string GetString(uint name);
        public void GetTexImage(uint target, int level, uint format, uint type, Stream pixels);
        public void GetTexParameterfv(uint target, uint pname, Stream @params);
        public void GetTexParameteriv(uint target, uint pname, Stream @params);
        public void GetTexLevelParameterfv(uint target, int level, uint pname, Stream @params);
        public void GetTexLevelParameteriv(uint target, int level, uint pname, Stream @params);
        bool IsEnabled(uint cap);
        public void DepthRange(double near, double far);
        public void Viewport(int x, int y, int width, int height);

        // GL_VERSION_1_1
        public void DrawArrays(uint mode, int first, int count);
        public void DrawElements(uint mode, int count, uint type, Stream indices);
        public void GetPointerv(uint pname, Stream @params);
        public void PolygonOffset(float factor, float units);
        public void CopyTexImage1D(uint target, int level, uint internalformat, int x, int y, int width, int border);
        public void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border);
        public void CopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);
        public void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);
        public void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, Stream pixels);
        public void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, Stream pixels);
        public void BindTexture(uint target, uint texture);
        public void DeleteTextures(int n, Stream textures);
        public void GenTextures(int n, Stream textures);
        public bool IsTexture(uint texture);

        // GL_VERSION_1_2
        public void BlendColor(float red, float green, float blue, float alpha);
        public void BlendEquation(uint mode);
        public void DrawRangeElements(uint mode, uint start, uint end, int count, uint type, Stream indices);
        public void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, Stream pixels);
        public void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, Stream pixels);
        public void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);

        // GL_VERSION_1_3
        public void ActiveTexture(uint texture);
        public void SampleCoverage(float value, bool invert);
        public void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, Stream data);
        public void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, Stream data);
        public void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, Stream data);
        public void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, Stream data);
        public void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, Stream data);
        public void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, Stream data);
        public void GetCompressedTexImage(uint target, int level, Stream img);

        // GL_VERSION_1_4
        public void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);
        public void MultiDrawArrays(uint mode, int[] first, int[] count, int primcount);
        public void MultiDrawElements(uint mode, int[] count, uint type, Stream indices, int primcount);
        public void PointParameterf(uint pname, float param);
        public void PointParameterfv(uint pname, Stream @params);
        public void PointParameteri(uint pname, int param);
        public void PointParameteriv(uint pname, Stream @params);

        // GL_VERSION_1_5
        public void GenQueries(int n, Stream ids);
        public void DeleteQueries(int n, Stream ids);
        public bool IsQuery(uint id);
        public void BeginQuery(uint target, uint id);
        public void EndQuery(uint target);
        public void GetQueryiv(uint target, uint pname, Stream @params);
        public void GetQueryObjectiv(uint id, uint pname, Stream @params);
        public void GetQueryObjectuiv(uint id, uint pname, Stream @params);
        public void BindBuffer(uint target, uint buffer);
        public void DeleteBuffers(int n, Stream buffers);
        public void GenBuffers(int n, Stream buffers);
        public bool IsBuffer(uint buffer);
        public void BufferData(uint target, long size, Stream data, uint usage);
        public void BufferSubData(uint target, long offset, long size, Stream data);
        public void GetBufferSubData(uint target, long offset, long size, Stream data);
        public Stream MapBuffer(uint target, uint access);
        public bool UnmapBuffer(uint target);
        public void GetBufferParameteriv(uint target, uint pname, Stream @params);
        public void GetBufferPointerv(uint target, uint pname, Stream @params);
    }
}
