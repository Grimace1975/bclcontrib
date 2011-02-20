//[Khronos]http://www.khronos.org/registry/webgl/specs/latest/
#if !CODE_ANALYSIS
#else
using System.Runtime.CompilerServices;
using System.TypedArrays;
using System.Html.Media.Graphics;
using System.Html;
#endif
namespace System.Interop.OpenGL
{
#if !CODE_ANALYSIS
    public delegate int[] ArrayBufferView();
#endif

#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public class WebGLRenderingContext : GLES20
    {
        protected WebGLRenderingContext() { }

#if CODE_ANALYSIS
        public virtual CanvasElementEx Canvas
        {
            get { return null; }
        }
#endif
        public virtual long DrawingBufferWidth
        {
            get { return 0; }
        }
        public virtual long DrawingBufferHeight
        {
            get { return 0; }
        }

        public virtual WebGLContextAttributes GetContextAttributes() { return null; }
        public virtual bool IsContextLost() { return false; }

        public virtual string[] GetSupportedExtensions() { return null; }
        public virtual object GetExtension(string name) { return null; }

        public virtual void ActiveTexture(uint texture) { }
        public virtual void AttachShader(WebGLProgram program, WebGLShader shader) { }
        public virtual void BindAttribLocation(WebGLProgram program, int index, string name) { }
        public virtual void BindBuffer(uint target, WebGLBuffer buffer) { }
        public virtual void BindFramebuffer(uint target, WebGLFramebuffer framebuffer) { }
        public virtual void BindRenderbuffer(uint target, WebGLRenderbuffer renderbuffer) { }
        public virtual void BindTexture(uint target, WebGLTexture texture) { }
        public virtual void BlendColor(float red, float green, float blue, float alpha) { }
        public virtual void BlendEquation(uint mode) { }
        public virtual void BlendEquationSeparate(uint modeRGB, uint modeAlpha) { }
        public virtual void BlendFunc(uint sfactor, uint dfactor) { }
        public virtual void BlendFuncSeparate(uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha) { }

        public virtual void BufferData(uint target, int size, int usage) { }
        public virtual void BufferData(uint target, ArrayBufferView data, int usage) { }
        public virtual void BufferSubData(uint target, long offset, ArrayBufferView data) { }
#if CODE_ANALYSIS
        public void BufferData(uint target, ArrayBuffer data, int usage) { }
        public void BufferSubData(uint target, long offset, ArrayBuffer data) { }
#endif

        public virtual uint CheckFramebufferStatus(uint target) { return 0; }
        public virtual void Clear(uint mask) { }
        public virtual void ClearColor(float red, float green, float blue, float alpha) { }
        public virtual void ClearDepth(float depth) { }
        public virtual void ClearStencil(int s) { }
        public virtual void ColorMask(bool red, bool green, bool blue, bool alpha) { }
        public virtual void CompileShader(WebGLShader shader) { }

        public virtual void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border) { }
        public virtual void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height) { }

        public virtual WebGLBuffer CreateBuffer() { return null; }
        public virtual WebGLFramebuffer CreateFramebuffer() { return null; }
        public virtual WebGLProgram CreateProgram() { return null; }
        public virtual WebGLRenderbuffer CreateRenderbuffer() { return null; }
        public virtual WebGLShader CreateShader(uint type) { return null; }
        public virtual WebGLTexture CreateTexture() { return null; }

        public virtual void CullFace(uint mode) { }

        public virtual void DeleteBuffer(WebGLBuffer buffer) { }
        public virtual void DeleteFramebuffer(WebGLFramebuffer framebuffer) { }
        public virtual void DeleteProgram(WebGLProgram program) { }
        public virtual void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer) { }
        public virtual void DeleteShader(WebGLShader shader) { }
        public virtual void DeleteTexture(WebGLTexture texture) { }

        public virtual void DepthFunc(uint func) { }
        public virtual void DepthMask(bool flag) { }
        public virtual void DepthRange(float zNear, float zFar) { }
        public virtual void DetachShader(WebGLProgram program, WebGLShader shader) { }
        public virtual void Disable(uint cap) { }
        public virtual void DisableVertexAttribArray(uint index) { }
        public virtual void DrawArrays(uint mode, uint first, int count) { }
        public virtual void DrawElements(uint mode, int count, uint type, long offset) { }

        public virtual void Enable(uint cap) { }
        public virtual void EnableVertexAttribArray(uint index) { }
        public virtual void Finish() { }
        public virtual void Flush() { }
        public virtual void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, WebGLRenderbuffer renderbuffer) { }
        public virtual void FramebufferTexture2D(uint target, uint attachment, uint textarget, WebGLTexture texture, int level) { }
        public virtual void FrontFace(uint mode) { }

        public virtual void GenerateMipmap(uint target) { }

        public virtual WebGLActiveInfo GetActiveAttrib(WebGLProgram program, int index) { return null; }
        public virtual WebGLActiveInfo GetActiveUniform(WebGLProgram program, int index) { return null; }
        public virtual WebGLShader[] GetAttachedShaders(WebGLProgram program) { return null; }

        public virtual int GetAttribLocation(WebGLProgram program, string name) { return 0; }

        public virtual object GetParameter(uint pname) { return null; }
        public virtual object GetBufferParameter(uint target, uint pname) { return null; }

        public virtual uint GetError() { return 0; }

        public virtual object GetFramebufferAttachmentParameter(uint target, uint attachment, uint pname) { return null; }
        public virtual object GetProgramParameter(WebGLProgram program, uint pname) { return null; }
        public virtual string GetProgramInfoLog(WebGLProgram program) { return null; }
        public virtual object GetRenderbufferParameter(uint target, uint pname) { return null; }
        public virtual object GetShaderParameter(WebGLShader shader, uint pname) { return null; }
        public virtual string GetShaderInfoLog(WebGLShader shader) { return null; }

        public virtual string GetShaderSource(WebGLShader shader) { return null; }

        public virtual object GetTexParameter(uint target, uint pname) { return null; }

        public virtual object GetUniform(WebGLProgram program, WebGLUniformLocation location) { return null; }

        public virtual WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name) { return null; }

        public virtual object GetVertexAttrib(uint index, uint pname) { return null; }

        public virtual long GetVertexAttribOffset(uint index, uint pname) { return 0; }

        public virtual void Hint(uint target, uint mode) { }
        public virtual bool IsBuffer(WebGLBuffer buffer) { return false; }
        public virtual bool IsEnabled(uint cap) { return false; }
        public virtual bool IsFramebuffer(WebGLFramebuffer framebuffer) { return false; }
        public virtual bool IsProgram(WebGLProgram program) { return false; }
        public virtual bool IsRenderbuffer(WebGLRenderbuffer renderbuffer) { return false; }
        public virtual bool IsShader(WebGLShader shader) { return false; }
        public virtual bool IsTexture(WebGLTexture texture) { return false; }
        public virtual void LineWidth(float width) { }
        public virtual void LinkProgram(WebGLProgram program) { }
        public virtual void PixelStorei(uint pname, uint param) { }
        public virtual void PolygonOffset(float factor, float units) { }

        public virtual void ReadPixels(int x, int y, int width, int height, uint format, uint type, ArrayBufferView pixels) { }

        public virtual void RenderbufferStorage(uint target, uint internalformat, int width, int height) { }
        public virtual void SampleCoverage(float value, bool invert) { }
        public virtual void Scissor(int x, int y, int width, int height) { }

        public virtual void ShaderSource(WebGLShader shader, string source) { }

        public virtual void StencilFunc(uint func, int @ref, uint mask) { }
        public virtual void StencilFuncSeparate(uint face, int func, int @ref, uint mask) { }
        public virtual void StencilMask(uint mask) { }
        public virtual void StencilMaskSeparate(uint face, uint mask) { }
        public virtual void StencilOp(uint fail, uint zfail, uint zpass) { }
        public virtual void StencilOpSeparate(int face, int fail, int zfail, int zpass) { }

        public virtual void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, ArrayBufferView pixels) { }
