//using System.Primitives;
//using System.Collections.Generic;
//using System.IO;
//using System.Xml;
//using System.Primitives.TextPacks;
////[assembly: Instinct.Pattern.Environment.Attribute.FactoryConfiguration("smartForm", typeof(System.Patterns.IContract))]
//namespace System.Patterns.Forms
//{
//    /// <summary>
//    /// Represents a advanced form processing type that maintains state, processing functionality, and TextColumn/Element support.
//    /// </summary>
//
//    public class SmartForm
//    {
//        private Dictionary<string, string> _valueHash = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//        private Dictionary<string, string> _replaceTagHash = new Dictionary<string, string>();
//        private int _xmlDepth;
//        private SmartFormMeta _meta = new SmartFormMeta();

//        #region Class Types
//        /// <summary>
//        /// Contract
//        /// </summary>
//        public class Contract : Patterns.Generic.SingletonFactoryWithCreate<Patterns.IContract>
//        {
//            /// <summary>
//            /// Creates the specified key.
//            /// </summary>
//            /// <param name="key">The key.</param>
//            /// <returns></returns>
//            protected static Patterns.IContract Create(string key)
//            {
//                return InversionEx.Resolve<Patterns.IContract>(ResolveLifetime.ApplicationUnit, key, new { typeA = "System.Primitives.SmartFormContracts.{0}SmartFormContract, " + AssemblyRef.This });
//            }
//        }
//        #endregion

//        /// <summary>
//        /// Initializes a new instance of the <see cref="SmartForm"/> class.
//        /// </summary>
//        public SmartForm() { }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="SmartForm"/> class.
//        /// </summary>
//        /// <param name="metaState">State of the meta.</param>
//        public SmartForm(string metaState)
//        {
//            if (string.IsNullOrEmpty(metaState))
//                using (XmlReader r = XmlReader.Create(new StringReader(metaState)))
//                    ReadMetaStateRecurse(string.Empty, r);
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="SmartForm"/> class.
//        /// </summary>
//        /// <param name="metaState">State of the meta.</param>
//        public SmartForm(XmlReader metaState)
//        {
//            if (metaState != null)
//                ReadMetaStateRecurse(string.Empty, metaState);
//        }
//        ///// <summary>
//        ///// Implements the virtual <see cref="Instinct.Base.Dispose(bool)"/> method of
//        ///// <see cref="Instinct.Base"/>.
//        ///// </summary>
//        ///// <param name="disposing">If set to <c>true</c> release any unmanaged resources
//        ///// used by <see cref="SmartForm"/>.</param>
//        ///// <remarks>Calls <c>Dispose()</c> on each item contained in the the underlying
//        ///// <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> instances used.</remarks>
//        //protected override void Dispose(bool disposing)
//        //{
//        //    if (disposing)
//        //    {
//        //        m_replaceTagHash.Clear();
//        //        m_valueHash.Clear();
//        //        m_metaState.Clear();
//        //    }
//        //}

//        /// <summary>
//        /// Gets or sets the <see cref="System.String"/> value associated with the specified key.
//        /// </summary>
//        /// <value></value>
//        public string this[string key]
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(key))
//                    throw new ArgumentNullException("key");
//                string value;
//                return (_valueHash.TryGetValue(key, out value) ? value : string.Empty);
//            }
//            set
//            {
//                if (string.IsNullOrEmpty(key))
//                    throw new ArgumentNullException("key");
//                _valueHash[key] = (value ?? string.Empty);
//            }
//        }

//        /// <summary>
//        /// Executes the contract.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <param name="method">The method.</param>
//        /// <param name="parameterArray">The parameter array.</param>
//        /// <returns></returns>
//        public object ExecuteContract(string key, string method, params object[] args)
//        {
//            object[] array;
//            if ((args != null) && (args.Length > 0))
//            {
//                array = new object[args.Length + 1];
//                args.CopyTo(array, 1);
//                array[0] = this;
//            }
//            else
//                array = new[] { this };
//            return Contract.Get(key).Execute(method, array);
//        }

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is error.
//        /// </summary>
//        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
//        public bool IsError { get; set; }

