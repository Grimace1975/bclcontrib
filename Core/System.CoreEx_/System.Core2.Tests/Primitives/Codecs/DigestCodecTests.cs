using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.Codecs
{
    [TestClass]
	public class DigestCodecTests
    {
        [TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PrimeEncode_Null_Throws()
        {
			Assert.AreEqual(string.Empty, DigestCodec.Encode(null));
        }

		#region Binding
		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void Decode_Valid_Throws()
		{
			var codec = (new DigestCodec() as ICodec);
			codec.Decode(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Encode_Null_Throws()
		{
			var codec = (new DigestCodec() as ICodec);
			Assert.AreEqual(string.Empty, codec.Encode(null));
		}
		#endregion
	}
}
