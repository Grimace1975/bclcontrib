namespace System.Interop.Intermediate
{
	/// <summary>
	/// CLI types as available in metadata, including classes like 
	/// values types, objects and method pointers.
	/// <para>
	/// Most often, you would want to use <see cref="StackTypeDescription"/> 
	/// instead of this enumeration.
	/// </para>
	/// <remarks>
	/// This enumeration contains more than the six basic CLI types: 
	/// It also contains variations of the numeric types.
	/// </remarks>
	/// </summary>
	internal enum CliType
	{
		None = 0,
		Int32 = 1,
		Int64 = 2,
		NativeInt = 3,
		Float32 = 4,
		Float64 = 5,
		VectorI4 = 6,
		VectorF4 = 7,
		VectorD2 = 7,
		/// <summary>
		/// Any value type.
		/// </summary>
		ValueType = 8,
		/// <summary>
		/// Any object type.
		/// </summary>
		ObjectType = 9,
		/// <summary>
		/// The "&amp;" type.
		/// </summary>
		ManagedPointer = 10,
	}
}
