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
//using System.Configuration;
//using System.Security.Cryptography;
//using System.IO;
//using System.Text;
//namespace System.Security
//{
//    /// <summary>
//    /// Crypt class
//    /// </summary>
//    public class Crypt
//    {
//        private static readonly byte[] s_dllKey = Convert.FromBase64String("");
//        private static readonly byte[] s_dllIv = Convert.FromBase64String("");
//        private const int IvSize = 16;
//        private byte[] _systemIv;
//        private byte[] _systemKey;
//        private byte[] _publicIv;
//        private byte[] _publicKey;
//        private string _configPath;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="Crypt"/> class.
//        /// </summary>
//        public Crypt()
//            : this("") { }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="Crypt"/> class.
//        /// </summary>
//        /// <param name="configPath">The config path.</param>
//        public Crypt(string configPath)
//        {
//            ConfigPath = configPath;
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="Crypt"/> class.
//        /// </summary>
//        /// <param name="configPath">The config path.</param>
//        /// <param name="publicKey">The public key.</param>
//        public Crypt(string configPath, string publicKey)
//        {
//            ConfigPath = configPath;
//            DecodeKey(_systemKey, _systemIv, publicKey, out _publicKey, out _publicIv);
//        }

//        /// <summary>
//        /// Gets the random byte array.
//        /// </summary>
//        /// <param name="length">The length.</param>
//        /// <returns></returns>
//        public byte[] GetRandomByteArray(int length)
//        {
//            byte[] byteArray = new byte[length];
//            new RNGCryptoServiceProvider().GetBytes(byteArray);
//            return byteArray;
//        }

//        #region Decode
//        /// <summary>
//        /// Decodes the specified text.
//        /// </summary>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Decode(string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return null;
//            if ((_publicKey == null) || (_publicIv == null))
//                throw new InvalidOperationException(CoreEx_.Local.UndefinedCryptPublicKey);
//            return DecodeStream(_publicKey, _publicIv, text);
//        }
//        /// <summary>
//        /// Decodes the specified public key.
//        /// </summary>
//        /// <param name="publicKey">The public key.</param>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Decode(string publicKey, string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return null;
//            if (string.IsNullOrEmpty(publicKey))
//                throw new ArgumentNullException("publicKey", CoreEx_.Local.UndefinedCryptPublicKey);
//            byte[] key;
//            byte[] iv;
//            DecodeKey(_systemKey, _systemIv, publicKey, out key, out iv);
//            return DecodeStream(key, iv, text);
//        }
//        /// <summary>
//        /// Decodes the specified public key.
//        /// </summary>
//        /// <param name="publicKey">The public key.</param>
//        /// <param name="publicIv">The public iv.</param>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Decode(byte[] publicKey, byte[] publicIv, string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return null;
//            if (publicKey == null)
//                throw new ArgumentNullException("publicKey");
//            if (publicIv == null)
//                throw new ArgumentNullException("publicIv");
//            return DecodeStream(publicKey, publicIv, text);
//        }

//        //public void DecodeKey(string publicKey, out byte[] key, out byte[] iv) {
//        //   DecodeKey(m_systemKey, m_systemIv, publicKey, out key, out iv);
//        //}
//        //private void DecodeKey(string systemKey, string systemIv, string publicKey, out byte[] key, out byte[] iv) {
//        //   DecodeKey(System.Convert.FromBase64String(systemKey), System.Convert.FromBase64String(systemIv), publicKey, out key, out iv);
//        //}
//        /// <summary>
//        /// Decodes the key.
//        /// </summary>
//        /// <param name="systemKey">The system key.</param>
//        /// <param name="systemIv">The system iv.</param>
//        /// <param name="publicKey">The public key.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="iv">The iv.</param>
//        private void DecodeKey(byte[] systemKey, byte[] systemIv, string publicKey, out byte[] key, out byte[] iv)
//        {
//            if (string.IsNullOrEmpty(publicKey))
//                throw new ArgumentNullException("publicKey", CoreEx_.Local.UndefinedCryptPublicKey);
//            byte[] array = Convert.FromBase64String(DecodeStream(systemKey, systemIv, publicKey));
//            if (array.Length < IvSize + 1)
//                throw new ArgumentException(CoreEx_.Local.InvalidCryptPublicKey, "publicKey");
//            iv = new byte[IvSize];
//            Buffer.BlockCopy(array, 0, iv, 0, IvSize);
//            key = new byte[array.Length - IvSize];
//            Buffer.BlockCopy(array, IvSize, key, 0, key.Length);
//        }
//        //public string DecodeKey(string publicKey) {
//        //   return DecodeKey(m_systemKey, m_systemIv, publicKey);
//        //}
//        //private string DecodeKey(string systemKey, string systemIv, string publicKey) {
//        //   return DecodeKey(System.Convert.FromBase64String(systemKey), System.Convert.FromBase64String(systemIv), publicKey);
//        //}
//        //private string DecodeKey(byte[] systemKey, byte[] systemIv, string publicKey) {
//        //   return DecodeStream(systemKey, systemIv, publicKey);
//        //}

