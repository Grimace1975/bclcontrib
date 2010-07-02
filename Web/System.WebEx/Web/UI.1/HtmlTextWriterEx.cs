using System.Reflection;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
namespace System.Web.UI
{
    /// <summary>
    /// HtmlTextWriterEx
    /// </summary>
    public class HtmlTextWriterEx : HtmlTextWriter
    {
        public static readonly string TofuUri = "/WebResource.axd?d=5n2TUlkoTdAS-KsOrl50eRfwyioj-rmID-zYEi3RUhcnLXiS2Mpbuuzm1Sc8arqCRn7QycAdJALOtf73b2HlzQ2&amp;t=633426477648026143";
        internal static readonly int HtmlAttributeSplit = (int)HtmlAttribute.__Undefined;
        private HtmlBuilder _htmlBuilder;

        static HtmlTextWriterEx()
        {
            FixHtmlTagTable();
        }

        private static void FixHtmlTagTable()
        {
            Type htmlTextWriterType = Type.GetType("System.Web.UI.HtmlTextWriter, " + AssemblyRef.SystemWeb, true, true);
            // remove br tag
            var _tagKeyLookupTableField = htmlTextWriterType.GetField("_tagKeyLookupTable", BindingFlags.Static | BindingFlags.NonPublic);
            var _tagKeyLookupTable = (Hashtable)_tagKeyLookupTableField.GetValue(null);
            _tagKeyLookupTable.Remove("br");
            // add br tag
            Type htmlTextWriter_TagTypeType = Type.GetType("System.Web.UI.HtmlTextWriter+TagType, " + AssemblyRef.SystemWeb, true, true);
            int tagType_NonClosing = (int)Enum.Parse(htmlTextWriter_TagTypeType, "NonClosing");
            var registerTagMethod = htmlTextWriterType.GetMethod("RegisterTag", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(string), typeof(HtmlTextWriterTag), htmlTextWriter_TagTypeType }, null);
            registerTagMethod.Invoke(null, BindingFlags.InvokeMethod, null, new object[] { "br", HtmlTextWriterTag.Br, tagType_NonClosing }, CultureInfo.CurrentCulture);
        }

        public HtmlTextWriterEx()
            : base(new StringWriter())
        {
            _htmlBuilder = CreateHtmlBuilder(this);
        }
        public HtmlTextWriterEx(TextWriter w)
            : base(w)
        {
            _htmlBuilder = CreateHtmlBuilder(this);
        }
        public HtmlTextWriterEx(TextWriter w, string tabText)
            : base(w, tabText)
        {
            _htmlBuilder = CreateHtmlBuilder(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _htmlBuilder.Dispose();
            base.Dispose(disposing);
        }

        public void AddAttributeIfUndefined(HtmlAttribute attribute, string value)
        {
            int attribute2 = (int)attribute;
            if (attribute2 < HtmlAttributeSplit)
                AddAttributeIfUndefined((HtmlTextWriterAttribute)attribute2, value);
            else if (attribute2 > HtmlAttributeSplit)
                AddStyleAttributeIfUndefined((HtmlTextWriterStyle)attribute2 - HtmlAttributeSplit - 1, value);
            else
                throw new ArgumentException(string.Format("Local.InvalidHtmlAttribA", attribute.ToString()), "attribute");
        }

        public void AddAttributeIfUndefined(HtmlTextWriterAttribute attribute, string value)
        {
            if (!IsAttributeDefined(attribute))
                AddAttribute(attribute, value);
        }

        public void AddStyleAttributeIfUndefined(HtmlTextWriterStyle attribute, string value)
        {
            if (!IsStyleAttributeDefined(attribute))
                AddStyleAttribute(attribute, value);
        }

        public HtmlBuilder HtmlBuilder
        {
            get { return _htmlBuilder; }
        }

        public string InnerText
        {
            get
            {
                var innerWriter = (InnerWriter as HtmlTextWriter);
                if (innerWriter != null)
                {
                    var innerStringWriter = (innerWriter.InnerWriter as StringWriter);
                    if (innerStringWriter != null)
                        return innerStringWriter.ToString();
                }
                return string.Empty;
            }
        }

        public bool TryAddAttribute(HtmlAttribute key, out string value)
        {
            int key2 = (int)key;
            if (key2 < HtmlAttributeSplit)
                return TryAddAttribute((HtmlTextWriterAttribute)key2, out value);
            else if (key2 > HtmlAttributeSplit)
                return TryAddStyleAttribute((HtmlTextWriterStyle)(key2 - HtmlAttributeSplit - 1), out value);
            throw new ArgumentException(string.Format("Local.InvalidHtmlAttribA", key.ToString()), "key");
        }

        public bool TryAddAttribute(HtmlTextWriterAttribute key, out string value)
        {
            if (!IsAttributeDefined(key, out value))
            {
                AddAttribute(key, value);
                return true;
            }
            return false;
        }

        public bool TryAddStyleAttribute(HtmlTextWriterStyle key, out string value)
        {
            if (!IsStyleAttributeDefined(key, out value))
            {
                AddStyleAttribute(key, value);
                return true;
            }
            return false;
        }

        public static void RenderControl(Control control, out string text)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            using (var w = new StringWriter())
            {
                using (var w2 = new HtmlTextWriter(w))
                    control.RenderControl(w2);
                text = w.ToString();
            }
        }
        public static void RenderControl(Control control, StringBuilder b)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            using (var w = new StringWriter(b))
                using (var w2 = new HtmlTextWriter(w))
                    control.RenderControl(w2);
        }

        #region Class-Factory
        public virtual HtmlBuilder CreateHtmlBuilder(HtmlTextWriterEx w)
        {
            return new HtmlBuilder(w);
        }
        #endregion
    }
}