//        /// <summary>
//        /// Gets the meta.
//        /// </summary>
//        /// <value>The meta.</value>
//        public SmartFormMeta Meta { get; private set; }

//        #region SERIALIZE
//        /// <summary>
//        /// Metas the read error.
//        /// </summary>
//        /// <param name="scopeKey">The scope key.</param>
//        /// <param name="message">The message.</param>
//        private void MetaReadError(string scopeKey, string message)
//        {
//            IsError = true;
//            _meta[scopeKey + "::Error." + _meta.Count.ToString()] = new SmartFormMeta.Element(SmartFormMeta.ElementType.Error, message);
//        }

//        /// <summary>
//        /// Reads the meta field.
//        /// </summary>
//        /// <param name="scopeKey">The scope key.</param>
//        /// <param name="r">The XML reader.</param>
//        private void ReadMetaField(string scopeKey, XmlReader r)
//        {
//            switch (r.LocalName)
//            {
//                case "label":
//                    // <lable>LABEL</label>
//                    _meta[scopeKey + "::Label." + _meta.Count.ToString()] = new SmartFormMeta.Element(SmartFormMeta.ElementType.Label, r.ReadString());
//                    return;
//                case "textBox":
//                case "textBox2":
//                    // <textbox name="" label="" type="" ... />
//                    var elementType = (r.LocalName == "textBox" ? SmartFormMeta.ElementType.TextBox : SmartFormMeta.ElementType.TextBox2);
//                    string fieldName = r.GetAttribute("name");
//                    string fieldLabel = r.GetAttribute("label");
//                    string fieldType = r.GetAttribute("type");
//                    int attributeCount = r.AttributeCount;
//                    if ((attributeCount >= 3) && (fieldName != null) && (fieldLabel != null) && (fieldType != null))
//                    {
//                        if ((fieldName.Length == 0) || (fieldLabel.Length == 0) || (fieldType.Length == 0))
//                        {
//                            MetaReadError(scopeKey, "Name, label and type are required: " + StringEx.Axb(scopeKey, CoreEx.Scope, fieldName));
//                            break;
//                        }
//                        Nattrib fieldAttrib;
//                        if (attributeCount > 3)
//                        {
//                            fieldAttrib = new Nattrib();
//                            //xmlReader.MoveToFirstAttribute();
//                            //do
//                            //{
//                            //    switch (xmlReader.LocalName)
//                            //    {
//                            //        case "name":
//                            //        case "label":
//                            //        case "type":
//                            //            continue;
//                            //    }
//                            //    fieldAttrib[xmlReader.LocalName] = xmlReader.Value;
//                            //} while (xmlReader.MoveToNextAttribute());
//                        }
//                        else
//                            fieldAttrib = null;
//                        string key = StringEx.Axb(scopeKey, CoreEx.Scope, fieldName);
//                        if (!_meta.ContainsKey(key))
//                            _meta[key] = new SmartFormMeta.Element(elementType, fieldLabel, fieldType, fieldAttrib);
//                        else
//                        {
//                            MetaReadError(scopeKey, "Duplicate key: " + key);
//                            return;
//                        }
//                    }
//                    else
//                    {
//                        MetaReadError(scopeKey, "Invalid type: " + scopeKey + CoreEx.Scope + r.LocalName);
//                        return;
//                    }
//                    return;
//            }
//            MetaReadError(scopeKey, "Invalid field type: " + scopeKey + CoreEx.Scope + r.LocalName);
//        }

//        /// <summary>
//        /// Loads the meta state information contained within the string provided by conversion to an XmlDocument and performing
//        /// a recursive load.
//        /// </summary>
//        /// <param name="metaState">State of the meta.</param>
//        public void ReadMetaState(string metaState)
//        {
//            if (string.IsNullOrEmpty(metaState))
//            {
//                _meta.Clear();
//                return;
//            }
//            using (var r = XmlReader.Create(new StringReader(metaState)))
//                ReadMetaState(r);
//        }
//        /// <summary>
//        /// Loads the meta state information contained within the string provided by conversion to an XmlDocument and performing
//        /// a recursive load.
//        /// </summary>
//        /// <param name="r">State of the meta.</param>
//        public void ReadMetaState(XmlReader r)
//        {
//            _meta.Clear();
//            if (r == null)
//                return;
//            // 1. find root element
//            r.ReadToFollowing("smartForm");
//            if (r.EOF)
//                return;
//            _xmlDepth = r.Depth;
//            // 2. parse document
//            ReadMetaStateRecurse(string.Empty, r);
//        }

