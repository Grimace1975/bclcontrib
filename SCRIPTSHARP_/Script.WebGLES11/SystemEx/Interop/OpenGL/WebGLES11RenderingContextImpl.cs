//[Khronos]http://www.khronos.org/opengles/1_X/
#if !CODE_ANALYSIS
using System;
using System.IO;
namespace System.Interop.OpenGL
#else
using System.Html;
using System;
using System.Specialized;
using System.Interop.OpenGL;
using SystemEx.IO;
using System.TypedArrays;
namespace SystemEx.Interop.OpenGL
#endif
{
    public partial class WebGLES11RenderingContextImpl : WebGLES11RenderingContext
    {
        #region Logging
        private int _logCount = 0;
        private void Log(string msg)
        {
            if (_logCount >= 1000)
                return;
#if CODE_ANALYSIS
            MozConsole.Log(_logCount++ + ": " + msg);
#endif
        }
        private void CheckError(string s)
        {
            uint err = gl.GetError();
            if (err != NO_ERROR)
                Log("GL_ERROR in " + s + "(): " + err);
        }
        //public string FloatArrayToString(Float32Array fa)
        //{
        //    StringBuilder b = new StringBuilder();
        //    b.Append("len: " + fa.Length);
        //    b.Append("data: ");
        //    for (int i = 0; i < Math.Min(fa.Length, 10); i++)
        //        b.Append(fa[i] + ",");
        //    return b.ToString();
        //}

        //public string Int32ArrayToString(Int32Array fa)
        //{
        //    StringBuilder b = new StringBuilder();
        //    b.Append("len: " + fa.Length);
        //    b.Append("data: ");
        //    for (int i = 0; i < Math.Min(fa.Length, 10); i++)
        //        b.Append(fa[i] + ",");
        //    return b.ToString();
        //}

        //public string Uint16ArrayToString(Uint16Array fa)
        //{
        //    StringBuilder b = new StringBuilder();
        //    b.Append("len: " + fa.Length);
        //    b.Append("data: ");
        //    for (int i = 0; i < Math.Min(fa.Length, 10); i++)
        //        b.Append(fa[i] + ",");
        //    return b.ToString();
        //}
        #endregion
        private const int SMALL_BUF_COUNT = 4;
        private WebGLUniformLocation _uMvpMatrix;
        private WebGLUniformLocation _uSampler0;
        private WebGLUniformLocation _uSampler1;
        private WebGLUniformLocation _uTexEnv0;
        private WebGLUniformLocation _uTexEnv1;
        private WebGLUniformLocation _uEnableTexture0;
        private WebGLUniformLocation _uEnableTexture1;
        private WebGLBufferData[] _buffers = new WebGLBufferData[SMALL_BUF_COUNT];
        public WebGLRenderingContext gl;    
        private uint _clientActiveTexture = 0;
        private uint _activeTexture = 0;
        private uint[] _boundTextureId = new uint[2];
        private int[] _texEnvMode = new int[2];
        private WebGLBuffer _elementBuffer;
        //
        private WebGLBuffer[] _staticDrawBuffers = new WebGLBuffer[0];
        private WebGLTexture[] _textures = new WebGLTexture[0];
        private int[] _textureFormats = new int[0];
        private uint[] _textureFormat = new uint[0];

#if CODE_ANALYSIS
        private CanvasElementEx _canvas;
        public WebGLES11RenderingContextImpl(CanvasElementEx canvas)
            : base(canvas.Width, canvas.Height)
        {
            _canvas = canvas;
            gl = canvas.GetContextWebGL();
#else
        public WebGLES11RenderingContextImpl()
            : base(100, 100)
        {
            gl = null;
#endif
            if (gl == null)
                throw new Exception("UnsupportedOperationException: WebGL N/A");
            InitShaders(); CheckError("initShader");
            _elementBuffer = gl.CreateBuffer(); CheckError("createBuffer f. elements");
            for (int index = 0; index < _buffers.Length; index++)
            {
                WebGLBufferData b = new WebGLBufferData();
                b.ToBind = null;
                b.Buffer = gl.CreateBuffer(); CheckError("createBuffer" + index);
                b.Stride = 0;
                b.Size = 0;
                b.Type = 0;
                b.ByteSize = 0;
                b.Normalized = false;
                _buffers[index] = b;
            }
        }

        private void InitShaders()
        {
            // create our shaders
            WebGLShader vertexShader = LoadShader(VERTEX_SHADER, @"
        attribute vec4 a_position;
        attribute vec4 a_color;
        attribute vec2 a_texCoord0;
        attribute vec2 a_texCoord1;
        uniform mat4 u_mvpMatrix;
        varying vec4 v_color;
        varying vec2 v_texCoord0;
        varying vec2 v_texCoord1;
        void main()
        {
            gl_Position = u_mvpMatrix * a_position;
            v_color = a_color;
            v_texCoord0 = a_texCoord0;
            v_texCoord1 = a_texCoord1;
        }");
            WebGLShader fragmentShader = LoadShader(FRAGMENT_SHADER, @"
        #ifdef GL_ES
        precision mediump float;
        #endif
        uniform sampler2D s_texture0;
        uniform sampler2D s_texture1;
        uniform int s_texEnv0;
        uniform int s_texEnv1;
        uniform int u_enable_texture_0;
        uniform int u_enable_texture_1;
        varying vec4 v_color;
        varying vec2 v_texCoord0;
        varying vec2 v_texCoord1;
        vec4 finalColor;
        void main()
        {
            finalColor = v_color;
            if (u_enable_texture_0 == 1)
            {
                vec4 texel = texture2D(s_texture0, v_texCoord0);
                if (s_texEnv0 == 1) {
                    finalColor = finalColor * texel;
                } else if (s_texEnv0 == 2) {
                    finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);
                } else {
                    finalColor = texel;
                }
            }
            if (u_enable_texture_1 == 1) {
                vec4 texel = texture2D(s_texture1, v_texCoord1);
                if (s_texEnv1 == 1) {
                    finalColor = finalColor * texel;
                } else if (s_texEnv1 == 2) {
                    finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);
                } else {
                    finalColor = texel;
                }
            }
            // simple alpha check
            if (finalColor.a == 0.0) {
                discard;
            }
            float gamma = 1.5;
            float igamma = 1.0 / gamma;
            gl_FragColor = vec4(pow(finalColor.r, igamma), pow(finalColor.g, igamma), pow(finalColor.b, igamma), finalColor.a);
        }");
            if ((vertexShader == null) || (fragmentShader == null))
                throw new Exception("RuntimeException: shader error");
            // Create the program object
            WebGLProgram programObject = gl.CreateProgram();
            if ((programObject == null) || (gl.GetError() != NO_ERROR))
                throw new Exception("RuntimeException: program error");
            // Attach our two shaders to the program
            gl.AttachShader(programObject, vertexShader);
            gl.AttachShader(programObject, fragmentShader);
            // Bind "vPosition" to attribute 0
            gl.BindAttribLocation(programObject, ARRAY_POSITION, "a_position");
            gl.BindAttribLocation(programObject, ARRAY_COLOR, "a_color");
            gl.BindAttribLocation(programObject, ARRAY_TEXCOORD_0, "a_texCoord0");
            gl.BindAttribLocation(programObject, ARRAY_TEXCOORD_1, "a_texCoord1");
            // Link the program
            gl.LinkProgram(programObject);
            // TODO(haustein) get position, color from the linker, too
            _uMvpMatrix = gl.GetUniformLocation(programObject, "u_mvpMatrix");
            _uSampler0 = gl.GetUniformLocation(programObject, "s_texture0");
            _uSampler1 = gl.GetUniformLocation(programObject, "s_texture1");
            _uTexEnv0 = gl.GetUniformLocation(programObject, "s_texEnv0");
            _uTexEnv1 = gl.GetUniformLocation(programObject, "s_texEnv1");
            _uEnableTexture0 = gl.GetUniformLocation(programObject, "u_enable_texture_0");
            _uEnableTexture1 = gl.GetUniformLocation(programObject, "u_enable_texture_1");
            // Check the link status
            bool linked = (bool)gl.GetProgramParameter(programObject, LINK_STATUS);
            if (!linked)
                throw new Exception("RuntimeException: linker Error: " + gl.GetProgramInfoLog(programObject));
            gl.UseProgram(programObject);
            gl.Uniform1i(_uSampler0, 0);
            gl.Uniform1i(_uSampler1, 1);
            gl.ActiveTexture(TEXTURE0);
        }

        private WebGLShader LoadShader(uint shaderType, string shaderSource)
        {
            // Create the shader object
            WebGLShader shader = gl.CreateShader(shaderType);
            if (shader == null)
                throw new Exception("RuntimeException:");
            // Load the shader source
            gl.ShaderSource(shader, shaderSource);
            // Compile the shader
            gl.CompileShader(shader);
            // Check the compile status
            bool compiled = (bool)gl.GetShaderParameter(shader, COMPILE_STATUS);
            if (!compiled)
                // Something went wrong during compilation; get the error
                throw new Exception("RuntimeException: Shader compile error: " + gl.GetShaderInfoLog(shader));
            return shader;
        }

        #region Implementation GLES11

        /* Available only in Common profile */
        public override void ClearColor(float red, float green, float blue, float alpha) { gl.ClearColor(red, green, blue, alpha); CheckError("glClearColor"); }
        public override void Color4f(float red, float green, float blue, float alpha) { gl.VertexAttrib4f(ARRAY_COLOR, red, green, blue, alpha); CheckError("glColor4f"); }
        public override void DepthRangef(float zNear, float zFar) { gl.DepthRange(zNear, zFar); CheckError("glDepthRangef"); }
        //?public override void DrawBuffer(int buf) { /*TODO*/ }
        public override void PointParameterf(uint pname, float param) { }
        public override void PointParameterfv(uint pname, Stream @params) { }
        public override void PointSize(float size) { }
        public override void TexParameterf(uint target, uint pname, float param) { gl.TexParameterf(target, pname, param); CheckError("glTexParameterf"); }
        public override void TexParameterfv(uint target, uint pname, Stream @params) { }

        /* Available in both Common and Common-Lite profiles */
        public override void ActiveTexture(uint texture) { gl.ActiveTexture(texture); CheckError("glActiveTexture"); _activeTexture = texture - TEXTURE0; }
        public override void BindTexture(uint target, uint texture)
        {
            WebGLTexture t = _textures[texture];
            if (t == null)
            {
                t = gl.CreateTexture();
                _textures[texture] = t;
            }
            //Log("binding texture " + t + " id " + texture + " for activeTexture: " + (_activeTexture - TEXTURE0));
            gl.BindTexture(target, t); CheckError("glBindTexture");
            _boundTextureId[_activeTexture] = texture;
            //glColor3f((float)Math.Random(), (float)Math.Random(), (float)Math.Random());
        }
        public override void BlendFunc(uint sfactor, uint dfactor) { gl.BlendFunc(sfactor, dfactor); CheckError("glBlendFunc"); }
        public override void Clear(uint mask) { gl.Clear(mask); CheckError("glClear"); }
        public override void ClientActiveTexture(uint texture) { _clientActiveTexture = texture - TEXTURE0; }
        public override void ColorPointer(int size, uint type, int stride, Stream pointer) { VertexAttribPointer(ARRAY_COLOR, size, type, true, stride, pointer); CheckError("glColorPointer"); }
        public override void CullFace(uint mode) { gl.CullFace(mode); CheckError("glCullFace"); }
        public override void DeleteTextures(int n, Stream textures)
        {
            for (int index = 0; index < n; index++)
            {
                int textureIndex = SE.ReadInt32(textures);
                gl.DeleteTexture(_textures[textureIndex]); CheckError("glDeleteTexture");
                _textures[textureIndex] = null;
            }
        }
        public override void DepthFunc(uint func) { gl.DepthFunc(func); CheckError("glDepthFunc"); }
        public override void DepthMask(bool flag) { gl.DepthMask(flag); CheckError("glDepthMask"); }
        public override void DepthRangex(int zNear, int zFar) { gl.DepthRange(zNear, zFar); CheckError("glDepthRangex"); }
        public override void Disable(uint cap)
        {
            // In ES, you don't enable/disable TEXTURE_2D. We use it this call to disable one of the two active textures supported by the shader.
            if (cap != TEXTURE_2D)
            {
                gl.Disable(cap); CheckError("glDisable");
                return;
            }
            switch (_activeTexture)
            {
                case 0:
                    gl.Uniform1i(_uEnableTexture0, 0);
                    break;
                case 1:
                    gl.Uniform1i(_uEnableTexture1, 0);
                    break;
                default:
                    throw new Exception("RuntimeException:");
            }
        }
        public override void DisableClientState(uint array)
        {
            switch (array)
            {
                case GLES11.COLOR_ARRAY:
                    gl.DisableVertexAttribArray(ARRAY_COLOR); CheckError("DisableClientState colorArr");
                    break;
                case GLES11.VERTEX_ARRAY:
                    gl.DisableVertexAttribArray(ARRAY_POSITION); CheckError("DisableClientState vertexArrr");
                    break;
                case GLES11.TEXTURE_COORD_ARRAY:
                    switch (_clientActiveTexture)
                    {
                        case 0:
                            gl.DisableVertexAttribArray(ARRAY_TEXCOORD_0); CheckError("DisableClientState texCoord0");
                            break;
                        case 1:
                            gl.DisableVertexAttribArray(ARRAY_TEXCOORD_1); CheckError("DisableClientState texCoord1");
                            break;
                        default:
                            throw new Exception("RuntimeException:");
                    }
                    break;
                default:
                    Log("unsupported / unrecogized client state");
                    break;
            }
        }
        public override void DrawArrays(uint mode, int first, int count) { /*Log("drawArrays mode:" + mode + " first:" + first + " count:" +count);*/ PrepareDraw(); gl.DrawArrays(mode, first, count); CheckError("drawArrays"); }
        public override void DrawElements(uint mode, int count, uint type, Stream indicies)
        {
            PrepareDraw();
            gl.BindBuffer(ELEMENT_ARRAY_BUFFER, _elementBuffer); CheckError("bindBuffer(el)");
            gl.BufferData(ELEMENT_ARRAY_BUFFER, GetTypedArray(indicies, UNSIGNED_SHORT), DYNAMIC_DRAW); CheckError("bufferData(el)");
            //count = indicies.Remaining();
            gl.DrawElements(mode, count, UNSIGNED_SHORT, 0); CheckError("drawElements");
        }
        public override void Enable(uint cap)
        {
            if (cap != TEXTURE_2D)
            {
                gl.Enable(cap); CheckError("glEnable");
                return;
            }
            // In ES, you don't enable/disable TEXTURE_2D. We use it this call to enable one of the two active textures supported by the shader.
            switch (_activeTexture)
            {
                case 0:
                    gl.Uniform1i(_uEnableTexture0, 1);
                    break;
                case 1:
                    gl.Uniform1i(_uEnableTexture1, 1);
                    break;
                default:
                    throw new Exception("RuntimeException:");
            }
        }
        public override void EnableClientState(uint array)
        {
            switch (array)
            {
                case GLES11.COLOR_ARRAY:
                    gl.EnableVertexAttribArray(ARRAY_COLOR); CheckError("enableClientState colorArr");
                    break;
                case GLES11.VERTEX_ARRAY:
                    gl.EnableVertexAttribArray(ARRAY_POSITION); CheckError("enableClientState vertexArrr");
                    break;
                case GLES11.TEXTURE_COORD_ARRAY:
                    switch (_clientActiveTexture)
                    {
                        case 0:
                            gl.EnableVertexAttribArray(ARRAY_TEXCOORD_0); CheckError("enableClientState texCoord0");
                            break;
                        case 1:
                            gl.EnableVertexAttribArray(ARRAY_TEXCOORD_1); CheckError("enableClientState texCoord1");
                            break;
                        default:
                            throw new Exception("RuntimeException:");
                    }
                    break;
                default:
                    Log("unsupported / unrecogized client state " + array);
                    break;
            }
        }
        public override void Finish() { gl.Finish(); }
        public override uint GetError() { return gl.GetError(); }
        public override string GetString(uint name) { return "glGetString not implemented"; }
        public override void PixelStorei(uint pname, int param) { gl.PixelStorei(pname, param); }
        public override void PointParameterx(uint pname, int param) { }
        public override void PointParameterxv(uint pname, Stream @params) { }
        public override void PointSizex(int size) { }
        public override void ReadPixels(int x, int y, int width, int height, uint format, uint type, Stream pixels) { }
        public override void Scissor(int x, int y, int width, int height) { gl.Scissor(x, y, width, height); CheckError("glScissor"); }
        public override void ShadeModel(uint mode) { }
        public override void TexCoordPointer(int size, uint type, int stride, Stream pointer) { VertexAttribPointer(ARRAY_TEXCOORD_0 + _clientActiveTexture, size, type, false, stride, pointer); CheckError("texCoordPointer"); }
        public override void TexEnvi(uint target, uint pname, int param) { _texEnvMode[_activeTexture] = param; }
        public override void TexEnvx(uint target, uint pname, int param) { _texEnvMode[_activeTexture] = param; }
        public override void TexEnviv(uint target, uint pname, Stream @params) { }
        public override void TexEnvxv(uint target, uint pname, Stream @params) { }
        public override void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, Stream pixels) { _textureFormat[_boundTextureId[_activeTexture]] = internalformat; gl.TexImage2D(target, level, internalformat, width, height, border, format, type, GetTypedArray(pixels, type)); CheckError("glTexImage2D"); }
#if CODE_ANALYSIS
        public override void TexImage2Di(uint target, int level, uint internalformat, uint format, uint type, ImageElement image) { /*Log("setting texImage2d; image: " + image.Src);*/ gl.TexImage2D(target, level, internalformat, format, type, image); CheckError("texImage2D"); }
        public override void TexImage2De(uint target, int level, uint internalformat, uint format, uint type, CanvasElement canvas) { gl.TexImage2D(target, level, internalformat, format, type, canvas); CheckError("texImage2D"); }
        public override void TexImage2Dx(uint target, int level, uint internalformat, uint format, uint type, CanvasElementEx canvas) { gl.TexImage2D(target, level, internalformat, format, type, canvas); CheckError("texImage2D"); }
#endif
        public override void TexParameteri(uint target, uint pname, int param) { gl.TexParameteri(target, pname, param); CheckError("glTexParameteri"); }
        public override void TexParameterx(uint target, uint pname, int param) { gl.TexParameteri(target, pname, param); CheckError("glTexParameteri"); }
        public override void TexParameteriv(uint target, uint pname, Stream @params) { }
        public override void TexParameterxv(uint target, uint pname, Stream @params) { }
        public override void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, Stream pixels) { gl.TexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, GetTypedArray(pixels, type)); CheckError("glTexSubImage2D"); }
        public override void VertexPointer(int size, uint type, int stride, Stream pointer) { VertexAttribPointer(ARRAY_POSITION, size, type, false, stride, pointer); CheckError("glVertexPointer"); }
        public override void Viewport(int x, int y, int width, int height) { base.Viewport(x, y, width, height); gl.Viewport(x, y, width, height); CheckError("glViewport"); }

        #endregion

        #region Implementation GLES20

        public override void GenerateMipmap(uint target) { gl.GenerateMipmap(target); CheckError("genMipmap"); }

        #endregion

        private void PolygonMode(int i, int j) { }

        private int GetTextureMode(uint indx)
        {
            return (_texEnvMode[indx] == (int)REPLACE ? 0 : (_textureFormats[_boundTextureId[indx]] == 3 ? 2 : 1));
        }

        private void UpdatTCBuffer(Stream s, int offset, int count)
        {
            WebGLBufferData b = _buffers[ARRAY_TEXCOORD_0];
            gl.BindBuffer(ARRAY_BUFFER, b.Buffer);
            long p = s.Position;
            long l = s.Length;
            s.Position = p + offset;
            s.SetLength(p + offset + count);
            gl.BufferSubData(ARRAY_BUFFER, offset * 4, GetTypedArray(s, FLOAT));
            s.Position = p;
            s.SetLength(l);
        }

        private void PrepareDraw()
        {
            if (UpdateMvpMatrix())
            {
                gl.UniformMatrix4fv(_uMvpMatrix, false, _mvpMatrix); CheckError("prepareDraw");
            }
            gl.Uniform1i(_uTexEnv0, GetTextureMode(0));
            gl.Uniform1i(_uTexEnv1, GetTextureMode(1));
            for (uint indx = 0; indx < SMALL_BUF_COUNT; indx++)
            {
                WebGLBufferData b = _buffers[indx];
                if (b.ToBind != null)
                {
                    gl.BindBuffer(ARRAY_BUFFER, b.Buffer); CheckError("bindBuffer" + indx);
                    gl.BufferData(ARRAY_BUFFER, b.ToBind, STREAM_DRAW); CheckError("bufferData" + indx);
                    gl.VertexAttribPointer(indx, b.Size, b.Type, b.Normalized, b.Stride, 0); CheckError("vertexAttribPointer");
                    b.ToBind = null;
                }
            }
            //Log("prepDraw: " + sizes);
        }

        private void VertexAttribPointer(uint indx, int size, uint type, bool normalize, int stride, Stream pointer)
        {
            WebGLBufferData b = _buffers[indx];
            b.Stride = stride;
            b.Size = size;
            b.Normalized = normalize;
            b.Type = type;
            b.ToBind = GetTypedArray(pointer, type);
        }

        private void VertexAttribStaticDrawPointer(uint indx, int size, uint type, bool normalized, int stride, long offset, Stream pointer, int staticDrawIndex)
        {
            WebGLBuffer b = _staticDrawBuffers[staticDrawIndex];
            if (b == null)
            {
                _staticDrawBuffers[staticDrawIndex] = b = gl.CreateBuffer();
                gl.BindBuffer(ARRAY_BUFFER, b);
                gl.BufferData(ARRAY_BUFFER, GetTypedArray(pointer, type), STATIC_DRAW); CheckError("bufferData");
                //Log("static buffer created; id: " + staticDrawIndex + " remaining: " + pointer.Remaining());
            }
            gl.BindBuffer(ARRAY_BUFFER, b);
            gl.VertexAttribPointer(indx, size, type, normalized, stride, offset); CheckError("vertexAttribPointer");
            _buffers[indx].ToBind = null;
        }

        private ArrayBufferView GetTypedArray(Stream s, uint type)
        {
#if CODE_ANALYSIS
            MemoryStream memoryStream = (s as MemoryStream);
            //if (arrayHolder == null)
            //{
            //    if ((type != BYTE) && (type != UNSIGNED_BYTE))
            //        throw new Exception("RuntimeException: Buffer byte order problem");
            //    //arrayHolder = (HasArrayBufferView)((ByteBufferWrapper)s).getByteBuffer();
            //}
            ArrayBufferView array = null; // memoryStream.GetBuffer();
            int remainingBytes = (int)(s.Length - s.Position);
            int byteOffset = 0; // array.ByteOffset + ((int)s.Position * elementSize);
            switch (type)
            {
                case FLOAT:
                    return new Float32Array(array.Buffer, byteOffset, remainingBytes / 4);
                case UNSIGNED_BYTE:
                    return new Uint8Array(array.Buffer, byteOffset, remainingBytes);
                case UNSIGNED_SHORT:
                    return new Uint16Array(array.Buffer, byteOffset, remainingBytes / 2);
                case INT:
                    return new Int32Array(array.Buffer, byteOffset, remainingBytes / 4);
                case SHORT:
                    return new Int16Array(array.Buffer, byteOffset, remainingBytes / 2);
                case BYTE:
                    return new Int8Array(array.Buffer, byteOffset, remainingBytes);
            }
#endif
            throw new Exception("IllegalArgumentException:");
        }
    }
}
