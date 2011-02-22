using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// A generic type without type parameters.
	/// </summary>
	class GenericType
	{
		private Type _type;

		public GenericType(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (!type.IsGenericTypeDefinition)
				throw new ArgumentException("type argument is not generic.");
			_type = type;
		}

		/// <summary>
		/// The generic type without parameters.
		/// </summary>
		public Type Type
		{
			get { return _type; }
		}
	}
}
