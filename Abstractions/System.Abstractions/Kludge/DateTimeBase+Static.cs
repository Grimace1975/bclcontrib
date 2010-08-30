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
using System.Globalization;
namespace System.Kludge
{
	public abstract partial class DateTimeBase
	{
		[ThreadStatic]
		private static DateTimeBase s_utcNowMock;

		public static DateTimeBase NowMock
		{
			get { return s_utcNowMock.ToLocalTime(); }
			set { s_utcNowMock = value.ToUniversalTime(); }
		}

		public static DateTimeBase UtcNowMock
		{
			get { return s_utcNowMock; }
			set { s_utcNowMock = value; }
		}

		public static int Compare(DateTimeBase t1, DateTimeBase t2) { return DateTime.Compare(t1.WrappedValue, t2.WrappedValue); }

		public static bool Equals(DateTimeBase t1, DateTimeBase t2) { return DateTime.Equals(t1.WrappedValue, t2.WrappedValue); }

		public static DateTimeBase FromBinary(long dateData) { return new DateTimeWrapper(DateTime.FromBinary(dateData)); }

		public static DateTimeBase FromFileTime(long fileTime) { return new DateTimeWrapper(DateTime.FromFileTime(fileTime)); }

		public static DateTimeBase FromFileTimeUtc(long fileTime) { return new DateTimeWrapper(DateTime.FromFileTimeUtc(fileTime)); }

		public static DateTimeBase FromOADate(double d) { return new DateTimeWrapper(DateTime.FromOADate(d)); }

		public static DateTimeBase operator +(DateTimeBase d, TimeSpan t) { return new DateTimeWrapper(d.WrappedValue + t); }

		public static bool operator ==(DateTimeBase d1, DateTimeBase d2) { return d1.WrappedValue == d2.WrappedValue; }

		public static bool operator >(DateTimeBase t1, DateTimeBase t2) { return t1.WrappedValue > t2.WrappedValue; }

		public static bool operator >=(DateTimeBase t1, DateTimeBase t2) { return t1.WrappedValue >= t2.WrappedValue; }

		public static bool operator !=(DateTimeBase d1, DateTimeBase d2) { return d1.WrappedValue != d2.WrappedValue; }

		public static bool operator <(DateTimeBase t1, DateTimeBase t2) { return t1.WrappedValue < t2.WrappedValue; }

		public static bool operator <=(DateTimeBase t1, DateTimeBase t2) { return t1.WrappedValue <= t2.WrappedValue; }

		public static TimeSpan operator -(DateTimeBase d1, DateTimeBase d2) { return d1.WrappedValue - d2.WrappedValue; }

		public static DateTimeBase operator -(DateTimeBase d, TimeSpan t) { return new DateTimeWrapper(d.WrappedValue - t); }

		public static DateTimeBase Parse(string s) { return new DateTimeWrapper(DateTime.Parse(s)); }

		public static DateTimeBase Parse(string s, IFormatProvider provider) { return new DateTimeWrapper(DateTime.Parse(s, provider)); }

		public static DateTimeBase Parse(string s, IFormatProvider provider, DateTimeStyles styles) { return new DateTimeWrapper(DateTime.Parse(s, provider, styles)); }

		public static DateTimeBase ParseExact(string s, string format, IFormatProvider provider) { return new DateTimeWrapper(DateTime.ParseExact(s, format, provider)); }

		public static DateTimeBase ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style) { return new DateTimeWrapper(DateTime.ParseExact(s, format, provider, style)); }

		public static DateTimeBase ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style) { return new DateTimeWrapper(DateTime.ParseExact(s, formats, provider, style)); }

		public static DateTimeBase SpecifyKind(DateTimeBase value, DateTimeKind kind) { return new DateTimeWrapper(DateTime.SpecifyKind(value.WrappedValue, kind)); }

		public static bool TryParse(string s, out DateTimeBase result)
		{
			DateTime resultAsDateTime;
			bool returnValue = DateTime.TryParse(s, out resultAsDateTime);
			result = new DateTimeWrapper(resultAsDateTime);
			return returnValue;
		}
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTimeBase result)
		{
			DateTime resultAsDateTime;
			bool returnValue = DateTime.TryParse(s, provider, styles, out resultAsDateTime);
			result = new DateTimeWrapper(resultAsDateTime);
			return returnValue;
		}

		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTimeBase result)
		{
			DateTime resultAsDateTime;
			bool returnValue = DateTime.TryParseExact(s, formats, provider, style, out resultAsDateTime);
			result = new DateTimeWrapper(resultAsDateTime);
			return returnValue;
		}

		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTimeBase result)
		{
			DateTime resultAsDateTime;
			bool returnValue = DateTime.TryParseExact(s, format, provider, style, out resultAsDateTime);
			result = new DateTimeWrapper(resultAsDateTime);
			return returnValue;
		}

		public static DateTimeBase Now
		{
			get { return (s_utcNowMock != null ? s_utcNowMock.ToLocalTime() : new DateTimeWrapper(DateTime.Now)); }
		}

		public static DateTimeBase Today
		{
			get { return Now.Date; }
		}

		public static DateTimeBase UtcNow
		{
			get { return (s_utcNowMock != null ? s_utcNowMock : new DateTimeWrapper(DateTime.UtcNow)); }
		}

        public override bool Equals(object obj)
        {
            return (s_utcNowMock != null ? s_utcNowMock.Equals(obj) : base.Equals(obj));
        }

        public override int GetHashCode()
        {
            return (s_utcNowMock != null ? s_utcNowMock.GetHashCode() : base.GetHashCode());
        }
	}
}
