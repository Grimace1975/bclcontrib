using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.Codecs
{
    [TestClass]
	public class RtfCodecTests
    {
        [TestMethod]
        public void PrimeDecode_NullOrEmpty_EqualsStringEmpty()
        {
			Assert.AreEqual(string.Empty, RtfCodec.Decode(null));
        }

        [TestMethod]
		public void PrimeEncode_NullOrEmpty_EqualsStringEmpty()
        {
			Assert.AreEqual(string.Empty, RtfCodec.Encode(null));
        }

		#region Binding
		[TestMethod]
		public void Decode_NullOrEmpty_EqualsStringEmpty()
		{
			var codec = (new RtfCodec() as ICodec);
			Assert.AreEqual(string.Empty, codec.Decode(null));
		}

		[TestMethod]
		public void Encode_NullOrEmpty_EqualsStringEmpty()
		{
			var codec = (new RtfCodec() as ICodec);
			Assert.AreEqual(string.Empty, codec.Encode(null));
		}
		#endregion
	}
}
