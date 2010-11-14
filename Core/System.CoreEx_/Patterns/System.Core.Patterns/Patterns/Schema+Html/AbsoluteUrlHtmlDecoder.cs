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
using System.Linq;
using System.Text;
using System.Text.Html;
using System.Primitives.DataTypes;
namespace System.Patterns.Schema
{
    /// <summary>
    /// AbsoluteUrlHtmlDecoder
    /// </summary>
    public class AbsoluteUrlHtmlDecoder : HtmlDecoderBase
    {
        public override void Decode(StringBuilder b, HtmlFormatterBase formatter, string text)
        {
            if (formatter == null)
                throw new ArgumentNullException("formatter");
            // fix http:// link, and make target="_blank"
            using (var r = new HtmlStringReader(text, b))
            {
                int textLength = text.Length;
                int urlStartIndex;
                while ((urlStartIndex = new int[] { r.IndexOf("http://"), r.IndexOf("https://") }.MinSkipNull(-1)) > -1)
                //while ((urlStartIndex = ArrayEx.MinSkipNull(-1, r.IndexOf("http://"), r.IndexOf("https://"))) > -1)
                {
                    // locate end of url
                    char c;
                    int urlEndIndex = 0;
                    for (urlEndIndex = urlStartIndex; (urlEndIndex < textLength) && ((c = text[urlEndIndex]) != '\"') && (UriDataType.IsValidCharacterForTextParsing(c)); urlEndIndex++) ;
                    urlEndIndex--;
                    if ((urlEndIndex > 0) && (".?".IndexOf(text[urlEndIndex]) > -1))
                        urlEndIndex--;
                    string url = text.Substring(urlStartIndex, urlEndIndex - urlStartIndex + 1);
                    //
                    r.SetSearchValue(urlStartIndex, urlEndIndex);
                    // check if in anchor
                    if (r.SearchIfInElement("left"))
                    {
                        r.StreamToBeginElement();
                        formatter.Format(r, url);
                    }
                    // else if in any open element then stream to end of element. ex: <img src="http://www.xyz.com/" />"
                    else if (r.SearchIfInElement())
                    {
                        r.StreamToEndOfFirstElement();
                        continue;
                    }
                    // free floating
                    else
                    {
                        r.StreamToEndOfValue();
                        r.Append(formatter.Format(url));
                    }
                }
            }
        }
    }
}