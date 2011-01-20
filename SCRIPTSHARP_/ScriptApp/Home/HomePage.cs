using System;
using System.Html;
using System.TypedArrays;
using SystemEx;
using SystemEx.Html;
using SystemEx.IO;
namespace ScriptApp.Home
{
    internal static class HomePage
    {
        static HomePage()
        {
            LocalStorage["Key"] = "Value";


            WindowEx.Atob("This is here");

            FileInfo testFile = new FileInfo("/Test/This");
            Script.Alert(testFile.Path);
            //testFile.CreateNewFile();
            
            //foreach (FileInfo file in FileInfo.Root.ListFiles())
            //    Script.Alert(file.Path);
            //Script.Alert("Done");
        }
    }
}
