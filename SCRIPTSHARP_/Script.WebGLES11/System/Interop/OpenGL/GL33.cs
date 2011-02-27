using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public abstract class GL33
    {
        // GL_VERSION_3_0
        public const uint COMPARE_REF_TO_TEXTURE = 0x884E;
        public const uint CLIP_DISTANCE0 = 0x3000;
        public const uint CLIP_DISTANCE1 = 0x3001;
        public const uint CLIP_DISTANCE2 = 0x3002;
        public const uint CLIP_DISTANCE3 = 0x3003;
        public const uint CLIP_DISTANCE4 = 0x3004;
        public const uint CLIP_DISTANCE5 = 0x3005;
        public const uint CLIP_DISTANCE6 = 0x3006;
        public const uint CLIP_DISTANCE7 = 0x3007;
        public const uint MAX_CLIP_DISTANCES = 0x0D32;
        public const uint MAJOR_VERSION = 0x821B;
        public const uint MINOR_VERSION = 0x821C;
        public const uint NUM_EXTENSIONS = 0x821D;
        public const uint CONTEXT_FLAGS = 0x821E;
        public const uint DEPTH_BUFFER = 0x8223;
        public const uint STENCIL_BUFFER = 0x8224;
        public const uint COMPRESSED_RED = 0x8225;
        public const uint COMPRESSED_RG = 0x8226;
        public const uint CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT = 0x0001;
        public const uint RGBA32F = 0x8814;
        public const uint RGB32F = 0x8815;
        public const uint RGBA16F = 0x881A;
        public const uint RGB16F = 0x881B;
        public const uint VERTEX_ATTRIB_ARRAY_INTEGER = 0x88FD;
        public const uint MAX_ARRAY_TEXTURE_LAYERS = 0x88FF;
        public const uint MIN_PROGRAM_TEXEL_OFFSET = 0x8904;
        public const uint MAX_PROGRAM_TEXEL_OFFSET = 0x8905;
        public const uint CLAMP_READ_COLOR = 0x891C;
        public const uint FIXED_ONLY = 0x891D;
        public const uint MAX_VARYING_COMPONENTS = 0x8B4B;
        public const uint TEXTURE_1D_ARRAY = 0x8C18;
        public const uint PROXY_TEXTURE_1D_ARRAY = 0x8C19;
        public const uint TEXTURE_2D_ARRAY = 0x8C1A;
        public const uint PROXY_TEXTURE_2D_ARRAY = 0x8C1B;
        public const uint TEXTURE_BINDING_1D_ARRAY = 0x8C1C;
        public const uint TEXTURE_BINDING_2D_ARRAY = 0x8C1D;
        public const uint R11F_G11F_B10F = 0x8C3A;
        public const uint UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B;
        public const uint RGB9_E5 = 0x8C3D;
        public const uint UNSIGNED_INT_5_9_9_9_REV = 0x8C3E;
        public const uint TEXTURE_SHARED_SIZE = 0x8C3F;
        public const uint TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 0x8C76;
        public const uint TRANSFORM_FEEDBACK_BUFFER_MODE = 0x8C7F;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS = 0x8C80;
        public const uint TRANSFORM_FEEDBACK_VARYINGS = 0x8C83;
        public const uint TRANSFORM_FEEDBACK_BUFFER_START = 0x8C84;
        public const uint TRANSFORM_FEEDBACK_BUFFER_SIZE = 0x8C85;
        public const uint PRIMITIVES_GENERATED = 0x8C87;
        public const uint TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 0x8C88;
        public const uint RASTERIZER_DISCARD = 0x8C89;
        public const uint MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A;
        public const uint MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS = 0x8C8B;
        public const uint INTERLEAVED_ATTRIBS = 0x8C8C;
        public const uint SEPARATE_ATTRIBS = 0x8C8D;
        public const uint TRANSFORM_FEEDBACK_BUFFER = 0x8C8E;
        public const uint TRANSFORM_FEEDBACK_BUFFER_BINDING = 0x8C8F;
        public const uint RGBA32UI = 0x8D70;
        public const uint RGB32UI = 0x8D71;
        public const uint RGBA16UI = 0x8D76;
        public const uint RGB16UI = 0x8D77;
        public const uint RGBA8UI = 0x8D7C;
        public const uint RGB8UI = 0x8D7D;
        public const uint RGBA32I = 0x8D82;
        public const uint RGB32I = 0x8D83;
        public const uint RGBA16I = 0x8D88;
        public const uint RGB16I = 0x8D89;
        public const uint RGBA8I = 0x8D8E;
        public const uint RGB8I = 0x8D8F;
        public const uint RED_INTEGER = 0x8D94;
        public const uint GREEN_INTEGER = 0x8D95;
        public const uint BLUE_INTEGER = 0x8D96;
        public const uint RGB_INTEGER = 0x8D98;
        public const uint RGBA_INTEGER = 0x8D99;
        public const uint BGR_INTEGER = 0x8D9A;
        public const uint BGRA_INTEGER = 0x8D9B;
        public const uint SAMPLER_1D_ARRAY = 0x8DC0;
        public const uint SAMPLER_2D_ARRAY = 0x8DC1;
        public const uint SAMPLER_1D_ARRAY_SHADOW = 0x8DC3;
        public const uint SAMPLER_2D_ARRAY_SHADOW = 0x8DC4;
        public const uint SAMPLER_CUBE_SHADOW = 0x8DC5;
        public const uint UNSIGNED_INT_VEC2 = 0x8DC6;
        public const uint UNSIGNED_INT_VEC3 = 0x8DC7;
        public const uint UNSIGNED_INT_VEC4 = 0x8DC8;
        public const uint INT_SAMPLER_1D = 0x8DC9;
        public const uint INT_SAMPLER_2D = 0x8DCA;
        public const uint INT_SAMPLER_3D = 0x8DCB;
        public const uint INT_SAMPLER_CUBE = 0x8DCC;
        public const uint INT_SAMPLER_1D_ARRAY = 0x8DCE;
        public const uint INT_SAMPLER_2D_ARRAY = 0x8DCF;
        public const uint UNSIGNED_INT_SAMPLER_1D = 0x8DD1;
        public const uint UNSIGNED_INT_SAMPLER_2D = 0x8DD2;
        public const uint UNSIGNED_INT_SAMPLER_3D = 0x8DD3;
        public const uint UNSIGNED_INT_SAMPLER_CUBE = 0x8DD4;
        public const uint UNSIGNED_INT_SAMPLER_1D_ARRAY = 0x8DD6;
        public const uint UNSIGNED_INT_SAMPLER_2D_ARRAY = 0x8DD7;
        public const uint QUERY_WAIT = 0x8E13;
        public const uint QUERY_NO_WAIT = 0x8E14;
        public const uint QUERY_BY_REGION_WAIT = 0x8E15;
        public const uint QUERY_BY_REGION_NO_WAIT = 0x8E16;
        public const uint BUFFER_ACCESS_FLAGS = 0x911F;
        public const uint BUFFER_MAP_LENGTH = 0x9120;
        public const uint BUFFER_MAP_OFFSET = 0x9121;
        /* Reuse tokens from ARB_depth_buffer_float */
        /* DEPTH_COMPONENT32F */
        /* DEPTH32F_STENCIL8 */
        /* FLOAT_32_UNSIGNED_INT_24_8_REV */
        /* Reuse tokens from ARB_framebuffer_object */
        /* INVALID_FRAMEBUFFER_OPERATION */
        /* FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING */
        /* FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE */
        /* FRAMEBUFFER_ATTACHMENT_RED_SIZE */
        /* FRAMEBUFFER_ATTACHMENT_GREEN_SIZE */
        /* FRAMEBUFFER_ATTACHMENT_BLUE_SIZE */
        /* FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE */
        /* FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE */
        /* FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE */
        /* FRAMEBUFFER_DEFAULT */
        /* FRAMEBUFFER_UNDEFINED */
        /* DEPTH_STENCIL_ATTACHMENT */
        /* INDEX */
        /* MAX_RENDERBUFFER_SIZE */
        /* DEPTH_STENCIL */
        /* UNSIGNED_INT_24_8 */
        /* DEPTH24_STENCIL8 */
        /* TEXTURE_STENCIL_SIZE */
        /* TEXTURE_RED_TYPE */
        /* TEXTURE_GREEN_TYPE */
        /* TEXTURE_BLUE_TYPE */
        /* TEXTURE_ALPHA_TYPE */
        /* TEXTURE_DEPTH_TYPE */
        /* UNSIGNED_NORMALIZED */
        /* FRAMEBUFFER_BINDING */
        /* DRAW_FRAMEBUFFER_BINDING */
        /* RENDERBUFFER_BINDING */
        /* READ_FRAMEBUFFER */
        /* DRAW_FRAMEBUFFER */
        /* READ_FRAMEBUFFER_BINDING */
        /* RENDERBUFFER_SAMPLES */
        /* FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE */
        /* FRAMEBUFFER_ATTACHMENT_OBJECT_NAME */
        /* FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL */
        /* FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE */
        /* FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER */
        /* FRAMEBUFFER_COMPLETE */
        /* FRAMEBUFFER_INCOMPLETE_ATTACHMENT */
        /* FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT */
        /* FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER */
        /* FRAMEBUFFER_INCOMPLETE_READ_BUFFER */
        /* FRAMEBUFFER_UNSUPPORTED */
        /* MAX_COLOR_ATTACHMENTS */
        /* COLOR_ATTACHMENT0 */
        /* COLOR_ATTACHMENT1 */
        /* COLOR_ATTACHMENT2 */
        /* COLOR_ATTACHMENT3 */
        /* COLOR_ATTACHMENT4 */
        /* COLOR_ATTACHMENT5 */
        /* COLOR_ATTACHMENT6 */
        /* COLOR_ATTACHMENT7 */
        /* COLOR_ATTACHMENT8 */
        /* COLOR_ATTACHMENT9 */
        /* COLOR_ATTACHMENT10 */
        /* COLOR_ATTACHMENT11 */
        /* COLOR_ATTACHMENT12 */
        /* COLOR_ATTACHMENT13 */
        /* COLOR_ATTACHMENT14 */
        /* COLOR_ATTACHMENT15 */
        /* DEPTH_ATTACHMENT */
        /* STENCIL_ATTACHMENT */
        /* FRAMEBUFFER */
        /* RENDERBUFFER */
        /* RENDERBUFFER_WIDTH */
        /* RENDERBUFFER_HEIGHT */
        /* RENDERBUFFER_INTERNAL_FORMAT */
        /* STENCIL_INDEX1 */
        /* STENCIL_INDEX4 */
        /* STENCIL_INDEX8 */
        /* STENCIL_INDEX16 */
        /* RENDERBUFFER_RED_SIZE */
        /* RENDERBUFFER_GREEN_SIZE */
        /* RENDERBUFFER_BLUE_SIZE */
        /* RENDERBUFFER_ALPHA_SIZE */
        /* RENDERBUFFER_DEPTH_SIZE */
        /* RENDERBUFFER_STENCIL_SIZE */
        /* FRAMEBUFFER_INCOMPLETE_MULTISAMPLE */
        /* MAX_SAMPLES */
        /* Reuse tokens from ARB_framebuffer_sRGB */
        /* FRAMEBUFFER_SRGB */
        /* Reuse tokens from ARB_half_float_vertex */
        /* HALF_FLOAT */
        /* Reuse tokens from ARB_map_buffer_range */
        /* MAP_READ_BIT */
        /* MAP_WRITE_BIT */
        /* MAP_INVALIDATE_RANGE_BIT */
        /* MAP_INVALIDATE_BUFFER_BIT */
        /* MAP_FLUSH_EXPLICIT_BIT */
        /* MAP_UNSYNCHRONIZED_BIT */
        /* Reuse tokens from ARB_texture_compression_rgtc */
        /* COMPRESSED_RED_RGTC1 */
        /* COMPRESSED_SIGNED_RED_RGTC1 */
        /* COMPRESSED_RG_RGTC2 */
        /* COMPRESSED_SIGNED_RG_RGTC2 */
        /* Reuse tokens from ARB_texture_rg */
        /* RG */
        /* RG_INTEGER */
        /* R8 */
        /* R16 */
        /* RG8 */
        /* RG16 */
        /* R16F */
        /* R32F */
        /* RG16F */
        /* RG32F */
        /* R8I */
        /* R8UI */
        /* R16I */
        /* R16UI */
        /* R32I */
        /* R32UI */
        /* RG8I */
        /* RG8UI */
        /* RG16I */
        /* RG16UI */
        /* RG32I */
        /* RG32UI */
        /* Reuse tokens from ARB_vertex_array_object */
        /* VERTEX_ARRAY_BINDING */

        // GL_VERSION_3_1
        public const uint SAMPLER_2D_RECT = 0x8B63;
        public const uint SAMPLER_2D_RECT_SHADOW = 0x8B64;
        public const uint SAMPLER_BUFFER = 0x8DC2;
        public const uint INT_SAMPLER_2D_RECT = 0x8DCD;
        public const uint INT_SAMPLER_BUFFER = 0x8DD0;
        public const uint UNSIGNED_INT_SAMPLER_2D_RECT = 0x8DD5;
        public const uint UNSIGNED_INT_SAMPLER_BUFFER = 0x8DD8;
        public const uint TEXTURE_BUFFER = 0x8C2A;
        public const uint MAX_TEXTURE_BUFFER_SIZE = 0x8C2B;
        public const uint TEXTURE_BINDING_BUFFER = 0x8C2C;
        public const uint TEXTURE_BUFFER_DATA_STORE_BINDING = 0x8C2D;
        public const uint TEXTURE_BUFFER_FORMAT = 0x8C2E;
        public const uint TEXTURE_RECTANGLE = 0x84F5;
        public const uint TEXTURE_BINDING_RECTANGLE = 0x84F6;
        public const uint PROXY_TEXTURE_RECTANGLE = 0x84F7;
        public const uint MAX_RECTANGLE_TEXTURE_SIZE = 0x84F8;
        public const uint RED_SNORM = 0x8F90;
        public const uint RG_SNORM = 0x8F91;
        public const uint RGB_SNORM = 0x8F92;
        public const uint RGBA_SNORM = 0x8F93;
        public const uint R8_SNORM = 0x8F94;
        public const uint RG8_SNORM = 0x8F95;
        public const uint RGB8_SNORM = 0x8F96;
        public const uint RGBA8_SNORM = 0x8F97;
        public const uint R16_SNORM = 0x8F98;
        public const uint RG16_SNORM = 0x8F99;
        public const uint RGB16_SNORM = 0x8F9A;
        public const uint RGBA16_SNORM = 0x8F9B;
        public const uint SIGNED_NORMALIZED = 0x8F9C;
        public const uint PRIMITIVE_RESTART = 0x8F9D;
        public const uint PRIMITIVE_RESTART_INDEX = 0x8F9E;
        /* Reuse tokens from ARB_copy_buffer */
        /* COPY_READ_BUFFER */
        /* COPY_WRITE_BUFFER */
        /* Reuse tokens from ARB_draw_instanced (none) */
        /* Reuse tokens from ARB_uniform_buffer_object */
        /* UNIFORM_BUFFER */
        /* UNIFORM_BUFFER_BINDING */
        /* UNIFORM_BUFFER_START */
        /* UNIFORM_BUFFER_SIZE */
        /* MAX_VERTEX_UNIFORM_BLOCKS */
        /* MAX_FRAGMENT_UNIFORM_BLOCKS */
        /* MAX_COMBINED_UNIFORM_BLOCKS */
        /* MAX_UNIFORM_BUFFER_BINDINGS */
        /* MAX_UNIFORM_BLOCK_SIZE */
        /* MAX_COMBINED_VERTEX_UNIFORM_COMPONENTS */
        /* MAX_COMBINED_FRAGMENT_UNIFORM_COMPONENTS */
        /* UNIFORM_BUFFER_OFFSET_ALIGNMENT */
        /* ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH */
        /* ACTIVE_UNIFORM_BLOCKS */
        /* UNIFORM_TYPE */
        /* UNIFORM_SIZE */
        /* UNIFORM_NAME_LENGTH */
        /* UNIFORM_BLOCK_INDEX */
        /* UNIFORM_OFFSET */
        /* UNIFORM_ARRAY_STRIDE */
        /* UNIFORM_MATRIX_STRIDE */
        /* UNIFORM_IS_ROW_MAJOR */
        /* UNIFORM_BLOCK_BINDING */
        /* UNIFORM_BLOCK_DATA_SIZE */
        /* UNIFORM_BLOCK_NAME_LENGTH */
        /* UNIFORM_BLOCK_ACTIVE_UNIFORMS */
        /* UNIFORM_BLOCK_ACTIVE_UNIFORM_INDICES */
        /* UNIFORM_BLOCK_REFERENCED_BY_VERTEX_SHADER */
        /* UNIFORM_BLOCK_REFERENCED_BY_FRAGMENT_SHADER */
        /* INVALID_INDEX */

        // GL_VERSION_3_2
        public const uint CONTEXT_CORE_PROFILE_BIT = 0x00000001;
        public const uint CONTEXT_COMPATIBILITY_PROFILE_BIT = 0x00000002;
        public const uint LINES_ADJACENCY = 0x000A;
        public const uint LINE_STRIP_ADJACENCY = 0x000B;
        public const uint TRIANGLES_ADJACENCY = 0x000C;
        public const uint TRIANGLE_STRIP_ADJACENCY = 0x000D;
        public const uint PROGRAM_POINT_SIZE = 0x8642;
        public const uint MAX_GEOMETRY_TEXTURE_IMAGE_UNITS = 0x8C29;
        public const uint FRAMEBUFFER_ATTACHMENT_LAYERED = 0x8DA7;
        public const uint FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS = 0x8DA8;
        public const uint GEOMETRY_SHADER = 0x8DD9;
        public const uint GEOMETRY_VERTICES_OUT = 0x8916;
        public const uint GEOMETRY_INPUT_TYPE = 0x8917;
        public const uint GEOMETRY_OUTPUT_TYPE = 0x8918;
        public const uint MAX_GEOMETRY_UNIFORM_COMPONENTS = 0x8DDF;
        public const uint MAX_GEOMETRY_OUTPUT_VERTICES = 0x8DE0;
        public const uint MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS = 0x8DE1;
        public const uint MAX_VERTEX_OUTPUT_COMPONENTS = 0x9122;
        public const uint MAX_GEOMETRY_INPUT_COMPONENTS = 0x9123;
        public const uint MAX_GEOMETRY_OUTPUT_COMPONENTS = 0x9124;
        public const uint MAX_FRAGMENT_INPUT_COMPONENTS = 0x9125;
        public const uint CONTEXT_PROFILE_MASK = 0x9126;
        /* MAX_VARYING_COMPONENTS */
        /* FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER */
        /* Reuse tokens from ARB_depth_clamp */
        /* DEPTH_CLAMP */
        /* Reuse tokens from ARB_draw_elements_base_vertex (none) */
        /* Reuse tokens from ARB_fragment_coord_conventions (none) */
        /* Reuse tokens from ARB_provoking_vertex */
        /* QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION */
        /* FIRST_VERTEX_CONVENTION */
        /* LAST_VERTEX_CONVENTION */
        /* PROVOKING_VERTEX */
        /* Reuse tokens from ARB_seamless_cube_map */
        /* TEXTURE_CUBE_MAP_SEAMLESS */
        /* Reuse tokens from ARB_sync */
        /* MAX_SERVER_WAIT_TIMEOUT */
        /* OBJECT_TYPE */
        /* SYNC_CONDITION */
        /* SYNC_STATUS */
        /* SYNC_FLAGS */
        /* SYNC_FENCE */
        /* SYNC_GPU_COMMANDS_COMPLETE */
        /* UNSIGNALED */
        /* SIGNALED */
        /* ALREADY_SIGNALED */
        /* TIMEOUT_EXPIRED */
        /* CONDITION_SATISFIED */
        /* WAIT_FAILED */
        /* TIMEOUT_IGNORED */
        /* SYNC_FLUSH_COMMANDS_BIT */
        /* TIMEOUT_IGNORED */
        /* Reuse tokens from ARB_texture_multisample */
        /* SAMPLE_POSITION */
        /* SAMPLE_MASK */
        /* SAMPLE_MASK_VALUE */
        /* MAX_SAMPLE_MASK_WORDS */
        /* TEXTURE_2D_MULTISAMPLE */
        /* PROXY_TEXTURE_2D_MULTISAMPLE */
        /* TEXTURE_2D_MULTISAMPLE_ARRAY */
        /* PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY */
        /* TEXTURE_BINDING_2D_MULTISAMPLE */
        /* TEXTURE_BINDING_2D_MULTISAMPLE_ARRAY */
        /* TEXTURE_SAMPLES */
        /* TEXTURE_FIXED_SAMPLE_LOCATIONS */
        /* SAMPLER_2D_MULTISAMPLE */
        /* INT_SAMPLER_2D_MULTISAMPLE */
        /* UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE */
        /* SAMPLER_2D_MULTISAMPLE_ARRAY */
        /* INT_SAMPLER_2D_MULTISAMPLE_ARRAY */
        /* UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY */
        /* MAX_COLOR_TEXTURE_SAMPLES */
        /* MAX_DEPTH_TEXTURE_SAMPLES */
        /* MAX_INTEGER_SAMPLES */
        /* Don't need to reuse tokens from ARB_vertex_array_bgra since they're already in 1.2 core */

        // GL_VERSION_3_3
        public const uint VERTEX_ATTRIB_ARRAY_DIVISOR = 0x88FE;
        /* Reuse tokens from ARB_blend_func_extended */
        /* SRC1_COLOR */
        /* ONE_MINUS_SRC1_COLOR */
        /* ONE_MINUS_SRC1_ALPHA */
        /* MAX_DUAL_SOURCE_DRAW_BUFFERS */
        /* Reuse tokens from ARB_explicit_attrib_location (none) */
        /* Reuse tokens from ARB_occlusion_query2 */
        /* ANY_SAMPLES_PASSED */
        /* Reuse tokens from ARB_sampler_objects */
        /* SAMPLER_BINDING */
        /* Reuse tokens from ARB_shader_bit_encoding (none) */
        /* Reuse tokens from ARB_texture_rgb10_a2ui */
        /* RGB10_A2UI */
        /* Reuse tokens from ARB_texture_swizzle */
        /* TEXTURE_SWIZZLE_R */
        /* TEXTURE_SWIZZLE_G */
        /* TEXTURE_SWIZZLE_B */
        /* TEXTURE_SWIZZLE_A */
        /* TEXTURE_SWIZZLE_RGBA */
        /* Reuse tokens from ARB_timer_query */
        /* TIME_ELAPSED */
        /* TIMESTAMP */
        /* Reuse tokens from ARB_vertex_type_2_10_10_10_rev */
        /* INT_2_10_10_10_REV */
    }
}