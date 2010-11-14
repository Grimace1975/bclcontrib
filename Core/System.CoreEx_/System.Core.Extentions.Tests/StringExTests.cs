using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System;
namespace System
{
	[TestClass]
	public class StringExTests
	{
		[TestMethod]
		public void Axb_Strings()
		{
			Assert.AreEqual("axb", StringEx.Axb("left", "x", "right"));
			Assert.AreEqual("right", StringEx.Axb("", "x", "right"));
			Assert.AreEqual("left", StringEx.Axb("left", "x", ""));
			Assert.AreEqual("", StringEx.Axb("", "x", ""));
		}

		[TestMethod]
		public void Axb_StringBuilder()
		{
			Assert.AreEqual("axb", StringEx.Axb(new StringBuilder("left"), "x", "right").ToString());
			Assert.AreEqual("right", StringEx.Axb(new StringBuilder(), "x", "right").ToString());
			Assert.AreEqual("left", StringEx.Axb(new StringBuilder("left"), "x", "").ToString());
			Assert.AreEqual("", StringEx.Axb(new StringBuilder(), "x", "").ToString());
		}

		[TestMethod]
		public void Ay()
		{
			Assert.AreEqual("ay", StringEx.Ay("left", "y"));
			Assert.AreEqual("", StringEx.Ay("", "y"));
		}


		//[TestMethod]
		//public void AbbreviateTextTest()
		//{
		//    string input = "All the king's horses and all the king's men.";
		//    int length = 30;
		//    string trailer = "...";
		//    string expected = "All the king's horses...";
		//    string actual;
		//    actual = StringHelper.AbbreviateText(input, length, trailer);
		//    Console.WriteLine(actual);
		//    Assert.AreEqual(expected, actual);
		//}
	}
}
