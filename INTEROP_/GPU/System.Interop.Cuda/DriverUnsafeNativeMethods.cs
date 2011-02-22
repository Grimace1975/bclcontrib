using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
namespace System.Interop.Cuda
{
	/// <summary>
	/// This maps CUresult.
	/// </summary>
	internal enum DriverStatusCode
	{
		CUDA_SUCCESS = 0,
		CUDA_ERROR_INVALID_VALUE = 1,
		CUDA_ERROR_OUT_OF_MEMORY = 2,
		CUDA_ERROR_NOT_INITIALIZED = 3,
		CUDA_ERROR_DEINITIALIZED = 4,

		CUDA_ERROR_NO_DEVICE = 100,
		CUDA_ERROR_INVALID_DEVICE = 101,

		CUDA_ERROR_INVALID_IMAGE = 200,
		CUDA_ERROR_INVALID_CONTEXT = 201,
		CUDA_ERROR_CONTEXT_ALREADY_CURRENT = 202,
		CUDA_ERROR_MAP_FAILED = 205,
		CUDA_ERROR_UNMAP_FAILED = 206,
		CUDA_ERROR_ARRAY_IS_MAPPED = 207,
		CUDA_ERROR_ALREADY_MAPPED = 208,
		CUDA_ERROR_NO_BINARY_FOR_GPU = 209,
		CUDA_ERROR_ALREADY_ACQUIRED = 210,
		CUDA_ERROR_NOT_MAPPED = 211,

		CUDA_ERROR_INVALID_SOURCE = 300,
		CUDA_ERROR_FILE_NOT_FOUND = 301,

		CUDA_ERROR_INVALID_HANDLE = 400,

		CUDA_ERROR_NOT_FOUND = 500,

		CUDA_ERROR_NOT_READY = 600,

		CUDA_ERROR_LAUNCH_FAILED = 700,
		CUDA_ERROR_LAUNCH_OUT_OF_RESOURCES = 701,
		CUDA_ERROR_LAUNCH_TIMEOUT = 702,
		CUDA_ERROR_LAUNCH_INCOMPATIBLE_TEXTURING = 703,

		CUDA_ERROR_UNKNOWN = 999
	}

	/// <summary>
	/// CUdevprop.
	/// </summary>
	internal struct CUdevprop
	{
		public int MaxThreadsPerBlock;
		public int MaxThreadsDimX;
		public int MaxThreadsDimY;
		public int MaxThreadsDimZ;
		public int MaxGridSizeX;
		public int MaxGridSizeY;
		public int MaxGridSizeZ;
		public int SharedMemPerBlock;
		public int TotalConstantMemory;
		public int SimdWidth;
		public int MemPitch;
		public int RegsPerBlock;
		public int ClockRate;
		public int TextureAlign;
	}

	internal enum CUdevice_attribute
	{
		CU_DEVICE_ATTRIBUTE_MAX_THREADS_PER_BLOCK = 1,
		CU_DEVICE_ATTRIBUTE_MAX_BLOCK_DIM_X = 2,
		CU_DEVICE_ATTRIBUTE_MAX_BLOCK_DIM_Y = 3,
		CU_DEVICE_ATTRIBUTE_MAX_BLOCK_DIM_Z = 4,
		CU_DEVICE_ATTRIBUTE_MAX_GRID_DIM_X = 5,
		CU_DEVICE_ATTRIBUTE_MAX_GRID_DIM_Y = 6,
		CU_DEVICE_ATTRIBUTE_MAX_GRID_DIM_Z = 7,
		CU_DEVICE_ATTRIBUTE_MAX_SHARED_MEMORY_PER_BLOCK = 8,
		[Obsolete("Use CU_DEVICE_ATTRIBUTE_MAX_SHARED_MEMORY_PER_BLOCK.")]
		CU_DEVICE_ATTRIBUTE_SHARED_MEMORY_PER_BLOCK = 8,
		CU_DEVICE_ATTRIBUTE_TOTAL_CONSTANT_MEMORY = 9,
		CU_DEVICE_ATTRIBUTE_WARP_SIZE = 10,
		CU_DEVICE_ATTRIBUTE_MAX_PITCH = 11,
		CU_DEVICE_ATTRIBUTE_MAX_REGISTERS_PER_BLOCK = 12,
		[Obsolete("Use CU_DEVICE_ATTRIBUTE_MAX_REGISTERS_PER_BLOCK")]
		CU_DEVICE_ATTRIBUTE_REGISTERS_PER_BLOCK = 12,
		CU_DEVICE_ATTRIBUTE_CLOCK_RATE = 13,
		CU_DEVICE_ATTRIBUTE_TEXTURE_ALIGNMENT = 14,
		CU_DEVICE_ATTRIBUTE_GPU_OVERLAP = 15,
		CU_DEVICE_ATTRIBUTE_MULTIPROCESSOR_COUNT = 16
	}

	/// <summary>
	/// A CUDA device.
	/// </summary>
	struct CUdevice
	{
		public IntPtr IntPtr;
	}

	/// <summary>
	/// It doesn't seem like there is any way or need to clean up this handle, but it is a handle, and we want strong typing...
	/// </summary>
	class CUcontext : SafeHandleZeroOrMinusOneIsInvalid
	{
		public CUcontext(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		protected override bool ReleaseHandle()
		{
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuCtxDestroy(this);
			return rc == DriverStatusCode.CUDA_SUCCESS;
		}
	}

	class CUmodule : SafeHandleZeroOrMinusOneIsInvalid
	{
		public CUmodule(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		protected override bool ReleaseHandle()
		{
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuModuleUnload(this);
			return rc == DriverStatusCode.CUDA_SUCCESS;
		}
	}

	struct CUfunction
	{
		public IntPtr IntPtr;
	}

	struct CUtexref
	{
		public IntPtr IntPtr;
	}

	struct CUdeviceptr
	{
		public int Ptr;
	}

	static class DriverUnsafeNativeMethods
	{

		[DllImport("nvcuda")]
		extern static DriverStatusCode cuInit(uint Flags);

		#region Device management

		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuDeviceGet(CUdevice deviceOut, int ordinal);
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
		public extern static DriverStatusCode cuCtxDestroy(CUcontext ctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxAttach(ref CUcontext pctx, uint flags);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxDetach(CUcontext ctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxPushCurrent(CUcontext ctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxPopCurrent(ref CUcontext pctx);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuCtxGetDevice(ref CUdevice device);
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
		public extern static DriverStatusCode cuLaunch(CUfunction f);
		[DllImport("nvcuda")]
		public extern static DriverStatusCode cuLaunchGrid(CUfunction f, int grid_width, int grid_height);
		//		[DllImport("nvcuda")]
		//		public extern static DriverStatusCode cuLaunchGridAsync( CUfunction f, int grid_width, int grid_height, CUstream hStream );

		#endregion
	}
}
