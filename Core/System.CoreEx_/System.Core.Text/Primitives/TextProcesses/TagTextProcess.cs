using System.Text;
namespace System.Primitives.TextProcesses
{
    /// <summary>
    /// Provides processing implementation for strings encoded using the format "[[key{parameter}parameter2]]".
    /// </summary>
    public class TagTextProcess : TextProcessBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagTextProcess"/> class.
        /// </summary>
        public TagTextProcess() { }

        public override string Process(string[] texts, Nattrib attrib)
        {
            if (texts == null)
                throw new ArgumentNullException("text");
            if (texts.Length != 1)
                throw new ArgumentOutOfRangeException("text");
            string singleText = texts[0];
            int index = 0;
            while ((singleText.IndexOf("[[") > -1) && (index++ < 5))
                singleText = ProcessInternal(singleText, attrib);
            return singleText;
        }

        private string ProcessInternal(string text, Nattrib attrib)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            int openTagIndex;
            int closeTagIndex;
            int startIndex = 0;
            var b = new StringBuilder();
            var textSearcher = new StringSearcher(text, "<![CDATA[", "]]>");
            // 1. expand tag
            while (textSearcher.FindTextSpan(startIndex, "[[", "]]", out openTagIndex, out closeTagIndex))
            {
                b.Append(text.Substring(startIndex, openTagIndex - startIndex));
                startIndex = openTagIndex + 2;
                //
                string tagKey;
                string[] args;
                //
                int openArgIndex;
                int closeArgIndex;
                if (((openArgIndex = textSearcher.IndexOf("{", startIndex, closeTagIndex - startIndex - 1)) > -1) && ((closeArgIndex = textSearcher.IndexOf("}", openArgIndex, closeTagIndex - openArgIndex - 1)) > -1))
                {
                    // has arguments
                    tagKey = text.Substring(startIndex, openArgIndex - startIndex).Trim();
                    string arg = text.Substring(openArgIndex + 1, closeArgIndex - openArgIndex - 1);
                    string arg2 = text.Substring(closeArgIndex + 1, closeTagIndex - closeArgIndex - 2);
                    args = new[] { arg, arg2 };
                }
                else
                {
                    // tag only - no arguments
                    tagKey = text.Substring(startIndex, closeTagIndex - startIndex - 1).Trim();
                    args = null;
                }
                //
                TextProcessBase tag;
                if ((tagKey.Length > 0) && ((tag = TextProcessBase.Get(tagKey)) != null))
                    b.Append(tag.Process(args));
                else
                    b.Append(text.Substring(openTagIndex, closeTagIndex - openTagIndex + 2));
                startIndex = closeTagIndex + 1;
            }
            b.Append(text.Substring(startIndex));
            return b.ToString();
        }
    }
}