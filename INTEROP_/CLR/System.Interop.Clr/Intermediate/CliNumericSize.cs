namespace System.Interop.Intermediate
{
	/// <summary>
	/// Sizes of numeric operands on the stack.
	/// </summary>
	internal enum CliNumericSize : byte
	{
		None,
		OneByte = 1,
		TwoBytes = 2,
		FourBytes = 4,
		EightBytes = 8,
		SixteenBytes = 16,
	}
}
