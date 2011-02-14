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
namespace System
{
    /// <summary>
    /// DateTimeEx
    /// </summary>
	public static class DateTimeEx
	{
        public static IEnumerable<DateTime> GetDatesInRange(DateTime startDate, DateTime endDate, Func<DateTime, DateTime> accumulator)
        {
            for (var date = startDate; date <= endDate; date = accumulator(date))
                yield return date;
        }

        public static IEnumerable<WeekOfMonth> GetWeeksOfMonth(DateTime startDate, DateTime endDate)
        {
            var startOfFirstMonth = startDate.AddDays(-startDate.Day + 1);
            var endOfLastMonth = startOfFirstMonth.AddMonths(1).AddDays(-1);
            int week = 1;
            for (DateTime startOfWeek = startOfFirstMonth, endOfWeek = startOfFirstMonth.AddDays(6 - (int)startOfFirstMonth.DayOfWeek);
                (startOfWeek <= endOfLastMonth);
                endOfWeek = endOfWeek.AddDays(7), startOfWeek = endOfWeek.AddDays(-6), week++)
            {
                var clippedEndOfWeek = (endOfWeek.Month == startOfWeek.Month ? endOfWeek : endOfLastMonth);
                var minStartDate = (startOfWeek < startDate ? startOfWeek : startDate);
                var minEndDate = (clippedEndOfWeek < endDate ? clippedEndOfWeek : endDate);
                if ((startOfWeek <= endDate) && (endOfWeek >= startDate))
                    yield return new WeekOfMonth
                    {
                        StartDate = minStartDate,
                        EndDate = minEndDate,
                        Week = week,
                        LastWeekOfMonth = (endOfWeek.Month != startOfWeek.Month),
                    };
                // advance to next month
                if (endOfWeek.Month != startOfWeek.Month)
                    week = 1;
            }
        }

        public struct WeekOfMonth
        {
            public DateTime StartDate;
            public DateTime EndDate;
            public int Week;
            public bool LastWeekOfMonth;
        }
	}
}
