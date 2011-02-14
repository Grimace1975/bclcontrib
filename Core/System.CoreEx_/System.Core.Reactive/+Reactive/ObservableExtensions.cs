#region Foreign-License
// .Net40 Kludge
#endregion
#if !CLR4
using System.Collections;
namespace System
{
	public static class ObservableExtensions
	{
		public static IDisposable Subscribe<TSource>(this IObservable<TSource> source)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			return source.Subscribe<TSource>(delegate(TSource _) { }, delegate(Exception exception) { throw exception.PrepareForRethrow(); }, delegate { });
		}

		public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (onNext == null)
				throw new ArgumentNullException("onNext");
			return source.Subscribe<TSource>(onNext, delegate(Exception exception) { throw exception.PrepareForRethrow(); }, delegate { });
		}

		public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError)
		{
			if (onNext == null)
				throw new ArgumentNullException("onNext");
			if (onError == null)
				throw new ArgumentNullException("onError");
			return source.Subscribe<TSource>(onNext, onError, delegate { });
		}

		public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext, Action onCompleted)
		{
			if (onNext == null)
				throw new ArgumentNullException("onNext");
			if (onCompleted == null)
				throw new ArgumentNullException("onCompleted");
			return source.Subscribe<TSource>(onNext, delegate(Exception exception) { throw exception.PrepareForRethrow(); }, onCompleted);
		}

		public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext, Action<Exception> onError, Action onCompleted)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (onNext == null)
				throw new ArgumentNullException("onNext");
			if (onError == null)
				throw new ArgumentNullException("onError");
			if (onCompleted == null)
				throw new ArgumentNullException("onCompleted");
			return source.Subscribe(new AnonymousObserver<TSource>(onNext, onError, onCompleted));
		}
	}
}
#endif