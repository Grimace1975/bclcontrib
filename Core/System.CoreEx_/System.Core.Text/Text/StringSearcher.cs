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
namespace System.Text
{
    /// <summary>
    /// StringSearcher
    /// </summary>
    public class StringSearcher
    {
        private List<TextSpan> _excludeTextSpanList;
        private string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSearch"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="openExcludeToken">The open exclude token.</param>
        /// <param name="closeExcludeToken">The close exclude token.</param>
        public StringSearcher(string text, string openExcludeToken, string closeExcludeToken)
            : this(text, false, new string[] { openExcludeToken, closeExcludeToken }) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSearch"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="isNested">if set to <c>true</c> [is nested].</param>
        /// <param name="excludeTokens">The exclude tokens.</param>
        public StringSearcher(string text, bool isNested, string[] excludeTokens)
        {
            _text = text;
            _excludeTextSpanList = (!isNested ? CreateFlatTextSpanList(0, excludeTokens) : CreateNestedTextSpanList(0, excludeTokens));
        }

        /// <summary>
        /// Adds the exclude text span.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        public void AddExcludeTextSpan(int startIndex, int length)
        {
            if ((startIndex < 0) || (startIndex >= _text.Length))
                throw new ArgumentOutOfRangeException("startIndex", startIndex, string.Format(Local.IndexOutOfRangeAB, 0, _text.Length));
            if ((length <= 0) || ((startIndex + length) > _text.Length))
                throw new ArgumentOutOfRangeException("length", length, string.Format(Local.IndexOutOfRangeAB, 1, _text.Length - startIndex));
            _excludeTextSpanList.Add(new TextSpan(startIndex, startIndex + length - 1));
        }

        /// <summary>
        /// Creates the flat text span list.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="tokenArray">The token array.</param>
        /// <returns></returns>
        private List<TextSpan> CreateFlatTextSpanList(int startIndex, string[] tokens)
        {
            if ((tokens == null) || (tokens.Length == 0))
                throw new ArgumentNullException("tokens");
            if (tokens.Length != 2)
                throw new ArgumentException("tokens", Local.InvalidArrayLength);
            string openToken = tokens[0];
            int openTokenLength = openToken.Length;
            string closeToken = tokens[1];
            int closeTokenLength = closeToken.Length;
            //
            var textSpanList = new List<TextSpan>();
            TextSpan textSpan = null;
            int nextStartIndex = startIndex;
            int nextEndIndex = -1;
            do
            {
                // locate openning token
                int startTextSpanIndex = _text.IndexOf(openToken, nextStartIndex, StringComparison.OrdinalIgnoreCase);
                if (startTextSpanIndex == -1)
                    break;
                // starting position to search for closing token
                if ((nextEndIndex == -1) || (nextEndIndex < startTextSpanIndex))
                    nextEndIndex = startTextSpanIndex;
                // locate closing token
                int endTextSpanIndex = _text.IndexOf(closeToken, nextEndIndex, StringComparison.OrdinalIgnoreCase);
                if (endTextSpanIndex == -1)
                    break;
                if (textSpan == null)
                    // first span
                    textSpan = new TextSpan(startTextSpanIndex, endTextSpanIndex + closeTokenLength - 1);
                else if (textSpan.Merge(startTextSpanIndex, endTextSpanIndex + closeTokenLength - 1))
                {
                    // overlapping with current span - do nothing
                }
                else
                {
                    // not-overlapping with current span
                    textSpanList.MergeAdd(textSpan);
                    textSpan = new TextSpan(startTextSpanIndex, endTextSpanIndex + closeTokenLength - 1);
                }
                nextStartIndex = startTextSpanIndex + openTokenLength;
                nextEndIndex = endTextSpanIndex + closeTokenLength;
            } while (true);
            // append last span, if one exists
            if (textSpan != null)
                textSpanList.MergeAdd(textSpan);
            return textSpanList;
        }

        /// <summary>
        /// Creates the nested text span list.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="tokenArray">The token array.</param>
        /// <returns></returns>
        private List<TextSpan> CreateNestedTextSpanList(int startIndex, string[] tokens)
        {
            if ((tokens == null) || (tokens.Length == 0))
                throw new ArgumentNullException("tokens");
            if ((tokens.Length % 2) != 0)
                throw new ArgumentException("tokens", Local.InvalidArrayLength);
            var textSpanList = new List<TextSpan>();
            // for each span token pair
            for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex += 2)
            {
                string openToken = tokens[tokenIndex];
                int openTokenLength = openToken.Length;
                string closeToken = tokens[tokenIndex + 1];
                int closeTokenLength = closeToken.Length;
                //
                TextSpan textSpan = null;
                int nextStartIndex = startIndex;
                int nextEndIndex = -1;
                do
                {
                    // locate openning token
                    int startTextSpanIndex = _text.IndexOf(openToken, nextStartIndex, StringComparison.OrdinalIgnoreCase);
                    if (startTextSpanIndex == -1)
                        break;
                    // starting position to search for closing token
                    // starting position to search for closing token
                    if ((nextEndIndex == -1) || (nextEndIndex < startTextSpanIndex))
                        nextEndIndex = startTextSpanIndex;
                    // locate closing token
                    int endTextSpanIndex = _text.IndexOf(closeToken, nextEndIndex, StringComparison.OrdinalIgnoreCase);
                    if (endTextSpanIndex == -1)
                        break;
                    if (textSpan == null)
                        // first span
                        textSpan = new TextSpan(startTextSpanIndex, endTextSpanIndex + closeTokenLength - 1);
                    else if (textSpan.Merge(startTextSpanIndex, endTextSpanIndex + closeTokenLength - 1))
                    {
                        // overlapping with current span - do nothing
                    }
                    else
                    {
                        // not-overlapping with current span
                        textSpanList.MergeAdd(textSpan);
                        textSpan = new TextSpan(startTextSpanIndex, endTextSpanIndex + closeTokenLength - 1);
                    }
                    nextStartIndex = startTextSpanIndex + openTokenLength;
                    nextEndIndex = endTextSpanIndex + closeTokenLength;
                } while (true);
                // append last span, if one exists
                if (textSpan != null)
                    textSpanList.MergeAdd(textSpan);
            }
            return textSpanList;
        }

