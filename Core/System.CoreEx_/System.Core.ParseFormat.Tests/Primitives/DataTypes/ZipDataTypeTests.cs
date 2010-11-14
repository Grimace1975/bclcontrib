//using Microsoft.VisualStudio.TestTools.UnitTesting;
//namespace System.Primitives.DataTypes
//{
//    [TestClass]
//    public class ZipDataTypeTests
//    {
//        [TestMethod]
//        public void PrimeFormat_NullAndNullFormat_IsNull()
//        {
//            Assert.IsNull(ZipDataType.Prime.Format(null, null));
//        }

//        [TestMethod]
//        public void PrimeFormat_TestAndNullFormat_EqualsTest()
//        {
//            Assert.AreEqual("Test", ZipDataType.Prime.Format("Test", null));
//        }

//        [TestMethod]
//        public void PrimeTryPase_NullOrStringEmptyAndNullFormat_IsFalse()
//        {
//            string value;
//            Assert.IsFalse(ZipDataType.Prime.TryParse(null, null, out value));
//        }

//        [TestMethod]
//        public void PrimeTryPase_VariousAndNullFormat_IsTrue()
//        {
//            string value;
//            Assert.IsTrue(ZipDataType.Prime.TryParse("123", null, out value));
//            Assert.IsTrue(ZipDataType.Prime.TryParse("1234", null, out value));
//            Assert.IsTrue(ZipDataType.Prime.TryParse("12345", null, out value));
//            Assert.IsTrue(ZipDataType.Prime.TryParse("1234567", null, out value));
//            Assert.IsTrue(ZipDataType.Prime.TryParse("12345678", null, out value));
//            Assert.IsTrue(ZipDataType.Prime.TryParse("123456789", null, out value));
//            Assert.IsTrue(ZipDataType.Prime.TryParse("12345-6789", null, out value));
//        }

//        [TestMethod]
//        public void PrimeTryPase_VariousAndNullFormat_IsFalse()
//        {
//            string value;
//            Assert.IsFalse(ZipDataType.Prime.TryParse("1", null, out value));
//            Assert.IsFalse(ZipDataType.Prime.TryParse("12", null, out value));
//            Assert.IsFalse(ZipDataType.Prime.TryParse("123456", null, out value));
//            Assert.IsFalse(ZipDataType.Prime.TryParse("1234567890", null, out value));
//        }

//        [TestMethod]
//        public void PrimeType_Valid_EqualsStringType()
//        {
//            Assert.AreSame(typeof(string), ZipDataType.Prime.Type);
//        }

//        #region Binding
//        [TestMethod]
//        public void Format_TestAndNullFormat_EqualsTest()
//        {
//            var formatter = new ZipDataType().Formatter;
//            Assert.AreEqual("Test", formatter.Format("Test", null, null));
//        }

//        [TestMethod]
//        public void TryParse_TestAndNullFormat_EqualsTest()
//        {
//            var formatter = new ZipDataType().Formatter;
//            Assert.AreEqual("Test", formatter.Format("Test", null, null));
//        }

//        [TestMethod]
//        public void TypeCode_Valid_EqualsTypeCodeString()
//        {
//            var dataType = new ZipDataType();
//            Assert.AreEqual(typeof(string), dataType.Type);
//        }
//        #endregion
//    }
//}