//        /// <summary>
//        /// Decodes the stream.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <param name="iv">The iv.</param>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        private string DecodeStream(byte[] key, byte[] iv, string text)
//        {
//            // setup cryptography environment
//            var decryptor = new RijndaelManaged();
//            decryptor.Key = key;
//            decryptor.IV = iv;
//            // decode
//            var decodedDataStream = new MemoryStream();
//            var cryptoStream = new CryptoStream(decodedDataStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write);
//            byte[] decodingDataArray = Convert.FromBase64String(text);
//            cryptoStream.Write(decodingDataArray, 0, decodingDataArray.Length);
//            cryptoStream.Close();
//            byte[] rawDecodedDataArray = decodedDataStream.ToArray();
//            decodedDataStream.Close();
//            // extract decoded data
//            byte[] decodedDataArray = new byte[rawDecodedDataArray.Length - IvSize];
//            Buffer.BlockCopy(rawDecodedDataArray, IvSize, decodedDataArray, 0, decodedDataArray.Length);
//            return Encoding.Unicode.GetString(decodedDataArray);
//        }
//        #endregion

//        #region Encode
//        /// <summary>
//        /// Encodes the specified text.
//        /// </summary>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Encode(string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return null;
//            if ((_publicKey == null) || (_publicIv == null))
//                throw new InvalidOperationException(CoreEx_.Local.UndefinedCryptPublicKey);
//            return EncodeStream(_publicKey, _publicIv, text);
//        }
//        /// <summary>
//        /// Encodes the specified public key.
//        /// </summary>
//        /// <param name="publicKey">The public key.</param>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Encode(string publicKey, string text)
//        {
//            if (string.IsNullOrEmpty(text))
//                return null;
//            byte[] key;
//            byte[] iv;
//            DecodeKey(_systemKey, _systemIv, publicKey, out key, out iv);
//            return EncodeStream(key, iv, text);
//        }
//        /// <summary>
//        /// Encodes the specified public key.
//        /// </summary>
//        /// <param name="publicKey">The public key.</param>
//        /// <param name="publicIv">The public iv.</param>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        public string Encode(byte[] publicKey, byte[] publicIv, string text)
//        {
//            return (!string.IsNullOrEmpty(text) ? EncodeStream(publicKey, publicIv, text) : null);
//        }

//        //public string EncodeKey(string privateKey) {
//        //   return EncodeStream(m_systemKey, m_systemIv, privateKey);
//        //}
//        //public string EncodeKey(string systemKey, string systemIv, string privateKey) {
//        //   return EncodeStream(System.Convert.FromBase64String(systemKey), System.Convert.FromBase64String(systemIv), privateKey);
//        //}
//        //private string EncodeKey(byte[] systemKey, byte[] systemIv, string privateKey) {
//        //   return EncodeStream(systemKey, systemIv, privateKey);
//        //}

//        /// <summary>
//        /// Encodes the stream.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <param name="iv">The iv.</param>
//        /// <param name="text">The text.</param>
//        /// <returns></returns>
//        private string EncodeStream(byte[] key, byte[] iv, string text)
//        {
//            // setup cryptography environment
//            var encryptor = new RijndaelManaged();
//            encryptor.Key = key;
//            encryptor.IV = iv;
//            // encode
//            var encodedDataStream = new MemoryStream();
//            var cryptoStream = new CryptoStream(encodedDataStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
//            byte[] encodingDataArray = Encoding.Unicode.GetBytes(text);
//            byte[] rawEncodingByteArray = new byte[IvSize + encodingDataArray.Length];
//            Buffer.BlockCopy(GetRandomByteArray(IvSize), 0, rawEncodingByteArray, 0, IvSize);
//            Buffer.BlockCopy(encodingDataArray, 0, rawEncodingByteArray, IvSize, encodingDataArray.Length);
//            cryptoStream.Write(rawEncodingByteArray, 0, rawEncodingByteArray.Length);
//            cryptoStream.Close();
//            return Convert.ToBase64String(encodedDataStream.ToArray());
//        }
//        #endregion

