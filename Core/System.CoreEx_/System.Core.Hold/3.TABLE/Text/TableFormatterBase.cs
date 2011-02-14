using System.Collections;
using System.Collections.Generic;
namespace System.Text
{
	/// <summary>
	/// TableFormatterBase
	/// </summary>
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public abstract class TableFormatterBase
	{
		protected bool _isDirty = false;
		protected Dictionary<string, string> _valueHash = new Dictionary<string, string>();
		protected StringBuilder _textBuilder = new StringBuilder();
		protected StringBuilder _innerTextBuilder;

		/// <summary>
		/// Initializes a new instance of the <see cref="TextTableBase"/> class.
		/// </summary>
		protected TableFormatterBase()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextTableBase"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		protected TableFormatterBase(TableBase table)
		{
			var keys = table.Keys;
			while (table.Read())
			{
				NewRow();
				foreach (string key in keys)
					this[key] = table[key].ToString();
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="System.String"/> value associated with the specified key.
		/// </summary>
		/// <value></value>
		public string this[string key]
		{
			get { return _valueHash[key]; }
			set
			{
				_isDirty = true;
				_valueHash[key] = value;
			}
		}

		/// <summary>
		/// Adds a string representation of the current element to the underlying instance of StringBuilder used to maintain the
		/// text stream that represents the xml document being built.
		/// </summary>
		public void NewRow()
		{
			if (!_isDirty)
				return;
			_textBuilder.Append(GetCurrentElement());
			if (_innerTextBuilder != null)
				_innerTextBuilder.Length = 0;
			_valueHash.Clear();
			_isDirty = false;
		}

		/// <summary>
		/// Appends text to the end of the text body.
		/// </summary>
		/// <param name="text">The text.</param>
		public virtual void AppendInnerText(string text)
		{
			if (_innerTextBuilder == null)
				_innerTextBuilder = new StringBuilder();
			_innerTextBuilder.Append(text);
		}

		/// <summary>
		/// Clears this <see cref="XmlBuilder"/> instance.
		/// </summary>
		public virtual void Clear()
		{
			_textBuilder.Length = 0;
			if (_innerTextBuilder != null)
				_innerTextBuilder.Length = 0;
			_valueHash.Clear();
			_isDirty = false;
		}

		/// <summary>
		/// Gets a string representation in xml format of the current element of the class.
		/// </summary>
		/// <returns></returns>
		public abstract string GetCurrentElement();

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		protected virtual string GetText(string text)
		{
			return text;
		}

		/// <summary>
		/// Returns a string of Xml that representats the current xml built so far, plus the current element, and closing off
		/// the root node closing tag.
		/// </summary>
		/// <returns></returns>
		public virtual string Peek()
		{
			string text = _textBuilder.ToString();
			if (_isDirty)
				text += GetCurrentElement();
			return GetText(text);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public new virtual string ToString()
		{
			NewRow();
			string text = _textBuilder.ToString();
			Clear();
			return GetText(text);
		}
	}
}