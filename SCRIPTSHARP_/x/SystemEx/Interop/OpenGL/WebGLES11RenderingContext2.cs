//#if !CODE_ANALYSIS
//using System.IO;
//using System.Collections;
//namespace System.Interop.OpenGL
//#else
//using System;
//using SystemEx.IO;
//using System.Collections;
//using System.Specialized;
//using System.Interop.OpenGL;
//using System.Html;
//namespace SystemEx.Interop.OpenGL
//#endif
//{
//    public partial class WebGLES11RenderingContext2 : WebGLES11RenderingContext
//    {
//        private int logCount = 0;
//        public void log(string msg)
//        {
//            if (logCount >= 1000)
//                return;
//            MozConsole.Log(logCount++ + ": " + msg);
//        }
//        private const int SMALL_BUF_COUNT = 4;
//        private WebGLUniformLocation _uMvpMatrix;
//        private WebGLUniformLocation _uSampler0;
//        private WebGLUniformLocation _uSampler1;
//        private WebGLUniformLocation _uTexEnv0;
//        private WebGLUniformLocation _uTexEnv1;
//        private WebGLUniformLocation _uEnableTexture0;
//        private WebGLUniformLocation _uEnableTexture1;
//        private WebGLBuffer[] _staticBuffers; // = new Array();
//        private WebGLES11BufferData[] _bufferData = new WebGLES11BufferData[SMALL_BUF_COUNT];
//        public WebGLRenderingContext gl;
//        private Stream colorBuffer;
//        private CanvasElementEx _canvas;
//        private WebGLTexture[] _textures; // = (JsArray<WebGLTexture>) JsArray.createArray();
//        private int[] _textureFormats; // = (JsArrayInteger) JsArray.createArray();
//        private uint _clientActiveTexture = 0;
//        private uint _activeTexture = 0;
//        private int[] _boundTextureId = new int[2];
//        private int[] _texEnvMode = new int[2];
//        private int[] _textureFormat; // = (JsArrayInteger) JavaScriptObject.createArray();
//        private WebGLBuffer _elementBuffer;

//        public WebGLES11RenderingContext2(CanvasElementEx canvas)
//            : base(canvas.Width, canvas.Height)
//        {
//            _canvas = canvas;
//            gl = canvas.GetContextWebGL();
//            if (gl == null)
//                throw new Exception("UnsupportedOperationException: WebGL N/A");
//            InitShaders(); CheckError("initShader");
//            _elementBuffer = gl.CreateBuffer(); CheckError("createBuffer f. elements");
//            for (int index = 0; index < _bufferData.Length; index++)
//            {
//                WebGLES11BufferData b = new WebGLES11BufferData();
//                b.Buffer = gl.CreateBuffer(); CheckError("createBuffer" + index);
//                _bufferData[index] = b;
//            }
//        }

//        public void CheckError(string s)
//        {
//            uint err = gl.GetError();
//            if (err != NO_ERROR)
//                log("GL_ERROR in " + s + "(): " + err);
//        }

