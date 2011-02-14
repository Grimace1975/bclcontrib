using System.Runtime.CompilerServices;
using System.TypedArrays;
using System.Html.Media.Graphics;
using System.Html;
namespace System.WebGL
{
    [IgnoreNamespace, Imported]
    public class WebGLRenderingContext
    {
        /* ClearBufferMask */
        public const uint DEPTH_BUFFER_BIT = 0x00000100;
        public const uint STENCIL_BUFFER_BIT = 0x00000400;
        public const uint COLOR_BUFFER_BIT = 0x00004000;

        /* BeginMode */
        public const uint POINTS = 0x0000;
        public const uint LINES = 0x0001;
        public const uint LINE_LOOP = 0x0002;
        public const uint LINE_STRIP = 0x0003;
        public const uint TRIANGLES = 0x0004;
        public const uint TRIANGLE_STRIP = 0x0005;
        public const uint TRIANGLE_FAN = 0x0006;

        /* AlphaFunction (not supported in ES20) */
        /* NEVER */
        /* LESS */
        /* EQUAL */
        /* LEQUAL */
        /* GREATER */
        /* NOTEQUAL */
        /* GEQUAL */
        /* ALWAYS */

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
        /* ZERO */
        /* ONE */
        public const uint DST_COLOR = 0x0306;
        public const uint ONE_MINUS_DST_COLOR = 0x0307;
        public const uint SRC_ALPHA_SATURATE = 0x0308;
        /* SRC_ALPHA */
        /* ONE_MINUS_SRC_ALPHA */
        /* DST_ALPHA */
        /* ONE_MINUS_DST_ALPHA */

        /* BlendEquationSeparate */
        public const uint FUNC_ADD = 0x8006;
        public const uint BLEND_EQUATION = 0x8009;
        public const uint BLEND_EQUATION_RGB = 0x8009; /* same as BLEND_EQUATION */
        public const uint BLEND_EQUATION_ALPHA = 0x883D;

        /* BlendSubtract */
        public const uint FUNC_SUBTRACT = 0x800A;
        public const uint FUNC_REVERSE_SUBTRACT = 0x800B;

        /* Separate Blend Functions */
        public const uint BLEND_DST_RGB = 0x80C8;
        public const uint BLEND_SRC_RGB = 0x80C9;
        public const uint BLEND_DST_ALPHA = 0x80CA;
        public const uint BLEND_SRC_ALPHA = 0x80CB;
        public const uint CONSTANT_COLOR = 0x8001;
        public const uint ONE_MINUS_CONSTANT_COLOR = 0x8002;
        public const uint CONSTANT_ALPHA = 0x8003;
        public const uint ONE_MINUS_CONSTANT_ALPHA = 0x8004;
        public const uint BLEND_COLOR = 0x8005;

        /* Buffer Objects */
        public const uint ARRAY_BUFFER = 0x8892;
        public const uint ELEMENT_ARRAY_BUFFER = 0x8893;
        public const uint ARRAY_BUFFER_BINDING = 0x8894;
        public const uint ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;

        public const uint STREAM_DRAW = 0x88E0;
        public const uint STATIC_DRAW = 0x88E4;
        public const uint DYNAMIC_DRAW = 0x88E8;

        public const uint BUFFER_SIZE = 0x8764;
        public const uint BUFFER_USAGE = 0x8765;

        public const uint CURRENT_VERTEX_ATTRIB = 0x8626;

        /* CullFaceMode */
        public const uint FRONT = 0x0404;
        public const uint BACK = 0x0405;
        public const uint FRONT_AND_BACK = 0x0408;

        /* DepthFunction */
        /* NEVER */
        /* LESS */
        /* EQUAL */
        /* LEQUAL */
        /* GREATER */
        /* NOTEQUAL */
        /* GEQUAL */
        /* ALWAYS */

