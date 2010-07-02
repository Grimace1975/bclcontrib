using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.Codecs
{
    [TestClass]
	public class CsvCodecTests
    {
		[TestMethod]
		public void PrimeDecode_NullOrEmpty_EqualsStringEmpty()
		{
			Assert.AreEqual(string.Empty, CsvCodec.Decode(null));
		}

		[TestMethod]
		public void PrimeDecode_TestString_EqualsTest()
		{
			Assert.AreEqual("Test", CsvCodec.Decode("Test"));
		}


		[TestMethod]
		public void PrimeDecode_QuotedTestString_EqualsTest()
		{
			Assert.AreEqual("Test", CsvCodec.Decode("\"Test\""));
		}

		[TestMethod]
		public void PrimeDecode_QuotedEmptyString_EqualsEmptyString()
		{
			Assert.AreEqual(string.Empty, CsvCodec.Decode("\"\""));
		}

		[TestMethod]
		public void PrimeDecode_QuotedEscapedTest_EqualsTestWithSubQuotes()
		{
			Assert.AreEqual("Test \"Quote\"", CsvCodec.Decode("\"Test \"\"Quote\"\"\""));
		}


		[TestMethod]
		public void PrimeEncode_NullOrEmpty_EqualsStringEmpty()
		{
			Assert.AreEqual("\"\"", CsvCodec.Encode(null));
		}

		[TestMethod]
		public void PrimeEncode_TestString_EqualsQuotedTest()
		{
			Assert.AreEqual("\"Test\"", CsvCodec.Encode("Test"));
		}

		[TestMethod]
		public void PrimeEncode_TestWithSubQuotesString_EqualsQuotedEscapedTest()
		{
			Assert.AreEqual("\"Test \"\"Quote\"\"\"", CsvCodec.Encode("Test \"Quote\""));
		}

		#region Binding
		[TestMethod]
		public void Decode_NullOrEmpty_EqualsStringEmpty()
		{
            var codec = (new CsvCodec() as ICodec);
			Assert.AreEqual(string.Empty, codec.Decode(null));
		}

		[TestMethod]
		public void Encode_NullOrEmpty_EqualsStringEmpty()
		{
            var codec = (new CsvCodec() as ICodec);
			Assert.AreEqual("\"\"", codec.Encode(null));
		}
		#endregion
	}
}
