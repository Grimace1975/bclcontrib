using System;
namespace SystemEx
{
    public class JSArrayBoolean
    {
        protected JSArrayBoolean() { }

        public bool Get(int index) { return (bool)Script.Literal("this[{0}]", index); }
        public string JoinA() { return Join(","); }
        public string Join(string separator) { return (string)Script.Literal("this.join({0})", separator); }
        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
            set { Script.Literal("this.length = {0}", value); }
        }
        public void Push(bool value) { Script.Literal("this[this.length] = {0}", value); }
        public void Set(int index, bool value) { Script.Literal("this[{0}] = {1}", index, value); }
        public bool Shift() { return (bool)Script.Literal("this.shift()"); }
        public void Unshift(bool value) { Script.Literal("this.unshift(value);"); }
    }

}
