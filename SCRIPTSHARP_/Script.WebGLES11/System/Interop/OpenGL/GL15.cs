using System.Runtime.CompilerServices;
//[Khronos GL*]http://www.opengl.org/registry/api/gl3.h
namespace System.Interop.OpenGL
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public abstract class GL15
    {
        public const uint DEPTH_BUFFER_BIT = 0x00000100;
        public const uint STENCIL_BUFFER_BIT = 0x00000400;
        public const uint COLOR_BUFFER_BIT = 0x00004000;
        /* Boolean */
        public const uint FALSE = 0;
        public const uint TRUE = 1;
        /* BeginMode */
        public const uint POINTS = 0x0000;
        public const uint LINES = 0x0001;
        public const uint LINE_LOOP = 0x0002;
        public const uint LINE_STRIP = 0x0003;
        public const uint TRIANGLES = 0x0004;
        public const uint TRIANGLE_STRIP = 0x0005;
        public const uint TRIANGLE_FAN = 0x0006;
        /* AlphaFunction */
        public const uint NEVER = 0x0200;
        public const uint LESS = 0x0201;
        public const uint EQUAL = 0x0202;
        public const uint LEQUAL = 0x0203;
        public const uint GREATER = 0x0204;
        public const uint NOTEQUAL = 0x0205;
        public const uint GEQUAL = 0x0206;
        public const uint ALWAYS = 0x0207;
        /* BlendingFactorDest */
        public const uint ZERO = 0;
        public const uint ONE = 1;
        public const uint SRC_COLOR = 0x0300;
        public const uint ONE_MINUS_SRC_COLOR = 0x0301;
        public const uint SRC_ALPHA = 0x0302;
        public const uint ONE_MINUS_SRC_ALPHA = 0x0303;
        public const uint DST_ALPHA = 0x0304;
        public const uint ONE_MINUS_DST_ALPHA = 0x0305;
        /* BlendingFactorSrc */
        public const uint DST_COLOR = 0x0306;
        public const uint ONE_MINUS_DST_COLOR = 0x0307;
        public const uint SRC_ALPHA_SATURATE = 0x0308;
        /* DrawBufferMode */
        public const uint NONE = 0;
        public const uint FRONT_LEFT = 0x0400;
        public const uint FRONT_RIGHT = 0x0401;
        public const uint BACK_LEFT = 0x0402;
        public const uint BACK_RIGHT = 0x0403;
        public const uint FRONT = 0x0404;
        public const uint BACK = 0x0405;
        public const uint LEFT = 0x0406;
        public const uint RIGHT = 0x0407;
        public const uint FRONT_AND_BACK = 0x0408;
        /* ErrorCode */
        public const uint NO_ERROR = 0;
        public const uint INVALID_ENUM = 0x0500;
        public const uint INVALID_VALUE = 0x0501;
        public const uint INVALID_OPERATION = 0x0502;
        public const uint OUT_OF_MEMORY = 0x0505;
        /* FrontFaceDirection */
        public const uint CW = 0x0900;
        public const uint CCW = 0x0901;
        /* GetPName */
        public const uint POINT_SIZE = 0x0B11;
        public const uint POINT_SIZE_RANGE = 0x0B12;
        public const uint POINT_SIZE_GRANULARITY = 0x0B13;
        public const uint LINE_SMOOTH = 0x0B20;
        public const uint LINE_WIDTH = 0x0B21;
        public const uint LINE_WIDTH_RANGE = 0x0B22;
        public const uint LINE_WIDTH_GRANULARITY = 0x0B23;
        public const uint POLYGON_SMOOTH = 0x0B41;
        public const uint CULL_FACE = 0x0B44;
        public const uint CULL_FACE_MODE = 0x0B45;
        public const uint FRONT_FACE = 0x0B46;
        public const uint DEPTH_RANGE = 0x0B70;
        public const uint DEPTH_TEST = 0x0B71;
        public const uint DEPTH_WRITEMASK = 0x0B72;
        public const uint DEPTH_CLEAR_VALUE = 0x0B73;
        public const uint DEPTH_FUNC = 0x0B74;
        public const uint STENCIL_TEST = 0x0B90;
        public const uint STENCIL_CLEAR_VALUE = 0x0B91;
        public const uint STENCIL_FUNC = 0x0B92;
        public const uint STENCIL_VALUE_MASK = 0x0B93;
        public const uint STENCIL_FAIL = 0x0B94;
        public const uint STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public const uint STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public const uint STENCIL_REF = 0x0B97;
        public const uint STENCIL_WRITEMASK = 0x0B98;
        public const uint VIEWPORT = 0x0BA2;
        public const uint DITHER = 0x0BD0;
        public const uint BLEND_DST = 0x0BE0;
        public const uint BLEND_SRC = 0x0BE1;
        public const uint BLEND = 0x0BE2;
        public const uint LOGIC_OP_MODE = 0x0BF0;
        public const uint COLOR_LOGIC_OP = 0x0BF2;
        public const uint DRAW_BUFFER = 0x0C01;
        public const uint READ_BUFFER = 0x0C02;
        public const uint SCISSOR_BOX = 0x0C10;
        public const uint SCISSOR_TEST = 0x0C11;
        public const uint COLOR_CLEAR_VALUE = 0x0C22;
        public const uint COLOR_WRITEMASK = 0x0C23;
        public const uint DOUBLEBUFFER = 0x0C32;
        public const uint STEREO = 0x0C33;
        public const uint LINE_SMOOTH_HINT = 0x0C52;
        public const uint POLYGON_SMOOTH_HINT = 0x0C53;
        public const uint UNPACK_SWAP_BYTES = 0x0CF0;
        public const uint UNPACK_LSB_FIRST = 0x0CF1;
        public const uint UNPACK_ROW_LENGTH = 0x0CF2;
        public const uint UNPACK_SKIP_ROWS = 0x0CF3;
        public const uint UNPACK_SKIP_PIXELS = 0x0CF4;
        public const uint UNPACK_ALIGNMENT = 0x0CF5;
        public const uint PACK_SWAP_BYTES = 0x0D00;
        public const uint PACK_LSB_FIRST = 0x0D01;
        public const uint PACK_ROW_LENGTH = 0x0D02;
        public const uint PACK_SKIP_ROWS = 0x0D03;
        public const uint PACK_SKIP_PIXELS = 0x0D04;
        public const uint PACK_ALIGNMENT = 0x0D05;
        public const uint MAX_TEXTURE_SIZE = 0x0D33;
        public const uint MAX_VIEWPORT_DIMS = 0x0D3A;
        public const uint SUBPIXEL_BITS = 0x0D50;
        public const uint TEXTURE_1D = 0x0DE0;
        public const uint TEXTURE_2D = 0x0DE1;
        public const uint POLYGON_OFFSET_UNITS = 0x2A00;
        public const uint POLYGON_OFFSET_POINT = 0x2A01;
        public const uint POLYGON_OFFSET_LINE = 0x2A02;
        public const uint POLYGON_OFFSET_FILL = 0x8037;
        public const uint POLYGON_OFFSET_FACTOR = 0x8038;
        public const uint TEXTURE_BINDING_1D = 0x8068;
        public const uint TEXTURE_BINDING_2D = 0x8069;
        /* GetTextureParameter */
        public const uint TEXTURE_WIDTH = 0x1000;
        public const uint TEXTURE_HEIGHT = 0x1001;
        public const uint TEXTURE_INTERNAL_FORMAT = 0x1003;
        public const uint TEXTURE_BORDER_COLOR = 0x1004;
        public const uint TEXTURE_RED_SIZE = 0x805C;
        public const uint TEXTURE_GREEN_SIZE = 0x805D;
        public const uint TEXTURE_BLUE_SIZE = 0x805E;
        public const uint TEXTURE_ALPHA_SIZE = 0x805F;
        /* HintMode */
        public const uint DONT_CARE = 0x1100;
        public const uint FASTEST = 0x1101;
        public const uint NICEST = 0x1102;
        /* DataType */
        public const uint BYTE = 0x1400;
        public const uint UNSIGNED_BYTE = 0x1401;
        public const uint SHORT = 0x1402;
        public const uint UNSIGNED_SHORT = 0x1403;
        public const uint INT = 0x1404;
        public const uint UNSIGNED_INT = 0x1405;
        public const uint FLOAT = 0x1406;
        public const uint DOUBLE = 0x140A;
        /* LogicOp */
        public const uint CLEAR = 0x1500;
        public const uint AND = 0x1501;
        public const uint AND_REVERSE = 0x1502;
        public const uint COPY = 0x1503;
        public const uint AND_INVERTED = 0x1504;
        public const uint NOOP = 0x1505;
        public const uint XOR = 0x1506;
        public const uint OR = 0x1507;
        public const uint NOR = 0x1508;
        public const uint EQUIV = 0x1509;
        public const uint INVERT = 0x150A;
        public const uint OR_REVERSE = 0x150B;
        public const uint COPY_INVERTED = 0x150C;
        public const uint OR_INVERTED = 0x150D;
        public const uint NAND = 0x150E;
        public const uint SET = 0x150F;
        /* MatrixMode (for gl3.h, FBO attachment type) */
        public const uint TEXTURE = 0x1702;
        /* PixelCopyType */
        public const uint COLOR = 0x1800;
        public const uint DEPTH = 0x1801;
        public const uint STENCIL = 0x1802;
        /* PixelFormat */
        public const uint STENCIL_INDEX = 0x1901;
        public const uint DEPTH_COMPONENT = 0x1902;
        public const uint RED = 0x1903;
        public const uint GREEN = 0x1904;
        public const uint BLUE = 0x1905;
        public const uint ALPHA = 0x1906;
        public const uint RGB = 0x1907;
        public const uint RGBA = 0x1908;
        /* PolygonMode */
        public const uint POINT = 0x1B00;
        public const uint LINE = 0x1B01;
        public const uint FILL = 0x1B02;
        /* StencilOp */
        public const uint KEEP = 0x1E00;
        public const uint REPLACE = 0x1E01;
        public const uint INCR = 0x1E02;
        public const uint DECR = 0x1E03;
        /* StringName */
        public const uint VENDOR = 0x1F00;
        public const uint RENDERER = 0x1F01;
        public const uint VERSION = 0x1F02;
        public const uint EXTENSIONS = 0x1F03;
        /* TextureMagFilter */
        public const uint NEAREST = 0x2600;
        public const uint LINEAR = 0x2601;
        /* TextureMinFilter */
        public const uint NEAREST_MIPMAP_NEAREST = 0x2700;
        public const uint LINEAR_MIPMAP_NEAREST = 0x2701;
        public const uint NEAREST_MIPMAP_LINEAR = 0x2702;
        public const uint LINEAR_MIPMAP_LINEAR = 0x2703;
        /* TextureParameterName */
        public const uint TEXTURE_MAG_FILTER = 0x2800;
        public const uint TEXTURE_MIN_FILTER = 0x2801;
        public const uint TEXTURE_WRAP_S = 0x2802;
        public const uint TEXTURE_WRAP_T = 0x2803;
        /* TextureTarget */
        public const uint PROXY_TEXTURE_1D = 0x8063;
        public const uint PROXY_TEXTURE_2D = 0x8064;
        /* TextureWrapMode */
        public const uint REPEAT = 0x2901;
        /* PixelInternalFormat */
        public const uint R3_G3_B2 = 0x2A10;
        public const uint RGB4 = 0x804F;
        public const uint RGB5 = 0x8050;
        public const uint RGB8 = 0x8051;
        public const uint RGB10 = 0x8052;
        public const uint RGB12 = 0x8053;
        public const uint RGB16 = 0x8054;
        public const uint RGBA2 = 0x8055;
        public const uint RGBA4 = 0x8056;
        public const uint RGB5_A1 = 0x8057;
        public const uint RGBA8 = 0x8058;
        public const uint RGB10_A2 = 0x8059;
        public const uint RGBA12 = 0x805A;
        public const uint RGBA16 = 0x805B;

        // GL_VERSION_1_2
        public const uint UNSIGNED_BYTE_3_3_2 = 0x8032;
        public const uint UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const uint UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const uint UNSIGNED_INT_8_8_8_8 = 0x8035;
        public const uint UNSIGNED_INT_10_10_10_2 = 0x8036;
        public const uint TEXTURE_BINDING_3D = 0x806A;
        public const uint PACK_SKIP_IMAGES = 0x806B;
        public const uint PACK_IMAGE_HEIGHT = 0x806C;
        public const uint UNPACK_SKIP_IMAGES = 0x806D;
        public const uint UNPACK_IMAGE_HEIGHT = 0x806E;
        public const uint TEXTURE_3D = 0x806F;
        public const uint PROXY_TEXTURE_3D = 0x8070;
        public const uint TEXTURE_DEPTH = 0x8071;
        public const uint TEXTURE_WRAP_R = 0x8072;
        public const uint MAX_3D_TEXTURE_SIZE = 0x8073;
        public const uint UNSIGNED_BYTE_2_3_3_REV = 0x8362;
        public const uint UNSIGNED_SHORT_5_6_5 = 0x8363;
        public const uint UNSIGNED_SHORT_5_6_5_REV = 0x8364;
        public const uint UNSIGNED_SHORT_4_4_4_4_REV = 0x8365;
        public const uint UNSIGNED_SHORT_1_5_5_5_REV = 0x8366;
        public const uint UNSIGNED_INT_8_8_8_8_REV = 0x8367;
        public const uint UNSIGNED_INT_2_10_10_10_REV = 0x8368;
        public const uint BGR = 0x80E0;
        public const uint BGRA = 0x80E1;
        public const uint MAX_ELEMENTS_VERTICES = 0x80E8;
        public const uint MAX_ELEMENTS_INDICES = 0x80E9;
        public const uint CLAMP_TO_EDGE = 0x812F;
        public const uint TEXTURE_MIN_LOD = 0x813A;
        public const uint TEXTURE_MAX_LOD = 0x813B;
        public const uint TEXTURE_BASE_LEVEL = 0x813C;
        public const uint TEXTURE_MAX_LEVEL = 0x813D;
        public const uint SMOOTH_POINT_SIZE_RANGE = 0x0B12;
        public const uint SMOOTH_POINT_SIZE_GRANULARITY = 0x0B13;
        public const uint SMOOTH_LINE_WIDTH_RANGE = 0x0B22;
        public const uint SMOOTH_LINE_WIDTH_GRANULARITY = 0x0B23;
        public const uint ALIASED_LINE_WIDTH_RANGE = 0x846E;

        // GL_ARB_imaging
        public const uint CONSTANT_COLOR = 0x8001;
        public const uint ONE_MINUS_CONSTANT_COLOR = 0x8002;
        public const uint CONSTANT_ALPHA = 0x8003;
        public const uint ONE_MINUS_CONSTANT_ALPHA = 0x8004;
        public const uint BLEND_COLOR = 0x8005;
        public const uint FUNC_ADD = 0x8006;
        public const uint MIN = 0x8007;
        public const uint MAX = 0x8008;
        public const uint BLEND_EQUATION = 0x8009;
        public const uint FUNC_SUBTRACT = 0x800A;
        public const uint FUNC_REVERSE_SUBTRACT = 0x800B;

        // GL_VERSION_1_3
        public const uint TEXTURE0 = 0x84C0;
        public const uint TEXTURE1 = 0x84C1;
        public const uint TEXTURE2 = 0x84C2;
        public const uint TEXTURE3 = 0x84C3;
        public const uint TEXTURE4 = 0x84C4;
        public const uint TEXTURE5 = 0x84C5;
        public const uint TEXTURE6 = 0x84C6;
        public const uint TEXTURE7 = 0x84C7;
        public const uint TEXTURE8 = 0x84C8;
        public const uint TEXTURE9 = 0x84C9;
        public const uint TEXTURE10 = 0x84CA;
        public const uint TEXTURE11 = 0x84CB;
        public const uint TEXTURE12 = 0x84CC;
        public const uint TEXTURE13 = 0x84CD;
        public const uint TEXTURE14 = 0x84CE;
        public const uint TEXTURE15 = 0x84CF;
        public const uint TEXTURE16 = 0x84D0;
        public const uint TEXTURE17 = 0x84D1;
        public const uint TEXTURE18 = 0x84D2;
        public const uint TEXTURE19 = 0x84D3;
        public const uint TEXTURE20 = 0x84D4;
        public const uint TEXTURE21 = 0x84D5;
        public const uint TEXTURE22 = 0x84D6;
        public const uint TEXTURE23 = 0x84D7;
        public const uint TEXTURE24 = 0x84D8;
        public const uint TEXTURE25 = 0x84D9;
        public const uint TEXTURE26 = 0x84DA;
        public const uint TEXTURE27 = 0x84DB;
        public const uint TEXTURE28 = 0x84DC;
        public const uint TEXTURE29 = 0x84DD;
        public const uint TEXTURE30 = 0x84DE;
        public const uint TEXTURE31 = 0x84DF;
        public const uint ACTIVE_TEXTURE = 0x84E0;
        public const uint MULTISAMPLE = 0x809D;
        public const uint SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const uint SAMPLE_ALPHA_TO_ONE = 0x809F;
        public const uint SAMPLE_COVERAGE = 0x80A0;
        public const uint SAMPLE_BUFFERS = 0x80A8;
        public const uint SAMPLES = 0x80A9;
        public const uint SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const uint SAMPLE_COVERAGE_INVERT = 0x80AB;
        public const uint TEXTURE_CUBE_MAP = 0x8513;
        public const uint TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public const uint PROXY_TEXTURE_CUBE_MAP = 0x851B;
        public const uint MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;
        public const uint COMPRESSED_RGB = 0x84ED;
        public const uint COMPRESSED_RGBA = 0x84EE;
        public const uint TEXTURE_COMPRESSION_HINT = 0x84EF;
        public const uint TEXTURE_COMPRESSED_IMAGE_SIZE = 0x86A0;
        public const uint TEXTURE_COMPRESSED = 0x86A1;
        public const uint NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const uint COMPRESSED_TEXTURE_FORMATS = 0x86A3;
        public const uint CLAMP_TO_BORDER = 0x812D;

        // GL_VERSION_1_4
        public const uint BLEND_DST_RGB = 0x80C8;
        public const uint BLEND_SRC_RGB = 0x80C9;
        public const uint BLEND_DST_ALPHA = 0x80CA;
        public const uint BLEND_SRC_ALPHA = 0x80CB;
        public const uint POINT_FADE_THRESHOLD_SIZE = 0x8128;
        public const uint DEPTH_COMPONENT16 = 0x81A5;
        public const uint DEPTH_COMPONENT24 = 0x81A6;
        public const uint DEPTH_COMPONENT32 = 0x81A7;
        public const uint MIRRORED_REPEAT = 0x8370;
        public const uint MAX_TEXTURE_LOD_BIAS = 0x84FD;
        public const uint TEXTURE_LOD_BIAS = 0x8501;
        public const uint INCR_WRAP = 0x8507;
        public const uint DECR_WRAP = 0x8508;
        public const uint TEXTURE_DEPTH_SIZE = 0x884A;
        public const uint TEXTURE_COMPARE_MODE = 0x884C;
        public const uint TEXTURE_COMPARE_FUNC = 0x884D;

        // GL_VERSION_1_5
        public const uint BUFFER_SIZE = 0x8764;
        public const uint BUFFER_USAGE = 0x8765;
        public const uint QUERY_COUNTER_BITS = 0x8864;
        public const uint CURRENT_QUERY = 0x8865;
        public const uint QUERY_RESULT = 0x8866;
        public const uint QUERY_RESULT_AVAILABLE = 0x8867;
        public const uint ARRAY_BUFFER = 0x8892;
        public const uint ELEMENT_ARRAY_BUFFER = 0x8893;
        public const uint ARRAY_BUFFER_BINDING = 0x8894;
        public const uint ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;
        public const uint VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;
        public const uint READ_ONLY = 0x88B8;
        public const uint WRITE_ONLY = 0x88B9;
        public const uint READ_WRITE = 0x88BA;
        public const uint BUFFER_ACCESS = 0x88BB;
        public const uint BUFFER_MAPPED = 0x88BC;
        public const uint BUFFER_MAP_POINTER = 0x88BD;
        public const uint STREAM_DRAW = 0x88E0;
        public const uint STREAM_READ = 0x88E1;
        public const uint STREAM_COPY = 0x88E2;
        public const uint STATIC_DRAW = 0x88E4;
        public const uint STATIC_READ = 0x88E5;
        public const uint STATIC_COPY = 0x88E6;
        public const uint DYNAMIC_DRAW = 0x88E8;
        public const uint DYNAMIC_READ = 0x88E9;
        public const uint DYNAMIC_COPY = 0x88EA;
        public const uint SAMPLES_PASSED = 0x8914;
    }
}