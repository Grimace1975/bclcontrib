using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.DataTypes
{
	[TestClass]
	public class PhoneDataTypeTests
	{
		[TestMethod]
		public void PrimeFormat_NullAndNullFormat_IsNull()
		{
			Assert.IsNull(PhoneDataType.Prime.Format(null, null));
		}

		[TestMethod]
		public void PrimeFormat_TestAndNullFormat_EqualsTest()
		{
			Assert.AreEqual("Test", PhoneDataType.Prime.Format("Test", null));
		}

		[TestMethod]
		public void PrimeTryPase_NullOrStringEmptyAndNullFormat_IsFalse()
		{
			string value;
			Assert.IsFalse(PhoneDataType.Prime.TryParse(null, null, out value));
		}

		[TestMethod]
		public void PrimeTryPase_VariousAndNullFormat_IsTrue()
		{
			string value;
			Assert.IsTrue(PhoneDataType.Prime.TryParse("1234567", null, out value));
			Assert.IsTrue(PhoneDataType.Prime.TryParse("1234567890", null, out value));
			Assert.IsTrue(PhoneDataType.Prime.TryParse("12345678901", null, out value));
			Assert.IsTrue(PhoneDataType.Prime.TryParse("123-4567", null, out value));
			Assert.IsTrue(PhoneDataType.Prime.TryParse("(123) 456-7890", null, out value));
			Assert.IsTrue(PhoneDataType.Prime.TryParse("123-456-7890", null, out value));
			Assert.IsTrue(PhoneDataType.Prime.TryParse("123-4567890", null, out value));
		}

		[TestMethod]
		public void PrimeTryPase_VariousAndNullFormat_IsFalse()
		{
			string value;
			Assert.IsFalse(PhoneDataType.Prime.TryParse("x", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("1", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("12", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("123", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("1234", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("12345", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("123456", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("12345678", null, out value));
			Assert.IsFalse(PhoneDataType.Prime.TryParse("123456789", null, out value));
		}

		[TestMethod]
		public void PrimeTypeCode_Valid_EqualsTypeCodeString()
		{
			Assert.AreSame(typeof(string), PhoneDataType.Prime.Type);
		}

		#region Binding
		[TestMethod]
		public void Format_TestAndNullFormat_EqualsTest()
		{
			var formatter = new PhoneDataType().Formatter;
			Assert.AreEqual("Test", formatter.Format("Test", null, null));
		}
		#endregion
	}
}
