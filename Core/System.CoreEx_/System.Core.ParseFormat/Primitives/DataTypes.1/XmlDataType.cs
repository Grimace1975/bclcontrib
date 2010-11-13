#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Xml;
using System.IO;
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// XmlDataType
    /// </summary>
    public class XmlDataType : DataTypeBase
    {
        private static readonly Type s_conformanceLevel = typeof(ConformanceLevel);
        private static readonly Type s_validationType = typeof(ValidationType);

        public class FormatAttrib { }

        public class ParseAttrib
        {
            public ConformanceLevel? ConformanceLevel { get; set; }
            public string SchemaUri { get; set; }
            public string TargetNamespace { get; set; }
            public ValidationType? ValidationType { get; set; }
        }

        public XmlDataType()
            : base(Prime.Type, Prime.FormFieldMeta, new DataTypeFormatter(), new DataTypeParser()) { }

        public class DataTypeFormatter : DataTypeFormatterBase<string, FormatAttrib, ParseAttrib>
        {
            public DataTypeFormatter()
                : base(Prime.Format, Prime.TryParse) { }
        }

        public class DataTypeParser : DataTypeParserBase<string, ParseAttrib>
        {
            public DataTypeParser()
                : base(Prime.TryParse) { }
        }

        /// <summary>
        /// Prime
        /// </summary>
        public class Prime
        {
            public static string Format(string value, FormatAttrib attrib)
            {
                return value;
            }

            public static bool TryParse(string text, ParseAttrib attrib, out string value)
            {
                if (string.IsNullOrEmpty(text))
                {
                    value = string.Empty; return false;
                }
                // check attrib
                var settings = new XmlReaderSettings();
                if (attrib != null)
                {
                    if (attrib.ConformanceLevel != null)
                        settings.ConformanceLevel = attrib.ConformanceLevel.Value;
                    if (attrib.ValidationType != null)
                    {
                        settings.ValidationType = attrib.ValidationType.Value;
                        switch (settings.ValidationType)
                        {
                            case ValidationType.DTD:
                                settings.ProhibitDtd = false;
                                break;
                            case ValidationType.Schema:
                                string targetNamespace = attrib.TargetNamespace;
                                string schemaUri = attrib.SchemaUri;
                                if ((!string.IsNullOrEmpty(targetNamespace)) && (!string.IsNullOrEmpty(schemaUri)))
                                    settings.Schemas.Add(targetNamespace, schemaUri);
                                break;
                        }
                    }
                }
                try
                {
                    var xmlReader = XmlReader.Create(new StringReader(text), settings);
                    xmlReader.Close();
                }
                catch { value = string.Empty; return false; }
                value = text; return true;
            }

            public static Type Type
            {
                get { return typeof(string); }
            }

            public static DataTypeFormFieldMeta FormFieldMeta
            {
                get
                {
                    return new DataTypeFormFieldMeta()
                    {
                        GetBinderType = (int applicationType) => "TextArea",
                        GetMaxLength = (int applicationType) => -1,
                        GetSize = (int applicationType, int length) => "60x15",
                    };
                }
            }
        }
    }
}