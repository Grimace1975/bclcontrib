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

        private string ProcessInternal(string texts, Nattrib attrib)
        {
            if (texts == null)
                throw new ArgumentNullException("text");
            int openTagIndex;
            int closeTagIndex;
            int startIndex = 0;
            var b = new StringBuilder();
            var textSearcher = new StringSearcher(texts, "<![CDATA[", "]]>");
            // 1. expand tag
            while (textSearcher.FindTextSpan(startIndex, "[[", "]]", out openTagIndex, out closeTagIndex))
            {
                b.Append(texts.Substring(startIndex, openTagIndex - startIndex));
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
                    tagKey = texts.Substring(startIndex, openArgIndex - startIndex).Trim();
                    string arg = texts.Substring(openArgIndex + 1, closeArgIndex - openArgIndex - 1);
                    string arg2 = texts.Substring(closeArgIndex + 1, closeTagIndex - closeArgIndex - 2);
                    args = new[] { arg, arg2 };
                }
                else
                {
                    // tag only - no arguments
                    tagKey = texts.Substring(startIndex, closeTagIndex - startIndex - 1).Trim();
                    args = null;
                }
                //
                TextProcessBase tag;
                if ((tagKey.Length > 0) && ((tag = TextProcessBase.Get(tagKey)) != null))
                    b.Append(tag.Process(args));
                else
                    b.Append(texts.Substring(openTagIndex, closeTagIndex - openTagIndex + 2));
                startIndex = closeTagIndex + 1;
            }
            b.Append(texts.Substring(startIndex));
            return b.ToString();
        }
    }
}