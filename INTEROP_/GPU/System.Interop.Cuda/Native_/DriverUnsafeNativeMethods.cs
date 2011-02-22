using System;
using System.Runtime.InteropServices;
using System.Text;
namespace System.Interop.Cuda.Native_
{
    /// <summary>
    /// DriverUnsafeNativeMethods
    /// </summary>
	public static class DriverUnsafeNativeMethods
	{
		public static void CheckReturnCode(DriverStatusCode rc)
		{
			if (rc != DriverStatusCode.CUDA_SUCCESS)
				throw new CudaException("A CUDA error occurred: " + rc);
		}

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuInit(uint Flags);

		#region Device management

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceGet(out CUdevice deviceOut, int ordinal);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceGetCount(out int count);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceGetName([MarshalAs(UnmanagedType.LPStr)] StringBuilder name, int namelen, CUdevice dev);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceComputeCapability(out int major, out int minor, CUdevice dev);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceTotalMem(out uint bytes, CUdevice dev);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceGetProperties(out CUdevprop prop, CUdevice dev);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceGetAttribute(out int value, CUdevice_attribute attrib, CUdevice dev);

		#endregion

		#region Context Management

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxCreate(out CUcontext pctx, uint flags, CUdevice dev);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxDestroy(IntPtr ctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxAttach(out CUcontextAttachedHandle pctx, uint flags);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxDetach(IntPtr ctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxPushCurrent(CUcontext ctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxPopCurrent(ref CUcontext pctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxGetDevice(out CUdevice device);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxSynchronize();

		#endregion

		#region Module Management

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuModuleLoad(out CUmodule module, [MarshalAs(UnmanagedType.LPStr)] string fname);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuModuleLoadData(out CUmodule module, [MarshalAs(UnmanagedType.LPStr)] string cubinImage);
		[DllImport("nvcuda")]
		[Obsolete("Not obsolete, but don't know what the pointer type is.")]
		public extern static DriverStatusCode cuModuleLoadFatBinary(ref CUmodule module, IntPtr fatbin);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuModuleUnload(CUmodule hmod);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuModuleGetFunction(out CUfunction hfunc, CUmodule hmod, [MarshalAs(UnmanagedType.LPStr)] string name);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuModuleGetGlobal(out CUdevice dptr, out uint bytes, CUmodule hmod, [MarshalAs(UnmanagedType.LPStr)] string name);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuModuleGetTexRef(out CUtexref pTexRef, CUmodule hmod, [MarshalAs(UnmanagedType.LPStr)] string name);

		#endregion

		#region Memory Management

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuMemGetInfo(out uint free, out uint total);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuMemAlloc(out CUdeviceptr dptr, uint bytesize);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuMemFree(CUdeviceptr dptr);

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuMemcpyHtoD(CUdeviceptr dstDevice, IntPtr src, uint ByteCount);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuMemcpyDtoH(IntPtr dstHost, CUdeviceptr srcDevice, uint ByteCount);

		#endregion

		#region Execution Control

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuParamSetSize(CUfunction hfunc, uint numbytes);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuParamSeti(CUfunction hfunc, int offset, uint value);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuParamSetf(CUfunction hfunc, int offset, float value);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuParamSetv(CUfunction hfunc, int offset, IntPtr ptr, uint numbytes);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuParamSetTexRef(CUfunction hfunc, int texunit, CUtexref hTexRef);

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuLaunch(CUfunction f );
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuLaunchGrid (CUfunction f, int grid_width, int grid_height);
//		[DllImport("nvcuda")]
//		public extern static DriverStatusCode cuLaunchGridAsync( CUfunction f, int grid_width, int grid_height, CUstream hStream );

		#endregion

		#region Function Management

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuFuncSetBlockShape (CUfunction hfunc, int x, int y, int z);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuFuncSetSharedSize (CUfunction hfunc, uint bytes);

		#endregion
	}
}