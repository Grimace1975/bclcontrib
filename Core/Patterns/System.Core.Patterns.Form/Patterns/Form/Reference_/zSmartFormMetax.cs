//using System.Collections.Generic;
//namespace System.Patterns.Forms
//{
//    /// <summary>
//    /// Represents a advanced form processing type that maintains state, processing functionality, and TextColumn/Element support.
//    /// </summary>
//
//    public class SmartFormMeta : Dictionary<string, SmartFormMeta.Element>
//    {
//        /// <summary>
//        /// Enumeration indicating type of element.
//        /// </summary>
//        public enum ElementType
//        {
//            /// <summary>
//            /// Indicates a Unit element
//            /// </summary>
//            Unit,
//            /// <summary>
//            /// Indicates a Label element
//            /// </summary>
//            Label,
//            /// <summary>
//            /// Indicates a TextBox element
//            /// </summary>
//            TextBox,
//            /// <summary>
//            /// Indicates a TextBox element
//            /// </summary>
//            TextBox2,
//            /// <summary>
//            /// Indicates an Error element
//            /// </summary>
//            Error
//        }

//        /// <summary>
//        /// Structure that represents an element contained within a SmartForm instance.
//        /// </summary>
//        public struct Element
//        {
//            /// <summary>
//            /// Type of this MetaElement
//            /// </summary>
//            private ElementType _type;
//            /// <summary>
//            /// Label of the MetaElement
//            /// </summary>
//            private string _label;
//            /// <summary>
//            /// TextColumn type of the MetaElement
//            /// </summary>
//            private string _fieldType;
//            /// <summary>
//            /// Attributes associated with the MetaElement
//            /// </summary>
//            private Nattrib _fieldAttrib;

//            /// <summary>
//            /// Initializes a new instance of the <see cref="MetaElement"/> class.
//            /// </summary>
//            /// <param name="type">The type.</param>
//            /// <param name="label">The label.</param>
//            internal Element(ElementType type, string label)
//                : this(type, label, null, null) { }
//            /// <summary>
//            /// Initializes a new instance of the <see cref="MetaElement"/> class.
//            /// </summary>
//            /// <param name="type">The type.</param>
//            /// <param name="label">The label.</param>
//            /// <param name="fieldType">Type of the field.</param>
//            /// <param name="fieldAttrib">The field attrib.</param>
//            public Element(ElementType type, string label, string fieldType, Nattrib fieldAttrib)
//            {
//                _type = type;
//                _label = label;
//                _fieldType = fieldType;
//                _fieldAttrib = fieldAttrib;
//            }

//            /// <summary>
//            /// Gets the type of this MetaElement
//            /// </summary>
//            /// <value>The type.</value>
//            public ElementType Type
//            {
//                get { return _type; }
//            }

//            /// <summary>
//            /// Gets the label of the MetaElement
//            /// </summary>
//            /// <value>The label.</value>
//            public string Label
//            {
//                get { return _label; }
//            }

//            /// <summary>
//            /// Gets the field type of the MetaElement
//            /// </summary>
//            /// <value>The type of the field.</value>
//            public string FieldType
//            {
//                get { return _fieldType; }
//            }

//            /// <summary>
//            /// Gets the attributes associated with the MetaElement
//            /// </summary>
//            /// <value>The field attrib.</value>
//            public Nattrib FieldAttrib
//            {
//                get { return _fieldAttrib; }
//            }
//        }
//    }
//}
