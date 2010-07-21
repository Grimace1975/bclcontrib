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
using System.Patterns.Form;
namespace System.Web.UI
{
    public partial class HtmlBuilder
    {
        #region Html-Other
        public HtmlBuilder o_CommandTarget(Control commandTargetControl)
        {
            var lastCommandTargetControl = _commandTargetControl;
            _commandTargetControl = commandTargetControl;
            ElementPush(HtmlTag._CommandTarget, lastCommandTargetControl);
            return this;
        }

        public HtmlBuilder o_Script(params string[] args) { return o_Script(Nattrib.Parse(args)); }
        public HtmlBuilder o_Script(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Type, "text/javascript");
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Script);
            _textWriter.Write("//<!CDATA[[ <!--\n");
            ElementPush(HtmlTag.Script, null);
            return this;
        }

        public HtmlBuilder x_CommandTarget()
        {
            ElementPop(HtmlTag._CommandTarget, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Script()
        {
            ElementPop(HtmlTag.Script, null, HtmlTag.__Undefined, null);
            return this;
        }
        #endregion

        #region Html-Container
        public HtmlBuilder o_Div(params string[] args) { return o_Div(Nattrib.Parse(args)); }
        public HtmlBuilder o_Div(Nattrib attrib)
        {
            _writeCount++;
            bool isState;
            HtmlBuilderDivTag lastDivState;
            if (attrib != null)
            {
                isState = attrib.Slice<bool>("state");
                if (isState)
                {
                    lastDivState = _divTag;
                    _divTag = CreateHtmlBuilderDivTag(this, attrib);
                }
                else
                    lastDivState = null;
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            else
            {
                isState = false;
                lastDivState = null;
            }
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            ElementPush(HtmlTag.Div, new DivElementState(isState, lastDivState));
            return this;
        }

        public HtmlBuilder x_Div()
        {
            ElementPop(HtmlTag.Div, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Iframe(string url, params string[] args) { return x_Iframe(url, Nattrib.Parse(args)); }
        public HtmlBuilder x_Iframe(string url, Nattrib attrib)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Src, url);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Iframe);
            _textWriter.RenderEndTag();
            return this;
        }
        #endregion

        #region Html-Content
        public HtmlBuilder o_A(string url, params string[] args) { return o_A(url, (Nattrib.Parse(args))); }
        public HtmlBuilder o_A(string url, Nattrib attrib)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (_isInAnchor)
                throw new InvalidOperationException("Local.RedefineHtmlAnchor");
            _isInAnchor = true;
            _writeCount++;
            // todo: add
            //if (attrib.IsExist("alt") == false) {
            //   attrib["alt"] = attrib["title"];
            //}
            string textProcess;
            if (attrib != null)
            {
                textProcess = attrib.Slice<string>("textProcess");
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            else
                textProcess = string.Empty;
            //if (textProcess.Length > 0)
            //    url = KernelFactory.TextProcess[textProcess].Process(url, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Href, url);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.A);
            ElementPush(HtmlTag.A, null);
            return this;
        }

        public HtmlBuilder o_H1(params string[] args) { return o_H1(Nattrib.Parse(args)); }
        public HtmlBuilder o_H1(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.H1);
            ElementPush(HtmlTag.H1, null);
            return this;
        }

        public HtmlBuilder o_H2(params string[] args) { return o_H2(Nattrib.Parse(args)); }
        public HtmlBuilder o_H2(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.H2);
            ElementPush(HtmlTag.H2, null);
            return this;
        }

        public HtmlBuilder o_H3(params string[] args) { return o_H3(Nattrib.Parse(args)); }
        public HtmlBuilder o_H3(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.H3);
            ElementPush(HtmlTag.H3, null);
            return this;
        }

        public HtmlBuilder o_P(params string[] args) { return o_P(Nattrib.Parse(args)); }
        public HtmlBuilder o_P(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.P);
            ElementPush(HtmlTag.P, null);
            return this;
        }

        public HtmlBuilder o_Span(params string[] args) { return o_Span(Nattrib.Parse(args)); }
        public HtmlBuilder o_Span(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            ElementPush(HtmlTag.Span, null);
            return this;
        }

        public HtmlBuilder x_A()
        {
            if (!_isInAnchor)
                return this;
            ElementPop(HtmlTag.A, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Br()
        {
            _writeCount++;
            // note: default registration in htmltextwriter is incorrect, correction applied in static constructor.
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Br);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder x_H1()
        {
            ElementPop(HtmlTag.H1, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_H2()
        {
            ElementPop(HtmlTag.H2, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_H3()
        {
            ElementPop(HtmlTag.H3, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Hr(params string[] args) { return x_Hr(Nattrib.Parse(args)); }
        public HtmlBuilder x_Hr(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Hr);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder x_Img(string url, string alt, params string[] args) { return x_Img(url, alt, (Nattrib.Parse(args))); }
        public HtmlBuilder x_Img(string url, string alt, Nattrib attrib)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (alt == null)
                throw new ArgumentNullException("alt");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Src, url);
            _textWriter.AddAttributeIfUndefined(System.Web.UI.HtmlTextWriterAttribute.Alt, alt);
            _textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Img);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder x_P()
        {
            ElementPop(HtmlTag.P, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Span()
        {
            ElementPop(HtmlTag.Span, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Tofu(params string[] args) { return x_Tofu(Nattrib.Parse(args)); }
        public HtmlBuilder x_Tofu(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Src, HtmlTextWriterEx.TofuUri);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Img);
            _textWriter.RenderEndTag();
            return this;
        }
        #endregion

        #region Html-Option
        public HtmlBuilder AddOption(string key) { return AddOption(key, key); }
        public HtmlBuilder AddOption(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            value = value.Truncate(_option.Size, StringExtensions.TextTruncateType.Normal);
            switch (_option.Type)
            {
                case HtmlBuilderOptionType.Select:
                    _writeCount++;
                    if (key == _option.Value)
                        _textWriter.AddAttribute(HtmlTextWriterAttribute.Selected, "selected");
                    o_Option(key);
                    _textWriter.WriteEncodedText(value);
                    x_Option();
                    break;
                case HtmlBuilderOptionType.Radio:
                    _writeCount++;
                    o_Tr();
                    _textWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "25px");
                    o_Td();
                    if (key == _option.Value)
                        _textWriter.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                    x_Input(_option.Key, key, "type=radio");
                    o_Td();
                    _textWriter.WriteEncodedText(value);
                    break;
                case HtmlBuilderOptionType.Static:
                    if (key == _option.Value)
                    {
                        _writeCount++;
                        _textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "iStatic");
                        _textWriter.RenderBeginTag(HtmlTextWriterTag.Span);
                        _textWriter.WriteEncodedText(value);
                        _textWriter.RenderEndTag();
                    }
                    break;
            }
            return this;
        }

        public HtmlBuilder BeginOption(HtmlBuilderOptionType type, string key, string value, int size)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            _option = new HtmlOption { Type = type, Key = key, Value = value, Size = size };
            return this;
        }

        public HtmlBuilder BeginOptionGroup(string name, params string[] args)
        {
            if (_option == null)
                throw new ArgumentNullException("_option");
            switch (_option.Type)
            {
                case HtmlBuilderOptionType.Select:
                    o_Optgroup(name, args);
                    break;
            }
            return this;
        }

        public HtmlBuilder EndOption()
        {
            if (_option == null)
                throw new ArgumentNullException("_option");
            switch (_option.Type)
            {
                case HtmlBuilderOptionType.Select:
                    x_Select();
                    break;
                case HtmlBuilderOptionType.Radio:
                    x_Table();
                    break;
            }
            return this;
        }

        public HtmlBuilder EndOptionGroup()
        {
            if (_option == null)
                throw new ArgumentNullException("_option");
            switch (_option.Type)
            {
                case HtmlBuilderOptionType.Select:
                    x_Optgroup();
                    break;
            }
            return this;
        }
        #endregion

        #region Html-List
        public HtmlBuilder o_Li(params string[] args) { return o_Li(Nattrib.Parse(args)); }
        public HtmlBuilder o_Li(Nattrib attrib)
        {
            ElementPop(HtmlTag.Li, null, HtmlTag.__OlUl, null);
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Li);
            ElementPush(HtmlTag.Li, null);
            return this;
        }

        public HtmlBuilder o_Ol(params string[] args) { return o_Ol(Nattrib.Parse(args)); }
        public HtmlBuilder o_Ol(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Ol);
            ElementPush(HtmlTag.Ol, null);
            return this;
        }

        public HtmlBuilder o_Ul(params string[] args) { return o_Ul(Nattrib.Parse(args)); }
        public HtmlBuilder o_Ul(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
            ElementPush(HtmlTag.Ul, null);
            return this;
        }

        public HtmlBuilder x_Li()
        {
            ElementPop(HtmlTag.Li, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Ol()
        {
            ElementPop(HtmlTag.Ol, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Ul()
        {
            ElementPop(HtmlTag.Ul, null, HtmlTag.__Undefined, null);
            return this;
        }
        #endregion

        #region Html-Table
        public HtmlBuilder o_Colgroup(params string[] args) { return o_Colgroup(Nattrib.Parse(args)); }
        public HtmlBuilder o_Colgroup(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            if (_tableTag.Stage > HtmlBuilderTableTag.TableStage.Colgroup)
                throw new InvalidOperationException(string.Format("Local.InvalidTableStageAB", _tableTag.Stage.ToString(), HtmlBuilderTableTag.TableStage.Colgroup.ToString()));
            ElementPop(HtmlTag.Colgroup, null, HtmlTag.Table, null);
            _writeCount++;
            _tableTag.Stage = HtmlBuilderTableTag.TableStage.Colgroup;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Colgroup);
            ElementPush(HtmlTag.Colgroup, null);
            return this;
        }

        public HtmlBuilder o_Table(params string[] args) { return o_Table(Nattrib.Parse(args)); }
        public HtmlBuilder o_Table(Nattrib attrib)
        {
            _writeCount++;
            var lastTableState = _tableTag;
            _tableTag = CreateHtmlBuilderTableTag(this, attrib);
            string caption;
            if (attrib != null)
            {
                caption = attrib.Slice<string>("caption");
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            else
                caption = string.Empty;
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Cellpadding, "0");
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Cellspacing, "0");
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Table);
            if (caption.Length > 0)
            {
                _textWriter.RenderBeginTag(HtmlTextWriterTag.Caption);
                _textWriter.WriteEncodedText(caption);
                _textWriter.RenderEndTag();
            }
            ElementPush(HtmlTag.Table, lastTableState);
            return this;
        }

        public HtmlBuilder o_Tbody(params string[] args) { return o_Tbody(Nattrib.Parse(args)); }
        public HtmlBuilder o_Tbody(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            if (_tableTag.Stage > HtmlBuilderTableTag.TableStage.Tbody)
                throw new InvalidOperationException(string.Format("Local.InvalidTableStageAB", _tableTag.Stage.ToString(), HtmlBuilderTableTag.TableStage.Tbody.ToString()));
            ElementPop(HtmlTag.Tbody, null, HtmlTag.Table, null);
            _writeCount++;
            _tableTag.Stage = HtmlBuilderTableTag.TableStage.Tbody;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);
            ElementPush(HtmlTag.Tbody, null);
            return this;
        }

        public HtmlBuilder o_Td(params string[] args) { return o_Td(Nattrib.Parse(args)); }
        public HtmlBuilder o_Td(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            _tableTag.ColumnIndex++;
            ElementPop(HtmlTag.Td, null, HtmlTag.Tr, null);
            _writeCount++;
            _tableTag.AddAttribute(this, _textWriter, HtmlTag.Td, attrib);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            ElementPush(HtmlTag.Td, _writeCount);
            return this;
        }

        public HtmlBuilder o_Tfoot(params string[] args) { return o_Tfoot(Nattrib.Parse(args)); }
        public HtmlBuilder o_Tfoot(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            if (_tableTag.Stage > HtmlBuilderTableTag.TableStage.TheadTfoot)
                throw new InvalidOperationException(string.Format("Local.InvalidTableStageAB", _tableTag.Stage.ToString(), HtmlBuilderTableTag.TableStage.TheadTfoot.ToString()));
            ElementPop(HtmlTag.Tfoot, null, HtmlTag.Table, null);
            _writeCount++;
            _tableTag.Stage = HtmlBuilderTableTag.TableStage.TheadTfoot;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Tfoot);
            ElementPush(HtmlTag.Tfoot, null);
            return this;
        }

        public HtmlBuilder o_Th(params string[] args) { return o_Th(Nattrib.Parse(args)); }
        public HtmlBuilder o_Th(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            _tableTag.ColumnIndex++;
            ElementPop(HtmlTag.Th, null, HtmlTag.Tr, null);
            _writeCount++;
            _tableTag.AddAttribute(this, _textWriter, HtmlTag.Th, attrib);
            _textWriter.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Th);
            ElementPush(HtmlTag.Th, _writeCount);
            return this;
        }

        public HtmlBuilder o_Thead(params string[] args) { return o_Thead(Nattrib.Parse(args)); }
        public HtmlBuilder o_Thead(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            if (_tableTag.Stage > HtmlBuilderTableTag.TableStage.TheadTfoot)
                throw new InvalidOperationException(string.Format("Local.InvalidTableStageAB", _tableTag.Stage.ToString(), HtmlBuilderTableTag.TableStage.TheadTfoot.ToString()));
            ElementPop(HtmlTag.Thead, null, HtmlTag.Table, null);
            _writeCount++;
            _tableTag.Stage = HtmlBuilderTableTag.TableStage.TheadTfoot;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Thead);
            ElementPush(HtmlTag.Thead, null);
            return this;
        }

        public HtmlBuilder o_Tr(params string[] args) { return o_Tr(Nattrib.Parse(args)); }
        public HtmlBuilder o_Tr(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            _tableTag.RowIndex++;
            _tableTag.ColumnIndex = 0;
            ElementPop(HtmlTag.Tr, null, HtmlTag.Table, null);
            _writeCount++;
            _tableTag.AddAttribute(this, _textWriter, HtmlTag.Tr, attrib);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            ElementPush(HtmlTag.Tr, null);
            return this;
        }

        public HtmlBuilder x_Col(params string[] args) { return x_Col(Nattrib.Parse(args)); }
        public HtmlBuilder x_Col(Nattrib attrib)
        {
            if (_tableTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlTable");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Col);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder x_Colgroup()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Colgroup, null, HtmlTag.Table, null);
            return this;
        }

        public HtmlBuilder x_Table()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Table, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Tbody()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Tbody, null, HtmlTag.Table, null);
            return this;
        }

        public HtmlBuilder x_Td()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Td, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Tfoot()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Tfoot, null, HtmlTag.Table, null);
            return this;
        }

        public HtmlBuilder x_Th()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Th, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Thead()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Thead, null, HtmlTag.Table, null);
            return this;
        }

        public HtmlBuilder x_Tr()
        {
            if (_tableTag == null)
                return this;
            ElementPop(HtmlTag.Tr, null, HtmlTag.__Undefined, null);
            return this;
        }
        #endregion

        #region Html-Form
        public HtmlBuilder o_Button(params string[] args) { return o_Button(Nattrib.Parse(args)); }
        public HtmlBuilder o_Button(Nattrib attrib)
        {
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Button);
            ElementPush(HtmlTag.Button, null);
            return this;
        }

        public HtmlBuilder o_Fieldset(params string[] args) { return o_Fieldset(Nattrib.Parse(args)); }
        public HtmlBuilder o_Fieldset(Nattrib attrib)
        {
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            string legend;
            string legendAccessKey;
            if (attrib != null)
            {
                legend = attrib.Slice<string>("legend");
                legendAccessKey = attrib.Slice<string>("legendaccesskey");
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            else
            {
                legend = string.Empty;
                legendAccessKey = string.Empty;
            }
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Fieldset);
            if ((legend.Length > 0) || (legendAccessKey.Length > 0))
            {
                if (legendAccessKey.Length > 0)
                    _textWriter.AddAttribute(HtmlTextWriterAttribute.Accesskey, legendAccessKey);
                _textWriter.RenderBeginTag(HtmlTextWriterTag.Legend);
                if (legend.Length > 0)
                    _textWriter.Write(legend);
                _textWriter.RenderEndTag();
            }
            ElementPush(HtmlTag.Fieldset, null);
            return this;
        }

        public HtmlBuilder o_Form(Control commandTargetControl, IFormContext formContext, params string[] args) { return o_Form(commandTargetControl, formContext, Nattrib.Parse(args)); }
        public HtmlBuilder o_Form(Control commandTargetControl, IFormContext formContext, Nattrib attrib)
        {
            if (_formTag != null)
                throw new InvalidOperationException("Local.RedefineHtmlForm");
            _writeCount++;
            _formTag = CreateHtmlBuilderFormTag(this, formContext, attrib);
            //Http.Instance.EnterForm(this, attrib);
            string method;
            if (attrib != null)
            {
                _formName = attrib.Slice("name", "Form");
                method = attrib.Slice("method", "POST");
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            else
            {
                _formName = "Form";
                method = "POST";
            }
            if (_formName.Length > 0)
                _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Name, _formName);
            if (method.Length > 0)
                _textWriter.AddAttribute("method", method.ToUpperInvariant());
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Form);
            ElementPush(HtmlTag.Form, null);
            if (commandTargetControl != null)
                o_CommandTarget(commandTargetControl);
            return this;
        }

        public HtmlBuilder o_FormReference(Control commandTargetControl, IFormContext formContext, params string[] args) { return o_FormReference(commandTargetControl, formContext, Nattrib.Parse(args)); }
        public HtmlBuilder o_FormReference(Control commandTargetControl, IFormContext formContext, Nattrib attrib)
        {
            if (_formTag != null)
                throw new InvalidOperationException("Local.RedefineHtmlForm");
            _formTag = CreateHtmlBuilderFormTag(this, formContext, attrib);
            ElementPush(HtmlTag._FormReference, null);
            if (commandTargetControl != null)
                o_CommandTarget(commandTargetControl);
            return this;
        }

        public HtmlBuilder o_Label(string forName, params string[] args) { return o_Label(forName, Nattrib.Parse(args)); }
        public HtmlBuilder o_Label(string forName, Nattrib attrib)
        {
            if (forName == null)
                throw new ArgumentNullException("forName");
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.For, forName);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Label);
            ElementPush(HtmlTag.Label, null);
            return this;
        }

        public HtmlBuilder o_Optgroup(string name, params string[] args) { return o_Optgroup(name, Nattrib.Parse(args)); }
        public HtmlBuilder o_Optgroup(string name, Nattrib attrib)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttribute("Label", name);
            _textWriter.RenderBeginTag("Optgroup");
            ElementPush("Optgroup", null);
            return this;
        }

        public HtmlBuilder o_Option(string value, params string[] args) { return o_Option(value, Nattrib.Parse(args)); }
        public HtmlBuilder o_Option(string value, Nattrib attrib)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            if (attrib != null)
                AddAttribute(attrib, null);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Value, value);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Option);
            ElementPush(HtmlTag.Option, null);
            return this;
        }

        public HtmlBuilder o_Select(string name, params string[] args) { return o_Select(name, Nattrib.Parse(args)); }
        public HtmlBuilder o_Select(string name, Nattrib attrib)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            if (attrib != null)
            {
                // scale
                if (attrib.Exists("size"))
                {
                    string size = attrib.Slice<string>("size");
                    if (size.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Height, size);
                    else if (!size.EndsWith("u", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Height, (size.Parse<int>() * 7).ToString() + "px");
                    else
                        _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Size, size.Substring(0, size.Length - 1));
                }
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Id, name);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Name, name);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Select);
            ElementPush(HtmlTag.Select, null);
            return this;
        }

        public HtmlBuilder o_Textarea(string name, params string[] args) { return o_Textarea(name, Nattrib.Parse(args)); }
        public HtmlBuilder o_Textarea(string name, Nattrib attrib)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            if (attrib != null)
            {
                //+ scale
                if (attrib.Exists("cols"))
                {
                    string cols = attrib.Slice<string>("cols");
                    if (cols.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Width, cols);
                    else if (!cols.EndsWith("u", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Width, (cols.Parse<int>() * 7).ToString() + "px");
                    else
                        _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Cols, cols.Substring(0, cols.Length - 1));
                }
                if (attrib.Exists("rows"))
                {
                    string rows = attrib.Slice<string>("rows");
                    if (rows.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Height, rows);
                    else if (!rows.EndsWith("u", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Height, (rows.Parse<int>() * 7).ToString() + "px");
                    else
                        _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Rows, rows.Substring(0, rows.Length - 1));
                }
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Id, name);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Name, name);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Textarea);
            ElementPush(HtmlTag.Textarea, null);
            return this;
        }

        public HtmlBuilder x_Button()
        {
            ElementPop(HtmlTag.Button, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Fieldset()
        {
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            ElementPop(HtmlTag.Fieldset, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Form()
        {
            if (_formTag == null)
                return this;
            ElementPop(HtmlTag.Form, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_FormReference()
        {
            if (_formTag == null)
                return this;
            ElementPop(HtmlTag._FormReference, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Input(string name, string value, params string[] args) { return x_Input(name, value, Nattrib.Parse(args)); }
        public HtmlBuilder x_Input(string name, string value, Nattrib attrib)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (value == null)
                throw new ArgumentNullException("value");
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            _writeCount++;
            string type;
            if (attrib != null)
            {
                type = attrib.Slice<string>("type", "text");
                // scale
                if (attrib.Exists("size"))
                {
                    string size = attrib.Slice<string>("size");
                    if (size.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Width, size);
                    else if (!size.EndsWith("u", StringComparison.OrdinalIgnoreCase))
                        _textWriter.AddStyleAttributeIfUndefined(HtmlTextWriterStyle.Width, (size.Parse<int>() * 7).ToString() + "px");
                    else
                        _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Size, size.Substring(0, size.Length - 1));
                }
                if (attrib.Count > 0)
                    AddAttribute(attrib, null);
            }
            else
                type = "text";
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Id, name);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Name, name);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Type, type);
            _textWriter.AddAttributeIfUndefined(HtmlTextWriterAttribute.Value, value);
            _textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            _textWriter.RenderEndTag();
            return this;
        }

        public HtmlBuilder x_Label()
        {
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            ElementPop(HtmlTag.Label, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Optgroup()
        {
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            ElementPop(HtmlTag.Unknown, "Optgroup", HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Option()
        {
            if (_formTag == null)
                throw new InvalidOperationException("Local.UndefinedHtmlForm");
            ElementPop(HtmlTag.Option, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Select()
        {
            if (_formTag == null)
                return this;
            ElementPop(HtmlTag.Select, null, HtmlTag.__Undefined, null);
            return this;
        }

        public HtmlBuilder x_Textarea()
        {
            if (_formTag == null)
                return this;
            ElementPop(HtmlTag.Textarea, null, HtmlTag.__Undefined, null);
            return this;
        }
        #endregion
    }
}