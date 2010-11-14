namespace System.Interop.Cuda
{
	public static class ThreadIndex
	{
		public static short X { get { return -1; } }
		public static short Y { get { return -1; } }
		public static short Z { get { return -1; } }
	}

	public static class BlockSize
	{
		public static short X { get { return -1; } }
		public static short Y { get { return -1; } }
		public static short Z { get { return -1; } }
	}

	public static class BlockIndex
	{
		public static int X { get { return -1; } }
		public static int Y { get { return -1; } }
		public static int Z { get { return -1; } }
	}

	public static class GridSize
	{
		public static int X { get { return -1; } }
		public static int Y { get { return -1; } }
		public static int Z { get { return -1; } }
	}
}
