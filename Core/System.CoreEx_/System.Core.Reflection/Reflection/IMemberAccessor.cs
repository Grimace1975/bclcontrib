// from automapper
namespace System.Reflection
{
    public interface IMemberAccessor : IMemberGetter, IMemberResolver, IValueResolver, ICustomAttributeProvider
    {
        void SetValue(object destination, object value);
    }
}



