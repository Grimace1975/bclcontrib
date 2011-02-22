using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.Codecs
{
    [TestClass]
	public class QuotedPrintableCodecTests
    {
        [TestMethod]
		public void PrimeDecode_NullOrEmpty_EqualsStringEmpty()
        {
			Assert.AreEqual(string.Empty, QuotedPrintableCodec.Decode(null));
        }

        [TestMethod]
		public void PrimeEncode_NullOrEmpty_EqualsStringEmpty()
        {
			Assert.AreEqual(string.Empty, QuotedPrintableCodec.Encode(null));
        }

		#region Binding
		[TestMethod]
		public void Decode_NullOrEmpty_EqualsStringEmpty()
		{
			var codec = (new QuotedPrintableCodec() as ICodec);
			Assert.AreEqual(string.Empty, codec.Decode(null));
		}

		[TestMethod]
		public void Encode_NullOrEmpty_EqualsStringEmpty()
		{
			var codec = (new QuotedPrintableCodec() as ICodec);
			Assert.AreEqual(string.Empty, codec.Encode(null));
		}
		#endregion
	}
}
