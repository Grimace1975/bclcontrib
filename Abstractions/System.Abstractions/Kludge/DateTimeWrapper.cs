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
    /// DateTimeWrapper
    /// </summary>
    public class DateTimeWrapper : DateTimeBase
    {
        private DateTime _dateTime;

        public DateTimeWrapper(DateTime dateTime)
        {
            if (dateTime == null)
                throw new ArgumentNullException("dateTime");
            _dateTime = dateTime;
        }

        public override DateTimeBase Add(TimeSpan value) { return new DateTimeWrapper(_dateTime.Add(value)); }
        public override DateTimeBase AddDays(double value) { return new DateTimeWrapper(_dateTime.AddDays(value)); }
        public override DateTimeBase AddHours(double value) { return new DateTimeWrapper(_dateTime.AddHours(value)); }
        public override DateTimeBase AddMilliseconds(double value) { return new DateTimeWrapper(_dateTime.AddMilliseconds(value)); }
        public override DateTimeBase AddMinutes(double value) { return new DateTimeWrapper(_dateTime.AddMinutes(value)); }
        public override DateTimeBase AddMonths(int months) { return new DateTimeWrapper(_dateTime.AddMonths(months)); }
        public override DateTimeBase AddSeconds(double value) { return new DateTimeWrapper(_dateTime.AddSeconds(value)); }
        public override DateTimeBase AddTicks(long value) { return new DateTimeWrapper(_dateTime.AddTicks(value)); }
        public override DateTimeBase AddYears(int value) { return new DateTimeWrapper(_dateTime.AddYears(value)); }
        public override int CompareTo(DateTimeBase value) { return _dateTime.CompareTo(value.WrappedValue); }
        public override int CompareTo(object value) { return _dateTime.CompareTo(value); }
        public override string[] GetDateTimeFormats() { return _dateTime.GetDateTimeFormats(); }
        public override string[] GetDateTimeFormats(char format) { return _dateTime.GetDateTimeFormats(format); }
        public override string[] GetDateTimeFormats(IFormatProvider provider) { return _dateTime.GetDateTimeFormats(provider); }
        public override string[] GetDateTimeFormats(char format, IFormatProvider provider) { return _dateTime.GetDateTimeFormats(format, provider); }
        public override bool IsDaylightSavingTime() { return _dateTime.IsDaylightSavingTime(); }
        public override TimeSpan Subtract(DateTime value) { return _dateTime.Subtract(value); }
        public override DateTimeBase Subtract(TimeSpan value) { return new DateTimeWrapper(_dateTime.Subtract(value)); }
        public override long ToBinary() { return _dateTime.ToBinary(); }
        public override long ToFileTime() { return _dateTime.ToFileTime(); }
        public override long ToFileTimeUtc() { return _dateTime.ToFileTimeUtc(); }
        public override DateTimeBase ToLocalTime() { return new DateTimeWrapper(_dateTime.ToLocalTime()); }
        public override string ToLongDateString() { return _dateTime.ToLongDateString(); }
        public override string ToLongTimeString() { return _dateTime.ToLongTimeString(); }
        public override double ToOADate() { return _dateTime.ToOADate(); }
        public override string ToShortDateString() { return _dateTime.ToShortDateString(); }
        public override string ToShortTimeString() { return _dateTime.ToShortTimeString(); }
        public override DateTimeBase ToUniversalTime() { return new DateTimeWrapper(_dateTime.ToUniversalTime()); }

        public override DateTimeBase Date
        {
            get { return new DateTimeWrapper(_dateTime.Date); }
        }

        public override int Day
        {
            get { return _dateTime.Day; }
        }

        public override DayOfWeek DayOfWeek
        {
            get { return _dateTime.DayOfWeek; }
        }

        public override int DayOfYear
        {
            get { return _dateTime.DayOfYear; }
        }

        public override int Hour
        {
            get { return _dateTime.Hour; }
        }

        public override DateTimeKind Kind
        {
            get { return _dateTime.Kind; }
        }

        public override int Millisecond
        {
            get { return _dateTime.Millisecond; }
        }

        public override int Minute
        {
            get { return _dateTime.Minute; }
        }

        public override int Month
        {
            get { return _dateTime.Month; }
        }

        public override int Second
        {
            get { return _dateTime.Second; }
        }

        public override long Ticks
        {
            get { return _dateTime.Ticks; }
        }

        public override TimeSpan TimeOfDay
        {
            get { return _dateTime.TimeOfDay; }
        }

        public override DateTime WrappedValue
        {
            get { return _dateTime; }
        }

        public override int Year
        {
            get { return _dateTime.Year; }
        }
    }
}
