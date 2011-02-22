using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System
{
	[TestClass]
	public class EnumExTests
	{
		public enum TestEnum
		{
			[EnumName("With Attrib")]
			WithAttrib = 10010,
			WithoutAttrib = 10020,
		}

		[TestMethod]
		public void ToString_OutOfRange_IsNull()
		{
			string name = EnumEx.ToString(typeof(TestEnum), (TestEnum)3);
			Assert.AreEqual("3", name);
		}

		[TestMethod]
		public void ToStringTEnum_OutOfRange_IsNull()
		{
			string name = EnumEx.ToString<TestEnum>((TestEnum)3);
			Assert.AreEqual("3", name);
		}

		[TestMethod]
		public void ToString_WithAttrib_EqualsFirstName()
		{
			string name = EnumEx.ToString(typeof(TestEnum), TestEnum.WithAttrib);
			Assert.AreEqual("WithAttrib", name);
		}

		[TestMethod]
		public void ToStringTEnum_WithAttrib_EqualsFirstName()
		{
			string name = EnumEx.ToString<TestEnum>(TestEnum.WithAttrib);
			Assert.AreEqual("WithAttrib", name);
		}

		[TestMethod]
		public void ToName_OutOfRange_IsNull()
		{
			string name = EnumEx.ToName(typeof(TestEnum), (TestEnum)3);
			Assert.AreEqual("3", name);
		}

		[TestMethod]
		public void ToNameTEnum_OutOfRange_IsNull()
		{
			string name = EnumEx.ToName<TestEnum>((TestEnum)3);
			Assert.AreEqual("3", name);
		}

		[TestMethod]
		public void ToName_WithAttrib_EqualsFirstName()
		{
			string name = EnumEx.ToName(typeof(TestEnum), TestEnum.WithAttrib);
			Assert.AreEqual("With Attrib", name);
		}

		[TestMethod]
		public void ToNameTEnum_WithAttrib_EqualsFirstName()
		{
			string name = EnumEx.ToName<TestEnum>(TestEnum.WithAttrib);
			Assert.AreEqual("With Attrib", name);
		}

		[TestMethod]
		public void ToName_WithoutAttrib_EqualsFirstName()
		{
			string name = EnumEx.ToName(typeof(TestEnum), TestEnum.WithoutAttrib);
			Assert.AreEqual("WithoutAttrib", name);
		}

		[TestMethod]
		public void ToNameTEnum_WithoutAttrib_EqualsFirstName()
		{
			string name = EnumEx.ToName<TestEnum>(TestEnum.WithoutAttrib);
			Assert.AreEqual("WithoutAttrib", name);
		}
	}
}
