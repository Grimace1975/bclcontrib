#if !CODE_ANALYSIS
namespace System
#else
using System;
namespace SystemEx
#endif
{
    /// <summary>
    /// sizebuf_t
    /// </summary>
    public partial class ByteBuilder
    {
        public bool CanOverflow;
        public bool HasOverflowed;
        public byte[] Data;
        public int MaxCapacity;
        public int Length;
        public int ReadIndex;

        public ByteBuilder(byte[] data, int length)
        {
            Data = data;
            MaxCapacity = length;
        }

        private static ErrorHandler _errorHandler;
        public static ErrorHandler ErrorHandler
        {
            get { return _errorHandler; }
            set { _errorHandler = value; }
        }

        public void Clear()
        {
            if (Data != null)
                JSArrayEx.Clear(Data, 0, Data.Length);
            Length = 0;
            HasOverflowed = false;
        }

        public int GetSpace(int length)
        {
            if (Length + length > MaxCapacity)
            {
                if (_errorHandler != null)
                {
                    if (!CanOverflow)
                        _errorHandler(ErrorCode.ERR_FATAL, "SZ_GetSpace: overflow without allowoverflow set", null);
                    if (length > MaxCapacity)
                        _errorHandler(ErrorCode.ERR_FATAL, "SZ_GetSpace: " + length + " is > full buffer size", null);
                    _errorHandler(ErrorCode.INFO, "SZ_GetSpace: overflow\n", null);
                }
                Clear();
                HasOverflowed = true;
            }
            int lastLength = Length;
            Length += length;
            return lastLength;
        }

        public void Append(byte[] data)
        {
            int length = data.Length;
            JSArrayEx.Copy(data, 0, data, GetSpace(length), length);
        }

        public void Append2(byte[] data, int length)
        {
            JSArrayEx.Copy(data, 0, data, GetSpace(length), length);
        }

        public void Append3(byte[] data, int offset, int length)
        {
            JSArrayEx.Copy(data, offset, data, GetSpace(length), length);
        }

        public void Print(string data2)
        {
            //Com.dprintln("SZ.print():<" + data2 + ">");
            int length = data2.Length;
            byte[] str = JSConvert.StringToBytes(data2);
            if (Length != 0)
                if (Data[Length - 1] != 0)
                    JSArrayEx.Copy(str, 0, Data, GetSpace(length + 1), length);
                else
                    JSArrayEx.Copy(str, 0, Data, GetSpace(length) - 1, length);
            else
                // first print.
                JSArrayEx.Copy(str, 0, Data, GetSpace(length), length);
            Data[Length - 1] = 0;
        }
    }
}
