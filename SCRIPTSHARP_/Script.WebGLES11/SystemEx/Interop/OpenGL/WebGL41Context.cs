#if !CODE_ANALYSIS
namespace System.Interop.OpenGL
#else
using System;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class GL15Context
    {
// GL_VERSION_4_0
/* OpenGL 4.0 also reuses entry points from these extensions: */
/* ARB_texture_query_lod (no entry points) */
/* ARB_draw_indirect */
/* ARB_gpu_shader5 (no entry points) */
/* ARB_gpu_shader_fp64 */
/* ARB_shader_subroutine */
/* ARB_tessellation_shader */
/* ARB_texture_buffer_object_rgb32 (no entry points) */
/* ARB_texture_cube_map_array (no entry points) */
/* ARB_texture_gather (no entry points) */
/* ARB_transform_feedback2 */
/* ARB_transform_feedback3 */
public void glMinSampleShading (GLclampf value);
public void glBlendEquationi (GLuint buf, GLenum mode);
public void glBlendEquationSeparatei (GLuint buf, GLenum modeRGB, GLenum modeAlpha);
public void glBlendFunci (GLuint buf, GLenum src, GLenum dst);
public void glBlendFuncSeparatei (GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

// GL_VERSION_4_1
/* OpenGL 4.1 also reuses entry points from these extensions: */
/* ARB_ES2_compatibility */
/* ARB_get_program_binary */
/* ARB_separate_shader_objects */
/* ARB_shader_precision (no entry points) */
/* ARB_vertex_attrib_64bit */
/* ARB_viewport_array */
    }
}
