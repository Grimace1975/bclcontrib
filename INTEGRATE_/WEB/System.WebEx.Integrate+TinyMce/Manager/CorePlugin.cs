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
//using Moxiecode.Manager.Utils;
//using System.IO;
//namespace TinyMceV3.Manager
//{
//    /// <summary>
//    /// CorePlugin
//    /// </summary>
//    public class CorePlugin : Moxiecode.Manager.Plugin
//    {
//        private static readonly System.Reflection.FieldInfo s_mangerEngine_WwwRootField = typeof(Moxiecode.Manager.ManagerEngine).GetField("wwwroot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CorePlugin"/> class.
//        /// </summary>
//        public CorePlugin()
//            : base()
//        {
//        }

//        /// <summary>
//        /// Called when [pre init].
//        /// </summary>
//        /// <param name="man">The man.</param>
//        /// <param name="prefix">The prefix.</param>
//        /// <returns></returns>
//        public override bool OnPreInit(Moxiecode.Manager.ManagerEngine man, string prefix)
//        {
//            string encodedResource;
//            string virtualRootPath;
//            TinyMceV3.ApplicationUnit_.WebApplicationControl.TinyMceTextBox.GetRootPathValues(HtmlTextBoxHelper.ResourceId, out encodedResource, out virtualRootPath);
//            //+
//            man.Config["filesystem.rootpath"] = encodedResource;
//            man.Config["preview.wwwroot"] = virtualRootPath;
//            //+ lifecycle fixed could not properly set, so jaming value into field.
//            s_mangerEngine_WwwRootField.SetValue(man, virtualRootPath);
//            //+
//            return base.OnPreInit(man, prefix);
//        }

//        /// <summary>
//        /// Called when [before RPC].
//        /// </summary>
//        /// <param name="man">The man.</param>
//        /// <param name="cmd">The CMD.</param>
//        /// <param name="input">The input.</param>
//        /// <returns></returns>
//        public override bool OnBeforeRPC(Moxiecode.Manager.ManagerEngine man, string cmd, System.Collections.Hashtable input)
//        {
//            switch (cmd)
//            {
//                case "listFiles":
//                    string prefix = man.Prefix;
//                    if ((prefix != "fm") && (prefix != "im"))
//                    {
//                        break;
//                    }
//                    //+ create default document folder
//                    string resourcePath;
//                    int resourcePathIndex;
//                    if (TryParseEncodedPath(input, out resourcePath, out resourcePathIndex) == true)
//                    {
//                        input["path"] = resourcePath;
//                        if (!Directory.Exists(resourcePath))
//                        {
//                            //string unixResourcePath = Moxiecode.Manager.Utils.PathUtils.ToUnixPath(resourcePath);
//                            //if (KernelText.StartsWith(unixResourcePath, man.DecryptPath("{" + resourcePathIndex + "}")) == true)
//                            {
//                                Directory.CreateDirectory(resourcePath);
//                            }
//                            //string[] rootItems = man.Config["filesystem.rootpath"].Split(';');
//                            //foreach (string rootItem in rootItems)
//                            //{
//                            //    int equalIndex = rootItem.IndexOf('=');
//                            //    string rootPath = (equalIndex > -1 ? rootItem.Substring(equalIndex + 1) : rootItem);
//                            //    if (unixPath.StartsWith(rootPath) == true)
//                            //    {
//                            //        FileSystem.CreateDirectory(resourcePath);
//                            //        break;
//                            //    }
//                            //}
//                        }
//                    }
//                    break;
//            }
//            return base.OnBeforeRPC(man, cmd, input);
//        }

//        /// <summary>
//        /// Tries the parse encoded path.
//        /// </summary>
//        /// <param name="input">The input.</param>
//        /// <param name="resourcePath">The resource path.</param>
//        /// <param name="resourcePathIndex">Index of the resource path.</param>
//        /// <returns></returns>
//        public static bool TryParseEncodedPath(System.Collections.Hashtable input, out string resourcePath, out int resourcePathIndex)
//        {
//            resourcePathIndex = 0;
//            if ((input.ContainsKey("path") == true) && ((resourcePath = (string)input["path"]).StartsWith("deg:") == true))
//            {
//                resourcePath = resourcePath.Substring(4);
//                return true;
//            }
//            else
//            {
//                resourcePath = string.Empty;
//                return false;
//            }

//        }
//    }
//}