        /* EnableCap */
        /* TEXTURE_2D */
        public const uint CULL_FACE = 0x0B44;
        public const uint BLEND = 0x0BE2;
        public const uint DITHER = 0x0BD0;
        public const uint STENCIL_TEST = 0x0B90;
        public const uint DEPTH_TEST = 0x0B71;
        public const uint SCISSOR_TEST = 0x0C11;
        public const uint POLYGON_OFFSET_FILL = 0x8037;
        public const uint SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const uint SAMPLE_COVERAGE = 0x80A0;

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
        public const uint LINE_WIDTH = 0x0B21;
        public const uint ALIASED_POINT_SIZE_RANGE = 0x846D;
        public const uint ALIASED_LINE_WIDTH_RANGE = 0x846E;
        public const uint CULL_FACE_MODE = 0x0B45;
        public const uint FRONT_FACE = 0x0B46;
        public const uint DEPTH_RANGE = 0x0B70;
        public const uint DEPTH_WRITEMASK = 0x0B72;
        public const uint DEPTH_CLEAR_VALUE = 0x0B73;
        public const uint DEPTH_FUNC = 0x0B74;
        public const uint STENCIL_CLEAR_VALUE = 0x0B91;
        public const uint STENCIL_FUNC = 0x0B92;
        public const uint STENCIL_FAIL = 0x0B94;
        public const uint STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public const uint STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public const uint STENCIL_REF = 0x0B97;
        public const uint STENCIL_VALUE_MASK = 0x0B93;
        public const uint STENCIL_WRITEMASK = 0x0B98;
        public const uint STENCIL_BACK_FUNC = 0x8800;
        public const uint STENCIL_BACK_FAIL = 0x8801;
        public const uint STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
        public const uint STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
        public const uint STENCIL_BACK_REF = 0x8CA3;
        public const uint STENCIL_BACK_VALUE_MASK = 0x8CA4;
        public const uint STENCIL_BACK_WRITEMASK = 0x8CA5;
        public const uint VIEWPORT = 0x0BA2;
        public const uint SCISSOR_BOX = 0x0C10;
        /* SCISSOR_TEST */
        public const uint COLOR_CLEAR_VALUE = 0x0C22;
        public const uint COLOR_WRITEMASK = 0x0C23;
        public const uint UNPACK_ALIGNMENT = 0x0CF5;
        public const uint PACK_ALIGNMENT = 0x0D05;
        public const uint MAX_TEXTURE_SIZE = 0x0D33;
        public const uint MAX_VIEWPORT_DIMS = 0x0D3A;
        public const uint SUBPIXEL_BITS = 0x0D50;
        public const uint RED_BITS = 0x0D52;
        public const uint GREEN_BITS = 0x0D53;
        public const uint BLUE_BITS = 0x0D54;
        public const uint ALPHA_BITS = 0x0D55;
        public const uint DEPTH_BITS = 0x0D56;
        public const uint STENCIL_BITS = 0x0D57;
        public const uint POLYGON_OFFSET_UNITS = 0x2A00;
        /* POLYGON_OFFSET_FILL */
        public const uint POLYGON_OFFSET_FACTOR = 0x8038;
        public const uint TEXTURE_BINDING_2D = 0x8069;
        public const uint SAMPLE_BUFFERS = 0x80A8;
        public const uint SAMPLES = 0x80A9;
        public const uint SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const uint SAMPLE_COVERAGE_INVERT = 0x80AB;

        /* GetTextureParameter */
        /* TEXTURE_MAG_FILTER */
        /* TEXTURE_MIN_FILTER */
        /* TEXTURE_WRAP_S */
        /* TEXTURE_WRAP_T */

        public const uint NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const uint COMPRESSED_TEXTURE_FORMATS = 0x86A3;

        /* HintMode */
        public const uint DONT_CARE = 0x1100;
        public const uint FASTEST = 0x1101;
        public const uint NICEST = 0x1102;

        /* HintTarget */
        public const uint GENERATE_MIPMAP_HINT = 0x8192;

        /* DataType */
        public const uint BYTE = 0x1400;
        public const uint UNSIGNED_BYTE = 0x1401;
        public const uint SHORT = 0x1402;
        public const uint UNSIGNED_SHORT = 0x1403;
        public const uint INT = 0x1404;
        public const uint UNSIGNED_INT = 0x1405;
        public const uint FLOAT = 0x1406;

        /* PixelFormat */
        public const uint DEPTH_COMPONENT = 0x1902;
        public const uint ALPHA = 0x1906;
        public const uint RGB = 0x1907;
        public const uint RGBA = 0x1908;
        public const uint LUMINANCE = 0x1909;
        public const uint LUMINANCE_ALPHA = 0x190A;

