using System.Collections.Generic;
namespace System.Interop.Cuda
{
	class BasicBlock
	{
		public ListInstruction Head { get; private set; }
		public ListInstruction Tail { get; private set; }
		public string Name { get; private set; }

		private BasicBlock() { }

		public BasicBlock(string name)
		{
			Name = name;
		}

		public IEnumerable<ListInstruction> Instructions
		{
			get
			{
				ListInstruction curr = Head;
				while (curr != null)
				{
					yield return curr;
					curr = curr.Next;
				}
			}
		}

		public void Append(ListInstruction newinst)
		{
			if (Tail != null)
			{
				Tail.Next = newinst;
				newinst.Previous = Tail;
			}
			Tail = newinst;

			if (Head == null)
				Head = newinst;
		}

		public void Replace(ListInstruction inst, ListInstruction replacement)
		{
			Utilities.DebugAssert(replacement.Previous == null && replacement.Next == null, "inst.Previous == null && inst.Next == null");

			replacement.Next = inst.Next;
			replacement.Previous = inst.Previous;

			if (inst.Previous != null)
				inst.Previous.Next = replacement;
			if (inst.Next != null)
				inst.Next.Previous = replacement;

			if (Head == inst)
				Head = replacement;
			if (Tail == inst)
				Tail = replacement;
		}

		public void InsertAfter(ListInstruction inst, ListInstruction newinst)
		{
			Utilities.DebugAssert(newinst.Previous == null && newinst.Next == null, "inst.Previous == null && inst.Next == null");

			newinst.Previous = inst;
			newinst.Next = inst.Next;

			if (inst.Next != null)
				inst.Next.Previous = newinst;
			if (Tail == inst)
				Tail = newinst;

			inst.Next = newinst;
		}
	}
}
