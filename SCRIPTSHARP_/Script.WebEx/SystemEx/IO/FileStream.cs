using System;
using SystemEx.Html;
using System.Html;
namespace SystemEx.IO
{
    public class FileStream : Stream
    {
        private readonly string _name;
        private readonly bool _isWriteable;
        private bool _isDirty;
        private string _data;
        private int _newDataPosition;
        private StringBuilder _newData;
        private int _position;
        private int _length;

        public FileStream(FileInfo fileInfo, FileMode fileMode, FileAccess fileAccess)
        {
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

        public long Poition
        {
            get { return _position; }
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
            int p2 = _newDataPosition + StringBuilderEx.GetLength(_newData);
            _data = _data.Substring(0, _newDataPosition) + _newData.ToString() + (p2 < _data.Length ? _data.Substring(p2, _data.Length) : "");
            _newData = null;
        }

        void Flush()
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
                return _data.CharAt(_position++);
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
