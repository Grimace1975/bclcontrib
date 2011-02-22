using System;
using SystemEx.Html;
using System.Html;
using System.Runtime.CompilerServices;
namespace SystemEx.IO
{
    public class FileStream : Stream
    {
        private readonly string _name;
        private readonly bool _isWriteable;
        private bool _isDirty;
        private string _data;
        private long _newDataPosition;
        private StringBuilder _newData;
        private long _position;
        private long _length;

        [AlternateSignature]
        public extern FileStream(string path, FileMode fileMode, FileAccess fileAccess);
        public FileStream(string path, FileMode fileMode, FileAccess fileAccess, FileInfo fileInfo)
        {
            if (fileInfo == null)
                fileInfo = new FileInfo(path);
            _name = fileInfo.GetCanonicalPath();
            if ((fileAccess != FileAccess.Read) && (fileAccess != FileAccess.ReadWrite))
                throw new Exception("IllegalArgumentException: fileAccess");
            _isWriteable = (fileAccess == FileAccess.ReadWrite);
            if (fileInfo.Exists())
            {
                try
                {
                    _data = WindowEx.Atob(LocalStorage.GetItem(_name));
                    _length = _data.Length;
                }
                catch (Exception e) { throw (e.Message.StartsWith("IOException") ? new Exception("FileNotFoundException:" + e) : e); }
            }
            else if (_isWriteable)
            {
                _data = "";
                _isDirty = true;
                try
                {
                    Flush();
                }
                catch (Exception e) { throw (e.Message.StartsWith("IOException") ? new Exception("FileNotFoundException:" + e) : e); }
            }
            else
                throw new Exception("FileNotFoundException:" + _name);
        }

        public long FilePointer
        {
            get { return _position; }
        }

        public void Seek(long position)
        {
            if (position < 0)
                throw new Exception("IllegalArgumentException:");
            _position = (int)position;
        }

        public long Length
        {
            get { return _length; }
            set
            {
                if (_length != value)
                {
                    Consolidate();
                    if (_data.Length > value)
                    {
                        _data = _data.Substring(0, (int)value);
                        _length = (int)value;
                    }
                    else
                        while (_length < value)
                            WriteByte(0);
                }
            }
        }

        public override long Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public override void Close()
        {
            if (_data != null)
            {
                Flush();
                _data = null;
            }
        }

        private void Consolidate()
        {
            if (_newData == null)
                return;
            if (_data.Length < _newDataPosition)
            {
                StringBuilder filler = new StringBuilder();
                while (_data.Length + StringBuilderEx.GetLength(filler) < _newDataPosition)
                    filler.Append('\0');
                _data += filler.ToString();
            }
            long p2 = _newDataPosition + StringBuilderEx.GetLength(_newData);
            _data = _data.Substring(0, (int)_newDataPosition) + _newData.ToString() + (p2 < _data.Length ? _data.Substring((int)p2, _data.Length) : string.Empty);
            _newData = null;
        }

        public override void Flush()
        {
            if (!_isDirty)
                return;
            Consolidate();
            LocalStorage.SetItem(_name, WindowEx.Btoa(_data));
            _isDirty = false;
        }

        public override int ReadByte()
        {
            if (_position >= _length)
                return -1;
            else
            {
                Consolidate();
                return _data.CharAt((int)_position++);
            }
        }

        public override void WriteByte(int b)
        {
            if (!_isWriteable)
                throw new Exception("IOException: not writeable");
            if (_newData == null)
            {
                _newDataPosition = _position;
                _newData = new StringBuilder();
            }
            else if (_newDataPosition + StringBuilderEx.GetLength(_newData) != _position)
            {
                Consolidate();
                _newDataPosition = _position;
                _newData = new StringBuilder();
            }
            _newData.Append((char)(b & 255));
            _position++;
            _length = Math.Max(_position, _length);
            _isDirty = true;
        }
    }
}
