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
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace System.Interop.Cuda
{
	static class ShellUtilities
	{
		internal static string ExecuteShellScript(string scriptText)
		{
			string tempFileName = null;
			try
			{
				tempFileName = Path.GetTempFileName();
				File.WriteAllText(tempFileName, scriptText.Replace("\r\n", "\n"));
				return ExecuteCommandAndGetOutput("sh", tempFileName);
			}
			finally
			{
				if ((tempFileName != null) && (File.Exists(tempFileName)))
					File.Delete(tempFileName);
			}
		}

		internal static string ExecuteCommandAndGetOutput(string program, string arguments)
		{
			using (Process p = new Process())
			{
				p.StartInfo.FileName = program;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.UseShellExecute = false;
				if (!string.IsNullOrEmpty(arguments))
					p.StartInfo.Arguments = arguments;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.RedirectStandardError = true;
				p.Start();
				var b = new StringBuilder();
				while (!p.HasExited)
				{
					b.AppendLine(p.StandardOutput.ReadToEnd());
					if (p.StandardError.Peek() != -1)
					{
						string text = p.StandardError.ReadToEnd();
						throw new ShellExecutionException(text);
					}
				}
				if (p.ExitCode != 0)
					throw new ShellExecutionException(b.ToString());
				return b.ToString();
			}
		}
	}
}
