using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System
{
	[TestClass]
	public class DateTimeExtensionsTests
	{
		[TestMethod]
		public void DateDiff_Day()
		{
			int value = new DateTime(2009, 1, 1).DateDiff(DateTimeExtensions.DatePart.Day, new DateTime(2009, 1, 2));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void DateDiff_DayOfYear()
		{
			int value = new DateTime(2009, 1, 1).DateDiff(DateTimeExtensions.DatePart.DayOfYear, new DateTime(2009, 1, 2));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Hour()
		{
			int value = new DateTime(2009, 1, 1, 0, 0, 0).DateDiff(DateTimeExtensions.DatePart.Hour, new DateTime(2009, 1, 1, 1, 0, 0));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Millisecond()
		{
			int value = new DateTime(2009, 1, 1, 0, 0, 0, 0).DateDiff(DateTimeExtensions.DatePart.Millisecond, new DateTime(2009, 1, 1, 0, 0, 0, 1));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Minute()
		{
			int value = new DateTime(2009, 1, 1, 0, 0, 0).DateDiff(DateTimeExtensions.DatePart.Minute, new DateTime(2009, 1, 1, 0, 1, 0));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Month()
		{
			int value = new DateTime(2009, 1, 1).DateDiff(DateTimeExtensions.DatePart.Month, new DateTime(2009, 2, 1));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Quarter()
		{
			int value = new DateTime(2009, 1, 1).DateDiff(DateTimeExtensions.DatePart.Quarter, new DateTime(2009, 4, 1));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Second()
		{
			int value = new DateTime(2009, 1, 1, 0, 0, 0).DateDiff(DateTimeExtensions.DatePart.Second, new DateTime(2009, 1, 1, 0, 0, 1));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Week()
		{
			int value = new DateTime(2009, 1, 1).DateDiff(DateTimeExtensions.DatePart.Week, new DateTime(2009, 1, 7));
			Assert.AreEqual(1, value);
		}
		[TestMethod]
		public void DateDiff_Year()
		{
			int value = new DateTime(2009, 1, 1).DateDiff(DateTimeExtensions.DatePart.Year, new DateTime(2010, 1, 1));
			Assert.AreEqual(1, value);
		}

		[TestMethod]
		public void ShiftDate_EndOfMonth()
		{
			var value = new DateTime(2009, 1, 15).ShiftDate(DateTimeExtensions.ShiftDateMethod.EndOfMonth);
			Assert.AreEqual(new DateTime(2009, 1, 31), value);
		}
		[TestMethod]
		public void ShiftDate_FirstOfMonth()
		{
			var value = new DateTime(2009, 1, 15).ShiftDate(DateTimeExtensions.ShiftDateMethod.FirstOfMonth);
			Assert.AreEqual(new DateTime(2009, 1, 1), value);
		}
	}
}
