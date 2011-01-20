using System.Runtime.CompilerServices;
namespace System.WebGL
{
    /// <summary>
    /// The WebGLContextAttributes interface contains drawing surface attributes and is passed as the second parameter to getContext. A native object may be supplied as this parameter; the specified attributes will be queried from this object.
    /// </summary>
    [IgnoreNamespace, Imported]
    // [Record]
    public class WebGLContextAttributes
    {
        /// <summary>
        /// The following list describes each attribute in the WebGLContextAttributes object and its use. For each attribute the default value is shown. The default value is used either if no second parameter is passed to getContext, or if a native object is passed which has no attribute of the given name.
        /// </summary>
        public WebGLContextAttributes() { }

        /// <summary>
        /// Default: true.
        /// If the value is true, the drawing buffer has an alpha channel for the purposes of performing OpenGL destination alpha operations and compositing with the page. If the value is false, no alpha buffer is available.
        /// </summary>
        public bool Alpha
        {
            get { return false; }
        }

        /// <summary>
        /// Default: true.
        /// If the value is true, the drawing buffer has a depth buffer of at least 16 bits. If the value is false, no depth buffer is available.
        /// </summary>
        public bool Depth
        {
            get { return false; }
        }

        /// <summary>
        /// Default: false.
        /// If the value is true, the drawing buffer has a stencil buffer of at least 8 bits. If the value is false, no stencil buffer is available.
        /// </summary>
        public bool Stencil
        {
            get { return false; }
        }

        /// <summary>
        /// Default: true.
        /// If the value is true and the implementation supports antialiasing the drawing buffer will perform antialiasing using its choice of technique (multisample/supersample) and quality. If the value is false or the implementation does not support antialiasing, no antialiasing is performed.
        /// </summary>
        public bool Antialias
        {
            get { return false; }
        }

        /// <summary>
        /// Default: true.
        /// If the value is true the page compositor will assume the drawing buffer contains colors with premultiplied alpha. If the value is false the page compositor will assume that colors in the drawing buffer are not premultiplied. This flag is ignored if the alpha flag is false. See Premultiplied Alpha for more information on the effects of the premultipliedAlpha flag.
        /// </summary>
        public bool PremultipliedAlpha
        {
            get { return false; }
        }

        /// <summary>
        /// Default: false.
        /// If false, once the drawing buffer is presented as described in theDrawing Buffer section, the contents of the drawing buffer are cleared to their default values. All elements of the drawing buffer (color, depth and stencil) are cleared. If the value is true the buffers will not be cleared and will preserve their values until cleared or overwritten by the author. 
        /// note: On some hardware setting the preserveDrawingBuffer flag to true can have significant performance implications.
        /// </summary>
        public bool PreserveDrawingBuffer
        {
            get { return false; }
        }
    }
}
