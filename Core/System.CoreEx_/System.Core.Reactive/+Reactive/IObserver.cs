#region Foreign-License
// .Net40 Kludge
#endregion
#if !CLR4
namespace System
{
	public interface IObserver<T>
	{
		void OnCompleted();
		void OnError(Exception exception);
		void OnNext(T value);
	}
}
#endif