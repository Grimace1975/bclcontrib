#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// DecimalRangeDataType
    /// </summary>
	public class DecimalRangeDataType : DataTypeBase
	{
		public class FormatAttrib { }

		public class RangeParserAttrib
		{
			public decimal? MinValue { get; set; }
			public decimal? MaxValue { get; set; }
			public decimal? MaxRangeCoverage { get; set; }
		}

		public class ParseAttrib : RangeParserAttrib { }

		public DecimalRangeDataType()
			: base(Prime.Type, Prime.FormFieldMeta, new DataTypeFormatter(), new DataTypeParser()) { }

		public class DataTypeFormatter : DataTypeFormatterBase<string, FormatAttrib, ParseAttrib>
		{
			public DataTypeFormatter()
				: base(Prime.Format, Prime.TryParse) { }
		}

		public class DataTypeParser : DataTypeParserBase<string, ParseAttrib>
		{
			public DataTypeParser()
				: base(Prime.TryParse) { }
		}

		/// <summary>
		/// Rangeses to XML.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static string RangesToXml(string text, decimal step, Nattrib attrib)
		{
			ICollection<Range<decimal>> ranges;
			if (!ParserEx.TryParseRanges(text, Prime.RangeParser, attrib.Get<RangeParserAttrib>(), out ranges))
				return null;
			// to xml
			var b = new StringBuilder();
            var w = XmlWriter.Create(b, new XmlWriterSettings() { OmitXmlDeclaration = true });
			w.WriteStartElement("root");
			foreach (var range in ranges)
			{
				if (range.HasEndValue)
				{
					for (decimal index = range.BeginValue; index <= range.EndValue; index += step)
					{
						w.WriteStartElement("item");
						w.WriteAttributeString("key", index.ToString());
						w.WriteEndElement();
					}
					continue;
				}
				w.WriteStartElement("item");
				w.WriteAttributeString("key", range.BeginValue.ToString());
				w.WriteEndElement();
			}
			w.WriteEndElement();
			w.Close();
			return b.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		public static class Prime
		{
			public static string Format(string value, FormatAttrib attrib)
			{
				return value;
			}

			public static bool TryParse(string text, ParseAttrib attrib, out string value)
			{
				if (string.IsNullOrEmpty(text))
				{
					value = string.Empty;
					return false;
				}
				ICollection<Range<decimal>> ranges;
				if (!ParserEx.TryParseRanges(text, RangeParser, attrib, out ranges))
				{
					value = string.Empty;
					return false;
				}
				value = text;
				return true;
			}

			public static Type Type
			{
				get { return typeof(string); }
			}

			public static DataTypeFormFieldMeta FormFieldMeta
			{
				get
				{
					return new DataTypeFormFieldMeta()
					{
						GetBinderType = (int applicationType) => "Text",
						GetMaxLength = (int applicationType) => 100,
						GetSize = (int applicationType, int length) => "25",
					};
				}
			}

			public static Range<decimal> RangeParser<TAttrib>(string beginValue, bool hasEndValue, string endValue, TAttrib attrib)
				where TAttrib : RangeParserAttrib
			{
				// check attrib
				decimal? minValue;
				decimal? maxValue;
				decimal? maxRangeCoverage;
				if (attrib != null)
				{
					minValue = attrib.MinValue;
					maxValue = attrib.MaxValue;
					maxRangeCoverage = attrib.MaxRangeCoverage;
				}
				else
				{
					minValue = null;
					maxValue = null;
					maxRangeCoverage = null;
				}
				// from integerdatatype
				decimal validBeginValue;
				if ((!decimal.TryParse(beginValue, out validBeginValue))
				|| ((maxValue.HasValue) && (validBeginValue < minValue.Value))
				|| ((maxValue.HasValue) && (validBeginValue > maxValue.Value)))
				{
					return null;
				}
				if (hasEndValue)
				{
					// from integerdatatype
					decimal validEndValue;
					if ((!decimal.TryParse(endValue, out validEndValue))
						|| ((maxValue.HasValue) && (validEndValue < minValue.Value))
						|| ((maxValue.HasValue) && (validEndValue > maxValue.Value))
						|| ((maxRangeCoverage.HasValue) && (Math.Abs(validBeginValue - validEndValue) > maxRangeCoverage.Value)))
					{
						return null;
					}
					return new Range<decimal> { BeginValue = validBeginValue, EndValue = validEndValue };
				}
				return new Range<decimal> { BeginValue = validBeginValue };
			}
		}
	}
}