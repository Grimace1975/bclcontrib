#region Foreign-License
//	LumenWorks.Framework.IO.CSV.CsvReader
//	Copyright (c) 2005 Sébastien Lorion
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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Debug = System.Diagnostics.Debug;
using System.Globalization;
using System.Diagnostics;
namespace System.IO.Csv
{
    /// <summary>
    /// Represents a reader that provides fast, non-cached, forward-only access to CSV data.  
    /// </summary>
    public partial class CsvReader : IDataReader, IEnumerable<string[]>, IDisposable
    {
        public const int DefaultBufferSize = 0x1000;
        private static readonly StringComparer s_fieldHeaderComparer = StringComparer.CurrentCultureIgnoreCase;
        private TextReader _r;
        private int _bSize;
        private CsvReaderSettings _settings;
        private char _delimiter;
        /// <summary>
        /// Indicates if the class is initialized.
        /// </summary>
        private bool _initialized;
        /// <summary>
        /// Contains the field headers.
        /// </summary>
        private string[] _fieldHeaders;
        /// <summary>
        /// Contains the dictionary of field indexes by header. The key is the field name and the value is its index.
        /// </summary>
        private Dictionary<string, int> _fieldHeaderIndexes;
        /// <summary>
        /// Contains the starting position of the next unread field.
        /// </summary>
        private int _nextFieldStart;
        /// <summary>
        /// Contains the index of the next unread field.
        /// </summary>
        private int _nextFieldIndex;
        /// <summary>
        /// Contains the array of the field values for the current record.
        /// A null value indicates that the field have not been parsed.
        /// </summary>
        private string[] _fields;
        /// <summary>
        /// Contains the maximum number of fields to retrieve for each record.
        /// </summary>
        private int _fieldCount;
        /// <summary>
        /// Contains the read buffer.
        /// </summary>
        private char[] _b;
        /// <summary>
        /// Contains the current read buffer length.
        /// </summary>
        private int _bLength;
        /// <summary>
        /// Indicates if the last read operation reached an EOL character.
        /// </summary>
        private bool _endOfLine;
        private bool _endOfStream;
        private long _currentRecordIndex;

        /// <summary>
        /// Indicates if the first record is in cache.
        /// This can happen when initializing a reader with no headers
        /// because one record must be read to get the field count automatically
        /// </summary>
        private bool _firstRecordInCache;

        public CsvReader(TextReader r)
            : this(r, new CsvReaderSettings { }, DefaultBufferSize) { }
        public CsvReader(TextReader r, bool hasHeaders)
            : this(r, new CsvReaderSettings { HasHeaders = hasHeaders }, DefaultBufferSize) { }
        public CsvReader(TextReader r, CsvReaderSettings settings)
            : this(r, settings, DefaultBufferSize) { }
        public CsvReader(TextReader r, CsvReaderSettings settings, int bufferSize)
        {
#if DEBUG
            _allocStack = new StackTrace();
#endif
            if (r == null)
                throw new ArgumentNullException("r");
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException("bufferSize", bufferSize, ExceptionMessage.BufferSizeTooSmall);
            _settings = settings;
            _delimiter = settings.Delimiter;
            _r = r;
            BufferSize = bufferSize;
            _currentRecordIndex = -1;
        }

        #region IDisposable

#if DEBUG
        /// <summary>
        /// Contains the stack when the object was allocated.
        /// </summary>
        private System.Diagnostics.StackTrace _allocStack;
#endif
        /// <summary>
        /// Contains the disposed status flag.
        /// </summary>
        private bool _isDisposed = false;
        /// <summary>
        /// Contains the locking object for multi-threading purpose.
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// Occurs when the instance is disposed of.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating whether the instance has been disposed of.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if the instance has been disposed of; otherwise, <see langword="false"/>.
        /// </value>
        [System.ComponentModel.Browsable(false)]
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// Raises the <see cref="M:Disposed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected virtual void OnDisposed(EventArgs e)
        {
            var handler = Disposed;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Checks if the instance has been disposed of, and if it has, throws an <see cref="T:System.ComponentModel.ObjectDisposedException"/>; otherwise, does nothing.
        /// </summary>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        /// 	The instance has been disposed of.
        /// </exception>
        /// <remarks>
        /// 	Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="M:Dispose()"/>.
        /// </remarks>
        protected void CheckDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
        }

