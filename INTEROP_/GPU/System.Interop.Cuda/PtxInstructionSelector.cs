using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace System.Interop.Cuda
{
	class PtxInstructionSelector
	{
		public HashSet<FieldInfo> UsedStaticFields { get; private set; }

		public PtxInstructionSelector()
		{
			UsedStaticFields = new HashSet<FieldInfo>();
		}

		public List<BasicBlock> Select(List<BasicBlock> inputblocks)
		{
			// construct all output blocks up front, so we can reference them for branches.
			var blockmap = inputblocks.ToDictionary(ib => ib, ib => new BasicBlock(ib.Name));

			foreach (BasicBlock ib in inputblocks)
			{
				var ob = blockmap[ib];

				foreach (ListInstruction inputinst in ib.Instructions)
				{
					Select(inputinst, ob, blockmap);
				}
			}

			return inputblocks.Select(ib => blockmap[ib]).ToList();
		}

		void Select(ListInstruction inst, BasicBlock ob, Dictionary<BasicBlock, BasicBlock> blockmap)
		{
			ListInstruction new1;
			PtxCode opcode;

			switch (inst.IrCode)
			{
				case IrCode.Add:
					switch (inst.Destination.StackType)
					{
						case StackType.I4: opcode = PtxCode.Add_S32; break;
						case StackType.R4: opcode = PtxCode.Add_F32; break;
						default: throw new InvalidIRException();
					}
					new1 = new ListInstruction(opcode, inst);
					ob.Append(new1);
					return;
				case IrCode.Add_Ovf:
				case IrCode.Add_Ovf_Un:
					break;
				case IrCode.And:
					ob.Append(new ListInstruction(PtxCode.And_B32, inst));
					return;
				case IrCode.Arglist:
					break;
				case IrCode.Beq:
				case IrCode.Bge:
				case IrCode.Bge_Un:
				case IrCode.Bgt:
				case IrCode.Bgt_Un:
				case IrCode.Ble:
				case IrCode.Ble_Un:
				case IrCode.Blt:
				case IrCode.Blt_Un:
				case IrCode.Bne_Un:
				case IrCode.Box:
					break;
				case IrCode.Brfalse:
					{
						Utilities.DebugAssert(inst.Predicate == null);
						GlobalVReg pred = GlobalVReg.FromType(StackType.ValueType, VRegType.Register, CudaStateSpace.Register, typeof(PredicateValue));
						ob.Append(new ListInstruction(PtxCode.Setp_Eq_S32) { Destination = pred, Source1 = inst.Source1, Source2 = GlobalVReg.FromImmediate(0) });
						ob.Append(new ListInstruction(PtxCode.Bra) { Operand = inst.Operand, Predicate = pred });
					}
					return;
				case IrCode.Brtrue:
					{
						Utilities.DebugAssert(inst.Predicate == null);
						GlobalVReg pred = GlobalVReg.FromType(StackType.ValueType, VRegType.Register, CudaStateSpace.Register, typeof(PredicateValue));
						ob.Append(new ListInstruction(PtxCode.Setp_Eq_S32) { Destination = pred, Source1 = inst.Source1, Source2 = GlobalVReg.FromImmediate(0) });
						ob.Append(new ListInstruction(PtxCode.Bra) { Operand = inst.Operand, Predicate = pred, PredicateNegation = true });
					}
					return;
				//					throw new InvalidIRException("Conditional branch code " + inst.IrCode + " encountered.");
				case IrCode.Br:
					new1 = new ListInstruction(PtxCode.Bra, inst) { Operand = blockmap[(BasicBlock)inst.Operand] };
					ob.Append(new1);
					return;
				case IrCode.Break:
				case IrCode.Call:
					//					ob.Append(new MethodCallListInstruction(PtxCode.Call, inst));
					HandleCall((MethodCallListInstruction)inst, ob);
					return;
				case IrCode.Calli:
				case IrCode.Callvirt:
				case IrCode.Castclass:
					break;
				case IrCode.Ceq:
					switch (inst.Source1.StackType)
					{
						case StackType.I4: opcode = PtxCode.Setp_Eq_S32; break;
						case StackType.R4: opcode = PtxCode.Setp_Eq_F32; break;
						default: throw new NotImplementedException();
					}
					ob.Append(new ListInstruction(opcode, inst));
					return;
				case IrCode.Cgt:
					switch (inst.Source1.StackType)
					{
						case StackType.I4: opcode = PtxCode.Setp_Gt_S32; break;
						case StackType.R4: opcode = PtxCode.Setp_Gt_F32; break;
						default: throw new NotImplementedException();
					}
					ob.Append(new ListInstruction(opcode, inst));
					return;
				case IrCode.Cgt_Un:
					switch (inst.Source1.StackType)
					{
						case StackType.I4: opcode = PtxCode.Setp_Hi_U32; break;
						case StackType.R4: opcode = PtxCode.Setp_Gtu_F32; break;
						default: throw new NotImplementedException();
					}
					ob.Append(new ListInstruction(opcode, inst));
					return;
				case IrCode.Ckfinite:
					break;
				case IrCode.Clt:
					switch (inst.Source1.StackType)
					{
						case StackType.I4: opcode = PtxCode.Setp_Lt_S32; break;
						case StackType.R4: opcode = PtxCode.Setp_Lt_F32; break;
						default: throw new NotImplementedException();
					}
					ob.Append(new ListInstruction(opcode, inst));
					return;
				case IrCode.Clt_Un:
					switch (inst.Source1.StackType)
					{
						case StackType.I4: opcode = PtxCode.Setp_Lo_U32; break;
						case StackType.R4: opcode = PtxCode.Setp_Ltu_F32; break;
						default: throw new NotImplementedException();
					}
					ob.Append(new ListInstruction(opcode, inst));
					return;
				case IrCode.Constrained:
				case IrCode.Conv_I:
				case IrCode.Conv_I1:
				case IrCode.Conv_I2:
					break;
				case IrCode.Conv_I4:
					switch (inst.Source1.StackType)
					{
						case StackType.R4: ob.Append(new ListInstruction(PtxCode.Cvt_Rzi_S32_F32, inst)); return;
						default: throw new NotSupportedException();
					}
				case IrCode.Conv_I8:
				case IrCode.Conv_Ovf_I:
				case IrCode.Conv_Ovf_I_Un:
				case IrCode.Conv_Ovf_I1:
				case IrCode.Conv_Ovf_I1_Un:
				case IrCode.Conv_Ovf_I2:
				case IrCode.Conv_Ovf_I2_Un:
				case IrCode.Conv_Ovf_I4:
				case IrCode.Conv_Ovf_I4_Un:
				case IrCode.Conv_Ovf_I8:
				case IrCode.Conv_Ovf_I8_Un:
				case IrCode.Conv_Ovf_U:
				case IrCode.Conv_Ovf_U_Un:
				case IrCode.Conv_Ovf_U1:
				case IrCode.Conv_Ovf_U1_Un:
				case IrCode.Conv_Ovf_U2:
				case IrCode.Conv_Ovf_U2_Un:
				case IrCode.Conv_Ovf_U4:
				case IrCode.Conv_Ovf_U4_Un:
				case IrCode.Conv_Ovf_U8:
				case IrCode.Conv_Ovf_U8_Un:
				case IrCode.Conv_R_Un:
					break;
				case IrCode.Conv_R4:
					switch (inst.Source1.StackType)
					{
						case StackType.I4: ob.Append(new ListInstruction(PtxCode.Cvt_Rn_F32_S32, inst)); return;
						default: throw new NotSupportedException();
					}
				case IrCode.Conv_R8:
				case IrCode.Conv_U:
				case IrCode.Conv_U1:
				case IrCode.Conv_U2:
				case IrCode.Conv_U4:
					switch (inst.Source1.StackType)
					{
						case StackType.R4: ob.Append(new ListInstruction(PtxCode.Cvt_Rzi_U32_F32, inst)); return;
						default: throw new NotSupportedException();
					}
				case IrCode.Conv_U8:
				case IrCode.Cpblk:
				case IrCode.Cpobj:
					break;
				case IrCode.Div:
					switch (inst.Destination.StackType)
					{
						case StackType.I4: opcode = PtxCode.Div_S32; break;
						case StackType.R4: opcode = PtxCode.Div_F32; break;
						default: throw new InvalidIRException();
					}
					new1 = new ListInstruction(opcode, inst);
					ob.Append(new1);
					return;
				case IrCode.Div_Un:
					switch (inst.Destination.StackType)
					{
						case StackType.I4: opcode = PtxCode.Div_U32; break;
						default: throw new InvalidIRException();
					}
					new1 = new ListInstruction(opcode, inst);
					ob.Append(new1);
					return;
				case IrCode.Dup:
				case IrCode.Endfilter:
				case IrCode.Endfinally:
				case IrCode.Initblk:
				case IrCode.Initobj:
				case IrCode.IntrinsicCall:
				case IrCode.IntrinsicNewObj:
				case IrCode.Isinst:
				case IrCode.Jmp:
					break;
				case IrCode.Ldarg:
					switch (inst.Destination.StackType)
					{
						case StackType.Object: opcode = PtxCode.Ld_Param_S32; break;
						case StackType.I4: opcode = PtxCode.Ld_Param_S32; break;
						case StackType.R4: opcode = PtxCode.Ld_Param_F32; break;
						default: throw new InvalidIRException();
					}
					new1 = new ListInstruction(opcode, inst);
					ob.Append(new1);
					return;
				case IrCode.Ldarga:
					break;
				case IrCode.Ldc_I4:
					ob.Append(new ListInstruction(PtxCode.Mov_S32, inst) { Source1 = GlobalVReg.FromImmediate(inst.Operand, StackType.I4), Operand = null });
					return;
				case IrCode.Ldc_I8:
					break;
				case IrCode.Ldc_R4:
					ob.Append(new ListInstruction(PtxCode.Mov_F32, inst) { Source1 = GlobalVReg.FromImmediate(inst.Operand, StackType.R4), Operand = null });
					return;
				case IrCode.Ldc_R8:
					break;
				case IrCode.Ldelem:
				case IrCode.Ldelem_I:
				case IrCode.Ldelem_I1:
				case IrCode.Ldelem_I2:
				case IrCode.Ldelem_I8:
					break;
				case IrCode.Ldelem_R4: SelectLdElem(inst, ob, PtxCode.Ld_Global_F32, 4); return;
				case IrCode.Ldelem_R8:
				case IrCode.Ldelem_Ref:
				case IrCode.Ldelem_U1:
				case IrCode.Ldelem_U2:
					break;
				case IrCode.Ldelem_I4:
				case IrCode.Ldelem_U4: SelectLdElem(inst, ob, PtxCode.Ld_Global_S32, 4); return;
				case IrCode.Ldelema:
					{
						var elementsize = inst.OperandAsGlobalVRegNonNull.GetElementSize();
						GenerateComputeElementAddress(inst, inst.Destination, inst.Source1, inst.Source2, elementsize, ob);
						return;
					}
				case IrCode.Ldfld:
				case IrCode.Ldflda:
				case IrCode.Ldftn:
				case IrCode.Ldind_I:
				case IrCode.Ldind_I1:
				case IrCode.Ldind_I2:
				case IrCode.Ldind_I4:
				case IrCode.Ldind_I8:
				case IrCode.Ldind_R4:
				case IrCode.Ldind_R8:
				case IrCode.Ldind_Ref:
				case IrCode.Ldind_U1:
				case IrCode.Ldind_U2:
				case IrCode.Ldind_U4:
				case IrCode.Ldlen:
					break;
				case IrCode.Ldloc:
					{
						switch (inst.OperandAsGlobalVReg.StackType)
						{
							case StackType.I4:
							case StackType.Object: opcode = PtxCode.Mov_S32; break;
							case StackType.R4: opcode = PtxCode.Mov_F32; break;
							default: throw new NotImplementedException();
						}
						ob.Append(new ListInstruction(opcode, inst) { Source1 = inst.OperandAsGlobalVReg });
						return;
					}
				case IrCode.Ldloca:
					break;
				case IrCode.Ldnull:
					// RH 20080816: Using i4 for now.
					ob.Append(new ListInstruction(PtxCode.Mov_S32, inst) { Source1 = GlobalVReg.FromImmediate(0, StackType.I4) });
					break;
				case IrCode.Ldobj:
					break;
				case IrCode.Ldsfld:
					{
						var field = (FieldInfo)inst.Operand;
						UsedStaticFields.Add(field);
						ob.Append(new ListInstruction(PtxCode.Mov_S32, inst) { Source1 = GlobalVReg.FromStaticField(field) });
						return;
					}
				case IrCode.Ldsflda:
				case IrCode.Ldstr:
				case IrCode.Ldtoken:
				case IrCode.Ldvirtftn:
				case IrCode.Leave:
				case IrCode.Leave_S:
				case IrCode.Localloc:
				case IrCode.Mkrefany:
					break;
				case IrCode.Mul:
					switch (inst.Destination.StackType)
					{
						case StackType.I4: opcode = PtxCode.Mul_Lo_S32; break;
						case StackType.R4: opcode = PtxCode.Mul_F32; break;
						default: throw new InvalidIRException();
					}
					new1 = new ListInstruction(opcode, inst);
					ob.Append(new1);
					return;
				case IrCode.Mul_Ovf:
				case IrCode.Mul_Ovf_Un:
				case IrCode.Neg:
				case IrCode.Newarr:
				case IrCode.Newobj:
					break;
				case IrCode.Nop:
					return;
				case IrCode.Not:
					break;
				case IrCode.Or:
					ob.Append(new ListInstruction(PtxCode.Or_B32, inst));
					return;
				case IrCode.Pop:
					return;
				case IrCode.PpeCall:
				case IrCode.Prefix1:
				case IrCode.Prefix2:
				case IrCode.Prefix3:
				case IrCode.Prefix4:
				case IrCode.Prefix5:
				case IrCode.Prefix6:
				case IrCode.Prefix7:
				case IrCode.Prefixref:
				case IrCode.Readonly:
				case IrCode.Refanytype:
				case IrCode.Refanyval:
				case IrCode.Rem:
				case IrCode.Rem_Un:
					break;
				case IrCode.Ret:
					new1 = new ListInstruction(PtxCode.Ret);
					ob.Append(new1);
					return;
				case IrCode.Rethrow:
					break;
				case IrCode.Shl:
					ob.Append(new ListInstruction(PtxCode.Shl_B32, inst));
					return;
				case IrCode.Shr:
					ob.Append(new ListInstruction(PtxCode.Shr_S32, inst));
					return;
				case IrCode.Shr_Un:
					ob.Append(new ListInstruction(PtxCode.Shr_U32, inst));
					return;
				case IrCode.Sizeof:
				case IrCode.SpuInstructionMethodCall:
				case IrCode.Starg:
				case IrCode.Stelem:
				case IrCode.Stelem_I:
				case IrCode.Stelem_I1:
				case IrCode.Stelem_I2:
				case IrCode.Stelem_I4:
				case IrCode.Stelem_I8:
				case IrCode.Stelem_R4:
				case IrCode.Stelem_R8:
				case IrCode.Stelem_Ref:
				case IrCode.Stfld:
				case IrCode.Stind_I:
				case IrCode.Stind_I1:
					break;
				case IrCode.Stind_I2:
					ob.Append(new ListInstruction(PtxCode.St_Global_S16, inst));
					return;
				case IrCode.Stind_I4:
					ob.Append(new ListInstruction(PtxCode.St_Global_S32, inst));
					return;
				case IrCode.Stind_I8:
					break;
				case IrCode.Stind_R4:
					ob.Append(new ListInstruction(PtxCode.St_Global_F32, inst));
					return;
				case IrCode.Stind_R8:
				case IrCode.Stind_Ref:
				case IrCode.Stloc:
					{
						switch (inst.OperandAsGlobalVReg.StackType)
						{
							case StackType.I4:
							case StackType.Object: opcode = PtxCode.Mov_S32; break;
							case StackType.R4: opcode = PtxCode.Mov_F32; break;
							default: throw new NotImplementedException();
						}
						ob.Append(new ListInstruction(opcode, inst) { Destination = inst.OperandAsGlobalVReg, Operand = null });
						return;
					}
				case IrCode.Stobj:
				case IrCode.Stsfld:
					break;
				case IrCode.Sub:
					switch (inst.Destination.StackType)
					{
						case StackType.I4: opcode = PtxCode.Sub_S32; break;
						case StackType.R4: opcode = PtxCode.Sub_F32; break;
						default: throw new InvalidIRException();
					}
					new1 = new ListInstruction(opcode, inst);
					ob.Append(new1);
					return;
				case IrCode.Sub_Ovf:
				case IrCode.Sub_Ovf_Un:
				case IrCode.Switch:
				case IrCode.Tailcall:
				case IrCode.Throw:
					// kind of a hack, but convenient at least for initial testing.
					ob.Append(new ListInstruction(PtxCode.Exit));
					break;
				case IrCode.Unaligned:
				case IrCode.Unbox:
				case IrCode.Unbox_Any:
				case IrCode.Volatile:
					break;
				case IrCode.Xor:
					ob.Append(new ListInstruction(PtxCode.Xor_B32, inst));
					return;
			}

			throw new NotImplementedException("Opcode not implemented in instruction selector: " + inst.IrCode);
		}

		private void GenerateComputeElementAddress(ListInstruction inst, GlobalVReg destination, GlobalVReg baseAddress, GlobalVReg index, int elementsize, BasicBlock ob)
		{
			var offset = GlobalVReg.FromNumericType(StackType.I4, VRegType.Register, CudaStateSpace.Register);

			// Determine byte offset.
			Utilities.AssertArgument(elementsize == 4, "Only four-byte elements are currently supported.");
			const int shiftcount = 2;
			ob.Append(new ListInstruction(PtxCode.Shl_B32)
						{
							Destination = offset,
							Source1 = index,
							Source2 = GlobalVReg.FromImmediate(shiftcount, StackType.I4),
							Predicate = inst.Predicate,
							PredicateNegation = inst.PredicateNegation
						});
			// Determine element address.
			ob.Append(new ListInstruction(PtxCode.Add_S32)
						{
							Destination = destination,
							Source1 = baseAddress,
							Source2 = offset,
							Predicate = inst.Predicate,
							PredicateNegation = inst.PredicateNegation
						});
		}

		private void HandleCall(MethodCallListInstruction inst, BasicBlock ob)
		{
			SpecialMethodInfo smi;
			if (!(inst.Operand is MethodBase))
			{
				if (inst.Operand is CudaMethod)
					throw new NotImplementedException("Method calls are not implemented. Method: " +
													  ((CudaMethod)inst.Operand).PtxName);
				else
					throw new InvalidIRException("Bad method call operand: " + inst.Operand);
			}
			var calledMethod = (MethodBase)inst.Operand;
			if (!SpecialMethodInfo.TryGetMethodInfo(calledMethod, out smi))
				throw new InvalidIRException("Special method without metadata encountered.");

			if (smi.IsGlobalVReg)
			{
				PtxCode ptxcode;
				switch (smi.HardcodedGlobalVReg.StackType)
				{
					case StackType.I2: ptxcode = PtxCode.Cvt_S32_U16; break;
					case StackType.I4: ptxcode = PtxCode.Mov_S32; break;
					default: throw new InvalidIRException();
				}
				ob.Append(new ListInstruction(ptxcode)
							{
								Destination = inst.Destination,
								Source1 = smi.HardcodedGlobalVReg,
								Predicate = inst.Predicate,
								PredicateNegation = inst.PredicateNegation
							});
				return;
			}
			if (smi.IsSinglePtxCode)
			{
				var newinst = new ListInstruction(smi.PtxCode)
								{
									Destination = inst.Destination,
									Source1 = inst.Parameters.ElementAtOrDefault(0),
									Source2 = inst.Parameters.ElementAtOrDefault(1),
									Source3 = inst.Parameters.ElementAtOrDefault(2),
									Predicate = inst.Predicate,
									PredicateNegation = inst.PredicateNegation,
								};
				if (smi.HardcodedGlobalVReg != null)
					newinst.Source1 = smi.HardcodedGlobalVReg;

				switch (smi.PtxCode)
				{
					case PtxCode.Bar_Sync:
						newinst.Source1 = GlobalVReg.FromImmediate(0, StackType.I4);
						break;
				}
				ob.Append(newinst);
				return;
			}
			if (smi.IsSpecialMethodCode)
			{
				switch (smi.SpecialMethodCode)
				{
					case SpecialMethodCode.Shared1DLoad:
						{
							var address = GlobalVReg.FromNumericType(StackType.ManagedPointer, VRegType.Register, CudaStateSpace.Register);
							GenerateComputeElementAddress(inst, address, inst.Parameters[0], inst.Parameters[1], 4, ob);
							PtxCode code;
							switch (inst.Destination.StackType)
							{
								case StackType.I4: code = PtxCode.Ld_Shared_S32; break;
								case StackType.R4: code = PtxCode.Ld_Shared_F32; break;
								default: throw new InvalidIRException();
							}
							ob.Append(new ListInstruction(code)
										{
											Destination = inst.Destination,
											Source1 = address,
											Predicate = inst.Predicate,
											PredicateNegation = inst.PredicateNegation
										});
						}
						break;
					case SpecialMethodCode.Shared1DStore:
						{
							var address = GlobalVReg.FromNumericType(StackType.ManagedPointer, VRegType.Register, CudaStateSpace.Register);
							GenerateComputeElementAddress(inst, address, inst.Parameters[0], inst.Parameters[1], 4, ob);
							PtxCode code;
							GlobalVReg value = inst.Parameters[2];
							switch (value.StackType)
							{
								case StackType.I4: code = PtxCode.St_Shared_S32; break;
								case StackType.R4: code = PtxCode.St_Shared_F32; break;
								default: throw new InvalidIRException();
							}
							ob.Append(new ListInstruction(code)
							{
								Source1 = address,
								Source2 = value,
								Predicate = inst.Predicate,
								PredicateNegation = inst.PredicateNegation
							});
						}
						break;
				}
				return;
			}

			throw new NotImplementedException();
		}

		private void SelectLdElem(ListInstruction ldElemInst, BasicBlock ob, PtxCode ptxLoadCode, int elementsize)
		{
			var offset = GlobalVReg.FromNumericType(StackType.I4, VRegType.Register, CudaStateSpace.Register);
			// Determine byte offset.
			ob.Append(new ListInstruction(PtxCode.Mul_Lo_S32, ldElemInst)
						{
							Destination = offset,
							Source1 = ldElemInst.Source2, // index
							Source2 = GlobalVReg.FromImmediate(elementsize, StackType.I4)
						});
			// Determine element address.
			var address = GlobalVReg.FromNumericType(StackType.I4, VRegType.Register, CudaStateSpace.Register);
			ob.Append(new ListInstruction(PtxCode.Add_S32, ldElemInst)
						{
							Destination = address,
							Source1 = ldElemInst.Source1,
							Source2 = offset
						});
			ob.Append(new ListInstruction(ptxLoadCode, ldElemInst)
						{
							Destination = ldElemInst.Destination,
							Source1 = address,
							Source2 = null
						});
		}
	}
}
