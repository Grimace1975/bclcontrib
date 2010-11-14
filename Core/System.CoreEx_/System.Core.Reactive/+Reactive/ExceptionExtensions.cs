#region Foreign-License
// .Net40 Kludge
#endregion
#if !CLR4
using System.Reflection;
using System.Threading;
namespace System
{
    public static class ExceptionExtensions
    {
        private const string ExceptionPrepForRemotingMethodName = "PrepForRemoting";
        private static readonly MethodInfo s_prepForRemotingMethod = typeof(Exception).GetMethod("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo s_internalPreserveStackTraceMethod = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool IsCritical(this Exception exception)
        {
            return ((exception is AccessViolationException) || ((exception is NullReferenceException) || ((exception is StackOverflowException) || ((exception is OutOfMemoryException) || ((exception is ExecutionEngineException) || (exception is ThreadAbortException))))));
        }

        public static Exception PrepareForRethrow(this Exception exception) { return PrepareForRethrow(exception, false); }
        public static Exception PrepareForRethrow(this Exception exception, bool remoting)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");
            if (remoting)
                s_prepForRemotingMethod.Invoke(exception, null);
            else
                s_internalPreserveStackTraceMethod.Invoke(exception, null);
            return exception;
        }
    }
}
#endif