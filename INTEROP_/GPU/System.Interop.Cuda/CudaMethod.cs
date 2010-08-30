using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Interop.Intermediate;
//using CellDotNet.Intermediate;
//using CellDotNet.Spe;

namespace System.Interop.Cuda
{
	internal enum CudaMethodCompileState
	{
		None,
		TreeConstructionDone,
		ListContructionDone,
		ConditionalBranchHandlingDone,
		InstructionSelectionDone
	}

	/// <summary>
	/// Equivalent of <see cref="MethodCompiler"/>. Maybe this one should also be used for cell one day.
	/// </summary>
	class CudaMethod
	{
		public CudaMethodCompileState State { get; private set; }

		private readonly MethodBase _method;
		public string PtxName { get; private set; }

		public List<BasicBlock> Blocks { get; private set; }
		public List<GlobalVReg> Parameters { get; private set; }

		public CudaMethod(MethodBase method)
		{
			Utilities.AssertArgumentNotNull(method, "method");

			// Protect lambdas.
			PtxName = method.Name.Replace('<', '$').Replace('>', '$');

			_method = method;
		}

		public void PerformProcessing(CudaMethodCompileState targetstate)
		{
			List<IRBasicBlock> treeblocks = null;
			List<MethodVariable> variables = null;
			List<MethodParameter> parameters = null;

			if (targetstate > State && State == CudaMethodCompileState.None)
			{
				PerformTreeConstruction(_method, out treeblocks, out parameters, out variables);
				// We're not storing the tree...
				//				_state = CompileState.TreeConstructionDone;
			}

			if (targetstate > State && State <= CudaMethodCompileState.ListContructionDone - 1)
			{
				List<BasicBlock> newblocks;
				List<GlobalVReg> newparams;
				new TreeConverter().PerformListConstruction(treeblocks, parameters, variables, PtxName, out newblocks, out newparams);
				Blocks = newblocks;
				Parameters = newparams;
				State = CudaMethodCompileState.ListContructionDone;
			}
			if (targetstate > State && State <= CudaMethodCompileState.ConditionalBranchHandlingDone - 1)
			{
				PerformConditionalBranchDecomposition();
				State = CudaMethodCompileState.ConditionalBranchHandlingDone;
			}
			if (targetstate > State && State == CudaMethodCompileState.InstructionSelectionDone - 1)
			{
				PerformInstructionSelection();
				State = CudaMethodCompileState.InstructionSelectionDone;
			}
		}

		private void PerformConditionalBranchDecomposition()
		{
			foreach (var block in Blocks)
			{
				ListInstruction inst = block.Head;
				while (inst != null)
				{
					IrCode cmpopcode = 0;
					GlobalVReg pred = null;
					bool predicatenegation = false;

					var isFP = !(inst is MethodCallListInstruction) && inst.Source1 != null &&
							   (inst.Source1.StackType == StackType.R4 || inst.Source1.StackType == StackType.R8);

					switch (inst.IrCode)
					{
						case IrCode.Beq:
							ReplaceConditional(inst, block, IrCode.Ceq, false);
							break;
						case IrCode.Bge:
							cmpopcode = isFP ? IrCode.Clt_Un : IrCode.Clt;
							ReplaceConditional(inst, block, cmpopcode, true);
							break;
						case IrCode.Bge_Un:
							ReplaceConditional(inst, block, IrCode.Clt_Un, true);
							break;
						case IrCode.Bgt:
							ReplaceConditional(inst, block, IrCode.Cgt, false);
							break;
						case IrCode.Bgt_Un:
							ReplaceConditional(inst, block, IrCode.Cgt_Un, false);
							break;
						case IrCode.Ble:
							cmpopcode = isFP ? IrCode.Cgt_Un : IrCode.Cgt;
							ReplaceConditional(inst, block, cmpopcode, true);
							break;
						case IrCode.Ble_Un:
							cmpopcode = isFP ? IrCode.Cgt : IrCode.Cgt_Un;
							ReplaceConditional(inst, block, cmpopcode, true);
							break;
						case IrCode.Blt:
							ReplaceConditional(inst, block, IrCode.Clt, false);
							break;
						case IrCode.Blt_Un:
							ReplaceConditional(inst, block, IrCode.Clt_Un, false);
							break;
						case IrCode.Bne_Un:
							ReplaceConditional(inst, block, IrCode.Ceq, true);
							break;
						case IrCode.Brfalse:
						case IrCode.Brtrue:
							// We don't handle the case where the condition is a native int, because that would mean
							// using ptx setp here, and it's probably best to avoid ptx opcodes before instruction selection.
							// Alternatively, an intrinsic method call could be used.
							// 
							// At least for now (20080827), we will handle them in the instruction selector.
							break;
					}

					inst = inst.Next;
				}
			}
		}

