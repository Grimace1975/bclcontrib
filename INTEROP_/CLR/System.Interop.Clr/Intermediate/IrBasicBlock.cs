using System.Collections.Generic;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// A basic block for trees.
	/// </summary>
	internal class IrBasicBlock
	{
		private IrBasicBlock _next;
		private int _blockNumber;
		private readonly List<TreeInstruction> _roots = new List<TreeInstruction>();
		private readonly HashSet<IrBasicBlock> _ingoing = new HashSet<IrBasicBlock>();
		private readonly HashSet<IrBasicBlock> _outgoing = new HashSet<IrBasicBlock>();

		public IrBasicBlock Next
		{
			get { return _next; }
			set { _next = value; }
		}

		/// <summary>
		/// A simple sequence number without meaning.
		/// </summary>
		public int BlockNumber
		{
			get { return _blockNumber; }
		}

		public IrBasicBlock(int blockNumber)
		{
			_blockNumber = blockNumber;
		}

		/// <summary>
		/// Roots of the tree representation.
		/// </summary>
		public List<TreeInstruction> Roots
		{
			get { return _roots; }
		}


		/// <summary>
		/// Ingoing basic blocks.
		/// </summary>
		public HashSet<IrBasicBlock> Ingoing
		{
			get { return _ingoing; }
		}

		/// <summary>
		/// Outgoing basic blocks.
		/// </summary>
		public HashSet<IrBasicBlock> Outgoing
		{
			get { return _outgoing; }
		}

		public IEnumerable<TreeInstruction> EnumerateInstructions()
		{
			foreach (TreeInstruction root in Roots)
				foreach (TreeInstruction inst in root.IterateSubtree())
					yield return inst;
		}

		static public void ForeachTreeInstruction(IEnumerable<IrBasicBlock> blocks, Action<TreeInstruction> action)
		{
			foreach (IrBasicBlock block in blocks)
				foreach (TreeInstruction inst in block.EnumerateInstructions())
					action(inst);
		}

		/// <summary>
		/// Applies <paramref name="converter"/> to each node in the tree; when the converter return non-null, the current
		/// node is replaced with the return value.
		/// </summary>
		/// <param name="blocks"></param>
		/// <param name="converter"></param>
		static public void ConvertTreeInstructions(IEnumerable<IrBasicBlock> blocks, Converter<TreeInstruction, TreeInstruction> converter)
		{
			foreach (IrBasicBlock block in blocks)
			{
				for (int r = 0; r < block.Roots.Count; r++)
				{
					TreeInstruction root = block.Roots[r];
					TreeInstruction newRoot = TreeInstruction.ForeachTreeInstruction(root, converter);
					if (newRoot != null)
						block.Roots[r] = newRoot;
				}
			}
		}

		public static IEnumerable<TreeInstruction> EnumerateTreeInstructions(List<IrBasicBlock> blocks)
		{
			foreach (IrBasicBlock block in blocks)
				foreach (TreeInstruction inst in block.EnumerateInstructions())
					yield return inst;
		}
	}
}