//        private void InitShaders()
//        {
//            // create our shaders
//            WebGLShader vertexShader = LoadShader(VERTEX_SHADER, @"
//attribute vec4 a_position;
//attribute vec4 a_color;
//attribute vec2 a_texCoord0;
//attribute vec2 a_texCoord1;
//uniform mat4 u_mvpMatrix;
//varying vec4 v_color;
//varying vec2 v_texCoord0;
//varying vec2 v_texCoord1;
//void main()
//{
//    gl_Position = u_mvpMatrix * a_position;
//    v_color = a_color;
//    v_texCoord0 = a_texCoord0;
//    v_texCoord1 = a_texCoord1;
//}");
//            WebGLShader fragmentShader = LoadShader(FRAGMENT_SHADER, @"
//#ifdef GL_ES
//precision mediump float;
//#endif
//uniform sampler2D s_texture0;
//uniform sampler2D s_texture1;
//uniform int s_texEnv0;
//uniform int s_texEnv1;
//uniform int u_enable_texture_0;
//uniform int u_enable_texture_1;
//varying vec4 v_color;
//varying vec2 v_texCoord0;
//varying vec2 v_texCoord1;
//vec4 finalColor;
//void main()
//{
//    finalColor = v_color;
//    if (u_enable_texture_0 == 1)
//    {
//        vec4 texel = texture2D(s_texture0, v_texCoord0);
//        if (s_texEnv0 == 1) {
//            finalColor = finalColor * texel;
//        } else if (s_texEnv0 == 2) {
//            finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);
//        } else {
//            finalColor = texel;
//        }
//    }
//    if (u_enable_texture_1 == 1) {
//        vec4 texel = texture2D(s_texture1, v_texCoord1);
//        if (s_texEnv1 == 1) {
//            finalColor = finalColor * texel;
//        } else if (s_texEnv1 == 2) {
//            finalColor = vec4(texel.r, texel.g, texel.b, finalColor.a);
//        } else {
//            finalColor = texel;
//        }
//    }
//    // simple alpha check
//    if (finalColor.a == 0.0) {
//        discard;
//    }
//    float gamma = 1.5;
//    float igamma = 1.0 / gamma;
//    gl_FragColor = vec4(pow(finalColor.r, igamma), pow(finalColor.g, igamma), pow(finalColor.b, igamma), finalColor.a);
//}");
//            if ((vertexShader == null) || (fragmentShader == null))
//                throw new Exception("RuntimeException: shader error");
//            // Create the program object
//            WebGLProgram programObject = gl.CreateProgram();
//            if ((programObject == null) || (gl.GetError() != NO_ERROR))
//                throw new Exception("RuntimeException: program error");
//            // Attach our two shaders to the program
//            gl.AttachShader(programObject, vertexShader);
//            gl.AttachShader(programObject, fragmentShader);
//            // Bind "vPosition" to attribute 0
//            gl.BindAttribLocation(programObject, ARRAY_POSITION, "a_position");
//            gl.BindAttribLocation(programObject, WebGL.ARRAY_COLOR, "a_color");
//            gl.BindAttribLocation(programObject, WebGL.ARRAY_TEXCOORD_0, "a_texCoord0");
//            gl.BindAttribLocation(programObject, WebGL.ARRAY_TEXCOORD_1, "a_texCoord1");
//            // Link the program
//            gl.LinkProgram(programObject);
//            // TODO(haustein) get position, color from the linker, too
//            _uMvpMatrix = gl.GetUniformLocation(programObject, "u_mvpMatrix");
//            _uSampler0 = gl.GetUniformLocation(programObject, "s_texture0");
//            _uSampler1 = gl.GetUniformLocation(programObject, "s_texture1");
//            _uTexEnv0 = gl.GetUniformLocation(programObject, "s_texEnv0");
//            _uTexEnv1 = gl.GetUniformLocation(programObject, "s_texEnv1");
//            _uEnableTexture0 = gl.GetUniformLocation(programObject, "u_enable_texture_0");
//            _uEnableTexture1 = gl.GetUniformLocation(programObject, "u_enable_texture_1");
//            // Check the link status
//            bool linked = (bool)gl.GetProgramParameter(programObject, LINK_STATUS);
//            if (!linked)
//                throw new Exception("RuntimeException: linker Error: " + gl.GetProgramInfoLog(programObject));
//            gl.UseProgram(programObject);
//            gl.Uniform1i(_uSampler0, 0);
//            gl.Uniform1i(_uSampler1, 1);
//            gl.ActiveTexture(TEXTURE0);
//        }

//        private WebGLShader LoadShader(uint shaderType, string shaderSource)
//        {
//            // Create the shader object
//            WebGLShader shader = gl.CreateShader(shaderType);
//            if (shader == null)
//                throw new Exception("RuntimeException:");
//            // Load the shader source
//            gl.ShaderSource(shader, shaderSource);
//            // Compile the shader
//            gl.CompileShader(shader);
//            // Check the compile status
//            bool compiled = (bool)gl.GetShaderParameter(shader, COMPILE_STATUS);
//            if (!compiled)
//                // Something went wrong during compilation; get the error
//                throw new Exception("RuntimeException: Shader compile error: " + gl.GetShaderInfoLog(shader));
//            return shader;
//        }