        /// <summary>
        /// Releases all resources used by the instance.
        /// </summary>
        /// <remarks>
        /// 	Calls <see cref="M:Dispose(Boolean)"/> with the disposing parameter set to <see langword="true"/> to free unmanaged and managed resources.
        /// </remarks>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by this instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// 	<see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Refer to http://www.bluebytesoftware.com/blog/PermaLink,guid,88e62cdf-5919-4ac7-bc33-20c06ae539ae.aspx
            // Refer to http://www.gotdotnet.com/team/libraries/whitepapers/resourcemanagement/resourcemanagement.aspx
            // No exception should ever be thrown except in critical scenarios.
            // Unhandled exceptions during finalization will tear down the process.
            if (!_isDisposed)
                try
                {
                    // Dispose-time code should call Dispose() on all owned objects that implement the IDisposable interface. 
                    // "owned" means objects whose lifetime is solely controlled by the container. 
                    // In cases where ownership is not as straightforward, techniques such as HandleCollector can be used.  
                    // Large managed object fields should be nulled out.
                    // Dispose-time code should also set references of all owned objects to null, after disposing them. This will allow the referenced objects to be garbage collected even if not all references to the "parent" are released. It may be a significant memory consumption win if the referenced objects are large, such as big arrays, collections, etc. 
                    if (disposing)
                        // Acquire a lock on the object while disposing.
                        if (_r != null)
                            lock (_lock)
                                if (_r != null)
                                {
                                    _r.Dispose();
                                    _r = null;
                                    _b = null;
                                    _endOfStream = true;
                                }
                }
                finally
                {
                    // Ensure that the flag is set
                    _isDisposed = true;
                    // Catch any issues about firing an event on an already disposed object.
                    try
                    {
                        OnDisposed(EventArgs.Empty);
                    }
                    catch { }
                }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the instance is reclaimed by garbage collection.
        /// </summary>
        ~CsvReader()
        {
#if DEBUG
            Debug.WriteLine("FinalizableObject was not disposed" + _allocStack.ToString());
#endif
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// Gets the maximum number of fields to retrieve for each record.
        /// </summary>
        /// <value>The maximum number of fields to retrieve for each record.</value>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public int FieldCount
        {
            get
            {
                EnsureInitialize();
                return _fieldCount;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the current stream position is at the end of the stream.
        /// </summary>
        /// <value><see langword="true"/> if the current stream position is at the end of the stream; otherwise <see langword="false"/>.</value>
        //public override bool EndOfStream { get; private set; }

        /// <summary>
        /// Gets the field headers.
        /// </summary>
        /// <returns>The field headers or an empty array if headers are not supported.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public string[] GetFieldHeaders()
        {
            EnsureInitialize();
            Debug.Assert(_fieldHeaders != null, "Field headers must be non null.");
            var fieldHeaders = new string[_fieldHeaders.Length];
            Array.Copy(_fieldHeaders, fieldHeaders, _fieldHeaders.Length);
            return fieldHeaders;
        }

        /// <summary>
        /// Indicates if one or more field are missing for the current record.
        /// Resets after each successful record read.
        /// </summary>
        public bool MissingFieldFlag { get; private set; }

        /// <summary>
        /// Indicates if a parse error occured for the current record.
        /// Resets after each successful record read.
        /// </summary>
        public bool ParseErrorFlag { get; private set; }

        /// <summary>
        /// Gets the buffer size.
        /// </summary>
        public int BufferSize
        {
            get { return _bSize; }
            internal set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("bufferSize", value, ExceptionMessage.BufferSizeTooSmall);
                _bSize = value;
                if (_r is StreamReader)
                {
                    var stream = ((StreamReader)_r).BaseStream;
                    // Handle bad implementations returning 0 or less
                    if ((stream.CanSeek) && (stream.Length > 0))
                        _bSize = (int)Math.Min(value, stream.Length);
                }
            }
        }

        /// <summary>
        /// Occurs when there is an error while parsing the CSV stream.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ParseError;

        /// <summary>
        /// Raises the <see cref="M:ParseError"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ParseErrorEventArgs"/> that contains the event data.</param>
        protected virtual void OnParseError(ParseErrorEventArgs e)
        {
            var handler = ParseError;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Gets the current record index in the CSV file.
        /// </summary>
        /// <value>The current record index in the CSV file.</value>
        public virtual long CurrentRecordIndex
        {
            get { return _currentRecordIndex; }
        }

        /// <summary>
        /// Gets the field with the specified name and record position. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
        /// </summary>
        /// <value>
        /// The field with the specified name and record position.
        /// </value>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="field"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="T:InvalidOperationException">
        ///	The CSV does not have headers (<see cref="M:HasHeaders"/> property is <see langword="false"/>).
        /// </exception>
        /// <exception cref="T:ArgumentException">
        ///		<paramref name="field"/> not found.
        /// </exception>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		Record index must be > 0.
        /// </exception>
        /// <exception cref="T:InvalidOperationException">
        ///		Cannot move to a previous record in forward-only mode.
        /// </exception>
        /// <exception cref="T:EndOfStreamException">
        ///		Cannot read record at <paramref name="record"/>.
        ///	</exception>
        ///	<exception cref="T:MalformedCsvException">
        ///		The CSV appears to be corrupt at the current position.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public string this[int recordIndex, string field]
        {
            get
            {
                MoveTo(recordIndex);
                return this[field];
            }
        }

        /// <summary>
        /// Gets the field at the specified index and record position.
        /// </summary>
        /// <value>
        /// The field at the specified index and record position.
        /// A <see langword="null"/> is returned if the field cannot be found for the record.
        /// </value>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		<paramref name="field"/> must be included in [0, <see cref="M:FieldCount"/>[.
        /// </exception>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		Record index must be > 0.
        /// </exception>
        /// <exception cref="T:InvalidOperationException">
        ///		Cannot move to a previous record in forward-only mode.
        /// </exception>
        /// <exception cref="T:EndOfStreamException">
        ///		Cannot read record at <paramref name="recordIndex"/>.
        /// </exception>
        /// <exception cref="T:MalformedCsvException">
        ///		The CSV appears to be corrupt at the current position.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public string this[int recordIndex, int field]
        {
            get
            {
                MoveTo(recordIndex);
                return this[field];
            }
        }

        /// <summary>
        /// Gets the field with the specified name. <see cref="M:hasHeaders"/> must be <see langword="true"/>.
        /// </summary>
        /// <value>
        /// The field with the specified name.
        /// </value>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="field"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="T:InvalidOperationException">
        ///	The CSV does not have headers (<see cref="M:HasHeaders"/> property is <see langword="false"/>).
        /// </exception>
        /// <exception cref="T:ArgumentException">
        ///		<paramref name="field"/> not found.
        /// </exception>
        /// <exception cref="T:MalformedCsvException">
        ///		The CSV appears to be corrupt at the current position.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public string this[string field]
        {
            get
            {
                if (string.IsNullOrEmpty(field))
                    throw new ArgumentNullException("field");
                if (!_settings.HasHeaders)
                    throw new InvalidOperationException(ExceptionMessage.NoHeaders);
                int index = GetFieldIndex(field);
                if (index < 0)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldHeaderNotFound, field), "field");
                return this[index];
            }
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
        /// <exception cref="T:MalformedCsvException">
        ///		The CSV appears to be corrupt at the current position.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public virtual string this[int field]
        {
            get { return ReadField(field, false, false); }
        }

        public virtual bool EndOfStream
        {
            get { return _endOfStream; }
        }

        /// <summary>
        /// Ensures that the reader is initialized.
        /// </summary>
        private void EnsureInitialize()
        {
            if (!_initialized)
                Read(true, false);
            Debug.Assert(_fieldHeaders != null);
            Debug.Assert((_fieldHeaders.Length > 0) || ((_fieldHeaders.Length == 0) && (_fieldHeaderIndexes == null)));
        }

        /// <summary>
        /// Fills the buffer with data from the reader.
        /// </summary>
        /// <returns><see langword="true"/> if data was successfully read; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private bool ReadBuffer()
        {
            if (_endOfStream)
                return false;
            CheckDisposed();
            _bLength = _r.Read(_b, 0, _bSize);
            if (_bLength > 0)
                return true;
            _endOfStream = true;
            _b = null;
            return false;
        }

        /// <summary>
        /// Gets the field index for the provided header.
        /// </summary>
        /// <param name="header">The header to look for.</param>
        /// <returns>The field index for the provided header. -1 if not found.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public int GetFieldIndex(string header)
        {
            EnsureInitialize();
            int index;
            return ((_fieldHeaderIndexes != null) && (_fieldHeaderIndexes.TryGetValue(header, out index)) ? index : -1);
        }

        /// <summary>
        /// Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///		The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.
        /// </exception>
        public void CopyCurrentRecordTo(string[] array) { CopyCurrentRecordTo(array, 0); }
        /// <summary>
        /// Copies the field array of the current record to a one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array"> The one-dimensional <see cref="T:Array"/> that is the destination of the fields of the current record.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="array"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		<paramref name="index"/> is les than zero or is equal to or greater than the length <paramref name="array"/>. 
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///	No current record.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///		The number of fields in the record is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.
        /// </exception>
        public void CopyCurrentRecordTo(string[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if ((index < 0) || (index >= array.Length))
                throw new ArgumentOutOfRangeException("index", index, string.Empty);
            if ((CurrentRecordIndex < 0) || (!_initialized))
                throw new InvalidOperationException(ExceptionMessage.NoCurrentRecord);
            if ((array.Length - index) < _fieldCount)
                throw new ArgumentException(ExceptionMessage.NotEnoughSpaceInArray, "array");
            for (int fieldIndex = 0; fieldIndex < _fieldCount; fieldIndex++)
                array[index + fieldIndex] = (ParseErrorFlag ? null : this[fieldIndex]);
        }

        /// <summary>
        /// Gets the current raw CSV data.
        /// </summary>
        /// <remarks>Used for exception handling purpose.</remarks>
        /// <returns>The current raw CSV data.</returns>
        public string GetCurrentRawData()
        {
            return ((_b != null) && (_bLength > 0) ? new string(_b, 0, _bLength) : string.Empty);
        }

        /// <summary>
        /// Moves to the specified record index.
        /// </summary>
        /// <param name="recordIndex">The record index.</param>
        /// <exception cref="T:ArgumentOutOfRangeException">
        ///		Record index must be > 0.
        /// </exception>
        /// <exception cref="T:InvalidOperationException">
        ///		Cannot move to a previous record in forward-only mode.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public virtual void MoveTo(long recordIndex)
        {
            if (recordIndex < 0)
                throw new ArgumentOutOfRangeException("recordIndex", recordIndex, ExceptionMessage.RecordIndexLessThanZero);
            if (recordIndex < CurrentRecordIndex)
                throw new InvalidOperationException(ExceptionMessage.CannotMovePreviousRecordInForwardOnly);
            // Get number of record to read
            long offsetIndex = recordIndex - CurrentRecordIndex;
            if (offsetIndex > 0)
                do
                {
                    if (!Read())
                        throw new EndOfStreamException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.CannotReadRecordAtIndex, CurrentRecordIndex - offsetIndex));
                }
                while (--offsetIndex > 0);
        }

        /// <summary>
        /// Parses a new line delimiter.
        /// </summary>
        /// <param name="index">The starting position of the parsing. Will contain the resulting end position.</param>
        /// <returns><see langword="true"/> if a new line delimiter was found; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private bool ParseNewLine(ref int index)
        {
            Debug.Assert(index <= _bLength);
            // Check if already at the end of the buffer
            if (index == _bLength)
            {
                index = 0;
                if (!ReadBuffer())
                    return false;
            }
            char c = _b[index];
            // Treat \r as new line only if it's not the delimiter
            if ((c == '\r') && (_delimiter != '\r'))
            {
                index++;
                // Skip following \n (if there is one)
                if (index < _bLength)
                    if (_b[index] == '\n')
                        index++;
                    else if (ReadBuffer())
                        index = (_b[0] == '\n' ? 1 : 0);
                if (index >= _bLength)
                {
                    ReadBuffer();
                    index = 0;
                }
                return true;
            }
            else if (c == '\n')
            {
                index++;
                if (index >= _bLength)
                {
                    ReadBuffer();
                    index = 0;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the character at the specified position is a new line delimiter.
        /// </summary>
        /// <param name="index">The position of the character to verify.</param>
        /// <returns>
        /// 	<see langword="true"/> if the character at the specified position is a new line delimiter; otherwise, <see langword="false"/>.
        /// </returns>
        private bool IsNewLine(int index)
        {
            Debug.Assert(index < _bLength);
            char c = _b[index];
            return (c == '\n' ? true : ((c == '\r') && (_delimiter != '\r')));
        }

        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as white space.
        /// </summary>
        /// <param name="c">A Unicode character.</param>
        /// <returns><see langword="true"/> if <paramref name="c"/> is white space; otherwise, <see langword="false"/>.</returns>
        private bool IsWhiteSpace(char c)
        {
            // Handle cases where the delimiter is a whitespace (e.g. tab) then char.IsLatin1(char c) in {see Reflector}
            return (c == _delimiter ? false : (c <= '\x00ff' ? (c == ' ') || (c == '\t') : CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator));
        }

        /// <summary>
        /// Reads the field at the specified index.
        /// Any unread fields with an inferior index will also be read as part of the required parsing.
        /// </summary>
        /// <param name="field">The field index.</param>
        /// <param name="initializing">Indicates if the reader is currently initializing.</param>
        /// <param name="discardValue">Indicates if the value(s) are discarded.</param>
        /// <returns>
        /// The field at the specified index. 
        /// A <see langword="null"/> indicates that an error occured or that the last field has been reached during initialization.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///		<paramref name="field"/> is out of range.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///		There is no current record.
        /// </exception>
        /// <exception cref="MissingFieldCsvException">
        ///		The CSV data appears to be missing a field.
        /// </exception>
        /// <exception cref="MalformedCsvException">
        ///		The CSV data appears to be malformed.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private string ReadField(int field, bool initializing, bool discardValue)
        {
            if (!initializing)
            {
                if ((field < 0) || (field >= _fieldCount))
                    throw new ArgumentOutOfRangeException("field", field, string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldIndexOutOfRange, field));
                if (CurrentRecordIndex < 0)
                    throw new InvalidOperationException(ExceptionMessage.NoCurrentRecord);
                // Directly return field if cached
                if (_fields[field] != null)
                    return _fields[field];
                else if (MissingFieldFlag)
                    return HandleMissingField(null, field, ref _nextFieldStart);
            }
            CheckDisposed();
            var trimmingOptions = _settings.TrimmingOptions;
            var escape = _settings.Escape;
            var quote = _settings.Quote;
            int nextIndex = _nextFieldIndex;
            while (nextIndex < field + 1)
            {
                // Handle case where stated start of field is past buffer
                // This can occur because _nextFieldStart is simply 1 + last char position of previous field
                if (_nextFieldStart == _bLength)
                {
                    _nextFieldStart = 0;
                    // Possible EOF will be handled later (see Handle_EOF1)
                    ReadBuffer();
                }
                string value = null;
                if (MissingFieldFlag)
                    value = HandleMissingField(value, nextIndex, ref _nextFieldStart);
                else if (_nextFieldStart == _bLength)
                {
                    // Handle_EOF1: Handle EOF here
                    // If current field is the requested field, then the value of the field is "" as in "f1,f2,f3,(\s*)" otherwise, the CSV is malformed
                    if (nextIndex == field)
                    {
                        if (!discardValue)
                        {
                            value = string.Empty;
                            _fields[nextIndex] = value;
                        }
                    }
                    else
                        value = HandleMissingField(value, nextIndex, ref _nextFieldStart);
                }
                else
                {
                    // Trim spaces at start
                    if ((trimmingOptions & ValueTrimmingOptions.UnquotedOnly) != 0)
                        SkipWhiteSpaces(ref _nextFieldStart);
                    if (_endOfStream)
                        value = string.Empty;
                    else if (_b[_nextFieldStart] != quote)
                    {
                        // Non-quoted field
                        int startIndex = _nextFieldStart;
                        int index = _nextFieldStart;
                        for (; ; )
                        {
                            while (index < _bLength)
                            {
                                char c = _b[index];
                                if (c == _delimiter)
                                {
                                    _nextFieldStart = index + 1;
                                    break;
                                }
                                else if ((c == '\r') || (c == '\n'))
                                {
                                    _nextFieldStart = index;
                                    _endOfLine = true;
                                    break;
                                }
                                else
                                    index++;
                            }
                            if (index < _bLength)
                                break;
                            else
                            {
                                if (!discardValue)
                                    value += new string(_b, startIndex, index - startIndex);
                                startIndex = 0;
                                index = 0;
                                _nextFieldStart = 0;
                                if (!ReadBuffer())
                                    break;
                            }
                        }
                        if (!discardValue)
                        {
                            if ((trimmingOptions & ValueTrimmingOptions.UnquotedOnly) == 0)
                            {
                                if ((!_endOfStream) && (index > startIndex))
                                    value += new string(_b, startIndex, index - startIndex);
                            }
                            else
                            {
                                if ((!_endOfStream) && (index > startIndex))
                                {
                                    // Do the trimming
                                    index--;
                                    while ((index > -1) && (IsWhiteSpace(_b[index])))
                                        index--;
                                    index++;
                                    if (index > 0)
                                        value += new string(_b, startIndex, index - startIndex);
                                }
                                else
                                    index = -1;
                                // If pos <= 0, that means the trimming went past buffer start, and the concatenated value needs to be trimmed too.
                                if (index <= 0)
                                {
                                    index = (value == null ? -1 : value.Length - 1);
                                    // Do the trimming
                                    while ((index > -1) && (IsWhiteSpace(value[index])))
                                        index--;
                                    index++;
                                    if ((index > 0) && (index != value.Length))
                                        value = value.Substring(0, index);
                                }
                            }
                            if (value == null)
                                value = string.Empty;
                        }
                        if ((_endOfLine) || (_endOfStream))
                        {
                            _endOfLine = ParseNewLine(ref _nextFieldStart);
                            // Reaching a new line is ok as long as the parser is initializing or it is the last field
                            if ((!initializing) && (nextIndex != _fieldCount - 1))
                            {
                                if ((value != null) && (value.Length == 0))
                                    value = null;
                                value = HandleMissingField(value, nextIndex, ref _nextFieldStart);
                            }
                        }
                        if (!discardValue)
                            _fields[nextIndex] = value;
                    }
                    else
                    {
                        // Quoted field, Skip quote
                        int startIndex = _nextFieldStart + 1;
                        int index = startIndex;
                        bool quoted = true;
                        bool escaped = false;
                        if ((trimmingOptions & ValueTrimmingOptions.QuotedOnly) != 0)
                        {
                            SkipWhiteSpaces(ref startIndex);
                            index = startIndex;
                        }
                        for (; ; )
                        {
                            while (index < _bLength)
                            {
                                char c = _b[index];
                                if (escaped)
                                {
                                    escaped = false;
                                    startIndex = index;
                                }
                                // if current char is escape AND (escape and quote are different OR next char is a quote)
                                else if ((c == escape) && ((escape != quote) || ((index + 1 < _bLength) && (_b[index + 1] == quote)) || ((index + 1 == _bLength) && (_r.Peek() == quote))))
                                {
                                    if (!discardValue)
                                        value += new string(_b, startIndex, index - startIndex);
                                    escaped = true;
                                }
                                else if (c == quote)
                                {
                                    quoted = false;
                                    break;
                                }
                                index++;
                            }
                            if (!quoted)
                                break;
                            else
                            {
                                if ((!discardValue) && (!escaped))
                                    value += new string(_b, startIndex, index - startIndex);
                                startIndex = 0;
                                index = 0;
                                _nextFieldStart = 0;
                                if (!ReadBuffer())
                                {
                                    HandleParseError(new MalformedCsvException(GetCurrentRawData(), _nextFieldStart, Math.Max(0, CurrentRecordIndex), nextIndex), ref _nextFieldStart);
                                    return null;
                                }
                            }
                        }
                        if (!_endOfStream)
                        {
                            // Append remaining parsed buffer content
                            if (!discardValue && index > startIndex)
                                value += new string(_b, startIndex, index - startIndex);
                            if ((!discardValue) && (value != null) && ((trimmingOptions & ValueTrimmingOptions.QuotedOnly) != 0))
                            {
                                int newLength = value.Length;
                                while (newLength > 0 && IsWhiteSpace(value[newLength - 1]))
                                    newLength--;
                                if (newLength < value.Length)
                                    value = value.Substring(0, newLength);
                            }
                            // Skip quote
                            _nextFieldStart = index + 1;
                            // Skip whitespaces between the quote and the delimiter/eol
                            SkipWhiteSpaces(ref _nextFieldStart);
                            // Skip delimiter
                            bool delimiterSkipped;
                            if ((_nextFieldStart < _bLength) && (_b[_nextFieldStart] == _delimiter))
                            {
                                _nextFieldStart++;
                                delimiterSkipped = true;
                            }
                            else
                                delimiterSkipped = false;
                            // Skip new line delimiter if initializing or last field
                            // (if the next field is missing, it will be caught when parsed)
                            if ((!_endOfStream) && (!delimiterSkipped) && ((initializing) || (nextIndex == _fieldCount - 1)))
                                _endOfLine = ParseNewLine(ref _nextFieldStart);
                            // If no delimiter is present after the quoted field and it is not the last field, then it is a parsing error
                            if ((!delimiterSkipped) && (!_endOfStream) && !((_endOfLine) || (IsNewLine(_nextFieldStart))))
                                HandleParseError(new MalformedCsvException(GetCurrentRawData(), _nextFieldStart, Math.Max(0, CurrentRecordIndex), nextIndex), ref _nextFieldStart);
                        }
                        if (!discardValue)
                        {
                            if (value == null)
                                value = string.Empty;
                            _fields[nextIndex] = value;
                        }
                    }
                }
                _nextFieldIndex = Math.Max(nextIndex + 1, _nextFieldIndex);
                // If initializing, return null to signify the last field has been reached
                if (nextIndex == field)
                    return (initializing ? ((_endOfLine) || (_endOfStream) ? null : (string.IsNullOrEmpty(value) ? string.Empty : value)) : value);
                nextIndex++;
            }
            // Getting here is bad ...
            HandleParseError(new MalformedCsvException(GetCurrentRawData(), _nextFieldStart, Math.Max(0, CurrentRecordIndex), nextIndex), ref _nextFieldStart);
            return null;
        }

        public IEnumerable<TSource> AsEnumerable<TSource>(Func<CsvReader, TSource> selector)
            where TSource : new()
        {
            while (Read())
                yield return selector(this);
        }

        /// <summary>
        /// Reads the next record.
        /// </summary>
        /// <returns><see langword="true"/> if a record has been successfully reads; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public bool Read() { return Read(false, false); }
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
        protected virtual bool Read(bool onlyReadHeaders, bool skipToNextLine)
        {
            if (_endOfStream)
            {
                if (_firstRecordInCache)
                {
                    _firstRecordInCache = false;
                    _currentRecordIndex++;
                    return true;
                }
                return false;
            }
            CheckDisposed();
            if (!_initialized)
            {
                _b = new char[_bSize];
                // will be replaced if and when headers are read
                _fieldHeaders = new string[0];
                if (!ReadBuffer())
                    return false;
                if (!SkipEmptyAndCommentedLines(ref _nextFieldStart))
                    return false;
                // Keep growing _fields array until the last field has been found and then resize it to its final correct size
                _fieldCount = 0;
                _fields = new string[16];
                while (ReadField(_fieldCount, true, false) != null)
                    if (ParseErrorFlag)
                    {
                        _fieldCount = 0;
                        Array.Clear(_fields, 0, _fields.Length);
                        ParseErrorFlag = false;
                        _nextFieldIndex = 0;
                    }
                    else
                    {
                        _fieldCount++;
                        if (_fieldCount == _fields.Length)
                            Array.Resize<string>(ref _fields, (_fieldCount + 1) * 2);
                    }
                // _fieldCount contains the last field index, but it must contains the field count, so increment by 1
                _fieldCount++;
                if (_fields.Length != _fieldCount)
                    Array.Resize<string>(ref _fields, _fieldCount);
                _initialized = true;
                // If headers are present, call ReadNextRecord again
                if (_settings.HasHeaders)
                {
                    // Don't count first record as it was the headers
                    _currentRecordIndex = -1;
                    _firstRecordInCache = false;
                    _fieldHeaders = new string[_fieldCount];
                    _fieldHeaderIndexes = new Dictionary<string, int>(_fieldCount, s_fieldHeaderComparer);
                    for (int fieldIndex = 0; fieldIndex < _fields.Length; fieldIndex++)
                    {
                        _fieldHeaders[fieldIndex] = _fields[fieldIndex];
                        _fieldHeaderIndexes.Add(_fields[fieldIndex], fieldIndex);
                    }
                    // Proceed to first record
                    if (!onlyReadHeaders)
                    {
                        // Calling again ReadNextRecord() seems to be simpler, but in fact would probably cause many subtle bugs because
                        // the derived does not expect a recursive behavior so simply do what is needed here and no more.
                        if (!SkipEmptyAndCommentedLines(ref _nextFieldStart))
                            return false;
                        Array.Clear(_fields, 0, _fields.Length);
                        _nextFieldIndex = 0;
                        _endOfLine = false;
                        _currentRecordIndex++;
                        return true;
                    }
                }
                else
                {
                    if (onlyReadHeaders)
                    {
                        _firstRecordInCache = true;
                        _currentRecordIndex = -1;
                    }
                    else
                    {
                        _firstRecordInCache = false;
                        _currentRecordIndex = 0;
                    }
                }
            }
            else
            {
                if (skipToNextLine)
                    SkipToNextLine(ref _nextFieldStart);
                else if ((CurrentRecordIndex > -1) && (!MissingFieldFlag))
                {
                    // If not already at end of record, move there
                    if ((!_endOfLine) && (!_endOfStream))
                    {
                        if (!_settings.SupportsMultiline)
                            SkipToNextLine(ref _nextFieldStart);
                        else
                            // a dirty trick to handle the case where extra fields are present
                            while (ReadField(_nextFieldIndex, true, true) != null) ;
                    }
                }
                if ((!_firstRecordInCache) && (!SkipEmptyAndCommentedLines(ref _nextFieldStart)))
                    return false;
                if ((_settings.HasHeaders) || (!_firstRecordInCache))
                    _endOfLine = false;
                // Check to see if the first record is in cache. This can happen when initializing a reader with no headers
                // because one record must be read to get the field count automatically
                if (_firstRecordInCache)
                    _firstRecordInCache = false;
                else
                {
                    Array.Clear(_fields, 0, _fields.Length);
                    _nextFieldIndex = 0;
                }
                MissingFieldFlag = false;
                ParseErrorFlag = false;
                _currentRecordIndex++;
            }
            return true;
        }

        /// <summary>
        /// Skips empty and commented lines.
        /// If the end of the buffer is reached, its content be discarded and filled again from the reader.
        /// </summary>
        /// <param name="index">
        /// The position in the buffer where to start parsing. 
        /// Will contains the resulting position after the operation.
        /// </param>
        /// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private bool SkipEmptyAndCommentedLines(ref int index)
        {
            if (index < _bLength)
                DoSkipEmptyAndCommentedLines(ref index);
            while ((index >= _bLength) && (!_endOfStream))
                if (ReadBuffer())
                {
                    index = 0;
                    DoSkipEmptyAndCommentedLines(ref index);
                }
                else
                    return false;
            return !_endOfStream;
        }

        /// <summary>
        /// <para>Worker method.</para>
        /// <para>Skips empty and commented lines.</para>
        /// </summary>
        /// <param name="index">
        /// The position in the buffer where to start parsing. 
        /// Will contains the resulting position after the operation.
        /// </param>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private void DoSkipEmptyAndCommentedLines(ref int index)
        {
            while (index < _bLength)
                if (_b[index] == _settings.Comment)
                {
                    index++;
                    SkipToNextLine(ref index);
                }
                else if ((_settings.SkipEmptyLines) && (ParseNewLine(ref index)))
                    continue;
                else
                    break;
        }

        /// <summary>
        /// Skips whitespace characters.
        /// </summary>
        /// <param name="index">The starting position of the parsing. Will contain the resulting end position.</param>
        /// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private bool SkipWhiteSpaces(ref int index)
        {
            for (; ; )
            {
                while ((index < _bLength) && (IsWhiteSpace(_b[index])))
                    index++;
                if (index < _bLength)
                    break;
                else
                {
                    index = 0;
                    if (!ReadBuffer())
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Skips ahead to the next NewLine character.
        /// If the end of the buffer is reached, its content be discarded and filled again from the reader.
        /// </summary>
        /// <param name="index">
        /// The position in the buffer where to start parsing. 
        /// Will contains the resulting position after the operation.
        /// </param>
        /// <returns><see langword="true"/> if the end of the reader has not been reached; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        private bool SkipToNextLine(ref int index)
        {
            // ((index = 0) == 0) is a little trick to reset position inline
            while (((index < _bLength) || (ReadBuffer() && ((index = 0) == 0))) && (!ParseNewLine(ref index)))
                index++;
            return !_endOfStream;
        }

        /// <summary>
        /// Handles a parsing error.
        /// </summary>
        /// <param name="error">The parsing error that occured.</param>
        /// <param name="index">The current position in the buffer.</param>
        /// <exception cref="ArgumentNullException">
        ///	<paramref name="error"/> is <see langword="null"/>.
        /// </exception>
        private void HandleParseError(MalformedCsvException error, ref int index)
        {
            if (error == null)
                throw new ArgumentNullException("error");
            ParseErrorFlag = true;
            switch (_settings.DefaultParseErrorAction)
            {
                case ParseErrorAction.ThrowException:
                    throw error;
                case ParseErrorAction.RaiseEvent:
                    var e = new ParseErrorEventArgs(error, ParseErrorAction.ThrowException);
                    OnParseError(e);
                    switch (e.Action)
                    {
                        case ParseErrorAction.ThrowException:
                            throw e.Error;
                        case ParseErrorAction.RaiseEvent:
                            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.ParseErrorActionInvalidInsideParseErrorEvent, e.Action), e.Error);
                        case ParseErrorAction.AdvanceToNextLine:
                            // already at EOL when fields are missing, so don't skip to next line in that case
                            if ((!MissingFieldFlag) && (index >= 0))
                                SkipToNextLine(ref index);
                            break;
                        default:
                            throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.ParseErrorActionNotSupported, e.Action), e.Error);
                    }
                    break;
                case ParseErrorAction.AdvanceToNextLine:
                    // already at EOL when fields are missing, so don't skip to next line in that case
                    if ((!MissingFieldFlag) && (index >= 0))
                        SkipToNextLine(ref index);
                    break;
                default:
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.ParseErrorActionNotSupported, _settings.DefaultParseErrorAction), error);
            }
        }

