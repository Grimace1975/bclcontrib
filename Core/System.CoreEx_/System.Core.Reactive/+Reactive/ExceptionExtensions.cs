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
		private static readonly MethodInfo s_prepForRemoting = typeof(Exception).GetMethod("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance);

		public static bool IsCritical(this Exception exception)
		{
			return ((exception is AccessViolationException) || ((exception is NullReferenceException) || ((exception is StackOverflowException) || ((exception is OutOfMemoryException) || ((exception is ExecutionEngineException) || (exception is ThreadAbortException))))));
		}

		public static Exception PrepareForRethrow(this Exception exception)
		{
			if (exception == null)
				throw new ArgumentNullException("exception");
			s_prepForRemoting.Invoke(exception, new object[0]);
			return exception;
		}
	}
}
#endif