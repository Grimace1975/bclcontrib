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
//using System.Collections.Generic;
////[assembly: Instinct.Pattern.Environment.Attribute.FactoryConfiguration("smartForm", typeof(System.Patterns.IContract))]
//namespace System.Patterns.Forms
//{
//    /// <summary>
//    /// Represents a advanced form processing type that maintains state, processing functionality, and TextColumn/Element support.
//    /// </summary>
//
//    public class SmartForm
//    {
//        private Dictionary<string, string> _values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
//        private Dictionary<string, string> _replaceTags = new Dictionary<string, string>();

//        //#region Class Types
//        ///// <summary>
//        ///// Contract
//        ///// </summary>
//        //public class Contract : Patterns.Generic.SingletonFactoryWithCreate<Patterns.IContract>
//        //{
//        //    /// <summary>
//        //    /// Creates the specified key.
//        //    /// </summary>
//        //    /// <param name="key">The key.</param>
//        //    /// <returns></returns>
//        //    protected static Patterns.IContract Create(string key)
//        //    {
//        //        return InversionEx.Resolve<Patterns.IContract>(ResolveLifetime.ApplicationUnit, key, new { typeA = "System.Primitives.SmartFormContracts.{0}SmartFormContract, " + AssemblyRef.This });
//        //    }
//        //}
//        //#endregion

//        /// <summary>
//        /// Gets or sets the <see cref="System.String"/> value associated with the specified key.
//        /// </summary>
//        /// <value></value>
//        public string this[string key]
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(key))
//                    throw new ArgumentNullException("key");
//                string value;
//                return (_values.TryGetValue(key, out value) ? value : string.Empty);
//            }
//            set
//            {
//                if (string.IsNullOrEmpty(key))
//                    throw new ArgumentNullException("key");
//                _values[key] = (value ?? string.Empty);
//            }
//        }

//        ///// <summary>
//        ///// Executes the contract.
//        ///// </summary>
//        ///// <param name="key">The key.</param>
//        ///// <param name="method">The method.</param>
//        ///// <param name="parameterArray">The parameter array.</param>
//        ///// <returns></returns>
//        //public object ExecuteContract(string key, string method, params object[] args)
//        //{
//        //    object[] array;
//        //    if ((args != null) && (args.Length > 0))
//        //    {
//        //        array = new object[args.Length + 1];
//        //        args.CopyTo(array, 1);
//        //        array[0] = this;
//        //    }
//        //    else
//        //        array = new[] { this };
//        //    return Contract.Get(key).Execute(method, array);
//        //}

//        /// <summary>
//        /// Gets or sets a value indicating whether this instance is error.
//        /// </summary>
//        /// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
//        public bool HasError { get; set; }

//        public Dictionary<string, string> Values
//        {
//            get { return _values; }
//        }

//        public Dictionary<string, string> ReplaceTags
//        {
//            get { return _replaceTags; }
//        }

//        ///// <summary>
//        ///// Gets the meta.
//        ///// </summary>
//        ///// <value>The meta.</value>
//        //public SmartFormMeta Meta { get; protected set; }

//        /// <summary>
//        /// Creates the merged text resulting from applying all replacement tags against the string value associated with the key provided.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns></returns>
//        public string CreateMergedText(string key)
//        {
//            if (string.IsNullOrEmpty(key))
//                throw new ArgumentNullException("key");
//            string value;
//            if (!_values.TryGetValue(key, out value))
//                return string.Empty;
//            if (value.Length > 0)
//                foreach (string replaceTagKey in _replaceTags.Keys)
//                    value = value.Replace("[:" + replaceTagKey + ":]", _replaceTags[replaceTagKey]);
//            return value;
//        }
//    }
//}
