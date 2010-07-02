using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Primitives.DataTypes;
namespace System
{
	[TestClass]
	public class FormatterExTests
	{
		/// <summary>
		/// 
		/// </summary>
		public class CustomType
		{
			public static bool TryParse(string value, out CustomType customType)
			{
				customType = new CustomType();
				return true;
			}
			public override string ToString()
			{
				return "CustomType";
			}
		}

		[TestMethod]
		public void FormatRanges()
		{
			var range = new[] {
				new Range<int> { BeginValue = 1, EndValue = 3 },
				new Range<int> { BeginValue = 5 },
				new Range<int> { BeginValue = 7, EndValue = 8 },
			};
			var rangeText = FormatterEx.FormatRanges(range, FormatterEx.Format<int>, (Nattrib)null);
			Assert.AreEqual("1 - 3, 5, 7 - 8", rangeText);
		}

		[TestMethod]
		public void Format_Object()
		{
			var boolText = FormatterEx.Format<bool>("true");
			var dateTimeText = FormatterEx.Format<DateTime>("1/1/2009");
			var decimalText = FormatterEx.Format<decimal>("1.0");
			var intText = FormatterEx.Format<int>("1");
			var stringText = FormatterEx.Format<string>((object)"text");
			var customText = FormatterEx.Format<CustomType>("1");
			//var dataTypeText = FormatterEx.Format<IntegerDataType>(1);
			Assert.AreEqual("Yes", boolText);
			Assert.AreEqual("1/1/2009 12:00 AM", dateTimeText);
			Assert.AreEqual("1.0000", decimalText);
			Assert.AreEqual("1", intText);
			Assert.AreEqual("text", stringText);
			Assert.AreEqual("CustomType", customText);
			//Assert.AreEqual("1", dataTypeText);
		}

		[TestMethod]
		public void Format_T()
		{
			var boolText = FormatterEx.Format<bool>(true);
			var dateTimeText = FormatterEx.Format<DateTime>(new DateTime(2009, 1, 1));
			var decimalText = FormatterEx.Format<decimal>(1M);
			var intText = FormatterEx.Format<int>(1);
			var stringText = FormatterEx.Format<string>("text");
			var customText = FormatterEx.Format<CustomType>(new CustomType());
			Assert.AreEqual("Yes", boolText);
			Assert.AreEqual("1/1/2009 12:00 AM", dateTimeText);
			Assert.AreEqual("1.0000", decimalText);
			Assert.AreEqual("1", intText);
			Assert.AreEqual("text", stringText);
			Assert.AreEqual("CustomType", customText);
		}
	}
}
