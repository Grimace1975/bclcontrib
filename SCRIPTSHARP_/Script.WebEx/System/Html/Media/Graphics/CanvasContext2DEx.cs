using System.Runtime.CompilerServices;
using System.Html.Media.Graphics;
using System.Interop.OpenGL;
namespace System.Html.Media.Graphics
{
    [Imported, IgnoreNamespace]
    public class CanvasContext2DEx : CanvasContextEx
    {
        public void Arc(float x, float y, float radius, float startAngle, float endAngle, bool anticlockwise) { }
        public void ArcTo(float x1, float y1, float x2, float y2, float radius) { }
        public void BeginPath() { }
        public void BezierCurveTo(float cp1x, float cp1y, float cp2x, float cp2y, float x, float y) { }
        private void ClearRect(float x, float y, float w, float h) { }
        public void Clip() { }
        public void ClosePath() { }
        public ImageData CreateImageData(ImageData imagedata) { return null; }
        public ImageData CreateImageData(float sw, float sh) { return null; }
        public Gradient CreateLinearGradient(float x0, float y0, float x1, float y1) { return null; }
        public Pattern CreatePattern(CanvasElement canvas, string repetition) { return null; }
        public Pattern CreatePattern(CanvasElementEx canvas, string repetition) { return null; }
        public Pattern CreatePattern(ImageElement image, string repetition) { return null; }
        public Gradient CreateRadialGradient(float x0, float y0, float r0, float x1, float y1, float r1) { return null; }
        public void DrawImage(CanvasElement image, float dx, float dy) { }
        public void DrawImage(CanvasElementEx image, float dx, float dy) { }
        public void DrawImage(ImageElement image, float dx, float dy) { }
        public void DrawImage(CanvasElement image, float dx, float dy, float dw, float dh) { }
        public void DrawImage(CanvasElementEx image, float dx, float dy, float dw, float dh) { }
        public void DrawImage(ImageElement image, float dx, float dy, float dw, float dh) { }
        public void DrawImage(CanvasElement image, float sx, float sy, float sw, float sh, float dx, float dy, float dw, float dh) { }
        public void DrawImage(CanvasElementEx image, float sx, float sy, float sw, float sh, float dx, float dy, float dw, float dh) { }
        public void DrawImage(ImageElement image, float sx, float sy, float sw, float sh, float dx, float dy, float dw, float dh) { }
        public void Fill() { }
        //private void FillRect(float x, float y, float w, float h) { }
        public void FillText(string text, float x, float y) { }
        public void FillText(string text, float x, float y, float maxWidth) { }
        public ImageData GetImageData(float sx, float sy, float sw, float sh) { return null; }
        public bool IsPointInPath(float x, float y) { return false; }
        public void LineTo(float x, float y) { }
        public TextMetrics MeasureText(string text) { return null; }
        public void MoveTo(float x, float y) { }
        public void PutImageData(ImageData imagedata, float dx, float dy) { }
        public void PutImageData(ImageData imagedata, float dx, float dy, float dirtyX, float dirtyY, float dirtyWidth, float dirtyHeight) { }
        public void QuadraticCurveTo(float cpx, float cpy, float x, float y) { }
        public void Rect(float x, float y, float w, float h) { }
        public void Restore() { }
        public void Rotate(float angle) { }
        public void Save() { }
        public void Scale(float x, float y) { }
        public void SetTransform(float m11, float m12, float m21, float m22, float dx, float dy) { }
        public void Stroke() { }
        //private void StrokeRect(float x, float y, float w, float h) { }
        public void StrokeText(string text, float x, float y) { }
        public void StrokeText(string text, float x, float y, float maxWidth) { }
        public void Transform(float m11, float m12, float m21, float m22, float dx, float dy) { }
        public void Translate(float x, float y) { }

        [IntrinsicProperty, ScriptName("globalAlpha")]
        public float Alpha
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty, ScriptName("globalCompositeOperation")]
        public CompositeOperation CompositeOperation
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public object FillStyle
        {
            get { return null; }
            set { }
        }

        [IntrinsicProperty]
        public string Font
        {
            get { return null; }
            set { }
        }

        [IntrinsicProperty]
        public LineCap LineCap
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public LineJoin LineJoin
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public float LineWidth
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public int MiterLimit
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public float ShadowBlur
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public string ShadowColor
        {
            get { return null; }
            set { }
        }

        [IntrinsicProperty]
        public float ShadowOffsetX
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public float ShadowOffsetY
        {
            get { return 0; }
            set { }
        }

        [IntrinsicProperty]
        public object StrokeStyle
        {
            get { return null; }
            set { }
        }
    }
}
