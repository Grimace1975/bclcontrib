using System;
namespace SystemEx.TypedArrays
{
    public class DataView : ArrayBufferView
    {
        protected DataView() { }

        public byte GetByte(int byteOffset) { return (byte)Script.Literal("this.getUInt8({0})", byteOffset); }
        public sbyte GetSByte(int byteOffset) { return (sbyte)Script.Literal("this.getInt8({0})", byteOffset); }
        public short GetInt16(int byteOffset, bool littleEndian) { return (short)Script.Literal("this.getInt16({0}, {1})", byteOffset, littleEndian); }
        public ushort GetUInt16(int byteOffset, bool littleEndian) { return (ushort)Script.Literal("this.getUInt16({0}, {1})", byteOffset, littleEndian); }
        public int GetInt32(int byteOffset, bool littleEndian) { return (int)Script.Literal("this.getInt32({0}, {1})", byteOffset, littleEndian); }
        public uint GetUInt32(int byteOffset, bool littleEndian) { return (uint)Script.Literal("this.getUInt32({0}, {1})", byteOffset, littleEndian); }
        public float GetSingle(int byteOffset, bool littleEndian) { return (float)Script.Literal("this.getFloat({0}, {1})", byteOffset, littleEndian); }
        public double GetDouble(int byteOffset, bool littleEndian) { return (double)Script.Literal("this.getDouble({0}, {1})", byteOffset, littleEndian); }

        public void SetByte(int byteOffset, byte value, bool littleEndian) { Script.Literal("this.setUint8({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetSByte(int byteOffset, sbyte value, bool littleEndian) { Script.Literal("this.setInt8({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetInt16(int byteOffset, short value, bool littleEndian) { Script.Literal("this.setInt16({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetUint16(int byteOffset, ushort value, bool littleEndian) { Script.Literal("this.setUint16({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetInt32(int byteOffset, int value, bool littleEndian) { Script.Literal("this.setInt32({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetUint32(int byteOffset, uint value, bool littleEndian) { Script.Literal("this.setUint32({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetSingle(int byteOffset, float value, bool littleEndian) { Script.Literal("this.setFloat({0}, {1}, {2})", byteOffset, value, littleEndian); }
        public void SetDouble(int byteOffset, double value, bool littleEndian) { Script.Literal("this.setDouble({0}, {1}, {2})", byteOffset, value, littleEndian); }
    }
}
