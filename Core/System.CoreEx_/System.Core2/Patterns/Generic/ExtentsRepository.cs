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
using System.Collections.Generic;
using System.Reflection;
namespace System.Patterns.Generic
{
    /// <summary>
    /// IExtentsRepository
    /// </summary>
    public interface IExtentsRepository
    {
        bool HasExtents { get; }
        bool HasExtent<T>();
        bool HasExtent(Type type);
        void Set<T>(T value);
        void SetMany<T>(IEnumerable<T> value);
        void Set(Type type, object value);
        void Clear<T>();
        void Clear(Type type);
        T Get<T>();
        IEnumerable<T> GetMany<T>();
        object Get(Type type);
        bool TryGetExtent<T>(out T extent);
        void AddRange(IEnumerable<object> extents);
    }

    /// <summary>
    /// ExtentsRepository
    /// </summary>
    /// <typeparam name="TShard">The type of the shard.</typeparam>
    public class ExtentsRepository : IExtentsRepository
    {
        private static readonly MethodInfo s_hasExtentMethodInfo = typeof(ExtentsRepository).GetGenericMethod("HasExtent");
        private static readonly MethodInfo s_setMethodInfo = typeof(ExtentsRepository).GetGenericMethod("Set");
        private static readonly MethodInfo s_clearMethodInfo = typeof(ExtentsRepository).GetGenericMethod("Clear");
        private static readonly MethodInfo s_getMethodInfo = typeof(ExtentsRepository).GetGenericMethod("Get");
        private Dictionary<Type, object> _extents;

        public bool HasExtents
        {
            get { return ((_extents != null) && (_extents.Count > 0)); }
        }

        public bool HasExtent<T>()
        {
            return (_extents != null) && (_extents.ContainsKey(typeof(T)));
        }
        public bool HasExtent(Type type) { return (bool)s_hasExtentMethodInfo.MakeGenericMethod(type).Invoke(this, null); }

        public void Set<T>(T value)
        {
            if (_extents == null)
                _extents = new Dictionary<Type, object>();
            _extents[typeof(T)] = value;
        }
        public void SetMany<T>(IEnumerable<T> value)
        {
            if (_extents == null)
                _extents = new Dictionary<Type, object>();
            _extents[typeof(IEnumerable<T>)] = value;
        }
        public void Set(Type type, object value) { s_setMethodInfo.MakeGenericMethod(type).Invoke(this, new[] { value }); }

        public void Clear<T>()
        {
            if (_extents != null)
                _extents.Remove(typeof(T));
        }
        public void Clear(Type type) { s_clearMethodInfo.MakeGenericMethod(type).Invoke(this, null); }

        public T Get<T>()
        {
            object value;
            return ((_extents == null) || (!_extents.TryGetValue(typeof(T), out value)) ? default(T) : (T)value);
        }
        public IEnumerable<T> GetMany<T>()
        {
            object value;
            return ((_extents == null) || (!_extents.TryGetValue(typeof(IEnumerable<T>), out value)) ? default(IEnumerable<T>) : (IEnumerable<T>)value);
        }
        public object Get(Type type) { return s_hasExtentMethodInfo.MakeGenericMethod(type).Invoke(this, null); }

        public bool TryGetExtent<T>(out T extent)
        {
            object value;
            if ((_extents == null) || (!_extents.TryGetValue(typeof(T), out value)))
            {
                extent = default(T);
                return false;
            }
            extent = (T)value;
            return true;
        }

        public void AddRange(IEnumerable<object> extents)
        {
            if (extents == null)
                throw new ArgumentNullException("extents");
            foreach (var extent in extents)
                Set(extent.GetType(), extent);
        }
    }
}
