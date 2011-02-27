//[Khronos]http://www.khronos.org/opengles/1_X/
#if !CODE_ANALYSIS
using System.IO;
using System.Collections;
namespace System.Interop.OpenGL
#else
using System;
using SystemEx.IO;
using System.Collections;
using System.Interop.OpenGL;
using System.Html;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class WebGLES11RenderingContext : WebGLRenderingContext
    {
        protected const uint ARRAY_POSITION = 0;
        protected const uint ARRAY_COLOR = 1;
        protected const uint ARRAY_TEXCOORD_0 = 2;
        protected const uint ARRAY_TEXCOORD_1 = 3;
        private uint _matrixMode = GLES11.MODELVIEW;
        private int _viewportX;
        private int _viewportY;
        private int _viewportW;
        private int _viewportH;
        private float[] _projectionMatrix = new float[16];
        private float[] _modelViewMatrix = new float[16];
        private float[] _textureMatrix = new float[16];
        private float[] _currentMatrix;
        private Stack _projectionMatrixStack = new Stack();
        private Stack _modelViewMatrixStack = new Stack();
        private Stack _textureMatrixStack = new Stack();
        private Stack _currentMatrixStack;
        private float[] _tmpMatrix = new float[16];
        protected float[] _mvpMatrix = new float[16];
        private bool _mvpDirty = true;
        private int _width;
        private int _height;

        public WebGLES11RenderingContext(int width, int height)
        {
            MathMatrix.SetIdentityM(_modelViewMatrix, 0);
            MathMatrix.SetIdentityM(_projectionMatrix, 0);
            MathMatrix.SetIdentityM(_textureMatrix, 0);
            _width = width;
            _height = height;
        }

        /* Available only in Common profile */
        public virtual void AlphaFunc(uint func, float r) { }
//# public abstract void ClearColor(float red, float green, float blue, float alpha);
        // ClearDepthf (GLclampf depth);
        // ClipPlanef (GLenum plane, const GLfloat *equation);
        public abstract void Color4f(float red, float green, float blue, float alpha);
        public abstract void DepthRangef(float zNear, float zFar);
        // Fogf (GLenum pname, GLfloat param);
        // Fogfv (GLenum pname, const GLfloat *params);
        public virtual void Frustumf(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            double a = 2 * zNear;
            double b = right - left;
            double c = top - bottom;
            double d = zFar - zNear;
            float[] matrix = {
		        (float)(a / b), 0,  0, 0,
		        0,  (float)(a / c), 0, 0,
		        (float)((right + left) / b), (float)((top + bottom) / c), (float)((-zFar - zNear) / d), -1,
		        0, 0, (float)((-a * zFar) / d), 0
            };
            MultMatrixf(matrix);
        }
        // GetClipPlanef (GLenum pname, GLfloat eqn[4]);
        public virtual void GetFloatv(uint name, Stream s)
        {
            switch (name)
            {
                case GLES11.MODELVIEW:
                case GLES11.MODELVIEW_MATRIX:
                    long p = s.Position;
                    for (int index = 0; index < _modelViewMatrix.Length; index++)
                        SE.WriteSingle(s, _modelViewMatrix[index]);
                    s.Position = p;
                    break;
                default:
                    throw new Exception("ArgumentException: glGetFloat");
            }
        }
        // GetLightfv (GLenum light, GLenum pname, GLfloat *params);
        // GetMaterialfv (GLenum face, GLenum pname, GLfloat *params);
        // GetTexEnvfv (GLenum env, GLenum pname, GLfloat *params);
        // GetTexParameterfv (GLenum target, GLenum pname, GLfloat *params);
        // LightModelf (GLenum pname, GLfloat param);
        // LightModelfv (GLenum pname, const GLfloat *params);
        // Lightf (GLenum light, GLenum pname, GLfloat param);
        // Lightfv (GLenum light, GLenum pname, const GLfloat *params);
        // LineWidth (GLfloat width);
        public virtual void LoadMatrixf(Stream s)
        {
            long p = s.Position;
            for (int index = 0; index < _currentMatrix.Length; index++)
                _currentMatrix[index] = SE.ReadSingle(s);
            s.Position = p;
            _mvpDirty = true;
        }
        // Materialf (GLenum face, GLenum pname, GLfloat param);
        // Materialfv (GLenum face, GLenum pname, const GLfloat *params);
        public virtual void MultMatrixf(float[] m) //, int ofs)
        {
            int ofs = 0;
            MathMatrix.MultiplyMM(_tmpMatrix, 0, _currentMatrix, 0, m, ofs);
            JSArrayEx.Copy(_tmpMatrix, 0, _currentMatrix, 0, 16);
            _mvpDirty = true;
        }
        // MultiTexCoord4f (GLenum target, GLfloat s, GLfloat t, GLfloat r, GLfloat q);
        // Normal3f (GLfloat nx, GLfloat ny, GLfloat nz);
        public virtual void Orthof(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            float[] matrix = {
		        2f/(right-left), 0,  0, 0,
		        //
		        0,  2f/(top-bottom), 0, 0,
		        //
		        0, 0, -2f/zFar-zNear, 0,
		        //
		        -(right+left)/(right-left), -(top+bottom)/(top-bottom), -(zFar+zNear)/(zFar-zNear), 1f
            };
            MultMatrixf(matrix);
            _mvpDirty = true;
        }
        public abstract void PointParameterf(uint pname, float param);
        public abstract void PointParameterfv(uint pname, Stream @params);
        public abstract void PointSize(float size);
        // PolygonOffset (GLfloat factor, GLfloat units);
        public virtual void Rotatef(float angle, float x, float y, float z)
        {
            if ((x != 0) || (y != 0) || (z != 0))
                // right thing to do? or rotate around a default axis?
                MathMatrix.RotateM2(_currentMatrix, 0, angle, x, y, z);
            _mvpDirty = true;
        }
        public virtual void Scalef(float x, float y, float z)
        {
            MathMatrix.ScaleM2(_currentMatrix, 0, x, y, z);
            _mvpDirty = true;
        }
        // TexEnvf (GLenum target, GLenum pname, GLfloat param);
        // TexEnvfv (GLenum target, GLenum pname, const GLfloat *params);
//# public abstract void TexParameterf(uint target, uint pname, float param);
        public abstract void TexParameterfv(uint target, uint pname, Stream @params);
        public virtual void Translatef(float x, float y, float z)
        {
            MathMatrix.TranslateM2(_currentMatrix, 0, x, y, z);
            _mvpDirty = true;
        }


        /* Available in both Common and Common-Lite profiles */
        //# public abstract void ActiveTexture(uint texture);
        public virtual void AlphaFuncx(uint func, int r) { }
        // BindBuffer (GLenum target, GLuint buffer);
        public abstract void BindTexture(uint target, uint texture);
//# public abstract void BlendFunc(uint sfactor, uint dfactor);
        // BufferData (GLenum target, GLsizeiptr size, const GLvoid *data, GLenum usage);
        // BufferSubData (GLenum target, GLintptr offset, GLsizeiptr size, const GLvoid *data);
//# public abstract void Clear(uint mask);
        // ClearColorx (GLclampx red, GLclampx green, GLclampx blue, GLclampx alpha);
        // ClearDepthx (GLclampx depth);
        // ClearStencil (GLint s);
        public abstract void ClientActiveTexture(uint texture);
        // ClipPlanex (GLenum plane, const GLfixed *equation);
        // Color4ub (GLubyte red, GLubyte green, GLubyte blue, GLubyte alpha);
        // Color4x (GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);
        // ColorMask (GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
        public abstract void ColorPointer(int size, uint type, int stride, Stream pointer);
        // CompressedTexImage2D (GLenum target, GLint level, GLenum internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, const GLvoid *data);
        // CompressedTexSubImage2D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, const GLvoid *data);
        // CopyTexImage2D (GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
        // CopyTexSubImage2D (GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
        // CullFace (GLenum mode);
        // DeleteBuffers (GLsizei n, const GLuint *buffers);
        public abstract void DeleteTextures(int n, Stream textures);
//# public abstract void DepthFunc(uint func);
//# public abstract void DepthMask(bool flag);
        public abstract void DepthRangex(int zNear, int zFar);
//# public abstract void Disable(uint cap);
        public abstract void DisableClientState(uint array);
//# public abstract void DrawArrays(uint mode, int first, int count);
        public abstract void DrawElements(uint mode, int count, uint type, Stream indicies);
//# public abstract void Enable(uint cap);
        public abstract void EnableClientState(uint array);
//# public abstract void Finish();
        // Flush (void);
        // Fogx (GLenum pname, GLfixed param);
        // Fogxv (GLenum pname, const GLfixed *params);
        // FrontFace (GLenum mode);
        // Frustumx (GLfixed left, GLfixed right, GLfixed bottom, GLfixed top, GLfixed zNear, GLfixed zFar);
        // GetBooleanv (GLenum pname, GLboolean *params);
        // GetBufferParameteriv (GLenum target, GLenum pname, GLint *params);
        // GetClipPlanex (GLenum pname, GLfixed eqn[4]);
        // GenBuffers (GLsizei n, GLuint *buffers);
        // GenTextures (GLsizei n, GLuint *textures);
//# public abstract uint GetError();
        // GetFixedv (GLenum pname, GLfixed *params);
        public virtual void GetIntegerv(uint pname, Stream s)
        {
            switch (pname)
            {
                case GLES11.MATRIX_MODE:
                    SE.WriteUInt32(s, _matrixMode);
                    break;
                default:
                    throw new Exception("ArgumentException:");
            }
        }
        // GetLightxv (GLenum light, GLenum pname, GLfixed *params);
        // GetMaterialxv (GLenum face, GLenum pname, GLfixed *params);
        // GetPointerv (GLenum pname, GLvoid **params);
        public abstract string GetString(uint name);
        // GetTexEnviv (GLenum env, GLenum pname, GLint *params);
        // GetTexEnvxv (GLenum env, GLenum pname, GLfixed *params);
        // GetTexParameteriv (GLenum target, GLenum pname, GLint *params);
        // GetTexParameterxv (GLenum target, GLenum pname, GLfixed *params);
        // Hint (GLenum target, GLenum mode);
        // IsBuffer (GLuint buffer);
        // IsEnabled (GLenum cap);
        // IsTexture (GLuint texture);
        // LightModelx (GLenum pname, GLfixed param);
        // LightModelxv (GLenum pname, const GLfixed *params);
        // Lightx (GLenum light, GLenum pname, GLfixed param);
        // Lightxv (GLenum light, GLenum pname, const GLfixed *params);
        // LineWidthx (GLfixed width);
        public virtual void LoadIdentity()
        {
            MathMatrix.SetIdentityM(_currentMatrix, 0);
            _mvpDirty = true;
        }
        public virtual void LoadMatrixx(Stream m)
        {
            long p = m.Position;
            for (int index = 0; index < _currentMatrix.Length; index++)
                _currentMatrix[index] = (float)SE.ReadUInt32(m);
            m.Position = p;
            _mvpDirty = true;
        }
        // LogicOp (GLenum opcode);
        // Materialx (GLenum face, GLenum pname, GLfixed param);
        // Materialxv (GLenum face, GLenum pname, const GLfixed *params);
        public virtual void MatrixMode(uint mode)
        {
            switch (mode)
            {
                case GLES11.MODELVIEW:
                    _currentMatrix = _modelViewMatrix;
                    _currentMatrixStack = _modelViewMatrixStack;
                    break;
                case GLES11.PROJECTION:
                    _currentMatrix = _projectionMatrix;
                    _currentMatrixStack = _projectionMatrixStack;
                    break;
                case TEXTURE:
                    _currentMatrix = _textureMatrix;
                    _currentMatrixStack = _textureMatrixStack;
                    break;
                default:
                    throw new Exception("ArgumentException: Unrecoginzed matrix mode: " + mode);
            }
            _matrixMode = mode;
        }
        // MultMatrixx (const GLfixed *m);
        // MultiTexCoord4x (GLenum target, GLfixed s, GLfixed t, GLfixed r, GLfixed q);
        // Normal3x (GLfixed nx, GLfixed ny, GLfixed nz);
        // NormalPointer (GLenum type, GLsizei stride, const GLvoid *pointer);
        public virtual void Orthox(int left, int right, int bottom, int top, int zNear, int zFar)
        {
            float l = left;
            float r = right;
            float b = bottom;
            float n = zNear;
            float f = zFar;
            float t = top;
            float[] matrix = {
		        2f/(r-l), 0,  0, 0,
		        0,  2f/(t-b), 0, 0,
		        0, 0, -2f/f-n, 0,
		        -(r+l)/(r-l), -(t+b)/(t-b), -(f+n)/(f-n), 1f
            };
            MultMatrixf(matrix);
            _mvpDirty = true;
        }
//# public abstract void PixelStorei(uint pname, int param);
        public abstract void PointParameterx(uint pname, int param);
        public abstract void PointParameterxv(uint pname, Stream @params);
        public abstract void PointSizex(int size);
        // PolygonOffsetx (GLfixed factor, GLfixed units);
        public virtual void PopMatrix()
        {
            float[] top = (float[])_currentMatrixStack.Pop();
            JSArrayEx.Copy(top, 0, _currentMatrix, 0, 16);
            _mvpDirty = true;
        }
        public virtual void PushMatrix()
        {
            float[] copy = new float[16];
            JSArrayEx.Copy(_currentMatrix, 0, copy, 0, 16);
            _currentMatrixStack.Push(copy);
        }
        public abstract void ReadPixels(int x, int y, int width, int height, uint format, uint type, Stream pixels);
        // Rotatex (GLfixed angle, GLfixed x, GLfixed y, GLfixed z);
        // SampleCoverage (GLclampf value, GLboolean invert);
        // SampleCoveragex (GLclampx value, GLboolean invert);
        public virtual void Scalex(int x, int y, int z)
        {
            MathMatrix.ScaleM2(_currentMatrix, 0, (float)x, (float)y, (float)z);
            _mvpDirty = true;
        }
        //# public abstract void Scissor(int x, int y, int width, int height);
        public abstract void ShadeModel(uint mode);
        // StencilFunc (GLenum func, GLint ref, GLuint mask);
        // StencilMask (GLuint mask);
        // StencilOp (GLenum fail, GLenum zfail, GLenum zpass);
        public abstract void TexCoordPointer(int size, uint type, int stride, Stream pointer);
        public abstract void TexEnvi(uint target, uint pname, int param);
        public abstract void TexEnvx(uint target, uint pname, int param);
        public abstract void TexEnviv(uint target, uint pname, Stream @params);
        public abstract void TexEnvxv(uint target, uint pname, Stream @params);
        public abstract void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, Stream pixels);