        /* PixelType */
        /* UNSIGNED_BYTE */
        public const uint UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const uint UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const uint UNSIGNED_SHORT_5_6_5 = 0x8363;

        /* Shaders */
        public const uint FRAGMENT_SHADER = 0x8B30;
        public const uint VERTEX_SHADER = 0x8B31;
        public const uint MAX_VERTEX_ATTRIBS = 0x8869;
        public const uint MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
        public const uint MAX_VARYING_VECTORS = 0x8DFC;
        public const uint MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
        public const uint MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
        public const uint MAX_TEXTURE_IMAGE_UNITS = 0x8872;
        public const uint MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
        public const uint SHADER_TYPE = 0x8B4F;
        public const uint DELETE_STATUS = 0x8B80;
        public const uint LINK_STATUS = 0x8B82;
        public const uint VALIDATE_STATUS = 0x8B83;
        public const uint ATTACHED_SHADERS = 0x8B85;
        public const uint ACTIVE_UNIFORMS = 0x8B86;
        public const uint ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
        public const uint ACTIVE_ATTRIBUTES = 0x8B89;
        public const uint ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
        public const uint SHADING_LANGUAGE_VERSION = 0x8B8C;
        public const uint CURRENT_PROGRAM = 0x8B8D;

        /* StencilFunction */
        public const uint NEVER = 0x0200;
        public const uint LESS = 0x0201;
        public const uint EQUAL = 0x0202;
        public const uint LEQUAL = 0x0203;
        public const uint GREATER = 0x0204;
        public const uint NOTEQUAL = 0x0205;
        public const uint GEQUAL = 0x0206;
        public const uint ALWAYS = 0x0207;

        /* StencilOp */
        /* ZERO */
        public const uint KEEP = 0x1E00;
        public const uint REPLACE = 0x1E01;
        public const uint INCR = 0x1E02;
        public const uint DECR = 0x1E03;
        public const uint INVERT = 0x150A;
        public const uint INCR_WRAP = 0x8507;
        public const uint DECR_WRAP = 0x8508;

        /* StringName */
        public const uint VENDOR = 0x1F00;
        public const uint RENDERER = 0x1F01;
        public const uint VERSION = 0x1F02;

        /* TextureMagFilter */
        public const uint NEAREST = 0x2600;
        public const uint LINEAR = 0x2601;

        /* TextureMinFilter */
        /* NEAREST */
        /* LINEAR */
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
        public const uint TEXTURE_2D = 0x0DE1;
        public const uint TEXTURE = 0x1702;

        public const uint TEXTURE_CUBE_MAP = 0x8513;
        public const uint TEXTURE_BINDING_CUBE_MAP = 0x8514;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
        public const uint TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
        public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
        public const uint MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;

        /* TextureUnit */
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

        /* TextureWrapMode */
        public const uint REPEAT = 0x2901;
        public const uint CLAMP_TO_EDGE = 0x812F;
        public const uint MIRRORED_REPEAT = 0x8370;

        /* Uniform Types */
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
        public const uint SAMPLER_2D = 0x8B5E;
        public const uint SAMPLER_CUBE = 0x8B60;

        /* Vertex Arrays */
        public const uint VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
        public const uint VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
        public const uint VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
        public const uint VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
        public const uint VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
        public const uint VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
        public const uint VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;

        ///* Read Format */
        //public const uint IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;
        //public const uint IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;

        /* Shader Source */
        public const uint COMPILE_STATUS = 0x8B81;
        public const uint INFO_LOG_LENGTH = 0x8B84;
        public const uint SHADER_SOURCE_LENGTH = 0x8B88;

        /* Shader Precision-Specified Types */
        public const uint LOW_FLOAT = 0x8DF0;
        public const uint MEDIUM_FLOAT = 0x8DF1;
        public const uint HIGH_FLOAT = 0x8DF2;
        public const uint LOW_INT = 0x8DF3;
        public const uint MEDIUM_INT = 0x8DF4;
        public const uint HIGH_INT = 0x8DF5;

        /* Framebuffer Object. */
        public const uint FRAMEBUFFER = 0x8D40;
        public const uint RENDERBUFFER = 0x8D41;

