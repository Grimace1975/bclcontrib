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
namespace System.Primitives
{
    /// <summary>
    /// DataTypeEnum
    /// Enumeration representing the types of extractions support by a class.
    /// </summary>
    public class DataTypeEnum
    {
        public const string Bool = "Bool";
        public const string CreditCardId = "CreditCardId";
        public const string Date = "Date";
        public const string DateTime = "DateTime";
        public const string Decimal = "Decimal";
		public const string DecimalRange = "DecimalRange";
        public const string Email = "Email";
        public const string EmailList = "EmailList";
        public const string Hostname = "Hostname";
        public const string HostnameList = "HostnameList";
        public const string Integer = "Integer";
		public const string IntegerRange = "IntegerRange";
		//public const string Key = "Key";
        public const string MonthAndDay = "MonthAndDay";
        public const string Memo = "Memo";
        public const string Money = "Money";
        public const string NotBool = "NotBool";
        public const string Percent = "Percent";
        public const string Phone = "Phone";
        public const string Real = "Real";
		public const string Regex = "Regex";
        public const string Sequence = "Sequence";
        public const string Text = "Text";
        public const string Time = "Time";
        public const string Uri = "Uri";
        public const string UriId = "UriId";
        public const string Xml = "Xml";
        public const string Zip = "Zip";
        public const string SmallText = "SmallText";
        public const string MediumText = "MediumText";
        public const string LargeText = "LargeText";
    }
}
