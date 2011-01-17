using System.Runtime.CompilerServices;
namespace System
{
    [IgnoreNamespace, Imported]
    public class JSArrayString
    {
        protected JSArrayString() { }

        [IntrinsicProperty]
        public string this[int index]
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

        public void Push(string value) { }
        public void Set(int index, string value) { }
        public string Shift() { return null; }
        public void Unshift(string value) { }
    }
}
