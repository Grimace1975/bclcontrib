using System.Collections;
using System.Collections.Generic;
namespace System.Text
{
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public class TextTableFormatter : TableFormatterBase
	{
		private Dictionary<string, TextColumn> _columnHash = new Dictionary<string, TextColumn>();

		/// <summary>
		/// Initializes a new instance of the <see cref="TextTableFormatter"/> class.
		/// </summary>
		public TextTableFormatter()
			: base()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextTableFormatter"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		public TextTableFormatter(TableBase table)
			: base(table)
		{
		}

		/// <summary>
		/// Adds the column.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="name">The name.</param>
		public void AddColumn(string key, string name)
		{
			TextColumn textColumn;
			if (_columnHash.TryGetValue(key, out textColumn) == false)
			{
				_columnHash[key] = new TextColumn(name);
			}
			else
			{
				textColumn.Name = name;
			}
		}

		/// <summary>
		/// Gets a string representation in xml format of the current element of the class.
		/// </summary>
		/// <returns></returns>
		public override string GetCurrentElement()
		{
			return string.Empty;
		}

		#region TEXTCOLUMN
		/// <summary>
		/// TextColumn
		/// </summary>
		internal class TextColumn
		{
			private string m_name;
			private int m_maxLength;

			/// <summary>
			/// Initializes a new instance of the <see cref="TextColumn"/> class.
			/// </summary>
			/// <param name="name">The name.</param>
			public TextColumn(string name)
			{
				m_name = name;
				m_maxLength = name.Length;
			}

			/// <summary>
			/// Checks the length.
			/// </summary>
			/// <param name="length">The length.</param>
			public void CheckLength(int length)
			{
				if (length > m_maxLength)
				{
					m_maxLength = length;
				}
			}

			/// <summary>
			/// Gets or sets the name.
			/// </summary>
			/// <value>The name.</value>
			public string Name
			{
				get { return m_name; }
				set
				{
					m_name = value;
					int length = m_name.Length;
					if (length > m_maxLength)
					{
						m_maxLength = length;
					}
				}
			}

			/// <summary>
			/// Gets or sets the length of the max.
			/// </summary>
			/// <value>The length of the max.</value>
			public int MaxLength
			{
				get { return m_maxLength; }
				set { m_maxLength = value; }
			}
		}
		#endregion TEXTCOLUMN
	}
}