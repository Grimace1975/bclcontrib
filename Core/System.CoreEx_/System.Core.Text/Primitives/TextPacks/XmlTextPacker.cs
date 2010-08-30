using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace System.Primitives.TextPacks
{
    /// <summary>
    /// <root key="value">
    ///    <sub key2="value2">text</sub>
    /// </root>
    /// Key = value {attrib on root}
    /// Sub:: = text {node in root}
    /// Sub::Key2 = value2 {attrib on node}
    /// </summary>

    public class XmlTextPack : TextPackBase
    {
        public static XmlTextPack Instance = new XmlTextPack();

        #region Class Types
        /// <summary>
        /// Context
        /// </summary>
        private class Context
        {
            public string Key;
            public string ScopeKey;
            public XmlElement XmlElement;
            public XmlDocument XmlDocument;

            /// <summary>
            /// Gets the context.
            /// </summary>
            /// <param name="pack">The pack.</param>
            /// <param name="contextKey">The context key.</param>
            /// <param name="context">The context.</param>
            /// <returns></returns>
            public static Context GetContext(string pack, string contextKey, ref object context)
            {
                if (contextKey == null)
                    throw new ArgumentNullException("contextKey");
                if (context == null)
                    context = new Context();
                var context2 = (Context)context;
                if (context2.Key == contextKey)
                    return context2;
                context2.Key = contextKey;
                var xmlDocument = new XmlDocument();
                if (pack.Length > 0)
                    xmlDocument.LoadXml(pack);
                context2.XmlDocument = xmlDocument;
                return context2;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTextPack"/> class.
        /// </summary>
        private XmlTextPack() { }

        /// <summary>
        /// Parses the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isValue">if set to <c>true</c> [is value].</param>
        /// <param name="scopeKey">The scope key.</param>
        /// <param name="itemKey">The item key.</param>
        private void ParseKey(string key, out bool isValue, out string scopeKey, out string itemKey)
        {
            // precalc key
            if (key.IndexOf(CoreEx.Scope) == -1)
                key = CoreEx.Scope + key;
            // parse key
            isValue = key.EndsWith(CoreEx.Scope);
            if (!isValue)
            {
                int scopeIndex = key.LastIndexOf(CoreEx.Scope);
                if (scopeIndex == 0)
                {
                    scopeKey = string.Empty;
                    itemKey = key.Substring(2);
                }
                else
                {
                    scopeIndex += 2;
                    scopeKey = key.Substring(0, scopeIndex);
                    itemKey = key.Substring(scopeIndex);
                }
            }
            else
            {
                scopeKey = key;
                itemKey = string.Empty;
            }
        }

        /// <summary>
        /// Encodes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private string EncodeKey(string key)
        {
            if (key.IndexOf(CoreEx.Scope) == -1)
                return "\x01" + key.Replace(CoreEx.Scope, "\x01") + "-";
            return key.Replace(CoreEx.Scope, "\x01") + "_";
        }

        /// <summary>
        /// Decodes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private string DecodeKey(string key)
        {
            if (key.EndsWith("-"))
                return key.Substring(1, key.Length - 2).Replace("\x01", CoreEx.Scope);
            return key.Substring(0, key.Length - 1).Replace("\x01", CoreEx.Scope);
        }

        /// <summary>
        /// Parses the encoded key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isValue">if set to <c>true</c> [is value].</param>
        /// <param name="scopeKey">The scope key.</param>
        /// <param name="itemKey">The item key.</param>
        private void ParseEncodedKey(string key, out bool isValue, out string scopeKey, out string itemKey)
        {
            // parse key
            int keyLength = key.Length;
            isValue = (key[keyLength - 2] == '\x01'); //- key.EndsWith("\x01_");
            if (!isValue)
            {
                int scopeIndex = key.LastIndexOf("\x01", keyLength - 2);
                if (scopeIndex == 0)
                {
                    scopeKey = string.Empty;
                    itemKey = key.Substring(1, keyLength - 2);
                }
                else
                {
                    scopeIndex += 1;
                    scopeKey = key.Substring(0, scopeIndex);
                    itemKey = key.Substring(scopeIndex, keyLength - scopeIndex - 1);
                }
            }
            else
            {
                scopeKey = key.Substring(0, keyLength - 1);
                itemKey = string.Empty;
            }
        }

        /// <summary>
        /// Gets the value associated with the specified key out of the packed representation provided.
        /// </summary>
        /// <param name="pack">The packed representation of the data</param>
        /// <param name="key">The key to use</param>
        /// <param name="contextKey">The context key to use in looking for a preparsed collection.</param>
        /// <param name="context">The context object to use in looking for a preparsed collection.</param>
        /// <returns></returns>
        public override string GetValue(string pack, string key, string contextKey, ref object context)
        {
            if (string.IsNullOrEmpty(pack))
                return string.Empty;
            // context
            var context2 = Context.GetContext(pack, contextKey, ref context);
            var xmlDocument = context2.XmlDocument;
            // parse key
            bool isValue;
            string scopeKey;
            string itemKey;
            ParseKey(key, out isValue, out scopeKey, out itemKey);
            // xmlelement
            XmlElement xmlElement;
            if (context2.ScopeKey != scopeKey)
            {
                context2.ScopeKey = scopeKey;
                // find element
                xmlElement = xmlDocument.DocumentElement;
                if (xmlElement == null)
                {
                    context2.XmlElement = null;
                    return string.Empty;
                }
                string xpath = "/" + xmlElement.Name;
                if (scopeKey.Length > 0)
                    // singhj: scopeKey doesn't include trailing "::"
                    xpath += "/" + scopeKey.Replace(CoreEx.Scope, "/"); // was: xpath += "/" + scopeKey.Substring(0, scopeKey.Length - 2).Replace(KernelText.Scope, "/");
                xmlElement = (xmlDocument.SelectSingleNode(xpath) as XmlElement);
                context2.XmlElement = xmlElement;
            }
            else
                xmlElement = context2.XmlElement;
            // get value
            return (xmlElement != null ? (!isValue ? xmlElement.GetAttribute(itemKey) : xmlElement.InnerText) : string.Empty);
        }

        /// <summary>
        /// Sets the value within the packed string specified that is associated with the key provided.
        /// </summary>
        /// <param name="pack">The packed string to inspect.</param>
        /// <param name="key">The key to use</param>
        /// <param name="value">The value to set</param>
        /// <param name="contextKey">The context key to use in storing the result.</param>
        /// <param name="context">The context object to use in storing the result.</param>
        /// <returns>The packed format of the value</returns>
        public override string SetValue(string pack, string key, string value, string contextKey, ref object context)
        {
            // context
            var context2 = Context.GetContext(pack, contextKey, ref context);
            var xmlDocument = context2.XmlDocument;
            // parse key
            bool isValue;
            string scopeKey;
            string itemKey;
            ParseKey(key, out isValue, out scopeKey, out itemKey);
            // xmlelement
            XmlElement xmlElement;
            if (context2.ScopeKey != scopeKey)
            {
                context2.ScopeKey = scopeKey;
                // find element
                xmlElement = xmlDocument.DocumentElement;
                if (xmlElement == null)
                {
                    xmlElement = xmlDocument.CreateElement("root");
                    xmlDocument.AppendChild(xmlElement);
                }
                if (scopeKey.Length > 0)
                {
                    // singhj: scopeKey doesn't include trailing "::"
                    string[] scopeKeyArray = scopeKey.Split(new string[] { CoreEx.Scope }, StringSplitOptions.None); //was: string[] scopeKeyArray = scopeKey.Substring(0, scopeKey.Length - 2).Split(new string[] { KernelText.Scope }, StringSplitOptions.None);
                    foreach (string scopeKey2 in scopeKeyArray)
                    {
                        var xmlElement2 = xmlElement[scopeKey2];
                        if (xmlElement2 == null)
                        {
                            xmlElement2 = xmlDocument.CreateElement(scopeKey2);
                            xmlElement.AppendChild(xmlElement2);
                        }
                        xmlElement = xmlElement2;
                    }
                }
                context2.XmlElement = xmlElement;
            }
            else
                xmlElement = context2.XmlElement;
            // set value
            if (!isValue)
                xmlElement.SetAttribute(itemKey, value);
            else
                xmlElement.Value = value;
            return xmlDocument.InnerXml;
        }

        #region CODEC
        /// <summary>
        /// Provides the ability to decode the contents of the pack provided into the hash instance provided, based on the logic
        /// provided by <see cref="M:PackDecodeRecurse">PackDecodeRecurse</see>. The result is contained in the hash provided.
        /// </summary>
        /// <param name="pack">The packed string to process.</param>
        /// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
        /// <param name="hash">The hash containing the contents to pack</param>
        /// <param name="validKeyIndex">Collection of valid keys used to filter the packed items.</param>
        public override void PackDecode(string pack, string namespaceKey, IDictionary<string, string> set, IDictionary<string, string> validKeyIndex)
        {
            if (string.IsNullOrEmpty(pack))
                return;
            var r = XmlReader.Create(new StringReader(pack));
            if (r.IsStartElement())
                PackDecodeRecurse(string.Empty, r, (namespaceKey ?? string.Empty), set, validKeyIndex);
            r.Close();
        }

        /// <summary>
        /// Packs the decode recurse.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <param name="xmlReader">The XML reader.</param>
        /// <param name="namespaceKey">The namespace key.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="validKeyIndex">Index of the valid key.</param>
        private static void PackDecodeRecurse(string scope, XmlReader r, string namespaceKey, IDictionary<string, string> set, IDictionary<string, string> validKeyIndex)
        {
            bool isInNamespace = scope.StartsWith(namespaceKey, StringComparison.OrdinalIgnoreCase);
            if (isInNamespace)
            {
                // parse attributes
                if (r.HasAttributes)
                {
                    while (r.MoveToNextAttribute())
                    {
                        string key = scope + r.Name;
                        // check validkeyindex and commit
                        if ((validKeyIndex == null) || (validKeyIndex.ContainsKey(key)))
                            set[scope + r.Name] = r.Value;
                    }
                    // move the reader back to the element node.
                    r.MoveToElement();
                }
            }
            if (!r.IsEmptyElement)
            {
                // read the start tag.
                r.Read();
                bool isRead = true;
                while (isRead)
                    switch (r.MoveToContent())
                    {
                        case System.Xml.XmlNodeType.CDATA:
                        case System.Xml.XmlNodeType.Text:
                            if (isInNamespace)
                            {
                                string key = (scope.Length > 0 ? scope : CoreEx.Scope);
                                // check validkeyindex and commit
                                if ((validKeyIndex == null) || (validKeyIndex.ContainsKey(key)))
                                    set[key] = r.Value;
                            }
                            r.Read();
                            break;
                        case System.Xml.XmlNodeType.Element:
                            // handle nested elements.
                            if (r.IsStartElement())
                            {
                                PackDecodeRecurse(scope + r.Name + CoreEx.Scope, r, namespaceKey, set, validKeyIndex);
                                r.Read();
                            }
                            break;
                        default:
                            isRead = false;
                            break;
                    }
            }
        }

        /// <summary>
        /// Provides the ability to pack the contents in the hash provided into a different string representation.
        /// Results is contained in the provided StringBuilder instance.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
        /// <param name="textBuilder">The text builder.</param>
        /// <param name="validKeyIndex">Index of the valid key.</param>
        public override void PackEncode(IDictionary<string, string> set, string namespaceKey, StringBuilder b, IDictionary<string, string> validKeyIndex)
        {
            if ((set == null) || (set.Count == 0))
                return;
            // precalc keys
            // pull keys from existing hash and validate against provided IDictionary, and encode into tree key structure
            // field is prepended with identifyer for unencoding at the end to find original key
            var keyList = new List<string>((ICollection<string>)set.Keys);
            for (int keyListIndex = keyList.Count - 1; keyListIndex >= 0; keyListIndex--)
            {
                string key = keyList[keyListIndex];
                // check for validkeyindex
                if ((validKeyIndex != null) && (!validKeyIndex.ContainsKey(key)))
                {
                    keyList.RemoveAt(keyListIndex);
                    continue;
                }
                // encode key
                keyList[keyListIndex] = EncodeKey(key);
            }
            keyList.Sort(0, keyList.Count, StringComparer.OrdinalIgnoreCase);
            //
            var xmlWriter = XmlTextWriter.Create(b);
            xmlWriter.WriteStartElement("root");
            //
            string lastScopeKey = string.Empty;
            string elementValue = null;
            foreach (string key in keyList)
            {
                // parse encoded key
                bool isValue;
                string scopeKey;
                string itemKey;
                ParseEncodedKey(key, out isValue, out scopeKey, out itemKey);
                // process element
                if ((scopeKey.Length > 1) && (lastScopeKey != scopeKey))
                {
                    // write latched value
                    if (elementValue != null)
                    {
                        xmlWriter.WriteString(elementValue);
                        elementValue = null;
                    }
                    // element
                    if (scopeKey.StartsWith(lastScopeKey))
                    {
                        // start elements
                        int lastScopeKeyLength = lastScopeKey.Length;
                        string[] createScopeKeyArray = scopeKey.Substring(lastScopeKeyLength, scopeKey.Length - lastScopeKeyLength - 1).Split('\x01');
                        foreach (string createScopeKey in createScopeKeyArray)
                            xmlWriter.WriteStartElement(createScopeKey);
                    }
                    else
                    {
                        // end and start elements
                        string[] lastScopeKeyArray = lastScopeKey.Substring(0, lastScopeKey.Length - 1).Split('\x01');
                        string[] scopeKeyArray = scopeKey.Substring(0, scopeKey.Length - 1).Split('\x01');
                        int scopeKeyArrayLength = scopeKeyArray.Length;
                        // skip existing elements
                        int index;
                        for (index = 0; index < lastScopeKeyArray.Length; index++)
                            if ((index >= scopeKeyArrayLength) || (scopeKeyArray[index] != lastScopeKeyArray[index]))
                                break;
                        // end elements
                        for (int lastScopeKeyIndex = lastScopeKeyArray.Length - 1; lastScopeKeyIndex >= index; lastScopeKeyIndex--)
                            xmlWriter.WriteEndElement(); //-lastScopeKeyArray[lastScopeKeyIndex]
                        // start elements
                        for (int scopeKeyIndex = index; scopeKeyIndex < scopeKeyArray.Length; scopeKeyIndex++)
                            xmlWriter.WriteStartElement(scopeKeyArray[scopeKeyIndex]);
                    }
                    lastScopeKey = scopeKey;
                }
                // decode key and set value
                string value = set[DecodeKey(key)];
                if (!isValue)
                    xmlWriter.WriteAttributeString(itemKey, (value ?? string.Empty));
                else
                    if (!string.IsNullOrEmpty(value))
                        elementValue = value;
            }
            // overflow close
            // write latched value
            if (elementValue != null)
                xmlWriter.WriteString(elementValue);
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
        #endregion
    }
}
