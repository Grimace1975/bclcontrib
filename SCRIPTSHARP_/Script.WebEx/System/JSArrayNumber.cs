using System.Runtime.CompilerServices;
namespace System
{
    [IgnoreNamespace, Imported]
    public class JSArrayNumber
    {
        protected JSArrayNumber() { }

        [IntrinsicProperty]
        public double this[int index]
        {
            get { return 0F; }
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

        public void Push(double value) { }
        public void Set(int index, double value) { }
        public double Shift() { return 0F; }
        public void Unshift(double value) { }
    }
}
