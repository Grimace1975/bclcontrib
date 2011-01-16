using System;
using SystemEx.Html;
namespace SystemEx.IO
{
    public class File
    {
        public static bool Delete_(string path)
        {
            if (path == null)
                throw new Exception("ArgumentNullException: path");
            return new FileInfo(path).Delete_();
        }

        public static bool Exists(string path)
        {
            if (path == null)
                throw new Exception("ArgumentNullException: path");
            return new FileInfo(path).Exists();
        }
    }
}
