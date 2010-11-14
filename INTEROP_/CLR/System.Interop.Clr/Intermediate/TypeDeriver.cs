using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
namespace System.Interop.Intermediate
{
    /// <summary>
    /// Used to derive types in the IR tree.
    /// </summary>
    class TypeDeriver
    {
        static private readonly CliType[,] s_binaryNumericOps = GetBinaryOpsTypeTable();
        private readonly TypeCache _typecache = new TypeCache();
        private static readonly Dictionary<Type, StackTypeDescription> s_metadataCilTypes = new Dictionary<Type, StackTypeDescription>
       	{
       		{typeof (bool), StackTypeDescription.Int32},
       		{typeof (sbyte), StackTypeDescription.Int32},
       		{typeof (byte), StackTypeDescription.Int32},
       		{typeof (short), StackTypeDescription.Int32},
       		{typeof (ushort), StackTypeDescription.Int32},
       		{typeof (char), StackTypeDescription.Int32},
       		{typeof (int), StackTypeDescription.Int32},
       		{typeof (uint), StackTypeDescription.Int32},
       		{typeof (long), StackTypeDescription.Int64},
       		{typeof (ulong), StackTypeDescription.Int64},
       		{typeof (IntPtr), StackTypeDescription.NativeInt},
       		{typeof (UIntPtr), StackTypeDescription.NativeInt},
       		{typeof (float), StackTypeDescription.Float32},
       		{typeof (double), StackTypeDescription.Float64},
       		{typeof (VectorI4), StackTypeDescription.VectorI4},
       		{typeof (VectorF4), StackTypeDescription.VectorF4},
       		{typeof (VectorD2), StackTypeDescription.VectorD2}
       	};

        /// <summary>
        /// Creates and caches <see cref="TypeDescription"/> objects.
        /// </summary>
        public class TypeCache
        {
            /// <summary>
            /// Key is assembly name and full type name.
            /// </summary>
            private Dictionary<Type, TypeDescription> _history;

            public TypeCache()
            {
                _history = new Dictionary<Type, TypeDescription>();
            }

            public TypeDescription GetTypeDescription(Type type)
            {
                //				if (!type.IsPrimitive)
                //					throw new ArgumentException();
                TypeDescription desc;
                if (_history.TryGetValue(type, out desc))
                    return desc;
                desc = CreateTypeDescription(type);
                _history.Add(type, desc);
                return desc;
            }

            private TypeDescription CreateTypeDescription(Type type)
            {
                // TODO: This reference stuff is wrong.
                if (type.IsByRef)
                    return new TypeDescription(type.MakeByRefType());
                else if (type.IsPointer)
                    return new TypeDescription(type.MakePointerType());
                //				else if (type.IsGenericType)
                //					throw new NotImplementedException("Generic types are not yet implemented.");
                else
                    return new TypeDescription(type);
            }
        }

        /// <summary>
        /// Translates the type reference to a <see cref="TypeDescription"/>.
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        private TypeDescription GetTypeDescription(Type reference)
        {
            return _typecache.GetTypeDescription(reference);
        }