//        //public string FloatArrayToString(Float32Array fa)
//        //{
//        //    StringBuilder b = new StringBuilder();
//        //    b.Append("len: " + fa.Length);
//        //    b.Append("data: ");
//        //    for (int i = 0; i < Math.Min(fa.Length, 10); i++)
//        //        b.Append(fa[i] + ",");
//        //    return b.ToString();
//        //}

//        //public string Int32ArrayToString(Int32Array fa)
//        //{
//        //    StringBuilder b = new StringBuilder();
//        //    b.Append("len: " + fa.Length);
//        //    b.Append("data: ");
//        //    for (int i = 0; i < Math.Min(fa.Length, 10); i++)
//        //        b.Append(fa[i] + ",");
//        //    return b.ToString();
//        //}

//        //public string Uint16ArrayToString(Uint16Array fa)
//        //{
//        //    StringBuilder b = new StringBuilder();
//        //    b.Append("len: " + fa.Length);
//        //    b.Append("data: ");
//        //    for (int i = 0; i < Math.Min(fa.Length, 10); i++)
//        //        b.Append(fa[i] + ",");
//        //    return b.ToString();
//        //}

//        public override void ActiveTexture(uint texture) { gl.ActiveTexture(texture); CheckError("glActiveTexture"); _activeTexture = texture - TEXTURE0; }
//        public override void glAlphaFunc(int i, float j)
//        {
//            // TODO: Remove this. Alpha text/func are unsupported in ES.
//        }

//        public override void glClientActiveTexture(uint texture)
//        {
//            _clientActiveTexture = texture - TEXTURE0;
//        }

//        public override void glColorPointer(int size, int stride, FloatBuffer colorArrayBuf) { glColorPointer(size, FLOAT, stride, colorArrayBuf); }
//        public override void glColorPointer(int size, bool unsigned, int stride, ByteBuffer colorAsByteBuffer) { glColorPointer(size, unsigned ? UNSIGNED_BYTE : BYTE, stride, colorAsByteBuffer); }
//        private void glColorPointer(int size, int type, int stride, Buffer buf)
//        {
//            glVertexAttribPointer(WebGL.ARRAY_COLOR, size, type, true, stride, buf);
//            CheckError("glColorPointer");
//        }

//        public override void glDeleteTextures(IntBuffer texnumBuffer)
//        {
//            for (int i = 0; i < texnumBuffer.remaining(); i++)
//            {
//                int tid = texnumBuffer.get(texnumBuffer.position() + i);
//                gl.DeleteTexture(_textures[tid]);
//                _textures[tid] = null;
//                CheckError("glDeleteTexture");
//            }
//        }

//        public override void glDepthFunc(uint func) { gl.DepthFunc(func); CheckError("glDepthFunc"); }
//        public override void glDepthMask(bool b) { gl.DepthMask(b); CheckError("glDepthMask"); }

//        public override void glDepthRange(float gldepthmin, float gldepthmax)
//        {
//            gl.depthRange(gldepthmin, gldepthmax);
//            CheckError("glDepthRange");
//        }

//        public override void glDrawBuffer(int buf)
//        {
//            // specify which color buffers are to be drawn into
//        }

//        public override void glDrawElements(int mode, ShortBuffer srcIndexBuf)
//        {
//            prepareDraw();

//            gl.bindBuffer(ELEMENT_ARRAY_BUFFER, _elementBuffer);
//            CheckError("bindBuffer(el)");
//            gl.bufferData(ELEMENT_ARRAY_BUFFER, getTypedArray(srcIndexBuf,
//                UNSIGNED_SHORT), DYNAMIC_DRAW);
//            CheckError("bufferData(el)");

//            int count = srcIndexBuf.remaining();
//            gl.drawElements(mode, count, UNSIGNED_SHORT, 0);
//            CheckError("drawElements");
//        }

