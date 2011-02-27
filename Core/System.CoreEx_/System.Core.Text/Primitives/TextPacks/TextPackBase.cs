using System.Collections.Generic;
using System.Text;
namespace System.Primitives.TextPacks
{
	/// <summary>
	/// Abstract base class that defines the core feature-set for encoding and decoding string data
	/// into and out of specific string representations defined by the implementation logic
	/// provided by the subclass.
	/// </summary>
    public abstract class TextPackBase : ITextPack
	{
		/// <summary>
		/// Gets the value associated with the specified key out of the packed representation provided.
		/// </summary>
		/// <param name="pack">The packed representation of the data</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public string GetValue(string pack, string key)
		{
			object context = null;
			return GetValue(pack, key, string.Empty, ref context);
		}
		/// <summary>
		/// Gets the value associated with the specified key out of the packed representation provided.
		/// </summary>
		/// <param name="pack">The packed representation of the data</param>
		/// <param name="key">The key to use</param>
		/// <param name="contextKey">The context key to use in looking for a preparsed collection.</param>
		/// <param name="context">The context object to use in looking for a preparsed collection.</param>
		/// <returns></returns>
		public abstract string GetValue(string pack, string key, string contextKey, ref object context);

		/// <summary>
		/// Provides the ability to decode the contents of the pack provided into the hash instance provided, based on the logic
		/// provided by the implementating class. The result is contained in the hash instance returned.
		/// </summary>
		/// <param name="pack">The packed string to process.</param>
		public IDictionary<string, string> PackDecode(string pack)
		{
			var set = new Dictionary<string, string>();
			PackDecode(pack, null, set, null);
			return set;
		}
		/// <summary>
		/// Provides the ability to decode the contents of the pack provided into the hash instance provided, based on the logic
		/// provided by the implementating class. The result is contained in the hash instance returned.
		/// </summary>
		/// <param name="pack">The packed string to process.</param>
		/// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
		public IDictionary<string, string> PackDecode(string pack, string namespaceKey)
		{
			var set = new Dictionary<string, string>();
			PackDecode(pack, namespaceKey, set, null);
			return set;
		}
		/// <summary>
		/// Provides the ability to decode the contents of the pack provided into the hash instance provided, based on the logic
		/// provided by the implementating class. The result is contained in the hash provided.
		/// </summary>
		/// <param name="pack">The packed string to process.</param>
		/// <param name="hash">The hash containing the contents to pack</param>
		public void PackDecode(string pack, IDictionary<string, string> set)
		{
			if (pack == null)
				return;
			PackDecode(pack, null, set, null);
		}
		/// <summary>
		/// Provides the ability to decode the contents of the pack provided into the hash instance provided, based on the logic
		/// provided by the implementating class. The result is contained in the hash provided.
		/// </summary>
		/// <param name="pack">The packed string to process.</param>
		/// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
		/// <param name="hash">The hash containing the contents to pack</param>
		/// <param name="validKeyIndex">Collection of valid keys used to filter the packed items.</param>
		public abstract void PackDecode(string pack, string namespaceKey, IDictionary<string, string> set, IDictionary<string, string> validKeyIndex);

		/// <summary>
		/// Provides the ability to pack the contents in the hash provided into a different string representation based on the logic
		/// provided by the implementating class.  Result of the packing is the return value of the method.
		/// </summary>
		/// <param name="hash">The hash containing the contents to pack</param>
		public string PackEncode(IDictionary<string, string> hash)
		{
			var b = new StringBuilder();
			PackEncode(hash, null, b, null);
			return b.ToString();
		}
		/// <summary>
		/// Provides the ability to pack the contents in the hash provided into a different string representation based on the logic
		/// provided by the implementating class. Result of the packing is the return value of the method.
		/// </summary>
		/// <param name="hash">The hash containing the contents to pack</param>
		/// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
		public string PackEncode(IDictionary<string, string> set, string namespaceKey)
		{
			var b = new StringBuilder();
			PackEncode(set, namespaceKey, b, null);
			return b.ToString();
		}
		/// <summary>
		/// Provides the ability to pack the contents in the hash provided into a different string representation based on the logic
		/// provided by the implementating class. Results is contained in the provided StringBuilder instance.
		/// </summary>
		/// <param name="hash">The hash containing the contents to pack</param>
		/// <param name="namespaceKey">The namespace key to use for qualify keys.</param>
		/// <param name="textBuilder">The text builder used to build the packed result.</param>
		/// <param name="validKeyIndex">Collection of valid keys used to filter the packed items.</param>
		public abstract void PackEncode(IDictionary<string, string> set, string namespaceKey, StringBuilder b, IDictionary<string, string> validKeyIndex);

		/// <summary>
		/// Sets the value within the packed string specified that is associated with the key provided.
		/// </summary>
		/// <param name="pack">The packed string to inspect.</param>
		/// <param name="key">The key to use</param>
		/// <param name="value">The value to set</param>
		/// <returns>The packed format of the value</returns>
		public string SetValue(string pack, string key, string value)
		{
			object context = null;
			return SetValue(pack, key, value, string.Empty, ref context);
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
		public abstract string SetValue(string pack, string key, string value, string contextKey, ref object context);
	}
}
