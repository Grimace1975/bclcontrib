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
namespace System.Quality
{
    /// <summary>
    /// TypeMockManager
    /// </summary>
    public static class TypeMockManager
    {
        private static readonly object _lock = new object();
        private static Func<ITypeMocker> _provider;
        private static ITypeMocker _typeMocker;

        public static void SetMockProvider(Func<ITypeMocker> provider)
        {
            _provider = provider;
        }

        public static ITypeMocker Current
        {
            get
            {
                if (_provider == null)
                    throw new InvalidOperationException("TODO");
                if (_typeMocker == null)
                    lock (_lock)
                        if (_typeMocker == null)
                            _typeMocker = _provider();
                return _typeMocker;
            }
        }
    }
}
