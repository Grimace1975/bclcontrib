#region License
/*
The MIT License

Copyright (c) 2008 Sky Morey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
namespace System
{
    // Structs containing (and wrapping) a single reference will have reference semantics
    [Serializable]
    public struct WeakReference<T>
        where T : class
    {
        private readonly WeakReference _wrapped;

        public WeakReference(T target) { _wrapped = new WeakReference(target); }
        public WeakReference(T target, bool trackResurrection) { _wrapped = new WeakReference(target, trackResurrection); }

        public bool IsAlive
        {
            get { return _wrapped.IsAlive; }
        }

        public T Target
        {
            get
            {
                // Will throw ClassCastException if class is not expected object, if you wish to prevent this - use "wrapped.Target as T" construct thich will return "null" - valid return value in this rare situation
                // i.e. in case if wrapped WeakReference was modified in some way outside of wrapper
                return (T)_wrapped.Target;
            }
            set { _wrapped.Target = value; }
        }

        //public override bool Equals(object o)
        //{
        //    if (o == this)
        //        return true;
        //    var key = (o as WeakReference<T>);
        //    if (key == null)
        //        return false;
        //    T item = Item;
        //    if (item == null)
        //        return false;
        //    return ((_hashCode == key._hashCode) && (object.Equals(item, key.Item)));
        //}

        //public override int GetHashCode()
        //{
        //    return _wrapped.GetHashCode();
        //}

        public bool TrackResurrection
        {
            get { return _wrapped.TrackResurrection; }
        }

        public static implicit operator T(WeakReference<T> reference)
        {
            return (reference._wrapped != null ? (T)reference._wrapped.Target : null);
        }
    }
}