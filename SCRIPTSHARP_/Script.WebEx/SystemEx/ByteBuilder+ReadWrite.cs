#if !CODE_ANALYSIS
namespace System
#else
namespace SystemEx
#endif
{
    /// <summary>
    /// ByteBuilderExtensions
    /// MSG moved mainly here
    /// </summary>
    public partial class ByteBuilder
    {
        // 2k read buffer.
        public static byte[] _stringBuffer = new byte[2048];

        #region Writes

        //public void WriteChar(float c) { WriteChar2((int)c); }
        public void WriteChar(char c)
        {
            Data[GetSpace(1)] = (byte)(c & 0xFF);
        }

        //public void WriteByte(float c) { WriteByte2((int)c); }
        public void WriteByte(byte c)
        {
            Data[GetSpace(1)] = (byte)(c & 0xFF);
        }

        public void WriteInt16(short c)
        {
            int index = GetSpace(2);
            Data[index++] = (byte)(c & 0xff);
            Data[index] = (byte)((c >> 8) & 0xFF);
        }

        public void WriteInt32(int c)
        {
            int index = GetSpace(4);
            Data[index++] = (byte)((c & 0xff));
            Data[index++] = (byte)((c >> 8) & 0xff);
            Data[index++] = (byte)((c >> 16) & 0xff);
            Data[index++] = (byte)((c >> 24) & 0xff);
        }

        public void WriteInt64(long c) { WriteInt32((int)c); }

        public void WriteSingle(float f) { WriteInt32(JSConvert.SingleToInt32Bits(f)); }

        //public void WriteString2(byte[] s) { WriteString(JSConvertEx.BytesToString(s).Trim()); }
        public void WriteString(string s)
        {
            if (s == null)
                s = string.Empty;
            Append(JSConvert.StringToBytes(s));
            WriteByte(0);
            //Com.dprintln("MSG.WriteString:" + s.replace('\0', '@'));
        }

        #endregion

        #region Reads

        public void BeginReading()
        {
            ReadIndex = 0;
        }

        // returns -1 if no more characters are available, but also [-128 , 127]
        public char ReadChar()
        {
            char c;
            if (ReadIndex + 1 > Length)
                c = (char)0xff;
            else
                c = (char)Data[ReadIndex];
            ReadIndex++;
            // kickangles bugfix (rst)
            return c;
        }

        public byte ReadByte()
        {
            byte c;
            if (ReadIndex + 1 > Length)
                c = 0xff;
            else
                c = (byte)(Data[ReadIndex] & 0xff);
            ReadIndex++;
            return c;
        }

        public short ReadInt16()
        {
            short c;
            if (ReadIndex + 2 > Length)
                c = -1;
            else
                c = (short)((Data[ReadIndex] & 0xff) + (Data[ReadIndex + 1] << 8));
            ReadIndex += 2;
            return (short)c;
        }

        public int ReadInt32()
        {
            int c;
            if (ReadIndex + 4 > Length)
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "buffer underrun in ReadLong!", null);
                c = -1;
            }
            else
                c = (Data[ReadIndex] & 0xff)
                | ((Data[ReadIndex + 1] & 0xff) << 8)
                | ((Data[ReadIndex + 2] & 0xff) << 16)
                | ((Data[ReadIndex + 3] & 0xff) << 24);
            ReadIndex += 4;
            return c;
        }

        public long ReadInt64() { return ReadInt32(); }

        public float ReadSingle() { return JSConvert.Int32BitsToSingle(ReadInt32()); }

        public string ReadString()
        {
            int index = 0;
            do
            {
                byte c = ReadByte();
                if ((c == 0xff) || (c == 0))
                    break;
                _stringBuffer[index] = c;
                index++;
            } while (index < 2047);
            string ret = JSConvert.BytesToString(_stringBuffer, 0, index);
            // Com.dprintln("MSG.ReadString:[" + ret + "]");
            return ret;
        }

        public string ReadStringLine()
        {
            int index = 0;
            do
            {
                byte c = ReadByte();
                if ((c == 0xff) || (c == 0) || (c == 0x0a))
                    break;
                _stringBuffer[index] = c;
                index++;
            } while (index < 2047);
            string ret = JSConvert.BytesToString(_stringBuffer, 0, index).Trim();
            //Com.dprintln("MSG.ReadStringLine:[" + ret.Replace('\0', '@') + "]");
            return ret;
        }

        public void ReadData(byte[] data, int length)
        {
            for (int index = 0; index < length; index++)
                data[index] = ReadByte();
        }

        #endregion
    }
}
