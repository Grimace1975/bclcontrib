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
//using System.Web;
//using System.Patterns.Reporting;
//using System.Collections.Generic;
//namespace System.Patterns.Reporting
//{
//    /// <summary>
//    /// FlatFileCsvEmitterExtensions
//    /// </summary>
//    public static partial class FlatFileCsvEmitterExtensions
//    {
//        public static void EmitHttp<TItem>(this FlatFileCsvEmitter emitter, string fileName, IEnumerable<TItem> set) { EmitHttp<TItem>(emitter, (FlatFileContext)null, fileName, set); }
//        public static void EmitHttp<TItem>(this FlatFileCsvEmitter emitter, FlatFileContext context, string fileName, IEnumerable<TItem> set)
//        {
//            if (string.IsNullOrEmpty(fileName))
//                throw new ArgumentNullException("fileName");
//            if (set == null)
//                throw new ArgumentNullException("set");
//            // emit
//            var response = HttpContext.Current.Response;
//            //Http.Instance.IsRender = false;
//            response.ContentType = "application/csv; name=" + fileName;
//            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
//            emitter.Emit<TItem>(context, response.Output, set);
//        }
//    }
//}