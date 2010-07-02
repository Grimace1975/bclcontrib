using System.ComponentModel;
namespace System
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterfaceBase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}