#if CODE_ANALYSIS
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, ImageData pixels) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, ImageElement image) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, CanvasElement canvas) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, CanvasElementEx canvas) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, VideoElement video) { }
#endif

        public virtual void TexParameterf(uint target, uint pname, float param) { }
        public virtual void TexParameteri(uint target, uint pname, int param) { }

        public virtual void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, ArrayBufferView pixels) { }
#if CODE_ANALYSIS
        public void TexSubImage2D(uint target, int level, int xoffset, int yoffset, uint format, uint type, ImageData pixels) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, ImageElement image) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, CanvasElement canvas) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, CanvasElementEx canvas) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, VideoElement video) { }
#endif

        public virtual void Uniform1f(WebGLUniformLocation location, float x) { }
        public virtual void Uniform1fv(WebGLUniformLocation location, float[] v) { }
        public virtual void Uniform1i(WebGLUniformLocation location, int x) { }
        public virtual void Uniform1iv(WebGLUniformLocation location, int[] v) { }
        public virtual void Uniform2f(WebGLUniformLocation location, float x, float y) { }
        public virtual void Uniform2fv(WebGLUniformLocation location, float[] v) { }
        public virtual void Uniform2i(WebGLUniformLocation location, int x, int y) { }
        public virtual void Uniform2iv(WebGLUniformLocation location, int[] v) { }
        public virtual void Uniform3f(WebGLUniformLocation location, float x, float y, float z) { }
        public virtual void Uniform3fv(WebGLUniformLocation location, float[] v) { }
        public virtual void Uniform3i(WebGLUniformLocation location, int x, int y, int z) { }
        public virtual void Uniform3iv(WebGLUniformLocation location, int[] v) { }
        public virtual void Uniform4f(WebGLUniformLocation location, float x, float y, float z, float w) { }
        public virtual void Uniform4fv(WebGLUniformLocation location, float[] v) { }
        public virtual void Uniform4i(WebGLUniformLocation location, int x, int y, int z, int w) { }
