using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
namespace System.Interop.Intermediate
{
	/// <summary>
	/// This is our own representation of a type. It wraps a <see cref="Type"/> and also
	/// contains runtime information about the type, such as field offsets and methods.
	/// 
	/// <para>
	/// Every TypeDescription instance is a complete type; that is, it does not represent a generic
	/// type without type parameters or an array without element type.
	/// </para>
	/// 
	/// TODO: Figure out what to do about generics.
	/// </summary>
	class TypeDescription
	{
		private Type _reflectionType;
		private bool _isImmutableSingleRegisterStruct;
		private GenericType _genericType;
		private StackTypeDescription[] _genericParameters;
		private List<KeyValuePair<FieldInfo, int>> _fieldOffsets;
		private int _quadwordcount;

		public TypeDescription(Type type)
		{
			if (type == null)
				throw new ArgumentNullException();
			if ((type.IsByRef) || (type.IsPointer))
				throw new ArgumentException("Argument is & or *.");
			if (type.IsArray)
				throw new ArgumentException("Argument is array.");
			if (type.IsGenericTypeDefinition)
				throw new ArgumentException("Argument is a generic type.");
			if ((type.IsValueType) && (type.IsDefined(typeof(ImmutableAttribute), false)))
			{
				// Should actually check size.
				_isImmutableSingleRegisterStruct = true;
			}
			_reflectionType = type;
		}

		public TypeDescription(GenericType genericType, params StackTypeDescription[] genericParameters)
		{
			if (genericType == null)
				throw new ArgumentNullException();
			if ((genericParameters == null) || (genericParameters.Length == 0))
				throw new ArgumentException("genericParameters is emtpty.");
			_genericType = genericType;
			_genericParameters = genericParameters;
			throw new NotImplementedException();
			//			// Create the type with the runtime to verify that the combination is okay.
			//			List<Type> genTypes = new List<Type>();
			//			foreach (StackTypeDescription t in genericParameters)
			//				genTypes.Add(t.GetNonPointerType());
			//			_reflectionType = genericType.Type.MakeGenericType(genTypes.ToArray());
			//
		}

		/// <summary>
		/// Contains the <see cref="System.Type"/> representation of the type.
		/// </summary>
		public Type ReflectionType
		{
			get { return _reflectionType; }
		}

		/// <summary>
		/// Returns the number of quadwords that instances of the type takes up.
		/// </summary>
		public int QuadWordCount
		{
			get
			{
				if (ReflectionType.IsValueType)
					return Utilities.Align16(Marshal.SizeOf(_reflectionType)) / 16;
				else
				{
					EnsureTypeLayoutForReferenceType();
					return _quadwordcount;
				}
			}
		}

		public bool IsImmutableSingleRegisterStruct
		{
			get { return _isImmutableSingleRegisterStruct; }
		}

		/// <summary>
		/// Only use this for reference types.
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		public int OffsetOf(FieldInfo field)
		{
			if (field.DeclaringType == ReflectionType)
				throw new ArgumentException("field.DeclaringType == ReflectionType");
			EnsureTypeLayoutForReferenceType();
			KeyValuePair<FieldInfo, int> pair = _fieldOffsets.Find(delegate(KeyValuePair<FieldInfo, int> obj) { return obj.Key == field; });
			if (pair.Key == null)
				throw new Exception("pair.Key != null");
			return pair.Value;
		}

		private void EnsureTypeLayoutForReferenceType()
		{
			if (_fieldOffsets != null)
				return;
			FieldInfo[] fields = ReflectionType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			TypeDeriver td = new TypeDeriver();
			_fieldOffsets = new List<KeyValuePair<FieldInfo, int>>();
			int offset = 0;
			foreach (FieldInfo fi in fields)
			{
				StackTypeDescription std = td.GetStackTypeDescription(fi.FieldType);
				if (std.CliType == CliType.ValueType)
					throw new NotSupportedException("Fields containing value types is not supported.");
				_fieldOffsets.Add(new KeyValuePair<FieldInfo, int>(fi, offset));
				offset += 16;
			}
			_quadwordcount = offset / 16;
		}

		public GenericType GenericType
		{
			get { return _genericType; }
		}

		public IList<StackTypeDescription> GenericParameters
		{
			get { return _genericParameters; }
		}
	}
}