//        /// <summary>
//        /// Reads the meta state recurse.
//        /// </summary>
//        /// <param name="scopeKey">The scope key.</param>
//        /// <param name="r">The XML reader.</param>
//        private void ReadMetaStateRecurse(string scopeKey, XmlReader r)
//        {
//            while ((r.Read()) && (r.Depth >= _xmlDepth))
//                if (r.LocalName == "unit")
//                    if (r.NodeType == XmlNodeType.Element)
//                    {
//                        string unitName;
//                        if (((unitName = r.GetAttribute("name")) == null) || (unitName.Length == 0))
//                        {
//                            MetaReadError(scopeKey, "Unit missing name attribute");
//                            continue;
//                        }
//                        string key = StringEx.Axb(scopeKey, CoreEx.Scope, unitName);
//                        _meta[key + CoreEx.Scope] = new SmartFormMeta.Element(SmartFormMeta.ElementType.Unit, r.GetAttribute("label"));
//                        ReadMetaStateRecurse(key, r);
//                    }
//                    else
//                        return;
//                else if (r.NodeType == XmlNodeType.Element)
//                    ReadMetaField(scopeKey, r);
//        }

//        /// <summary>
//        /// Loads the valueState information using the XmlTextPack instance associated with the underlying TextPackBase.
//        /// </summary>
//        /// <param name="valueState">State of the value.</param>
//        public void ReadValueState(string valueState)
//        {
//            ReadValueState(valueState, XmlTextPack.Instance);
//        }
//        /// <summary>
//        /// 	<summary>
//        /// Loads the valueState information using the TextPackBase provided.
//        /// </summary>
//        /// 	<param name="valueState">State of the value.</param>
//        /// </summary>
//        /// <param name="valueState">State of the value.</param>
//        /// <param name="textPack">The text pack.</param>
//        public void ReadValueState(string valueState, TextPackBase textPack)
//        {
//            if (textPack == null)
//                throw new ArgumentNullException("textPack");
//            _valueHash.Clear();
//            if (string.IsNullOrEmpty(valueState))
//                return;
//            textPack.PackDecode(valueState, _valueHash);
//        }

//        /// <summary>
//        /// Writes the meta field.
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <param name="element">The element.</param>
//        /// <param name="w">The XML writer.</param>
//        public void WriteMetaField(string name, SmartFormMeta.Element element, XmlWriter w)
//        {
//            var elementType = element.Type;
//            switch (elementType)
//            {
//                case SmartFormMeta.ElementType.Label:
//                    w.WriteElementString("label", element.Label);
//                    break;
//                case SmartFormMeta.ElementType.TextBox:
//                case SmartFormMeta.ElementType.TextBox2:
//                    w.WriteStartElement(elementType == SmartFormMeta.ElementType.TextBox ? "textBox" : "textBox2");
//                    w.WriteAttributeString("name", name);
//                    w.WriteAttributeString("label", element.Label);
//                    w.WriteAttributeString("type", element.FieldType);
//                    //var fieldAttrib = element.FieldAttrib;
//                    //if (fieldAttrib != null)
//                    //    foreach (string fieldAttribKey in fieldAttrib.Keys)
//                    //        xmlWriter.WriteAttributeString(fieldAttribKey, fieldAttrib[fieldAttribKey]);
//                    w.WriteEndElement();
//                    break;
//                case SmartFormMeta.ElementType.Error:
//                    w.WriteElementString("error", element.Label);
//                    break;
//            }
//        }

