using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace System.Primitives.DataTypes
{
	[TestClass]
	public class BoolDataTypeTests
	{
		[TestMethod]
		public void PrimeFormat_TrueAndNullFormat_EqualsYes()
		{
			Assert.AreEqual(BoolDataType.YesString, BoolDataType.Prime.Format(true, null));
		}

		[TestMethod]
		public void PrimeFormat_TrueAndTrueFalseFormat_EqualsTrue()
		{
			Assert.AreEqual(bool.TrueString, BoolDataType.Prime.Format(true, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.TrueFalse,
			}));
		}

		[TestMethod]
		public void PrimeFormat_TrueAndYesNoFormat_EqualsYes()
		{
			Assert.AreEqual(BoolDataType.YesString, BoolDataType.Prime.Format(true, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.YesNo,
			}));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PrimeFormat_TrueAndValuesFormatWithNoValues_Throws()
		{
			Assert.AreEqual(BoolDataType.YesString, BoolDataType.Prime.Format(true, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.Values,
			}));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PrimeFormat_TrueAndValuesFormatWithOneValues_Throws()
		{
			Assert.AreEqual(BoolDataType.YesString, BoolDataType.Prime.Format(true, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.Values,
				Values = new string[] { "1" },
			}));
		}

		[TestMethod]
		public void PrimeFormat_TrueAndValuesFormatWithTwoValues_Equals1()
		{
			Assert.AreEqual("1", BoolDataType.Prime.Format(true, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.Values,
				Values = new string[] { "1", "0" },
			}));
		}

		// Not
		[TestMethod]
		public void PrimeFormat_FalseAndNullFormat_EqualsNo()
		{
			Assert.AreEqual(BoolDataType.NoString, BoolDataType.Prime.Format(false, null));
		}

		[TestMethod]
		public void PrimeFormat_FalseAndTrueFalseFormat_EqualsFalse()
		{
			Assert.AreEqual(bool.FalseString, BoolDataType.Prime.Format(false, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.TrueFalse,
			}));
		}

		[TestMethod]
		public void PrimeFormat_FalseAndYesNoFormat_EqualsNo()
		{
			Assert.AreEqual(BoolDataType.NoString, BoolDataType.Prime.Format(false, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.YesNo,
			}));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PrimeFormat_FalseAndValuesFormatWithNoValues_Throws()
		{
			Assert.AreEqual(BoolDataType.NoString, BoolDataType.Prime.Format(false, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.Values,
			}));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void PrimeFormat_FalseAndValuesFormatWithOneValues_Throws()
		{
			Assert.AreEqual(BoolDataType.NoString, BoolDataType.Prime.Format(false, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.Values,
				Values = new string[] { "1" },
			}));
		}

		[TestMethod]
		public void PrimeFormat_FalseAndValuesFormatWithTwoValues_Equals0()
		{
			Assert.AreEqual("0", BoolDataType.Prime.Format(false, new BoolDataType.FormatAttrib
			{
				Formats = BoolDataType.Formats.Values,
				Values = new string[] { "1", "0" },
			}));
		}

		#region Binding
		[TestMethod]
		public void Format_TrueAndNullFormat_EqualsYes()
		{
			var formatter = new BoolDataType().Formatter;
			Assert.AreEqual(BoolDataType.YesString, formatter.Format(true, null, null));
		}
		#endregion
	}
}
