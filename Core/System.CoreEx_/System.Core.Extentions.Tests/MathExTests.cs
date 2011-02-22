using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System
{
	[TestClass]
	public class MathExTests
	{
		[TestMethod]
		public void Ceiling()
		{
			decimal value = MathEx.Ceiling(1.234M);
			Assert.AreEqual(2M, value);
		}

		[TestMethod]
		public void Ceiling_2()
		{
			decimal value = MathEx.Ceiling(1.234M, 1);
			Assert.AreEqual(1.3M, value);
		}

		[TestMethod]
		public void CreateRandom()
		{
			int value = MathEx.CreateRandom(1, 10);
			Assert.IsTrue(value >= 1);
			Assert.IsTrue(value <= 10);
		}

		[TestMethod]
		public void Floor()
		{
			decimal value = MathEx.Floor(1.234M);
			Assert.AreEqual(1M, value);
		}

		[TestMethod]
		public void Floor_2()
		{
			decimal value = MathEx.Floor(1.234M, 1);
			Assert.AreEqual(1.2M, value);
		}
	}
}
