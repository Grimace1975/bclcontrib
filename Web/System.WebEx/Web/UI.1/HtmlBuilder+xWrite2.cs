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
//        //        public void WriteHtml(string value)
//        //        {
//        //            if ((value != null) && (value.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(m_htmlTextProcess.Process(value, null));
//        //            }
//        //        }
//        //        public void WriteHtml(object value)
//        //        {
//        //            string textValue = (value as string);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(m_htmlTextProcess.Process(textValue, null));
//        //            }
//        //        }
//        //        public void WriteHtml(string value, string defaultValue)
//        //        {
//        //            if ((value != null) && (value.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(m_htmlTextProcess.Process(value, null));
//        //            }
//        //            else if ((defaultValue != null) && (defaultValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(defaultValue);
//        //            }
//        //        }
//        //        public void WriteHtml(object value, string defaultValue)
//        //        {
//        //            string textValue = (value as string);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(m_htmlTextProcess.Process(textValue, null));
//        //            }
//        //            else if ((defaultValue != null) && (defaultValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(defaultValue);
//        //            }
//        //        }
//        //        public void WriteHtml(string value, bool isExcludeBr)
//        //        {
//        //            if ((value != null) && (value.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                Attrib attrib = new Attrib(new string[] { isExcludeBr == true ? "format=ExcludeBr" : "format=Full" });
//        //                m_textWriter.Write(m_htmlTextProcess.Process(value, attrib));
//        //            }
//        //        }
//        //        public void WriteHtml(object value, bool isExcludeBr)
//        //        {
//        //            string textValue = (value as string);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                Attrib attrib = new Attrib(new string[] { isExcludeBr == true ? "format=ExcludeBr" : "format=Full" });
//        //                m_textWriter.Write(m_htmlTextProcess.Process(textValue, attrib));
//        //            }
//        //        }
//        //        public void WriteHtml(string value, string defaultValue, bool isExcludeBr)
//        //        {
//        //            if ((value != null) && (value.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                Attrib attrib = new Attrib(new string[] { isExcludeBr == true ? "format=ExcludeBr" : "format=Full" });
//        //                m_textWriter.Write(m_htmlTextProcess.Process(value, attrib));
//        //            }
//        //            else if ((defaultValue != null) && (defaultValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(defaultValue);
//        //            }
//        //        }
//        //        public void WriteHtml(object value, string defaultValue, bool isExcludeBr)
//        //        {
//        //            string textValue = (value as string);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                Attrib attrib = new Attrib(new string[] { isExcludeBr == true ? "format=ExcludeBr" : "format=Full" });
//        //                m_textWriter.Write(m_htmlTextProcess.Process(textValue, attrib));
//        //            }
//        //            else if ((defaultValue != null) && (defaultValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(defaultValue);
//        //            }
//        //        }

//        //        public void WriteInt32(int value)
//        //        {
//        //            m_writeCount++;
//        //            m_textWriter.Write(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
//        //        }
//        //        public void WriteInt32(object value)
//        //        {
//        //            string textValue;
//        //            if (value is int)
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(((int)value).ToString(System.Globalization.CultureInfo.InvariantCulture));
//        //            }
//        //            else if ((textValue = (value as string)) != null)
//        //            {
//        //                int intValue;
//        //                if (int.TryParse(textValue, out intValue) == true)
//        //                {
//        //                    m_writeCount++;
//        //                    m_textWriter.Write(intValue.ToString(System.Globalization.CultureInfo.InvariantCulture));
//        //                }
//        //            }
//        //        }
//        //        public void WriteInt32(object value, string defaultValue)
//        //        {
//        //            string textValue;
//        //            if (value is int)
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(((int)value).ToString(System.Globalization.CultureInfo.InvariantCulture));
//        //                return;
//        //            }
//        //            else if ((textValue = (value as string)) != null)
//        //            {
//        //                int intValue;
//        //                if (int.TryParse(textValue, out intValue) == true)
//        //                {
//        //                    m_writeCount++;
//        //                    m_textWriter.Write(intValue.ToString(System.Globalization.CultureInfo.InvariantCulture));
//        //                    return;
//        //                }
//        //            }
//        //            if ((defaultValue != null) && (defaultValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.Write(defaultValue);
//        //            }
//        //        }