        /// <summary>
        /// Derives the type from the instruction's immediate children, ie. it is not recursive.
        /// </summary>
        /// <param name="inst"></param>
        public void DeriveType(TreeInstruction inst)
        {
            StackTypeDescription t;
            switch (inst.Opcode.FlowControl)
            {
                case FlowControl.Branch:
                case FlowControl.Break:
                    t = StackTypeDescription.None;
                    break;
                case FlowControl.Call:
                    {
                        MethodCallInstruction mci = (MethodCallInstruction)inst;
                        foreach (TreeInstruction param in mci.Parameters)
                            DeriveType(param);
                        MethodBase methodBase = mci.IntrinsicMethod ?? mci.OperandMethod;
                        MethodInfo mi = methodBase as MethodInfo;
                        if ((mci.Opcode == IrOpCodes.Newobj) || (mci.Opcode == IrOpCodes.IntrinsicNewObj))
                            t = GetStackTypeDescription(methodBase.DeclaringType);
                        else if ((mi != null) && (mi.ReturnType != typeof(void)))
                            t = GetStackTypeDescription(mi.ReturnType);
                        else
                            t = StackTypeDescription.None;
                    }
                    break;
                case FlowControl.Cond_Branch:
                    t = StackTypeDescription.None;
                    break;
                //				case FlowControl.Meta:
                //				case FlowControl.Phi:
                //					throw new ILException("Meta or Phi.");
                case FlowControl.Next:
                    try
                    {
                        t = DeriveTypeForFlowNext(inst);
                    }
                    catch (NotImplementedException e) { throw new NotImplementedException("Error while deriving flow instruction opcode: " + inst.Opcode.IrCode, e); }
                    break;
                case FlowControl.Return:
                    TreeInstruction firstchild;
                    firstchild = inst.GetChildInstructions().FirstOrDefault();
                    t = (firstchild != null ? inst.Left.StackType : StackTypeDescription.None);
                    break;
                case FlowControl.Throw:
                    t = StackTypeDescription.None;
                    break;
                default:
                    throw new IlSemanticErrorException("Invalid FlowControl: " + inst.Opcode.FlowControl);
            }
            inst.StackType = t;
        }

