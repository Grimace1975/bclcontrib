using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace System.Interop.Cuda
{
	class PtxCompiler
	{
		public string CompileToCubin(string ptx)
		{
			return CompileToCubin(ptx, null, null);
		}

		/// <summary>
		/// Compiles the <paramref name="ptx"/> according to the arguments, and returns the resulting cubin file.
		/// </summary>
		/// <param name="ptx"></param>
		/// <param name="optimizationLevel"></param>
		/// <param name="maxRegisterCount"></param>
		/// <returns></returns>
		public string CompileToCubin(string ptx, int? optimizationLevel, int? maxRegisterCount)
		{
			string cudaPath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\NVIDIA Corporation\Installed Products\NVIDIA CUDA", "InstallDir", null);
			if (cudaPath == null)
				cudaPath = @"C:\NVIDIA\CUDA";

			string ptxas = Path.Combine(cudaPath, @"bin\ptxas");
			string arguments = "";

			if (optimizationLevel != null)
			{
				if (optimizationLevel >= 0 && optimizationLevel <= 4)
					arguments += " -O" + optimizationLevel;
				else
					throw new ArgumentOutOfRangeException("optimizationLevel");
			}

			if (maxRegisterCount != null)
			{
				if (maxRegisterCount < 1 || maxRegisterCount > 200)
					throw new ArgumentOutOfRangeException("maxRegisterCount");
				arguments += " --maxrregcount " + maxRegisterCount;
			}

			using (var ptxfile = new TemporaryFile())
			using (var cubinfile = new TemporaryFile())
			{
				arguments = string.Format("\"{0}\" {1} -o \"{2}\"", ptxfile.Path, arguments, cubinfile.Path);
				File.WriteAllText(ptxfile.Path, ptx);

				try
				{
					ShellUtilities.ExecuteCommandAndGetOutput(ptxas, arguments);
				}
				catch (ShellExecutionException e)
				{
					throw new PtxCompilationException("An error occurred while compiling PTX.PTX:\r\n" + ptx, e);
				}
				catch (Win32Exception e)
				{
					throw new PtxCompilationException("An error occurred while starting PTX compiler.", e);
				}

				return File.ReadAllText(cubinfile.Path);
			}
		}
	}
}