//        public override void glFinish()
//        {
//            gl.finish();
//        }

//        public override string glGetString(int id)
//        {
//            // TODO: Where is getParameter()?
//            // String s = gl.getParameter(id);
//            //return s == null ? "" : s;
//            return "glGetString not implemented";
//        }

//        public override void glPixelStorei(int i, int j)
//        {
//            gl.pixelStorei(i, j);
//        }

//        public override void glPointParameterf(int id, float value)
//        {
//            // TODO Auto-generated method stub
//        }

//        public override void glPointSize(float value)
//        {
//            // TODO Auto-generated method stub
//        }

//        public override void glPolygonMode(int i, int j)
//        {
//            // TODO Auto-generated method stub
//        }

//        public override void glReadPixels(int x, int y, int width, int height, int glBgr, int glUnsignedByte, ByteBuffer image)
//        {
//            // TODO Auto-generated method stub
//        }

//        public override void glTexCoordPointer(int size, int byteStride, FloatBuffer buf)
//        {
//            glVertexAttribPointer(GLAdapter.ARRAY_TEXCOORD_0 + _clientActiveTexture, size, GL_FLOAT, false, byteStride, buf);
//            CheckError("texCoordPointer");
//        }

//        public override void glTexEnvi(int target, int pid, int value)
//        {
//            _texEnvMode[_activeTexture] = value;
//        }

//        public override void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, ByteBuffer pixels)
//        {
//            _textureFormat.set(_boundTextureId[_activeTexture], internalformat);
//            ArrayBufferView array = getTypedArray(pixels, type);
//            gl.texImage2D(target, level, internalformat, width, height, border,
//                format, type, array);
//            CheckError("glTexImage2D");
//        }

//        public override void glTexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, IntBuffer pixels)
//        {
//            _textureFormat.set(_boundTextureId[_activeTexture], internalformat);
//            ArrayBufferView array = getTypedArray(pixels, type);
//            gl.texImage2D(target, level, internalformat, width, height, border,
//                format, type, array);
//            CheckError("glTexImage2D");
//        }

//        public override void glTexParameteri(int glTexture2d, int glTextureMinFilter, int glFilterMin)
//        {
//            gl.texParameteri(glTexture2d, glTextureMinFilter, glFilterMin);
//            CheckError("glTexParameteri");
//        }

//        public override void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, ByteBuffer pixels)
//        {
//            ArrayBufferView array = getTypedArray(pixels, type);
//            gl.texSubImage2D(target, level, xoffset, yoffset, width, height, format,
//                type, array);
//            CheckError("glTexSubImage2D");
//        }

//        public override void glTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, IntBuffer pixels)
//        {
//            ArrayBufferView array = getTypedArray(pixels, type);
//            gl.texSubImage2D(target, level, xoffset, yoffset, width, height, format,
//                type, array);
//            CheckError("glTexSubImage2D");
//        }

//        public override void glVertexPointer(int size, int byteStride, FloatBuffer buf)
//        {
//            glVertexAttribPointer(GLAdapter.ARRAY_POSITION, size, GL_FLOAT, false, byteStride, buf);
//            CheckError("glVertexPointer");

//        }

//        public override void setDisplayMode(DisplayMode displayMode)
//        {
//            _canvas.setWidth(displayMode.width);
//            _canvas.setHeight(displayMode.height);
//        }

//        public DisplayMode[] getAvailableDisplayModes()
//        {
//            return new DisplayMode[] { getDisplayMode(), new DisplayMode(Window.getClientWidth(), Window.getClientHeight(), 32, 60) };
//        }

//        public override void shutdow()
//        {
//            // TODO Auto-generated method stub
//        }

//        public override void swapBuffers()
//        {
//            // TODO Auto-generated method stub
//        }