		private static void ReplaceConditional(ListInstruction inst, BasicBlock block, IrCode cmpopcode, bool predicateNegation)
		{
			Utilities.DebugAssert(inst.Predicate == null, "Can't handle existing predicate.");
			var target = (BasicBlock)inst.Operand;

			GlobalVReg pred = GlobalVReg.FromType(StackType.ValueType, VRegType.Register, CudaStateSpace.Register, typeof(PredicateValue));
			var newcmp = new ListInstruction(cmpopcode) { Source1 = inst.Source1, Source2 = inst.Source2, Destination = pred };
			block.Replace(inst, newcmp);
			inst = newcmp;

			var newbranch = new ListInstruction(IrCode.Br, target) { Predicate = pred, PredicateNegation = predicateNegation };

			block.InsertAfter(inst, newbranch);
		}

		static void PerformTreeConstruction(MethodBase method, out List<IRBasicBlock> treeBlocks,
									out List<MethodParameter> parameters, out List<MethodVariable> variables)
		{
			var typederiver = new TypeDeriver();

			// Build Parameters.
			parameters = new List<MethodParameter>();
			int i = 0;

			if ((method.CallingConvention & CallingConventions.HasThis) != 0)
			{
				StackTypeDescription type = new StackTypeDescription(new TypeDescription(method.DeclaringType));

				StackTypeDescription thistype;
				if (!type.IsPointerType && type.CliType == CliType.ValueType)
					thistype = type.GetManagedPointer();
				else
					thistype = type;

				parameters.Add(new MethodParameter(thistype));
				i++;
			}

			foreach (ParameterInfo pi in method.GetParameters())
			{
				//Not true in instance methods.
				//				Utilities.Assert(pi.Position == i, "pi.Index == i");

				Utilities.Assert(pi.Position == i - ((method.CallingConvention & CallingConventions.HasThis) != 0 ? 1 : 0), "pi.Index == i");
				i++;

				parameters.Add(new MethodParameter(pi, typederiver.GetStackTypeDescription(pi.ParameterType)));
			}


			// Build Variables.
			variables = new List<MethodVariable>();
			i = 0;
			if (!(method is DynamicMethod))
			{
				MethodBody body = method.GetMethodBody();
				if (body == null)
					throw new MethodResolveException("Method " + method.Name + " has no body.");

				foreach (LocalVariableInfo lv in body.LocalVariables)
				{
					Utilities.Assert(lv.LocalIndex == i, "lv.LocalIndex == i");
					i++;

					variables.Add(new MethodVariable(lv, typederiver.GetStackTypeDescription(lv.LocalType)));
				}
			}

			if (method is MethodInfo)
			{
				MethodInfo mi = (MethodInfo)method;
				//				var _returnType = typederiver.GetStackTypeDescription(mi.ReturnType);
			}


			ILReader reader = new ILReader(method);
			try
			{
				treeBlocks = new IRTreeBuilder().BuildBasicBlocks(method, reader, variables,
																  new ReadOnlyCollection<MethodParameter>(parameters));
			}
			catch (NotImplementedException e)
			{
				throw new ILParseException(string.Format("An error occurred while parsing method '{0}.{1}'.",
					method.DeclaringType.Name, method.Name), e);
			}
			catch (ILParseException e)
			{
				throw new ILParseException(string.Format("An error occurred while parsing method '{0}.{1}'.",
														 method.DeclaringType.Name, method.Name), e);
			}
		}


		#region TreeConverter

		class TreeConverter
		{
			private Dictionary<MethodVariable, GlobalVReg> _variableMap;

			/// <summary>
			/// Constructs list IR from tree IR.
			/// </summary>
			public void PerformListConstruction(List<IrBasicBlock> treeblocks, List<MethodParameter> oldparameters, List<MethodVariable> oldvariables, string ptxname, out List<BasicBlock> newblocks, out List<GlobalVReg> newparams)
			{
				_variableMap = new Dictionary<MethodVariable, GlobalVReg>();
				newparams = new List<GlobalVReg>(oldparameters.Count);
				foreach (MethodParameter oldp in oldparameters)
				{
					var newp = GlobalVReg.FromStackTypeDescription(oldp.StackType, VRegType.Address, CudaStateSpace.Parameter);
					// TODO: Prefix the name to avoid clashes with other symbols.
					newp.Name = oldp.Name;
					newparams.Add(newp);
					_variableMap.Add(oldp, newp);
				}
				foreach (MethodVariable variable in oldvariables)
					_variableMap.Add(variable, GlobalVReg.FromStackTypeDescription(variable.StackType, VRegType.Register, CudaStateSpace.Register));

				// construct all output blocks up front, so we can reference them for branches.
				var blocknum = 0;
				var blockmap = treeblocks.ToDictionary(tb => tb, tb => new BasicBlock("LL" + ptxname + blocknum++));

				foreach (IRBasicBlock irblock in treeblocks)
				{
					var block = blockmap[irblock];

					foreach (var treeroot in irblock.Roots)
					{
						ConvertTreeNode(treeroot, block, blockmap, false);
					}
				}

				newblocks = treeblocks.Select(tb => blockmap[tb]).ToList();
			}

