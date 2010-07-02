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
//        //        public string GetCommandEvent(DataCommand dataCommand)
//        //        {
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            return m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //        }
//        //        public string GetCommandEvent(string commandId, string commandText)
//        //        {
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            return m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //        }
//        //        public string GetCommandEvent(System.Web.UI.Control control, DataCommand dataCommand)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            return control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //        }
//        //        public string GetCommandEvent(System.Web.UI.Control control, string commandId, string commandText)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            return control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //        }

//        //        public string GetCommandUrl(DataCommand dataCommand)
//        //        {
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            return m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //        }
//        //        public string GetCommandUrl(string commandId, string commandText)
//        //        {
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            return m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //        }
//        //        public string GetCommandUrl(System.Web.UI.Control control, DataCommand dataCommand)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            return control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //        }
//        //        public string GetCommandUrl(System.Web.UI.Control control, string commandId, string commandText)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            return control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //        }

//        //        public void RenderButton(string text, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderButton(string text, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderClientButton(text, commandEvent, attrib);
//        //        }
//        //        public void RenderButton(string text, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderButton(string text, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderClientButton(text, commandEvent, attrib);
//        //        }
//        //        public void RenderButton(System.Web.UI.Control control, string text, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderButton(System.Web.UI.Control control, string text, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderClientButton(text, commandEvent, attrib);
//        //        }
//        //        public void RenderButton(System.Web.UI.Control control, string text, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderButton(System.Web.UI.Control control, string text, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderClientButton(text, commandEvent, attrib);
//        //        }

//        //        public void RenderClientButton(string text, string commandEvent, params string[] parameterArray)
//        //        {
//        //            RenderClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderClientButton(string text, string commandEvent, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (commandEvent == null)
//        //            {
//        //                throw new ArgumentNullException("commandEvent");
//        //            }
//        //            m_writeCount++;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                commandEvent = KernelText.Axb(attrib.Slice("onclick"), ";", commandEvent);
//        //                AddHtmlAttrib(attrib, null);
//        //            }
//        //            if (commandEvent.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Onclick, commandEvent);
//        //            }
//        //            //m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Value, text);
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Button);
//        //            m_textWriter.Write(text);
//        //            m_textWriter.RenderEndTag();
//        //        }

//        //        public void RenderInputButton(string text, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderInputClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderInputButton(string text, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderInputClientButton(text, commandEvent, attrib);
//        //        }
//        //        public void RenderInputButton(string text, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderInputClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderInputButton(string text, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderInputClientButton(text, commandEvent, attrib);
//        //        }
//        //        public void RenderInputButton(System.Web.UI.Control control, string text, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderInputClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderInputButton(System.Web.UI.Control control, string text, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderInputClientButton(text, commandEvent, attrib);
//        //        }
//        //        public void RenderInputButton(System.Web.UI.Control control, string text, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderInputClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderInputButton(System.Web.UI.Control control, string text, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderInputClientButton(text, commandEvent, attrib);
//        //        }

//        //        public void RenderInputClientButton(string text, string commandEvent, params string[] parameterArray)
//        //        {
//        //            RenderInputClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderInputClientButton(string text, string commandEvent, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (commandEvent == null)
//        //            {
//        //                throw new ArgumentNullException("commandEvent");
//        //            }
//        //            m_writeCount++;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                AddHtmlAttrib(attrib, null);
//        //            }
//        //            if (commandEvent.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Onclick, commandEvent);
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Value, text);
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Input);
//        //            m_textWriter.RenderEndTag();
//        //        }

//        //        public void RenderImageButton(string url, string alt, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageButton(string url, string alt, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, attrib);
//        //        }
//        //        public void RenderImageButton(string url, string alt, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageButton(string url, string alt, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, attrib);
//        //        }
//        //        public void RenderImageButton(System.Web.UI.Control control, string url, string alt, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageButton(System.Web.UI.Control control, string url, string alt, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, attrib);
//        //        }
//        //        public void RenderImageButton(System.Web.UI.Control control, string url, string alt, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageButton(System.Web.UI.Control control, string url, string alt, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageClientButton(url, alt, commandEvent, attrib);
//        //        }

