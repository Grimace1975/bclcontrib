using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// Represents an instruction in an instruction tree.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay}")]
	class TreeInstruction
	{
		private TreeInstruction _left;
		private TreeInstruction _right;
		private IrOpCode _opcode;
		private object _operand;
		private int _offset = -1;
		private StackTypeDescription _stackType;

		public TreeInstruction(IrOpCode opcode)
		{
			_opcode = opcode;
		}

		public TreeInstruction(IrOpCode opcode, StackTypeDescription stackType, object operand, int offset)
		{
			_opcode = opcode;
			_operand = operand;
			_offset = offset;
			_stackType = stackType;
		}

		public TreeInstruction(IrOpCode opcode, StackTypeDescription stackType, object operand, int offset, TreeInstruction left)
		{
			_left = left;
			_opcode = opcode;
			_operand = operand;
			_offset = offset;
			_stackType = stackType;
		}

		public TreeInstruction(IrOpCode opcode, StackTypeDescription stackType, object operand, int offset, TreeInstruction left, TreeInstruction right)
		{
			_left = left;
			_opcode = opcode;
			_operand = operand;
			_offset = offset;
			_stackType = stackType;
			_right = right;
		}

		public TreeInstruction Left
		{
			get { return _left; }
			set { _left = value; }
		}

		/// <summary>
		/// Returns left and right if they are non-null.
		/// <para>Overridden implementations may return more - as does <see cref="MethodCallInstruction"/>.</para>
		/// </summary>
		/// <returns></returns>
		public virtual TreeInstruction[] GetChildInstructions()
		{
            //Utilities.PretendVariableIsUsed(DebuggerDisplay);
            //Utilities.PretendVariableIsUsed(SubTreeText);
			if (Right != null)
				return new TreeInstruction[] { Left, Right };
			else if (Left != null)
				return new TreeInstruction[] { Left };
			else
				return new TreeInstruction[0];
		}

		/// <summary>
		/// Replaces the specified child with <paramref name="newchild"/>.
		/// </summary>
		/// <param name="childIndex"></param>
		/// <param name="newchild"></param>
		public virtual void ReplaceChild(int childIndex, TreeInstruction newchild)
		{
			switch (childIndex)
			{
				case 0:
                    if (Left == null)
                        throw new NullReferenceException("Left");
					Left = newchild;
					break;
				case 1:
                    if (Right == null)
                        throw new NullReferenceException("Right");
					Right = newchild;
					break;
				default:
					throw new ArgumentOutOfRangeException("childIndex");
			}
		}

		private string DebuggerDisplay
		{
			get
			{
				if ((Operand is string) || (Operand is int))
					return "" + Opcode.IrCode + " " + Operand;
				else if (Operand is LocalVariableInfo)
				{
					LocalVariableInfo r = (LocalVariableInfo)Operand;
					return string.Format("{0} V_{1} ({2})", Opcode, r.LocalIndex, r.LocalType.Name);
				}
				else if (Operand is FieldInfo)
				{
					FieldInfo f = (FieldInfo)Operand;
					return string.Format("{0} {1} ({2})", Opcode, f.Name, f.FieldType.Name);
				}
				else
					return Opcode.IrCode.ToString();
			}
		}

		public string DebuggerTreeDisplay()
		{
			string leftString = (_left != null ? _left.DebuggerTreeDisplay() : string.Empty);
			string rightString = (_right != null ? _right.DebuggerTreeDisplay() : string.Empty);
			return ((_left == null) && (_right == null) ? string.Format("{0} {1}", DebuggerDisplay, _offset) : string.Format("{0} {1} [{2}, {3}]", DebuggerDisplay, _offset, leftString, rightString));
		}

		private string SubTreeText
		{
			get { return new TreeDrawer().DrawSubTree(this); }
		}

		public TreeInstruction Right
		{
			get { return _right; }
			set { _right = value; }
		}

		public IrOpCode Opcode
		{
			get { return _opcode; }
			set { _opcode = value; }
		}

		#region Operand

		public object Operand
		{
			get { return _operand; }
			set { _operand = value; }
		}

		public MethodBase OperandAsMethod
		{
			get { return _operand as MethodBase; }
		}

		//public MethodCompiler OperandAsMethodCompiler
		//{
		//    get { return _operand as MethodCompiler; }
		//}

		//public PpeMethod OperandAsPpeMethod
		//{
		//    get { return _operand as PpeMethod; }
		//}

		public IrBasicBlock OperandAsBasicBlock
		{
			get { return _operand as IrBasicBlock; }
		}

		public int OperandAsInt32
		{
			get { return (int)_operand; }
		}

		public MethodVariable OperandAsVariable
		{
			get { return _operand as MethodVariable; }
		}

		public FieldInfo OperandAsField
		{
			get { return _operand as FieldInfo; }
		}

		#endregion

		/// <summary>
		/// Offset in the IL stream, where applicable; otherwise -1. Used for branching.
		/// </summary>
		public int Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		public StackTypeDescription StackType
		{
			get { return _stackType; }
			set { _stackType = value; }
		}

		#region Tree iteration / checking.

		/// <summary>
		/// Applies <paramref name="converter"/> to each node in the tree; when the converter return non-null, the current
		/// node is replaced with the return value.
		/// </summary>
		/// <param name="root"></param>
		/// <param name="converter"></param>
		/// <returns></returns>
		public static TreeInstruction ForeachTreeInstruction(TreeInstruction root, Converter<TreeInstruction, TreeInstruction> converter)
		{
			if (root == null)
				return null;
			TreeInstruction newRoot = null;
			var parentlist = new Stack<TreeInstruction>();
			var childIndexList = new Stack<int>();
			TreeInstruction parent = null;
			int childIndex = 0;
			TreeInstruction inst = root;
			do
			{
				var newInst = converter(inst);
				if (newInst != null)
				{
					inst = newInst;
					if (parent != null)
						parent.ReplaceChild(childIndex, newInst);
					else
						newRoot = newInst;
				}
				// Go to the nest instruction.
				if (inst.GetChildInstructions().Length > 0)
				{
					parentlist.Push(parent);
					childIndexList.Push(childIndex);
					parent = inst;
					childIndex = 0;
					inst = inst.GetChildInstructions()[0];
				}
				else if (parent != null && childIndex + 1 < parent.GetChildInstructions().Length)
					inst = parent.GetChildInstructions()[++childIndex];
				else if (parent != null)
				{
					while (parent != null && childIndex + 1 >= parent.GetChildInstructions().Length)
					{
						parent = parentlist.Pop();
						childIndex = childIndexList.Pop();
					}
					if (parent != null)
						inst = parent.GetChildInstructions()[++childIndex];
					//parent = parentlist.Peek();
					//childIndex = childIndexList.Peek();
				}
			} while (parent != null);
			return newRoot;
		}

		/// <summary>
		/// For checking tree construction.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TreeInstruction> IterateSubtree()
		{
			var list = new List<TreeInstruction>();
			BuildPreorder(list);
			return list;
		}

		/// <summary>
		/// For checking tree construction.
		/// </summary>
		/// <param name="list"></param>
		internal virtual void BuildPreorder(List<TreeInstruction> list)
		{
			list.Add(this);
			if (Left != null)
				Left.BuildPreorder(list);
			if (Right != null)
				Right.BuildPreorder(list);
		}

		#endregion

		/// <summary>
		/// Returns the first instruction in the tree; that is, the instruction in the far left side.
		/// </summary>
		/// <returns></returns>
		public TreeInstruction GetFirstInstruction()
		{
			TreeInstruction parent = this;
			TreeInstruction child = null;
			do
			{
				if (child != null)
					parent = child;
				child = parent.GetChildInstructions().FirstOrDefault();
			} while (child != null);
			return parent;
		}

		/// <summary>
		/// Returns the first instruction in the tree that has an address.
		/// Instructions that were not in the IL do not have an offset and therefore
		/// cannot be targets for branches.
		/// </summary>
		/// <returns></returns>
		public TreeInstruction GetFirstInstructionWithOffset()
		{
			TreeInstruction parent = this;
			TreeInstruction child = null;
			do
			{
				if (child != null)
					parent = child;
				child = parent.GetChildInstructions().FirstOrDefault();
			} while ((child != null) && (child.Offset >= 0));
			//if (parent.Offset < 0)
			//    throw new ILSemanticErrorException("Can't find first instruction with offset.");
			return parent;
		}
	}
}
