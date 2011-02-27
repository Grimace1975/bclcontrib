using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace System.Interop.Cuda
{
	enum SpecialMethodCode
	{
		None,
		Shared1DLoad,
		Shared1DStore,
	}

	class SpecialMethodInfo
	{
		static readonly Dictionary<MethodBase, SpecialMethodInfo> dict = new Dictionary<MethodBase, SpecialMethodInfo>
		{
			{typeof(ThreadIndex).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%tid.x"))},
			{typeof(ThreadIndex).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%tid.y"))},
			{typeof(ThreadIndex).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%tid.z"))},
			{typeof(BlockSize).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ntid.x"))},
			{typeof(BlockSize).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ntid.y"))},
			{typeof(BlockSize).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ntid.z"))},
			{typeof(BlockIndex).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ctaid.x"))},
			{typeof(BlockIndex).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ctaid.y"))},
			{typeof(BlockIndex).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%ctaid.z"))},
			{typeof(GridSize).GetProperty("X").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%nctaid.x"))},
			{typeof(GridSize).GetProperty("Y").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%nctaid.y"))},
			{typeof(GridSize).GetProperty("Z").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.SpecialRegister, "%nctaid.z"))},

			{new Action(CudaRuntime.SyncThreads).Method, new SpecialMethodInfo(PtxCode.Bar_Sync)},
			{new Func<int>(CudaRuntime.GetClock).Method, new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I4, VRegType.SpecialRegister, "%clock"))},
			{typeof(CudaRuntime).GetProperty("WarpSize").GetGetMethod(), new SpecialMethodInfo(GlobalVReg.FromSpecialRegister(StackType.I2, VRegType.Immediate, "WARP_SZ"))},

			{typeof(Shared1D<int>).GetProperty("Item").GetGetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DLoad)},
			{typeof(Shared1D<int>).GetProperty("Item").GetSetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DStore)},
			{typeof(Shared1D<uint>).GetProperty("Item").GetGetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DLoad)},
			{typeof(Shared1D<uint>).GetProperty("Item").GetSetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DStore)},
			{typeof(Shared1D<float>).GetProperty("Item").GetGetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DLoad)},
			{typeof(Shared1D<float>).GetProperty("Item").GetSetMethod(), new SpecialMethodInfo(SpecialMethodCode.Shared1DStore)},

		};

		public bool IsSinglePtxCode { get; private set; }
		public bool IsSpecialMethodCode { get; private set; }
		public bool IsGlobalVReg { get; private set; }

		public PtxCode PtxCode { get; private set; }
		public GlobalVReg HardcodedGlobalVReg { get; private set; }
		public SpecialMethodCode SpecialMethodCode { get; private set; }

		public SpecialMethodInfo(PtxCode ptxCode)
		{
			IsSinglePtxCode = true;
			PtxCode = ptxCode;
		}

		internal SpecialMethodInfo(GlobalVReg globalVReg)
		{
			IsGlobalVReg = true;
			HardcodedGlobalVReg = globalVReg;
		}

		public SpecialMethodInfo(SpecialMethodCode specialMethodCode)
		{
			IsSpecialMethodCode = true;
			SpecialMethodCode = specialMethodCode;
		}

		public static bool TryGetMethodInfo(MethodBase method, out SpecialMethodInfo specialMethodInfo)
		{
			return dict.TryGetValue(method, out specialMethodInfo);
		}

		public static bool IsSpecialMethod(MethodBase method)
		{
			return dict.ContainsKey(method);
		}
	}
}
