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
namespace SystemEx.Interop.OpenGL
#endif
{
    public partial class WebGLES11RenderingContext : WebGLRenderingContext
    {
        //public const uint GLES11_ARRAY_TEXCOORD_1 = 3;
        //public const uint GLES11_ARRAY_TEXCOORD_0 = 2;
        //public const uint GLES11_ARRAY_COLOR = 1;
        //public const uint GLES11_ARRAY_POSITION = 0;

        //
        //public const uint GLES11_MODELVIEW = 0x1700;
        //public const uint GLES11_PROJECTION = 0x1701;
        public const uint _MATRIX_MODE = 0x0BA0;
        /* WebGL-transition enums */
        public const uint _QUADS = 0x0007;
        public const uint _POLYGON = TRIANGLE_FAN;
        public const uint _MODELVIEW_MATRIX = 2982;
        public const uint _SIMPLE_TEXUTRED_QUAD = 0xFFFFFFFF;

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
        private float[] _mvpMatrix = new float[16];
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

        public void glLoadIdentity()
        {
            MathMatrix.SetIdentityM(_currentMatrix, 0);
            _mvpDirty = true;
        }

        public void glMatrixMode(uint mode)
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

        public void glGetInteger(uint what, Stream s)
        {
            switch (what)
            {
                case _MATRIX_MODE:
                    SE.WriteUInt32(s, _matrixMode);
                    break;
                default:
                    throw new Exception("ArgumentException:");
            }
        }

        public void glMultMatrixf(float[] matrix, int ofs)
        {
            MathMatrix.MultiplyMM(_tmpMatrix, 0, _currentMatrix, 0, matrix, ofs);
            JSArrayEx.Copy(_tmpMatrix, 0, _currentMatrix, 0, 16);
            _mvpDirty = true;
        }

        public void glPushMatrix()
        {
            float[] copy = new float[16];
            JSArrayEx.Copy(_currentMatrix, 0, copy, 0, 16);
            _currentMatrixStack.Push(copy);
        }

        public void glPopMatrix()
        {
            float[] top = (float[])_currentMatrixStack.Pop();
            JSArrayEx.Copy(top, 0, _currentMatrix, 0, 16);
            _mvpDirty = true;
        }

        public void glRotatef(float angle, float x, float y, float z)
        {
            if ((x != 0) || (y != 0) || (z != 0))
                // right thing to do? or rotate around a default axis?
                MathMatrix.RotateM2(_currentMatrix, 0, angle, x, y, z);
            _mvpDirty = true;
        }

        public void glScalef(float x, float y, float z)
        {
            MathMatrix.ScaleM2(_currentMatrix, 0, x, y, z);
            _mvpDirty = true;
        }

        public void glTranslatef(float tx, float ty, float tz)
        {
            MathMatrix.TranslateM2(_currentMatrix, 0, tx, ty, tz);
            _mvpDirty = true;
        }

        public void glViewport(int x, int y, int w, int h)
        {
            _viewportX = x;
            _viewportY = y;
            _viewportW = w;
            _viewportH = h;
        }

        public void glFrustum(double left, double right, double bottom, double top, double znear, double zfar)
        {
            float[] matrix = new float[16];
            double temp, temp2, temp3, temp4;
            temp = 2 * znear;
            temp2 = right - left;
            temp3 = top - bottom;
            temp4 = zfar - znear;
            matrix[0] = (float)(temp / temp2);
            matrix[1] = 0;
            matrix[2] = 0;
            matrix[3] = 0;
            matrix[4] = 0;
            matrix[5] = (float)(temp / temp3);
            matrix[6] = 0;
            matrix[7] = 0;
            matrix[8] = (float)((right + left) / temp2);
            matrix[9] = (float)((top + bottom) / temp3);
            matrix[10] = (float)((-zfar - znear) / temp4);
            matrix[11] = -1;
            matrix[12] = 0;
            matrix[13] = 0;
            matrix[14] = (float)((-temp * zfar) / temp4);
            matrix[15] = 0;
            glMultMatrixf(matrix, 0);
        }

        private bool UpdateMvpMatrix()
        {
            if (!_mvpDirty)
                return false;
            MathMatrix.MultiplyMM(_mvpMatrix, 0, _projectionMatrix, 0, _modelViewMatrix, 0);
            _mvpDirty = false;
            return true;
        }

        public void glOrtho(int left, int right, int bottom, int top, int near, int far)
        {
            float l = left;
            float r = right;
            float b = bottom;
            float n = near;
            float f = far;
            float t = top;
            float[] matrix = {
		        2f/(r-l), 0,  0, 0,
		        //
		        0,  2f/(t-b), 0, 0,
		        //
		        0, 0, -2f/f-n, 0,
		        //
		        -(r+l)/(r-l), -(t+b)/(t-b), -(f+n)/(f-n), 1f
            };
            glMultMatrixf(matrix, 0);
            _mvpDirty = true;
        }

        public bool glProject(float objX, float objY, float objZ, int[] view, float[] win)
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

        public void glGetFloat(uint name, Stream s)
        {
            switch (name)
            {
                case GLES11.MODELVIEW:
                case _MODELVIEW_MATRIX:
                    long p = s.Position;
                    for (int index = 0; index < _modelViewMatrix.Length; index++)
                        SE.WriteSingle(s, _modelViewMatrix[index]);
                    s.Position = p;
                    break;
                default:
                    throw new Exception("ArgumentException: glGetFloat");
            }
        }

        public void glLoadMatrix(Stream s)
        {
            long p = s.Position;
            for (int index = 0; index < _currentMatrix.Length; index++)
                _currentMatrix[index] = SE.ReadSingle(s);
            s.Position = p;
            _mvpDirty = true;
        }


        //public DisplayMode getDisplayMode()
        //{
        //    return new DisplayMode(Width, Height, 24, 60);
        //}
    }
}
