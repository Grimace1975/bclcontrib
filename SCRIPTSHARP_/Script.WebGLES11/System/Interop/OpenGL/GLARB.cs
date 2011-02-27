using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public abstract class GL33
    {
// GL_ARB_depth_buffer_float
public const uint DEPTH_COMPONENT32F             =0x8CAC;
public const uint DEPTH32F_STENCIL8              =0x8CAD;
public const uint FLOAT_32_UNSIGNED_INT_24_8_REV =0x8DAD;

// GL_ARB_framebuffer_object
public const uint INVALID_FRAMEBUFFER_OPERATION  =0x0506;
public const uint FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING =0x8210;
public const uint FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE =0x8211;
public const uint FRAMEBUFFER_ATTACHMENT_RED_SIZE =0x8212;
public const uint FRAMEBUFFER_ATTACHMENT_GREEN_SIZE =0x8213;
public const uint FRAMEBUFFER_ATTACHMENT_BLUE_SIZE =0x8214;
public const uint FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE =0x8215;
public const uint FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE =0x8216;
public const uint FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE =0x8217;
public const uint FRAMEBUFFER_DEFAULT            =0x8218;
public const uint FRAMEBUFFER_UNDEFINED          =0x8219;
public const uint DEPTH_STENCIL_ATTACHMENT       =0x821A;
public const uint MAX_RENDERBUFFER_SIZE          =0x84E8;
public const uint DEPTH_STENCIL                  =0x84F9;
public const uint UNSIGNED_INT_24_8              =0x84FA;
public const uint DEPTH24_STENCIL8               =0x88F0;
public const uint TEXTURE_STENCIL_SIZE           =0x88F1;
public const uint TEXTURE_RED_TYPE               =0x8C10;
public const uint TEXTURE_GREEN_TYPE             =0x8C11;
public const uint TEXTURE_BLUE_TYPE              =0x8C12;
public const uint TEXTURE_ALPHA_TYPE             =0x8C13;
public const uint TEXTURE_DEPTH_TYPE             =0x8C16;
public const uint UNSIGNED_NORMALIZED            =0x8C17;
public const uint FRAMEBUFFER_BINDING            =0x8CA6;
public const uint DRAW_FRAMEBUFFER_BINDING       = FRAMEBUFFER_BINDING;
public const uint RENDERBUFFER_BINDING           =0x8CA7;
public const uint READ_FRAMEBUFFER               =0x8CA8;
public const uint DRAW_FRAMEBUFFER               =0x8CA9;
public const uint READ_FRAMEBUFFER_BINDING       =0x8CAA;
public const uint RENDERBUFFER_SAMPLES           =0x8CAB;
public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE =0x8CD0;
public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_NAME =0x8CD1;
public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL =0x8CD2;
public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE =0x8CD3;
public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER =0x8CD4;
public const uint FRAMEBUFFER_COMPLETE           =0x8CD5;
public const uint FRAMEBUFFER_INCOMPLETE_ATTACHMENT =0x8CD6;
public const uint FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT =0x8CD7;
public const uint FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER =0x8CDB;
public const uint FRAMEBUFFER_INCOMPLETE_READ_BUFFER =0x8CDC;
public const uint FRAMEBUFFER_UNSUPPORTED        =0x8CDD;
public const uint MAX_COLOR_ATTACHMENTS          =0x8CDF;
public const uint COLOR_ATTACHMENT0              =0x8CE0;
public const uint COLOR_ATTACHMENT1              =0x8CE1;
public const uint COLOR_ATTACHMENT2              =0x8CE2;
public const uint COLOR_ATTACHMENT3              =0x8CE3;
public const uint COLOR_ATTACHMENT4              =0x8CE4;
public const uint COLOR_ATTACHMENT5              =0x8CE5;
public const uint COLOR_ATTACHMENT6              =0x8CE6;
public const uint COLOR_ATTACHMENT7              =0x8CE7;
public const uint COLOR_ATTACHMENT8              =0x8CE8;
public const uint COLOR_ATTACHMENT9              =0x8CE9;
public const uint COLOR_ATTACHMENT10             =0x8CEA;
public const uint COLOR_ATTACHMENT11             =0x8CEB;
public const uint COLOR_ATTACHMENT12             =0x8CEC;
public const uint COLOR_ATTACHMENT13             =0x8CED;
public const uint COLOR_ATTACHMENT14             =0x8CEE;
public const uint COLOR_ATTACHMENT15             =0x8CEF;
public const uint DEPTH_ATTACHMENT               =0x8D00;
public const uint STENCIL_ATTACHMENT             =0x8D20;
public const uint FRAMEBUFFER                    =0x8D40;
public const uint RENDERBUFFER                   =0x8D41;
public const uint RENDERBUFFER_WIDTH             =0x8D42;
public const uint RENDERBUFFER_HEIGHT            =0x8D43;
public const uint RENDERBUFFER_INTERNAL_FORMAT   =0x8D44;
public const uint STENCIL_INDEX1                 =0x8D46;
public const uint STENCIL_INDEX4                 =0x8D47;
public const uint STENCIL_INDEX8                 =0x8D48;
public const uint STENCIL_INDEX16                =0x8D49;
public const uint RENDERBUFFER_RED_SIZE          =0x8D50;
public const uint RENDERBUFFER_GREEN_SIZE        =0x8D51;
public const uint RENDERBUFFER_BLUE_SIZE         =0x8D52;
public const uint RENDERBUFFER_ALPHA_SIZE        =0x8D53;
public const uint RENDERBUFFER_DEPTH_SIZE        =0x8D54;
public const uint RENDERBUFFER_STENCIL_SIZE      =0x8D55;
public const uint FRAMEBUFFER_INCOMPLETE_MULTISAMPLE =0x8D56;
public const uint MAX_SAMPLES                    =0x8D57;

// GL_ARB_framebuffer_sRGB
public const uint FRAMEBUFFER_SRGB               =0x8DB9;

// GL_ARB_half_float_vertex
public const uint HALF_FLOAT                     =0x140B;

// GL_ARB_map_buffer_range
public const uint MAP_READ_BIT                   =0x0001;
public const uint MAP_WRITE_BIT                  =0x0002;
public const uint MAP_INVALIDATE_RANGE_BIT       =0x0004;
public const uint MAP_INVALIDATE_BUFFER_BIT      =0x0008;
public const uint MAP_FLUSH_EXPLICIT_BIT         =0x0010;
public const uint MAP_UNSYNCHRONIZED_BIT         =0x0020;

// GL_ARB_texture_compression_rgtc
public const uint COMPRESSED_RED_RGTC1           =0x8DBB;
public const uint COMPRESSED_SIGNED_RED_RGTC1    =0x8DBC;
public const uint COMPRESSED_RG_RGTC2            =0x8DBD;
public const uint COMPRESSED_SIGNED_RG_RGTC2     =0x8DBE;
    
//GL_ARB_texture_rg
public const uint RG                             =0x8227;
public const uint RG_INTEGER                     =0x8228;
public const uint R8                             =0x8229;
public const uint R16                            =0x822A;
public const uint RG8                            =0x822B;
public const uint RG16                           =0x822C;
public const uint R16F                           =0x822D;
public const uint R32F                           =0x822E;
public const uint RG16F                          =0x822F;
public const uint RG32F                          =0x8230;
public const uint R8I                            =0x8231;
public const uint R8UI                           =0x8232;
public const uint R16I                           =0x8233;
public const uint R16UI                          =0x8234;
public const uint R32I                           =0x8235;
public const uint R32UI                          =0x8236;
public const uint RG8I                           =0x8237;
public const uint RG8UI                          =0x8238;
public const uint RG16I                          =0x8239;
public const uint RG16UI                         =0x823A;
public const uint RG32I                          =0x823B;
public const uint RG32UI                         =0x823C;

// GL_ARB_vertex_array_object
public const uint VERTEX_ARRAY_BINDING           =0x85B5;

// GL_ARB_uniform_buffer_object
public const uint UNIFORM_BUFFER                 =0x8A11;
public const uint UNIFORM_BUFFER_BINDING         =0x8A28;
public const uint UNIFORM_BUFFER_START           =0x8A29;
public const uint UNIFORM_BUFFER_SIZE            =0x8A2A;
public const uint MAX_VERTEX_UNIFORM_BLOCKS      =0x8A2B;
public const uint MAX_GEOMETRY_UNIFORM_BLOCKS    =0x8A2C;
public const uint MAX_FRAGMENT_UNIFORM_BLOCKS    =0x8A2D;
public const uint MAX_COMBINED_UNIFORM_BLOCKS    =0x8A2E;
public const uint MAX_UNIFORM_BUFFER_BINDINGS    =0x8A2F;
public const uint MAX_UNIFORM_BLOCK_SIZE         =0x8A30;
public const uint MAX_COMBINED_VERTEX_UNIFORM_COMPONENTS =0x8A31;
public const uint MAX_COMBINED_GEOMETRY_UNIFORM_COMPONENTS =0x8A32;
public const uint MAX_COMBINED_FRAGMENT_UNIFORM_COMPONENTS =0x8A33;
public const uint UNIFORM_BUFFER_OFFSET_ALIGNMENT =0x8A34;
public const uint ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH =0x8A35;
public const uint ACTIVE_UNIFORM_BLOCKS          =0x8A36;
public const uint UNIFORM_TYPE                   =0x8A37;
public const uint UNIFORM_SIZE                   =0x8A38;
public const uint UNIFORM_NAME_LENGTH            =0x8A39;
public const uint UNIFORM_BLOCK_INDEX            =0x8A3A;
public const uint UNIFORM_OFFSET                 =0x8A3B;
public const uint UNIFORM_ARRAY_STRIDE           =0x8A3C;
public const uint UNIFORM_MATRIX_STRIDE          =0x8A3D;
public const uint UNIFORM_IS_ROW_MAJOR           =0x8A3E;
public const uint UNIFORM_BLOCK_BINDING          =0x8A3F;
public const uint UNIFORM_BLOCK_DATA_SIZE        =0x8A40;
public const uint UNIFORM_BLOCK_NAME_LENGTH      =0x8A41;
public const uint UNIFORM_BLOCK_ACTIVE_UNIFORMS  =0x8A42;
public const uint UNIFORM_BLOCK_ACTIVE_UNIFORM_INDICES =0x8A43;
public const uint UNIFORM_BLOCK_REFERENCED_BY_VERTEX_SHADER =0x8A44;
public const uint UNIFORM_BLOCK_REFERENCED_BY_GEOMETRY_SHADER =0x8A45;
public const uint UNIFORM_BLOCK_REFERENCED_BY_FRAGMENT_SHADER =0x8A46;
public const uint INVALID_INDEX                  =0xFFFFFFFFu;

// GL_ARB_copy_buffer
public const uint COPY_READ_BUFFER               =0x8F36;
public const uint COPY_WRITE_BUFFER              =0x8F37;

// GL_ARB_depth_clamp
public const uint DEPTH_CLAMP                    =0x864F;

/// GL_ARB_draw_elements_base_vertex

// GL_ARB_fragment_coord_conventions

// GL_ARB_provoking_vertex
public const uint QUADS_FOLLOW_PROVOKING_VERTEX_CONVENTION =0x8E4C;
public const uint FIRST_VERTEX_CONVENTION        =0x8E4D;
public const uint LAST_VERTEX_CONVENTION         =0x8E4E;
public const uint PROVOKING_VERTEX               =0x8E4F;

// GL_ARB_seamless_cube_map
public const uint TEXTURE_CUBE_MAP_SEAMLESS      =0x884F;

// GL_ARB_sync
public const uint MAX_SERVER_WAIT_TIMEOUT        =0x9111;
public const uint OBJECT_TYPE                    =0x9112;
public const uint SYNC_CONDITION                 =0x9113;
public const uint SYNC_STATUS                    =0x9114;
public const uint SYNC_FLAGS                     =0x9115;
public const uint SYNC_FENCE                     =0x9116;
public const uint SYNC_GPU_COMMANDS_COMPLETE     =0x9117;
public const uint UNSIGNALED                     =0x9118;
public const uint SIGNALED                       =0x9119;
public const uint ALREADY_SIGNALED               =0x911A;
public const uint TIMEOUT_EXPIRED                =0x911B;
public const uint CONDITION_SATISFIED            =0x911C;
public const uint WAIT_FAILED                    =0x911D;
public const uint SYNC_FLUSH_COMMANDS_BIT        =0x00000001;
public const ulong TIMEOUT_IGNORED                =0xFFFFFFFFFFFFFFFFull;

// GL_ARB_texture_multisample
public const uint SAMPLE_POSITION                =0x8E50;
public const uint SAMPLE_MASK                    =0x8E51;
public const uint SAMPLE_MASK_VALUE              =0x8E52;
public const uint MAX_SAMPLE_MASK_WORDS          =0x8E59;
public const uint TEXTURE_2D_MULTISAMPLE         =0x9100;
public const uint PROXY_TEXTURE_2D_MULTISAMPLE   =0x9101;
public const uint TEXTURE_2D_MULTISAMPLE_ARRAY   =0x9102;
public const uint PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY =0x9103;
public const uint TEXTURE_BINDING_2D_MULTISAMPLE =0x9104;
public const uint TEXTURE_BINDING_2D_MULTISAMPLE_ARRAY =0x9105;
public const uint TEXTURE_SAMPLES                =0x9106;
public const uint TEXTURE_FIXED_SAMPLE_LOCATIONS =0x9107;
public const uint SAMPLER_2D_MULTISAMPLE         =0x9108;
public const uint INT_SAMPLER_2D_MULTISAMPLE     =0x9109;
public const uint UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE =0x910A;
public const uint SAMPLER_2D_MULTISAMPLE_ARRAY   =0x910B;
public const uint INT_SAMPLER_2D_MULTISAMPLE_ARRAY =0x910C;
public const uint UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY =0x910D;
public const uint MAX_COLOR_TEXTURE_SAMPLES      =0x910E;
public const uint MAX_DEPTH_TEXTURE_SAMPLES      =0x910F;
public const uint MAX_INTEGER_SAMPLES            =0x9110;

// GL_ARB_vertex_array_bgra
/* BGRA */

// GL_ARB_draw_buffers_blend

// GL_ARB_sample_shading
public const uint SAMPLE_SHADING_ARB             =0x8C36;
public const uint MIN_SAMPLE_SHADING_VALUE_ARB   =0x8C37;

// GL_ARB_texture_cube_map_array
public const uint TEXTURE_CUBE_MAP_ARRAY_ARB     =0x9009;
public const uint TEXTURE_BINDING_CUBE_MAP_ARRAY_ARB =0x900A;
public const uint PROXY_TEXTURE_CUBE_MAP_ARRAY_ARB =0x900B;
public const uint SAMPLER_CUBE_MAP_ARRAY_ARB     =0x900C;
public const uint SAMPLER_CUBE_MAP_ARRAY_SHADOW_ARB =0x900D;
public const uint INT_SAMPLER_CUBE_MAP_ARRAY_ARB =0x900E;
public const uint UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY_ARB =0x900F;

// GL_ARB_texture_gather
public const uint MIN_PROGRAM_TEXTURE_GATHER_OFFSET_ARB =0x8E5E;
public const uint MAX_PROGRAM_TEXTURE_GATHER_OFFSET_ARB =0x8E5F;

// GL_ARB_texture_query_lod

// GL_ARB_shading_language_include
public const uint SHADER_INCLUDE_ARB             =0x8DAE;
public const uint NAMED_STRING_LENGTH_ARB        =0x8DE9;
public const uint NAMED_STRING_TYPE_ARB          =0x8DEA;
        
// GL_ARB_texture_compression_bptc
public const uint COMPRESSED_RGBA_BPTC_UNORM_ARB =0x8E8C;
public const uint COMPRESSED_SRGB_ALPHA_BPTC_UNORM_ARB =0x8E8D;
public const uint COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB =0x8E8E;
public const uint COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB =0x8E8F;

// GL_ARB_blend_func_extended
public const uint SRC1_COLOR                     =0x88F9;
/* SRC1_ALPHA */
public const uint ONE_MINUS_SRC1_COLOR           =0x88FA;
public const uint ONE_MINUS_SRC1_ALPHA           =0x88FB;
public const uint MAX_DUAL_SOURCE_DRAW_BUFFERS   =0x88FC;

// GL_ARB_explicit_attrib_location

// GL_ARB_occlusion_query2
public const uint ANY_SAMPLES_PASSED             =0x8C2F;

// GL_ARB_sampler_objects
public const uint SAMPLER_BINDING                =0x8919;

// GL_ARB_shader_bit_encoding

// GL_ARB_texture_rgb10_a2ui
public const uint RGB10_A2UI                     =0x906F;

// GL_ARB_texture_swizzle
public const uint TEXTURE_SWIZZLE_R              =0x8E42;
public const uint TEXTURE_SWIZZLE_G              =0x8E43;
public const uint TEXTURE_SWIZZLE_B              =0x8E44;
public const uint TEXTURE_SWIZZLE_A              =0x8E45;
public const uint TEXTURE_SWIZZLE_RGBA           =0x8E46;

// GL_ARB_timer_query
public const uint TIME_ELAPSED                   =0x88BF;
public const uint TIMESTAMP                      =0x8E28;

// GL_ARB_vertex_type_2_10_10_10_rev
/* UNSIGNED_INT_2_10_10_10_REV */
public const uint INT_2_10_10_10_REV             =0x8D9F;

// GL_ARB_draw_indirect
public const uint DRAW_INDIRECT_BUFFER           =0x8F3F;
public const uint DRAW_INDIRECT_BUFFER_BINDING   =0x8F43;

// GL_ARB_gpu_shader5
public const uint GEOMETRY_SHADER_INVOCATIONS    =0x887F;
public const uint MAX_GEOMETRY_SHADER_INVOCATIONS =0x8E5A;
public const uint MIN_FRAGMENT_INTERPOLATION_OFFSET =0x8E5B;
public const uint MAX_FRAGMENT_INTERPOLATION_OFFSET =0x8E5C;
public const uint FRAGMENT_INTERPOLATION_OFFSET_BITS =0x8E5D;
/* MAX_VERTEX_STREAMS */

// GL_ARB_gpu_shader_fp64
/* DOUBLE */
public const uint DOUBLE_VEC2                    =0x8FFC;
public const uint DOUBLE_VEC3                    =0x8FFD;
public const uint DOUBLE_VEC4                    =0x8FFE;
public const uint DOUBLE_MAT2                    =0x8F46;
public const uint DOUBLE_MAT3                    =0x8F47;
public const uint DOUBLE_MAT4                    =0x8F48;
public const uint DOUBLE_MAT2x3                  =0x8F49;
public const uint DOUBLE_MAT2x4                  =0x8F4A;
public const uint DOUBLE_MAT3x2                  =0x8F4B;
public const uint DOUBLE_MAT3x4                  =0x8F4C;
public const uint DOUBLE_MAT4x2                  =0x8F4D;
public const uint DOUBLE_MAT4x3                  =0x8F4E;

// GL_ARB_shader_subroutine
public const uint ACTIVE_SUBROUTINES             =0x8DE5;
public const uint ACTIVE_SUBROUTINE_UNIFORMS     =0x8DE6;
public const uint ACTIVE_SUBROUTINE_UNIFORM_LOCATIONS =0x8E47;
public const uint ACTIVE_SUBROUTINE_MAX_LENGTH   =0x8E48;
public const uint ACTIVE_SUBROUTINE_UNIFORM_MAX_LENGTH =0x8E49;
public const uint MAX_SUBROUTINES                =0x8DE7;
public const uint MAX_SUBROUTINE_UNIFORM_LOCATIONS =0x8DE8;
public const uint NUM_COMPATIBLE_SUBROUTINES     =0x8E4A;
public const uint COMPATIBLE_SUBROUTINES         =0x8E4B;
/* UNIFORM_SIZE */
/* UNIFORM_NAME_LENGTH */

// GL_ARB_tessellation_shader
public const uint PATCHES                        =0x000E;
public const uint PATCH_VERTICES                 =0x8E72;
public const uint PATCH_DEFAULT_INNER_LEVEL      =0x8E73;
public const uint PATCH_DEFAULT_OUTER_LEVEL      =0x8E74;
public const uint TESS_CONTROL_OUTPUT_VERTICES   =0x8E75;
public const uint TESS_GEN_MODE                  =0x8E76;
public const uint TESS_GEN_SPACING               =0x8E77;
public const uint TESS_GEN_VERTEX_ORDER          =0x8E78;
public const uint TESS_GEN_POINT_MODE            =0x8E79;
/* TRIANGLES */
/* QUADS */
public const uint ISOLINES                       =0x8E7A;
/* EQUAL */
public const uint FRACTIONAL_ODD                 =0x8E7B;
public const uint FRACTIONAL_EVEN                =0x8E7C;
/* CCW */
/* CW */
public const uint MAX_PATCH_VERTICES             =0x8E7D;
public const uint MAX_TESS_GEN_LEVEL             =0x8E7E;
public const uint MAX_TESS_CONTROL_UNIFORM_COMPONENTS =0x8E7F;
public const uint MAX_TESS_EVALUATION_UNIFORM_COMPONENTS =0x8E80;
public const uint MAX_TESS_CONTROL_TEXTURE_IMAGE_UNITS =0x8E81;
public const uint MAX_TESS_EVALUATION_TEXTURE_IMAGE_UNITS =0x8E82;
public const uint MAX_TESS_CONTROL_OUTPUT_COMPONENTS =0x8E83;
public const uint MAX_TESS_PATCH_COMPONENTS      =0x8E84;
public const uint MAX_TESS_CONTROL_TOTAL_OUTPUT_COMPONENTS =0x8E85;
public const uint MAX_TESS_EVALUATION_OUTPUT_COMPONENTS =0x8E86;
public const uint MAX_TESS_CONTROL_UNIFORM_BLOCKS =0x8E89;
public const uint MAX_TESS_EVALUATION_UNIFORM_BLOCKS =0x8E8A;
public const uint MAX_TESS_CONTROL_INPUT_COMPONENTS =0x886C;
public const uint MAX_TESS_EVALUATION_INPUT_COMPONENTS =0x886D;
public const uint MAX_COMBINED_TESS_CONTROL_UNIFORM_COMPONENTS =0x8E1E;
public const uint MAX_COMBINED_TESS_EVALUATION_UNIFORM_COMPONENTS =0x8E1F;
public const uint UNIFORM_BLOCK_REFERENCED_BY_TESS_CONTROL_SHADER =0x84F0;
public const uint UNIFORM_BLOCK_REFERENCED_BY_TESS_EVALUATION_SHADER =0x84F1;
public const uint TESS_EVALUATION_SHADER         =0x8E87;
public const uint TESS_CONTROL_SHADER            =0x8E88;

// GL_ARB_texture_buffer_object_rgb32
/* RGB32F */
/* RGB32UI */
/* RGB32I */

// GL_ARB_transform_feedback2
public const uint TRANSFORM_FEEDBACK             =0x8E22;
public const uint TRANSFORM_FEEDBACK_BUFFER_PAUSED =0x8E23;
public const uint TRANSFORM_FEEDBACK_BUFFER_ACTIVE =0x8E24;
public const uint TRANSFORM_FEEDBACK_BINDING     =0x8E25;

// GL_ARB_transform_feedback3
public const uint MAX_TRANSFORM_FEEDBACK_BUFFERS =0x8E70;
public const uint MAX_VERTEX_STREAMS             =0x8E71;

// GL_ARB_ES2_compatibility
public const uint FIXED                          =0x140C;
public const uint IMPLEMENTATION_COLOR_READ_TYPE =0x8B9A;
public const uint IMPLEMENTATION_COLOR_READ_FORMAT =0x8B9B;
public const uint LOW_FLOAT                      =0x8DF0;
public const uint MEDIUM_FLOAT                   =0x8DF1;
public const uint HIGH_FLOAT                     =0x8DF2;
public const uint LOW_INT                        =0x8DF3;
public const uint MEDIUM_INT                     =0x8DF4;
public const uint HIGH_INT                       =0x8DF5;
public const uint SHADER_COMPILER                =0x8DFA;
public const uint NUM_SHADER_BINARY_FORMATS      =0x8DF9;
public const uint MAX_VERTEX_UNIFORM_VECTORS     =0x8DFB;
public const uint MAX_VARYING_VECTORS            =0x8DFC;
public const uint MAX_FRAGMENT_UNIFORM_VECTORS   =0x8DFD;

// GL_ARB_get_program_binary
public const uint PROGRAM_BINARY_RETRIEVABLE_HINT =0x8257;
public const uint PROGRAM_BINARY_LENGTH          =0x8741;
public const uint NUM_PROGRAM_BINARY_FORMATS     =0x87FE;
public const uint PROGRAM_BINARY_FORMATS         =0x87FF;

// GL_ARB_separate_shader_objects
public const uint VERTEX_SHADER_BIT              =0x00000001;
public const uint FRAGMENT_SHADER_BIT            =0x00000002;
public const uint GEOMETRY_SHADER_BIT            =0x00000004;
public const uint TESS_CONTROL_SHADER_BIT        =0x00000008;
public const uint TESS_EVALUATION_SHADER_BIT     =0x00000010;
public const uint ALL_SHADER_BITS                =0xFFFFFFFF;
public const uint PROGRAM_SEPARABLE              =0x8258;
public const uint ACTIVE_PROGRAM                 =0x8259;
public const uint PROGRAM_PIPELINE_BINDING       =0x825A;

// GL_ARB_shader_precision

// GL_ARB_vertex_attrib_64bit
/* RGB32I */
/* DOUBLE_VEC2 */
/* DOUBLE_VEC3 */
/* DOUBLE_VEC4 */
/* DOUBLE_MAT2 */
/* DOUBLE_MAT3 */
/* DOUBLE_MAT4 */
/* DOUBLE_MAT2x3 */
/* DOUBLE_MAT2x4 */
/* DOUBLE_MAT3x2 */
/* DOUBLE_MAT3x4 */
/* DOUBLE_MAT4x2 */
/* DOUBLE_MAT4x3 */

// GL_ARB_viewport_array
/* SCISSOR_BOX */
/* VIEWPORT */
/* DEPTH_RANGE */
/* SCISSOR_TEST */
public const uint MAX_VIEWPORTS                  =0x825B;
public const uint VIEWPORT_SUBPIXEL_BITS         =0x825C;
public const uint VIEWPORT_BOUNDS_RANGE          =0x825D;
public const uint LAYER_PROVOKING_VERTEX         =0x825E;
public const uint VIEWPORT_INDEX_PROVOKING_VERTEX =0x825F;
public const uint UNDEFINED_VERTEX               =0x8260;
/* FIRST_VERTEX_CONVENTION */
/* LAST_VERTEX_CONVENTION */
/* PROVOKING_VERTEX */

// GL_ARB_cl_event
public const uint SYNC_CL_EVENT_ARB              =0x8240;
public const uint SYNC_CL_EVENT_COMPLETE_ARB     =0x8241;

// GL_ARB_debug_output
public const uint DEBUG_OUTPUT_SYNCHRONOUS_ARB   =0x8242;
public const uint DEBUG_NEXT_LOGGED_MESSAGE_LENGTH_ARB =0x8243;
public const uint DEBUG_CALLBACK_FUNCTION_ARB    =0x8244;
public const uint DEBUG_CALLBACK_USER_PARAM_ARB  =0x8245;
public const uint DEBUG_SOURCE_API_ARB           =0x8246;
public const uint DEBUG_SOURCE_WINDOW_SYSTEM_ARB =0x8247;
public const uint DEBUG_SOURCE_SHADER_COMPILER_ARB =0x8248;
public const uint DEBUG_SOURCE_THIRD_PARTY_ARB   =0x8249;
public const uint DEBUG_SOURCE_APPLICATION_ARB   =0x824A;
public const uint DEBUG_SOURCE_OTHER_ARB         =0x824B;
public const uint DEBUG_TYPE_ERROR_ARB           =0x824C;
public const uint DEBUG_TYPE_DEPRECATED_BEHAVIOR_ARB =0x824D;
public const uint DEBUG_TYPE_UNDEFINED_BEHAVIOR_ARB =0x824E;
public const uint DEBUG_TYPE_PORTABILITY_ARB     =0x824F;
public const uint DEBUG_TYPE_PERFORMANCE_ARB     =0x8250;
public const uint DEBUG_TYPE_OTHER_ARB           =0x8251;
public const uint MAX_DEBUG_MESSAGE_LENGTH_ARB   =0x9143;
public const uint MAX_DEBUG_LOGGED_MESSAGES_ARB  =0x9144;
public const uint DEBUG_LOGGED_MESSAGES_ARB      =0x9145;
public const uint DEBUG_SEVERITY_HIGH_ARB        =0x9146;
public const uint DEBUG_SEVERITY_MEDIUM_ARB      =0x9147;
public const uint DEBUG_SEVERITY_LOW_ARB         =0x9148;
        
// GL_ARB_robustness
/* NO_ERROR */
public const uint CONTEXT_FLAG_ROBUST_ACCESS_BIT_ARB =0x00000004;
public const uint LOSE_CONTEXT_ON_RESET_ARB      =0x8252;
public const uint GUILTY_CONTEXT_RESET_ARB       =0x8253;
public const uint INNOCENT_CONTEXT_RESET_ARB     =0x8254;
public const uint UNKNOWN_CONTEXT_RESET_ARB      =0x8255;
public const uint RESET_NOTIFICATION_STRATEGY_ARB =0x8256;
public const uint NO_RESET_NOTIFICATION_ARB      =0x8261;

// GL_ARB_shader_stencil_export
    }
}