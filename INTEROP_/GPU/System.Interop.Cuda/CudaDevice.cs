using System.Collections.Generic;
using System.Text;
using System.Interop.Cuda.Native_;

namespace System.Interop.Cuda
{
	/// <summary>
	/// Represents a CUDA device. Does not need disposal.
	/// </summary>
	public class CudaDevice
	{
		private readonly CUdevice _cudevice;
		internal CUdevice CUdevice
		{
			get { return _cudevice; }
		}

		public Version ComputeCapability { get; private set; }
		public string Name { get; private set; }
		[CLSCompliant(false)]
		public uint TotalMemory { get; private set; }

		internal CudaDevice(int deviceOrdinal)
		{
			EnsureCudaInitialized();

			var rc = DriverUnsafeNativeMethods.cuDeviceGet(out _cudevice, deviceOrdinal);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);

			Initalize();
		}

		internal CudaDevice(CUdevice devicehandle)
		{
			EnsureCudaInitialized();

			_cudevice = devicehandle;

			Initalize();
		}

		private static bool? s_hasCuda;

		static internal void EnsureCudaInitialized()
		{
			if (s_hasCuda == null)
				s_hasCuda = DetectCudaDll();

			if (!s_hasCuda.Value)
				throw new CudaException("CUDA is not available.");
		}

		private void Initalize()
		{
			DriverStatusCode rc;

			int major, minor;
			rc = DriverUnsafeNativeMethods.cuDeviceComputeCapability(out major, out minor, _cudevice);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);
			ComputeCapability = new Version(major, minor);

			var sbname = new StringBuilder(200);
			rc = DriverUnsafeNativeMethods.cuDeviceGetName(sbname, sbname.Capacity, _cudevice);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);
			Name = sbname.ToString();

			uint totalmem;
			rc = DriverUnsafeNativeMethods.cuDeviceTotalMem(out totalmem, _cudevice);
			DriverUnsafeNativeMethods.CheckReturnCode(rc);
			TotalMemory = totalmem;
		}

		public static CudaDevice[] Devices
		{
			get
			{
				EnsureCudaInitialized();

				int devcount;
				DriverUnsafeNativeMethods.cuDeviceGetCount(out devcount);
				var arr = new CudaDevice[devcount];
				for (int i = 0; i < devcount; i++)
				{
					arr[i] = new CudaDevice(i);
				}

				return arr;
			}
		}

		public static CudaDevice PreferredDevice
		{
			get
			{
				var arr = Devices;
				if (arr.Length == 0)
					throw new NoSuchDeviceException();
				return arr[0];
			}
		}

		public static bool HasCudaDevice
		{
			get
			{
				if (s_hasCuda == null)
					s_hasCuda = DetectCudaDll();
				return s_hasCuda.Value;
			}
		}

		private static bool DetectCudaDll()
		{
			try
			{
				var rc = DriverUnsafeNativeMethods.cuInit(0);
				DriverUnsafeNativeMethods.CheckReturnCode(rc);
				return true;
			}
			catch (DllNotFoundException)
			{
				return false;
			}
		}
	}
}