//        public override void glBindTexture(int target, int textureId)
//        {
//            WebGLTexture texture = _textures.get(textureId);
//            if (texture == null)
//            {
//                texture = gl.createTexture();
//                _textures.set(textureId, texture);
//            }
//            // log ("binding texture " + texture + " id " + textureId + " for activeTexture: " + (activeTexture-GL_TEXTURE0));
//            gl.bindTexture(target, texture);
//            CheckError("glBindTexture");
//            _boundTextureId[_activeTexture] = textureId;
//            // glColor3f((float)Math.random(), (float)Math.random(), (float)Math.random());
//        }

//        public override void glBlendFunc(int a, int b)
//        {
//            gl.blendFunc(a, b);
//            CheckError("glBlendFunc");
//        }

//        public override void glClear(int mask)
//        {
//            gl.clear(mask);
//            CheckError("glClear");
//        }

//        public override void glColor4f(float red, float green, float blue, float alpha)
//        {
//            gl.vertexAttrib4f(GLAdapter.ARRAY_COLOR, red, green, blue, alpha);
//            CheckError("glColor4f");
//        }

//        public void glTexImage2d(int target, int level, int internalformat, int format, int type, ImageElement image)
//        {
//            // log("setting texImage2d; image: " + image.getSrc());
//            gl.texImage2D(target, level, internalformat, format, type, image);
//            CheckError("texImage2D");
//        }

//        public void glTexImage2d(int target, int level, int internalformat, int format, int type, CanvasElement image)
//        {
//            // log("setting texImage2d; image: " + image.getSrc());
//            gl.texImage2D(target, level, internalformat, format, type, image);
//            CheckError("texImage2D");
//        }

//        public override void glEnable(int i)
//        {
//            // In ES, you don't enable/disable TEXTURE_2D. We use it this call to
//            // enable one of the two active textures supported by the shader.
//            if (i == GL_TEXTURE_2D)
//            {
//                switch (_activeTexture)
//                {
//                    case 0:
//                        gl.uniform1i(_uEnableTexture0, 1);
//                        break;
//                    case 1:
//                        gl.uniform1i(_uEnableTexture1, 1);
//                        break;
//                    default:
//                        throw new RuntimeException();
//                }
//                return;
//            }

//            gl.enable(i);
//            CheckError("glEnable");
//        }

//        public override int glGetError()
//        {
//            return gl.getError();
//        }

//        public override void glClearColor(float f, float g, float h, float i)
//        {
//            gl.clearColor(f, g, h, i);
//            CheckError("glClearColor");
//        }

//        public override void glDrawArrays(int mode, int first, int count)
//        {
//            prepareDraw();
//            // log("drawArrays mode:" + mode + " first:" + first + " count:" +count);
//            gl.drawArrays(mode, first, count);
//            CheckError("drawArrays");
//        }

//        public void updatTCBuffer(FloatBuffer buf, int offset, int count)
//        {
//            WebGLES11BufferData bd = _bufferData[GLAdapter.ARRAY_TEXCOORD_0];
//            gl.bindBuffer(ARRAY_BUFFER, bd.Buffer);

//            int pos = buf.position();
//            int limit = buf.limit();

//            buf.position(pos + offset);
//            buf.limit(pos + offset + count);
//            ArrayBufferView data = getTypedArray(buf, GL_FLOAT);
//            gl.bufferSubData(ARRAY_BUFFER, offset * 4, data);
//            buf.position(pos);
//            buf.limit(limit);
//        }

//        private void prepareDraw()
//        {
//            if (updateMvpMatrix())
//            {
//                gl.UniformMatrix4fv(_uMvpMatrix, false, Float32Array.create(mvpMatrix));
//                CheckError("prepareDraw");
//            }
//            gl.Uniform1i(_uTexEnv0, getTextureMode(0));
//            gl.Uniform1i(_uTexEnv1, getTextureMode(1));
//            // StringBuilder sizes = new StringBuilder();
//            for (int i = 0; i < SMALL_BUF_COUNT; i++)
//            {
//                WebGLES11BufferData bd = _bufferData[i];
//                if (bd.ToBind != null)
//                {
//                    gl.BindBuffer(ARRAY_BUFFER, bd.Buffer);
//                    CheckError("bindBuffer" + i);
//                    // int len = bd.toBind.getByteLength();
//                    // if (len < bd.byteSize) {
//                    // gl.glBufferSubData(WebGL.GL_ARRAY_BUFFER, 0, bd.toBind);
//                    // } else {
//                    // bd.byteSize = len;
//                    gl.BufferData(ARRAY_BUFFER, bd.ToBind, STREAM_DRAW);
//                    // }
//                    CheckError("bufferData" + i);
//                    gl.VertexAttribPointer(i, bd.Size, bd.Type, bd.Normalize, bd.ByteStride, 0);
//                    CheckError("vertexAttribPointer");
//                    bd.ToBind = null;
//                }
//            }
//            // log ("prepDraw: " + sizes);
//        }

