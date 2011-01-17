using System;
using SystemEx.Html;
namespace SystemEx.IO
{
    public class Directory
    {
        public static bool Exists(string path)
        {
            if (path == null)
                throw new Exception("ArgumentNullException: path");
            return new FileInfo(path).Exists();
        }
    }
}
