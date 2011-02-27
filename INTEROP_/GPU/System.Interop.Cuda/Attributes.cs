namespace System.Interop.Cuda
{
	/// <summary>
	/// Specifies the size of a static array.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class StaticArrayAttribute : Attribute
	{
		public int SizeX { get; private set; }
		//		public int SizeY { get; private set; }
		//		public int SizeZ { get; private set; }

		public StaticArrayAttribute(int sizeX)
		{
			if (sizeX < 0)
				throw new ArgumentOutOfRangeException("sizeX");
			SizeX = sizeX;
		}

		//		public StaticArrayAttribute(int sizeX, int sizeY)
		//		{
		//			SizeX = sizeX;
		//			SizeY = sizeY;
		//		}

		//		public StaticArrayAttribute(int sizeX, int sizeY, int sizeZ)
		//		{
		//			SizeX = sizeX;
		//			SizeY = sizeY;
		//			SizeZ = sizeZ;
		//		}
	}

	public enum CudaMemoryType
	{
		None = 0,
		Shared = 1,
		Global = 2,
		// Texture = 3
	}

	/// <summary>
	/// Specifies the storage memory type for a variable/array.
	/// </summary>
	public class CudaMemoryAttribute : Attribute
	{
		public CudaMemoryType MemoryType { get; private set; }

		public CudaMemoryAttribute(CudaMemoryType memoryType)
		{
			MemoryType = memoryType;
		}
	}
}
