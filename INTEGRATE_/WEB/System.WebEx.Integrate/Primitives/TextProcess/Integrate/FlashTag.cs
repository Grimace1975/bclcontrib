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
//using System.Web.UI.WebControls;
//using System.Web.UI;
//namespace System.Primitives.TextProcesses.Integrate
//{
//    public class FlashTag : TextProcessBase
//    {
//        public FlashTag()
//            : base() { }

//        public override string Process(string[] texts, Nattrib attrib3)
//        {
//            if (texts.Length < 1)
//                throw new InvalidOperationException();
//            Nattrib attrib = Nattrib.Parse(texts[0].Split(';'));
//            var flashBlock = new FlashBlock(FlashBlock.FlashRenderType.SwfObject2_0) { ID = attrib.Slice("id", "flash" + CoreEx.GetNextId()), };
//            if (attrib.Slice<bool>("transparent"))
//                flashBlock.WindowMode = FlashBlock.WMode.Transparent;
//            else if (attrib.Exists("wmode"))
//                flashBlock.WindowMode = attrib.Slice<string>("wmode");
//            if (attrib.Exists("alt"))
//                flashBlock.Alt = attrib.Slice<string>("alt");
//            if (attrib.Exists("bgcolor"))
//                flashBlock.BgColor = attrib.Slice<string>("bgcolor");
//            if (attrib.Exists("expressInstallUri"))
//                flashBlock.ExpressInstallUri = attrib.Slice<string>("expressInstallUri");
//            if (attrib.Exists("menu"))
//                flashBlock.HasMenu = attrib.Slice<bool>("menu");
//            if (attrib.Exists("linkUri"))
//                flashBlock.LinkUrl = attrib.Slice<string>("linkUri");
//            if (attrib.Exists("uri"))
//                flashBlock.Uri = attrib.Slice<string>("uri");
//            if (attrib.Exists("version"))
//                flashBlock.Version = attrib.Slice<string>("version");
//            if (attrib.Exists("width"))
//                flashBlock.Width = new Unit(attrib.Slice<string>("width"));
//            if (attrib.Exists("height"))
//                flashBlock.Height = new Unit(attrib.Slice<string>("height"));
//            // additional variables
//            foreach (string name in attrib.Names)
//                flashBlock.Variable[name] = attrib.Value<string>(name);
//            // inner text
//            if (texts.Length > 1)
//                flashBlock.Write(texts[1]);
//            string responseText;
//            HtmlBuilder.RenderControl(flashBlock, out responseText);
//            return responseText;
//        }
//    }
//}