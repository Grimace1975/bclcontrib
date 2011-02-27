using System.Runtime.CompilerServices;
using System.Html.Media.Graphics;
using System.Interop.OpenGL;
namespace System.Html
{
    // [w3] http://www.khronos.org/webgl/wiki/Debugging
    [IgnoreNamespace, Imported]
    public class CanvasElementEx : Element
    {
        private CanvasElementEx() { }

        public object GetContext(string contextID) { return null; }
        public object GetContext(string contextID, params object[] args) { return null; }
        public CanvasContext2DEx GetContext2D() { return (CanvasContext2DEx)GetContext("2d"); }

        [AlternateSignature]
        public extern WebGLRenderingContext GetContextWebGL();
        public WebGLRenderingContext GetContextWebGL(WebGLContextAttributes attributes)
        {
            if (attributes == null)
                attributes = new WebGLContextAttributes();
            string[] names = { "experimental-webgl", "webgl", "moz-webgl", "webkit-webgl", "webkit-3d" };
            for (int index = 0; index < names.Length; index++)
            {
                try
                {
                    WebGLRenderingContext ctx = (WebGLRenderingContext)GetContext(names[index], attributes);
                    // Hook for the semi-standard WebGLDebugUtils script.
                    if ((bool)Script.Literal("window.WebGLDebugUtils"))
                        return (WebGLRenderingContext)Script.Literal("window.WebGLDebugUtils.makeDebugContext({0})", ctx);
                    return ctx;
                }
                catch { }
            }
            return null;
        }

        [ScriptName("toDataURL")]
        public string GetDataUrl()
        {
            return null;
        }

        [ScriptName("toDataURL")]
        public string GetDataUrl(string mimeType)
        {
            return null;
        }

        [ScriptName("toDataURL")]
        public string GetDataUrl(string mimeType, params object[] typeArguments)
        {
            return null;
        }

        [IntrinsicProperty]
        public int Height
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int Width
        {
            get { return 0; }
            set { }
        }
    }
}
