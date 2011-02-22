using System.Runtime.CompilerServices;
namespace System
{
    [IgnoreNamespace, Imported]
    public class JSArray
    {
        protected JSArray() { }

        [IntrinsicProperty]
        public object this[int index]
        {
            get { return null; }
            set { }
        }

        //public string JoinA() { return Join(","); }
        public string Join(string separator) { return null; }

        [IntrinsicProperty]
        public int Length
        {
            get { return 0; }
            set { }
        }

        public void Push(object value) { }
        public void Set(int index, object value) { }
        public object Shift() { return null; }
        public void Unshift(object value) { }
    }
}