        /// <summary>
        /// Finds the text span.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="openToken">The open token.</param>
        /// <param name="closeToken">The close token.</param>
        /// <param name="startTextSpanIndex">Start index of the text span.</param>
        /// <param name="endTextSpanIndex">End index of the text span.</param>
        /// <returns></returns>
        public bool FindTextSpan(int startIndex, string openToken, string closeToken, out int startTextSpanIndex, out int endTextSpanIndex)
        {
            startTextSpanIndex = -1;
            endTextSpanIndex = -1;
            int openTokenIndex;
            int openTokenLength = openToken.Length;
            int closeTokenIndex;
            int closeTokenLength = closeToken.Length;
            // find matching open/close tokens
            if ((startIndex < _text.Length) && ((openTokenIndex = IndexOf(openToken, startIndex)) > -1) && ((closeTokenIndex = IndexOf(closeToken, openTokenIndex)) > -1))
            {
                // handle multiple consecutive closing tokens are
                int moveBy = 0;
                while ((closeTokenIndex < _text.Length - closeTokenLength) && (_text.Substring(closeTokenIndex + 1, closeTokenLength) == closeToken))
                {
                    moveBy++;
                    closeTokenIndex++;
                }
                if (moveBy > 0)
                    closeTokenIndex -= (moveBy - (moveBy % closeTokenLength));
                // find matching open token to last
                openTokenIndex = LastIndexOf(openToken, closeTokenIndex, closeTokenIndex - openTokenIndex + 1);
                // compute start/end index
                startTextSpanIndex = openTokenIndex;
                endTextSpanIndex = closeTokenIndex + closeTokenLength - 1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public int IndexOf(string value, int startIndex)
        {
            return IndexOf(value, startIndex, value.Length - startIndex + 1);
        }
        public int IndexOf(string value, int startIndex, int count)
        {
            int endScanIndex = (count > -1 ? startIndex + count : _text.Length);
            int index = startIndex;
            do
            {
                index = _text.IndexOf(value, index, StringComparison.OrdinalIgnoreCase);
                if (index == -1)
                    return -1;
                // determine whether matched token is in an exclude zone
                TextSpan boundingTextSpan = _excludeTextSpanList.GetBoundingTextSpan(index);
                if (boundingTextSpan == null)
                    return index;
                index = boundingTextSpan.EndIndex;
                // increment by one character just in case searched value is subset
                index++;
            } while (index < endScanIndex);
            return -1;
        }

        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns></returns>
        public int LastIndexOf(string value, int startIndex)
        {
            return LastIndexOf(value, startIndex, startIndex + 1);
        }
        public int LastIndexOf(string value, int startIndex, int count)
        {
            int valueLength = value.Length;
            int nextStartIndex = startIndex;
            int index = -1;
            do
            {
                index = _text.LastIndexOf(value, nextStartIndex, count, StringComparison.OrdinalIgnoreCase);
                if (index == -1)
                    return -1;
                // determine whether matched token is in an exclude zone
                TextSpan boundingTextSpan = _excludeTextSpanList.GetBoundingTextSpan(index);
                if (boundingTextSpan == null)
                    return index;
                index = boundingTextSpan.StartIndex;
                count -= (nextStartIndex - index + 1);
                // decrement by one character just in case searched value is subset
                nextStartIndex = index - 1;
            } while (index > -1);
            return -1;
        }

        //#region DEBUG
        //public void Dump()
        //{
        //    string text = _text.Replace("\x0d", "?").Replace("\x0a", "?");
        //    var mask = new StringBuilder(new string(' ', text.Length), text.Length);
        //    foreach (TextSpan textSpan in _excludeTextSpanList)
        //        for (int textIndex = textSpan.StartIndex; textIndex <= textSpan.EndIndex; textIndex++)
        //            mask[textIndex] = '*';
        //    Console.WriteLine(text);
        //    Console.WriteLine(mask);
        //    Console.WriteLine("===");
        //}
        //#endregion
    }
}