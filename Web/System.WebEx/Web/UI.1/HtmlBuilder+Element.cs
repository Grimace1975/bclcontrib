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
using System.Collections.Generic;
using System.Globalization;
namespace System.Web.UI
{
    public partial class HtmlBuilder
    {
        private Stack<Element> _elementStack = new Stack<Element>();
        private string _formName;
        private bool _isInAnchor = false;
        private Control _commandTargetControl;
        private HtmlBuilderDivTag _divTag;
        private HtmlBuilderFormTag _formTag;
        private HtmlBuilderTableTag _tableTag;
        private HtmlOption _option;

        private struct Element
        {
            public HtmlTag Tag;
            public string ValueIfUnknown;
            public object State;

            public Element(HtmlTag tag, object state)
            {
                Tag = tag;
                State = state;
                ValueIfUnknown = null;
            }
            public Element(string tag, object state)
            {
                Tag = HtmlTag.Unknown;
                State = state;
                ValueIfUnknown = tag;
            }
        }

        private struct DivElementState
        {
            public bool IsState;
            public HtmlBuilderDivTag DivState;

            public DivElementState(bool isState, HtmlBuilderDivTag divState)
            {
                IsState = isState;
                DivState = divState;
            }
        }

        public class HtmlOption
        {
            public HtmlBuilderOptionType Type;
            public string Key;
            public string Value;
            public int Size;
        }

        public Control CommandTargetControl
        {
            get { return _commandTargetControl; }
            protected set { _commandTargetControl = value; }
        }

        public HtmlBuilderDivTag DivTag
        {
            get { return _divTag; }
        }

        public HtmlBuilderFormTag FormTag
        {
            get { return _formTag; }
        }

        public HtmlBuilderTableTag TableTag
        {
            get { return _tableTag; }
        }

        public void ElementPop(HtmlTag tag, string valueIfUnknown, HtmlTag stopTag, string stopValueIfUnknown)
        {
            var stackTag = HtmlTag.__Undefined;
            string stackValueIfUnknown = null;
            while (((stackTag != tag) || (stackValueIfUnknown != valueIfUnknown)) && (_elementStack.Count > 0))
            {
                Element peekElement;
                if (stopTag != HtmlTag.__Undefined)
                {
                    peekElement = _elementStack.Peek();
                    HtmlTag peekElementTag = peekElement.Tag;
                    switch (stopTag)
                    {
                        case HtmlTag.__OlUl:
                            if ((peekElementTag == HtmlTag.Ol) || (peekElementTag == HtmlTag.Ul))
                                return;
                            break;
                        default:
                            if ((peekElementTag == stopTag) && (peekElement.ValueIfUnknown == stopValueIfUnknown))
                                return;
                            break;
                    }
                }
                var element = _elementStack.Pop();
                stackTag = element.Tag;
                stackValueIfUnknown = element.ValueIfUnknown;
                switch (stackTag)
                {
                    case HtmlTag._CommandTarget:
                        _commandTargetControl = (Control)element.State;
                        break;
                    case HtmlTag.Script:
                        _writeCount++;
                        _textWriter.Write("//--> ]]>");
                        _textWriter.RenderEndTag();
                        break;

                    // Container
                    case HtmlTag.Div:
                        _writeCount++;
                        var divElementState = (DivElementState)element.State;
                        if (divElementState.IsState)
                            _divTag = divElementState.DivState;
                        _textWriter.RenderEndTag();
                        break;

                    // Content
                    case HtmlTag.A:
                        _writeCount++;
                        _isInAnchor = false;
                        _textWriter.RenderEndTag();
                        break;

                    // H1-3
                    case HtmlTag.H1:
                    case HtmlTag.H2:
                    case HtmlTag.H3:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;

                    // Paragraph
                    case HtmlTag.P:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;

                    // Span
                    case HtmlTag.Span:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;

                    // List
                    case HtmlTag.Li:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Ol:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Ul:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;

                    // Table
                    case HtmlTag.Colgroup:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Table:
                        _writeCount++;
                        _tableTag = (HtmlBuilderTableTag)element.State;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Tbody:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Td:
                        string nullTdBody;
                        if (((int)element.State == _writeCount) && ((nullTdBody = _tableTag.NullTdBody).Length > 0))
                            _textWriter.Write(nullTdBody);
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Tfoot:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Th:
                        string nullTdBody2;
                        if (((int)element.State == _writeCount) && ((nullTdBody2 = _tableTag.NullTdBody).Length > 0))
                            _textWriter.Write(nullTdBody2);
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Thead:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Tr:
                        var trCloseMethod = _tableTag.TrCloseMethod;
                        if (trCloseMethod != HtmlBuilderTableTag.TableTrCloseMethod.Undefined)
                        {
                            int tdCount = (_tableTag.ColumnCount - _tableTag.ColumnIndex);
                            switch (trCloseMethod)
                            {
                                case HtmlBuilderTableTag.TableTrCloseMethod.Td:
                                    for (int tdIndex = 1; tdIndex < tdCount; tdIndex++)
                                    {
                                        _tableTag.AddAttribute(this, _textWriter, HtmlTag.Td, null);
                                        _textWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                                        string nullTdBody3 = _tableTag.NullTdBody;
                                        if (nullTdBody3.Length > 0)
                                            _textWriter.Write(nullTdBody3);
                                        _textWriter.RenderEndTag();
                                    }
                                    break;
                                case HtmlBuilderTableTag.TableTrCloseMethod.TdColspan:
                                    if (tdCount > 0)
                                    {
                                        if (tdCount > 1)
                                            _textWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, tdCount.ToString());
                                        _tableTag.AddAttribute(this, _textWriter, HtmlTag.Td, null);
                                        _textWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                                        string nullTdBody4 = _tableTag.NullTdBody;
                                        if (nullTdBody4.Length > 0)
                                            _textWriter.Write(nullTdBody4);
                                        _textWriter.RenderEndTag();
                                    }
                                    break;
                            }
                        }
                        _tableTag.IsTrHeader = false;
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;

                    // Form
                    case HtmlTag._FormReference:
                        if (_formTag != null)
                            _formTag = null;
                        break;
                    case HtmlTag.Button:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Fieldset:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Form:
                        _writeCount++;
                        //Http.Instance.ExitForm(this);
                        if (_formTag != null)
                            _formTag = null;
                        _formName = null;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Label:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Option:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Select:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Textarea:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                    case HtmlTag.Unknown:
                        _writeCount++;
                        switch (element.ValueIfUnknown)
                        {
                            case "optgroup":
                                _textWriter.RenderEndTag();
                                break;
                            default:
                                _textWriter.RenderEndTag();
                                break;
                        }
                        break;
                    default:
                        _writeCount++;
                        _textWriter.RenderEndTag();
                        break;
                }
            }
        }

        public void ElementPush(HtmlTag tag, object state)
        {
            _elementStack.Push(new Element(tag, state));
        }
        public void ElementPush(string tag, object state)
        {
            _elementStack.Push(new Element(tag, state));
        }
    }
}