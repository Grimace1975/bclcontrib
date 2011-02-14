using System.Collections.Generic;
using System.IO;
using System.Reflection;
namespace System.Interop.Cuda
{
	enum CudaKernelCompileState
	{
		None,
		IRConstructionDone,
		InstructionSelectionDone,
		PtxEmissionComplete,
		PtxCompilationComplete,
		Complete,
	}

	public class CudaKernel<T> : CudaKernel where T : class
	{
		private readonly T _kernelWrapperDelegate;

		internal CudaKernel(MethodInfo kernelMethod)
			: base(kernelMethod)
		{
			throw new NotImplementedException();
		}
		//
		//		public CudaKernel(T kerneldelegate)
		//		{
		//			if (!(kerneldelegate is Delegate))
		//				throw new ArgumentException("Type argument must be a delegate type.");
		//
		//			// TODO Generate LCG delegate wrapper.
		//			this._kernelWrapperDelegate = kerneldelegate;
		//
		//			_kernelMethod = (kerneldelegate as Delegate).Method;
		//			Utilities.AssertArgument(_kernelMethod.IsStatic, "Kernel method must be static.");
		//		}

		public T Execute
		{
			get { return _kernelWrapperDelegate; }
		}
	}

	public class CudaKernel : IDisposable
	{
		private CudaKernelCompileState _state;

		private List<CudaMethod> _methods;
		private readonly MethodInfo _kernelMethod;
		private CudaContext _context;
		private PtxEmitter _emitter;
		private string _cubin;
		private CudaFunction _function;
		private string _kernelPtxName;

		public CudaKernel(MethodInfo kernelMethod)
		{
			Utilities.AssertArgumentNotNull(kernelMethod, "kernelMethod");

			_kernelMethod = kernelMethod;
			Utilities.AssertArgument(_kernelMethod.IsStatic, "Kernel method must be static.");
		}

		private CudaKernel()
		{
		}

		private void InitializeFromCubin(string cubin)
		{
			Utilities.AssertArgument(!string.IsNullOrEmpty(cubin), "");

			var r = new StringReader(cubin);
			string s;
			_kernelPtxName = null;
			while ((s = r.ReadLine()) != null)
			{
				if (s.Contains("name = "))
				{
					int startIndex = s.IndexOf("name = ");
					_kernelPtxName = s.Substring(startIndex + "name = ".Length).TrimEnd();
					break;
				}
			}
			if (_kernelPtxName == null)
				throw new ArgumentException("Can't find name of first kernel function. Is this a cubin?");

			_cubin = cubin;
			_state = CudaKernelCompileState.PtxCompilationComplete;
		}

		public static CudaKernel<T> Create<T>(MethodInfo method) where T : class
		{
			return new CudaKernel<T>(method);
		}

		public static CudaKernel Create(MethodInfo method)
		{
			return new CudaKernel(method);
		}

		public static CudaKernel Create(Delegate del)
		{
			return new CudaKernel(del.Method);
		}

		public static CudaKernel FromCubin(string cubin)
		{
			var k = new CudaKernel();
			k.InitializeFromCubin(cubin);
			return k;
		}

		internal void PerformProcessing(CudaKernelCompileState targetstate)
		{
			if (targetstate > _state && _state == CudaKernelCompileState.IRConstructionDone - 1)
			{
				_methods = PerformIRConstruction(_kernelMethod);
				_state = CudaKernelCompileState.IRConstructionDone;
			}
			if (targetstate > _state && _state == CudaKernelCompileState.InstructionSelectionDone - 1)
			{
				PerformInstructionSelection(_methods);
				_state = CudaKernelCompileState.InstructionSelectionDone;
			}
			if (targetstate > _state && _state == CudaKernelCompileState.PtxEmissionComplete - 1)
			{
				_emitter = PerformPtxEmission(_methods);
				_state = CudaKernelCompileState.PtxEmissionComplete;
			}
			if (targetstate > _state && _state == CudaKernelCompileState.PtxCompilationComplete - 1)
			{
				_cubin = PerformPtxCompilation(_emitter);
				_state = CudaKernelCompileState.PtxCompilationComplete;
			}
		}

		public void ExecuteUntyped(params object[] arguments)
		{
			EnsurePrepared();

			_function.Launch(arguments);
		}

		private string PerformPtxCompilation(PtxEmitter emitter)
		{
			AssertState(CudaKernelCompileState.PtxCompilationComplete - 1);

			string cubin = new PtxCompiler().CompileToCubin(emitter.GetEmittedPtx());
			return cubin;
		}

		private PtxEmitter PerformPtxEmission(List<CudaMethod> methods)
		{
			AssertState(CudaKernelCompileState.PtxEmissionComplete - 1);

			var emitter = new PtxEmitter();
			foreach (CudaMethod method in methods)
			{
				emitter.Emit(method);
			}

			return emitter;
		}

