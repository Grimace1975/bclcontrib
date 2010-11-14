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
//        //        private FrameIndex m_frameIndex = new FrameIndex();


//        //        public CollectionIndexBase<string, IHtmlContract> Frame
//        //        {
//        //            get
//        //            {
//        //                return m_frameIndex;
//        //            }
//        //        }
//        //        private class FrameIndex : Index.DictionaryCollectionIndex<string, IHtmlContract>
//        //        {
//        //            public FrameIndex()
//        //                : base(new System.Collections.Generic.Dictionary<string, IHtmlContract>())
//        //            {
//        //            }

//        //            /// <summary>
//        //            /// Gets or sets the value (of type TValue) associated with the specified key.
//        //            /// </summary>
//        //            /// <value></value>
//        //            public override IHtmlContract this[string key]
//        //            {
//        //                get
//        //                {
//        //                    if (key == null)
//        //                    {
//        //                        throw new ArgumentNullException("key");
//        //                    }
//        //                    IHtmlContract value;
//        //                    if (m_hash.TryGetValue(key, out value) == false)
//        //                    {
//        //                        //+ create
//        //                        value = HttpFactory.Frame[key];
//        //                        m_hash.Add(key, value);
//        //                    }
//        //                    return value;
//        //                }
//        //                set
//        //                {
//        //                    throw new InvalidOperationException();
//        //                }
//        //            }
//        //        }

//        //        public void FrameField(string key, params string[] parameterArray)
//        //        {
//        //            FrameField(key, ((parameterArray != null) && (parameterArray.Length > 0) ? new Attrib(parameterArray) : null));
//        //        }
//        //        public void FrameField(string key, Attrib attrib)
//        //        {
//        //            throw new NotImplementedException();
//        //            //	m_oFormFrame.Field(this, cKey, vAttrib);
//        //            //m_formDataChannel[key]
//        //        }

//        //        public void FrameContract(string method, params object[] parameterArray)
//        //        {
//        //            if (method == null)
//        //            {
//        //                throw new ArgumentNullException("method");
//        //            }
//        //            int scopeIndex = method.IndexOf(KernelText.Scope);
//        //            string frameKey;
//        //            if (scopeIndex > -1)
//        //            {
//        //                frameKey = method.Substring(0, scopeIndex);
//        //                method = method.Substring(scopeIndex + 2);
//        //            }
//        //            else
//        //            {
//        //                frameKey = string.Empty;
//        //            }
//        //            if (frameKey.Length > 0)
//        //            {
//        //                m_frameIndex[frameKey].Execute(this, method, parameterArray);
//        //            }
//        //            else
//        //            {
//        //                IHtmlContract frame = m_formState.Frame;
//        //                if (frame == null)
//        //                {
//        //                    throw new InvalidOperationException(Local.UndefinedHtmlFormFrame);
//        //                }
//        //                frame.Execute(this, method, parameterArray);
//        //            }
//        //        }

//        //        public string GetFrameResourceUrl(string resource, params object[] parameterArray)
//        //        {
//        //            if (resource == null)
//        //            {
//        //                throw new ArgumentNullException("resource");
//        //            }
//        //            int scopeIndex = resource.IndexOf(KernelText.Scope);
//        //            string frameKey;
//        //            if (scopeIndex > -1)
//        //            {
//        //                frameKey = resource.Substring(0, scopeIndex);
//        //                resource = resource.Substring(scopeIndex + 2);
//        //            }
//        //            else
//        //            {
//        //                frameKey = string.Empty;
//        //            }
//        //            return ((IHttpResourceUrl)m_frameIndex[frameKey]).GetResourceUrl(resource, parameterArray);
//        //        }

