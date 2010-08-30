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
namespace System.Web.UI
{
    public partial class HtmlBuilder
    {
        public HtmlBuilder BeginSmartTag(HtmlTag tag, params string[] args) { return BeginSmartTag(tag, Nattrib.Parse(args)); }
        public HtmlBuilder BeginSmartTag(HtmlTag tag, Nattrib attrib)
        {
            string c;
            switch (tag)
            {
                case HtmlTag._CommandTarget:
                    throw new NotSupportedException();
                case HtmlTag.Script:
                    o_Script(attrib);
                    return this;

                // Container
                case HtmlTag.Div:
                    o_Div(attrib);
                    return this;

                // Content
                case HtmlTag.A:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("url"))))
                        throw new ArgumentException("Local.UndefinedAttribUrl", "attrib");
                    o_A(c, attrib);
                    return this;
                case HtmlTag.H1:
                    o_H1(attrib);
                    return this;
                case HtmlTag.H2:
                    o_H2(attrib);
                    return this;
                case HtmlTag.H3:
                    o_H3(attrib);
                    return this;
                case HtmlTag.P:
                    o_P(attrib);
                    return this;
                case HtmlTag.Span:
                    o_Span(attrib);
                    return this;

                // List
                case HtmlTag.Li:
                    o_Li(attrib);
                    return this;
                case HtmlTag.Ol:
                    o_Ol(attrib);
                    return this;
                case HtmlTag.Ul:
                    o_Ul(attrib);
                    return this;

                // Table
                case HtmlTag.Colgroup:
                    o_Colgroup(attrib);
                    return this;
                case HtmlTag.Table:
                    o_Table(attrib);
                    return this;
                case HtmlTag.Tbody:
                    o_Tbody(attrib);
                    return this;
                case HtmlTag.Td:
                    o_Td(attrib);
                    return this;
                case HtmlTag.Tfoot:
                    o_Tfoot(attrib);
                    return this;
                case HtmlTag.Th:
                    o_Th(attrib);
                    return this;
                case HtmlTag.Thead:
                    o_Thead(attrib);
                    return this;
                case HtmlTag.Tr:
                    o_Tr(attrib);
                    return this;

                // Form
                case HtmlTag.Button:
                    o_Button(attrib);
                    return this;
                case HtmlTag.Fieldset:
                    o_Fieldset(attrib);
                    return this;
                case HtmlTag.Form:
                    throw new NotSupportedException();
                case HtmlTag._FormReference:
                    throw new NotSupportedException();
                case HtmlTag.Label:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("forName"))))
                        throw new ArgumentException("Local.UndefinedAttribForName", "attrib");
                    o_Label(c, attrib);
                    return this;
                // o_optgroup - no match
                case HtmlTag.Option:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("value"))))
                        throw new ArgumentException("Local.UndefinedAttribValue", "attrib");
                    o_Option(c, attrib);
                    return this;
                case HtmlTag.Select:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("name"))))
                        throw new ArgumentException("Local.UndefinedAttribName", "attrib");
                    o_Select(c, attrib);
                    return this;
                case HtmlTag.Textarea:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("name"))))
                        throw new ArgumentException("Local.UndefinedAttribName", "attrib");
                    o_Textarea(c, attrib);
                    return this;
            }
            BeginHtmlTag(tag);
            return this;
        }

        public HtmlBuilder EndSmartTag(HtmlTag tag, params string[] args) { return EndSmartTag(tag, Nattrib.Parse(args)); }
        public HtmlBuilder EndSmartTag(HtmlTag tag, Nattrib attrib)
        {
            string c;
            string c2;
            switch (tag)
            {
                case HtmlTag._CommandTarget:
                    x_CommandTarget();
                    return this;
                case HtmlTag.Script:
                    x_Script();
                    return this;

                // Container
                case HtmlTag.Div:
                    x_Div();
                    return this;
                case HtmlTag.Iframe:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("url"))))
                        throw new ArgumentException("Local.UndefinedAttribUrl", "attrib");
                    x_Iframe(c, attrib);
                    return this;

                // Content
                case HtmlTag.A:
                    x_A();
                    return this;
                case HtmlTag.Br:
                    x_Br();
                    return this;
                case HtmlTag.H1:
                    x_H1();
                    return this;
                case HtmlTag.H2:
                    x_H2();
                    return this;
                case HtmlTag.H3:
                    x_H3();
                    return this;
                case HtmlTag.Hr:
                    x_Hr(attrib);
                    return this;
                case HtmlTag.Img:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("url"))))
                        throw new ArgumentException("Local.UndefinedAttribUrl", "attrib");
                    if (string.IsNullOrEmpty(c2 = attrib.Slice<string>("value")))
                        throw new ArgumentException("Local.UndefinedAttribValue", "attrib");
                    x_Img(c, c2, attrib);
                    return this;
                case HtmlTag.P:
                    x_P();
                    return this;
                case HtmlTag.Span:
                    x_Span();
                    return this;
                //- x_tofu - no match

                // List
                case HtmlTag.Li:
                    x_Li();
                    return this;
                case HtmlTag.Ol:
                    x_Ol();
                    return this;
                case HtmlTag.Ul:
                    x_Ul();
                    return this;

                // Table
                case HtmlTag.Col:
                    x_Col(attrib);
                    return this;
                case HtmlTag.Colgroup:
                    x_Colgroup();
                    return this;
                case HtmlTag.Table:
                    x_Table();
                    return this;
                case HtmlTag.Tbody:
                    x_Tbody();
                    return this;
                case HtmlTag.Td:
                    x_Td();
                    return this;
                case HtmlTag.Tfoot:
                    x_Tfoot();
                    return this;
                case HtmlTag.Th:
                    x_Th();
                    return this;
                case HtmlTag.Thead:
                    x_Thead();
                    return this;
                case HtmlTag.Tr:
                    x_Tr();
                    return this;

                // Form
                case HtmlTag.Button:
                    x_Button();
                    return this;
                case HtmlTag.Fieldset:
                    x_Fieldset();
                    return this;
                case HtmlTag.Form:
                    x_Form();
                    return this;
                case HtmlTag._FormReference:
                    x_FormReference();
                    return this;
                case HtmlTag.Input:
                    if ((attrib == null) || (string.IsNullOrEmpty(c = attrib.Slice<string>("name"))))
                        throw new ArgumentException("Local.UndefinedAttribName", "attrib");
                    if (string.IsNullOrEmpty(c2 = attrib.Slice<string>("value")))
                        throw new ArgumentException("Local.UndefinedAttribValue", "attrib");
                    x_Input(c, c2, attrib);
                    return this;
                case HtmlTag.Label:
                    x_Label();
                    return this;
                // x_optgroup - no match
                case HtmlTag.Option:
                    x_Option();
                    return this;
                case HtmlTag.Select:
                    x_Select();
                    return this;
                case HtmlTag.Textarea:
                    x_Textarea();
                    return this;
            }
            EndHtmlTag();
            return this;
        }
    }
}