using System.Collections;
namespace System.Text
{
	/// <summary>
	/// CsvTableFormatter
	/// </summary>
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public class CsvTableFormatter : TableFormatterBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvTableFormatter"/> class.
		/// </summary>
		public CsvTableFormatter()
			: base()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvTableFormatter"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		public CsvTableFormatter(TableBase table)
			: base(table)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a string representation in xml format of the current element of the class.
		/// </summary>
		/// <returns></returns>
		public override string GetCurrentElement()
		{
			return string.Empty;
		}
	}
}