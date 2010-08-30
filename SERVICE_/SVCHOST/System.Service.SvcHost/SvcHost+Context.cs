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
using System.IO;
using DiagnosticsTrace = System.Diagnostics.Trace;
using System.Diagnostics;
namespace System.Service
{
    public partial class SvcHost
    {
        public class Context
        {
            /// <summary>
            /// Gets or sets the application id.
            /// </summary>
            /// <value>The application id.</value>
            public string ApplicationId { get; set; }

            /// <summary>
            /// Gets or sets the base directory.
            /// </summary>
            /// <value>The base directory.</value>
            public string BaseDirectory { get; set; }

            /// <summary>
            /// Gets or sets the config file path.
            /// </summary>
            /// <value>The config file path.</value>
            public string ConfigFilePath { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is debug.
            /// </summary>
            /// <value><c>true</c> if this instance is debug; otherwise, <c>false</c>.</value>
            public int Debug { get; set; }

            /// <summary>
            /// Gets or sets the private bin path.
            /// </summary>
            /// <value>The private bin path.</value>
            public string PrivateBinPath { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is trace.
            /// </summary>
            /// <value><c>true</c> if this instance is trace; otherwise, <c>false</c>.</value>
            public bool Trace { get; set; }

            /// <summary>
            /// Ensures the ends with.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="suffix">The suffix.</param>
            /// <returns></returns>
            public static string EnsureEndsWith(string text, string suffix)
            {
                return ((!string.IsNullOrEmpty(text)) && (!text.EndsWith(suffix)) ? text + suffix : text);
            }

            /// <summary>
            /// Parses the arguments.
            /// </summary>
            /// <param name="args">The args.</param>
            /// <param name="context">The context.</param>
            public static void ParseArguments(string[] args, out Context context)
            {
                string applicationId = null;
                string baseDirectory = null;
                string configFilePath = null;
                int debug = 0;
                string privateBinPath = null;
                bool trace = false;
                //
                int argIndex = 0;
                DiagnosticsTrace.WriteLine("Raw command-line");
                while (argIndex < args.Length)
                {
                    switch (args[argIndex++].ToLowerInvariant())
                    {
                        case "-applicationid":
                        case "-a":
                            applicationId = args[argIndex++];
                            DiagnosticsTrace.WriteLine(string.Format("    -applicationId: '{0}'", applicationId));
                            break;
                        case "-basedirectory":
                        case "-b":
                            baseDirectory = EnsureEndsWith(args[argIndex++], "\\");
                            DiagnosticsTrace.WriteLine(string.Format("    -baseDirectory: '{0}'", baseDirectory));
                            break;
                        case "-configfilepath":
                        case "-c":
                            configFilePath = args[argIndex++];
                            DiagnosticsTrace.WriteLine(string.Format("    -configFilePath: '{0}'", configFilePath));
                            break;
                        case "-debug":
                        case "-d":
                            debug = 1;
                            DiagnosticsTrace.WriteLine(string.Format("    -debug: '{0}'", debug));
                            break;
                        case "-debug2":
                            debug = 2;
                            DiagnosticsTrace.WriteLine(string.Format("    -debug: '{0}'", debug));
                            break;
                        case "-privatebinpath":
                        case "-p":
                            privateBinPath = args[argIndex++];
                            DiagnosticsTrace.WriteLine(string.Format("    -privateBinPath: '{0}'", privateBinPath));
                            break;
                        case "-trace":
                        case "-t":
                            trace = true;
                            DiagnosticsTrace.WriteLine(string.Format("    -trace: '{0}'", true));
                            DiagnosticsTrace.Listeners.Add(new TextWriterTraceListener(Console.Out));
                            break;
                    }
                }
                //
                if ((!string.IsNullOrEmpty(configFilePath)) && (string.IsNullOrEmpty(baseDirectory)))
                    baseDirectory = EnsureEndsWith(Path.GetFullPath(Path.GetDirectoryName(configFilePath)), "\\");
                else if ((string.IsNullOrEmpty(configFilePath)) && (!string.IsNullOrEmpty(baseDirectory)))
                    configFilePath = Path.GetFullPath(baseDirectory + "OperationQueueService.config");
                if ((string.IsNullOrEmpty(privateBinPath)) && (Directory.Exists(baseDirectory + "Bin")))
                    privateBinPath = baseDirectory + "bin";
                //
                DiagnosticsTrace.WriteLine(string.Format(@"Effective command-line:
-applicationId: '{0}'
-baseDirectory: '{1}'
-configFilePath: '{2}'
-debug: '{3}'
-privateBinPath: '{4}'
-trace: '{5}'", applicationId, baseDirectory, configFilePath, debug, privateBinPath, trace));
                context = new Context
                {
                    ApplicationId = applicationId,
                    BaseDirectory = baseDirectory,
                    ConfigFilePath = configFilePath,
                    Debug = debug,
                    PrivateBinPath = privateBinPath,
                    Trace = trace,
                };
            }

            /// <summary>
            /// Determines whether [is valid args] [the specified context].
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>
            /// 	<c>true</c> if [is valid args] [the specified context]; otherwise, <c>false</c>.
            /// </returns>
            public bool EnsureParentArguments()
            {
                if (!Directory.Exists(BaseDirectory))
                {
                    DiagnosticsTrace.WriteLine(string.Format("Invalid base directory '{0}'.", BaseDirectory));
                    BaseDirectory = null;
                }
                if (!File.Exists(ConfigFilePath))
                {
                    DiagnosticsTrace.WriteLine(string.Format("Invalid config filename '{0}'.", ConfigFilePath));
                    ConfigFilePath = null;
                }
                return ((!string.IsNullOrEmpty(ApplicationId)) && (!string.IsNullOrEmpty(ConfigFilePath)) && (!string.IsNullOrEmpty(BaseDirectory)));
            }

            /// <summary>
            /// Ensures the child arguments.
            /// </summary>
            /// <returns></returns>
            public bool EnsureChildArguments()
            {
                return true;
            }
        }
    }
}