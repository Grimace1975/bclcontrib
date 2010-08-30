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
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Security.Principal;
namespace System.Security
{
    /// <summary>
    /// SecurityEx
    /// </summary>
    public static class SecurityEx
    {
        /// <summary>
        /// Hash utility - pass the hash algorithm name as a string i.e. SHA256, SHA1, MD5 etc.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="algorithm"></param>      
        /// <returns>Hashed String</returns>
        public static string HashIt(string input, string algorithm)
        {
            return HashIt(input, algorithm, true);
        }

        /// <summary>
        /// Hash utility - pass the hash algorithm name as a string i.e. SHA256, SHA1, MD5 etc.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="algorithm"></param>
        /// <param name="upperCase"></param>
        /// <returns>Hashed String</returns>
        public static string HashIt(string input, string algorithm, bool upperCase)
        {
            // In theory it could be an empty string
            if (input == null)
                throw new ArgumentNullException("input");
            if (string.IsNullOrEmpty(algorithm))
                throw new ArgumentNullException("algorithm");
            byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName(algorithm)).ComputeHash(Encoding.UTF8.GetBytes(input));
            var resultAsText = BitConverter.ToString(result).Replace("-", string.Empty);
            return (upperCase ? resultAsText.ToUpper() : resultAsText.ToLower());
        }

#if !SqlServer
        //NOTE: For use in the PasswordEncrypt and PasswordDecrypt members only.
        //Since we're accepting a user created clear text password - it's  unlikey that the password
        //will be more than 128 bits long, or 16 bytes (although it's possibe). The default AES block size
        //is 128 bits and so even though the mode of encryption is CBC - we'll amlost never encrypt more than a single block.
        //Even if we were encrypting multiple blocks, the IV does not have to be a secret - so it's hard
        //coded here for now (although in the future it should be moved to an application specific configuration setting
        //along with the full length symmetric key).
        private const string s_hexIv = "C9DCF37AED8574A1441FD82DB743765C";

        /// <summary>
        /// Helper method to encrypt an account password. Password encryption must be 'reversable' in
        /// order to support WSSE authentication. Atom-enabled clients using WSSE will be performing the WSSE digest
        /// on the client (not on the server) and so we must be able to recover the user's password in order
        /// to verfify the WSSE digest.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Encrypt(string text, string key)
        {
            // In theory it could be an empty string
            if (text == null)
                throw new ArgumentNullException("text");
            byte[] bytes = SymmetricEncrypt(Encoding.UTF8.GetBytes(text), ConvertEx.FromBase16String(key), ConvertEx.FromBase16String(s_hexIv));
            return ConvertEx.ToBase16String(bytes);
        }

        /// <summary>
        /// Helper method to decrypt an account password. Password encryption must be 'reversable' in
        /// order to support WSSE authentication. Atom-enabled clients using WSSE will be performing the WSSE digest
        /// on the client (not on the server) and so we must be able to recover the user's password in order
        /// to verfify the WSSE diget.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Decrypt(string text, string key)
        {
            // In theory it could be an empty string
            if (text == null)
                throw new ArgumentNullException("text");
            byte[] bytes = SymmetricDecrypt(ConvertEx.FromBase16String(text), ConvertEx.FromBase16String(key), ConvertEx.FromBase16String(s_hexIv));
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Helper method to encrypt a byte array using AES. (Albahari C# 3.0 In a Nutshell)
        /// </summary>
        /// <param name="clearBytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] SymmetricEncrypt(byte[] clearBytes, byte[] key, byte[] iv)
        {
            if (clearBytes == null)
                throw new ArgumentNullException("clearBytes");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");
            // Aes 
            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateEncryptor(key, iv))
                return SymmetricTransform(clearBytes, key, iv, decryptor);
        }

        /// <summary>
        /// Helper method to decrypt a byte array using using AES. (Albahari C# 3.0 In a Nutshell)
        /// </summary>
        /// <param name="cipherBytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] SymmetricDecrypt(byte[] cipherBytes, byte[] key, byte[] iv)
        {
            if (cipherBytes == null)
                throw new ArgumentNullException("cipherBytes");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");
            // Aes 
            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
                return SymmetricTransform(cipherBytes, key, iv, decryptor);
        }
#endif

        /// <summary>
        /// Helper method to perform crypto transorm to a memory stream.  (Albahari C# 3.0 In a Nutshell)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="transformer"></param>
        /// <returns></returns>
        private static byte[] SymmetricTransform(byte[] data, byte[] key, byte[] iv, ICryptoTransform transformer)
        {
            var ms = new MemoryStream();
            using (Stream c = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                c.Write(data, 0, data.Length);
            return ms.ToArray();
        }

        /// <summary>
        /// Generates the symmetric key.
        /// </summary>
        /// <returns></returns>
        public static string GenerateSymmetricKey()
        {
            // Creates the default implementation, which is RijndaelManaged.  
            var algorithm = SymmetricAlgorithm.Create();
            algorithm.GenerateKey();
            return ConvertEx.ToBase16String(algorithm.Key);
        }

        /// <summary>
        /// Generates the symmetric iv.
        /// </summary>
        /// <returns></returns>
        public static string GenerateSymmetricIv()
        {
            // Creates the default implementation, which is RijndaelManaged.  
            var algorithm = SymmetricAlgorithm.Create();
            algorithm.GenerateIV();
            return ConvertEx.ToBase16String(algorithm.IV);
        }
    }
}
