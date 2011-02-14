using System.Collections;
namespace System.Text
{
	/// <summary>
	/// HtmlTableFormatter
	/// </summary>
	[CodeVersion(CodeVersionKind.Instinct, "1.0")]
	public class HtmlTableFormatter : TableFormatterBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTableFormatter"/> class.
		/// </summary>
		public HtmlTableFormatter()
			: base()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTableFormatter"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		public HtmlTableFormatter(TableBase table)
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