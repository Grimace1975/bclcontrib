using System;
namespace SystemEx
{
    public class JSArray
    {
        protected JSArray() { }

        public object Get(int index) { return Script.Literal("this[{0}]", index); }
        public string JoinA() { return Join(","); }
        public string Join(string separator) { return (string)Script.Literal("this.join({0})", separator); }
        public int Length
        {
            get { return (int)Script.Literal("this.length"); }
            set { Script.Literal("this.length = {0}", value); }
        }
        public void Push(object value) { Script.Literal("this[this.length] = {0}", value); }
        public void Set(int index, object value) { Script.Literal("this[{0}] = {1}", index, value); }
        public object Shift() { return Script.Literal("this.shift()"); }
        public void Unshift(object value) { Script.Literal("this.unshift(value);"); }
    }
}
