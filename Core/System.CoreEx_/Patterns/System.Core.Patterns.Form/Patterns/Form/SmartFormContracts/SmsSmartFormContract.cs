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
using System.Collections;
using System.Net;
using System.Net.Mail;
namespace System.Patterns.Forms.SmartFormContracts
{
    /// <summary>
    /// IContract implementation that implements the ability to send a SmartForm-defined email.
    /// </summary>
    //+ dont like the mergetext function should just take a string and merge, like the one in POM

    public class SmsSmartFormContract : Patterns.IContract
    {
        private static object[] s_defaultArgs = new object[] { null, string.Empty };
        private static ParserEx.EnumInt32Parser s_carrierIdParser = ParserEx.CreateEnumInt32Parser(typeof(SmsCarrierId));
        private SmsClientBase _smsClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartForm"/> class.
        /// </summary>
        public SmsSmartFormContract(SmsClientBase smsClient)
        {
            if (smsClient == null)
                throw new ArgumentNullException("smsClient");
            _smsClient = smsClient;
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
            int smsSentCount = 0;
            var phoneIdList = new List<string>();
            for (int argIndex = 1; argIndex < args.Length; argIndex++)
            {
                string scopeKey = (args[argIndex] as string);
                if (scopeKey == null)
                    throw new ArgumentNullException(string.Format("args[{0}]", argIndex));
                if (scopeKey.Length > 0)
                    scopeKey += "::";
                foreach (string phoneId2 in smartForm[scopeKey + "phoneId"].Replace(";", ",").Split(','))
                {
                    string phoneId = phoneId2.Trim();
                    if ((phoneId.Length > 0) && (!phoneIdList.Contains(phoneId.ToLowerInvariant())))
                    {
                        string carrierIdAsText = smartForm[scopeKey + "carrierId"];
                        int carrierId;
                        if ((carrierIdAsText.Length > 0) && (s_carrierIdParser.TryGetValue(carrierIdAsText, out carrierId)))
                        {
                            // execute
                            var message = new SmsMessage()
                            {
                                PhoneId = phoneId,
                                CarrierId = (SmsCarrierId)carrierId,
                                Body = smartForm.CreateMergedText(scopeKey + "textBody"),
                            };
                            Exception ex;
                            _smsClient.TrySend(message, out ex);
                            smsSentCount++;
                        }
                        // prevent resends
                        phoneIdList.Add(phoneId.ToLowerInvariant());
                    }
                }
                phoneIdList.Clear();
            }
            return smsSentCount;
        }
    }
}