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
//namespace System.Patterns.Caching
//{
//    //: might need to make thread safe
//    /// <summary>
//    /// Provides the core factory method mechanism for generating or accessing a singleton-based Cache Provider.
//    /// </summary>
//
//    public class CoreEnvironmentCacheProvider : StaticCacheProvider
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="KernelCacheProvider"/> class.
//        /// </summary>
//        /// <param name="isEnableCaching">if set to <c>true</c> [is enable caching].</param>
//        internal CoreEnvironmentCacheProvider(bool enableCaching)
//            : this()
//        {
//            //if (!enableCaching)
//            //    TransactionContext.Created += new EventHandler(TransactionContext_Created);
//        }
//        /// <summary>
//        /// Initializes a new instance of the <see cref="CoreEnvironmentCacheProvider"/> class.
//        /// </summary>
//        public CoreEnvironmentCacheProvider()
//            : base() { }

//        /// <summary>
//        /// Handles the StartupStarted event of the EnvironmentBase control.
//        /// </summary>
//        /// <param name="sender">The source of the event.</param>
//        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
//        private static void TransactionContext_Created(object sender, EventArgs e)
//        {
//            Hash.Clear();
//        }
//    }
//}
