#if !CODE_ANALYSIS
namespace System.Interop.OpenGL
#else
using System;
using SystemEx.IO;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class GL15Context
    {
        // GL_VERSION_2_0
        public abstract void BlendEquationSeparate(uint modeRGB, uint modeAlpha);
        public abstract void DrawBuffers(int n, Stream bufs);
        public abstract void StencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass);
        public abstract void StencilFuncSeparate(uint face, uint func, int @ref, uint mask);
        public abstract void StencilMaskSeparate(uint face, uint mask);
        public abstract void AttachShader(uint program, uint shader);
        public abstract void BindAttribLocation(uint program, uint index, string name);
        public abstract void CompileShader(uint shader);
        public abstract uint CreateProgram();
        public abstract uint CreateShader(uint type);
        public abstract void DeleteProgram(uint program);
        public abstract void DeleteShader(uint shader);
        public abstract void DetachShader(uint program, uint shader);
        public abstract void DisableVertexAttribArray(uint index);
        public abstract void EnableVertexAttribArray(uint index);
        public abstract void GetActiveAttrib(uint program, uint index, int bufSize, out int length, out int size, out uint type, out string name);
        public abstract void GetActiveUniform(uint program, uint index, int bufSize, out int length, out int size, out uint type, out string name);
        public abstract void GetAttachedShaders(uint program, int maxCount, out int count, out Stream obj);
        public abstract int GetAttribLocation(uint program, string name);
        public abstract void GetProgramiv(uint program, uint pname, Stream @params);
        public abstract void GetProgramInfoLog(uint program, int bufSize, out int length, out string infoLog);
        public abstract void GetShaderiv(uint shader, uint pname, Stream @params);
        public abstract void GetShaderInfoLog(uint shader, int bufSize, out int length, out string infoLog);
        public abstract void GetShaderSource(uint shader, int bufSize, out int length, out string source);
        public abstract int GetUniformLocation(uint program, string name);
        public abstract void GetUniformfv(uint program, int location, Stream @params);
        public abstract void GetUniformiv(uint program, int location, Stream @params);
        public abstract void GetVertexAttribdv(uint index, uint pname, Stream @params);
        public abstract void GetVertexAttribfv(uint index, uint pname, Stream @params);
        public abstract void GetVertexAttribiv(uint index, uint pname, Stream @params);
        public abstract void GetVertexAttribPointerv(uint index, uint pname, Stream pointer);
        public abstract bool IsProgram(uint program);
        public abstract bool IsShader(uint shader);
        public abstract void LinkProgram(uint program);
        public abstract void ShaderSource(uint shader, int count, out string s, out int length);
        public abstract void UseProgram(uint program);
        public abstract void Uniform1f(int location, float v0);
        public abstract void Uniform2f(int location, float v0, float v1);
        public abstract void Uniform3f(int location, float v0, float v1, float v2);
        public abstract void Uniform4f(int location, float v0, float v1, float v2, float v3);
        public abstract void Uniform1i(int location, int v0);
        public abstract void Uniform2i(int location, int v0, int v1);
        public abstract void Uniform3i(int location, int v0, int v1, int v2);
        public abstract void Uniform4i(int location, int v0, int v1, int v2, int v3);
        public abstract void Uniform1fv(int location, int count, Stream value);
        public abstract void Uniform2fv(int location, int count, Stream value);
        public abstract void Uniform3fv(int location, int count, Stream value);
        public abstract void Uniform4fv(int location, int count, Stream value);
        public abstract void Uniform1iv(int location, int count, Stream value);
        public abstract void Uniform2iv(int location, int count, Stream value);
        public abstract void Uniform3iv(int location, int count, Stream value);
        public abstract void Uniform4iv(int location, int count, Stream value);
        public abstract void UniformMatrix2fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix3fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix4fv(int location, int count, bool transpose, Stream value);
        public abstract void ValidateProgram(uint program);
        public abstract void VertexAttrib1d(uint index, double x);
        public abstract void VertexAttrib1dv(uint index, Stream v);
        public abstract void VertexAttrib1f(uint index, float x);
        public abstract void VertexAttrib1fv(uint index, Stream v);
        public abstract void VertexAttrib1s(uint index, short x);
        public abstract void VertexAttrib1sv(uint index, Stream v);
        public abstract void VertexAttrib2d(uint index, double x, double y);
        public abstract void VertexAttrib2dv(uint index, Stream v);
        public abstract void VertexAttrib2f(uint index, float x, float y);
        public abstract void VertexAttrib2fv(uint index, Stream v);
        public abstract void VertexAttrib2s(uint index, short x, short y);
        public abstract void VertexAttrib2sv(uint index, Stream v);
        public abstract void VertexAttrib3d(uint index, double x, double y, double z);
        public abstract void VertexAttrib3dv(uint index, Stream v);
        public abstract void VertexAttrib3f(uint index, float x, float y, float z);
        public abstract void VertexAttrib3fv(uint index, Stream v);
        public abstract void VertexAttrib3s(uint index, short x, short y, short z);
        public abstract void VertexAttrib3sv(uint index, Stream v);
        public abstract void VertexAttrib4Nbv(uint index, Stream v);
        public abstract void VertexAttrib4Niv(uint index, Stream v);
        public abstract void VertexAttrib4Nsv(uint index, Stream v);
        public abstract void VertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w);
        public abstract void VertexAttrib4Nubv(uint index, Stream v);
        public abstract void VertexAttrib4Nuiv(uint index, Stream v);
        public abstract void VertexAttrib4Nusv(uint index, Stream v);
        public abstract void VertexAttrib4bv(uint index, Stream v);
        public abstract void VertexAttrib4d(uint index, double x, double y, double z, double w);
        public abstract void VertexAttrib4dv(uint index, Stream v);
        public abstract void VertexAttrib4f(uint index, float x, float y, float z, float w);
        public abstract void VertexAttrib4fv(uint index, Stream v);
        public abstract void VertexAttrib4iv(uint index, Stream v);
        public abstract void VertexAttrib4s(uint index, short x, short y, short z, short w);
        public abstract void VertexAttrib4sv(uint index, Stream v);
        public abstract void VertexAttrib4ubv(uint index, Stream v);
        public abstract void VertexAttrib4uiv(uint index, Stream v);
        public abstract void VertexAttrib4usv(uint index, Stream v);
        public abstract void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, Stream pointer);

        // GL_VERSION_2_1
        public abstract void UniformMatrix2x3fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix3x2fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix2x4fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix4x2fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix3x4fv(int location, int count, bool transpose, Stream value);
        public abstract void UniformMatrix4x3fv(int location, int count, bool transpose, Stream value);
    }
}
