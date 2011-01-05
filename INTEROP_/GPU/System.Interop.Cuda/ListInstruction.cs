using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using CellDotNet.Intermediate;
//using JetBrains.Annotations;
using System.Interop.Intermediate;
namespace System.Interop.Cuda
{
	/// <summary>
	/// This class is used for the IL-based IR, but also for the output of instruction selection, so it needs to accomodate to
	/// multiple ISAs.
	/// </summary>
	[DebuggerDisplay("{IrCode} - {PtxCode}")]
	class ListInstruction
	{
		public ListInstruction(IrCode opcode, object operand)
		{
			_opCode = (int)opcode;
			Operand = operand;
		}

		public ListInstruction(PtxCode opcode)
		{
			_opCode = (int)opcode;
		}

		public ListInstruction(IrCode opcode)
		{
			_opCode = (int)opcode;
		}

		public ListInstruction(PtxCode opcode, object operand)
		{
			_opCode = (int)opcode;
			Operand = operand;
		}

		/// <summary>
		/// Copy constructor. Next and previous are not copied.
		/// </summary>
		public ListInstruction(PtxCode opcode, ListInstruction template)
		{
			_opCode = (int)opcode;
			// Yes, virtcalls here, so MethodCallListInstruction probably shouldn't use this constructor.
			Destination = template.Destination;
			Source1 = template.Source1;
			Source2 = template.Source2;
			Source3 = template.Source3;
			Predicate = template.Predicate;
			PredicateNegation = template.PredicateNegation;
			Operand = template.Operand;
		}

		private readonly int _opCode;

		/// <summary>
		/// The opcode cast as an IR opcode.
		/// </summary>
		public IrCode IrCode
		{
			get { return (IrCode)_opCode; }
		}

		/// <summary>
		/// The opcode cast as a PTX opcode.
		/// </summary>
		public PtxCode PtxCode
		{
			get { return (PtxCode)_opCode; }
		}

		//[CanBeNull]
		public object Operand { get; set; }
		public GlobalVReg OperandAsGlobalVRegNonNull
		{
			get
			{
				Utilities.DebugAssert((Operand as GlobalVReg) != null, "(Operand as GlobalVReg) != null");
				return Operand as GlobalVReg;
			}
		}
		//[CanBeNull]
		public GlobalVReg OperandAsGlobalVReg
		{
			get { return Operand as GlobalVReg; }
		}

		public ListInstruction Next { get; set; }
		public ListInstruction Previous { get; set; }

		//[CanBeNull]
		public virtual GlobalVReg Source1 { get; set; }
		//[CanBeNull]
		public virtual GlobalVReg Source2 { get; set; }
		//[CanBeNull]
		public virtual GlobalVReg Source3 { get; set; }
		//[CanBeNull]
		public GlobalVReg Destination { get; set; }

		//[CanBeNull]
		public GlobalVReg Predicate { get; set; }
		public bool PredicateNegation { get; set; }
	}

	class MethodCallListInstruction : ListInstruction
	{
		public MethodCallListInstruction(IrCode opcode, object operand)
			: base(opcode, operand)
		{
			Parameters = new List<GlobalVReg>(2);
		}

#pragma warning disable 0809 // "Obsolete member 'CellDotNet.Cuda.MethodCallListInstruction.Source1' overrides non-obsolete member 'CellDotNet.Cuda.ListInstruction.Source1'"

		[Obsolete("Not obsolete, but use Parameters instead.")]
		public override GlobalVReg Source1
		{
			get { throw new InvalidOperationException(); }
			set { throw new InvalidOperationException(); }
		}

		[Obsolete("Not obsolete, but use Parameters instead.")]
		public override GlobalVReg Source2
		{
			get { throw new InvalidOperationException(); }
			set { throw new InvalidOperationException(); }
		}

#pragma warning restore 0809

		public List<GlobalVReg> Parameters { get; private set; }
	}
}
