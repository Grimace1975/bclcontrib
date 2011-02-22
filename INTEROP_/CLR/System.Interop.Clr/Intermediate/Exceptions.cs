using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
namespace System.Interop.Intermediate
{
	[Serializable]
	public class IlSemanticErrorException : Exception
	{
		public IlSemanticErrorException() { }
		public IlSemanticErrorException(string message) : base(message) { }
		public IlSemanticErrorException(string message, Exception inner) : base(message, inner) { }
		protected IlSemanticErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class IlParseException : Exception
	{
		public IlParseException() { }
		public IlParseException(string message) : base(message) { }
		public IlParseException(string message, Exception inner) : base(message, inner) { }
		protected IlParseException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class InvalidIrTreeException : Exception
	{
		public InvalidIrTreeException() { }
		public InvalidIrTreeException(string message) : base(message) { }
		public InvalidIrTreeException(string message, Exception inner) : base(message, inner) { }
		protected InvalidIrTreeException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class InvalidIrException : Exception
	{
		public InvalidIrException() { }
		public InvalidIrException(string message) : base(message) { }
		public InvalidIrException(string message, Exception inner) : base(message, inner) { }
		protected InvalidIrException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	class IlNotImplementedException : Exception
	{
		public IlNotImplementedException() { }
		public IlNotImplementedException(string message) : base(message) { }
		public IlNotImplementedException(string message, Exception inner) : base(message, inner) { }
		public IlNotImplementedException(TreeInstruction inst) : this(inst.Opcode.IrCode.ToString()) { }
		public IlNotImplementedException(IrCode ilcode) : this(ilcode.ToString()) { }
        protected IlNotImplementedException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

}
