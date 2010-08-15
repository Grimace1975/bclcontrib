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
namespace System.Diagnostics
{
    /// <summary>
    /// Provides event logging ability by wrapping use of the 3rd-party .NET-based NLog logging library.
    /// </summary>
    public static class Logger
    {
#if CoreEvent_NLOG
        private static NLog.Logger s_logger = NLog.LogManager.GetCurrentClassLogger();
#endif

        /// <summary>
        /// Category of events for classifying logging actions.
        /// </summary>
        public enum LogEventType
        {
            /// <summary>
            /// Generic logging action indicating tracking information.
            /// </summary>
            Trace,
            /// <summary>
            /// Logging action involving application debugging information.
            /// </summary>
            Debug,
            /// <summary>
            /// Logging action involving non-specific information of general purpose.
            /// </summary>
            Information,
            /// <summary>
            /// Logging action involving information of concern.
            /// </summary>
            Warning,
            /// <summary>
            /// Logging action involving information regarding a system or application error of a non-critical nature.
            /// </summary>
            Error,
            /// <summary>
            /// Logging action involving information regarding a critical system or application error.
            /// </summary>
            Fatal
        }

        /// <summary>
        /// Logs an event.
        /// </summary>
        /// <param name="logEventType">Type of event to log.</param>
        /// <param name="module">Point of origin of the event in the form of Class::Method</param>
        /// <param name="text">Message to associated with the event.</param>
        /// <param name="args">List of parameters associated with event.</param>
        public static void LogEvent(LogEventType logEventType, string module, string text, params object[] args)
        {
#if CoreEvent_NLOG
            switch (logEventType)
            {
                case LogEventType.Trace:
                    s_logger.Trace(module + "\r\n" + text, parameterArray);
                    break;
                case LogEventType.Debug:
                    s_logger.Debug(module + "\r\n" + text, parameterArray);
                    break;
                case LogEventType.Information:
                    s_logger.Info(module + "\r\n" + text, parameterArray);
                    break;
                case LogEventType.Warning:
                    s_logger.Warn(module + "\r\n" + text, parameterArray);
                    break;
                case LogEventType.Error:
                    s_logger.Error(module + "\r\n" + text, parameterArray);
                    break;
                case LogEventType.Fatal:
                    s_logger.Fatal(module + "\r\n" + text, parameterArray);
                    break;
            }
#endif
        }
        /// <summary>
        /// Logs an exception having been raised.
        /// </summary>
        /// <param name="logEventType">Type of event to log.</param>
        /// <param name="module">Point of origin of the event in the form of Class::Method</param>
        /// <param name="e">The exception being logged.</param>
        public static void LogEvent(LogEventType logEventType, string module, Exception e)
        {
#if CoreEvent_NLOG
            switch (logEventType)
            {
                case LogEventType.Trace:
                    s_logger.TraceException(module, e);
                    break;
                case LogEventType.Debug:
                    s_logger.DebugException(module, e);
                    break;
                case LogEventType.Information:
                    s_logger.InfoException(module, e);
                    break;
                case LogEventType.Warning:
                    s_logger.WarnException(module, e);
                    break;
                case LogEventType.Error:
                    s_logger.ErrorException(module, e);
                    break;
                case LogEventType.Fatal:
                    s_logger.FatalException(module, e);
                    break;
            }
#endif
        }
    }
}
