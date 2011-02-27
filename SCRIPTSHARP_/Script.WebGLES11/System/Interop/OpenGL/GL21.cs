using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public abstract class GL21
    {
        // GL_VERSION_2_0
        public const uint BLEND_EQUATION_RGB = 0x8009;
        public const uint VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
        public const uint VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
        public const uint VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
        public const uint VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
        public const uint CURRENT_VERTEX_ATTRIB = 0x8626;
        public const uint VERTEX_PROGRAM_POINT_SIZE = 0x8642;
        public const uint VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
        public const uint STENCIL_BACK_FUNC = 0x8800;
        public const uint STENCIL_BACK_FAIL = 0x8801;
        public const uint STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
        public const uint STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
        public const uint MAX_DRAW_BUFFERS = 0x8824;
        public const uint DRAW_BUFFER0 = 0x8825;
        public const uint DRAW_BUFFER1 = 0x8826;
        public const uint DRAW_BUFFER2 = 0x8827;
        public const uint DRAW_BUFFER3 = 0x8828;
        public const uint DRAW_BUFFER4 = 0x8829;
        public const uint DRAW_BUFFER5 = 0x882A;
        public const uint DRAW_BUFFER6 = 0x882B;
        public const uint DRAW_BUFFER7 = 0x882C;
        public const uint DRAW_BUFFER8 = 0x882D;
        public const uint DRAW_BUFFER9 = 0x882E;
        public const uint DRAW_BUFFER10 = 0x882F;
        public const uint DRAW_BUFFER11 = 0x8830;
        public const uint DRAW_BUFFER12 = 0x8831;
        public const uint DRAW_BUFFER13 = 0x8832;
        public const uint DRAW_BUFFER14 = 0x8833;
        public const uint DRAW_BUFFER15 = 0x8834;
        public const uint BLEND_EQUATION_ALPHA = 0x883D;
        public const uint MAX_VERTEX_ATTRIBS = 0x8869;
        public const uint VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
        public const uint MAX_TEXTURE_IMAGE_UNITS = 0x8872;
        public const uint FRAGMENT_SHADER = 0x8B30;
        public const uint VERTEX_SHADER = 0x8B31;
        public const uint MAX_FRAGMENT_UNIFORM_COMPONENTS = 0x8B49;
        public const uint MAX_VERTEX_UNIFORM_COMPONENTS = 0x8B4A;
        public const uint MAX_VARYING_FLOATS = 0x8B4B;
        public const uint MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
        public const uint MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
        public const uint SHADER_TYPE = 0x8B4F;
        public const uint FLOAT_VEC2 = 0x8B50;
        public const uint FLOAT_VEC3 = 0x8B51;
        public const uint FLOAT_VEC4 = 0x8B52;
        public const uint INT_VEC2 = 0x8B53;
        public const uint INT_VEC3 = 0x8B54;
        public const uint INT_VEC4 = 0x8B55;
        public const uint BOOL = 0x8B56;
        public const uint BOOL_VEC2 = 0x8B57;
        public const uint BOOL_VEC3 = 0x8B58;
        public const uint BOOL_VEC4 = 0x8B59;
        public const uint FLOAT_MAT2 = 0x8B5A;
        public const uint FLOAT_MAT3 = 0x8B5B;
        public const uint FLOAT_MAT4 = 0x8B5C;
        public const uint SAMPLER_1D = 0x8B5D;
        public const uint SAMPLER_2D = 0x8B5E;
        public const uint SAMPLER_3D = 0x8B5F;
        public const uint SAMPLER_CUBE = 0x8B60;
        public const uint SAMPLER_1D_SHADOW = 0x8B61;
        public const uint SAMPLER_2D_SHADOW = 0x8B62;
        public const uint DELETE_STATUS = 0x8B80;
        public const uint COMPILE_STATUS = 0x8B81;
        public const uint LINK_STATUS = 0x8B82;
        public const uint VALIDATE_STATUS = 0x8B83;
        public const uint INFO_LOG_LENGTH = 0x8B84;
        public const uint ATTACHED_SHADERS = 0x8B85;
        public const uint ACTIVE_UNIFORMS = 0x8B86;
        public const uint ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
        public const uint SHADER_SOURCE_LENGTH = 0x8B88;
        public const uint ACTIVE_ATTRIBUTES = 0x8B89;
        public const uint ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
        public const uint FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B;
        public const uint SHADING_LANGUAGE_VERSION = 0x8B8C;
        public const uint CURRENT_PROGRAM = 0x8B8D;
        public const uint POINT_SPRITE_COORD_ORIGIN = 0x8CA0;
        public const uint LOWER_LEFT = 0x8CA1;
        public const uint UPPER_LEFT = 0x8CA2;
        public const uint STENCIL_BACK_REF = 0x8CA3;
        public const uint STENCIL_BACK_VALUE_MASK = 0x8CA4;
        public const uint STENCIL_BACK_WRITEMASK = 0x8CA5;

        // GL_VERSION_2_1
        public const uint PIXEL_PACK_BUFFER = 0x88EB;
        public const uint PIXEL_UNPACK_BUFFER = 0x88EC;
        public const uint PIXEL_PACK_BUFFER_BINDING = 0x88ED;
        public const uint PIXEL_UNPACK_BUFFER_BINDING = 0x88EF;
        public const uint FLOAT_MAT2x3 = 0x8B65;
        public const uint FLOAT_MAT2x4 = 0x8B66;
        public const uint FLOAT_MAT3x2 = 0x8B67;
        public const uint FLOAT_MAT3x4 = 0x8B68;
        public const uint FLOAT_MAT4x2 = 0x8B69;
        public const uint FLOAT_MAT4x3 = 0x8B6A;
        public const uint SRGB = 0x8C40;
        public const uint SRGB8 = 0x8C41;
        public const uint SRGB_ALPHA = 0x8C42;
        public const uint SRGB8_ALPHA8 = 0x8C43;
        public const uint COMPRESSED_SRGB = 0x8C48;
        public const uint COMPRESSED_SRGB_ALPHA = 0x8C49;
    }
}