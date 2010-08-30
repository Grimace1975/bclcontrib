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
using System.Text;
using System.Text.Html;
using System.Text.RegularExpressions;
using System.Primitives.DataTypes;
namespace System.Patterns.Schema
{
    /// <summary>
    /// MailToHtmlDecoder
    /// </summary>
    public class MailToHtmlDecoder : HtmlDecoderBase
    {
        private static readonly Regex s_emailRegExp = new Regex(EmailDataType.EmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public override void Decode(StringBuilder b, HtmlFormatterBase formatter, string text)
        {
            // fix email addresses (mailto:x@x)
            int textLength = text.Length;
            int startIndex = 0;
            int commitIndex = startIndex;
            int index;
            while ((index = text.IndexOf("mailto:", startIndex, StringComparison.OrdinalIgnoreCase)) > -1)
            {
                startIndex = index + 7;
                // check for space prefix
                if ((index > 0) && (text[index - 1] != ' '))
                    continue;
                string email;
                // locate end of email
                int index2;
                for (index2 = startIndex; (index2 < textLength) && (EmailDataType.IsValidCharacterForTextParsing(text[index2])); index2++) ;
                if ((index2 < textLength) && (index2 > 0) && (".".IndexOf(text[index2]) > -1))
                    index2--;
                if (index2 < textLength)
                {
                    email = text.Substring(startIndex, index2 - startIndex);
                    startIndex = index2;
                }
                else
                {
                    email = text.Substring(startIndex);
                    startIndex = textLength;
                }
                // check if email
                if (s_emailRegExp.IsMatch(email))
                {
                    // commit to textBuilder
                    if (index > commitIndex)
                        b.Append(text.Substring(commitIndex, index - commitIndex));
                    commitIndex = startIndex;
                    b.Append(formatter.Format(email));
                }
            }
            // overflow commit to textBuilder
            if (commitIndex < textLength)
                b.Append(text.Substring(commitIndex));
        }
    }
}