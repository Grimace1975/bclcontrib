using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
//using CellDotNet.Intermediate;
//using JetBrains.Annotations;
namespace System.Interop.Cuda
{
	/// <summary>
	/// The new stack type enum.
	/// </summary>
	enum StackType
	{
		None,
		I2,
		I4,
		I8,
		R4,
		R8,
		ValueType,
		Object,
		ManagedPointer,
		UnmanangedPointer,
	}

	/// <summary>
	/// Used by <see cref="GlobalVReg"/> to make is possible to determine how vregs should be treated.
	/// TODO: Fix this comment: When the value is <see cref="Register"/>, the vreg's state space is often  <see cref="CudaStateSpace.Register"/>.
	/// </summary>
	enum VRegType
	{
		None,
		Register,
		SpecialRegister,
		/// <summary>
		/// An address value as defined by a ptx array.
		/// </summary>
		Address,
		Immediate,
	}

	/// <summary>
	/// Describes the kinds of storage allocation a symbol uses, and allows <see cref="GlobalVReg"/> to
	/// determine how to declare symbols.
	/// </summary>
	internal enum CudaStateSpace
	{
		None,
		Register,
		Global,
		Local,
		Parameter,
		Shared,
		Texture,
		Constant
	}

	/// <summary>
	/// This one currently represents any variables, argument, value on the stack, constant or special register.
	/// </summary>
	class GlobalVReg
	{
		public VRegType Type { get; private set; }
		public CudaStateSpace StateSpace { get; private set; }
		public StackType StackType { get; private set; }

		//[CanBeNull]
		public Type ReflectionType { get; private set; }

		private object ImmediateValue { get; set; }

		//[CanBeNull]
		public PointerInfo PointerInfo { get; private set; }

		/// <summary>
		/// A name / textual representation which will be used in assembler, 
		/// except for values of type <see cref="VRegType.Immediate"/> which will use <see cref="ImmediateValue"/>.
		/// </summary>
		public string Name { get; set; }

		#region class GlobalFieldEqualityComparer

		/// <summary>
		/// Used by <see cref="PtxEmitter"/> to remember the fields that need 
		/// to be declared globally, and to only declare each once.
		/// </summary>
		public class GlobalFieldEqualityComparer : IEqualityComparer<GlobalVReg>
		{
			public bool Equals(GlobalVReg x, GlobalVReg y)
			{
				Utilities.DebugAssert(x.Type == VRegType.Address && y.Type == VRegType.Address);
				return x.Name == y.Name;
			}

			public int GetHashCode(GlobalVReg obj)
			{
				Utilities.DebugAssert(obj.Type == VRegType.Address);
				return obj.Name.GetHashCode();
			}
		}

		#endregion

		private GlobalVReg() { }

		public static GlobalVReg FromNumericType(StackType stacktype, VRegType type, CudaStateSpace stateSpace)
		{
			return new GlobalVReg { StackType = stacktype, Type = type, StateSpace = stateSpace };
		}

		public static GlobalVReg FromType(StackType stacktype, VRegType type, CudaStateSpace stateSpace, Type reflectionType)
		{
			return new GlobalVReg { StackType = stacktype, Type = type, ReflectionType = reflectionType, StateSpace = stateSpace };
		}

		public static GlobalVReg FromImmediate(object immediateValue, StackType stacktype)
		{
			return new GlobalVReg { StackType = stacktype, ImmediateValue = immediateValue, Type = VRegType.Immediate };
		}

		public static GlobalVReg FromImmediate(object immediateValue)
		{
			return FromImmediate(immediateValue, GetStackTypeForNumericType(immediateValue));
		}

		public static GlobalVReg FromSpecialRegister(StackType stacktype, VRegType type, string text)
		{
			return new GlobalVReg { Name = text, StackType = stacktype, Type = type };
		}

		public static GlobalVReg FromStaticField(FieldInfo field)
		{
			PointerInfo pi;
			VRegType type;
			CudaStateSpace stateSpace;

			if (!field.IsStatic)
				throw new NotSupportedException("Only static fields are supported.");
			if (field.FieldType.IsGenericType &&
				field.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Shared1D<int>).GetGenericTypeDefinition()))
			{
				var att = (StaticArrayAttribute)field.GetCustomAttributes(typeof(StaticArrayAttribute), false).SingleOrDefault();
				if (att == null)
					throw new CudaException("Field " + field.Name + " does not have a StaticArray size specification.");

				pi = new PointerInfo(att.SizeX);
				stateSpace = CudaStateSpace.Shared;
				type = VRegType.Address;
			}
			else
				throw new NotSupportedException("Only Shared1D static fields are currently supported.");

