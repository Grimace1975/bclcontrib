// from automapper
namespace System.Reflection
{
    public interface IMemberResolver : IValueResolver
    {
        Type MemberType { get; }
    }
}