//        //        public void RenderImageClientButton(string url, string alt, string commandEvent, params string[] parameterArray)
//        //        {
//        //            RenderImageClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageClientButton(string url, string alt, string commandEvent, Attrib attrib)
//        //        {
//        //            if ((url == null) || (url.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (commandEvent == null)
//        //            {
//        //                throw new ArgumentNullException("commandEvent");
//        //            }
//        //            m_writeCount++;
//        //            var onClickEvent = string.Empty;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                Attrib imageAttrib = attrib.Slice<Attrib>("image");
//        //                if (attrib.Count > 0)
//        //                {
//        //                    onClickEvent = attrib.Slice("onclick");
//        //                    AddHtmlAttrib(attrib, null);
//        //                }
//        //                AddHtmlAttrib(imageAttrib);
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Alt, alt);
//        //            if (alt.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Title, alt);
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Src, url);
//        //            if ((commandEvent.Length > 0) || (onClickEvent.Length > 0))
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Onclick, onClickEvent + commandEvent + ";return false;");
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Type, "image");
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Input);
//        //            m_textWriter.RenderEndTag();
//        //        }

//        //        public void RenderImageEventButton(string url, string alt, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageEventButton(string url, string alt, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, attrib);
//        //        }
//        //        public void RenderImageEventButton(string url, string alt, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageEventButton(string url, string alt, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandEvent = m_commandTargetControl.Page.ClientScript.GetPostBackEventReference(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, attrib);
//        //        }
//        //        public void RenderImageEventButton(System.Web.UI.Control control, string url, string alt, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageEventButton(System.Web.UI.Control control, string url, string alt, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText)) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, attrib);
//        //        }
//        //        public void RenderImageEventButton(System.Web.UI.Control control, string url, string alt, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageEventButton(System.Web.UI.Control control, string url, string alt, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandEvent = control.Page.ClientScript.GetPostBackEventReference(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty))) + ";";
//        //            RenderImageEventClientButton(url, alt, commandEvent, attrib);
//        //        }

//        //        public void RenderImageEventClientButton(string url, string alt, string commandEvent, params string[] parameterArray)
//        //        {
//        //            RenderImageEventClientButton(url, alt, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageEventClientButton(string url, string alt, string commandEvent, Attrib attrib)
//        //        {
//        //            if ((url == null) || (url.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (commandEvent == null)
//        //            {
//        //                throw new ArgumentNullException("commandEvent");
//        //            }
//        //            m_writeCount++;
//        //            Attrib imageAttrib;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                imageAttrib = attrib.Slice<Attrib>("image");
//        //                if (attrib.Count > 0)
//        //                {
//        //                    AddHtmlAttrib(attrib, null);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                imageAttrib = null;
//        //            }
//        //            if (commandEvent.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Onclick, commandEvent);
//        //            }
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
//        //            if ((imageAttrib != null) && (imageAttrib.Count > 0))
//        //            {
//        //                AddHtmlAttrib(imageAttrib);
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Src, url);
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Alt, alt);
//        //            if (alt.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Title, alt);
//        //            }
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Img);
//        //            m_textWriter.RenderEndTag();
//        //            m_textWriter.RenderEndTag();
//        //        }

//        //        public void RenderImageLinkButton(string url, string alt, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageLinkButton(string url, string alt, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, attrib);
//        //        }
//        //        public void RenderImageLinkButton(string url, string alt, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageLinkButton(string url, string alt, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, attrib);
//        //        }
//        //        public void RenderImageLinkButton(System.Web.UI.Control control, string url, string alt, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageLinkButton(System.Web.UI.Control control, string url, string alt, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, attrib);
//        //        }
//        //        public void RenderImageLinkButton(System.Web.UI.Control control, string url, string alt, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageLinkButton(System.Web.UI.Control control, string url, string alt, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (url == null)
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderImageLinkClientButton(url, alt, commandUrl, attrib);
//        //        }

//        //        public void RenderImageLinkClientButton(string url, string alt, string commandUrl, params string[] parameterArray)
//        //        {
//        //            RenderImageLinkClientButton(url, alt, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderImageLinkClientButton(string url, string alt, string commandUrl, Attrib attrib)
//        //        {
//        //            if ((url == null) || (url.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("url");
//        //            }
//        //            if (alt == null)
//        //            {
//        //                throw new ArgumentNullException("alt");
//        //            }
//        //            if (commandUrl == null)
//        //            {
//        //                throw new ArgumentNullException("commandUrl");
//        //            }
//        //            m_writeCount++;
//        //            Attrib imageAttrib;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                imageAttrib = attrib.Slice<Attrib>("image");
//        //                if (attrib.Count > 0)
//        //                {
//        //                    AddHtmlAttrib(attrib, null);
//        //                }
//        //            }
//        //            else
//        //            {
//        //                imageAttrib = null;
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Href, commandUrl);
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
//        //            if ((imageAttrib != null) && (imageAttrib.Count > 0))
//        //            {
//        //                AddHtmlAttrib(imageAttrib);
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Src, url);
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Alt, alt);
//        //            if (alt.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Title, alt);
//        //            }
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Img);
//        //            m_textWriter.RenderEndTag();
//        //            m_textWriter.RenderEndTag();
//        //        }

