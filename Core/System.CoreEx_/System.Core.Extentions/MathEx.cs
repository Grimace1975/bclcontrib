#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace System
{
	/// <summary>
	/// Provides a basic fa�ade pattern that facilitates common numeric-based calculations into a single class.
	/// </summary>
	public static class MathEx
	{
		internal static Random s_random = new Random();

		/// <summary>
		/// Returns the ceiling of the value provided.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static int Ceiling(decimal value)
		{
			return (int)Math.Ceiling((double)value);
		}
		/// <summary>
		/// Ceilings the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="decimals">The decimals.</param>
		/// <returns></returns>
		public static decimal Ceiling(decimal value, int decimals)
		{
			decimal factor = (decimal)Math.Pow(10.0, (float)decimals);
			return (decimal)Math.Ceiling((double)(value * factor)) / factor;
		}

		/// <summary>
		/// Create a random value between the values provided.
		/// </summary>
		/// <param name="minValue">The min value.</param>
		/// <param name="maxValue">The max value.</param>
		/// <returns>The random value generated.</returns>
		public static int CreateRandom(int minValue, int maxValue)
		{
			return s_random.Next(minValue, maxValue);
		}

		/// <summary>
		/// Returns the largest integer less than or equal to the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static int Floor(decimal value)
		{
			return (int)Math.Floor((double)value);
		}
		/// <summary>
		/// Floors the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="decimals">The decimals.</param>
		/// <returns></returns>
		public static decimal Floor(decimal value, int decimals)
		{
			decimal factor = (decimal)Math.Pow(10.0, (float)decimals);
			return (decimal)Math.Floor((double)(value * factor)) / factor;
		}
	}
}
