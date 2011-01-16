using System;
namespace SystemEx
{
    public class JSArrayString
    {
        protected JSArrayString() { }

        public string Get(int index) { return (string)Script.Literal("this[{0}]", index); }
        public string JoinA() { return Join(","); }
        public string Join(string separator) { return (string)Script.Literal("this.join({0})", separator); }
        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
            set { Script.Literal("this.length = {0}", value); }
        }
        public void Push(string value) { Script.Literal("this[this.length] = {0}", value); }
        public void Set(int index, string value) { Script.Literal("this[{0}] = {1}", index, value); }
        public string Shift() { return (string)Script.Literal("this.shift()"); }
        public void Unshift(string value) { Script.Literal("this.unshift(value);"); }
    }
}
