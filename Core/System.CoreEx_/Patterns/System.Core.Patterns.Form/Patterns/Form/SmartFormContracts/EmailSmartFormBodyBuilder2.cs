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
//using System.Collections.Generic;
//using System.Net.Mail;
//using System.IO;
//using System.Collections;
//namespace System.Patterns.Forms.SmartFormContracts
//{
//    public interface IEmailSmartFormBodyBuilder
//    {
//        void Execute(SmartForm smartForm, MailMessage emailMessage, string scopeKey);
//    }

//    /// <summary>
//    /// IContract implementation that implements the ability to send a SmartForm-defined email.
//    /// </summary>
//    //+ dont like the mergetext function should just take a string and merge, like the one in POM
//
//    public class EmailSmartFormBodyBuilder : IEmailSmartFormBodyBuilder
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="EmailSmartFormBodyBuilder"/> class.
//        /// </summary>
//        public EmailSmartFormBodyBuilder() { }

//        /// <summary>
//        /// Executes the specified method.
//        /// </summary>
//        /// <param name="method">The method.</param>
//        /// <param name="args">The args.</param>
//        /// <returns></returns>
//        public void Execute(SmartForm smartForm, MailMessage emailMessage, string scopeKey)
//        {
//            if (smartForm == null)
//                throw new ArgumentNullException("smartForm");
//            if (emailMessage == null)
//                throw new ArgumentNullException("emailMessage");
//            if (scopeKey == null)
//                throw new ArgumentNullException("scopeKey");
//            //
//            string htmlBody = smartForm.CreateMergedText(scopeKey + "htmlBody");
//            if (htmlBody.Length > 0)
//            {
//                //if ((_htmlSchema != null) && (htmlBody.IndexOf("<html>", StringComparison.OrdinalIgnoreCase) == -1))
//                //    htmlBody = "<html><body>" + _htmlSchema.DecodeHtml(htmlBody, (int)HtmlSchemaBase.DecodeFlags.CrLfToBr) + "</body></html>";
//                emailMessage.IsBodyHtml = true;
//                emailMessage.Body = htmlBody;
//            }
//            else
//            {
//                emailMessage.IsBodyHtml = false;
//                emailMessage.Body = smartForm.CreateMergedText(scopeKey + "textBody");
//            }
//        }
//    }
//}