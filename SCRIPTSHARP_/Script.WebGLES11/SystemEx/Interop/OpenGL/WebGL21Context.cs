#if !CODE_ANALYSIS
namespace System.Interop.OpenGL
#else
using System;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class GL15Context
    {
// GL_VERSION_2_0
public void glBlendEquationSeparate (uint modeRGB, uint modeAlpha);
public void glDrawBuffers (GLsizei n, const uint *bufs);
public void glStencilOpSeparate (uint face, uint sfail, uint dpfail, uint dppass);
public void glStencilFuncSeparate (uint face, uint func, GLint ref, uint mask);
public void glStencilMaskSeparate (uint face, uint mask);
public void glAttachShader (uint program, uint shader);
public void glBindAttribLocation (uint program, uint index, const GLchar *name);
public void glCompileShader (uint shader);
public uint glCreateProgram ();
public uint glCreateShader (uint type);
public void glDeleteProgram (uint program);
public void glDeleteShader (uint shader);
public void glDetachShader (uint program, uint shader);
public void glDisableVertexAttribArray (uint index);
public void glEnableVertexAttribArray (uint index);
public void glGetActiveAttrib (uint program, uint index, GLsizei bufSize, GLsizei *length, GLint *size, uint *type, GLchar *name);
public void glGetActiveUniform (uint program, uint index, GLsizei bufSize, GLsizei *length, GLint *size, uint *type, GLchar *name);
public void glGetAttachedShaders (uint program, GLsizei maxCount, GLsizei *count, uint *obj);
GLAPI GLint APIENTRY glGetAttribLocation (uint program, const GLchar *name);
public void glGetProgramiv (uint program, uint pname, GLint *params);
public void glGetProgramInfoLog (uint program, GLsizei bufSize, GLsizei *length, GLchar *infoLog);
public void glGetShaderiv (uint shader, uint pname, GLint *params);
public void glGetShaderInfoLog (uint shader, GLsizei bufSize, GLsizei *length, GLchar *infoLog);
public void glGetShaderSource (uint shader, GLsizei bufSize, GLsizei *length, GLchar *source);
GLAPI GLint APIENTRY glGetUniformLocation (uint program, const GLchar *name);
public void glGetUniformfv (uint program, GLint location, GLfloat *params);
public void glGetUniformiv (uint program, GLint location, GLint *params);
public void glGetVertexAttribdv (uint index, uint pname, GLdouble *params);
public void glGetVertexAttribfv (uint index, uint pname, GLfloat *params);
public void glGetVertexAttribiv (uint index, uint pname, GLint *params);
public void glGetVertexAttribPointerv (uint index, uint pname, GLvoid* *pointer);
GLAPI GLboolean APIENTRY glIsProgram (uint program);
GLAPI GLboolean APIENTRY glIsShader (uint shader);
public void glLinkProgram (uint program);
public void glShaderSource (uint shader, GLsizei count, const GLchar* *string, const GLint *length);
public void glUseProgram (uint program);
public void glUniform1f (GLint location, GLfloat v0);
public void glUniform2f (GLint location, GLfloat v0, GLfloat v1);
public void glUniform3f (GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
public void glUniform4f (GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
public void glUniform1i (GLint location, GLint v0);
public void glUniform2i (GLint location, GLint v0, GLint v1);
public void glUniform3i (GLint location, GLint v0, GLint v1, GLint v2);
public void glUniform4i (GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
public void glUniform1fv (GLint location, GLsizei count, const GLfloat *value);
public void glUniform2fv (GLint location, GLsizei count, const GLfloat *value);
public void glUniform3fv (GLint location, GLsizei count, const GLfloat *value);
public void glUniform4fv (GLint location, GLsizei count, const GLfloat *value);
public void glUniform1iv (GLint location, GLsizei count, const GLint *value);
public void glUniform2iv (GLint location, GLsizei count, const GLint *value);
public void glUniform3iv (GLint location, GLsizei count, const GLint *value);
public void glUniform4iv (GLint location, GLsizei count, const GLint *value);
public void glUniformMatrix2fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix3fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix4fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glValidateProgram (uint program);
public void glVertexAttrib1d (uint index, GLdouble x);
public void glVertexAttrib1dv (uint index, const GLdouble *v);
public void glVertexAttrib1f (uint index, GLfloat x);
public void glVertexAttrib1fv (uint index, const GLfloat *v);
public void glVertexAttrib1s (uint index, GLshort x);
public void glVertexAttrib1sv (uint index, const GLshort *v);
public void glVertexAttrib2d (uint index, GLdouble x, GLdouble y);
public void glVertexAttrib2dv (uint index, const GLdouble *v);
public void glVertexAttrib2f (uint index, GLfloat x, GLfloat y);
public void glVertexAttrib2fv (uint index, const GLfloat *v);
public void glVertexAttrib2s (uint index, GLshort x, GLshort y);
public void glVertexAttrib2sv (uint index, const GLshort *v);
public void glVertexAttrib3d (uint index, GLdouble x, GLdouble y, GLdouble z);
public void glVertexAttrib3dv (uint index, const GLdouble *v);
public void glVertexAttrib3f (uint index, GLfloat x, GLfloat y, GLfloat z);
public void glVertexAttrib3fv (uint index, const GLfloat *v);
public void glVertexAttrib3s (uint index, GLshort x, GLshort y, GLshort z);
public void glVertexAttrib3sv (uint index, const GLshort *v);
public void glVertexAttrib4Nbv (uint index, const GLbyte *v);
public void glVertexAttrib4Niv (uint index, const GLint *v);
public void glVertexAttrib4Nsv (uint index, const GLshort *v);
public void glVertexAttrib4Nub (uint index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);
public void glVertexAttrib4Nubv (uint index, const GLubyte *v);
public void glVertexAttrib4Nuiv (uint index, const uint *v);
public void glVertexAttrib4Nusv (uint index, const GLushort *v);
public void glVertexAttrib4bv (uint index, const GLbyte *v);
public void glVertexAttrib4d (uint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
public void glVertexAttrib4dv (uint index, const GLdouble *v);
public void glVertexAttrib4f (uint index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);
public void glVertexAttrib4fv (uint index, const GLfloat *v);
public void glVertexAttrib4iv (uint index, const GLint *v);
public void glVertexAttrib4s (uint index, GLshort x, GLshort y, GLshort z, GLshort w);
public void glVertexAttrib4sv (uint index, const GLshort *v);
public void glVertexAttrib4ubv (uint index, const GLubyte *v);
public void glVertexAttrib4uiv (uint index, const uint *v);
public void glVertexAttrib4usv (uint index, const GLushort *v);
public void glVertexAttribPointer (uint index, GLint size, uint type, GLboolean normalized, GLsizei stride, const GLvoid *pointer);

// GL_VERSION_2_1
public void glUniformMatrix2x3fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix3x2fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix2x4fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix4x2fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix3x4fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glUniformMatrix4x3fv (GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
    }
}
