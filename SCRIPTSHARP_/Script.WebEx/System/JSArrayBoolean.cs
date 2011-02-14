using System.Runtime.CompilerServices;
namespace System
{
    [IgnoreNamespace, Imported]
    public class JSArrayBoolean
    {
        protected JSArrayBoolean() { }

        [IntrinsicProperty]
        public bool this[int index]
        {
            get { return false; }
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

        public void Push(bool value) { }
        public void Set(int index, bool value) { }
        public bool Shift() { return false; }
        public void Unshift(bool value) { }
    }
}
