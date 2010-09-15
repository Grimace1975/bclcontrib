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
        //private static readonly byte[] s_genericIv = ConvertEx.FromBase16String("C9DCF37AED8574A1441FD82DB743765C");

        public static byte[] ComputeHash(byte[] data) { using (var algorithm2 = new SHA1Managed()) return ComputeHash(algorithm2, data); }
        public static byte[] ComputeHash(string algorithm, byte[] data) { using (var algorithm2 = (HashAlgorithm)CryptoConfig.CreateFromName(algorithm)) return ComputeHash(algorithm2, data); }
        public static byte[] ComputeHash(HashAlgorithm algorithm, byte[] data)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            return algorithm.ComputeHash(data);
        }

        public static byte[] SymmetricTransform(byte[] data, ICryptoTransform transformer)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (transformer == null)
                throw new ArgumentNullException("transformer");
            using (var ms = new MemoryStream())
            {
                using (var s = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                    s.Write(data, 0, data.Length);
                return ms.ToArray();
            }
        }

        #region Encrypt

        public static string SymmetricEncrypt(SymmetricAlgorithm algorithm, string data) { return Convert.ToBase64String(SymmetricEncrypt(algorithm, Encoding.UTF8.GetBytes(data))); }
        public static byte[] SymmetricEncrypt(SymmetricAlgorithm algorithm, byte[] data)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            using (var encryptor = algorithm.CreateEncryptor())
                return SymmetricTransform(data, encryptor);
        }
        public static string SymmetricEncrypt(SymmetricAlgorithm algorithm, string data, int ivSize) { return Convert.ToBase64String(SymmetricEncrypt(algorithm, Encoding.UTF8.GetBytes(data), ivSize)); }
        public static byte[] SymmetricEncrypt(SymmetricAlgorithm algorithm, byte[] data, int ivSize)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (ivSize < 0)
                throw new ArgumentOutOfRangeException("ivSize");
            if (data == null)
                throw new ArgumentNullException("data");
            using (var encryptor = algorithm.CreateEncryptor())
                return SymmetricEncryptTransform(data, ivSize, encryptor);
        }
        public static string SymmetricEncrypt(string data, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return Convert.ToBase64String(SymmetricEncrypt(algorithm, Encoding.UTF8.GetBytes(data), key, iv)); }
        public static string SymmetricEncrypt(SymmetricAlgorithm algorithm, string data, byte[] key, byte[] iv) { return Convert.ToBase64String(SymmetricEncrypt(algorithm, Encoding.UTF8.GetBytes(data), key, iv)); }
        public static byte[] SymmetricEncrypt(byte[] data, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return SymmetricEncrypt(algorithm, data, key, iv); }
        public static byte[] SymmetricEncrypt(SymmetricAlgorithm algorithm, byte[] data, byte[] key, byte[] iv)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");
            using (var encryptor = algorithm.CreateEncryptor(key, iv))
                return SymmetricTransform(data, encryptor);
        }
        public static string SymmetricEncrypt(string data, int ivSize, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return Convert.ToBase64String(SymmetricEncrypt(algorithm, Encoding.UTF8.GetBytes(data), ivSize, key, iv)); }
        public static string SymmetricEncrypt(SymmetricAlgorithm algorithm, string data, int ivSize, byte[] key, byte[] iv) { return Convert.ToBase64String(SymmetricEncrypt(algorithm, Encoding.UTF8.GetBytes(data), ivSize, key, iv)); }
        public static byte[] SymmetricEncrypt(byte[] data, int ivSize, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return SymmetricEncrypt(algorithm, data, ivSize, key, iv); }
        public static byte[] SymmetricEncrypt(SymmetricAlgorithm algorithm, byte[] data, int ivSize, byte[] key, byte[] iv)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (ivSize < 0)
                throw new ArgumentOutOfRangeException("ivSize");
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");
            using (var encryptor = algorithm.CreateEncryptor(key, iv))
                return SymmetricEncryptTransform(data, ivSize, encryptor);
        }

        public static byte[] SymmetricEncryptTransform(byte[] data, int ivSize, ICryptoTransform transformer)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (ivSize < 0)
                throw new ArgumentOutOfRangeException("ivSize");
            if (transformer == null)
                throw new ArgumentNullException("transformer");
            using (var ms = new MemoryStream())
            {
                using (var s = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                {
                    byte[] data2;
                    if (ivSize > 0)
                    {
                        data2 = new byte[ivSize + data.Length];
                        Buffer.BlockCopy(MakeRandomBytes(ivSize), 0, data2, 0, ivSize);
                        Buffer.BlockCopy(data, 0, data2, ivSize, data.Length);
                    }
                    else
                        data2 = data;
                    s.Write(data2, 0, data2.Length);
                }
                return ms.ToArray();
            }
        }

        #endregion

        #region Decrypt

        public static string SymmetricDecrypt(SymmetricAlgorithm algorithm, string data) { return Convert.ToBase64String(SymmetricDecrypt(algorithm, Encoding.UTF8.GetBytes(data))); }
        public static byte[] SymmetricDecrypt(SymmetricAlgorithm algorithm, byte[] data)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            using (var decryptor = algorithm.CreateDecryptor())
                return SymmetricTransform(data, decryptor);
        }
        public static string SymmetricDecrypt(SymmetricAlgorithm algorithm, string data, int ivSize) { return Convert.ToBase64String(SymmetricDecrypt(algorithm, Encoding.UTF8.GetBytes(data), ivSize)); }
        public static byte[] SymmetricDecrypt(SymmetricAlgorithm algorithm, byte[] data, int ivSize)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            if (ivSize < 0)
                throw new ArgumentOutOfRangeException("ivSize");
            using (var decryptor = algorithm.CreateDecryptor())
                return SymmetricDecryptTransform(data, ivSize, decryptor);
        }
        public static string SymmetricDecrypt(string data, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return Convert.ToBase64String(SymmetricDecrypt(algorithm, Encoding.UTF8.GetBytes(data), key, iv)); }
        public static string SymmetricDecrypt(SymmetricAlgorithm algorithm, string data, byte[] key, byte[] iv) { return Convert.ToBase64String(SymmetricDecrypt(algorithm, Encoding.UTF8.GetBytes(data), key, iv)); }
        public static byte[] SymmetricDecrypt(byte[] data, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return SymmetricDecrypt(algorithm, data, key, iv); }
        public static byte[] SymmetricDecrypt(SymmetricAlgorithm algorithm, byte[] data, byte[] key, byte[] iv)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");
            using (var decryptor = algorithm.CreateDecryptor(key, iv))
                return SymmetricTransform(data, decryptor);
        }
        public static string SymmetricDecrypt(string data, int ivSize, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return Convert.ToBase64String(SymmetricDecrypt(algorithm, Encoding.UTF8.GetBytes(data), ivSize, key, iv)); }
        public static string SymmetricDecrypt(SymmetricAlgorithm algorithm, string data, int ivSize, byte[] key, byte[] iv) { return Convert.ToBase64String(SymmetricDecrypt(algorithm, Encoding.UTF8.GetBytes(data), ivSize, key, iv)); }
        public static byte[] SymmetricDecrypt(byte[] data, int ivSize, byte[] key, byte[] iv) { using (var algorithm = SymmetricAlgorithm.Create()) return SymmetricDecrypt(algorithm, data, ivSize, key, iv); }
        public static byte[] SymmetricDecrypt(SymmetricAlgorithm algorithm, byte[] data, int ivSize, byte[] key, byte[] iv)
        {
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");
            if (data == null)
                throw new ArgumentNullException("data");
            if (ivSize < 0)
                throw new ArgumentOutOfRangeException("ivSize");
            if (key == null)
                throw new ArgumentNullException("key");
            if (iv == null)
                throw new ArgumentNullException("iv");
            using (var decryptor = algorithm.CreateDecryptor(key, iv))
                return SymmetricDecryptTransform(data, ivSize, decryptor);
        }

        public static byte[] SymmetricDecryptTransform(byte[] data, int ivSize, ICryptoTransform transformer)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (ivSize < 0)
                throw new ArgumentOutOfRangeException("ivSize");
            if (transformer == null)
                throw new ArgumentNullException("transformer");
            byte[] transformedData;
            using (var ms = new MemoryStream())
            {
                using (var s = new CryptoStream(ms, transformer, CryptoStreamMode.Write))
                    s.Write(data, 0, data.Length);
                transformedData = ms.ToArray();
            }
            if (ivSize > 0)
            {
                var data2 = new byte[transformedData.Length - ivSize];
                Buffer.BlockCopy(transformedData, ivSize, data2, 0, data2.Length);
                transformedData = data2;
            }
            return transformedData;
        }

        #endregion

        #region Utility

        public static byte[] MakeRandomBytes(int length)
        {
            var bytes = new byte[length];
            RNGCryptoServiceProvider.Create().GetBytes(bytes);
            return bytes;
        }

        public static byte[] GenerateSymmetricKey() { using (var algorithm = SymmetricAlgorithm.Create()) return GenerateSymmetricKey(algorithm); }
        public static byte[] GenerateSymmetricKey(SymmetricAlgorithm algorithm)
        {
            algorithm.GenerateKey();
            return algorithm.Key;
        }

        public static byte[] GenerateSymmetricIv() { using (var algorithm = SymmetricAlgorithm.Create()) return GenerateSymmetricIv(algorithm); }
        public static byte[] GenerateSymmetricIv(SymmetricAlgorithm algorithm)
        {
            algorithm.GenerateIV();
            return algorithm.IV;
        }

        #endregion
    }
}
