#region Foreign-License
// .Net40 Kludge
#endregion
#if !CLR4
namespace System
{
	public interface IObservable<T>
	{
		IDisposable Subscribe(IObserver<T> observer);
	}
}
#endif