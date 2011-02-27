#if !CODE_ANALYSIS
using System;
using System.IO;
using System.Collections;
namespace System.Interop.OpenGL
#else
using System;
using SystemEx.IO;
using System.Interop.OpenGL;
using System.Collections;
using System.Html;
namespace SystemEx.Interop.OpenGL
#endif
{
    public abstract class WebGL15Context
    {
        private static readonly int BEGIN_END_MAX_VERTICES = 16384;
        protected const uint ARRAY_POSITION = 0;
        protected const uint ARRAY_COLOR = 1;
        protected const uint ARRAY_TEXCOORD_0 = 2;
        protected const uint ARRAY_TEXCOORD_1 = 3;

        private uint _mode;
        //private int _staticBufferId = 0;
        private Stream _texCoordBuf = new MemoryStream(new byte[BEGIN_END_MAX_VERTICES * 2 * 4]);
        private Stream _vertexBuf = new MemoryStream(new byte[BEGIN_END_MAX_VERTICES * 3 * 4]);
        private Stream _st0011 = new MemoryStream(new byte[8 * 4]);
        private int _st0011BufferId = 0;
        //
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

        public WebGL15Context(int width, int height)
        {
            SE.WriteSingle(_st0011, 0f); SE.WriteSingle(_st0011, 0f);
            SE.WriteSingle(_st0011, 1f); SE.WriteSingle(_st0011, 0f);
            SE.WriteSingle(_st0011, 1f); SE.WriteSingle(_st0011, 1f);
            SE.WriteSingle(_st0011, 0f); SE.WriteSingle(_st0011, 1f);
            _st0011.Position = 0;
            //
            MathMatrix.SetIdentityM(_modelViewMatrix, 0);
            MathMatrix.SetIdentityM(_projectionMatrix, 0);
            MathMatrix.SetIdentityM(_textureMatrix, 0);
            _width = width;
            _height = height;
        }


        #region Internal

        protected bool UpdateMvpMatrix()
        {
            if (!_mvpDirty)
                return false;
            MathMatrix.MultiplyMM(_mvpMatrix, 0, _projectionMatrix, 0, _modelViewMatrix, 0);
            _mvpDirty = false;
            return true;
        }

        private bool Project(float objX, float objY, float objZ, int[] view, float[] win)
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

        private void VertexAttribStaticDrawPointer2(uint indx, int size, uint type, bool normalized, int stride, int offset, Stream pointer, int staticDrawIndex)
        {
            MemoryStream b = (MemoryStream)pointer;
            if ((type == GL15.BYTE) || (type == GL15.UNSIGNED_BYTE))
            {
                long p = b.Position;
                b.Position = p + offset;
                if (indx != ARRAY_COLOR)
                    throw new Exception("InvalidOperationException:");
                ColorPointer(size, type, stride, b);
                b.Position = p;
            }
            else if (type == GL15.FLOAT)
            {
                long p = b.Position;
                b.Position = p + offset / 4;
                switch (indx)
                {
                    case ARRAY_COLOR:
                        ColorPointer(size, type, stride, b);
                        break;
                    case ARRAY_POSITION:
                        VertexPointer(size, type, stride, b);
                        break;
                    case ARRAY_TEXCOORD_0:
                    case ARRAY_TEXCOORD_1:
                        TexCoordPointer(size, type, stride, b);
                        break;
                    default:
                        throw new Exception("ArgumentException: nioBuffer");
                }
                b.Position = p;
            }
        }

        #endregion

        #region Uknown

        public abstract void ClientActiveTexture(uint texture);

        #endregion

        // GL_VERSION_1_0
        // drawing-control commands
        public abstract void CullFace(uint mode);
        //public abstract void FrontFace(uint mode);
        //public abstract void Hint(uint target, uint mode);
        //public abstract void LineWidth(float width);
        public abstract void PointSize(float size);
        public abstract void PolygonMode(uint face, uint mode);
        public abstract void Scissor(int x, int y, int width, int height);
        public abstract void TexParameterf(uint target, uint pname, float param);
        public abstract void TexParameterfv(uint target, uint pname, Stream s);
        public abstract void TexParameteri(uint target, uint pname, int param);
        public abstract void TexParameteriv(uint target, uint pname, Stream s);
        //public abstract void TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, Stream pixels);
        public abstract void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, Stream pixels);
#if CODE_ANALYSIS
        public abstract void TexImage2Di(uint target, int level, uint internalformat, uint format, uint type, ImageElement image);
        public abstract void TexImage2De(uint target, int level, uint internalformat, uint format, uint type, CanvasElement canvas);
        public abstract void TexImage2Dex(uint target, int level, uint internalformat, uint format, uint type, CanvasElementEx canvas);
#endif
        // framebuf commands
        public abstract void DrawBuffer(uint mode);
        public abstract void Clear(uint mask);
        public abstract void ClearColor(float red, float green, float blue, float alpha);
        //public abstract void ClearStencil(int s);
        //public abstract void ClearDepth(double depth);
        //public abstract void StencilMask(uint mask);
        //public abstract void ColorMask(bool red, bool green, bool blue, bool alpha);
        public abstract void DepthMask(bool flag);
        // misc commands
        public abstract void Disable(uint cap);
        public abstract void Enable(uint cap);
        public abstract void Finish();
        //public abstract void Flush();
        // pixel-op commands
        public abstract void BlendFunc(uint sfactor, uint dfactor);
        //public abstract void LogicOp(uint opcode);
        //public abstract void StencilFunc(uint func, int @ref, uint mask);
        //public abstract void StencilOp(uint fail, uint zfail, uint zpass);
        public abstract void DepthFunc(uint func);
        // pixel-rw commands
        //public abstract void PixelStoref(uint pname, float param);
        public abstract void PixelStorei(uint pname, int param);
        //public abstract void ReadBuffer(uint mode);
        public abstract void ReadPixels(int x, int y, int width, int height, uint format, uint type, Stream pixels);
        // state-req commands
        //public abstract void GetBooleanv(uint pname, Stream s);
        //public abstract void GetDoublev(uint pname, Stream s);
        public abstract uint GetError();
        public virtual void GetFloatv(uint pname, Stream s)
        {
            switch (pname)
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
        public abstract string GetString(uint name);
        //public abstract void GetTexImage(uint target, int level, uint format, uint type, Stream pixels);
        //public abstract void GetTexParameterfv(uint target, uint pname, Stream @params);
        //public abstract void GetTexParameteriv(uint target, uint pname, Stream @params);
        //public abstract void GetTexLevelParameterfv(uint target, int level, uint pname, Stream @params);
        //public abstract void GetTexLevelParameteriv(uint target, int level, uint pname, Stream @params);
        //public abstract bool IsEnabled(uint cap);
        // xform commands
        public abstract void DepthRange(float near, float far); // (double near, double far);
        public virtual void Viewport(int x, int y, int width, int height)
        {
            _viewportX = x;
            _viewportY = y;
            _viewportW = width;
            _viewportH = height;
        }

        // GL_VERSION_1_0 [DEPRICATED]
        // display-list commands
        //WINGDIAPI void APIENTRY glNewList (GLuint list, GLenum mode);
        //WINGDIAPI void APIENTRY glEndList (void);
        //WINGDIAPI void APIENTRY glCallList (GLuint list);
        //WINGDIAPI void APIENTRY glCallLists (GLsizei n, GLenum type, const GLvoid *lists);
        //WINGDIAPI void APIENTRY glDeleteLists (GLuint list, GLsizei range);
        //WINGDIAPI GLuint APIENTRY glGenLists (GLsizei range);
        //WINGDIAPI void APIENTRY glListBase (GLuint base);
        // # drawing commands
        public void Begin(uint mode)
        {
            _mode = mode;
            _vertexBuf.SetLength(0);
            _texCoordBuf.SetLength(0);
        }
        //WINGDIAPI void APIENTRY glBitmap (GLsizei width, GLsizei height, GLfloat xorig, GLfloat yorig, GLfloat xmove, GLfloat ymove, const GLubyte *bitmap);
        //WINGDIAPI void APIENTRY glColor3b (GLbyte red, GLbyte green, GLbyte blue);
        //WINGDIAPI void APIENTRY glColor3bv (const GLbyte *v);
        //WINGDIAPI void APIENTRY glColor3d (GLdouble red, GLdouble green, GLdouble blue);
        //WINGDIAPI void APIENTRY glColor3dv (const GLdouble *v);
        public void Color3f(float red, float green, float blue) { Color4f(red, green, blue, 1.0f); }
        //WINGDIAPI void APIENTRY glColor3fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glColor3i (GLint red, GLint green, GLint blue);
        //WINGDIAPI void APIENTRY glColor3iv (const GLint *v);
        //WINGDIAPI void APIENTRY glColor3s (GLshort red, GLshort green, GLshort blue);
        //WINGDIAPI void APIENTRY glColor3sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glColor3ub (GLubyte red, GLubyte green, GLubyte blue);
        public void Color3ub(byte red, byte green, byte blue) { Color4ub(red, green, blue, (byte)0xFF); }
        //WINGDIAPI void APIENTRY glColor3ubv (const GLubyte *v);
        //WINGDIAPI void APIENTRY glColor3ui (GLuint red, GLuint green, GLuint blue);
        //WINGDIAPI void APIENTRY glColor3uiv (const GLuint *v);
        //WINGDIAPI void APIENTRY glColor3us (GLushort red, GLushort green, GLushort blue);
        //WINGDIAPI void APIENTRY glColor3usv (const GLushort *v);
        //WINGDIAPI void APIENTRY glColor4b (GLbyte red, GLbyte green, GLbyte blue, GLbyte alpha);
        //WINGDIAPI void APIENTRY glColor4bv (const GLbyte *v);
        //WINGDIAPI void APIENTRY glColor4d (GLdouble red, GLdouble green, GLdouble blue, GLdouble alpha);
        //WINGDIAPI void APIENTRY glColor4dv (const GLdouble *v);
        public abstract void Color4f(float red, float green, float blue, float alpha);
        //WINGDIAPI void APIENTRY glColor4fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glColor4i (GLint red, GLint green, GLint blue, GLint alpha);
        //WINGDIAPI void APIENTRY glColor4iv (const GLint *v);
        //WINGDIAPI void APIENTRY glColor4s (GLshort red, GLshort green, GLshort blue, GLshort alpha);
        //WINGDIAPI void APIENTRY glColor4sv (const GLshort *v);
        public void Color4ub(byte red, byte green, byte blue, byte alpha) { Color4f((red & 0xFF) / 255.0f, (green & 0xFF) / 255.0f, (blue & 0xFF) / 255f, (alpha & 0xFF) / 255.0f); }
        //WINGDIAPI void APIENTRY glColor4ubv (const GLubyte *v);
        //WINGDIAPI void APIENTRY glColor4ui (GLuint red, GLuint green, GLuint blue, GLuint alpha);
        //WINGDIAPI void APIENTRY glColor4uiv (const GLuint *v);
        //WINGDIAPI void APIENTRY glColor4us (GLushort red, GLushort green, GLushort blue, GLushort alpha);
        //WINGDIAPI void APIENTRY glColor4usv (const GLushort *v);
        //WINGDIAPI void APIENTRY glEdgeFlag (GLboolean flag);
        //WINGDIAPI void APIENTRY glEdgeFlagv (const GLboolean *flag);
        public void End()
        {
            int count = (int)_vertexBuf.Position / 3;
            _vertexBuf.Position = 0;
            // TODO: Save & restore state!!!
            EnableClientState(GLES11.VERTEX_ARRAY);
            VertexPointer(3, 0, 0, _vertexBuf);
            //
            if (_mode == GLXX.SIMPLE_TEXUTRED_QUAD)
            {
                EnableClientState(GLES11.TEXTURE_COORD_ARRAY);
                VertexAttribStaticDrawPointer2(ARRAY_TEXCOORD_0, 2, GL15.FLOAT, false, 0, 0, _st0011, _st0011BufferId);
                TexCoordPointer(2, GL15.FLOAT, 0, _st0011);
                DrawArrays(GL15.TRIANGLE_FAN, 0, 4);
            }
            else
            {
                if (_texCoordBuf.Position > 0)
                {
                    _texCoordBuf.Position = 0;
                    EnableClientState(GLES11.TEXTURE_COORD_ARRAY);
                    TexCoordPointer(2, GL15.FLOAT, 0, _texCoordBuf);
                }
                if (_mode == GLXX.QUADS)
                {
                    //Log.w("GLX", "glDrawQuads; count: " + count);
                    for (int index = 0; index < count; index += 4)
                        DrawArrays(GL15.TRIANGLE_FAN, index, 4);
                }
                else
                    DrawArrays(_mode, 0, count);
            }
            //DisableClientState(GLES11.TEXTURE_COORD_ARRAY);
            //DisableClientState(GLES11.VERTEX_ARRAY);
        }
        //WINGDIAPI void APIENTRY glIndexd (GLdouble c);
        //WINGDIAPI void APIENTRY glIndexdv (const GLdouble *c);
        //WINGDIAPI void APIENTRY glIndexf (GLfloat c);
        //WINGDIAPI void APIENTRY glIndexfv (const GLfloat *c);
        //WINGDIAPI void APIENTRY glIndexi (GLint c);
        //WINGDIAPI void APIENTRY glIndexiv (const GLint *c);
        //WINGDIAPI void APIENTRY glIndexs (GLshort c);
        //WINGDIAPI void APIENTRY glIndexsv (const GLshort *c);
        //WINGDIAPI void APIENTRY glNormal3b (GLbyte nx, GLbyte ny, GLbyte nz);
        //WINGDIAPI void APIENTRY glNormal3bv (const GLbyte *v);
        //WINGDIAPI void APIENTRY glNormal3d (GLdouble nx, GLdouble ny, GLdouble nz);
        //WINGDIAPI void APIENTRY glNormal3dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glNormal3f (GLfloat nx, GLfloat ny, GLfloat nz);
        //WINGDIAPI void APIENTRY glNormal3fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glNormal3i (GLint nx, GLint ny, GLint nz);
        //WINGDIAPI void APIENTRY glNormal3iv (const GLint *v);
        //WINGDIAPI void APIENTRY glNormal3s (GLshort nx, GLshort ny, GLshort nz);
        //WINGDIAPI void APIENTRY glNormal3sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glRasterPos2d (GLdouble x, GLdouble y);
        //WINGDIAPI void APIENTRY glRasterPos2dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glRasterPos2f (GLfloat x, GLfloat y);
        //WINGDIAPI void APIENTRY glRasterPos2fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glRasterPos2i (GLint x, GLint y);
        //WINGDIAPI void APIENTRY glRasterPos2iv (const GLint *v);
        //WINGDIAPI void APIENTRY glRasterPos2s (GLshort x, GLshort y);
        //WINGDIAPI void APIENTRY glRasterPos2sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glRasterPos3d (GLdouble x, GLdouble y, GLdouble z);
        //WINGDIAPI void APIENTRY glRasterPos3dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glRasterPos3f (GLfloat x, GLfloat y, GLfloat z);
        //WINGDIAPI void APIENTRY glRasterPos3fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glRasterPos3i (GLint x, GLint y, GLint z);
        //WINGDIAPI void APIENTRY glRasterPos3iv (const GLint *v);
        //WINGDIAPI void APIENTRY glRasterPos3s (GLshort x, GLshort y, GLshort z);
        //WINGDIAPI void APIENTRY glRasterPos3sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glRasterPos4d (GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //WINGDIAPI void APIENTRY glRasterPos4dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glRasterPos4f (GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        //WINGDIAPI void APIENTRY glRasterPos4fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glRasterPos4i (GLint x, GLint y, GLint z, GLint w);
        //WINGDIAPI void APIENTRY glRasterPos4iv (const GLint *v);
        //WINGDIAPI void APIENTRY glRasterPos4s (GLshort x, GLshort y, GLshort z, GLshort w);
        //WINGDIAPI void APIENTRY glRasterPos4sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glRectd (GLdouble x1, GLdouble y1, GLdouble x2, GLdouble y2);
        //WINGDIAPI void APIENTRY glRectdv (const GLdouble *v1, const GLdouble *v2);
        //WINGDIAPI void APIENTRY glRectf (GLfloat x1, GLfloat y1, GLfloat x2, GLfloat y2);
        //WINGDIAPI void APIENTRY glRectfv (const GLfloat *v1, const GLfloat *v2);
        //WINGDIAPI void APIENTRY glRecti (GLint x1, GLint y1, GLint x2, GLint y2);
        //WINGDIAPI void APIENTRY glRectiv (const GLint *v1, const GLint *v2);
        //WINGDIAPI void APIENTRY glRects (GLshort x1, GLshort y1, GLshort x2, GLshort y2);
        //WINGDIAPI void APIENTRY glRectsv (const GLshort *v1, const GLshort *v2);
        //WINGDIAPI void APIENTRY glTexCoord1d (GLdouble s);
        //WINGDIAPI void APIENTRY glTexCoord1dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glTexCoord1f (GLfloat s);
        //WINGDIAPI void APIENTRY glTexCoord1fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glTexCoord1i (GLint s);
        //WINGDIAPI void APIENTRY glTexCoord1iv (const GLint *v);
        //WINGDIAPI void APIENTRY glTexCoord1s (GLshort s);
        //WINGDIAPI void APIENTRY glTexCoord1sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glTexCoord2d (GLdouble s, GLdouble t);
        //WINGDIAPI void APIENTRY glTexCoord2dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glTexCoord2f (GLfloat s, GLfloat t);
        public void TexCoord2f(float s, float t)
        {
            SE.WriteSingle(_texCoordBuf, s);
            SE.WriteSingle(_texCoordBuf, t);
        }
        //WINGDIAPI void APIENTRY glTexCoord2fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glTexCoord2i (GLint s, GLint t);
        //WINGDIAPI void APIENTRY glTexCoord2iv (const GLint *v);
        //WINGDIAPI void APIENTRY glTexCoord2s (GLshort s, GLshort t);
        //WINGDIAPI void APIENTRY glTexCoord2sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glTexCoord3d (GLdouble s, GLdouble t, GLdouble r);
        //WINGDIAPI void APIENTRY glTexCoord3dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glTexCoord3f (GLfloat s, GLfloat t, GLfloat r);
        //WINGDIAPI void APIENTRY glTexCoord3fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glTexCoord3i (GLint s, GLint t, GLint r);
        //WINGDIAPI void APIENTRY glTexCoord3iv (const GLint *v);
        //WINGDIAPI void APIENTRY glTexCoord3s (GLshort s, GLshort t, GLshort r);
        //WINGDIAPI void APIENTRY glTexCoord3sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glTexCoord4d (GLdouble s, GLdouble t, GLdouble r, GLdouble q);
        //WINGDIAPI void APIENTRY glTexCoord4dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glTexCoord4f (GLfloat s, GLfloat t, GLfloat r, GLfloat q);
        //WINGDIAPI void APIENTRY glTexCoord4fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glTexCoord4i (GLint s, GLint t, GLint r, GLint q);
        //WINGDIAPI void APIENTRY glTexCoord4iv (const GLint *v);
        //WINGDIAPI void APIENTRY glTexCoord4s (GLshort s, GLshort t, GLshort r, GLshort q);
        //WINGDIAPI void APIENTRY glTexCoord4sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glVertex2d (GLdouble x, GLdouble y);
        //WINGDIAPI void APIENTRY glVertex2dv (const GLdouble *v);
        public void Vertex2f(int x, int y) { Vertex3f(x, y, 0); }
        //WINGDIAPI void APIENTRY glVertex2fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glVertex2i (GLint x, GLint y);
        //WINGDIAPI void APIENTRY glVertex2iv (const GLint *v);
        //WINGDIAPI void APIENTRY glVertex2s (GLshort x, GLshort y);
        //WINGDIAPI void APIENTRY glVertex2sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glVertex3d (GLdouble x, GLdouble y, GLdouble z);
        //WINGDIAPI void APIENTRY glVertex3dv (const GLdouble *v);
        public void Vertex3f(float x, float y, float z)
        {
            SE.WriteSingle(_vertexBuf, x);
            SE.WriteSingle(_vertexBuf, y);
            SE.WriteSingle(_vertexBuf, z);
        }
        //WINGDIAPI void APIENTRY glVertex3fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glVertex3i (GLint x, GLint y, GLint z);
        //WINGDIAPI void APIENTRY glVertex3iv (const GLint *v);
        //WINGDIAPI void APIENTRY glVertex3s (GLshort x, GLshort y, GLshort z);
        //WINGDIAPI void APIENTRY glVertex3sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glVertex4d (GLdouble x, GLdouble y, GLdouble z, GLdouble w);
        //WINGDIAPI void APIENTRY glVertex4dv (const GLdouble *v);
        //WINGDIAPI void APIENTRY glVertex4f (GLfloat x, GLfloat y, GLfloat z, GLfloat w);
        //WINGDIAPI void APIENTRY glVertex4fv (const GLfloat *v);
        //WINGDIAPI void APIENTRY glVertex4i (GLint x, GLint y, GLint z, GLint w);
        //WINGDIAPI void APIENTRY glVertex4iv (const GLint *v);
        //WINGDIAPI void APIENTRY glVertex4s (GLshort x, GLshort y, GLshort z, GLshort w);
        //WINGDIAPI void APIENTRY glVertex4sv (const GLshort *v);
        //WINGDIAPI void APIENTRY glClipPlane (GLenum plane, const GLdouble *equation);
        //WINGDIAPI void APIENTRY glColorMaterial (GLenum face, GLenum mode);
        //WINGDIAPI void APIENTRY glFogf (GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glFogfv (GLenum pname, const GLfloat *params);
        //WINGDIAPI void APIENTRY glFogi (GLenum pname, GLint param);
        //WINGDIAPI void APIENTRY glFogiv (GLenum pname, const GLint *params);
        //WINGDIAPI void APIENTRY glLightf (GLenum light, GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glLightfv (GLenum light, GLenum pname, const GLfloat *params);
        //WINGDIAPI void APIENTRY glLighti (GLenum light, GLenum pname, GLint param);
        //WINGDIAPI void APIENTRY glLightiv (GLenum light, GLenum pname, const GLint *params);
        //WINGDIAPI void APIENTRY glLightModelf (GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glLightModelfv (GLenum pname, const GLfloat *params);
        //WINGDIAPI void APIENTRY glLightModeli (GLenum pname, GLint param);
        //WINGDIAPI void APIENTRY glLightModeliv (GLenum pname, const GLint *params);
        //WINGDIAPI void APIENTRY glLineStipple (GLint factor, GLushort pattern);
        //WINGDIAPI void APIENTRY glMaterialf (GLenum face, GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glMaterialfv (GLenum face, GLenum pname, const GLfloat *params);
        //WINGDIAPI void APIENTRY glMateriali (GLenum face, GLenum pname, GLint param);
        //WINGDIAPI void APIENTRY glMaterialiv (GLenum face, GLenum pname, const GLint *params);
        //WINGDIAPI void APIENTRY glPolygonStipple (const GLubyte *mask);
        public abstract void ShadeModel(uint mode);
        //WINGDIAPI void APIENTRY glTexEnvf (GLenum target, GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glTexEnvfv (GLenum target, GLenum pname, const GLfloat *params);
        public abstract void TexEnvi(uint target, uint pname, int param);
        public abstract void TexEnviv(uint target, uint pname, Stream s);
        //WINGDIAPI void APIENTRY glTexGend (GLenum coord, GLenum pname, GLdouble param);
        //WINGDIAPI void APIENTRY glTexGendv (GLenum coord, GLenum pname, const GLdouble *params);
        //WINGDIAPI void APIENTRY glTexGenf (GLenum coord, GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glTexGenfv (GLenum coord, GLenum pname, const GLfloat *params);
        //WINGDIAPI void APIENTRY glTexGeni (GLenum coord, GLenum pname, GLint param);
        //WINGDIAPI void APIENTRY glTexGeniv (GLenum coord, GLenum pname, const GLint *params);
        // # feedback commands
        //WINGDIAPI void APIENTRY glFeedbackBuffer (GLsizei size, GLenum type, GLfloat *buffer);
        //WINGDIAPI void APIENTRY glSelectBuffer (GLsizei size, GLuint *buffer);
        //WINGDIAPI GLint APIENTRY glRenderMode (GLenum mode);
        //WINGDIAPI void APIENTRY glInitNames (void);
        //WINGDIAPI void APIENTRY glLoadName (GLuint name);
        //WINGDIAPI void APIENTRY glPassThrough (GLfloat token);
        //WINGDIAPI void APIENTRY glPopName (void);
        //WINGDIAPI void APIENTRY glPushName (GLuint name);
        //WINGDIAPI void APIENTRY glClearAccum (GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);
        //WINGDIAPI void APIENTRY glClearIndex (GLfloat c);
        //WINGDIAPI void APIENTRY glIndexMask (GLuint mask);
        //WINGDIAPI void APIENTRY glAccum (GLenum op, GLfloat value);
        //WINGDIAPI void APIENTRY glPopAttrib (void);
        //WINGDIAPI void APIENTRY glPushAttrib (GLbitfield mask);
        // # modeling commands
        //WINGDIAPI void APIENTRY glMap1d (GLenum target, GLdouble u1, GLdouble u2, GLint stride, GLint order, const GLdouble *points);
        //WINGDIAPI void APIENTRY glMap1f (GLenum target, GLfloat u1, GLfloat u2, GLint stride, GLint order, const GLfloat *points);
        //WINGDIAPI void APIENTRY glMap2d (GLenum target, GLdouble u1, GLdouble u2, GLint ustride, GLint uorder, GLdouble v1, GLdouble v2, GLint vstride, GLint vorder, const GLdouble *points);
        //WINGDIAPI void APIENTRY glMap2f (GLenum target, GLfloat u1, GLfloat u2, GLint ustride, GLint uorder, GLfloat v1, GLfloat v2, GLint vstride, GLint vorder, const GLfloat *points);
        //WINGDIAPI void APIENTRY glMapGrid1d (GLint un, GLdouble u1, GLdouble u2);
        //WINGDIAPI void APIENTRY glMapGrid1f (GLint un, GLfloat u1, GLfloat u2);
        //WINGDIAPI void APIENTRY glMapGrid2d (GLint un, GLdouble u1, GLdouble u2, GLint vn, GLdouble v1, GLdouble v2);
        //WINGDIAPI void APIENTRY glMapGrid2f (GLint un, GLfloat u1, GLfloat u2, GLint vn, GLfloat v1, GLfloat v2);
        //WINGDIAPI void APIENTRY glEvalCoord1d (GLdouble u);
        //WINGDIAPI void APIENTRY glEvalCoord1dv (const GLdouble *u);
        //WINGDIAPI void APIENTRY glEvalCoord1f (GLfloat u);
        //WINGDIAPI void APIENTRY glEvalCoord1fv (const GLfloat *u);
        //WINGDIAPI void APIENTRY glEvalCoord2d (GLdouble u, GLdouble v);
        //WINGDIAPI void APIENTRY glEvalCoord2dv (const GLdouble *u);
        //WINGDIAPI void APIENTRY glEvalCoord2f (GLfloat u, GLfloat v);
        //WINGDIAPI void APIENTRY glEvalCoord2fv (const GLfloat *u);
        //WINGDIAPI void APIENTRY glEvalMesh1 (GLenum mode, GLint i1, GLint i2);
        //WINGDIAPI void APIENTRY glEvalPoint1 (GLint i);
        //WINGDIAPI void APIENTRY glEvalMesh2 (GLenum mode, GLint i1, GLint i2, GLint j1, GLint j2);
        //WINGDIAPI void APIENTRY glEvalPoint2 (GLint i, GLint j);
        public void AlphaFunc(uint func, float @ref) { /* TODO: Remove this. Alpha text/func are unsupported in ES. */ }
        //WINGDIAPI void APIENTRY glPixelZoom (GLfloat xfactor, GLfloat yfactor);
        //WINGDIAPI void APIENTRY glPixelTransferf (GLenum pname, GLfloat param);
        //WINGDIAPI void APIENTRY glPixelTransferi (GLenum pname, GLint param);
        //WINGDIAPI void APIENTRY glPixelMapfv (GLenum map, GLsizei mapsize, const GLfloat *values);
        //WINGDIAPI void APIENTRY glPixelMapuiv (GLenum map, GLsizei mapsize, const GLuint *values);
        //WINGDIAPI void APIENTRY glPixelMapusv (GLenum map, GLsizei mapsize, const GLushort *values);
        //WINGDIAPI void APIENTRY glCopyPixels (GLint x, GLint y, GLsizei width, GLsizei height, GLenum type);
        //WINGDIAPI void APIENTRY glDrawPixels (GLsizei width, GLsizei height, GLenum format, GLenum type, const GLvoid *pixels);
        //WINGDIAPI void APIENTRY glGetClipPlane (GLenum plane, GLdouble *equation);
        //WINGDIAPI void APIENTRY glGetLightfv (GLenum light, GLenum pname, GLfloat *params);
        //WINGDIAPI void APIENTRY glGetLightiv (GLenum light, GLenum pname, GLint *params);
        //WINGDIAPI void APIENTRY glGetMapdv (GLenum target, GLenum query, GLdouble *v);
        //WINGDIAPI void APIENTRY glGetMapfv (GLenum target, GLenum query, GLfloat *v);
        //WINGDIAPI void APIENTRY glGetMapiv (GLenum target, GLenum query, GLint *v);
        //WINGDIAPI void APIENTRY glGetMaterialfv (GLenum face, GLenum pname, GLfloat *params);
        //WINGDIAPI void APIENTRY glGetMaterialiv (GLenum face, GLenum pname, GLint *params);
        //WINGDIAPI void APIENTRY glGetPixelMapfv (GLenum map, GLfloat *values);
        //WINGDIAPI void APIENTRY glGetPixelMapuiv (GLenum map, GLuint *values);
        //WINGDIAPI void APIENTRY glGetPixelMapusv (GLenum map, GLushort *values);
        //WINGDIAPI void APIENTRY glGetPolygonStipple (GLubyte *mask);
        //WINGDIAPI void APIENTRY glGetTexEnvfv (GLenum target, GLenum pname, GLfloat *params);
        //WINGDIAPI void APIENTRY glGetTexEnviv (GLenum target, GLenum pname, GLint *params);
        //WINGDIAPI void APIENTRY glGetTexGendv (GLenum coord, GLenum pname, GLdouble *params);
        //WINGDIAPI void APIENTRY glGetTexGenfv (GLenum coord, GLenum pname, GLfloat *params);
        //WINGDIAPI void APIENTRY glGetTexGeniv (GLenum coord, GLenum pname, GLint *params);
        //WINGDIAPI GLboolean APIENTRY glIsList (GLuint list);
        public void Frustum(float left, float right, float bottom, float top, float zNear, float zFar) // (double left, double right, double bottom, double top, double zNear, double zFar)
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
        public void LoadIdentity()
        {
            MathMatrix.SetIdentityM(_currentMatrix, 0);
            _mvpDirty = true;
        }
        public void LoadMatrixf(Stream s)
        {
            long p = s.Position;
            for (int index = 0; index < _currentMatrix.Length; index++)
                _currentMatrix[index] = SE.ReadSingle(s);
            s.Position = p;
            _mvpDirty = true;
        }
        //WINGDIAPI void APIENTRY glLoadMatrixd (const GLdouble *m);
        public void MatrixMode(uint mode)
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
                case GL15.TEXTURE:
                    _currentMatrix = _textureMatrix;
                    _currentMatrixStack = _textureMatrixStack;
                    break;
                default:
                    throw new Exception("ArgumentException: Unrecoginzed matrix mode: " + mode);
            }
            _matrixMode = mode;
        }
        public void MultMatrixf(float[] m)
        {
            int ofs = 0;
            MathMatrix.MultiplyMM(_tmpMatrix, 0, _currentMatrix, 0, m, ofs);
            JSArrayEx.Copy(_tmpMatrix, 0, _currentMatrix, 0, 16);
            _mvpDirty = true;
        }
        //WINGDIAPI void APIENTRY glMultMatrixd (const GLdouble *m);
        public void Ortho(float left, float right, float bottom, float top, float zNear, float zFar) // (double left, double right, double bottom, double top, double zNear, double zFar)
        {
            float[] matrix = {
		        2f/(right-left), 0,  0, 0,
		        //
		        0,  2f/(top-bottom), 0, 0,
		        //
		        0, 0, -2f/(zFar-zNear), 0,
		        //
		        -(right+left)/(right-left), -(top+bottom)/(top-bottom), -(zFar+zNear)/(zFar-zNear), 1f
            };
            MultMatrixf(matrix);
            _mvpDirty = true;
        }
        public void Orthox_(int left, int right, int bottom, int top, int zNear, int zFar)
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
		        0, 0, -2f/(f-n), 0,
		        -(r+l)/(r-l), -(t+b)/(t-b), -(f+n)/(f-n), 1f
            };
            MultMatrixf(matrix);
            _mvpDirty = true;
        }
        public void PopMatrix()
        {
            float[] top = (float[])_currentMatrixStack.Pop();
            JSArrayEx.Copy(top, 0, _currentMatrix, 0, 16);
            _mvpDirty = true;
        }
        public void PushMatrix()
        {
            float[] copy = new float[16];
            JSArrayEx.Copy(_currentMatrix, 0, copy, 0, 16);
            _currentMatrixStack.Push(copy);
        }
        //WINGDIAPI void APIENTRY glRotated (GLdouble angle, GLdouble x, GLdouble y, GLdouble z);
        public void Rotatef(float angle, float x, float y, float z)
        {
            if ((x != 0) || (y != 0) || (z != 0))
                // right thing to do? or rotate around a default axis?
                MathMatrix.RotateM2(_currentMatrix, 0, angle, x, y, z);
            _mvpDirty = true;
        }
        //WINGDIAPI void APIENTRY glScaled (GLdouble x, GLdouble y, GLdouble z);
        public void Scalef(float x, float y, float z)
        {
            MathMatrix.ScaleM2(_currentMatrix, 0, x, y, z);
            _mvpDirty = true;
        }
        public void Scalex_(int x, int y, int z)
        {
            MathMatrix.ScaleM2(_currentMatrix, 0, (float)x, (float)y, (float)z);
            _mvpDirty = true;
        }

        //WINGDIAPI void APIENTRY glTranslated (GLdouble x, GLdouble y, GLdouble z);
        public void Translatef(float x, float y, float z)
        {
            MathMatrix.TranslateM2(_currentMatrix, 0, x, y, z);
            _mvpDirty = true;
        }
        public void Translatex_(int x, int y, int z)
        {
            MathMatrix.TranslateM2(_currentMatrix, 0, (float)x, (float)y, (float)z);
            _mvpDirty = true;
        }


        // GL_VERSION_1_1
        public abstract void DrawArrays(uint mode, int first, int count);
        public abstract void DrawElements(uint mode, int count, uint type, Stream indices);
        //public abstract void GetPointerv(uint pname, Stream @params);
        //public abstract void PolygonOffset(float factor, float units);
        //public abstract void CopyTexImage1D(uint target, int level, uint internalformat, int x, int y, int width, int border);
        //public abstract void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border);
        //public abstract void CopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);
        //public abstract void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);
        //public abstract void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, Stream pixels);
        public abstract void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, Stream pixels);
        public abstract void BindTexture(uint target, uint texture);
        public abstract void DeleteTextures(int n, Stream textures);
        //public abstract void GenTextures(int n, Stream textures);
        //public abstract bool IsTexture(uint texture);

        // GL_VERSION_1_1 [DEPRICATED]
        //WINGDIAPI void APIENTRY glArrayElement (GLint i);
        public abstract void ColorPointer(int size, uint type, int stride, Stream pointer);
        public abstract void DisableClientState(uint array);
        //WINGDIAPI void APIENTRY glEdgeFlagPointer (GLsizei stride, const GLvoid *pointer);
        public abstract void EnableClientState(uint array);
        //WINGDIAPI void APIENTRY glIndexPointer (GLenum type, GLsizei stride, const GLvoid *pointer);
        //WINGDIAPI void APIENTRY glInterleavedArrays (GLenum format, GLsizei stride, const GLvoid *pointer);
        //WINGDIAPI void APIENTRY glNormalPointer (GLenum type, GLsizei stride, const GLvoid *pointer);
        public abstract void TexCoordPointer(int size, uint type, int stride, Stream pointer);
        public abstract void VertexPointer(int size, uint type, int stride, Stream pointer);
        //WINGDIAPI GLboolean APIENTRY glAreTexturesResident (GLsizei n, const GLuint *textures, GLboolean *residences);
        //WINGDIAPI void APIENTRY glPrioritizeTextures (GLsizei n, const GLuint *textures, const GLclampf *priorities);
        //WINGDIAPI void APIENTRY glIndexub (GLubyte c);
        //WINGDIAPI void APIENTRY glIndexubv (const GLubyte *c);
        //WINGDIAPI void APIENTRY glPopClientAttrib (void);
        //WINGDIAPI void APIENTRY glPushClientAttrib (GLbitfield mask);


        // GL_VERSION_1_2
        //public abstract void BlendColor(float red, float green, float blue, float alpha);
        //public abstract void BlendEquation(uint mode);
        //public abstract void DrawRangeElements(uint mode, uint start, uint end, int count, uint type, Stream indices);
        // # OpenGL 1.2 (EXT_texture3D) commands
        //public abstract void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, Stream pixels);
        //public abstract void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, Stream pixels);
        // # OpenGL 1.2 (EXT_copy_texture) commands (specific to texture3D)
        //public abstract void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);


        // GL_VERSION_1_3
        public abstract void ActiveTexture(uint texture);
        //public abstract void SampleCoverage(float value, bool invert);
        //public abstract void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, Stream data);
        //public abstract void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, Stream data);
        //public abstract void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, Stream data);
        //public abstract void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, Stream data);
        //public abstract void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, Stream data);
        //public abstract void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, Stream data);
        //public abstract void GetCompressedTexImage(uint target, int level, Stream img);


        // GL_VERSION_1_4
        //public abstract void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);
        //public abstract void MultiDrawArrays(uint mode, int[] first, int[] count, int primcount);
        //public abstract void MultiDrawElements(uint mode, int[] count, uint type, Stream indices, int primcount);
        public abstract void PointParameterf(uint pname, float param);
        public abstract void PointParameterfv(uint pname, Stream s);
        //public abstract void PointParameteri(uint pname, int param);
        //public abstract void PointParameteriv(uint pname, Stream @params);

        // GL_VERSION_1_5
        //public abstract void GenQueries(int n, Stream ids);
        //public abstract void DeleteQueries(int n, Stream ids);
        //public abstract bool IsQuery(uint id);
        //public abstract void BeginQuery(uint target, uint id);
        //public abstract void EndQuery(uint target);
        //public abstract void GetQueryiv(uint target, uint pname, Stream @params);
        //public abstract void GetQueryObjectiv(uint id, uint pname, Stream @params);
        //public abstract void GetQueryObjectuiv(uint id, uint pname, Stream @params);
        //public abstract void BindBuffer(uint target, uint buffer);
        //public abstract void DeleteBuffers(int n, Stream buffers);
        //public abstract void GenBuffers(int n, Stream buffers);
        //public abstract bool IsBuffer(uint buffer);
        //public abstract void BufferData(uint target, long size, Stream data, uint usage);
        //public abstract void BufferSubData(uint target, long offset, long size, Stream data);
        //public abstract void GetBufferSubData(uint target, long offset, long size, Stream data);
        //public abstract Stream MapBuffer(uint target, uint access);
        //public abstract bool UnmapBuffer(uint target);
        //public abstract void GetBufferParameteriv(uint target, uint pname, Stream @params);
        //public abstract void GetBufferPointerv(uint target, uint pname, Stream @params);


        //        public void SetDisplayMode(DisplayMode displayMode)
        //        {
        //#if CODE_ANALYSIS
        //            _canvas.setWidth(displayMode.width);
        //            _canvas.setHeight(displayMode.height);
        //#endif
        //        }

        //        public DisplayMode[] GetAvailableDisplayModes()
        //        {
        //#if CODE_ANALYSIS
        //            return new DisplayMode[] { getDisplayMode(), new DisplayMode(Window.getClientWidth(), Window.getClientHeight(), 32, 60) };
        //#else
        //            return null;
        //#endif
    }
}
