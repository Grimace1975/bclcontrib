//using Microsoft.VisualStudio.TestTools.UnitTesting;
//namespace System
//{
//    [TestClass]
//    public class ArrayExTests
//    {
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException))]
//        public void Coalesce_Null_Throws()
//        {
//            ArrayEx.Coalesce<string>(null, null);
//        }

//        [TestMethod]
//        public void Coalesce_TwoNullValues_IsNull()
//        {
//            var value = ArrayEx.Coalesce(null, new string[] { null, null });
//            Assert.IsNull(value);
//        }

//        [TestMethod]
//        public void Coalesce_NullAndNonNullValues_EqualsNonNull()
//        {
//            var value = ArrayEx.Coalesce(null, new string[] { null, "NonNull" });
//            var value2 = ArrayEx.Coalesce(int.MinValue, new int[] { int.MinValue, 1 });
//            Assert.AreEqual("NonNull", value);
//            Assert.AreEqual(1, value2);
//        }

//        [TestMethod]
//        public void MinSkipNull()
//        {
//            var value = ArrayEx.MinSkipNull(int.MinValue, new int[] { int.MinValue, 2, 1 });
//            Assert.AreEqual(1, value);
//        }

//        [TestMethod]
//        public void MinSkipNullT()
//        {
//            var value = ArrayEx.MinSkipNull<int>(int.MinValue, new int[] { int.MinValue, 2, 1 });
//            Assert.AreEqual(1, value);
//        }

//        [TestMethod]
//        public void FindSkipNull_EqualsOne()
//        {
//            var value = ArrayEx.FindSkipNull(int.MinValue, (a, b) => (a > b), new int[] { int.MinValue, 2, 1 });
//            Assert.AreEqual(1, value);
//        }
//    }
//}
