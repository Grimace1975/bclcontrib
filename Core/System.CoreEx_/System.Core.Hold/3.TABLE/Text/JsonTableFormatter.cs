using System.Collections;
namespace System.Text
{
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public class JsonTableFormatter : TableFormatterBase
	{
		private string _name;

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonTextTable"/> class.
		/// </summary>
		public JsonTableFormatter()
			: base()
		{
			_name = string.Empty;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonTextTable"/> class.
		/// </summary>
		/// <param name="table">The table to preload.</param>
		public JsonTableFormatter(TableBase table)
			: base(table)
		{
			_name = string.Empty;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonTextTable"/> class.
		/// </summary>
		/// <param name="name">The name of the channel.</param>
		public JsonTableFormatter(string name)
			: base()
		{
			_name = name;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonTextTable"/> class.
		/// </summary>
		/// <param name="name">The name of the channel.</param>
		/// <param name="table">The table to preload.</param>
		public JsonTableFormatter(string name, TableBase table)
			: base(table)
		{
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
				return string.Empty;
			}
			var b = new StringBuilder(", {");
			foreach (string valueKey in _valueHash.Keys)
			{
				string value = _valueHash[valueKey];
				if (value.Length > 0)
				{
					value = value.Replace("\"", "\\\"");
				}
				b.Append("\"" + valueKey + "\":\"" + value + "\", ");
			}
			if (b.Length > 0)
			{
				b.Length -= 2;
			}
			if ((_innerTextBuilder != null) && (_innerTextBuilder.Length > 0))
			{
				b.Append(_innerTextBuilder);
			}
			b.Append("}");
			return b.ToString();
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
				_name = value;
				Clear();
			}
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		protected override string GetText(string text)
		{
			if (text.Length > 0)
			{
				text = text.Substring(2);
			}
			if (_name.Length > 0)
			{
				return "{\"" + _name + "\":[" + text + "]}";
			}
			return "[" + text + "]";
		}
	}
}