#if !CODE_ANALYSIS
namespace System.Interop.OpenGL
#else
using System;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class GLARBContext
    {
// GL_ARB_depth_buffer_float

// GL_ARB_framebuffer_object
GLAPI GLboolean APIENTRY glIsRenderbuffer (GLuint renderbuffer);
public void glBindRenderbuffer (GLenum target, GLuint renderbuffer);
public void glDeleteRenderbuffers (GLsizei n, const GLuint *renderbuffers);
public void glGenRenderbuffers (GLsizei n, GLuint *renderbuffers);
public void glRenderbufferStorage (GLenum target, GLenum internalformat, GLsizei width, GLsizei height);
public void glGetRenderbufferParameteriv (GLenum target, GLenum pname, GLint *params);
GLAPI GLboolean APIENTRY glIsFramebuffer (GLuint framebuffer);
public void glBindFramebuffer (GLenum target, GLuint framebuffer);
public void glDeleteFramebuffers (GLsizei n, const GLuint *framebuffers);
public void glGenFramebuffers (GLsizei n, GLuint *framebuffers);
GLAPI GLenum APIENTRY glCheckFramebufferStatus (GLenum target);
public void glFramebufferTexture1D (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
public void glFramebufferTexture2D (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);
public void glFramebufferTexture3D (GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);
public void glFramebufferRenderbuffer (GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);
public void glGetFramebufferAttachmentParameteriv (GLenum target, GLenum attachment, GLenum pname, GLint *params);
public void glGenerateMipmap (GLenum target);
public void glBlitFramebuffer (GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);
public void glRenderbufferStorageMultisample (GLenum target, GLsizei samples, GLenum internalformat, GLsizei width, GLsizei height);
public void glFramebufferTextureLayer (GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);

// GL_ARB_framebuffer_sRGB

// GL_ARB_half_float_vertex

// GL_ARB_map_buffer_range
GLAPI GLvoid* APIENTRY glMapBufferRange (GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);
public void glFlushMappedBufferRange (GLenum target, GLintptr offset, GLsizeiptr length);

// GL_ARB_texture_compression_rgtc

// GL_ARB_texture_rg

// GL_ARB_vertex_array_object
public void glBindVertexArray (GLuint array);
public void glDeleteVertexArrays (GLsizei n, const GLuint *arrays);
public void glGenVertexArrays (GLsizei n, GLuint *arrays);
GLAPI GLboolean APIENTRY glIsVertexArray (GLuint array);

// GL_ARB_uniform_buffer_object
public void glGetUniformIndices (GLuint program, GLsizei uniformCount, const GLchar* *uniformNames, GLuint *uniformIndices);
public void glGetActiveUniformsiv (GLuint program, GLsizei uniformCount, const GLuint *uniformIndices, GLenum pname, GLint *params);
public void glGetActiveUniformName (GLuint program, GLuint uniformIndex, GLsizei bufSize, GLsizei *length, GLchar *uniformName);
GLAPI GLuint APIENTRY glGetUniformBlockIndex (GLuint program, const GLchar *uniformBlockName);
public void glGetActiveUniformBlockiv (GLuint program, GLuint uniformBlockIndex, GLenum pname, GLint *params);
public void glGetActiveUniformBlockName (GLuint program, GLuint uniformBlockIndex, GLsizei bufSize, GLsizei *length, GLchar *uniformBlockName);
public void glUniformBlockBinding (GLuint program, GLuint uniformBlockIndex, GLuint uniformBlockBinding);

// GL_ARB_copy_buffer
public void glCopyBufferSubData (GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);

// GL_ARB_depth_clamp

// GL_ARB_draw_elements_base_vertex
public void glDrawElementsBaseVertex (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLint basevertex);
public void glDrawRangeElementsBaseVertex (GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, const GLvoid *indices, GLint basevertex);
public void glDrawElementsInstancedBaseVertex (GLenum mode, GLsizei count, GLenum type, const GLvoid *indices, GLsizei primcount, GLint basevertex);
public void glMultiDrawElementsBaseVertex (GLenum mode, const GLsizei *count, GLenum type, const GLvoid* *indices, GLsizei primcount, const GLint *basevertex);

// GL_ARB_fragment_coord_conventions

// GL_ARB_provoking_vertex
public void glProvokingVertex (GLenum mode);

// GL_ARB_seamless_cube_map

// GL_ARB_sync
GLAPI GLsync APIENTRY glFenceSync (GLenum condition, GLbitfield flags);
GLAPI GLboolean APIENTRY glIsSync (GLsync sync);
public void glDeleteSync (GLsync sync);
GLAPI GLenum APIENTRY glClientWaitSync (GLsync sync, GLbitfield flags, GLuint64 timeout);
public void glWaitSync (GLsync sync, GLbitfield flags, GLuint64 timeout);
public void glGetInteger64v (GLenum pname, GLint64 *params);
public void glGetSynciv (GLsync sync, GLenum pname, GLsizei bufSize, GLsizei *length, GLint *values);

// GL_ARB_texture_multisample
public void glTexImage2DMultisample (GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);
public void glTexImage3DMultisample (GLenum target, GLsizei samples, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);
public void glGetMultisamplefv (GLenum pname, GLuint index, GLfloat *val);
public void glSampleMaski (GLuint index, GLbitfield mask);

// GL_ARB_vertex_array_bgra

// GL_ARB_draw_buffers_blend
public void glBlendEquationiARB (GLuint buf, GLenum mode);
public void glBlendEquationSeparateiARB (GLuint buf, GLenum modeRGB, GLenum modeAlpha);
public void glBlendFunciARB (GLuint buf, GLenum src, GLenum dst);
public void glBlendFuncSeparateiARB (GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

// GL_ARB_sample_shading
public void glMinSampleShadingARB (GLclampf value);

// GL_ARB_texture_cube_map_array

// GL_ARB_texture_gather

// GL_ARB_texture_query_lod

// GL_ARB_shading_language_include
public void glNamedStringARB (GLenum type, GLint namelen, const GLchar *name, GLint stringlen, const GLchar *string);
public void glDeleteNamedStringARB (GLint namelen, const GLchar *name);
public void glCompileShaderIncludeARB (GLuint shader, GLsizei count, const GLchar* *path, const GLint *length);
GLAPI GLboolean APIENTRY glIsNamedStringARB (GLint namelen, const GLchar *name);
public void glGetNamedStringARB (GLint namelen, const GLchar *name, GLsizei bufSize, GLint *stringlen, GLchar *string);
public void glGetNamedStringivARB (GLint namelen, const GLchar *name, GLenum pname, GLint *params);

// GL_ARB_texture_compression_bptc

// GL_ARB_blend_func_extended
public void glBindFragDataLocationIndexed (GLuint program, GLuint colorNumber, GLuint index, const GLchar *name);
GLAPI GLint APIENTRY glGetFragDataIndex (GLuint program, const GLchar *name);

// GL_ARB_explicit_attrib_location

// GL_ARB_occlusion_query2

// GL_ARB_sampler_objects
public void glGenSamplers (GLsizei count, GLuint *samplers);
public void glDeleteSamplers (GLsizei count, const GLuint *samplers);
GLAPI GLboolean APIENTRY glIsSampler (GLuint sampler);
public void glBindSampler (GLuint unit, GLuint sampler);
public void glSamplerParameteri (GLuint sampler, GLenum pname, GLint param);
public void glSamplerParameteriv (GLuint sampler, GLenum pname, const GLint *param);
public void glSamplerParameterf (GLuint sampler, GLenum pname, GLfloat param);
public void glSamplerParameterfv (GLuint sampler, GLenum pname, const GLfloat *param);
public void glSamplerParameterIiv (GLuint sampler, GLenum pname, const GLint *param);
public void glSamplerParameterIuiv (GLuint sampler, GLenum pname, const GLuint *param);
public void glGetSamplerParameteriv (GLuint sampler, GLenum pname, GLint *params);
public void glGetSamplerParameterIiv (GLuint sampler, GLenum pname, GLint *params);
public void glGetSamplerParameterfv (GLuint sampler, GLenum pname, GLfloat *params);
public void glGetSamplerParameterIuiv (GLuint sampler, GLenum pname, GLuint *params);

// GL_ARB_texture_rgb10_a2ui

// GL_ARB_texture_swizzle

// GL_ARB_timer_query
public void glQueryCounter (GLuint id, GLenum target);
public void glGetQueryObjecti64v (GLuint id, GLenum pname, GLint64 *params);
public void glGetQueryObjectui64v (GLuint id, GLenum pname, GLuint64 *params);

// GL_ARB_vertex_type_2_10_10_10_rev
public void glVertexP2ui (GLenum type, GLuint value);
public void glVertexP2uiv (GLenum type, const GLuint *value);
public void glVertexP3ui (GLenum type, GLuint value);
public void glVertexP3uiv (GLenum type, const GLuint *value);
public void glVertexP4ui (GLenum type, GLuint value);
public void glVertexP4uiv (GLenum type, const GLuint *value);
public void glTexCoordP1ui (GLenum type, GLuint coords);
public void glTexCoordP1uiv (GLenum type, const GLuint *coords);
public void glTexCoordP2ui (GLenum type, GLuint coords);
public void glTexCoordP2uiv (GLenum type, const GLuint *coords);
public void glTexCoordP3ui (GLenum type, GLuint coords);
public void glTexCoordP3uiv (GLenum type, const GLuint *coords);
public void glTexCoordP4ui (GLenum type, GLuint coords);
public void glTexCoordP4uiv (GLenum type, const GLuint *coords);
public void glMultiTexCoordP1ui (GLenum texture, GLenum type, GLuint coords);
public void glMultiTexCoordP1uiv (GLenum texture, GLenum type, const GLuint *coords);
public void glMultiTexCoordP2ui (GLenum texture, GLenum type, GLuint coords);
public void glMultiTexCoordP2uiv (GLenum texture, GLenum type, const GLuint *coords);
public void glMultiTexCoordP3ui (GLenum texture, GLenum type, GLuint coords);
public void glMultiTexCoordP3uiv (GLenum texture, GLenum type, const GLuint *coords);
public void glMultiTexCoordP4ui (GLenum texture, GLenum type, GLuint coords);
public void glMultiTexCoordP4uiv (GLenum texture, GLenum type, const GLuint *coords);
public void glNormalP3ui (GLenum type, GLuint coords);
public void glNormalP3uiv (GLenum type, const GLuint *coords);
public void glColorP3ui (GLenum type, GLuint color);
public void glColorP3uiv (GLenum type, const GLuint *color);
public void glColorP4ui (GLenum type, GLuint color);
public void glColorP4uiv (GLenum type, const GLuint *color);
public void glSecondaryColorP3ui (GLenum type, GLuint color);
public void glSecondaryColorP3uiv (GLenum type, const GLuint *color);
public void glVertexAttribP1ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
public void glVertexAttribP1uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
public void glVertexAttribP2ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
public void glVertexAttribP2uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
public void glVertexAttribP3ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
public void glVertexAttribP3uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);
public void glVertexAttribP4ui (GLuint index, GLenum type, GLboolean normalized, GLuint value);
public void glVertexAttribP4uiv (GLuint index, GLenum type, GLboolean normalized, const GLuint *value);

// GL_ARB_draw_indirect
public void glDrawArraysIndirect (GLenum mode, const GLvoid *indirect);
public void glDrawElementsIndirect (GLenum mode, GLenum type, const GLvoid *indirect);

// GL_ARB_gpu_shader5

// GL_ARB_gpu_shader_fp64
public void glUniform1d (GLint location, GLdouble x);
public void glUniform2d (GLint location, GLdouble x, GLdouble y);
public void glUniform3d (GLint location, GLdouble x, GLdouble y, GLdouble z);
public void glUniform4d (GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
public void glUniform1dv (GLint location, GLsizei count, const GLdouble *value);
public void glUniform2dv (GLint location, GLsizei count, const GLdouble *value);
public void glUniform3dv (GLint location, GLsizei count, const GLdouble *value);
public void glUniform4dv (GLint location, GLsizei count, const GLdouble *value);
public void glUniformMatrix2dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix3dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix4dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix2x3dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix2x4dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix3x2dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix3x4dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix4x2dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glUniformMatrix4x3dv (GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glGetUniformdv (GLuint program, GLint location, GLdouble *params);

// GL_ARB_shader_subroutine
GLAPI GLint APIENTRY glGetSubroutineUniformLocation (GLuint program, GLenum shadertype, const GLchar *name);
GLAPI GLuint APIENTRY glGetSubroutineIndex (GLuint program, GLenum shadertype, const GLchar *name);
public void glGetActiveSubroutineUniformiv (GLuint program, GLenum shadertype, GLuint index, GLenum pname, GLint *values);
public void glGetActiveSubroutineUniformName (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, GLsizei *length, GLchar *name);
public void glGetActiveSubroutineName (GLuint program, GLenum shadertype, GLuint index, GLsizei bufsize, GLsizei *length, GLchar *name);
public void glUniformSubroutinesuiv (GLenum shadertype, GLsizei count, const GLuint *indices);
public void glGetUniformSubroutineuiv (GLenum shadertype, GLint location, GLuint *params);
public void glGetProgramStageiv (GLuint program, GLenum shadertype, GLenum pname, GLint *values);

// GL_ARB_tessellation_shader
public void glPatchParameteri (GLenum pname, GLint value);
public void glPatchParameterfv (GLenum pname, const GLfloat *values);

// GL_ARB_texture_buffer_object_rgb32

// GL_ARB_transform_feedback2
public void glBindTransformFeedback (GLenum target, GLuint id);
public void glDeleteTransformFeedbacks (GLsizei n, const GLuint *ids);
public void glGenTransformFeedbacks (GLsizei n, GLuint *ids);
GLAPI GLboolean APIENTRY glIsTransformFeedback (GLuint id);
public void glPauseTransformFeedback (void);
public void glResumeTransformFeedback (void);
public void glDrawTransformFeedback (GLenum mode, GLuint id);

// GL_ARB_transform_feedback3
public void glDrawTransformFeedbackStream (GLenum mode, GLuint id, GLuint stream);
public void glBeginQueryIndexed (GLenum target, GLuint index, GLuint id);
public void glEndQueryIndexed (GLenum target, GLuint index);
public void glGetQueryIndexediv (GLenum target, GLuint index, GLenum pname, GLint *params);

// GL_ARB_ES2_compatibility
public void glReleaseShaderCompiler (void);
public void glShaderBinary (GLsizei count, const GLuint *shaders, GLenum binaryformat, const GLvoid *binary, GLsizei length);
public void glGetShaderPrecisionFormat (GLenum shadertype, GLenum precisiontype, GLint *range, GLint *precision);
public void glDepthRangef (GLclampf n, GLclampf f);
public void glClearDepthf (GLclampf d);

// GL_ARB_get_program_binary
public void glGetProgramBinary (GLuint program, GLsizei bufSize, GLsizei *length, GLenum *binaryFormat, GLvoid *binary);
public void glProgramBinary (GLuint program, GLenum binaryFormat, const GLvoid *binary, GLsizei length);
public void glProgramParameteri (GLuint program, GLenum pname, GLint value);

// GL_ARB_separate_shader_objects
public void glUseProgramStages (GLuint pipeline, GLbitfield stages, GLuint program);
public void glActiveShaderProgram (GLuint pipeline, GLuint program);
GLAPI GLuint APIENTRY glCreateShaderProgramv (GLenum type, GLsizei count, const GLchar* *strings);
public void glBindProgramPipeline (GLuint pipeline);
public void glDeleteProgramPipelines (GLsizei n, const GLuint *pipelines);
public void glGenProgramPipelines (GLsizei n, GLuint *pipelines);
GLAPI GLboolean APIENTRY glIsProgramPipeline (GLuint pipeline);
public void glGetProgramPipelineiv (GLuint pipeline, GLenum pname, GLint *params);
public void glProgramUniform1i (GLuint program, GLint location, GLint v0);
public void glProgramUniform1iv (GLuint program, GLint location, GLsizei count, const GLint *value);
public void glProgramUniform1f (GLuint program, GLint location, GLfloat v0);
public void glProgramUniform1fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
public void glProgramUniform1d (GLuint program, GLint location, GLdouble v0);
public void glProgramUniform1dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
public void glProgramUniform1ui (GLuint program, GLint location, GLuint v0);
public void glProgramUniform1uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
public void glProgramUniform2i (GLuint program, GLint location, GLint v0, GLint v1);
public void glProgramUniform2iv (GLuint program, GLint location, GLsizei count, const GLint *value);
public void glProgramUniform2f (GLuint program, GLint location, GLfloat v0, GLfloat v1);
public void glProgramUniform2fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
public void glProgramUniform2d (GLuint program, GLint location, GLdouble v0, GLdouble v1);
public void glProgramUniform2dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
public void glProgramUniform2ui (GLuint program, GLint location, GLuint v0, GLuint v1);
public void glProgramUniform2uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
public void glProgramUniform3i (GLuint program, GLint location, GLint v0, GLint v1, GLint v2);
public void glProgramUniform3iv (GLuint program, GLint location, GLsizei count, const GLint *value);
public void glProgramUniform3f (GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2);
public void glProgramUniform3fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
public void glProgramUniform3d (GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2);
public void glProgramUniform3dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
public void glProgramUniform3ui (GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2);
public void glProgramUniform3uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
public void glProgramUniform4i (GLuint program, GLint location, GLint v0, GLint v1, GLint v2, GLint v3);
public void glProgramUniform4iv (GLuint program, GLint location, GLsizei count, const GLint *value);
public void glProgramUniform4f (GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);
public void glProgramUniform4fv (GLuint program, GLint location, GLsizei count, const GLfloat *value);
public void glProgramUniform4d (GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2, GLdouble v3);
public void glProgramUniform4dv (GLuint program, GLint location, GLsizei count, const GLdouble *value);
public void glProgramUniform4ui (GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
public void glProgramUniform4uiv (GLuint program, GLint location, GLsizei count, const GLuint *value);
public void glProgramUniformMatrix2fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix3fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix4fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix2dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix3dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix4dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix2x3fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix3x2fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix2x4fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix4x2fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix3x4fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix4x3fv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLfloat *value);
public void glProgramUniformMatrix2x3dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix3x2dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix2x4dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix4x2dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix3x4dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glProgramUniformMatrix4x3dv (GLuint program, GLint location, GLsizei count, GLboolean transpose, const GLdouble *value);
public void glValidateProgramPipeline (GLuint pipeline);
public void glGetProgramPipelineInfoLog (GLuint pipeline, GLsizei bufSize, GLsizei *length, GLchar *infoLog);

// GL_ARB_vertex_attrib_64bit
public void glVertexAttribL1d (GLuint index, GLdouble x);
public void glVertexAttribL2d (GLuint index, GLdouble x, GLdouble y);
public void glVertexAttribL3d (GLuint index, GLdouble x, GLdouble y, GLdouble z);
public void glVertexAttribL4d (GLuint index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);
public void glVertexAttribL1dv (GLuint index, const GLdouble *v);
public void glVertexAttribL2dv (GLuint index, const GLdouble *v);
public void glVertexAttribL3dv (GLuint index, const GLdouble *v);
public void glVertexAttribL4dv (GLuint index, const GLdouble *v);
public void glVertexAttribLPointer (GLuint index, GLint size, GLenum type, GLsizei stride, const GLvoid *pointer);
public void glGetVertexAttribLdv (GLuint index, GLenum pname, GLdouble *params);

// GL_ARB_viewport_array
public void glViewportArrayv (GLuint first, GLsizei count, const GLfloat *v);
public void glViewportIndexedf (GLuint index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);
public void glViewportIndexedfv (GLuint index, const GLfloat *v);
public void glScissorArrayv (GLuint first, GLsizei count, const GLint *v);
public void glScissorIndexed (GLuint index, GLint left, GLint bottom, GLsizei width, GLsizei height);
public void glScissorIndexedv (GLuint index, const GLint *v);
public void glDepthRangeArrayv (GLuint first, GLsizei count, const GLclampd *v);
public void glDepthRangeIndexed (GLuint index, GLclampd n, GLclampd f);
public void glGetFloati_v (GLenum target, GLuint index, GLfloat *data);
public void glGetDoublei_v (GLenum target, GLuint index, GLdouble *data);

// GL_ARB_cl_event
GLAPI GLsync APIENTRY glCreateSyncFromCLeventARB (struct _cl_context * context, struct _cl_event * event, GLbitfield flags);

// GL_ARB_debug_output
public void glDebugMessageControlARB (GLenum source, GLenum type, GLenum severity, GLsizei count, const GLuint *ids, GLboolean enabled);
public void glDebugMessageInsertARB (GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, const GLchar *buf);
public void glDebugMessageCallbackARB (GLDEBUGPROCARB callback, const GLvoid *userParam);
GLAPI GLuint APIENTRY glGetDebugMessageLogARB (GLuint count, GLsizei bufsize, GLenum *sources, GLenum *types, GLuint *ids, GLenum *severities, GLsizei *lengths, GLchar *messageLog);

// GL_ARB_robustness
GLAPI GLenum APIENTRY glGetGraphicsResetStatusARB (void);
public void glGetnMapdvARB (GLenum target, GLenum query, GLsizei bufSize, GLdouble *v);
public void glGetnMapfvARB (GLenum target, GLenum query, GLsizei bufSize, GLfloat *v);
public void glGetnMapivARB (GLenum target, GLenum query, GLsizei bufSize, GLint *v);
public void glGetnPixelMapfvARB (GLenum map, GLsizei bufSize, GLfloat *values);
public void glGetnPixelMapuivARB (GLenum map, GLsizei bufSize, GLuint *values);
public void glGetnPixelMapusvARB (GLenum map, GLsizei bufSize, GLushort *values);
public void glGetnPolygonStippleARB (GLsizei bufSize, GLubyte *pattern);
public void glGetnColorTableARB (GLenum target, GLenum format, GLenum type, GLsizei bufSize, GLvoid *table);
public void glGetnConvolutionFilterARB (GLenum target, GLenum format, GLenum type, GLsizei bufSize, GLvoid *image);
public void glGetnSeparableFilterARB (GLenum target, GLenum format, GLenum type, GLsizei rowBufSize, GLvoid *row, GLsizei columnBufSize, GLvoid *column, GLvoid *span);
public void glGetnHistogramARB (GLenum target, GLboolean reset, GLenum format, GLenum type, GLsizei bufSize, GLvoid *values);
public void glGetnMinmaxARB (GLenum target, GLboolean reset, GLenum format, GLenum type, GLsizei bufSize, GLvoid *values);
public void glGetnTexImageARB (GLenum target, GLint level, GLenum format, GLenum type, GLsizei bufSize, GLvoid *img);
public void glReadnPixelsARB (GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, GLvoid *data);
public void glGetnCompressedTexImageARB (GLenum target, GLint lod, GLsizei bufSize, GLvoid *img);
public void glGetnUniformfvARB (GLuint program, GLint location, GLsizei bufSize, GLfloat *params);
public void glGetnUniformivARB (GLuint program, GLint location, GLsizei bufSize, GLint *params);
public void glGetnUniformuivARB (GLuint program, GLint location, GLsizei bufSize, GLuint *params);
public void glGetnUniformdvARB (GLuint program, GLint location, GLsizei bufSize, GLdouble *params);
    }
}
