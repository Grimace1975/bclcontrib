//using System;
//using System.Collections.Generic;
//using System.Threading;
//namespace Instinct.Html
//{
//    /// <summary>
//    /// HtmlFactory class
//    /// </summary>
//
//    public class HtmlFactory : Pattern.Generic.Singleton<HtmlFactory>
//    {
//        private static HtmlTextWriterIndex s_htmlTextWriterIndex = new HtmlTextWriterIndex();

//        /// <summary>
//        /// Initializes a new instance of the <see cref="HtmlFactory"/> class.
//        /// </summary>
//        protected HtmlFactory()
//        {
//        }

//        #region CLASSFACTORY
//        /// <summary>
//        /// Creates the HTML builder.
//        /// </summary>
//        /// <param name="writer">The writer.</param>
//        /// <returns></returns>
//        public virtual HtmlBuilder CreateHtmlBuilder(HtmlTextWriter writer)
//        {
//            return null; // new HtmlBuilder(writer);
//        }

//        /// <summary>
//        /// Creates the state of the HTML builder div.
//        /// </summary>
//        /// <param name="htmlBuilder">The HTML builder.</param>
//        /// <param name="attrib">The attrib.</param>
//        /// <returns></returns>
//        public virtual HtmlBuilderDivState CreateHtmlBuilderDivState(HtmlBuilder htmlBuilder, Attrib attrib)
//        {
//            return null; // new HtmlBuilderDivState(htmlBuilder, attrib);
//        }

//        ///// <summary>
//        ///// Creates the state of the HTML builder form.
//        ///// </summary>
//        ///// <param name="htmlBuilder">The HTML builder.</param>
//        ///// <param name="dataState">State of the data.</param>
//        ///// <param name="frame">The frame.</param>
//        ///// <param name="attrib">The attrib.</param>
//        ///// <returns></returns>
//        //public virtual HtmlBuilderFormState CreateHtmlBuilderFormState(HtmlBuilder htmlBuilder, DataState dataState, IHtmlContract frame, Attrib attrib)
//        //{
//        //    return null; // new HtmlBuilderFormState(htmlBuilder, dataState, frame, attrib);
//        //}

//        /// <summary>
//        /// Creates the state of the HTML builder table.
//        /// </summary>
//        /// <param name="htmlBuilder">The HTML builder.</param>
//        /// <param name="attrib">The attrib.</param>
//        /// <returns></returns>
//        public virtual HtmlBuilderTableState CreateHtmlBuilderTableState(HtmlBuilder htmlBuilder, Attrib attrib)
//        {
//            return null; // new HtmlBuilderTableState(htmlBuilder, attrib);
//        }

//        /// <summary>
//        /// Creates the HTML text writer.
//        /// </summary>
//        /// <param name="writer">The writer.</param>
//        /// <returns></returns>
//        public virtual HtmlTextWriter CreateHtmlTextWriter(System.Web.UI.HtmlTextWriter writer)
//        {
//            return new HtmlTextWriter(writer);
//        }
//        #endregion CLASSFACTORY

//        #region HTMLTEXTWRITER
//        /// <summary>
//        /// Gets the HtmlTextWriter instance in use.
//        /// </summary>
//        public static Instinct.Collections.Indexer<System.Web.UI.HtmlTextWriter, HtmlTextWriter> HtmlTextWriter
//        {
//            get { return s_htmlTextWriterIndex; }
//        }
//        /// <summary>
//        /// HtmlTextWriterIndex class
//        /// </summary>
//        private sealed class HtmlTextWriterIndex : Instinct.Collections.Indexer<System.Web.UI.HtmlTextWriter, HtmlTextWriter>
//        {
//            private static ReaderWriterLockSlim s_rwLock = new ReaderWriterLockSlim();
//            private Dictionary<System.Web.UI.HtmlTextWriter, HtmlTextWriter> _hash = new Dictionary<System.Web.UI.HtmlTextWriter, HtmlTextWriter>();

//            /// <summary>
//            /// Initializes a new instance of the <see cref="HtmlTextWriterIndex"/> class.
//            /// </summary>
//            public HtmlTextWriterIndex()
//                : base()
//            {
//            }

//            /// <summary>
//            /// Gets or sets the <see cref="Instinct.Html.HtmlTextWriter"/> with the specified key.
//            /// </summary>
//            /// <value></value>
//            public override HtmlTextWriter this[System.Web.UI.HtmlTextWriter key]
//            {
//                get
//                {
//                    if (key is HtmlTextWriter)
//                    {
//                        throw new ArgumentException(Http_.Local.AlreadyHtmlTextWriter, "key");
//                    }
//                    return s_rwLock.ThreadedGetWithCreate<HtmlTextWriter, System.Web.UI.HtmlTextWriter, Dictionary<System.Web.UI.HtmlTextWriter, HtmlTextWriter>>(_hash, key, delegate(System.Web.UI.HtmlTextWriter key2)
//                    {
//                        return Instance.CreateHtmlTextWriter(key2);
//                    });
//                }
//                set
//                {
//                    throw new NotSupportedException();
//                }
//            }

//            /// <summary>
//            /// Removes the specified key.
//            /// </summary>
//            /// <param name="key">The key.</param>
//            public void Remove(System.Web.UI.HtmlTextWriter key)
//            {
//                s_rwLock.ThreadedRemove<HtmlTextWriter, System.Web.UI.HtmlTextWriter, Dictionary<System.Web.UI.HtmlTextWriter, HtmlTextWriter>>(_hash, key);
//            }
//        }

//        /// <summary>
//        /// Removes the HTML text writer.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        public static void RemoveHtmlTextWriter(System.Web.UI.HtmlTextWriter key)
//        {
//            s_htmlTextWriterIndex.Remove(key);
//        }
//        #endregion HTMLTEXTWRITER
//    }
//}