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
namespace System
{
    /// <summary>
    /// DateTimeEx
    /// </summary>
	public class DateTimeEx
	{
		[ThreadStatic]
		private static DateTime? s_utcNowMock;

		public static DateTime? NowMock
		{
			get { return (s_utcNowMock.HasValue ? (DateTime?)s_utcNowMock.Value.ToLocalTime() : null); }
			set
			{
				if (value.HasValue)
					s_utcNowMock = value.Value.ToUniversalTime();
				else
					s_utcNowMock = null;
			}
		}

		public static DateTime? UtcNowMock
		{
			get { return s_utcNowMock; }
			set { s_utcNowMock = value; }
		}

        public static DateTime Today
        {
            get { return Now.Date; }
        }

		public static DateTime Now
		{
			get { return (!s_utcNowMock.HasValue ? DateTime.Now : s_utcNowMock.Value.ToLocalTime()); }
		}

		public static DateTime UtcNow
		{
			get { return (!s_utcNowMock.HasValue ? DateTime.UtcNow : s_utcNowMock.Value); }
		}
	}
}