//        //        public void WriteProcess(string value, params string[] parameterArray)
//        //        {
//        //            WriteProcess(value, ((parameterArray != null) && (parameterArray.Length > 0) ? new Attrib(parameterArray) : null));
//        //        }
//        //        public void WriteProcess(string value, Attrib attrib)
//        //        {
//        //            if ((value != null) && (value.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                if ((attrib != null) && (attrib.Count > 0))
//        //                {
//        //                    string textProcess = attrib.Slice("textProcess");
//        //                    if (textProcess.Length > 0)
//        //                    {
//        //                        value = KernelFactory.TextProcess[textProcess].Process(value, null);
//        //                    }
//        //                }
//        //                if ((attrib == null) || (attrib.Count == 0))
//        //                {
//        //                    m_textWriter.Write(value);
//        //                }
//        //                else
//        //                {
//        //                    AddHtmlAttrib(attrib, null);
//        //                    m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Span);
//        //                    m_textWriter.Write(value);
//        //                    m_textWriter.RenderEndTag();
//        //                }
//        //            }
//        //        }


//        //        public void WriteTextProcess(string value, params string[] parameterArray)
//        //        {
//        //            WriteTextProcess(value, ((parameterArray != null) && (parameterArray.Length > 0) ? new Attrib(parameterArray) : null));
//        //        }
//        //        public void WriteTextProcess(string value, Attrib attrib)
//        //        {
//        //            if ((value != null) && (value.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                if ((attrib != null) && (attrib.Count > 0))
//        //                {
//        //                    string textProcess = attrib.Slice("textProcess");
//        //                    if (textProcess.Length > 0)
//        //                    {
//        //                        value = KernelFactory.TextProcess[textProcess].Process(value, null);
//        //                    }
//        //                }
//        //                if ((attrib == null) || (attrib.Count == 0))
//        //                {
//        //                    m_textWriter.WriteEncodedText(value);
//        //                }
//        //                else
//        //                {
//        //                    AddHtmlAttrib(attrib, null);
//        //                    m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Span);
//        //                    m_textWriter.WriteEncodedText(value);
//        //                    m_textWriter.RenderEndTag();
//        //                }
//        //            }
//        //        }

//        //        public void WriteTextValue(object value, string dataType)
//        //        {
//        //            if ((dataType == null) || (dataType.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("dataType");
//        //            }
//        //            string textValue = KernelFactory.DataType[dataType].FormatValue(value, (Attrib)null);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.WriteEncodedText(textValue);
//        //            }
//        //        }
//        //        public void WriteTextValue(object value, string dataType, Attrib attrib)
//        //        {
//        //            if ((dataType == null) || (dataType.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("dataType");
//        //            }
//        //            string textValue = KernelFactory.DataType[dataType].FormatValue(value, attrib);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.WriteEncodedText(textValue);
//        //            }
//        //        }
//        //        public void WriteTextValue(object value, string dataType, string defaultValue)
//        //        {
//        //            if ((dataType == null) || (dataType.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("dataType");
//        //            }
//        //            string textValue = KernelFactory.DataType[dataType].FormatValue(value, defaultValue, null);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.WriteEncodedText(textValue);
//        //            }
//        //        }
//        //        public void WriteTextValue(object value, string dataType, string defaultValue, params string[] parameterArray)
//        //        {
//        //            if ((dataType == null) || (dataType.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("dataType");
//        //            }
//        //            string textValue = KernelFactory.DataType[dataType].FormatValue(value, defaultValue, new Attrib(parameterArray));
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.WriteEncodedText(textValue);
//        //            }
//        //        }
//        //        public void WriteTextValue(object value, string dataType, string defaultValue, Attrib attrib)
//        //        {
//        //            if ((dataType == null) || (dataType.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("dataType");
//        //            }
//        //            string textValue = KernelFactory.DataType[dataType].FormatValue(value, defaultValue, attrib);
//        //            if ((textValue != null) && (textValue.Length > 0))
//        //            {
//        //                m_writeCount++;
//        //                m_textWriter.WriteEncodedText(textValue);
//        //            }
//        //        }
