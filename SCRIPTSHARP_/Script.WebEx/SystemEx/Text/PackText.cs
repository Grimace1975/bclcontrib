#if !CODE_ANALYSIS
namespace System.Text
#else
using System;
namespace SystemEx.Text
#endif
{
    public class PackText
    {
        //	key / value info strings
        public const int MAX_INFO_KEY = 64;
        public const int MAX_INFO_VALUE = 64;
        public const int MAX_INFO_STRING = 512;
        public const string SPACES = "                     ";

        private static ErrorHandler _errorHandler;
        public static ErrorHandler ErrorHandler
        {
            get { return _errorHandler; }
            set { _errorHandler = value; }
        }

        /// <summary>
        /// Info_ValueForKey
        /// Searches the string for the given key and returns the associated value, or an empty string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Info_GetValueForKey(string s, string key)
        {
            string[] tokens = s.Split('\\');
            for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex += 2)
            {
                string key1 = tokens[tokenIndex];
                if (tokenIndex + 1 >= tokens.Length)
                {
                    if (_errorHandler != null)
                        _errorHandler(ErrorCode.INFO, "MISSING VALUE\n", null);
                    return s;
                }
                string value1 = tokens[tokenIndex + 1];
                if (key == key1)
                    return value1;
            }
            return string.Empty;
        }

        public static string Info_RemoveKey(string s, string key)
        {
            StringBuilder b = new StringBuilder(); //(512);
            if (key.IndexOf('\\') != -1)
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "Can't use a key with a \\\n", null);
                return s;
            }
            string[] tokens = s.Split('\\');
            for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex += 2)
            {
                string key1 = tokens[tokenIndex];
                if (tokenIndex + 1 >= tokens.Length)
                {
                    if (_errorHandler != null)
                        _errorHandler(ErrorCode.INFO, "MISSING VALUE\n", null);
                    return s;
                }
                string value1 = tokens[tokenIndex + 1];
                if (key != key1)
                    b.Append('\\').Append(key1).Append('\\').Append(value1);
            }
            return b.ToString();
        }

        /// <summary>
        /// Info_Validate
        /// Some characters are illegal in info strings because they can mess up the server's parsing
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Info_Validate(string s)
        {
            return !((s.IndexOf('"') != -1) || (s.IndexOf(';') != -1));
        }

        public static string Info_SetValueForKey(string s, string key, string value)
        {
            if ((value == null) || (value.Length == 0))
                return s;
            if ((key.IndexOf('\\') != -1) || (value.IndexOf('\\') != -1))
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "Can't use keys or values with a \\\n", null);
                return s;
            }
            if (key.IndexOf(';') != -1)
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "Can't use keys or values with a semicolon\n", null);
                return s;
            }
            if ((key.IndexOf('"') != -1) || (value.IndexOf('"') != -1))
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "Can't use keys or values with a \"\n", null);
                return s;
            }
            if ((key.Length > MAX_INFO_KEY - 1) || (value.Length > MAX_INFO_KEY - 1))
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "Keys and values must be < 64 characters.\n", null);
                return s;
            }
            StringBuilder b = new StringBuilder(Info_RemoveKey(s, key));
            if ((StringBuilderEx.GetLength(b) + 2 + key.Length + value.Length) > MAX_INFO_STRING)
            {
                if (_errorHandler != null)
                    _errorHandler(ErrorCode.INFO, "Info string length exceeded\n", null);
                return s;
            }
            b.Append('\\').Append(key).Append('\\').Append(value);
            return b.ToString();
        }

        public static void Info_Print(string s)
        {
            StringBuilder b = new StringBuilder(); //(512);
            string[] tokens = s.Split('\\');
            for (int tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex += 2)
            {
                string key1 = tokens[tokenIndex];
                if ((tokenIndex + 1) >= tokens.Length)
                {
                    if (_errorHandler != null)
                        _errorHandler(ErrorCode.INFO, "MISSING VALUE\n", null);
                    return;
                }
                string value1 = tokens[tokenIndex + 1];
                b.Append(key1);
                int length = key1.Length;
                if (length < 20)
                    b.Append(SPACES.Substring(length, SPACES.Length));
                b.Append('=').Append(value1).Append('\n');
            }
            if (_errorHandler != null)
                _errorHandler(ErrorCode.INFO, b.ToString(), null);
        }
    }
}