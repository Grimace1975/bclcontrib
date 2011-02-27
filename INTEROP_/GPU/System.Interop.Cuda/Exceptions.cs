using System.Runtime.Serialization;
namespace System.Interop.Cuda
{
	[Serializable]
	public class PtxCompilationException : Exception
	{
		public PtxCompilationException() { }
		public PtxCompilationException(string message) : base(message) { }
		public PtxCompilationException(string message, Exception inner) : base(message, inner) { }
		protected PtxCompilationException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class NoSuchDeviceException : Exception
	{
		public NoSuchDeviceException() { }
		public NoSuchDeviceException(string message) : base(message) { }
		public NoSuchDeviceException(string message, Exception inner) : base(message, inner) { }
		protected NoSuchDeviceException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class CudaException : Exception
	{
		public CudaException() { }
		public CudaException(string message) : base(message) { }
		public CudaException(string message, Exception inner) : base(message, inner) { }
		protected CudaException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }
	}
}