        /// <summary>
        /// Used by <see cref="DeriveType"/> to derive types for instructions 
        /// that do not change the flow; that is, OpCode.Flow == Next.
        /// </summary>
        /// <param name="inst"></param>
        private StackTypeDescription DeriveTypeForFlowNext(TreeInstruction inst)
        {
            // The cases are generated and all opcodes with flow==next are present (except macro codes such as ldc.i4.3).
            StackTypeDescription t;
            switch (inst.Opcode.IrCode)
            {
                case IrCode.Nop: // nop
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Ldnull: // ldnull
                    t = StackTypeDescription.ObjectType;
                    break;
                case IrCode.Ldc_I4: // ldc.i4
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldc_I8: // ldc.i8
                    t = StackTypeDescription.Int64;
                    break;
                case IrCode.Ldc_R4: // ldc.r4
                    t = StackTypeDescription.Float32;
                    break;
                case IrCode.Ldc_R8: // ldc.r8
                    t = StackTypeDescription.Float64;
                    break;
                case IrCode.Dup: // dup
                    throw new NotImplementedException("dup, pop");
                case IrCode.Pop: // pop
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Ldind_I1: // ldind.i1
                case IrCode.Ldind_U1: // ldind.u1
                case IrCode.Ldind_I2: // ldind.i2
                case IrCode.Ldind_U2: // ldind.u2
                case IrCode.Ldind_I4: // ldind.i4
                case IrCode.Ldind_U4: // ldind.u4
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldind_I8: // ldind.i8
                    t = StackTypeDescription.Int64;
                    break;
                case IrCode.Ldind_I: // ldind.i
                    t = StackTypeDescription.NativeInt;
                    break;
                case IrCode.Ldind_R4: // ldind.r4
                    t = StackTypeDescription.Float32;
                    break;
                case IrCode.Ldind_R8: // ldind.r8
                    t = StackTypeDescription.Float64;
                    break;
                case IrCode.Ldind_Ref: // ldind.ref
                    t = inst.Left.StackType.Dereference();
                    break;
                case IrCode.Stind_Ref: // stind.ref
                case IrCode.Stind_I1: // stind.i1
                case IrCode.Stind_I2: // stind.i2
                case IrCode.Stind_I4: // stind.i4
                case IrCode.Stind_I8: // stind.i8
                case IrCode.Stind_R4: // stind.r4
                case IrCode.Stind_R8: // stind.r8
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Add: // add
                case IrCode.Div: // div
                case IrCode.Sub: // sub
                case IrCode.Mul: // mul
                case IrCode.Rem: // rem
                case IrCode.Add_Ovf: // add.ovf
                case IrCode.Add_Ovf_Un: // add.ovf.un
                case IrCode.Mul_Ovf: // mul.ovf
                case IrCode.Mul_Ovf_Un: // mul.ovf.un
                case IrCode.Sub_Ovf: // sub.ovf
                case IrCode.Sub_Ovf_Un: // sub.ovf.un
                    t = GetNumericResultType(inst.Left.StackType, inst.Right.StackType);
                    break;
                case IrCode.And: // and
                case IrCode.Div_Un: // div.un
                case IrCode.Not: // not
                case IrCode.Or: // or
                case IrCode.Rem_Un: // rem.un
                case IrCode.Xor: // xor
                    // From CIL table 5.
                    if (inst.Left.StackType == inst.Right.StackType)
                        t = inst.Left.StackType;
                    else
                    {
                        if (((inst.Left.StackType == StackTypeDescription.Int32) && (inst.Right.StackType == StackTypeDescription.NativeInt)) ||
                            ((inst.Left.StackType == StackTypeDescription.NativeInt) && (inst.Right.StackType == StackTypeDescription.Int32)))
                            t = StackTypeDescription.NativeInt;
                        else
                            throw new InvalidIrTreeException();
                    }
                    break;
                case IrCode.Shl: // shl
                case IrCode.Shr: // shr
                case IrCode.Shr_Un: // shr.un
                    // CIL table 6.
                    t = inst.Left.StackType;
                    break;
                case IrCode.Neg: // neg
                    t = inst.Left.StackType;
                    break;
                case IrCode.Cpobj: // cpobj
                    throw new NotImplementedException(inst.Opcode.IrCode.ToString());
                case IrCode.Ldobj: // ldobj
                    t = inst.Left.StackType.Dereference();
                    break;
                case IrCode.Stfld: // stfld
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Ldfld: // ldfld
                    t = GetStackTypeDescription(inst.OperandAsField.FieldType);
                    break;
                case IrCode.Ldstr: // ldstr
                case IrCode.Castclass: // castclass
                case IrCode.Isinst: // isinst
                case IrCode.Unbox: // unbox
                case IrCode.Ldflda: // ldflda
                case IrCode.Ldsfld: // ldsfld
                    t = GetStackTypeDescription(inst.OperandAsField.FieldType);
                    break;
                case IrCode.Ldsflda: // ldsflda
                    throw new NotImplementedException(inst.Opcode.IrCode.ToString());
                case IrCode.Stsfld: // stsfld
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Stobj: // stobj
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Conv_Ovf_I8_Un: // conv.ovf.i8.un
                case IrCode.Conv_I8: // conv.i8
                case IrCode.Conv_Ovf_I8: // conv.ovf.i8
                    t = StackTypeDescription.Int64;
                    break;
                case IrCode.Conv_R4: // conv.r4
                    t = StackTypeDescription.Float32;
                    break;
                case IrCode.Conv_R8: // conv.r8
                    t = StackTypeDescription.Float64;
                    break;
                case IrCode.Conv_I1: // conv.i1
                case IrCode.Conv_Ovf_I1_Un: // conv.ovf.i1.un
                case IrCode.Conv_Ovf_I1: // conv.ovf.i1
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Conv_I2: // conv.i2
                case IrCode.Conv_Ovf_I2: // conv.ovf.i2
                case IrCode.Conv_Ovf_I2_Un: // conv.ovf.i2.un
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Conv_Ovf_I4: // conv.ovf.i4
                case IrCode.Conv_I4: // conv.i4
                case IrCode.Conv_Ovf_I4_Un: // conv.ovf.i4.un
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Conv_U4: // conv.u4
                case IrCode.Conv_Ovf_U4: // conv.ovf.u4
                case IrCode.Conv_Ovf_U4_Un: // conv.ovf.u4.un
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Conv_R_Un: // conv.r.un
                    t = StackTypeDescription.Float32; // really F, but we're 32 bit.
                    break;
                case IrCode.Conv_Ovf_U1_Un: // conv.ovf.u1.un
                case IrCode.Conv_U1: // conv.u1
                case IrCode.Conv_Ovf_U1: // conv.ovf.u1
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Conv_Ovf_U2_Un: // conv.ovf.u2.un
                case IrCode.Conv_U2: // conv.u2
                case IrCode.Conv_Ovf_U2: // conv.ovf.u2
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Conv_U8: // conv.u8
                case IrCode.Conv_Ovf_U8_Un: // conv.ovf.u8.un
                case IrCode.Conv_Ovf_U8: // conv.ovf.u8
                    t = StackTypeDescription.Int64;
                    break;
                case IrCode.Conv_I: // conv.i
                case IrCode.Conv_Ovf_I: // conv.ovf.i
                case IrCode.Conv_Ovf_I_Un: // conv.ovf.i.un
                    t = StackTypeDescription.NativeInt;
                    break;
                case IrCode.Conv_Ovf_U_Un: // conv.ovf.u.un
                case IrCode.Conv_Ovf_U: // conv.ovf.u
                case IrCode.Conv_U: // conv.u
                    t = StackTypeDescription.NativeInt;
                    break;
                case IrCode.Box: // box
                    throw new NotImplementedException();
                case IrCode.Newarr: // newarr
                    {
                        StackTypeDescription elementtype = (StackTypeDescription)inst.Operand;
                        t = elementtype.GetArrayType();
                        break;
                    }
                case IrCode.Ldlen: // ldlen
                    t = StackTypeDescription.NativeInt;
                    break;
                case IrCode.Ldelema: // ldelema
                    t = ((StackTypeDescription)inst.Operand).GetManagedPointer();
                    break;
                case IrCode.Ldelem_I1: // ldelem.i1
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldelem_U1: // ldelem.u1
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldelem_I2: // ldelem.i2
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldelem_U2: // ldelem.u2
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldelem_I4: // ldelem.i4
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldelem_U4: // ldelem.u4
                    t = StackTypeDescription.Int32;
                    break;
                case IrCode.Ldelem_I8: // ldelem.i8
                    t = StackTypeDescription.Int64;
                    break;
                case IrCode.Ldelem_I: // ldelem.i
                    // Guess this can also be unsigned?
                    t = StackTypeDescription.NativeInt;
                    break;
                case IrCode.Ldelem_R4: // ldelem.r4
                    t = StackTypeDescription.Float32;
                    break;
                case IrCode.Ldelem_R8: // ldelem.r8
                    t = StackTypeDescription.Float64;
                    break;
                case IrCode.Ldelem_Ref: // ldelem.ref
                    throw new NotImplementedException();
                case IrCode.Stelem_I: // stelem.i
                case IrCode.Stelem_I1: // stelem.i1
                case IrCode.Stelem_I2: // stelem.i2
                case IrCode.Stelem_I4: // stelem.i4
                case IrCode.Stelem_I8: // stelem.i8
                case IrCode.Stelem_R4: // stelem.r4
                case IrCode.Stelem_R8: // stelem.r8
                case IrCode.Stelem_Ref: // stelem.ref
                    t = StackTypeDescription.None;
                    break;
                //				case IRCode.Ldelem_Any: // ldelem.any
                //				case IRCode.Stelem_Any: // stelem.any
                //					throw new ILException("ldelem_any and stelem_any are invalid.");
                case IrCode.Unbox_Any: // unbox.any
                case IrCode.Refanyval: // refanyval
                case IrCode.Ckfinite: // ckfinite
                case IrCode.Mkrefany: // mkrefany
                case IrCode.Ldtoken: // ldtoken
                case IrCode.Stind_I: // stind.i
                case IrCode.Arglist: // arglist
                    throw new NotImplementedException();
                case IrCode.Ceq: // ceq
                case IrCode.Cgt: // cgt
                case IrCode.Cgt_Un: // cgt.un
                case IrCode.Clt: // clt
                case IrCode.Clt_Un: // clt.un
                    t = StackTypeDescription.Int32; // CLI says int32, but let's try...
                    break;
                case IrCode.Ldftn: // ldftn
                case IrCode.Ldvirtftn: // ldvirtftn
                    throw new NotImplementedException();
                case IrCode.Ldarg: // ldarg
                case IrCode.Ldloca: // ldloca
                case IrCode.Ldloc: // ldloc
                case IrCode.Ldarga: // ldarga
                    t = ((MethodVariable)inst.Operand).StackType;
                    if (t == StackTypeDescription.None)
                        throw new NotImplementedException("Invalid variable stack type for load instruction: None.");
                    if ((inst.Opcode.IrCode == IrCode.Ldloca) || (inst.Opcode.IrCode == IrCode.Ldarga))
                        t = t.GetManagedPointer();
                    break;
                case IrCode.Starg: // starg
                case IrCode.Stloc: // stloc
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Localloc: // localloc
                    throw new NotImplementedException();
                case IrCode.Initobj: // initobj
                    t = StackTypeDescription.None;
                    break;
                case IrCode.Constrained: // constrained.
                case IrCode.Cpblk: // cpblk
                case IrCode.Initblk: // initblk
                //				case IRCode.No: // no.
                case IrCode.Sizeof: // sizeof
                case IrCode.Refanytype: // refanytype
                case IrCode.Readonly: // readonly.
                    throw new NotImplementedException();
                default:
                    throw new IlSemanticErrorException();
            }
            return t;
        }
        
        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public StackTypeDescription GetStackTypeDescription(Type type)
        {
            if (type.IsPointer)
                throw new NotSupportedException();
            Type realtype = (type.IsByRef ? type.GetElementType() : type);
            StackTypeDescription std;
            if (realtype.IsPrimitive)
                std = s_metadataCilTypes[realtype];
            else if (realtype == typeof(VectorI4))
                std = StackTypeDescription.VectorI4;
            else if (realtype == typeof(VectorF4))
                std = StackTypeDescription.VectorF4;
            else if (realtype == typeof(VectorD2))
                std = StackTypeDescription.VectorD2;
            else if (realtype == typeof(void))
                std = StackTypeDescription.None;
            else if (realtype.IsArray)
            {
                Type elementtype = realtype.GetElementType();
                if (realtype.GetArrayRank() != 1)
                    throw new NotSupportedException("Only 1D arrays are supported.");
                if (!elementtype.IsValueType || !(elementtype.IsPrimitive || elementtype.Equals(typeof(VectorI4)) || elementtype.Equals(typeof(VectorF4))))
                    throw new NotSupportedException("Only 1D primitive value type arrays are supported.");
                StackTypeDescription elementstd = s_metadataCilTypes[elementtype];
                return elementstd.GetArrayType();
            }
            else
            {
                TypeDescription td = GetTypeDescription(realtype);
                std = new StackTypeDescription(td);
            }
            if (type.IsByRef)
                std = std.GetManagedPointer();
            return std;
        }

