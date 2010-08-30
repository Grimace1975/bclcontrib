using System.Xml;
using System.Collections;
namespace System.Text
{
	/// <summary>
	/// Provides a basic façade pattern-oriented implementation facilitating building XML-structure strings. Provides common function implementations
	/// for appending and/or converting to valid XML structures.
	/// </summary>
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public class XmlTableFormatter : TableFormatterBase
	{
		private string _rootNode;
		private string _name;

		//- Main -//
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTableFormatter"/> class.
		/// </summary>
		public XmlTableFormatter()
			: base()
		{
			_rootNode = "root";
			_name = "item";
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTableFormatter"/> class.
		/// </summary>
		/// <param name="table">The table to preload.</param>
		public XmlTableFormatter(TableBase table)
			: base(table)
		{
			_rootNode = "root";
			_name = "item";
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTableFormatter"/> class.
		/// </summary>
		/// <param name="name">The name of the channel.</param>
		public XmlTableFormatter(string name)
			: base()
		{
			_rootNode = string.Empty;
			_name = name;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTableFormatter"/> class.
		/// </summary>
		/// <param name="rootNode">The root node to use.</param>
		/// <param name="name">The name of the channel.</param>
		public XmlTableFormatter(string rootNode, string name)
			: base()
		{
			_rootNode = rootNode;
			_name = name;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTableFormatter"/> class.
		/// </summary>
		/// <param name="rootNode">The root node to use.</param>
		/// <param name="name">The name of the channel.</param>
		/// <param name="table">The table to preload.</param>
		public XmlTableFormatter(string rootNode, string name, TableBase table)
			: base(table)
		{
			_rootNode = rootNode;
			_name = name;
		}

		/// <summary>
		/// Gets a string representation in xml format of the current element of the class.
		/// </summary>
		/// <returns></returns>
		public override string GetCurrentElement()
		{
			if (_isDirty == false)
			{
				return "<" + _name + " />";
			}
			var b = new StringBuilder("<" + _name);
			foreach (string valueKey in _valueHash.Keys)
			{
				string value = _valueHash[valueKey];
				if (value.Length > 0)
					b.Append(" " + valueKey + "=\"" + XmlNodeEx.XmlTextEncode(value) + "\"");
			}
			if ((_innerTextBuilder != null) && (_innerTextBuilder.Length > 0))
				return b.Append(">").Append(_innerTextBuilder).Append("</" + _name + ">").ToString();
			return b.Append(" />").ToString();
		}

		/// <summary>
		/// Gets or sets the name of the current element. Adds the element is setting this value.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get { return _name; }
			set
			{
				NewRow();
				_name = value;
			}
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		protected override string GetText(string text)
		{
			if (_rootNode.Length > 0)
				return (text.Length > 0 ? "<" + _rootNode + ">" + text + "</" + _rootNode + ">" : "<" + _rootNode + "/>");
			return text;
		}

		/// <summary>
		/// Gets or sets the root node.
		/// </summary>
		/// <value>The root node.</value>
		public string RootNode
		{
			get { return _rootNode; }
			set
			{
				_rootNode = value;
				Clear();
			}
		}
	}
}