			/// <summary>
			/// Recursively convert <paramref name="treenode"/> to a <see cref="ListInstruction"/>
			/// and returns the produced variable/register.
			/// </summary>
			/// <param name="treenode"></param>
			/// <param name="block"></param>
			/// <param name="returnsValue"></param>
			/// <returns></returns>
			private GlobalVReg ConvertTreeNode(TreeInstruction treenode, BasicBlock block, Dictionary<IRBasicBlock, BasicBlock> blockmap, bool returnsValue)
			{
				ListInstruction newinst;

				if (treenode is MethodCallInstruction)
				{
					var mci = (MethodCallInstruction)treenode;
					var callinst = new MethodCallListInstruction(treenode.Opcode.IrCode, mci.Operand);

					foreach (TreeInstruction parameter in mci.Parameters)
					{
						callinst.Parameters.Add(ConvertTreeNode(parameter, block, blockmap, true));
					}
					newinst = callinst;
				}
				else
				{
					object operand;
					if (treenode.Operand is IRBasicBlock)
						operand = blockmap[treenode.OperandAsBasicBlock];
					else if (treenode.Operand is MethodVariable)
						operand = _variableMap[treenode.OperandAsVariable];
					else if (treenode.Operand is StackTypeDescription)
					{
						operand = GlobalVReg.FromStackTypeDescription(((StackTypeDescription)treenode.Operand), VRegType.None, CudaStateSpace.None);
					}
					else
						operand = treenode.Operand;


					// Some special cases/optimizations.
					switch (treenode.Opcode.IrCode)
					{
						case IrCode.Ldloc:
							return (GlobalVReg)operand;
						case IrCode.Ldc_I4:
							return GlobalVReg.FromImmediate(operand, StackType.I4);
						case IrCode.Ldc_R4:
							return GlobalVReg.FromImmediate(operand, StackType.R4);
					}


					newinst = new ListInstruction(treenode.Opcode.IrCode, operand);
					if (treenode.Left != null)
						newinst.Source1 = ConvertTreeNode(treenode.Left, block, blockmap, true);
					if (treenode.Right != null)
						newinst.Source2 = ConvertTreeNode(treenode.Right, block, blockmap, true);
				}

				block.Append(newinst);
				if (returnsValue)
				{
					newinst.Destination = GlobalVReg.FromStackTypeDescription(treenode.StackType, VRegType.Register, CudaStateSpace.Register);
					return newinst.Destination;
				}
				return null;
			}
		}

		#endregion

		private void PerformInstructionSelection()
		{
			var selector = new PtxInstructionSelector();
			Blocks = selector.Select(Blocks);


			FixS32ComparisonResultsPtxPass(Blocks);

			//			throw new NotImplementedException();
		}

		/// <summary>
		/// Comparisons in IL return an i4, so we need to insert a predicate vreg and then convert it to an i4.
		/// Comparisons resulting from conditional branche decomposition indicate that they return predicates, so we 
		/// don't need to do anything in those cases.
		/// This should probably ideally be done before instruction selection.
		/// </summary>
		private static void FixS32ComparisonResultsPtxPass(List<BasicBlock> blocks)
		{
			foreach (var block in blocks)
			{
				ListInstruction inst = block.Head;
				while (inst != null)
				{
					if (inst.PtxCode >= PtxCode.Setp_First && inst.PtxCode <= PtxCode.Setp_Last &&
						inst.Destination.StackType == StackType.I4)
					{
						var pred = GlobalVReg.FromType(StackType.ValueType, VRegType.Register, CudaStateSpace.Register, typeof(PredicateValue));
						var cmp = new ListInstruction(inst.PtxCode, inst) { Destination = pred };
						var conv = new ListInstruction(PtxCode.Selp_S32, inst) { Destination = inst.Destination, Source1 = GlobalVReg.FromImmediate(1, StackType.I4), Source2 = GlobalVReg.FromImmediate(0, StackType.I4), Source3 = pred };

						block.Replace(inst, cmp);
						block.InsertAfter(cmp, conv);
						inst = cmp;
					}

					inst = inst.Next;
				}
			}
		}
	}
}