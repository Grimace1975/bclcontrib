//#if !CODE_ANALYSIS
//namespace System.Interop.OpenGL
//#else
//using System;
//namespace SystemEx.Interop.OpenGL
//#endif
//{
//    public abstract class GL15Context
//    {
//// GL_VERSION_3_0
///* OpenGL 3.0 also reuses entry points from these extensions: */
///* ARB_framebuffer_object */
///* ARB_map_buffer_range */
///* ARB_vertex_array_object */
//public void glColorMaski (GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
//public void glGetBooleani_v (GLenum target, GLuint index, GLboolean *data);
//public void glGetIntegeri_v (GLenum target, GLuint index, GLint *data);
//public void glEnablei (GLenum target, GLuint index);
//public void glDisablei (GLenum target, GLuint index);
//GLAPI GLboolean APIENTRY glIsEnabledi (GLenum target, GLuint index);
//public void glBeginTransformFeedback (GLenum primitiveMode);
//public void glEndTransformFeedback (void);
//public void glBindBufferRange (GLenum target, GLuint index, GLuint buffer, GLintptr offset, GLsizeiptr size);
//public void glBindBufferBase (GLenum target, GLuint index, GLuint buffer);
//public void glTransformFeedbackVaryings (GLuint program, GLsizei count, const GLchar* *varyings, GLenum bufferMode);
//public void glGetTransformFeedbackVarying (GLuint program, GLuint index, GLsizei bufSize, GLsizei *length, GLsizei *size, GLenum *type, GLchar *name);
//public void glClampColor (GLenum target, GLenum clamp);
//public void glBeginConditionalRender (GLuint id, GLenum mode);
//public void glEndConditionalRender (void);
//public void glVertexAttribIPointer (GLuint index, GLint size, GLenum type, GLsizei stride, const GLvoid *pointer);
//public void glGetVertexAttribIiv (GLuint index, GLenum pname, GLint *params);
//public void glGetVertexAttribIuiv (GLuint index, GLenum pname, GLuint *params);
//public void glVertexAttribI1i (GLuint index, GLint x);
//public void glVertexAttribI2i (GLuint index, GLint x, GLint y);
//public void glVertexAttribI3i (GLuint index, GLint x, GLint y, GLint z);
//public void glVertexAttribI4i (GLuint index, GLint x, GLint y, GLint z, GLint w);
//public void glVertexAttribI1ui (GLuint index, GLuint x);
//public void glVertexAttribI2ui (GLuint index, GLuint x, GLuint y);
//public void glVertexAttribI3ui (GLuint index, GLuint x, GLuint y, GLuint z);
//public void glVertexAttribI4ui (GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
//public void glVertexAttribI1iv (GLuint index, const GLint *v);
//public void glVertexAttribI2iv (GLuint index, const GLint *v);
//public void glVertexAttribI3iv (GLuint index, const GLint *v);
//public void glVertexAttribI4iv (GLuint index, const GLint *v);
//public void glVertexAttribI1uiv (GLuint index, const GLuint *v);
//public void glVertexAttribI2uiv (GLuint index, const GLuint *v);
//public void glVertexAttribI3uiv (GLuint index, const GLuint *v);
//public void glVertexAttribI4uiv (GLuint index, const GLuint *v);
//public void glVertexAttribI4bv (GLuint index, const GLbyte *v);
//public void glVertexAttribI4sv (GLuint index, const GLshort *v);
//public void glVertexAttribI4ubv (GLuint index, const GLubyte *v);
//public void glVertexAttribI4usv (GLuint index, const GLushort *v);
//public void glGetUniformuiv (GLuint program, GLint location, GLuint *params);
//public void glBindFragDataLocation (GLuint program, GLuint color, const GLchar *name);
//GLAPI GLint APIENTRY glGetFragDataLocation (GLuint program, const GLchar *name);
//public void glUniform1ui (GLint location, GLuint v0);
//public void glUniform2ui (GLint location, GLuint v0, GLuint v1);
//public void glUniform3ui (GLint location, GLuint v0, GLuint v1, GLuint v2);
//public void glUniform4ui (GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
//public void glUniform1uiv (GLint location, GLsizei count, const GLuint *value);
//public void glUniform2uiv (GLint location, GLsizei count, const GLuint *value);
//public void glUniform3uiv (GLint location, GLsizei count, const GLuint *value);
//public void glUniform4uiv (GLint location, GLsizei count, const GLuint *value);
//public void glTexParameterIiv (GLenum target, GLenum pname, const GLint *params);
//public void glTexParameterIuiv (GLenum target, GLenum pname, const GLuint *params);
//public void glGetTexParameterIiv (GLenum target, GLenum pname, GLint *params);
//public void glGetTexParameterIuiv (GLenum target, GLenum pname, GLuint *params);
//public void glClearBufferiv (GLenum buffer, GLint drawbuffer, const GLint *value);
//public void glClearBufferuiv (GLenum buffer, GLint drawbuffer, const GLuint *value);
//public void glClearBufferfv (GLenum buffer, GLint drawbuffer, const GLfloat *value);
//public void glClearBufferfi (GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);
//GLAPI const GLubyte * APIENTRY glGetStringi (GLenum name, GLuint index);

//        // GL_VERSION_3_1
///* OpenGL 3.1 also reuses entry points from these extensions: */
///* ARB_copy_buffer */
///* ARB_uniform_buffer_object */
//#ifdef GL3_PROTOTYPES
//public void glDrawArraysInstanced (GLenum mode, GLint first, GLsizei count, GLsizei primcount);
//public void glDrawElementsInstanced (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLsizei primcount);
//public void glTexBuffer (GLenum target, GLenum internalformat, GLuint buffer);
//public void glPrimitiveRestartIndex (GLuint index);

//// GL_VERSION_3_2
///* OpenGL 3.2 also reuses entry points from these extensions: */
///* ARB_draw_elements_base_vertex */
///* ARB_provoking_vertex */
///* ARB_sync */
///* ARB_texture_multisample */
//public void glGetInteger64i_v (GLenum target, GLuint index, GLint64 *data);
//public void glGetBufferParameteri64v (GLenum target, GLenum pname, GLint64 *params);
//public void glFramebufferTexture (GLenum target, GLenum attachment, GLuint texture, GLint level);

//// GL_VERSION_3_3
///* OpenGL 3.3 also reuses entry points from these extensions: */
///* ARB_blend_func_extended */
///* ARB_sampler_objects */
///* ARB_explicit_attrib_location, but it has none */
///* ARB_occlusion_query2 (no entry points) */
///* ARB_shader_bit_encoding (no entry points) */
///* ARB_texture_rgb10_a2ui (no entry points) */
///* ARB_texture_swizzle (no entry points) */
///* ARB_timer_query */
///* ARB_vertex_type_2_10_10_10_rev */
//public void glVertexAttribDivisor (GLuint index, GLuint divisor);
//    }
//}
