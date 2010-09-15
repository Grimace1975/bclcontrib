//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Security;
//using System.Linq;
//namespace System
//{
//    /// <summary>
//    ///This is a test class for SecurityHelperTest and is intended
//    ///to contain all SecurityHelperTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class SecurityExTests
//    {
//        /// <summary>
//        ///A test for HashIt
//        ///</summary>
//        [TestMethod()]
//        public void HashIt()
//        {
//            string input = "All the king's horses and all the king's men.";
//            string algorithm = "SHA1";
//            string expected = "658E306DF9EF48F1D837D448FE57AC61F9444089";
//            string actual;
//            actual = SecurityEx.ComputeHash(input, algorithm);
//            Assert.AreEqual<string>(expected, actual);
//            Console.WriteLine(actual);
//            Assert.IsTrue(actual.Length == 40);
//        }

//        /// <summary>
//        ///A test for HashIt
//        ///</summary>
//        [TestMethod()]
//        public void HashIt2()
//        {
//            string input = "All the king's horses and all the king's men.";
//            string algorithm = "SHA1";
//            bool upperCase = false;
//            string expected = "658e306df9ef48f1d837d448fe57ac61f9444089";
//            string actual;
//            actual = SecurityEx.ComputeHash(input, algorithm, upperCase);
//            Assert.AreEqual<string>(expected, actual);
//            Console.WriteLine(actual);
//            Assert.IsTrue(actual.Length == 40);
//        }

//        /// <summary>
//        ///A test for GenerateSymetricKeyTest
//        ///</summary>
//        [TestMethod()]
//        public void GenerateSymmetricKey()
//        {
//            Console.WriteLine(SecurityEx.GenerateSymmetricKey());
//        }

//        /// <summary>
//        ///A test for GenerateSymetricKeyIv
//        ///</summary>
//        [TestMethod()]
//        public void GenerateSymmetricIv()
//        {
//            Console.WriteLine(SecurityEx.GenerateSymmetricIv());
//        }

//        /// <summary>
//        ///A test for SymetricEncrypt
//        ///</summary>
//        [TestMethod()]
//        public void SymmetricEncrypt()
//        {
//            byte[] clearBytes = ConvertEx.FromBase16String("0123456789ABCDEF");
//            byte[] key = ConvertEx.FromBase16String("A07C4E1BD5BBFF83BB8D72F2027CD32D077B8C5F7BABC52BD72A277C55943214");
//            byte[] iv = ConvertEx.FromBase16String("A9DCF37AED8574A1441FD82DB743765A");
//            byte[] expected = ConvertEx.FromBase16String("871131E5E3A58D2A6032FAE53E606C5B");
//            byte[] actual;
//            actual = SecurityEx.SymmetricEncrypt(clearBytes, key, iv);
//            Console.WriteLine(ConvertEx.ToBase16String(actual));
//            Assert.IsTrue(actual.Match(expected, true));
//        }

//        /// <summary>
//        ///A test for SymetricDecrypt
//        ///</summary>
//        [TestMethod()]
//        public void SymmetricDecrypt()
//        {
//            byte[] cipherBytes = ConvertEx.FromBase16String("871131E5E3A58D2A6032FAE53E606C5B");
//            byte[] key = ConvertEx.FromBase16String("A07C4E1BD5BBFF83BB8D72F2027CD32D077B8C5F7BABC52BD72A277C55943214");
//            byte[] iv = ConvertEx.FromBase16String("A9DCF37AED8574A1441FD82DB743765A");
//            byte[] expected = ConvertEx.FromBase16String("0123456789ABCDEF");
//            byte[] actual;
//            actual = SecurityEx.SymmetricDecrypt(cipherBytes, key, iv);
//            Console.WriteLine(ConvertEx.ToBase16String(actual));
//            Assert.IsTrue(actual.Match(expected, true));
//        }

//        /// <summary>
//        ///A test for Encrypt
//        ///</summary>
//        [TestMethod()]
//        public void Encrypt()
//        {
//            string clearText = "mysecretpassword12&";
//            string key = "A07C4E1BD5BBFF83BB8D72F2027CD32D077B8C5F7BABC52BD72A277C55943214";
//            string expected = "60F143C320DAF6F4443F38081D28AAB5C3DDCE081CA113E20FE8EBAE5A458EF3";
//            string actual;
//            actual = SecurityEx.Encrypt(clearText, key);
//            Console.WriteLine(actual);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for Decrypt
//        ///</summary>
//        [TestMethod()]
//        public void Decrypt()
//        {
//            string cipherText = "60F143C320DAF6F4443F38081D28AAB5C3DDCE081CA113E20FE8EBAE5A458EF3";
//            string key = "A07C4E1BD5BBFF83BB8D72F2027CD32D077B8C5F7BABC52BD72A277C55943214";
//            string expected = "mysecretpassword12&"; 
//            string actual;
//            actual = SecurityEx.Decrypt(cipherText, key);
//            Console.WriteLine(actual);
//            Assert.AreEqual(expected, actual);
//        }
//    }
//}