//        #region Key
//        /// <summary>
//        /// Gets or sets the config path.
//        /// </summary>
//        /// <value>The config path.</value>
//        public string ConfigPath
//        {
//            get { return _configPath; }
//            set
//            {
//                if (string.IsNullOrEmpty(value))
//                    throw new ArgumentNullException("value");
//                string systemPublicKey;
//                string[] values = value.Split(new char[] { ':' }, 2);
//                switch (values[0].ToLowerInvariant())
//                {
//#if !SqlServer
//                    case "config":
//                        if ((values.Length > 1) && ((systemPublicKey = ConfigurationManager.AppSettings[values[1]]) != null) && (systemPublicKey.Length > 0))
//                        {
//                            DecodeKey(s_dllKey, s_dllIv, systemPublicKey, out _systemKey, out _systemIv);
//                            _configPath = "Config";
//                        }
//                        else
//                            throw new ArgumentException(string.Format(CoreEx_.Local.UndefinedCryptPathConfigAB, value, (values.Length > 1 ? values[1] : string.Empty)), "value");
//                        break;
//#endif
//                    case "registry":
//                        Microsoft.Win32.RegistryKey configRegistryKey;
//                        string configRegistryPath = string.Empty;
//                        if ((values.Length <= 1) || ((configRegistryPath = values[1].ToLowerInvariant()).Length < 6) || (configRegistryPath[4] != '\\') || (!"|hkcu|hklm|".Contains("|" + configRegistryPath.Substring(0, 4) + "|")))
//                            throw new ArgumentException(string.Format(CoreEx_.Local.UndefinedCryptPathRegistryAB, value, configRegistryPath), "value");
//                        switch (configRegistryPath.Substring(0, 4))
//                        {
//                            case "hkcu":
//                                configRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(configRegistryPath.Substring(5));
//                                break;
//                            case "hklm":
//                                configRegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(configRegistryPath.Substring(5));
//                                break;
//                            default:
//                                configRegistryKey = null;
//                                break;
//                        }
//                        if ((configRegistryKey != null) && ((systemPublicKey = (configRegistryKey.GetValue("SystemCryptKey") as string)) != null) && (systemPublicKey.Length > 0))
//                        {
//                            DecodeKey(s_dllKey, s_dllIv, systemPublicKey, out _systemKey, out _systemIv);
//                            _configPath = "Registry";
//                        }
//                        else
//                            throw new ArgumentException(string.Format(CoreEx_.Local.UndefinedCryptPathRegistryAB, value, (values.Length > 1 ? values[1] : string.Empty)), "value");
//                        break;
//                    case "value":
//                        if ((values.Length > 1) && ((systemPublicKey = values[1]) != null) && (systemPublicKey.Length > 0))
//                        {
//                            DecodeKey(s_dllKey, s_dllIv, systemPublicKey, out _systemKey, out _systemIv);
//                            _configPath = "Value";
//                        }
//                        else
//                            throw new ArgumentException(string.Format(CoreEx_.Local.UndefinedCryptPathValueAB, value, (values.Length > 1 ? values[1] : string.Empty)), "value");
//                        break;
//                    case "instinct":
//                        _systemKey = s_dllKey;
//                        _systemIv = s_dllIv;
//                        _configPath = "Instinct";
//                        break;
//                    default:
//                        throw new ArgumentException(string.Format(CoreEx_.Local.UndefinedCryptPathA, value), "value");
//                }
//            }
//        }

//        /// <summary>
//        /// Sets the public key.
//        /// </summary>
//        /// <param name="value">The value.</param>
//        public void SetPublicKey(string value)
//        {
//            DecodeKey(_systemKey, _systemIv, value, out _publicKey, out _publicIv);
//        }
//        #endregion
//    }
//}
