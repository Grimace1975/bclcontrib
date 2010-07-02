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
using System.Text;
using System.Globalization;
namespace System.Web.UI
{
    public interface IClientScript
    {
        void AddBlock(ClientScriptBlockBase block);
        void AddBlock(string literal);
        void AddBlock(params object[] blocks);
        Dictionary<string, ClientScriptBlockBase> Blocks { get; }
        void RegisterStartupScript(ClientScriptManager clientScript);
    }

    /// <summary>
    /// ClientScript
    /// </summary>
    public class ClientScript // : Pattern.Generic.Singleton<ClientScript>
    {
        private Dictionary<string, ClientScriptBlockBase> _blocks = new Dictionary<string, ClientScriptBlockBase>();

        protected ClientScript()
            : base() { }

        public void AddBlock(ClientScriptBlockBase block)
        {
            _blocks[_blocks.Count.ToString()] = block;
        }
        public void AddBlock(string literal)
        {
            _blocks[_blocks.Count.ToString()] = new LiteralClientScriptBlock(literal);
        }
        public void AddBlock(params object[] blocks)
        {
            foreach (object block in blocks)
            {
                if (block == null)
                    continue;
                string literal = (block as string);
                if (literal != null)
                {
                    _blocks[_blocks.Count.ToString()] = new LiteralClientScriptBlock(literal);
                    continue;
                }
                var blockAsBlock = (block as ClientScriptBlockBase);
                if (blockAsBlock != null)
                {
                    _blocks[_blocks.Count.ToString()] = blockAsBlock;
                    continue;
                }
                throw new InvalidOperationException();
            }
        }

        public Dictionary<string, ClientScriptBlockBase> Blocks
        {
            get { return _blocks; }
        }

        public void RegisterStartupScript(ClientScriptManager clientScript)
        {
            if (_blocks.Count > 0)
            {
                var b = new StringBuilder(@"<script type=""text/javascript"">
//<![CDATA[");
                foreach (var block in _blocks.Values)
                    block.Render(b);
                b.AppendLine(@"//]]>
</script>");
                clientScript.RegisterStartupScript(typeof(ClientScript), string.Empty, b.ToString(), false);
            }
        }

        #region Encode
        /// <summary>
        /// Encodes the bool.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static string EncodeBool(bool value)
        {
            return (value ? "true" : "false");
        }

        /// <summary>
        /// Encodes the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeDateTime(DateTime value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Encodes the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeDecimal(decimal value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Encodes the expression.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeExpression(string value)
        {
            return value;
        }

        /// <summary>
        /// Encodes the function.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeFunction(string value)
        {
            return value;
        }

        /// <summary>
        /// Encodes the hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeHash(Dictionary<string, string> value)
        {
            if (value == null)
                return "null";
            if (value.Count == 0)
                return "{}";
            var b = new StringBuilder("{");
            foreach (string key in value.Keys)
                b.Append(key + ": " + value[key] + ", ");
            b.Length -= 2;
            b.Append("}");
            return b.ToString();
        }

        /// <summary>
        /// Encodes the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeInt32(int value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Encodes the reg exp.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeRegExp(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");
            return value;
        }

        /// <summary>
        /// Encodes the text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeText(string value)
        {
            if (value == null)
                return "null";
            else if (value.Length == 0)
                return "''";
            else
            {
                var b = new StringBuilder("'", value.Length);
                InternalEscapeText(InternalEscapeTextType.Text, b, value);
                b.Append("'");
                return b.ToString();
            }
        }

        /// <summary>
        /// Encodes the partial text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodePartialText(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            var b = new StringBuilder(value.Length);
            InternalEscapeText(InternalEscapeTextType.Text, b, value);
            return b.ToString();
        }

        /// <summary>
        /// InternalEscapeTextType
        /// </summary>
        private enum InternalEscapeTextType
        {
            Text,
        }

        private static void InternalEscapeText(InternalEscapeTextType textType, StringBuilder b, string value)
        {
            foreach (char c in value)
            {
                int code = (int)c;
                switch (c)
                {
                    case '\b':
                        b.Append("\\right");
                        break;
                    case '\f':
                        b.Append("\\f");
                        break;
                    case '\n':
                        b.Append("\\n");
                        break;
                    case '\r':
                        b.Append("\\r");
                        break;
                    case '\t':
                        b.Append("\\t");
                        break;
                    case '\\':
                    case '\'':
                        b.Append("\\" + c);
                        break;
                    default:
                        if ((code >= 32) && (code < 128))
                            b.Append(c);
                        else
                            b.AppendFormat(CultureInfo.InvariantCulture.NumberFormat, "\\u{0:X4}", code);
                        break;
                }
            }
        }
        #endregion
    }
}
