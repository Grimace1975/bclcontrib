using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Interop.Cuda.Native_;
namespace System.Interop.Cuda
{
	public class CudaContext : IDisposable
	{
		private readonly CUcontext _handle;

		private CudaContext(CudaDevice device)
		{
			var rc = DriverUnsafeNativeMethods.cuCtxCreate(out _handle, 0, device.CUdevice);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			Device = device;
		}

		private CudaContext(CUcontext handle, CudaDevice device)
		{
			_handle = handle;
			Device = device;
		}

		public static CudaContext GetCurrent()
		{
			return GetCurrentOrNew(false);
		}

		public static CudaContext GetCurrentOrNew()
		{
			return GetCurrentOrNew(true);
		}

		private static CudaContext GetCurrentOrNew(bool createIfNecessary)
		{
			CudaDevice.EnsureCudaInitialized();

			CUcontextAttachedHandle handle;
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuCtxAttach(out handle, 0);
			if (rc == DriverStatusCode.CUDA_ERROR_INVALID_CONTEXT)
			{
				handle.SetHandleAsInvalid();
				if (createIfNecessary)
				{
					return new CudaContext(CudaDevice.PreferredDevice);
				}
			}

			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			CUdevice devhandle;
			rc = DriverUnsafeNativeMethods.cuCtxGetDevice(out devhandle);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			var dev = new CudaDevice(devhandle);
			var ctx = new CudaContext(handle, dev);

			return ctx;
		}

		public void Dispose()
		{
			_handle.Dispose();
		}

		public CudaDevice Device { get; private set; }

		public void Synchronize()
		{
			var rc = DriverUnsafeNativeMethods.cuCtxSynchronize();
			DriverUnsafeNativeMethods.CheckReturnCode(rc);
		}

		public GlobalMemory<T> AllocateLinear<T>(int count) where T : struct
		{
			int elementSize = Marshal.SizeOf(typeof(T));
			uint bytecount = (uint)count * (uint)elementSize;
			CUdeviceptr dptr;
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuMemAlloc(out dptr, bytecount);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			return new GlobalMemory<T>(dptr, count, elementSize);
		}

#if UNITTEST

		/// <summary>
		/// 
		/// </summary>
		internal IntPtr CudaHandle
		{
			get { return _handle.DangerousGetHandle(); }
		}
#endif // UNITTEST

		#region  Memory Copy

		public void CopyHostToDevice<T>(T[] src, int srcIndex, GlobalMemory<T> dest, int destIndex, int elementCount) where T : struct
		{
			if (srcIndex + elementCount > src.Length || srcIndex < 0 || elementCount < 0)
				throw new ArgumentOutOfRangeException("Bad source.");
			if (destIndex + elementCount > dest.Length || destIndex < 0 || elementCount < 0)
				throw new ArgumentOutOfRangeException("Bad destination.");

			uint bc = (uint)dest.ElementSize * (uint)elementCount;
			var devPtr = new CUdeviceptr(dest.GetDeviceAddress() + dest.ElementSize * destIndex);
			var handle = new GCHandle();
			try
			{
				handle = GCHandle.Alloc(src);
				IntPtr hostPtr = Marshal.UnsafeAddrOfPinnedArrayElement(src, srcIndex);
				DriverStatusCode rc = DriverUnsafeNativeMethods.cuMemcpyHtoD(devPtr, hostPtr, bc);
				DriverUnsafeNativeMethods.CheckReturnCode(rc);
			}
			finally
			{
				if (handle.IsAllocated)
					handle.Free();
			}
		}

		public void CopyDeviceToHost<T>(GlobalMemory<T> src, int srcIndex, T[] dest, int destIndex, int elementCount) where T : struct
		{
			if (srcIndex + elementCount > src.Length || srcIndex < 0 || elementCount < 0)
				throw new ArgumentOutOfRangeException("Bad source.");
			if (destIndex + elementCount > dest.Length || destIndex < 0 || elementCount < 0)
				throw new ArgumentOutOfRangeException("Bad destination.");

			uint bc = (uint)src.ElementSize * (uint)elementCount;
			var devPtr = new CUdeviceptr(src.GetDeviceAddress() + src.ElementSize * destIndex);
			var handle = new GCHandle();
			try
			{
				handle = GCHandle.Alloc(src);
				IntPtr hostPtr = Marshal.UnsafeAddrOfPinnedArrayElement(dest, srcIndex);
				DriverStatusCode rc = DriverUnsafeNativeMethods.cuMemcpyDtoH(hostPtr, devPtr, bc);
				DriverUnsafeNativeMethods.CheckReturnCode(rc);
			}
			finally
			{
				if (handle.IsAllocated)
					handle.Free();
			}
		}

		public T[] CopyDeviceToHost<T>(GlobalMemory<T> src, int srcIndex, int elementCount) where T : struct
		{
			if (elementCount < 0)
				throw new ArgumentOutOfRangeException("elementCount");
			var buf = new T[elementCount];
			CopyDeviceToHost(src, srcIndex, buf, 0, elementCount);
			return buf;
		}

		#endregion
	}
}