		private void AssertState(CudaKernelCompileState requiredState)
		{
			if (_state != requiredState)
				throw new InvalidOperationException(string.Format("Operation is invalid for the current state. " +
					"Current state: {0}; required state: {1}.", _state, requiredState));
		}

		private void AssertStateMinimum(CudaKernelCompileState requiredState)
		{
			if (_state < requiredState)
				throw new InvalidOperationException(string.Format("Operation is invalid for the current state. " +
					"Current state: {0}; required state: {1}.", _state, requiredState));
		}

		private void PerformInstructionSelection(List<CudaMethod> methods)
		{
			AssertState(CudaKernelCompileState.InstructionSelectionDone - 1);

			foreach (CudaMethod method in methods)
			{
				method.PerformProcessing(CudaMethodCompileState.InstructionSelectionDone);
			}
		}

		private List<CudaMethod> PerformIRConstruction(MethodInfo kernelMethod)
		{
			AssertState(CudaKernelCompileState.IRConstructionDone - 1);

			var processedMethods = new Dictionary<MethodBase, CudaMethod>();
			var methodWorkList = new Stack<MethodBase>();
			var instructionsNeedingPatching = new List<ListInstruction>();

			// Construct CudaMethods by traversing the call graph.
			methodWorkList.Push(kernelMethod);
			while (methodWorkList.Count != 0)
			{
				MethodBase currentMethod = methodWorkList.Pop();
				if (processedMethods.ContainsKey(currentMethod))
					continue;
				var cm = new CudaMethod(currentMethod);
				cm.PerformProcessing(CudaMethodCompileState.ListContructionDone);
				processedMethods.Add(currentMethod, cm);

				foreach (BasicBlock block in cm.Blocks)
				{
					foreach (ListInstruction inst in block.Instructions)
					{
						if (!(inst.Operand is MethodBase))
							continue;

						CudaMethod calledmethod;
						var operandMethod = (MethodBase)inst.Operand;
						if (processedMethods.TryGetValue(operandMethod, out calledmethod))
						{
							// The encountered MethodBase has been encountered before.
							inst.Operand = calledmethod;
							continue;
						}

						if (!SpecialMethodInfo.IsSpecialMethod(operandMethod))
						{
							// The encountered MethodBase has not been encountered before, so it's pushed onto 
							// a work list, and make a note that the current instruction needs to be patched later.
							// This means that a method can appear on the work list multiple times.
							methodWorkList.Push(operandMethod);
							instructionsNeedingPatching.Add(inst);
						}
					}
				}
			}

			foreach (ListInstruction inst in instructionsNeedingPatching)
			{
				inst.Operand = processedMethods[(MethodBase)inst.Operand];
			}

			return new List<CudaMethod>(processedMethods.Values);
		}

		internal ICollection<CudaMethod> Methods
		{
			get { return _methods; }
		}

		internal MethodBase KernelMethod
		{
			get { return _kernelMethod; }
		}

		public CudaContext Context
		{
			get
			{
				if (_context == null)
					_context = CudaContext.GetCurrentOrNew();

				return _context;
			}
		}

		public void SetBlockSize(int x)
		{
			SetBlockSize(x, 1, 1);
		}

		public void SetBlockSize(int x, int y)
		{
			SetBlockSize(x, y, 1);
		}

		public void SetBlockSize(int x, int y, int z)
		{
			GetFunction().SetBlockSize(x, y, z);
		}

		public void SetGridSize(int x)
		{
			SetGridSize(x, 1);
		}

		public void SetGridSize(int x, int y)
		{
			GetFunction().SetGridSize(x, y);
		}

		private CudaFunction GetFunction()
		{
			EnsurePrepared();
			return _function;
		}

		/// <summary>
		/// Lock'n load.
		/// </summary>
		private void Prepare()
		{
			PerformProcessing(CudaKernelCompileState.Complete);

			// Making sure that a context is present, so CudaModule.LoadData works.
			Context.Equals(null);

			Utilities.Assert(!string.IsNullOrEmpty(_cubin), "No cubin?");

			var module = CudaModule.LoadData(_cubin);
			if (_kernelPtxName == null)
				_kernelPtxName = _methods[0].PtxName;
			_function = module.GetFunction(_kernelPtxName);
		}

		public void EnsurePrepared()
		{
			if (_function != null)
				return;

			Prepare();

			Utilities.AssertNotNull(_function, "_function");
		}

		public void Dispose()
		{
			if (_context != null)
				_context.Dispose();
			_context = null;
		}

		public string GetPtx()
		{
			AssertStateMinimum(CudaKernelCompileState.PtxEmissionComplete);
			return _emitter.GetEmittedPtx();
		}
	}
}