//        //        public void RenderLinkButton(string text, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderLinkClientButton(text, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderLinkButton(string text, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderLinkClientButton(text, commandUrl, attrib);
//        //        }
//        //        public void RenderLinkButton(string text, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderLinkClientButton(text, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderLinkButton(string text, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            if (m_commandTargetControl == null)
//        //            {
//        //                throw new InvalidOperationException(Local.UndefinedCommandTargetControl);
//        //            }
//        //            string commandUrl = m_commandTargetControl.Page.ClientScript.GetPostBackClientHyperlink(m_commandTargetControl, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderLinkClientButton(text, commandUrl, attrib);
//        //        }
//        //        public void RenderLinkButton(System.Web.UI.Control control, string text, DataCommand dataCommand, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderLinkClientButton(text, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderLinkButton(System.Web.UI.Control control, string text, DataCommand dataCommand, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (dataCommand == null)
//        //            {
//        //                throw new ArgumentNullException("dataCommand");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", dataCommand.CommandId, dataCommand.CommandText));
//        //            RenderLinkClientButton(text, commandUrl, attrib);
//        //        }
//        //        public void RenderLinkButton(System.Web.UI.Control control, string text, string commandId, string commandText, params string[] parameterArray)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderLinkClientButton(text, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderLinkButton(System.Web.UI.Control control, string text, string commandId, string commandText, Attrib attrib)
//        //        {
//        //            if (control == null)
//        //            {
//        //                throw new ArgumentNullException("control");
//        //            }
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if ((commandId == null) || (commandId.Length == 0))
//        //            {
//        //                throw new ArgumentNullException("commandId");
//        //            }
//        //            string commandUrl = control.Page.ClientScript.GetPostBackClientHyperlink(control, string.Format(System.Globalization.CultureInfo.InvariantCulture, "CommandId={0};CommandText={1}", commandId, (commandText ?? string.Empty)));
//        //            RenderLinkClientButton(text, commandUrl, attrib);
//        //        }

//        //        public void RenderLinkClientButton(string text, string commandUrl, params string[] parameterArray)
//        //        {
//        //            RenderLinkClientButton(text, commandUrl, new Attrib(parameterArray));
//        //        }
//        //        public void RenderLinkClientButton(string text, string commandUrl, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (commandUrl == null)
//        //            {
//        //                throw new ArgumentNullException("commandUrl");
//        //            }
//        //            m_writeCount++;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                AddHtmlAttrib(attrib, null);
//        //            }
//        //            m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Href, commandUrl);
//        //            if (text.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Title, text);
//        //            }
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
//        //            m_textWriter.Write(text);
//        //            m_textWriter.RenderEndTag();
//        //        }

//        //        public void RenderLinkEventClientButton(string text, string commandEvent, params string[] parameterArray)
//        //        {
//        //            RenderLinkEventClientButton(text, commandEvent, new Attrib(parameterArray));
//        //        }
//        //        public void RenderLinkEventClientButton(string text, string commandEvent, Attrib attrib)
//        //        {
//        //            if (text == null)
//        //            {
//        //                throw new ArgumentNullException("text");
//        //            }
//        //            if (commandEvent == null)
//        //            {
//        //                throw new ArgumentNullException("commandEvent");
//        //            }
//        //            m_writeCount++;
//        //            if ((attrib != null) && (attrib.Count > 0))
//        //            {
//        //                AddHtmlAttrib(attrib, null);
//        //            }
//        //            if (commandEvent.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Onclick, commandEvent);
//        //            }
//        //            if (text.Length > 0)
//        //            {
//        //                m_textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Title, text);
//        //            }
//        //            m_textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.A);
//        //            m_textWriter.Write(text);
//        //            m_textWriter.RenderEndTag();
//        //        }
