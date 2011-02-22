namespace System.Data
{
    //TODO: Add Safe Field for refrence types ? returns default
    /// <summary>
    /// DataRowExtensions
    /// </summary>
	public static class DataRowExtensions
	{
		public static T Field<T>(this DataRow r, DataColumn column, T defaultValue)
		{
			if (r == null)
				throw new ArgumentNullException("r");
			return (!r.IsNull(column) ? r.Field<T>(column) : defaultValue);
		}

		public static T Field<T>(this DataRow r, int columnIndex, T defaultValue)
		{
			if (r == null)
				throw new ArgumentNullException("r");
			return (!r.IsNull(columnIndex) ? r.Field<T>(columnIndex) : defaultValue);
		}

		public static T Field<T>(this DataRow r, string columnName, T defaultValue)
		{
			if (r == null)
				throw new ArgumentNullException("r");
			return (!r.IsNull(columnName) ? r.Field<T>(columnName) : defaultValue);
		}
	}
}