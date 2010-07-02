using System.Collections.Generic;
using System.Text;
namespace System.Primitives.TextPacks
{
    /// <summary>
    /// A subclass of TextPackBase used to provide a basic encoding and decoding function-set.
    /// </summary>

    public class SimpleTextPack : TextPackBase
    {
        public static readonly SimpleTextPack Instance = new SimpleTextPack();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleTextPack"/> class.
        /// </summary>
        private SimpleTextPack() { }

        /// <summary>
        /// Gets the value associated with the specified key out of the packed representation provided.
        /// </summary>
        /// <param name="pack">The packed representation of the data</param>
        /// <param name="key">The key to use</param>
        /// <param name="contextKey">The context key to use in looking for a preparsed collection.</param>
        /// <param name="context">The context object to use in looking for a preparsed collection.</param>
        /// <returns>
        /// The value retrieved from the packed string.
        /// </returns>
        public override string GetValue(string pack, string key, string contextKey, ref object context)
        {
            pack += "\x01";
            string packKey = "\x01" + key + "=";
            int packIndex = pack.IndexOf(packKey, StringComparison.OrdinalIgnoreCase);
            if (packIndex > -1)
            {
                packIndex += packKey.Length;
                return pack.Substring(packIndex, pack.IndexOf("\x01", packIndex) - packIndex).Replace("\xDE\xDE", "\x01");
            }
            return string.Empty;
        }

        /// <summary>
        /// Sets the value within the packed string specified that is associated with the key provided.
        /// The packing format uses "\x01[key]=[value]\x01".
        /// </summary>
        /// <param name="pack">The packed string to inspect.</param>
        /// <param name="key">The key to use</param>
        /// <param name="value">The value to set</param>
        /// <param name="contextKey">The context key to use in storing the result.</param>
        /// <param name="context">The context object to use in storing the result.</param>
        /// <returns>The packed format of the value</returns>
        public override string SetValue(string pack, string key, string value, string contextKey, ref object context)
        {
            if (value != null)
                value = value.Replace("\x01", "\xDE\xDE");
            pack += "\x01";
            string packKey = "\x01" + key + "=";
            int packIndex = pack.IndexOf(packKey, StringComparison.OrdinalIgnoreCase);
            if (packIndex > -1)
                pack = pack.Replace(pack.Substring(packIndex, pack.IndexOf("\x01", packIndex + 1) - packIndex), string.Empty);
            return pack + key + "=" + value;
        }

        #region Codec
        /// <summary>
        /// Provides the ability to decode the contents of the pack provided into the hash instance provided, based on the logic
        /// provided by the implementating class. The result is contained in the hash provided.
        /// </summary>
        /// <param name="pack">The packed string to process.</param>
        /// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
        /// <param name="hash">The hash containing the contents to pack</param>
        /// <param name="validKeyIndex">Collection of valid keys used to filter the packed items.</param>
        public override void PackDecode(string pack, string namespaceKey, IDictionary<string, string> set, IDictionary<string, string> validKeyIndex)
        {
            if (string.IsNullOrEmpty(pack))
                return;
            if (namespaceKey == null)
                namespaceKey = string.Empty;
            string searchKey = (namespaceKey.Length == 0 ? "\x01" : "\x01" + namespaceKey + "::");
            int searchKeyLength = searchKey.Length;
            int packIndex = 0;
            do
            {
                packIndex = pack.IndexOf(searchKey, packIndex, StringComparison.OrdinalIgnoreCase);
                if (packIndex > -1)
                {
                    packIndex += searchKeyLength;
                    int packIndex2 = pack.IndexOf("=", packIndex);
                    int packIndex3 = pack.IndexOf("\x01", packIndex);
                    string key = pack.Substring(packIndex, packIndex2 - packIndex);
                    packIndex = packIndex3;
                    // check validkeyindex and commit
                    if ((validKeyIndex == null) || (validKeyIndex.ContainsKey(key)))
                        set[key] = (packIndex3 > -1 ? pack.Substring(packIndex2 + 1, packIndex3 - packIndex2 - 1) : pack.Substring(packIndex2 + 1)).Replace("\xDE\xDE", "\x01");
                }
            } while (packIndex > -1);
        }

        /// <summary>
        /// Provides the ability to pack the contents in the hash provided into a different string representation based on the logic
        /// provided by the implementating class. Results is contained in the provided StringBuilder instance.
        /// The packing format uses "\x01[key]=[value]\x01".
        /// </summary>
        /// <param name="hash">The hash containing the contents to pack</param>
        /// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
        /// <param name="textBuilder">The text builder used to build the packed result.</param>
        /// <param name="validKeyIndex">Collection of valid keys used to filter the packed items.</param>
        public override void PackEncode(IDictionary<string, string> set, string namespaceKey, StringBuilder textBuilder, IDictionary<string, string> validKeyIndex)
        {
            if ((set == null) || (set.Count == 0))
                return;
            // check validkeyindex and commit
            foreach (string key in set.Keys)
                if ((validKeyIndex == null) || (validKeyIndex.ContainsKey(key)))
                {
                    textBuilder.Append("\x01" + key + "=");
                    textBuilder.Append(set[key].Replace("\x01", "\xDE\xDE"));
                }
        }
        #endregion

        #region PACKTEXT
        /// <summary>
        /// Creates the pack text representation using  the format uses "[text]\x01".
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string CreatePackText(string text)
        {
            return text + "\x01";
        }

        /// <summary>
        /// Gets the packed value associated with the specified key.
        /// </summary>
        /// <param name="pack">The packed string to use.</param>
        /// <param name="key">The key to find.</param>
        /// <returns></returns>
        public static string GetPackTextValue(string pack, string key)
        {
            string packKey = "\x01" + key + "=";
            int packIndex = pack.IndexOf(packKey, StringComparison.OrdinalIgnoreCase);
            if (packIndex > -1)
            {
                packIndex += packKey.Length;
                return pack.Substring(packIndex, pack.IndexOf("\x01", packIndex) - packIndex).Replace("\xDE\xDE", "\x01");
            }
            return string.Empty;
        }

        /// <summary>
        /// Sets or replaces the value in the packed string using the key and value provided.
        /// </summary>
        /// <param name="pack">The pack.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string SetPackTextValue(string pack, string key, string value)
        {
            if (value != null)
                value = value.Replace("\x01", "\xDE\xDE");
            string packKey = "\x01" + key + "=";
            int packIndex = pack.IndexOf(packKey, StringComparison.OrdinalIgnoreCase);
            if (packIndex > -1)
                pack = pack.Replace(pack.Substring(packIndex, pack.IndexOf("\x01", packIndex + 1) - packIndex), string.Empty);
            return pack + key + "=" + value + "\x01";
        }

        /// <summary>
        /// Deletes the value from the packed string if found.
        /// </summary>
        /// <param name="pack">The pack.</param>
        /// <returns></returns>
        public static string DeletePackText(string pack)
        {
            return (pack.Length > 1 ? pack.Substring(0, pack.Length - 1) : string.Empty);
        }
        #endregion
    }
}