        public const uint RGBA4 = 0x8056;
        public const uint RGB5_A1 = 0x8057;
        public const uint RGB565 = 0x8D62;
        public const uint DEPTH_COMPONENT16 = 0x81A5;
        public const uint STENCIL_INDEX = 0x1901;
        public const uint STENCIL_INDEX8 = 0x8D48;
        public const uint DEPTH_STENCIL = 0x84F9;

        public const uint RENDERBUFFER_WIDTH = 0x8D42;
        public const uint RENDERBUFFER_HEIGHT = 0x8D43;
        public const uint RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
        public const uint RENDERBUFFER_RED_SIZE = 0x8D50;
        public const uint RENDERBUFFER_GREEN_SIZE = 0x8D51;
        public const uint RENDERBUFFER_BLUE_SIZE = 0x8D52;
        public const uint RENDERBUFFER_ALPHA_SIZE = 0x8D53;
        public const uint RENDERBUFFER_DEPTH_SIZE = 0x8D54;
        public const uint RENDERBUFFER_STENCIL_SIZE = 0x8D55;

        public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
        public const uint FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
        public const uint FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;

        public const uint COLOR_ATTACHMENT0 = 0x8CE0;
        public const uint DEPTH_ATTACHMENT = 0x8D00;
        public const uint STENCIL_ATTACHMENT = 0x8D20;
        public const uint DEPTH_STENCIL_ATTACHMENT = 0x821A;

        public const uint NONE = 0;

        public const uint FRAMEBUFFER_COMPLETE = 0x8CD5;
        public const uint FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
        public const uint FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
        public const uint FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
        public const uint FRAMEBUFFER_UNSUPPORTED = 0x8CDD;

        public const uint FRAMEBUFFER_BINDING = 0x8CA6;
        public const uint RENDERBUFFER_BINDING = 0x8CA7;
        public const uint MAX_RENDERBUFFER_SIZE = 0x84E8;

        public const uint INVALID_FRAMEBUFFER_OPERATION = 0x0506;

        /* WebGL-specific enums */
        public const uint UNPACK_FLIP_Y_WEBGL = 0x9240;
        public const uint UNPACK_PREMULTIPLY_ALPHA_WEBGL = 0x9241;
        public const uint CONTEXT_LOST_WEBGL = 0x9242;
        public const uint UNPACK_COLORSPACE_CONVERSION_WEBGL = 0x9243;
        public const uint BROWSER_DEFAULT_WEBGL = 0x9244;


        protected WebGLRenderingContext() { }

        public CanvasElement Canvas
        {
            get { return null; }
        }
        public long DrawingBufferWidth
        {
            get { return 0; }
        }
        public long DrawingBufferHeight
        {
            get { return 0; }
        }

        public WebGLContextAttributes GetContextAttributes() { return null; }
        public bool IsContextLost() { return false; }

        public string[] GetSupportedExtensions() { return null; }
        public object GetExtension(string name) { return null; }

        public void ActiveTexture(uint texture) { }
        public void AttachShader(WebGLProgram program, WebGLShader shader) { }
        public void BindAttribLocation(WebGLProgram program, int index, string name) { }
        public void BindBuffer(uint target, WebGLBuffer buffer) { }
        public void BindFramebuffer(uint target, WebGLFramebuffer framebuffer) { }
        public void BindRenderbuffer(uint target, WebGLRenderbuffer renderbuffer) { }
        public void BindTexture(uint target, WebGLTexture texture) { }
        public void BlendColor(float red, float green, float blue, float alpha) { }
        public void BlendEquation(uint mode) { }
        public void BlendEquationSeparate(uint modeRGB, uint modeAlpha) { }
        public void BlendFunc(uint sfactor, uint dfactor) { }
        public void BlendFuncSeparate(uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha) { }

        public void BufferData(uint target, int size, int usage) { }
        public void BufferData(uint target, ArrayBufferView data, int usage) { }
        public void BufferData(uint target, ArrayBuffer data, int usage) { }
        public void bufferSubData(uint target, long offset, ArrayBufferView data) { }
        public void BufferSubData(uint target, long offset, ArrayBuffer data) { }

        public uint CheckFramebufferStatus(uint target) { return 0; }
        public void Clear(uint mask) { }
        public void ClearColor(float red, float green, float blue, float alpha) { }
        public void ClearDepth(float depth) { }
        public void ClearStencil(int s) { }
        public void ColorMask(bool red, bool green, bool blue, bool alpha) { }
        public void CompileShader(WebGLShader shader) { }

