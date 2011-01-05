// from automapper
namespace System.Reflection
{
    public interface IMemberGetter : IMemberResolver, IValueResolver, ICustomAttributeProvider
    {
        object GetValue(object source);
        MemberInfo MemberInfo { get; }
        string Name { get; }
    }
}
