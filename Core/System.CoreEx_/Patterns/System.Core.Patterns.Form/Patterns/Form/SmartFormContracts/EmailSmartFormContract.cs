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
using System.Collections.Generic;
using System.Net.Mail;
using System.IO;
using System.Collections;
namespace System.Patterns.Forms.SmartFormContracts
{
    /// <summary>
    /// IContract implementation that implements the ability to send a SmartForm-defined email.
    /// </summary>
    //+ dont like the mergetext function should just take a string and merge, like the one in POM

    public class EmailSmartFormContract : IContract
    {
        private static object[] s_defaultArgs = new object[] { null, string.Empty };
        private IEmailSmartFormBodyBuilder _bodyBuilder;
        private SmtpClient _smtpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartForm"/> class.
        /// </summary>
        public EmailSmartFormContract(IEmailSmartFormBodyBuilder bodyBuilder, SmtpClient smtpClient)
        {
            if (bodyBuilder == null)
                throw new ArgumentNullException("bodyBuilder");
            if (smtpClient == null)
                throw new ArgumentNullException("smtpClient");
            _bodyBuilder = bodyBuilder;
            _smtpClient = smtpClient;
        }

        /// <summary>
        /// Executes the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public object Execute(string method, params object[] args)
        {
            SmartForm smartForm;
            if ((smartForm = (args[0] as SmartForm)) == null)
                throw new ArgumentException(Local.TypeNotSmartForm, "args");
            if (args.Length == 1)
                args = s_defaultArgs;
            // send email
            int emailSentCount = 0;
            var toEmailList = new List<string>();
            for (int argIndex = 1; argIndex < args.Length; argIndex++)
            {
                string scopeKey = (args[argIndex] as string);
                if (scopeKey == null)
                    throw new ArgumentNullException(string.Format("args[{0}]", argIndex));
                if (scopeKey.Length > 0)
                    scopeKey += "::";
                foreach (string toEmail2 in smartForm[scopeKey + "toEmail"].Replace(";", ",").Split(','))
                {
                    string toEmail = toEmail2.Trim();
                    if ((toEmail.Length > 0) && (!toEmailList.Contains(toEmail.ToLowerInvariant())))
                    {
                        string fromEmail = smartForm[scopeKey + "fromEmail"];
                        if (fromEmail.Length > 0)
                        {
                            // execute
                            var emailMessage = new MailMessage();
                            string fromName = smartForm.CreateMergedText(scopeKey + "fromName");
                            emailMessage.From = (fromName.Length > 0 ? new MailAddress(fromEmail, fromName) : new MailAddress(fromEmail));
                            string toName = smartForm[scopeKey + "toName"];
                            emailMessage.To.Add(toName.Length > 0 ? new MailAddress(toEmail, toName) : new MailAddress(toEmail));
                            string ccEmail = smartForm[scopeKey + "ccEmail"];
                            if (ccEmail.Length > 0)
                                emailMessage.CC.Add(ccEmail.Replace(";", ","));
                            string bccEmail = smartForm[scopeKey + "bccEmail"];
                            if (bccEmail.Length > 0)
                                emailMessage.Bcc.Add(bccEmail.Replace(";", ","));
                            string replyToEmail = smartForm[scopeKey + "replyToEmail"];
                            if (replyToEmail.Length > 0)
                            {
                                string replyToName = smartForm[scopeKey + "replyToName"];
                                emailMessage.ReplyTo = (replyToName.Length > 0 ? new MailAddress(replyToEmail, replyToName) : new MailAddress(replyToEmail));
                            }
                            emailMessage.Subject = smartForm.CreateMergedText(scopeKey + "subject");
                            _bodyBuilder.Execute(smartForm, emailMessage, scopeKey);
                            string attachmentList = smartForm[scopeKey + "attachmentList"];
                            if (attachmentList.Length > 0)
                                foreach (string attachment in attachmentList.Split(';'))
                                {
                                    if ((attachment.Length > 0) && (File.Exists(attachment)))
                                        emailMessage.Attachments.Add(new Attachment(attachment));
                                }
                            _smtpClient.Send(emailMessage);
                            emailSentCount++;
                        }
                        // prevent resends
                        toEmailList.Add(toEmail.ToLowerInvariant());
                    }
                }
                toEmailList.Clear();
            }
            return emailSentCount;
        }
    }
}