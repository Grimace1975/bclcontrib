using System;
using SystemEx.Html;
using System.Runtime.CompilerServices;
using System.Collections;
namespace SystemEx.IO
{
    public class FileInfo
    {
        public const char SeparatorChar = '/';
        public const string Separator = "//";
        public const char PathSeparatorChar = ':';
        public const string PathSeparator = ":";
        public static FileInfo Root = new FileInfo("");

        private FileInfo _parent;
        private string _name;
        //private bool _absolute;

        [AlternateSignature]
        public extern FileInfo(string fileName);
        public FileInfo(string fileName, FileInfo parent)
        {
            while (fileName.EndsWith(Separator) && (fileName.Length > 0))
                fileName = fileName.Substring(0, fileName.Length - 1);
            int cut = fileName.LastIndexOf(SeparatorChar);
            if (cut == -1)
                _name = fileName;
            else if (cut == 0)
            {
                _name = fileName.Substring(cut, fileName.Length);
                _parent = (_name == "" ? null : Root);
            }
            else
            {
                _name = fileName.Substring(cut + 1, fileName.Length);
                _parent = new FileInfo(fileName.Substring(0, cut));
            }
        }

        public string Name
        {
            get { return _name; }
        }

        private string Parent
        {
            get { return (_parent == null ? "" : _parent.Path); }
        }

        private FileInfo ParentFileInfo
        {
            get { return _parent; }
        }

        public string Path
        {
            get { return (_parent == null ? _name : _parent.Path + SeparatorChar + _name); }
        }

        private bool IsRoot()
        {
            return ((_name == "") && (_parent == null));
        }

        internal bool IsAbsolute()
        {
            if (IsRoot())
                return true;
            if (_parent == null)
                return false;
            return _parent.IsAbsolute();
        }

        internal string GetAbsolutePath()
        {
            string path = GetAbsoluteFile().Path;
            return (path.Length == 0 ? "/" : path);
        }

        internal FileInfo GetAbsoluteFile()
        {
            if (IsAbsolute())
                return this;
            return new FileInfo(_name, _parent == null ? Root : _parent.GetAbsoluteFile());
        }

        internal string GetCanonicalPath()
        {
            return GetCanonicalFile().GetAbsolutePath();
        }

        internal FileInfo GetCanonicalFile()
        {
            FileInfo cParent = (_parent == null ? null : _parent.GetCanonicalFile());
            if (_name == ".")
                return (cParent == null ? Root : cParent);
            if ((cParent != null) && (cParent._name == ""))
                cParent = null;
            if (_name == "..")
            {
                if (cParent == null)
                    return Root;
                if (cParent._parent == null)
                    return Root;
                return cParent._parent;
            }
            return new FileInfo(_name, (cParent == null) && (_name != "") ? Root : cParent);
        }

        internal bool Exists()
        {
            try
            {
                return (LocalStorage.GetItem(GetCanonicalPath()) != null);
            }
            catch (Exception e) { if (e.Message.StartsWith("IOException")) return false; throw e; }
        }

        internal bool IsFileInfo()
        {
            try
            {
                String s = LocalStorage.GetItem(GetCanonicalPath());
                return ((s != null) && !s.StartsWith("{"));
            }
            catch (Exception e) { if (e.Message.StartsWith("IOException")) return false; throw e; }
        }

        public long Length
        {
            get
            {
                try
                {
                    if (!Exists())
                        return 0;
                    FileStream raf = new FileStream(this, FileMode.Append, FileAccess.Read);
                    long length = raf.Length;
                    raf.Close();
                    return length;
                }
                catch (Exception e) { if (e.Message.StartsWith("IOException")) return 0; throw e; }
            }
        }

        private bool CreateNewFile()
        {
            if (Exists() || !_parent.Exists())
                return false;
            LocalStorage.SetItem(GetCanonicalPath(), WindowEx.Btoa(""));
            return true;
        }

        internal bool Delete_()
        {
            try
            {
                if (!Exists())
                    return false;
                LocalStorage.RemoveItem(GetCanonicalPath());
                return true;
            }
            catch (Exception e) { if (e.Message.StartsWith("IOException")) return false; throw e; }
        }

        private bool MakeDirectory()
        {
            try
            {
                if ((_parent != null) && !_parent.Exists())
                    return false;
                if (Exists())
                    return false;
                // We may want to make this a JS map
                LocalStorage.SetItem(GetCanonicalPath(), "{}");
                return true;
            }
            catch (Exception e) { if (e.Message.StartsWith("IOException")) return false; throw e; }
        }

        internal bool MakeDirectories()
        {
            if (_parent != null)
                _parent.MakeDirectories();
            return MakeDirectory();
        }

        #region List

        [AlternateSignature]
        private extern FileInfo[] ListFiles();
        private IEnumerable ListFiles(FileInfoSearchPredicate predicate)
        {
            ArrayList files = new ArrayList();
            try
            {
                String prefix = GetCanonicalPath();
                if (!prefix.EndsWith(Separator))
                    prefix += SeparatorChar;
                int cut = prefix.Length;
                int count = LocalStorage.Length;
                for (int index = 0; index < count; index++)
                {
                    String key = LocalStorage.Key(index);
                    if (key.StartsWith(prefix) && (key.IndexOf(SeparatorChar, cut) == -1))
                    {
                        String name = key.Substring(cut, key.Length);
                        if ((predicate == null) || predicate(name, this))
                            files.Add(new FileInfo(name, this));
                    }
                }
            }
            catch (Exception) { }
            return files;
        }

        #endregion
    }
}
