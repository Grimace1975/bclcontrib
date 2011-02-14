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
using System.Collections.Generic;
using System.Threading;
using System.Configuration;
namespace System.Service
{
    /// <summary>
    /// SvcHost
    /// </summary>
    public partial class SvcHost
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin"); Trace.WriteLine("::Begin");
            Context context;
            Context.ParseArguments(args, out context);
            Console.Title = "SvcHost - " + context.ApplicationId;
            var appDomain = AppDomain.CurrentDomain;
            if (appDomain.FriendlyName != "SvcHost")
            {
                // parent
                Console.WriteLine("Parent-Begin"); Trace.WriteLine("Parent::Begin");
                if (context.EnsureParentArguments())
                {
                    Trace.WriteLine("Parent AppDomain::Determine if need to attach debugger");
                    if (context.Debug == 2)
                    {
                        AttachDebugger();
                        Debugger.Break();
                    }
                    RunSingleParentInstance(context);
                }
                Console.WriteLine("Parent-End"); Trace.WriteLine("Parent::End");
            }
            else
            {
                // child
                Console.WriteLine("Child-Begin"); Trace.WriteLine("Child::Begin");
                if (context.EnsureChildArguments())
                {
                    Trace.WriteLine(string.Format("    BaseDirectory: '{0}'", appDomain.BaseDirectory));
                    Trace.WriteLine(string.Format("    ConfigurationFile: '{0}'", appDomain.SetupInformation.ConfigurationFile));
                    if (context.Debug == 1)
                        AttachDebugger();
                    RunChildInstance(context);
                }
                Console.WriteLine("Child-End"); Trace.WriteLine("Child::End");
            }
            Console.WriteLine("End"); Trace.WriteLine("::End");
        }

        /// <summary>
        /// Executes the child app domain.
        /// </summary>
        /// <param name="applicationFullFilename">The application full filename.</param>
        /// <param name="context">The context.</param>
        static void ExecuteChildAppDomain(Context context)
        {
            Trace.WriteLine("ExecuteChildAppDomain::Begin");
            // setup new application domain
            var childAppDomainSetup = new AppDomainSetup();
            childAppDomainSetup.ApplicationBase = context.BaseDirectory;
            if (!string.IsNullOrEmpty(context.PrivateBinPath))
                childAppDomainSetup.PrivateBinPath = context.PrivateBinPath;
            childAppDomainSetup.ConfigurationFile = context.ConfigFilePath;
            // create the new application domain using current evidence and setup information            
            var evidence = AppDomain.CurrentDomain.Evidence;
            var childAppDomain = AppDomain.CreateDomain("ChildHost", evidence, childAppDomainSetup);
            try
            {
                Trace.WriteLine("ExecuteChildAppDomain::Executing child assembly");
                // execute with args
                var args = new List<string>(10);
                args.Add("-applicationId");
                args.Add(context.ApplicationId);
                if (context.Debug == 1)
                    args.Add("-debug");
                if (context.Trace)
                    args.Add("-trace");
                string applicationFullFilename = Process.GetCurrentProcess().MainModule.FileName;
                childAppDomain.ExecuteAssembly(applicationFullFilename, evidence, (args.Count == 0 ? null : args.ToArray()));
                Trace.WriteLine("ExecuteChildAppDomain::Child assembly completed");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ExecuteChildAppDomain::Child assembly error");
                Trace.TraceError(ex.Message);
                Trace.TraceError(ex.StackTrace);
                throw;
            }
            Trace.WriteLine("ExecuteChildAppDomain::End");
        }

        /// <summary>
        /// Runs the single instance.
        /// </summary>
        /// <param name="context">The context.</param>
        static void RunSingleParentInstance(Context context)
        {
            Trace.WriteLine("RunSingleInstance::Begin");
            // detect first instance
            bool createdNew;
            Trace.WriteLine("RunSingleInstance::Creating mutex");
            using (var singleInstanceMutex = new Mutex(false, "Global\\SvcHost::" + context.ApplicationId, out createdNew))
            {
                if (!createdNew)
                {
                    Trace.WriteLine("RunSingleInstance::Quiting due to Duplicate Instance");
                    return;
                }
                Trace.WriteLine("RunSingleInstance::Mutex created");
                ExecuteChildAppDomain(context);
            }
            Trace.WriteLine("RunSingleInstance::End");
        }

        /// <summary>
        /// Windows application host.
        /// </summary>
        /// <param name="context">The context.</param>
        static void RunChildInstance(Context context)
        {
            Trace.WriteLine("WindowsApplicationHost::Begin");
            var configSection = (SvcHostConfigurationSection)ConfigurationManager.GetSection("svcHost");
            TimeSpan threadTimeout = configSection.Timeout;
            bool runInBackground = configSection.RunInBackground;
            string executeAssemblyFile = configSection.ExecuteAssemblyFile;
            Trace.WriteLine(string.Format("runInBackground: '{0}', timeout: '{1}'", runInBackground, threadTimeout));
            var thread = new Thread(new ThreadStart(delegate
            {
                if (context.Debug == 1)
                    Debugger.Break();
                AppDomain.CurrentDomain.ExecuteAssembly(executeAssemblyFile);
            }))
            {
                Name = "SvcHost::" + context.ApplicationId,
                Priority = ThreadPriority.BelowNormal,
                IsBackground = runInBackground,
            };
            thread.Start();
            thread.Join(threadTimeout);
            // do not know if timeout or success, could log event here
            Trace.WriteLine("WindowsApplicationHost::End");
        }

        /// <summary>
        /// Attaches the debugger.
        /// </summary>
        private static void AttachDebugger()
        {
            Trace.WriteLine("AttachDebugger::Begin");
            Console.WriteLine();
            Console.WriteLine("Please attach your debugger to PID: {0}", Process.GetCurrentProcess().Id);
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Trace.WriteLine("AttachDebugger::End");
        }
    }
}