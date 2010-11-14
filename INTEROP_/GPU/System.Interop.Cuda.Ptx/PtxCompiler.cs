#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
namespace System.Interop.Cuda
{
    public class PtxCompiler
    {
        public static string CompileToCubin(string ptx) { return CompileToCubin(ptx, null, null); }
        public static string CompileToCubin(string ptx, int? optimizationLevel, int? maxRegisterCount)
        {
            string cudaPath = (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\NVIDIA Corporation\Installed Products\NVIDIA CUDA", "InstallDir", null) as string);
            if (cudaPath == null)
                cudaPath = @"C:\NVIDIA\CUDA";
            string ptxasPath = Path.Combine(cudaPath, @"bin\ptxas");
            string arguments = string.Empty;
            if (optimizationLevel != null)
            {
                if ((optimizationLevel >= 0) && (optimizationLevel <= 4))
                    arguments += " -O" + optimizationLevel;
                else
                    throw new ArgumentOutOfRangeException("optimizationLevel");
            }
            if (maxRegisterCount != null)
            {
                if ((maxRegisterCount < 1) || (maxRegisterCount > 200))
                    throw new ArgumentOutOfRangeException("maxRegisterCount");
                arguments += " --maxrregcount " + maxRegisterCount;
            }
            using (var ptxFile = new TemporaryFile())
            using (var cubinFile = new TemporaryFile())
            {
                arguments = string.Format("\"{0}\" {1} -o \"{2}\"", ptxFile.Path, arguments, cubinFile.Path);
                File.WriteAllText(ptxFile.Path, ptx);
                try
                {
                    ShellUtilities.ExecuteCommandAndGetOutput(ptxasPath, arguments);
                }
                catch (ShellExecutionException e)
                {
                    throw new PtxCompilationException("An error occurred while compiling PTX.PTX:\r\n" + ptx, e);
                }
                catch (Win32Exception e)
                {
                    throw new PtxCompilationException("An error occurred while starting PTX compiler.", e);
                }
                return File.ReadAllText(cubinFile.Path);
            }
        }
    }
}
