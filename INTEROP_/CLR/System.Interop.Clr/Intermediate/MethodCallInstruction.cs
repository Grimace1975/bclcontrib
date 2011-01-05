using System.Collections.Generic;
using System.Reflection;
//using CellDotNet.Spe;
namespace System.Interop.Intermediate
{
	class MethodCallInstruction : TreeInstruction
	{
		private MethodBase _intrinsicMethod;
		private List<TreeInstruction> _parameters = new List<TreeInstruction>();

		public MethodCallInstruction(MethodBase method, IrOpCode opcode)
			: base(opcode)
		{
			Operand = method;
			Opcode = opcode;
		}

		///// <summary>
		///// The reason that this ctor also takes the method as an argument is so that the type deriver
		///// can do its job.
		///// </summary>
		///// <param name="intrinsic"></param>
		///// <param name="method"></param>
		///// <param name="intrinsicCallOpCode"></param>
		//public MethodCallInstruction(MethodBase method, SpuIntrinsicMethod intrinsic, IrOpCode intrinsicCallOpCode)
		//    : base(intrinsicCallOpCode)
		//{
		//    if (intrinsic != SpuIntrinsicMethod.None)
		//        throw new ArgumentException("intrinsic != SpuIntrinsicMethod.None");
		//    Operand = intrinsic;
		//    _intrinsicMethod = method;
		//}

		//public MethodCallInstruction(MethodInfo method, SpuOpCode spuOpCode)
		//    : base(IrOpCodes.SpuInstructionMethod)
		//{
		//    Operand = spuOpCode;
		//    _intrinsicMethod = method;
		//}

		/// <summary>
		/// The Operand casted as a method.
		/// </summary>
		public MethodBase OperandMethod
		{
			get { return (MethodBase)Operand; }
		}

		///// <summary>
		///// The operand cast as a <see cref="MethodCompiler"/>.
		///// </summary>
		//public MethodCompiler TargetMethodCompiler
		//{
		//    get { return (MethodCompiler)Operand; }
		//}

		///// <summary>
		///// The operand cast as a <see cref="SpuRoutine"/>.
		///// </summary>
		//public SpuRoutine TargetRoutine
		//{
		//    get { return (SpuRoutine)Operand; }
		//}

		/// <summary>
		/// Intrinsic methods are exposed via this property so that the type deriver can do its job.
		/// </summary>
		public MethodBase IntrinsicMethod
		{
			get { return _intrinsicMethod; }
		}

		public List<TreeInstruction> Parameters
		{
			get { return _parameters; }
		}

		//public SpuOpCode OperandSpuOpCode
		//{
		//    get { return (SpuOpCode)Operand; }
		//}

		internal override void BuildPreorder(List<TreeInstruction> list)
		{
			list.Add(this);
			foreach (TreeInstruction param in Parameters)
				param.BuildPreorder(list);
		}

		public override TreeInstruction[] GetChildInstructions()
		{
			return Parameters.ToArray();
		}

		public override void ReplaceChild(int childIndex, TreeInstruction newchild)
		{
			Parameters[childIndex] = newchild;
		}

		//public void SetCalledMethod(SpuRoutine routine, IrOpCode callOpCode)
		//{
		//    if (routine == null)
		//        throw new ArgumentNullException("routine");
		//    Operand = routine;
		//    Opcode = callOpCode;
		//}
	}
}