        public void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border) { }
        public void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height) { }

        public WebGLBuffer CreateBuffer() { return null; }
        public WebGLFramebuffer CreateFramebuffer() { return null; }
        public WebGLProgram CreateProgram() { return null; }
        public WebGLRenderbuffer CreateRenderbuffer() { return null; }
        public WebGLShader CreateShader(int type) { return null; }
        public WebGLTexture CreateTexture() { return null; }

        public void CullFace(uint mode) { }

        public void DeleteBuffer(WebGLBuffer buffer) { }
        public void DeleteFramebuffer(WebGLFramebuffer framebuffer) { }
        public void DeleteProgram(WebGLProgram program) { }
        public void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer) { }
        public void DeleteShader(WebGLShader shader) { }
        public void DeleteTexture(WebGLTexture texture) { }

        public void DepthFunc(uint func) { }
        public void DepthMask(bool flag) { }
        public void DepthRange(float zNear, float zFar) { }
        public void DetachShader(WebGLProgram program, WebGLShader shader) { }
        public void Disable(uint cap) { }
        public void DisableVertexAttribArray(uint index) { }
        public void DrawArrays(uint mode, uint first, int count) { }
        public void DrawElements(uint mode, int count, uint type, long offset) { }

        public void Enable(uint cap) { }
        public void EnableVertexAttribArray(uint index) { }
        public void Finish() { }
        public void Flush() { }
        public void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, WebGLRenderbuffer renderbuffer) { }
        public void FramebufferTexture2D(uint target, uint attachment, uint textarget, WebGLTexture texture, int level) { }
        public void FrontFace(uint mode) { }

        public void GenerateMipmap(uint target) { }

        public WebGLActiveInfo GetActiveAttrib(WebGLProgram program, int index) { return null; }
        public WebGLActiveInfo GetActiveUniform(WebGLProgram program, int index) { return null; }
        public WebGLShader[] GetAttachedShaders(WebGLProgram program) { return null; }

        public int GetAttribLocation(WebGLProgram program, string name) { return 0; }

        public object GetParameter(uint pname) { return null; }
        public object GetBufferParameter(uint target, uint pname) { return null; }

        public uint GetError() { return 0; }

        public object GetFramebufferAttachmentParameter(uint target, uint attachment, uint pname) { return null; }
        public object GetProgramParameter(WebGLProgram program, uint pname) { return null; }
        public string GetProgramInfoLog(WebGLProgram program) { return null; }
        public object GetRenderbufferParameter(uint target, uint pname) { return null; }
        public object GetShaderParameter(WebGLShader shader, uint pname) { return null; }
        public string GetShaderInfoLog(WebGLShader shader) { return null; }

        public string GetShaderSource(WebGLShader shader) { return null; }

        public object GetTexParameter(uint target, uint pname) { return null; }

        public object GetUniform(WebGLProgram program, WebGLUniformLocation location) { return null; }

        public WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name) { return null; }

        public object GetVertexAttrib(uint index, uint pname) { return null; }

        public long GetVertexAttribOffset(uint index, uint pname) { return 0; }

        public void Hint(uint target, uint mode) { }
        public bool IsBuffer(WebGLBuffer buffer) { return false; }
        public bool IsEnabled(uint cap) { return false; }
        public bool IsFramebuffer(WebGLFramebuffer framebuffer) { return false; }
        public bool IsProgram(WebGLProgram program) { return false; }
        public bool IsRenderbuffer(WebGLRenderbuffer renderbuffer) { return false; }
        public bool IsShader(WebGLShader shader) { return false; }
        public bool IsTexture(WebGLTexture texture) { return false; }
        public void LineWidth(float width) { }
        public void LinkProgram(WebGLProgram program) { }
        public void PixelStorei(uint pname, uint param) { }
        public void PolygonOffset(float factor, float units) { }

        public void ReadPixels(int x, int y, int width, int height, uint format, uint type, ArrayBufferView pixels) { }

        public void RenderbufferStorage(uint target, uint internalformat, int width, int height) { }
        public void SampleCoverage(float value, bool invert) { }
        public void Scissor(int x, int y, int width, int height) { }

        public void ShaderSource(WebGLShader shader, string source) { }

        public void StencilFunc(uint func, int @ref, uint mask) { }
        public void StencilFuncSeparate(uint face, int func, int @ref, uint mask) { }
        public void StencilMask(uint mask) { }
        public void StencilMaskSeparate(uint face, uint mask) { }
        public void StencilOp(uint fail, uint zfail, uint zpass) { }
        public void StencilOpSeparate(int face, int fail, int zfail, int zpass) { }

        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, ArrayBufferView pixels) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, ImageData pixels) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, ImageElement image) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, CanvasElement canvas) { }
        public void TexImage2D(uint target, int level, uint internalformat, uint format, int type, VideoElement video) { }


        public void TexParameterf(uint target, uint pname, float param) { }
        public void TexParameteri(uint target, uint pname, int param) { }

        public void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, ArrayBufferView pixels) { }
        public void TexSubImage2D(uint target, int level, int xoffset, int yoffset, uint format, uint type, ImageData pixels) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, ImageElement image) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, CanvasElement canvas) { }
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, uint format, uint type, VideoElement video) { }

        public void Uniform1f(WebGLUniformLocation location, float x) { }
        public void Uniform1fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform1fv(WebGLUniformLocation location, float[] v) { }
        public void Uniform1i(WebGLUniformLocation location, int x) { }
        public void Uniform1iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform1iv(WebGLUniformLocation location, int[] v) { }
        public void Uniform2f(WebGLUniformLocation location, float x, float y) { }
        public void Uniform2fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform2fv(WebGLUniformLocation location, float[] v) { }
        public void Uniform2i(WebGLUniformLocation location, int x, int y) { }
        public void Uniform2iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform2iv(WebGLUniformLocation location, int[] v) { }
        public void Uniform3f(WebGLUniformLocation location, float x, float y, float z) { }
        public void Uniform3fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform3fv(WebGLUniformLocation location, float[] v) { }
        public void Uniform3i(WebGLUniformLocation location, int x, int y, int z) { }
        public void Uniform3iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform3iv(WebGLUniformLocation location, int[] v) { }
        public void Uniform4f(WebGLUniformLocation location, float x, float y, float z, float w) { }
        public void Uniform4fv(WebGLUniformLocation location, Float32Array v) { }
        public void Uniform4fv(WebGLUniformLocation location, float[] v) { }
        public void Uniform4i(WebGLUniformLocation location, int x, int y, int z, int w) { }
        public void Uniform4iv(WebGLUniformLocation location, Int32Array v) { }
        public void Uniform4iv(WebGLUniformLocation location, int[] v) { }

        public void UniformMatrix2fv(WebGLUniformLocation location, bool transpose, Float32Array value) { }
        public void UniformMatrix2fv(WebGLUniformLocation location, bool transpose, float[] value) { }
        public void UniformMatrix3fv(WebGLUniformLocation location, bool transpose, Float32Array value) { }
        public void UniformMatrix3fv(WebGLUniformLocation location, bool transpose, float[] value) { }
        public void UniformMatrix4fv(WebGLUniformLocation location, bool transpose, Float32Array value) { }
        public void UniformMatrix4fv(WebGLUniformLocation location, bool transpose, float[] value) { }


        public void UseProgram(WebGLProgram program) { }
        public void ValidateProgram(WebGLProgram program) { }

        public void VertexAttrib1f(uint indx, float x) { }
        public void VertexAttrib1fv(uint indx, Float32Array values) { }
        public void VertexAttrib1fv(uint indx, float[] values) { }
        public void VertexAttrib2f(uint indx, float x, float y) { }
        public void VertexAttrib2fv(uint indx, Float32Array values) { }
        public void VertexAttrib2fv(uint indx, float[] values) { }
        public void VertexAttrib3f(uint indx, float x, float y, float z) { }
        public void VertexAttrib3fv(uint indx, Float32Array values) { }
        public void VertexAttrib3fv(uint indx, float[] values) { }
        public void VertexAttrib4f(uint indx, float x, float y, float z, float w) { }
        public void VertexAttrib4fv(uint indx, Float32Array values) { }
        public void VertexAttrib4fv(uint indx, float[] values) { }
        public void VertexAttribPointer(uint indx, int size, uint type, bool normalized, int stride, long offset) { }

        public void Viewport(int x, int y, int width, int height) { }
    }
}