//        private int getTextureMode(int i)
//        {
//            return _texEnvMode[i] == GL_REPLACE ? 0 : (_textureFormats.get(_boundTextureId[i]) == 3 ? 2 : 1);
//        }

//        public override void glScissor(int i, int j, int width, int height)
//        {
//            gl.scissor(i, j, width, height);
//            CheckError("glScissor");
//        }

//        public override void glTexParameterf(int target, int pname, float param)
//        {
//            gl.texParameterf(target, pname, param);
//            CheckError("glTexParameterf");
//        }

//        public override void glEnableClientState(int i)
//        {
//            switch (i)
//            {
//                case GL_COLOR_ARRAY:
//                    gl.enableVertexAttribArray(GLAdapter.ARRAY_COLOR);
//                    CheckError("enableClientState colorArr");
//                    break;
//                case GL_VERTEX_ARRAY:
//                    gl.enableVertexAttribArray(GLAdapter.ARRAY_POSITION);
//                    CheckError("enableClientState vertexArrr");
//                    break;
//                case GL_TEXTURE_COORD_ARRAY:
//                    switch (_clientActiveTexture)
//                    {
//                        case 0:
//                            gl.enableVertexAttribArray(GLAdapter.ARRAY_TEXCOORD_0);
//                            CheckError("enableClientState texCoord0");
//                            break;
//                        case 1:
//                            gl.enableVertexAttribArray(GLAdapter.ARRAY_TEXCOORD_1);
//                            CheckError("enableClientState texCoord1");
//                            break;
//                        default:
//                            throw new RuntimeException();
//                    }
//                    break;
//                default:
//                    log("unsupported / unrecogized client state " + i);
//            }
//        }

//        public override void glDisableClientState(int i)
//        {
//            switch (i)
//            {
//                case GL_COLOR_ARRAY:
//                    gl.disableVertexAttribArray(GLAdapter.ARRAY_COLOR);
//                    break;
//                case GL_VERTEX_ARRAY:
//                    gl.disableVertexAttribArray(GLAdapter.ARRAY_POSITION);
//                    break;
//                case GL_TEXTURE_COORD_ARRAY:
//                    switch (_clientActiveTexture)
//                    {
//                        case 0:
//                            gl.disableVertexAttribArray(GLAdapter.ARRAY_TEXCOORD_0);
//                            break;
//                        case 1:
//                            gl.disableVertexAttribArray(GLAdapter.ARRAY_TEXCOORD_1);
//                            break;
//                        default:
//                            throw new RuntimeException();
//                    }
//                    break;
//                default:
//                    log("unsupported / unrecogized client state");
//            }
//            CheckError("DisableClientState");
//        }

//        public override void glDisable(int i)
//        {
//            // In ES, you don't enable/disable TEXTURE_2D. We use it this call to
//            // disable one of the two active textures supported by the shader.
//            if (i == GL_TEXTURE_2D)
//            {
//                switch (_activeTexture)
//                {
//                    case 0:
//                        gl.uniform1i(_uEnableTexture0, 0);
//                        break;
//                    case 1:
//                        gl.uniform1i(_uEnableTexture1, 0);
//                        break;
//                    default:
//                        throw new RuntimeException();
//                }
//                return;
//            }

//            gl.disable(i);
//            CheckError("glDisable");
//        }

