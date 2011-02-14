using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace System.Collections
{
	[TestClass]
	public class DictionaryExtensionsTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Insert_TextNullOrEmpty_Throws()
		{
			((Dictionary<string, string>)null).Insert((string[])null);
		}

		[TestMethod]
		public void Insert_TextNullOrEmpty_EqualsEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert((string[])null);
			Assert.IsNotNull(dictionary);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void Insert_TextSingleNull_Throws()
		{
			(new Dictionary<string, string>()).Insert(new string[] { null });
		}

		[TestMethod]
		public void Insert_TextSingleEmptyString_EqualsEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { string.Empty });
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		public void Insert_TextSingleKeyOnly_EqualsSingleKeyOnly()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key" });
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key"));
			Assert.AreEqual("key", dictionary["key"]);
		}

		[TestMethod]
		public void Insert_TextSingleKeyWithValue_EqualsSingleKeyWithValue()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" });
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key"));
			Assert.AreEqual("value", dictionary["key"]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Insert2_TextNullOrEmpty_Throws()
		{
			((Dictionary<string, string>)null).Insert((string[])null, 0);
		}

		[TestMethod]
		public void Insert2_TextNullOrEmpty_EqualEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert((string[])null, 0);
			Assert.IsNotNull(dictionary);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
		public void Insert2_TextSingleNull_Throws()
		{
			(new Dictionary<string, string>()).Insert(new string[] { null }, 0);
		}

		[TestMethod]
		public void Insert2_TextSingleEmptyString_EqualsEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { string.Empty }, 0);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		public void Insert2_TextSingleKeyOnly_EqualsSingleKeyOnly()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key" }, 0);
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key"));
			Assert.AreEqual("key", dictionary["key"]);
		}

		[TestMethod]
		public void Insert2_TextSingleKeyWithValue_EqualsSingleKeyWithValue()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" }, 0);
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key"));
			Assert.AreEqual("value", dictionary["key"]);
		}

		[TestMethod]
		public void Insert2_TextSingleKeyWithValueAndIndexOne_EqualsEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" }, 1);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Insert2_TextSingleKeyWithValueAndIndexNegativeOne_Throws()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" }, -1);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		public void Insert2_TextTwoKeyWithValueAndIndexOne_EqualsSingleKeyWithValueFromSecond()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value", "key2=value2" }, 1);
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key2"));
			Assert.AreEqual("value2", dictionary["key2"]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Insert3_TextNullOrEmpty_Throws()
		{
			((Dictionary<string, string>)null).Insert((string[])null, 0, 1);
		}

		[TestMethod]
		public void Insert3_TextNullOrEmpty_EqualEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert((string[])null, 0, 1);
			Assert.IsNotNull(dictionary);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Insert3_TextSingleNull_Throws()
		{
			(new Dictionary<string, string>()).Insert(new string[] { null }, 0, 1);
		}

		[TestMethod]
		public void Insert3_TextSingleEmptyString_EqualsEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { string.Empty }, 0, 1);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		public void Insert3_TextSingleKeyOnly_EqualsSingleKeyOnly()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key" }, 0, 1);
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key"));
			Assert.AreEqual("key", dictionary["key"]);
		}

		[TestMethod]
		public void Insert3_TextSingleKeyWithValue_EqualsSingleKeyWithValue()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" }, 0, 1);
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key"));
			Assert.AreEqual("value", dictionary["key"]);
		}

		[TestMethod]
		public void Insert3_TextSingleKeyWithValueAndIndexOne_EqualsEmptySet()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" }, 1, 1);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Insert3_TextSingleKeyWithValueAndIndexNegativeOne_Throws()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value" }, -1, 1);
			Assert.AreEqual(0, dictionary.Count);
		}

		[TestMethod]
		public void Insert3_TextTwoKeyWithValueAndIndexOne_EqualsSingleKeyWithValueFromSecond()
		{
			var dictionary = (new Dictionary<string, string>()).Insert(new string[] { "key=value", "key2=value2" }, 1, 2);
			Assert.AreEqual(1, dictionary.Count);
			Assert.IsTrue(dictionary.ContainsKey("key2"));
			Assert.AreEqual("value2", dictionary["key2"]);
		}
	}
}
