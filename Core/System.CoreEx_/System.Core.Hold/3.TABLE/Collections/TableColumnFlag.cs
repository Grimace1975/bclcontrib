namespace System.Collections
{
	/// <summary>
	/// TableColumnFlag
	/// </summary>
	[Flags]
	public enum TableColumnFlag
	{
		/// <summary>
		/// Normal
		/// </summary>
		Normal = 0x0,
		/// <summary>
		/// AutoFetch
		/// </summary>
		AutoFetch = 0x01,
		/// <summary>
		/// UseDefaultValue
		/// </summary>
		UseDefaultValue = 0x02 | AutoFetch,
		/// <summary>
		/// IdentityPrimaryKey
		/// </summary>
		IdentityPrimaryKey = 0x04 | PrimaryKey | AutoFetch,
		/// <summary>
		/// GuidPrimaryKey
		/// </summary>
		GuidPrimaryKey = 0x08 | PrimaryKey | AutoFetch,
		/// <summary>
		/// PrimaryKey
		/// </summary>
		PrimaryKey = 0x10,
		/// <summary>
		/// RowVersion
		/// </summary>
		RowVersion = 0x20 | AutoFetch | ReadOnly,
		/// <summary>
		/// ReadOnly
		/// </summary>
		ReadOnly = 0x40,
		/// <summary>
		/// Calculated
		/// </summary>
		Calculated = ReadOnly | AutoFetch,
		/// <summary>
		/// CommitExpression
		/// </summary>
		CommitExpression = 0x80 | AutoFetch,
	}
}
