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
using System.Linq;
using System.Collections.Generic;
namespace System.Patterns.Reporting
{
    /// <summary>
    /// MultiColumnLayout
    /// </summary>
    public class MultiColumnLayout
    {
        private ColumnBreak[] _columnBreaks;

        protected struct ColumnBreak
        {
            public int IdealIndex;
            public int BestOffset;
            public object BestGroupValue;
        }

        public MultiColumnLayout(int columns)
        {
            Columns = columns;
            _columnBreaks = new ColumnBreak[columns - 1];
            for (int columnBreakIndex = 0; columnBreakIndex < _columnBreaks.Length; columnBreakIndex++)
                _columnBreaks[columnBreakIndex] = new ColumnBreak() { BestOffset = int.MaxValue };
        }

        public void AdjustFromObject<T>(IEnumerable<T> items, Func<T, object> groupValue)
        {
            int itemCount;
            if ((items == null) || ((itemCount = items.Count()) == 0))
                return;
            // find ideal breaks
            decimal segmentSize = (itemCount / Columns);
            for (int columnBreakIndex = 0; columnBreakIndex < _columnBreaks.Length; columnBreakIndex++)
                _columnBreaks[columnBreakIndex].IdealIndex = (int)(segmentSize * (columnBreakIndex + 1));
            object lastGroupValue = null;
            // find actual breaks
            int itemIndex = 0;
            foreach (T item in items)
            {
                itemIndex++;
                if (IsGroupValueEqual(groupValue(item), ref lastGroupValue, true))
                    continue;
                // group has changed
                for (int columnBreakIndex = 0; columnBreakIndex < _columnBreaks.Length; columnBreakIndex++)
                {
                    var columnBreak = _columnBreaks[columnBreakIndex];
                    int offset = Math.Abs(columnBreak.IdealIndex - itemIndex);
                    if (columnBreak.BestOffset > offset)
                    {
                        columnBreak.BestOffset = offset;
                        columnBreak.BestGroupValue = lastGroupValue;
                    }
                    _columnBreaks[columnBreakIndex] = columnBreak;
                }
            }
        }

        public int Columns { get; private set; }

        protected ColumnBreak[] ColumnBreaks
        {
            get { return _columnBreaks; }
        }

        public bool IsColumnBreak(object groupValue)
        {
            bool isColumnBreak = false;
            for (int columnBreakIndex = 0; columnBreakIndex < _columnBreaks.Length; columnBreakIndex++)
                if (IsGroupValueEqual(_columnBreaks[columnBreakIndex].BestGroupValue, ref groupValue, false))
                {
                    _columnBreaks[columnBreakIndex].BestGroupValue = null;
                    isColumnBreak = true;
                }
            return isColumnBreak;
        }

        public virtual bool IsGroupValueEqual(object groupValue, ref object groupValue2, bool isMakeValue2Equal)
        {
            int intGroupValue = (groupValue != null ? (int)groupValue : -1);
            int intGroupValue2 = (groupValue2 != null ? (int)groupValue2 : -1);
            if (intGroupValue == intGroupValue2)
                return true;
            if (isMakeValue2Equal)
                groupValue2 = intGroupValue;
            return false;
        }
    }

}