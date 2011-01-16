using System;
namespace SystemEx
{
    public class JSArrayInteger
    {
        protected JSArrayInteger() { }

        public int Get(int index) { return (int)Script.Literal("this[{0}]", index); }
        public string JoinA() { return Join(","); }
        public string Join(string separator) { return (string)Script.Literal("this.join({0})", separator); }
        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
            set { Script.Literal("this.length = {0}", value); }
        }
        public void Push(int value) { Script.Literal("this[this.length] = {0}", value); }
        public void Set(int index, int value) { Script.Literal("this[{0}] = {1}", index, value); }
        public int Shift() { return (int)Script.Literal("this.shift()"); }
        public void Unshift(int value) { Script.Literal("this.unshift(value);"); }
    }

}
