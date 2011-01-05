using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Primitives.DataTypes;
namespace System
{
	[TestClass]
	public class ParserExTests
	{
		[TestMethod]
		public void TryParseRanges()
		{
			ICollection<Range<int>> ranges;
			ParserEx.TryParseRanges("1 - 3, 5, 7 - 8", IntegerRangeDataType.Prime.RangeParser, (IntegerRangeDataType.RangeParserAttrib)null, out ranges);
		}
	}
}
