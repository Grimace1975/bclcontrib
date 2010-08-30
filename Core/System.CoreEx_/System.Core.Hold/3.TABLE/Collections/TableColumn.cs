namespace System.Collections
{
	/// <summary>
	/// TableColumn
	/// </summary>
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public class TableColumn
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TableColumn"/> class.
		/// </summary>
		public TableColumn()
			: this(TypeCode.Empty, -1, TableColumnFlag.Normal) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="TableColumn"/> class.
		/// </summary>
		/// <param name="typeCode">The type code.</param>
		public TableColumn(System.TypeCode typeCode)
			: this(typeCode, -1, TableColumnFlag.Normal) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="TableColumn"/> class.
		/// </summary>
		/// <param name="typeCode">The type code.</param>
		/// <param name="maxLength">Length of the max.</param>
		/// <param name="columnFlag">The column flag.</param>
		public TableColumn(TypeCode typeCode, int maxLength, TableColumnFlag columnFlag)
		{
			TypeCode = typeCode;
			MaxLength = maxLength;
			ColumnFlag = columnFlag;
		}

		/// <summary>
		/// Gets or sets the length of the max.
		/// </summary>
		/// <value>The length of the max.</value>
		public int MaxLength { get; set; }

		/// <summary>
		/// Gets or sets the type code.
		/// </summary>
		/// <value>The type code.</value>
		public TypeCode TypeCode { get; set; }

		/// <summary>
		/// Gets or sets the column flag.
		/// </summary>
		/// <value>The column flag.</value>
		public TableColumnFlag ColumnFlag { get; set; }
	}
}