			return new GlobalVReg
					{
						ReflectionType = field.FieldType,
						StackType = GetStackType(field.FieldType),
						Name = EncodeFieldName(field),
						Type = type,
						PointerInfo = pi,
						StateSpace = stateSpace
					};
		}

		private static string EncodeFieldName(FieldInfo field)
		{
			return field.Name;
		}

		public string GetAssemblyText()
		{
			if (ImmediateValue != null)
			{
				if (ImmediateValue is float || ImmediateValue is double)
				{
					string s = ImmediateValue.ToString();
					if (s.IndexOf('.') == -1)
						return ImmediateValue + ".0";
					return s;
				}
				return ImmediateValue.ToString();
			}
			else return Name;
		}

		public static GlobalVReg FromStackTypeDescription(StackTypeDescription stackType, VRegType type, CudaStateSpace stateSpace)
		{
			switch (stackType.CliType)
			{
				case CliType.Int32:
					return FromNumericType(StackType.I4, type, stateSpace);
				case CliType.Int64:
					return FromNumericType(StackType.I8, type, stateSpace);
				case CliType.Float32:
					return FromNumericType(StackType.R4, type, stateSpace);
				case CliType.Float64:
					return FromNumericType(StackType.R8, type, stateSpace);
				case CliType.ObjectType:
					{
						if (stackType.IsArray)
						{
							//							StackType elementtype;
							//							switch (stackType.GetArrayElementType().CliType)
							//							{
							//								case CliType.Int32: elementtype = StackType.I4; break;
							//								case CliType.Float32: elementtype = StackType.R4; break;
							//								case CliType.Int64: elementtype = StackType.I8; break;
							//								case CliType.Float64: elementtype = StackType.R8; break;
							//								case CliType.: elementtype = StackType.R4; break;
							//									
							//							}
							// the type isn't very accurate, but it will do for now...
							return FromType(StackType.Object, type, stateSpace, typeof(Array));
						}
						else
							return FromType(StackType.Object, type, stateSpace, stackType.ComplexType.ReflectionType);
					}
				case CliType.ValueType:
					return FromType(StackType.ValueType, type, stateSpace, stackType.ComplexType.ReflectionType);
				case CliType.ManagedPointer:
					return FromNumericType(StackType.ManagedPointer, type, stateSpace);
				//					throw new NotImplementedException();
				case CliType.NativeInt:
					return FromNumericType(StackType.UnmanangedPointer, type, stateSpace);
				default:
					throw new ArgumentOutOfRangeException("stackType", "Bad CliType: " + stackType.CliType);
			}
		}

		/// <summary>
		/// Returns the size, including any padding
		/// </summary>
		/// <returns></returns>
		public int GetElementSize()
		{
			switch (StackType)
			{
				case StackType.I4:
				case StackType.R4:
					return 4;
				case StackType.I8:
				case StackType.R8:
					return 8;
				default:
					throw new NotImplementedException();
			}
		}

		private static StackType GetStackTypeForNumericType(object value)
		{
			if (value is int || value is uint)
				return StackType.I4;
			if (value is float)
				return StackType.R4;
			if (value is Double)
				return StackType.R8;
			throw new ArgumentOutOfRangeException("value", value, "wtf");
		}

		private static StackType GetStackType(Type type)
		{
			switch (System.Type.GetTypeCode(type))
			{
				case TypeCode.Int32:
				case TypeCode.UInt32:
					return StackType.I4;
				case TypeCode.Single:
					return StackType.R4;
				case TypeCode.Object:
					if (type.IsAssignableFrom(typeof(ValueType)))
						throw new NotSupportedException("Only some built-in value types like int and float are supported.");
					return StackType.Object;
				default:
					throw new ArgumentOutOfRangeException("type", type, "wtf");
			}
		}
	}

	/// <summary>
	/// Describe the intended use of a pointer in PTX: The target state space, and for buffers, the buffer size.
	/// <para>
	/// This information is necessary for declaring global ptx variables based on cli fields.
	/// </para>
	/// </summary>
	class PointerInfo
	{
		//		public CudaStateSpace TargetSpace { get; private set; }
		public int ElementCount { get; private set; }

		//		public PointerInfo(CudaStateSpace targetSpace, int elementCount)
		//		{
		//			TargetSpace = targetSpace;
		//			ElementCount = elementCount;
		//		}

		public PointerInfo(int elementCount)
		{
			ElementCount = elementCount;
		}
	}
}