//        public override void glCullFace(int c)
//        {
//            gl.cullFace(c);
//            CheckError("glCullFace");
//        }

//        public override void glShadeModel(int s)
//        {
//        }

//        public override void glViewport(int x, int y, int w, int h)
//        {
//            super.glViewport(x, y, w, h);
//            gl.viewport(x, y, w, h);
//            CheckError("glViewport");
//        }

//        public void glVertexAttribPointer(int arrayId, int size, int type, bool normalize, int byteStride, Buffer nioBuffer)
//        {
//            WebGLES11BufferData bd = _bufferData[arrayId];
//            bd.ByteStride = byteStride;
//            bd.Size = size;
//            bd.Normalize = normalize;
//            bd.Type = type;
//            ArrayBufferView webGLArray = getTypedArray(nioBuffer, type);
//            bd.ToBind = webGLArray;
//        }

//        public void glVertexAttribPointer(int arrayId, int size, int type, bool normalize, int byteStride, int offset, Buffer nioBuffer, int staticDrawId)
//        {
//            WebGLBuffer buffer = _staticBuffers.get(staticDrawId);
//            if (buffer == null)
//            {
//                buffer = gl.createBuffer();
//                _staticBuffers.set(staticDrawId, buffer);
//                gl.bindBuffer(ARRAY_BUFFER, buffer);
//                ArrayBufferView webGLArray = getTypedArray(nioBuffer, type);
//                gl.bufferData(ARRAY_BUFFER, webGLArray, STATIC_DRAW);
//                CheckError("bufferData");
//                log("static buffer created; id: " + staticDrawId + " remaining: " + nioBuffer.remaining());
//            }
//            gl.bindBuffer(ARRAY_BUFFER, buffer);
//            gl.vertexAttribPointer(arrayId, size, type, normalize, byteStride, offset);
//            _bufferData[arrayId].ToBind = null;
//            CheckError("vertexAttribPointer");
//        }

//        private ArrayBufferView getTypedArray(Buffer buffer, int type)
//        {
//            int elementSize;
//            HasArrayBufferView arrayHolder;
//            if (!(buffer is HasArrayBufferView))
//            {
//                if (type != GL_BYTE && type != GL_UNSIGNED_BYTE)
//                {
//                    log("buffer byte order problem");
//                    throw new Exception("RuntimeException: Buffer byte order problem");
//                }
//                if (buffer is IntBuffer)
//                    elementSize = 4;
//                else
//                    throw new RuntimeException("NYI");
//                arrayHolder = (HasArrayBufferView)((ByteBufferWrapper)buffer).getByteBuffer();
//            }
//            else
//            {
//                arrayHolder = (HasArrayBufferView)buffer;
//                elementSize = arrayHolder.getElementSize();
//            }
//            ArrayBufferView webGLArray = arrayHolder.getTypedArray();
//            int remainingBytes = buffer.remaining() * elementSize;
//            int byteOffset = webGLArray.getByteOffset() + buffer.position() * elementSize;
//            switch (type)
//            {
//                case FLOAT:
//                    return Float32Array.create(webGLArray.getBuffer(), byteOffset, remainingBytes / 4);
//                case UNSIGNED_BYTE:
//                    return Uint8Array.create(webGLArray.getBuffer(), byteOffset, remainingBytes);
//                case UNSIGNED_SHORT:
//                    return Uint16Array.create(webGLArray.getBuffer(), byteOffset, remainingBytes / 2);
//                case INT:
//                    return Int32Array.create(webGLArray.getBuffer(), byteOffset, remainingBytes / 4);
//                case SHORT:
//                    return Int16Array.create(webGLArray.getBuffer(), byteOffset, remainingBytes / 2);
//                case BYTE:
//                    return Int8Array.create(webGLArray.getBuffer(), byteOffset, remainingBytes);
//            }
//            throw new IllegalArgumentException();
//        }

//        public override void GenerateMipmap(uint target) { gl.GenerateMipmap(target); CheckError("genMipmap"); }
//    }
//}
