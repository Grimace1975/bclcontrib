using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// Handy class for hand-made IL for use in unit testing.
	/// Currently it does not support opcodes that references things such as locals,
	/// parameters, types etc.
	/// </summary>
	class IlWriter
	{
		private MemoryStream _il = new MemoryStream();
		private BinaryWriter _writer;

		public IlWriter()
		{
			_writer = new BinaryWriter(_il);
		}

		public void WriteOpcode(OpCode opcode)
		{
			if ((opcode.Value & 0xff00) == 0xfe00)
			{
				_writer.Write((byte)(opcode.Value >> 8));
				_writer.Write((byte)opcode.Value);
			}
			else
				_writer.Write((byte)opcode.Value);
		}

		public void WriteByte(int byteValue)
		{
			_writer.Write((byte)byteValue);
		}

		public void WriteInt32(int i)
		{
			_writer.Write(EncodeLittleEndian(i));
		}

		public void WriteFloat(float f1)
		{
			_writer.Write(EncodeLittleEndian((f1)));
		}

		public void WriteDouble(double d1)
		{
			_writer.Write(EncodeLittleEndian(d1));
		}

		private static byte[] EncodeLittleEndian(float f)
		{
			uint u = Utilities.ReinterpretAsUInt(f);
			return EncodeLittleEndian((int)u);
		}

		private static byte[] EncodeLittleEndian(int u)
		{
			return new byte[] { (byte)(u & 0xff), (byte)((u >> 8) & 0xff), (byte)((u >> 16) & 0xff), (byte)((u >> 24) & 0xff) };
		}

		private static byte[] EncodeLittleEndian(double d)
		{
			ulong u = Utilities.ReinterpretAsULong(d);
			return new byte[] { 
				(byte)(u & 0xff), (byte)((u >> 8) & 0xff), (byte)((u >> 16) & 0xff), (byte)((u >> 24) & 0xff),
				(byte)((u >> 32) & 0xff), (byte)((u >> 40) & 0xff), (byte)((u >> 48) & 0xff), (byte)((u >> 56) & 0xff)
			};
		}

		public byte[] ToByteArray()
		{
			return _il.ToArray();
		}

		public IlReader CreateReader()
		{
			return new IlReader(ToByteArray());
		}
	}
}
