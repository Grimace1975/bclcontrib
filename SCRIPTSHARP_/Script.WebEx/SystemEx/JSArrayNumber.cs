using System;
namespace SystemEx
{
    public class JSArrayNumber
    {
        protected JSArrayNumber() { }

        public double Get(int index) { return (double)Script.Literal("this[{0}]", index); }
        public string JoinA() { return Join(","); }
        public string Join(string separator) { return (string)Script.Literal("this.join({0})", separator); }
        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
            set { Script.Literal("this.length = {0}", value); }
        }
        public void Push(double value) { Script.Literal("this[this.length] = {0}", value); }
        public void Set(int index, double value) { Script.Literal("this[{0}] = {1}", index, value); }
        public double Shift() { return (double)Script.Literal("this.shift()"); }
        public void Unshift(double value) { Script.Literal("this.unshift(value);"); }
    }

}
