using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System
{
    [TestClass]
    public class ConvertExTests
    {
        [TestMethod]
        public void FromBase16String_NullOrEmpty_LengthEqualsZero()
        {
            var bytes = ConvertEx.FromBase16String(null);
            Assert.AreEqual(0, bytes.Length);
        }

        [TestMethod]
        public void FromBase16String()
        {
            var bytes = ConvertEx.FromBase16String("101C");
            Assert.IsTrue(new byte[] { 0x10, 0x1C }.Match(bytes, true));
        }

        [TestMethod]
        public void ToBase16String_Null()
        {
            var text = ConvertEx.ToBase16String(null);
            Assert.AreEqual(string.Empty, text);
        }

        [TestMethod]
        public void ToBase16String()
        {
            var text = ConvertEx.ToBase16String(new byte[] { 0x10, 0x1C });
            Assert.AreEqual("101C", text);
        }
    }
}
