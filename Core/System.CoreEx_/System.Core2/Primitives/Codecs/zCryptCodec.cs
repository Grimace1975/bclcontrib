//#region License
///*
//The MIT License

//Copyright (c) 2008 Sky Morey

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.
//*/
//#endregion
//namespace System.Primitives.Codecs
//{
//    /// <summary>
//    /// Provides standardized encode/decode functionality to/from an encrypted format.
//    /// </summary>
//    public class CryptCodec : ICodec
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="CryptCodec"/> class.
//        /// </summary>
//        public CryptCodec()
//        {
//            //Configuration.KernelApplicationUnitConfiguration applicationUnitConfig;
//            //if ((KernelFactory.s_applicationUnitConfigHash.Count == 0) || ((applicationUnitConfig = KernelFactory.s_applicationUnitConfigHash[Kernel.ApplicationUnit]) == null))
//            //{
//            //    throw new InvalidOperationException(string.Format(Kernel.Local.UndefinedApplicationUnitA, Kernel.ApplicationUnit));
//            //}
//            //Crypt = new Crypt(applicationUnitConfig.CryptConfigPath, applicationUnitConfig.CryptPublicKey);
//        }

//        ///// <summary>
//        ///// Gets the crypt.
//        ///// </summary>
//        ///// <value>The crypt.</value>
//        //public Crypt Crypt { get; private set; }

//        /// <summary>
//        /// Abstract member whose implementation by the derived class decodes a string into another string.
//        /// </summary>
//        /// <param name="text">String to decode.</param>
//        /// <returns>
//        /// Returns results of decoding <paramref name="text"/> object instance.
//        /// </returns>
//        public static string Decode(string text)
//        {
//            throw new NotImplementedException();
//            //return Crypt.Decode(text);
//        }

//        /// <summary>
//        /// Abstract member whose implementation by the derived class encodes a string into another string.
//        /// </summary>
//        /// <param name="text">String to decode.</param>
//        /// <returns>
//        /// Returns results of decoding <paramref name="text"/> object instance.
//        /// </returns>
//        public static string Encode(string text)
//        {
//            throw new NotImplementedException();
//            //return Crypt.Encode(text);
//        }

//        /// <summary>
//        /// Abstract member whose implementation by the derived class decodes a string into another string.
//        /// </summary>
//        /// <param name="text">String to decode.</param>
//        /// <returns>
//        /// Returns results of decoding <paramref name="text"/> object instance.
//        /// </returns>
//        string ICodec.Decode(string text) { return Decode(text); }

//        /// <summary>
//        /// Abstract member whose implementation by the derived class encodes a string into another string.
//        /// </summary>
//        /// <param name="text">String to decode.</param>
//        /// <returns>
//        /// Returns results of decoding <paramref name="text"/> object instance.
//        /// </returns>
//        string ICodec.Encode(string text) { return Encode(text); }
//    }
//}