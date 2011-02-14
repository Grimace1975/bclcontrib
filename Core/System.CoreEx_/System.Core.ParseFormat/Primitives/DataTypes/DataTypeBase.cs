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
//[assembly: Instinct.Pattern.Environment.Attribute.FactoryConfiguration("dataType", typeof(System.Primitives.DataTypeBase))]
#if !SqlServer
#endif
namespace System.Primitives.DataTypes
{
    /// <summary>
    /// DataTypeBase
    /// </summary>
    public abstract partial class DataTypeBase //: SimpleFactoryBase<DataTypeBase>
    {
        internal static readonly Type DataTypeBaseType = typeof(DataTypeBase);

        protected DataTypeBase(Type type, DataTypeFormFieldMeta formFieldMeta, DataTypeFormatterBase formatter, DataTypeParserBase parser)
        {
            Type = type;
            FormFieldMeta = formFieldMeta;
            Formatter = formatter;
            Parser = parser;
        }

        [Flags]
        public enum CountryId
        {
            None,
            Usa,
            Canada,
        }

//        protected static DataTypeBase Create<T>(IAppUnit appUnit)
//            where T : DataTypeBase
//        {
//#if !SqlServer
//            return ServiceLocator.Resolve<T>();
//#else
//            return default(T); //CreatePrime(key);
//#endif
//        }

        protected static DataTypeBase CreatePrimeById(string id)
        {
            switch (id)
            {
                case "Bool": return new BoolDataType();
                case "CreditCardId": return new CreditCardIdDataType();
                case "Date": return new DateDataType();
                case "DateTime": return new DateTimeDataType();
                case "Decimal": return new DecimalDataType();
                case "DecimalRange": return new DecimalRangeDataType();
                case "Email": return new EmailDataType();
                case "EmailList": return new EmailListDataType();
                case "Hostname": return new HostnameDataType();
                case "HostnameList": return new HostnameListDataType();
                case "Integer": return new IntegerDataType();
                case "IntegerRange": return new IntegerRangeDataType();
                case "Memo": return new MemoDataType();
                case "Money": return new MoneyDataType();
                case "MonthAndDay": return new MonthAndDayDataType();
                case "NotBool": return new NotBoolDataType();
                case "Percent": return new PercentDataType();
                case "Phone": return new PhoneDataType();
                case "Real": return new RealDataType();
                case "Regex": return new RegexDataType();
                case "Sequence": return new SequenceDataType();
                case "Text": return new TextDataType();
                case "Time": return new TimeDataType();
                case "Uri": return new UriDataType();
                case "UriId": return new UriIdDataType();
                case "Xml": return new XmlDataType();
                case "Zip": return new ZipDataType();
            }
            throw new InvalidOperationException();
        }

        public DataTypeFormatterBase Formatter { get; protected set; }
        public DataTypeParserBase Parser { get; protected set; }
        public DataTypeFormFieldMeta FormFieldMeta { get; protected set; }
        public Type Type { get; private set; }
    }
}