        static CliType[,] GetBinaryOpsTypeTable()
        {
            // The diagonal.
            CliType[,] table = new CliType[(int)CliType.ManagedPointer + 1, (int)CliType.ManagedPointer + 1];
            table[(int)CliType.Int32, (int)CliType.Int32] = CliType.Int32;
            table[(int)CliType.Int64, (int)CliType.Int64] = CliType.Int64;
            table[(int)CliType.NativeInt, (int)CliType.NativeInt] = CliType.NativeInt;
            table[(int)CliType.Float32, (int)CliType.Float32] = CliType.Float32;
            table[(int)CliType.Float64, (int)CliType.Float64] = CliType.Float64;
            table[(int)CliType.Int32, (int)CliType.NativeInt] = CliType.NativeInt;
            table[(int)CliType.NativeInt, (int)CliType.Int32] = CliType.NativeInt;
            table[(int)CliType.ManagedPointer, (int)CliType.Int32] = CliType.ManagedPointer;
            table[(int)CliType.Int32, (int)CliType.ManagedPointer] = CliType.ManagedPointer;
            table[(int)CliType.NativeInt, (int)CliType.Int32] = CliType.NativeInt;
            table[(int)CliType.Int32, (int)CliType.NativeInt] = CliType.NativeInt;
            return table;
        }

        /// <summary>
        /// Computes the result type of binary numeric operations given the specified input types.
        /// Computation is done according to table 2 and table 7 in the CIL spec plus intuition.
        /// </summary>
        /// <returns></returns>
        internal static StackTypeDescription GetNumericResultType(StackTypeDescription tleft, StackTypeDescription tright)
        {
            return StackTypeDescription.FromCliType(s_binaryNumericOps[(int)tleft.CliType, (int)tright.CliType]);
        }
    }
}
