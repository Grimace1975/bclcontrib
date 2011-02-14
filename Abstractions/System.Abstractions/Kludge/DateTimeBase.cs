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
namespace System.Kludge
{
    /// <summary>
    /// DateTimeBase
    /// </summary>
    public abstract partial class DateTimeBase
    {
        protected DateTimeBase() { }

        public virtual DateTimeBase Add(TimeSpan value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddDays(double value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddHours(double value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddMilliseconds(double value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddMinutes(double value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddMonths(int months) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddSeconds(double value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddTicks(long value) { throw new NotImplementedException(); }
        public virtual DateTimeBase AddYears(int value) { throw new NotImplementedException(); }
        public virtual int CompareTo(DateTimeBase value) { throw new NotImplementedException(); }
        public virtual int CompareTo(object value) { throw new NotImplementedException(); }
        public virtual string[] GetDateTimeFormats() { throw new NotImplementedException(); }
        public virtual string[] GetDateTimeFormats(char format) { throw new NotImplementedException(); }
        public virtual string[] GetDateTimeFormats(IFormatProvider provider) { throw new NotImplementedException(); }
        public virtual string[] GetDateTimeFormats(char format, IFormatProvider provider) { throw new NotImplementedException(); }
        public virtual bool IsDaylightSavingTime() { throw new NotImplementedException(); }
        public virtual TimeSpan Subtract(DateTime value) { throw new NotImplementedException(); }
        public virtual DateTimeBase Subtract(TimeSpan value) { throw new NotImplementedException(); }
        public virtual long ToBinary() { throw new NotImplementedException(); }
        public virtual long ToFileTime() { throw new NotImplementedException(); }
        public virtual long ToFileTimeUtc() { throw new NotImplementedException(); }
        public virtual DateTimeBase ToLocalTime() { throw new NotImplementedException(); }
        public virtual string ToLongDateString() { throw new NotImplementedException(); }
        public virtual string ToLongTimeString() { throw new NotImplementedException(); }
        public virtual double ToOADate() { throw new NotImplementedException(); }
        public virtual string ToShortDateString() { throw new NotImplementedException(); }
        public virtual string ToShortTimeString() { throw new NotImplementedException(); }
        public virtual DateTimeBase ToUniversalTime() { throw new NotImplementedException(); }

        public virtual DateTimeBase Date
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Day
        {
            get { throw new NotImplementedException(); }
        }

        public virtual DayOfWeek DayOfWeek
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int DayOfYear
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Hour
        {
            get { throw new NotImplementedException(); }
        }

        public virtual DateTimeKind Kind
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Millisecond
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Minute
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Month
        {
            get { throw new NotImplementedException(); }
        }

        public virtual int Second
        {
            get { throw new NotImplementedException(); }
        }

        public virtual long Ticks
        {
            get { throw new NotImplementedException(); }
        }

        public virtual TimeSpan TimeOfDay
        {
            get { throw new NotImplementedException(); }
        }

        public abstract DateTime WrappedValue { get; }

        public virtual int Year
        {
            get { throw new NotImplementedException(); }
        }
    }
}
