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
namespace System.Patterns.Schema
{
    /// <summary>
    /// UriContext
    /// </summary>
    public class _UriContext : UriContextBase, IUriPartScanner
    {
        private string _path;
        private int _pathLength;
        private int _pathIndex;
        private bool _hasOverflowed;

        public _UriContext(UriSchemaBase schema, Uri uri)
            : base(schema, uri)
        {
            AllowVirtualize = true;
        }

        public override bool HasOverflowed
        {
            get { return _hasOverflowed; }
            protected set
            {
                _hasOverflowed = value;
                if (_hasOverflowed)
                {
                    var onOverflow = ((IUriPartScanner)this).OnOverflow;
                    if (onOverflow != null)
                        onOverflow(this);
                }
            }
        }

        public override string Path
        {
            get { return _path.Substring(_pathIndex); }
            protected set
            {
                _path = value;
                _pathLength = _path.Length;
            }
        }

        public override Uri Uri { get; protected set; }

        Action<IUriPartScanner> IUriPartScanner.OnOverflow { get; set; }

        #region State
        public bool AllowVirtualize { get; set; }
        public int ApplicationType { get; set; }
        public int UriType { get; set; }
        #endregion

        #region IUriPartScanner
        void IUriPartScanner.IncreasePath(int value)
        {
            _pathIndex += value;
            if (_pathIndex >= _pathLength)
                HasOverflowed = true;
        }

        string IUriPartScanner.NormalizedPath
        {
            get { return (!_hasOverflowed ? _path.Substring(_pathIndex).EnsureEndsWith("/") : _path + new string('/', (_pathIndex - _pathLength + 1))); }
        }

        void IUriPartScanner.OnComplete()
        {
            Uri = new Uri(Path, UriKind.Relative);
        }
        #endregion
    }
}
