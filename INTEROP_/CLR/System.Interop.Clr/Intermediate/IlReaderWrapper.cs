using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// Wraps an ILReader and expands some IL instructions to a sequence of instructions.
	/// </summary>
	class IlReaderWrapper
	{
		private int _variableCount;
		private readonly IlReader _ilreader;
		private IlRecord _currentRecord;
		private readonly Queue<IlRecord> _instQueue = new Queue<IlRecord>();
		private MethodVariable _lastCreatedMethodVariable;

		struct IlRecord
		{
			public readonly OpCode OpCode;
			public readonly object Operand;
			public int InstructionSize;

			public bool IsEmpty
			{
				get { return OpCode == null; }
			}

			public IlRecord(OpCode opCode, object operand, int instructionSize)
			{
				OpCode = opCode;
				Operand = operand;
				InstructionSize = instructionSize;
			}
		}

		public IlReaderWrapper(IlReader reader)
		{
			_ilreader = reader;
		}

		public IlReader IlReader
		{
			get { return _ilreader; }
		}

		public MethodVariable LastCreatedMethodVariable
		{
			get { return _lastCreatedMethodVariable; }
		}

		public void Reset()
		{
			_ilreader.Reset();
			_variableCount = 0;
		}

		public int InstructionSize
		{
			get { return _currentRecord.InstructionSize; }
		}

		public bool Read(StackTypeDescription type)
		{
			_lastCreatedMethodVariable = null;
			if (_instQueue.Count > 0)
			{
				_currentRecord = _instQueue.Dequeue();
				return true;
			}
			if (!_ilreader.Read())
				return false;
			OpCode opcode = _ilreader.OpCode;
			object operand = _ilreader.Operand;
			if (opcode == OpCodes.Dup)
			{
				if (type == StackTypeDescription.None)
					throw new ArgumentException("Incompatible type.");
				MethodVariable mv = new MethodVariable(_variableCount + 2000, type);
				_currentRecord = new IlRecord(OpCodes.Stloc, mv, 0);
				_instQueue.Enqueue(new IlRecord(OpCodes.Ldloc, mv, 0));
				_instQueue.Enqueue(new IlRecord(OpCodes.Ldloc, mv, _ilreader.InstructionSize));
				_lastCreatedMethodVariable = mv;
			}
			else
				_currentRecord = new IlRecord(opcode, operand, _ilreader.InstructionSize);
			return true;
		}

		public int Offset
		{
			get { return _ilreader.Offset; }
		}

		public OpCode OpCode
		{
			get
			{
				if (_currentRecord.IsEmpty)
					throw new Exception("!_currentRecord.IsEmpty");
				return _currentRecord.OpCode;
			}
		}

		public object Operand
		{
			get
			{
				if (_currentRecord.IsEmpty)
					throw new Exception("!_currentRecord.IsEmpty");
				return _currentRecord.Operand;
			}
		}
	}
}