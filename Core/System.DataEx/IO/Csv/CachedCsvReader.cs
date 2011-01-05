#region Foreign-License
//	LumenWorks.Framework.IO.CSV.CachedCsvReader
//	Copyright (c) 2005 S�bastien Lorion
//
//	MIT license (http://en.wikipedia.org/wiki/MIT_License)
//
//	Permission is hereby granted, free of charge, to any person obtaining a copy
//	of this software and associated documentation files (the "Software"), to deal
//	in the Software without restriction, including without limitation the rights 
//	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//	of the Software, and to permit persons to whom the Software is furnished to do so, 
//	subject to the following conditions:
//
//	The above copyright notice and this permission notice shall be included in all 
//	copies or substantial portions of the Software.
//
//	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//	INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//	PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
//	FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//	ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Modified: Sky Morey <moreys@digitalev.com>
//
#endregion
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
namespace System.IO.Csv
{
    /// <summary>
    /// Represents a reader that provides fast, cached, dynamic access to CSV data.
    /// </summary>
    /// <remarks>The number of records is limited to <see cref="System.Int32.MaxValue"/> - 1.</remarks>
    public partial class CachedCsvReader : CsvReader, IListSource
    {
        private List<string[]> _records;
        private long _currentRecordIndex;
        private bool _readingStream;
        private CsvBindingList _bindingList;

        public CachedCsvReader(TextReader r)
            : this(r, new CsvReaderSettings { }, CsvReader.DefaultBufferSize) { }
        public CachedCsvReader(TextReader r, bool hasHeaders)
            : this(r, new CsvReaderSettings { HasHeaders = hasHeaders }, CsvReader.DefaultBufferSize) { }
        public CachedCsvReader(TextReader r, CsvReaderSettings settings)
            : this(r, settings, DefaultBufferSize) { }
        public CachedCsvReader(TextReader r, CsvReaderSettings settings, int bufferSize)
            : base(r, settings, bufferSize)
        {
            _records = new List<string[]>();
            _currentRecordIndex = -1;
        }

        /// <summary>
        /// Gets the current record index in the CSV file.
        /// </summary>
        /// <value>The current record index in the CSV file.</value>
        public override long CurrentRecordIndex
        {
            get { return _currentRecordIndex; }
        }

        /// <summary>
        /// Gets a value that indicates whether the current stream position is at the end of the stream.
        /// </summary>
        /// <value><see langword="true"/> if the current stream position is at the end of the stream; otherwise <see langword="false"/>.</value>
        public override bool EndOfStream
        {
            get { return (_currentRecordIndex < base.CurrentRecordIndex ? false : base.EndOfStream); }
        }

        /// <summary>
        /// Gets the field at the specified index.
        /// </summary>
        /// <value>The field at the specified index.</value>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		<paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.
        /// </exception>
        /// <exception cref="T:InvalidOperationException">
        ///		No record read yet. Call ReadLine() first.
        /// </exception>
        /// <exception cref="MissingFieldCsvException">
        ///		The CSV data appears to be missing a field.
        /// </exception>
        /// <exception cref="T:MalformedCsvException">
        ///		The CSV appears to be corrupt at the current position.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///		The instance has been disposed of.
        /// </exception>
        public override String this[int field]
        {
            get
            {
                if (_readingStream)
                    return base[field];
                else if (_currentRecordIndex > -1)
                {
                    if (field > -1 && field < this.FieldCount)
                        return _records[(int)_currentRecordIndex][field];
                    throw new ArgumentOutOfRangeException("field", field, string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldIndexOutOfRange, field));
                }
                throw new InvalidOperationException(ExceptionMessage.NoCurrentRecord);
            }
        }

        /// <summary>
        /// Reads the CSV stream from the current position to the end of the stream.
        /// </summary>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public virtual void ReadToEnd()
        {
            _currentRecordIndex = base.CurrentRecordIndex;
            while (Read()) ;
        }

        /// <summary>
        /// Reads the next record.
        /// </summary>
        /// <param name="onlyReadHeaders">
        /// Indicates if the reader will proceed to the next record after having read headers.
        /// <see langword="true"/> if it stops after having read headers; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="skipToNextLine">
        /// Indicates if the reader will skip directly to the next line without parsing the current one. 
        /// To be used when an error occurs.
        /// </param>
        /// <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        protected override bool Read(bool onlyReadHeaders, bool skipToNextLine)
        {
            if (_currentRecordIndex < base.CurrentRecordIndex)
            {
                _currentRecordIndex++;
                return true;
            }
            _readingStream = true;
            try
            {
                bool canRead = base.Read(onlyReadHeaders, skipToNextLine);
                if (canRead)
                {
                    string[] record = new string[this.FieldCount];
                    if (base.CurrentRecordIndex > -1)
                    {
                        CopyCurrentRecordTo(record);
                        _records.Add(record);
                    }
                    else
                    {
                        MoveTo(0);
                        CopyCurrentRecordTo(record);
                        MoveTo(-1);
                    }
                    if (!onlyReadHeaders)
                        _currentRecordIndex++;
                }
                else
                    // No more records to read, so set array size to only what is needed
                    _records.Capacity = _records.Count;
                return canRead;
            }
            finally { _readingStream = false; }
        }

        /// <summary>
        /// Moves before the first record.
        /// </summary>
        public void MoveToStart()
        {
            _currentRecordIndex = -1;
        }

        /// <summary>
        /// Moves to the last record read so far.
        /// </summary>
        public void MoveToLastCachedRecord()
        {
            _currentRecordIndex = base.CurrentRecordIndex;
        }

        /// <summary>
        /// Moves to the specified record index.
        /// </summary>
        /// <param name="recordIndex">The record index.</param>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		Record index must be > 0.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///		The instance has been disposed of.
        /// </exception>
        public override void MoveTo(long recordIndex)
        {
            if (recordIndex < -1)
                throw new ArgumentOutOfRangeException("record", recordIndex, ExceptionMessage.RecordIndexLessThanZero);
            if (recordIndex <= base.CurrentRecordIndex)
                _currentRecordIndex = recordIndex;
            else
            {
                _currentRecordIndex = base.CurrentRecordIndex;
                long offset = recordIndex - _currentRecordIndex;
                // read to the last record before the one we want
                while ((offset-- > 0) && (Read())) ;
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        IList IListSource.GetList()
        {
            if (_bindingList == null)
                _bindingList = new CsvBindingList(this);
            return _bindingList;
        }
    }
}