#if CODE_ANALYSIS
        public abstract void TexImage2Di(uint target, int level, uint internalformat, uint format, uint type, ImageElement image);
        public abstract void TexImage2De(uint target, int level, uint internalformat, uint format, uint type, CanvasElement canvas);
        public abstract void TexImage2Dx(uint target, int level, uint internalformat, uint format, uint type, CanvasElementEx canvas);
#endif
        //# public abstract void TexParameteri(uint target, uint pname, int param);
        public abstract void TexParameterx(uint target, uint pname, int param);
        public abstract void TexParameteriv(uint target, uint pname, Stream @params);
        public abstract void TexParameterxv(uint target, uint pname, Stream @params);
        public abstract void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, Stream pixels);
        public virtual void Translatex(int x, int y, int z)
        {
            MathMatrix.TranslateM2(_currentMatrix, 0, (float)x, (float)y, (float)z);
            _mvpDirty = true;
        }
        public abstract void VertexPointer(int size, uint type, int stride, Stream pointer);
        public override void Viewport(int x, int y, int width, int height)
        {
            _viewportX = x;
            _viewportY = y;
            _viewportW = width;
            _viewportH = height;
        }


        /* Available only here */
        protected bool UpdateMvpMatrix()
        {
            if (!_mvpDirty)
                return false;
            MathMatrix.MultiplyMM(_mvpMatrix, 0, _projectionMatrix, 0, _modelViewMatrix, 0);
            _mvpDirty = false;
            return true;
        }

        public bool Project(float objX, float objY, float objZ, int[] view, float[] win)
        {
            float[] v = { objX, objY, objZ, 1f };
            float[] v2 = new float[4];
            MathMatrix.MultiplyMV(v2, 0, _mvpMatrix, 0, v, 0);
            float w = v2[3];
            if (w == 0.0f)
                return false;
            float rw = 1.0f / w;
            win[0] = _viewportX + _viewportW * (v2[0] * rw + 1.0f) * 0.5f;
            win[1] = _viewportY + _viewportH * (v2[1] * rw + 1.0f) * 0.5f;
            win[2] = (v2[2] * rw + 1.0f) * 0.5f;
            return true;
        }

        //public DisplayMode getDisplayMode()
        //{
        //    return new DisplayMode(Width, Height, 24, 60);
        //}
    }

    //public class Extensions
    //{
    //    public void ColorPointer(int size, int stride, FloatBuffer colorArrayBuf) { glColorPointer(size, FLOAT, stride, colorArrayBuf); }
    //    public void ColorPointer(int size, bool unsigned, int stride, ByteBuffer colorAsByteBuffer) { glColorPointer(size, unsigned ? UNSIGNED_BYTE : BYTE, stride, colorAsByteBuffer); }
    //}
}
