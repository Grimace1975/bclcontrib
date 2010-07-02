using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.Codecs
{
    [TestClass]
    public class CryptCodecTests
    {
        [TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void PrimeDecode_NullOrEmpty_Throws()
        {
			CryptCodec.Decode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
		public void PrimeEncode_NullOrEmpty_Throws()
        {
			CryptCodec.Encode(null);
		}

		#region Binding
		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Decode_NullOrEmpty_Throws()
		{
			var codec = (new CryptCodec() as ICodec);
			codec.Decode(null);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Encode_NullOrEmpty_Throws()
		{
			var codec = (new CryptCodec() as ICodec);
			codec.Encode(null);
		}
		#endregion
	}
}
