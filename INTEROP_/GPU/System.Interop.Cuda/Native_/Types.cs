using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
namespace System.Interop.Cuda.Native_
{
	/// <summary>
    /// CUdevprop
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
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

	/// <summary>
	/// A CUDA device.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct CUdevice
	{
		public IntPtr IntPtr;
	}

	/// <summary>
	/// For handles retrieved with <see cref="DriverUnsafeNativeMethods.cuCtxCreate"/>.
	/// </summary>
	class CUcontext : SafeHandleZeroOrMinusOneIsInvalid
	{
		public CUcontext()
			: base(true) { }

		protected override void Dispose(bool disposing)
		{
			// Can't clear it up from the finalizer thread anyway. 
			// Should consider making this a non-safehandle, since IDisposable is all we really have for CUDA.
			if (!disposing)
			{
				Debug.WriteLine("Cannot clean up CUDA context from finalizer thread.");
				return;
			}
			base.Dispose(disposing);
		}

		protected override bool ReleaseHandle()
		{
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuCtxDestroy(handle);
			bool success = rc == DriverStatusCode.CUDA_SUCCESS;
			if (!success)
				Debug.WriteLine("Could not release created CUDA context: " + rc);
			return success;
		}
	}

	/// <summary>
	/// For handles retrieved with <see cref="DriverUnsafeNativeMethods.cuCtxAttach"/>.
	/// </summary>
	class CUcontextAttachedHandle : CUcontext
	{
		protected override bool ReleaseHandle()
		{
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuCtxDetach(handle);

			bool success = rc == DriverStatusCode.CUDA_SUCCESS;
			if (!success)
				Debug.WriteLine("Could not release attached CUDA context: " + rc);
			return success;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	struct CUmodule
	{
		public IntPtr IntPtr;
	}

	/// <summary>
	/// A reference to a pointer within a loaded module.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct CUfunction
	{
		public IntPtr IntPtr;
	}

	/// <summary>
	/// A reference to a texture within a loaded module.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct CUtexref
	{
		public IntPtr IntPtr;
	}

	/// <summary>
	/// A pointer to linear device memory.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct CUdeviceptr
	{
		public CUdeviceptr(int ptr)
		{
			Ptr = ptr;
		}

		public int Ptr;
	}
}