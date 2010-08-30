using System.Collections.Generic;
using System.Text;
namespace System.Primitives.TextPacks
{
	/// <summary>
	/// Abstract base class that defines the core feature-set for encoding and decoding string data
	/// into and out of specific string representations defined by the implementation logic
	/// provided by the subclass.
	/// </summary>
	public interface ITextPack
	{
		string GetValue(string pack, string key, string contextKey, ref object context);
		void PackDecode(string pack, string namespaceKey, IDictionary<string, string> set, IDictionary<string, string> validKeyIndex);
		void PackEncode(IDictionary<string, string> set, string namespaceKey, StringBuilder b, IDictionary<string, string> validKeyIndex);
		string SetValue(string pack, string key, string value, string contextKey, ref object context);
	}
}
