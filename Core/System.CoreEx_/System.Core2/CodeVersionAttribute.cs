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
	/// Standard attribute throughout Instinct for assigning a Version value to any individual type or member. This version
	/// value is independent of the assembly version of the underlying package that contains the type.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class CodeVersionAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeVersionAttribute"/> class.
		/// </summary>
		/// <param name="version">The version.</param>
		public CodeVersionAttribute(string version)
			: this(CodeVersionKind.Application, version) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="CodeVersionAttribute"/> class.
		/// </summary>
		/// <param name="kind">The kind.</param>
		/// <param name="version">The version.</param>
		public CodeVersionAttribute(CodeVersionKind kind, string version)
		{
			Kind = kind;
			Version = version;
		}

		/// <summary>
		/// Gets or sets the kind.
		/// </summary>
		/// <value>The kind.</value>
		public CodeVersionKind Kind { get; protected set; }

		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <value>The version.</value>
		public string Version { get; protected set; }
	}
}