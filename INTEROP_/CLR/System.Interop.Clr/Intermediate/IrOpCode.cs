using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// CellDotNet IR/IL opcode.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay}")]
	internal class IrOpCode
	{
		private FlowControl _flowControl;
		private string _name;
		private OpCode? _reflectionOpCode;
		private IrCode _irCode;

		/// <summary>
		/// Describes how an <see cref="IROpCode"/> behaves wrt. stack popping.
		/// </summary>
		internal enum PopBehavior
		{
			Pop0 = 0,
			Pop1 = 1,
			Pop2 = 2,
			Pop3 = 3,
			PopAll = 1000,
			VarPop = 1001
		}

		public IrOpCode(string name, IrCode irCode, FlowControl flowControl, OpCode? reflectionOpCode)
		{
			_flowControl = flowControl;
			_name = name;
			_reflectionOpCode = reflectionOpCode;
			_irCode = irCode;
            //Utilities.PretendVariableIsUsed(DebuggerDisplay);
		}

		private object DebuggerDisplay
		{
			get { return _name; }
		}

		public override string ToString()
		{
			return _name;
		}

		public FlowControl FlowControl
		{
			get { return _flowControl; }
		}

		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// The IL opcode that this IR opcode is based upon, if any.
		/// </summary>
		public OpCode? ReflectionOpCode
		{
			get { return _reflectionOpCode; }
		}

		public IrCode IrCode
		{
			get { return _irCode; }
		}

		public static PopBehavior GetPopBehavior(StackBehaviour stackBehaviourPop)
		{
			PopBehavior pb;
			switch (stackBehaviourPop)
			{
				case StackBehaviour.Pop0:
					pb = PopBehavior.Pop0;
					break;
				case StackBehaviour.Varpop:
					pb = PopBehavior.VarPop;
					break;
				case StackBehaviour.Pop1:
				case StackBehaviour.Popi:
				case StackBehaviour.Popref:
					pb = PopBehavior.Pop1;
					break;
				case StackBehaviour.Pop1_pop1:
				case StackBehaviour.Popi_pop1:
				case StackBehaviour.Popi_popi:
				case StackBehaviour.Popi_popi8:
				case StackBehaviour.Popi_popr4:
				case StackBehaviour.Popi_popr8:
				case StackBehaviour.Popref_pop1:
				case StackBehaviour.Popref_popi:
					pb = PopBehavior.Pop2;
					break;
				case StackBehaviour.Popi_popi_popi:
				case StackBehaviour.Popref_popi_pop1:
				case StackBehaviour.Popref_popi_popi:
				case StackBehaviour.Popref_popi_popi8:
				case StackBehaviour.Popref_popi_popr4:
				case StackBehaviour.Popref_popi_popr8:
				case StackBehaviour.Popref_popi_popref:
					pb = PopBehavior.Pop3;
					break;
				default:
					throw new ArgumentOutOfRangeException("stackBehaviourPop");
			}
			return pb;
		}
	}
}