        /// <summary>
        /// Handles a missing field error.
        /// </summary>
        /// <param name="value">The partially parsed value, if available.</param>
        /// <param name="fieldIndex">The missing field index.</param>
        /// <param name="currentPosition">The current position in the raw data.</param>
        /// <returns>
        /// The resulting value according to <see cref="M:MissingFieldAction"/>.
        /// If the action is set to <see cref="T:MissingFieldAction.TreatAsParseError"/>,
        /// then the parse error will be handled according to <see cref="DefaultParseErrorAction"/>.
        /// </returns>
        private string HandleMissingField(string value, int fieldIndex, ref int currentPosition)
        {
            if ((fieldIndex < 0) || (fieldIndex >= _fieldCount))
                throw new ArgumentOutOfRangeException("fieldIndex", fieldIndex, string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldIndexOutOfRange, fieldIndex));
            MissingFieldFlag = true;
            for (int i = fieldIndex + 1; i < _fieldCount; i++)
                _fields[i] = null;
            if (value != null)
                return value;
            else
            {
                switch (_settings.MissingFieldAction)
                {
                    case MissingFieldAction.ParseError:
                        HandleParseError(new MissingFieldCsvException(GetCurrentRawData(), currentPosition, Math.Max(0, CurrentRecordIndex), fieldIndex), ref currentPosition);
                        return value;
                    case MissingFieldAction.ReplaceByEmpty:
                        return string.Empty;
                    case MissingFieldAction.ReplaceByNull:
                        return null;
                    default:
                        throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.MissingFieldActionNotSupported, _settings.MissingFieldAction));
                }
            }
        }

        #region IDataReader

        /// <summary>
        /// Validates the state of the data reader.
        /// </summary>
        /// <param name="validations">The validations to accomplish.</param>
        /// <exception cref="InvalidOperationException">
        ///	No current record.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///	This operation is invalid when the reader is closed.
        /// </exception>
        private void ValidateDataReader(DataReaderValidations validations)
        {
            if (((validations & DataReaderValidations.IsInitialized) != 0) && (!_initialized))
                throw new InvalidOperationException(ExceptionMessage.NoCurrentRecord);
            if (((validations & DataReaderValidations.IsNotClosed) != 0) && (_isDisposed))
                throw new InvalidOperationException(ExceptionMessage.ReaderClosed);
        }

        /// <summary>
        /// Copy the value of the specified field to an array.
        /// </summary>
        /// <param name="field">The index of the field.</param>
        /// <param name="fieldOffset">The offset in the field value.</param>
        /// <param name="destinationArray">The destination array where the field value will be copied.</param>
        /// <param name="destinationOffset">The destination array offset.</param>
        /// <param name="length">The number of characters to copy from the field value.</param>
        /// <returns></returns>
        private long CopyFieldToArray(int field, long fieldOffset, Array destinationArray, int destinationOffset, int length)
        {
            EnsureInitialize();
            if ((field < 0) || (field >= _fieldCount))
                throw new ArgumentOutOfRangeException("field", field, string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldIndexOutOfRange, field));
            if ((fieldOffset < 0) || (fieldOffset >= int.MaxValue))
                throw new ArgumentOutOfRangeException("fieldOffset");
            // Array.Copy(...) will do the remaining argument checks
            if (length == 0)
                return 0;
            string value = (this[field] ?? string.Empty);
            Debug.Assert(fieldOffset < int.MaxValue);
            Debug.Assert((destinationArray.GetType() == typeof(char[])) || (destinationArray.GetType() == typeof(byte[])));
            if (destinationArray.GetType() == typeof(char[]))
                Array.Copy(value.ToCharArray((int)fieldOffset, length), 0, destinationArray, destinationOffset, length);
            else
            {
                var chars = value.ToCharArray((int)fieldOffset, length);
                var source = new byte[chars.Length];
                for (int index = 0; index < chars.Length; index++)
                    source[index] = Convert.ToByte(chars[index]);
                Array.Copy(source, 0, destinationArray, destinationOffset, length);
            }
            return length;
        }

        int IDataReader.RecordsAffected
        {
            // For SELECT statements, -1 must be returned.
            get { return -1; }
        }

        bool IDataReader.IsClosed
        {
            get { return _endOfStream; }
        }

        bool IDataReader.NextResult()
        {
            ValidateDataReader(DataReaderValidations.IsNotClosed);
            return false;
        }

        void IDataReader.Close()
        {
            Dispose();
        }

        bool IDataReader.Read()
        {
            ValidateDataReader(DataReaderValidations.IsNotClosed);
            return Read();
        }

        int IDataReader.Depth
        {
            get
            {
                ValidateDataReader(DataReaderValidations.IsNotClosed);
                return 0;
            }
        }

        DataTable IDataReader.GetSchemaTable()
        {
            EnsureInitialize();
            ValidateDataReader(DataReaderValidations.IsNotClosed);
            var schema = new DataTable("SchemaTable")
            {
                Locale = CultureInfo.InvariantCulture,
                MinimumCapacity = _fieldCount,
            };
            var columns = schema.Columns;
            columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableColumn.BaseColumnName, typeof(string)).ReadOnly = true;
            columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string)).ReadOnly = true;
            columns.Add(SchemaTableColumn.BaseTableName, typeof(string)).ReadOnly = true;
            columns.Add(SchemaTableColumn.ColumnName, typeof(string)).ReadOnly = true;
            columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int)).ReadOnly = true;
            columns.Add(SchemaTableColumn.ColumnSize, typeof(int)).ReadOnly = true;
            columns.Add(SchemaTableColumn.DataType, typeof(object)).ReadOnly = true;
            columns.Add(SchemaTableColumn.IsAliased, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableColumn.IsExpression, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableColumn.IsKey, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableColumn.IsLong, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableColumn.IsUnique, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableColumn.NumericPrecision, typeof(short)).ReadOnly = true;
            columns.Add(SchemaTableColumn.NumericScale, typeof(short)).ReadOnly = true;
            columns.Add(SchemaTableColumn.ProviderType, typeof(int)).ReadOnly = true;
            columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string)).ReadOnly = true;
            columns.Add(SchemaTableOptionalColumn.BaseServerName, typeof(string)).ReadOnly = true;
            columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableOptionalColumn.IsHidden, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool)).ReadOnly = true;
            columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool)).ReadOnly = true;
            string[] columnNames;
            if (_settings.HasHeaders)
                columnNames = _fieldHeaders;
            else
            {
                columnNames = new string[_fieldCount];
                for (int fieldIndex = 0; fieldIndex < _fieldCount; fieldIndex++)
                    columnNames[fieldIndex] = "Column" + fieldIndex.ToString(CultureInfo.InvariantCulture);
            }
            // null marks columns that will change for each row
            object[] schemaRow = new object[]
            { 
			    true,					// 00- AllowDBNull
			    null,					// 01- BaseColumnName
			    string.Empty,			// 02- BaseSchemaName
			    string.Empty,			// 03- BaseTableName
			    null,					// 04- ColumnName
			    null,					// 05- ColumnOrdinal
			    int.MaxValue,			// 06- ColumnSize
			    typeof(string),			// 07- DataType
			    false,					// 08- IsAliased
			    false,					// 09- IsExpression
			    false,					// 10- IsKey
			    false,					// 11- IsLong
			    false,					// 12- IsUnique
			    DBNull.Value,			// 13- NumericPrecision
			    DBNull.Value,			// 14- NumericScale
			    (int) DbType.String,	// 15- ProviderType
			    string.Empty,			// 16- BaseCatalogName
			    string.Empty,			// 17- BaseServerName
			    false,					// 18- IsAutoIncrement
			    false,					// 19- IsHidden
			    true,					// 20- IsReadOnly
			    false					// 21- IsRowVersion
            };
            for (int columnNameIndex = 0; columnNameIndex < columnNames.Length; columnNameIndex++)
            {
                schemaRow[1] = columnNames[columnNameIndex]; // Base column name
                schemaRow[4] = columnNames[columnNameIndex]; // Column name
                schemaRow[5] = columnNameIndex; // Column ordinal
                schema.Rows.Add(schemaRow);
            }
            return schema;
        }

        int IDataRecord.GetInt32(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Int32.Parse(this[i] ?? string.Empty, CultureInfo.CurrentCulture);
        }

        object IDataRecord.this[string name]
        {
            get
            {
                ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
                return this[name];
            }
        }

        object IDataRecord.this[int i]
        {
            get
            {
                ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
                return this[i];
            }
        }

        object IDataRecord.GetValue(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return (((IDataRecord)this).IsDBNull(i) ? (object)DBNull.Value : this[i]);
        }

        bool IDataRecord.IsDBNull(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return (string.IsNullOrEmpty(this[i]));
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return CopyFieldToArray(i, fieldOffset, buffer, bufferoffset, length);
        }

        byte IDataRecord.GetByte(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Byte.Parse(this[i], CultureInfo.CurrentCulture);
        }

        Type IDataRecord.GetFieldType(int field)
        {
            EnsureInitialize();
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            if ((field < 0) || (field >= _fieldCount))
                throw new ArgumentOutOfRangeException("i", field, string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldIndexOutOfRange, field));
            return typeof(string);
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Decimal.Parse(this[i], CultureInfo.CurrentCulture);
        }

        int IDataRecord.GetValues(object[] values)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            var record = (IDataRecord)this;
            for (int field = 0; field < _fieldCount; field++)
                values[field] = record.GetValue(field);
            return _fieldCount;
        }

        string IDataRecord.GetName(int i)
        {
            EnsureInitialize();
            ValidateDataReader(DataReaderValidations.IsNotClosed);
            if ((i < 0) || (i >= _fieldCount))
                throw new ArgumentOutOfRangeException("i", i, string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldIndexOutOfRange, i));
            return (_settings.HasHeaders ? _fieldHeaders[i] : "Column" + i.ToString(CultureInfo.InvariantCulture));
        }

        long IDataRecord.GetInt64(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Int64.Parse(this[i], CultureInfo.CurrentCulture);
        }

        double IDataRecord.GetDouble(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Double.Parse(this[i], CultureInfo.CurrentCulture);
        }

        bool IDataRecord.GetBoolean(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            string value = this[i];
            int result;
            return (Int32.TryParse(value, out result) ? result != 0 : Boolean.Parse(value));
        }

        Guid IDataRecord.GetGuid(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return new Guid(this[i]);
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return DateTime.Parse(this[i], CultureInfo.CurrentCulture);
        }

        int IDataRecord.GetOrdinal(string name)
        {
            EnsureInitialize();
            ValidateDataReader(DataReaderValidations.IsNotClosed);
            int index;
            if (!_fieldHeaderIndexes.TryGetValue(name, out index))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, ExceptionMessage.FieldHeaderNotFound, name), "name");
            return index;
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return typeof(string).FullName;
        }

        float IDataRecord.GetFloat(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Single.Parse(this[i], CultureInfo.CurrentCulture);
        }

        IDataReader IDataRecord.GetData(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return (i == 0 ? this : null);
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return CopyFieldToArray(i, fieldoffset, buffer, bufferoffset, length);
        }

        string IDataRecord.GetString(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return this[i];
        }

        char IDataRecord.GetChar(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Char.Parse(this[i]);
        }

        short IDataRecord.GetInt16(int i)
        {
            ValidateDataReader(DataReaderValidations.IsInitialized | DataReaderValidations.IsNotClosed);
            return Int16.Parse(this[i], CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns an <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.
        /// </summary>
        /// <returns>An <see cref="T:RecordEnumerator"/>  that can iterate through CSV records.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public CsvReader.RecordEnumerator GetEnumerator()
        {
            return new CsvReader.RecordEnumerator(this);
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through CSV records.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        IEnumerator<string[]> IEnumerable<string[]>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/>  that can iterate through CSV records.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}