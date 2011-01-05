using System.Interop.Cuda.Native_;
namespace System.Interop.Cuda
{
	internal class CudaModule : IDisposable
	{
		private CUmodule _handle;

		private CudaModule(CUmodule handle)
		{
			_handle = handle;
		}

		public static CudaModule LoadData(string cubin)
		{
			CUmodule handle;
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuModuleLoadData(out handle, cubin);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			return new CudaModule(handle);
		}

		public CudaFunction GetFunction(string name)
		{
			CUfunction func;
			DriverStatusCode rc = DriverUnsafeNativeMethods.cuModuleGetFunction(out func, _handle, name);
			if (rc == DriverStatusCode.CUDA_ERROR_NOT_FOUND)
				throw new ArgumentException("Module does not contain a function named '" + name + "'.");
			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			return new CudaFunction(func);
		}

		public void Dispose()
		{
			if (_handle.IntPtr == IntPtr.Zero)
				return;

			DriverStatusCode rc = DriverUnsafeNativeMethods.cuModuleUnload(_handle);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);
			_handle = default(CUmodule);
		}
	}
}
