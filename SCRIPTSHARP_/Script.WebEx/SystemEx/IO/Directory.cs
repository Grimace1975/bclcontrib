using System;
using SystemEx.Html;
namespace SystemEx.IO
{
    public class Directory
    {
        //public static bool CreateDirectory(string path)
        //{
        //    try
        //    {
        //        if ((parent != null) && !parent.Exists())
        //            return false;
        //        if (Exists(path))
        //            return false;
        //        // We may want to make this a JS map
        //        LocalStorage.SetItem(Path.GetCanonicalPath(path), "{}");
        //        return true;
        //    }
        //    catch (Exception e) { if (e.Message.StartsWith("IOException")) return false; throw e; }
        //}

        public static bool Exists(string path)
        {
            if (path == null)
                throw new Exception("ArgumentNullException: path");
            return new FileInfo(path).Exists();
        }
    }
}
