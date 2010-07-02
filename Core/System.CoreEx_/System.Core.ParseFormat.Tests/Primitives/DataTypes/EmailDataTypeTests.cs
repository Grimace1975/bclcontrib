using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.DataTypes
{
	[TestClass]
	public class EmailDataTypeTests
	{
		[TestMethod]
		public void PrimeFormat_NullAndNullFormat_IsNull()
		{
			Assert.IsNull(EmailDataType.Prime.Format(null, null));
		}

		[TestMethod]
		public void PrimeFormat_TestAndNullFormat_EqualsTest()
		{
			Assert.AreEqual("Test", EmailDataType.Prime.Format("Test", null));
		}

		[TestMethod]
		public void PrimeTryPase_NullOrStringEmptyAndNullFormat_IsFalse()
		{
			string value;
			Assert.IsFalse(EmailDataType.Prime.TryParse(null, null, out value));
		}

		[TestMethod]
		public void PrimeTryPase_VariousAndNullFormat_IsTrue()
		{
			string value;
			Assert.IsTrue(EmailDataType.Prime.TryParse("x@x", null, out value));
		}

		[TestMethod]
		public void PrimeTryPase_VariousAndNullFormat_IsFalse()
		{
			string value;
			Assert.IsFalse(EmailDataType.Prime.TryParse("x", null, out value));
		}

		[TestMethod]
		public void PrimeType_Valid_EqualsStringType()
		{
			Assert.AreSame(typeof(string), EmailDataType.Prime.Type);
		}

		#region Binding
		[TestMethod]
		public void Format_TestAndNullFormat_EqualsTest()
		{
			var formatter = new EmailDataType().Formatter;
			Assert.AreEqual("Test", formatter.Format("Test", null, null));
		}
		#endregion
	}
}
