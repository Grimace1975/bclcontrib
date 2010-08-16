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
using System.Net;
using System.Net.Mail;
namespace System.Interop.Core.Net
{
    /// <summary>
    /// SmsClient
    /// </summary>
    public class SmsClient : SmsClientBase
    {
        private SmtpClient _smtpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsClient"/> class.
        /// </summary>
        public SmsClient(SmtpClient smtpClient)
        {
            if (smtpClient == null)
                throw new ArgumentNullException("smtpClient");
            _smtpClient = smtpClient;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void TrySend(SmsMessage message, out Exception ex)
        {
            if (From == null)
                throw new NullReferenceException("From");
            var emailMessage = new MailMessage
            {
                From = From,
                IsBodyHtml = false,
                Body = message.Body,
            };
            emailMessage.To.Add(new MailAddress(GetCarrierEmail(message.CarrierId, message.PhoneId)));
            try
            {
                _smtpClient.Send(emailMessage);
                ex = null;
            }
            catch (SmtpException e) { ex = e; }
        }

        public MailAddress From { get; set; }

        /// <summary>
        /// Gets the carrier email.
        /// </summary>
        /// <param name="carrierId">The carrier id.</param>
        /// <param name="phoneId">The phone id.</param>
        /// <returns></returns>
        private static string GetCarrierEmail(SmsCarrierId carrierId, string phoneId)
        {
            phoneId = StringEx.ExtractString.ExtractDigit(phoneId);
            switch (carrierId)
            {
                case SmsCarrierId.Verizon:
                    return phoneId + "@vtext.com";
                case SmsCarrierId.Sprint:
                    return phoneId + "@messaging.sprintpcs.com";
                case SmsCarrierId.ATT:
                    return phoneId + "@txt.att.net";
                case SmsCarrierId.TMobile:
                    return phoneId + "@tmomail.net";
                case SmsCarrierId.AllTel:
                    return phoneId + "@message.alltel.com";
                case SmsCarrierId.Cricket:
                    return phoneId + "@mms.mycricket.com";
                case SmsCarrierId.Cingular:
                    return phoneId + "@mobile.mycingular.com";
                case SmsCarrierId.Nextel:
                    return phoneId + "@messaging.nextel.com";
                case SmsCarrierId.Unicel:
                    return phoneId + "@utext.com";
                case SmsCarrierId.VirginMobile:
                    return phoneId + "@vmobl.com";
                case SmsCarrierId.NorthwestMissouriCellular:
                    return phoneId + "@mynwmcell.com";
                case SmsCarrierId.USCellular:
                    return phoneId + "@email.uscc.net";
                default:
                    throw new IndexOutOfRangeException(carrierId.ToString());
            }
        }
    }
}