#if CODE_ANALYSIS
        public void Uniform1fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform1iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform2fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform2iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform3fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform3iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform4fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform4iv(WebGLUniformLocation location, Int32Array v) { }
#endif

        public virtual void Uniform4iv(WebGLUniformLocation location, int[] v) { }

        public virtual void UniformMatrix2fv(WebGLUniformLocation location, bool transpose, float[] value) { }
        public virtual void UniformMatrix3fv(WebGLUniformLocation location, bool transpose, float[] value) { }
        public virtual void UniformMatrix4fv(WebGLUniformLocation location, bool transpose, float[] value) { }
#if CODE_ANALYSIS
        public void UniformMatrix2fv(WebGLUniformLocation location, bool transpose, Float32Array value) { }
        public void UniformMatrix3fv(WebGLUniformLocation location, bool transpose, Float32Array value) { }
        public void UniformMatrix4fv(WebGLUniformLocation location, bool transpose, Float32Array value) { }
#endif

        public virtual void UseProgram(WebGLProgram program) { }
        public virtual void ValidateProgram(WebGLProgram program) { }

        public virtual void VertexAttrib1f(uint indx, float x) { }
        public virtual void VertexAttrib1fv(uint indx, float[] values) { }
        public virtual void VertexAttrib2f(uint indx, float x, float y) { }
        public virtual void VertexAttrib2fv(uint indx, float[] values) { }
        public virtual void VertexAttrib3f(uint indx, float x, float y, float z) { }
        public virtual void VertexAttrib3fv(uint indx, float[] values) { }
        public virtual void VertexAttrib4f(uint indx, float x, float y, float z, float w) { }
        public virtual void VertexAttrib4fv(uint indx, float[] values) { }
#if CODE_ANALYSIS
        public void VertexAttrib1fv(uint indx, Float32Array values) { }
        public void VertexAttrib2fv(uint indx, Float32Array values) { }
        public void VertexAttrib3fv(uint indx, Float32Array values) { }
        public void VertexAttrib4fv(uint indx, Float32Array values) { }
#endif
        public virtual void VertexAttribPointer(uint indx, int size, uint type, bool normalized, int stride, long offset) { }

        public virtual void Viewport(int x, int y, int width, int height) { }
    }
}