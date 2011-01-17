using System.Runtime.CompilerServices;
namespace System
{
    [IgnoreNamespace, Imported]
    public class JSArrayInteger
    {
        protected JSArrayInteger() { }

        [IntrinsicProperty]
        public int this[int index]
        {
            get { return 0; }
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

        public void Push(int value) { }
        public void Set(int index, int value) { }
        public int Shift() { return 0; }
        public void Unshift(int value) { }
    }
}