//        /// <summary>
//        /// Writes the state of the meta.
//        /// </summary>
//        /// <param name="w">The XML writer.</param>
//        public void WriteMetaState(XmlWriter w)
//        {
//            if (_meta.Count == 0)
//                return;
//            w.WriteStartElement("smartForm");
//            int metaKeyIndex = 0;
//            string[] metaKeys = new List<string>(_meta.Keys).ToArray();
//            WriteMetaStateRecurse(string.Empty, ref metaKeyIndex, metaKeys, w);
//            w.WriteEndElement();
//        }

//        /// <summary>
//        /// Writes the meta state recurse.
//        /// </summary>
//        /// <param name="scopeKey">The scope key.</param>
//        /// <param name="metaKeyIndex">Index of the meta key.</param>
//        /// <param name="metaKeys">The meta keys.</param>
//        /// <param name="w">The XML writer.</param>
//        private void WriteMetaStateRecurse(string scopeKey, ref int metaKeyIndex, string[] metaKeys, XmlWriter w)
//        {
//            while (metaKeyIndex < metaKeys.Length)
//            {
//                string metaKey = metaKeys[metaKeyIndex];
//                if (!metaKey.StartsWith(scopeKey))
//                    return;
//                int scopeKeyLength = scopeKey.Length;
//                int nextScopeIndex = metaKey.IndexOf(CoreEx.Scope, scopeKeyLength);
//                if (nextScopeIndex > -1)
//                {
//                    // add unit
//                    string scopeName = metaKey.Substring(scopeKeyLength, nextScopeIndex - scopeKeyLength);
//                    w.WriteStartElement("unit");
//                    w.WriteAttributeString("name", scopeName);
//                    var element = _meta[metaKey];
//                    if (element.Type == SmartFormMeta.ElementType.Unit)
//                    {
//                        w.WriteAttributeString("label", element.Label);
//                        metaKeyIndex++;
//                    }
//                    WriteMetaStateRecurse(scopeKey + scopeName + CoreEx.Scope, ref metaKeyIndex, metaKeys, w);
//                    w.WriteEndElement();
//                    continue;
//                }
//                // add leaf node
//                string name = metaKey.Substring(scopeKeyLength);
//                WriteMetaField(name, _meta[metaKey], w);
//                metaKeyIndex++;
//            }
//        }

//        /// <summary>
//        /// Writes the state of the value.
//        /// </summary>
//        /// <returns></returns>
//        public string WriteValueState()
//        {
//            return WriteValueState(System.Primitives.TextPacks.XmlTextPack.Instance);
//        }
//        /// <summary>
//        /// Writes the state of the value.
//        /// </summary>
//        /// <param name="textPack">The text pack.</param>
//        /// <returns></returns>
//        public string WriteValueState(TextPackBase textPack)
//        {
//            if (textPack == null)
//                throw new ArgumentNullException("textPack");
//            return (_valueHash.Count == 0 ? string.Empty : textPack.PackEncode(_valueHash));
//        }
//        #endregion

//        #region ReplaceTag
//        /// <summary>
//        /// Adds a replacement tag to the collection used by this instance of SmartForm.
//        /// </summary>
//        /// <param name="replaceTag">The replace tag.</param>
//        /// <param name="value">The value.</param>
//        public void AddReplaceTag(string replaceTag, string value)
//        {
//            if (string.IsNullOrEmpty(replaceTag))
//                throw new ArgumentNullException("key");
//            _replaceTagHash["[:" + replaceTag + ":]"] = (value ?? string.Empty);
//        }

//        /// <summary>
//        /// Clears the current collection of replacement tags in use.
//        /// </summary>
//        public void ClearReplaceTag()
//        {
//            _replaceTagHash.Clear();
//        }

//        /// <summary>
//        /// Creates the merged text resulting from applying all replacement tags against the string value associated with the key provided.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns></returns>
//        public string CreateMergedText(string key)
//        {
//            if (string.IsNullOrEmpty(key))
//                throw new ArgumentNullException("key");
//            string value;
//            if (!_valueHash.TryGetValue(key, out value))
//                return string.Empty;
//            if (value.Length > 0)
//                foreach (string replaceTagKey in _replaceTagHash.Keys)
//                    value = value.Replace(replaceTagKey, _replaceTagHash[replaceTagKey]);
//            return value;
//        }
//        #endregion
//    }
//}
