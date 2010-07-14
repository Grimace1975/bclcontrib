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
//namespace System.Web.UI.WebControls
//{
//    /// <summary>
//    /// TextBoxEx2
//    /// </summary>
//    public class TextBoxEx2 : TextBox
//    {
//        public TextBoxEx2()
//            : base() { }

//        public string HintText { get; set; }

//        public string HintCssClass { get; set; }

//        public string OnClientEnterKey { get; set; }

//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);
//            if (Text == HintText)
//                Text = string.Empty;
//        }

//        //        protected override void OnPreRender(System.EventArgs e)
//        //        {
//        //            base.OnPreRender(e);
//        //            //+
//        //            bool isHasHint = ((m_hintText != null) && (m_hintText.Length > 0) ? true : false);
//        //            bool isHasOnClientEnterKey = ((m_onClientEnterKey != null) && (m_onClientEnterKey.Length > 0) ? true : false);
//        //            if ((isHasHint == true) || (isHasOnClientEnterKey == true))
//        //            {
//        //                System.Text.StringBuilder script = new System.Text.StringBuilder();
//        //                script.Append(@"
//        //<script type=""text/javascript"">
//        ///* <![CDATA[ */
//        //	Kernel.AddEvent(window, 'load', function () {
//        //		var textBox = _gel('" + this.ClientID + @"');
//        //        if (textBox) {");
//        //                if (isHasHint == true)
//        //                {
//        //                    string effectiveHintCssClass = string.Empty;
//        //                    string normalCssClass = string.Empty;
//        //                    if (Text == string.Empty)
//        //                    {
//        //                        Text = HintText;
//        //                        if ((HintCssClass != null) && (HintCssClass.Length > 0))
//        //                        {
//        //                            effectiveHintCssClass = CssClass + " " + HintCssClass;
//        //                            normalCssClass = CssClass;
//        //                        }
//        //                    }
//        //                    if (effectiveHintCssClass.Length > 0)
//        //                    {
//        //                        script.Append(@"
//        //    		textBox.className = '" + effectiveHintCssClass + @"';");
//        //                    }
//        //                    script.Append(@"
//        //	    	Kernel.AddEvent(textBox, 'blur', function(e) {var source=Kernel.GetEventSource(e); if ((source != null) && (source.value == '')) {source.value = '" + HintText + @"';" + (effectiveHintCssClass.Length > 0 ? "source.className='" + effectiveHintCssClass + "';" : string.Empty) + @"}});
//        //    		Kernel.AddEvent(textBox, 'focus', function(e) {var source=Kernel.GetEventSource(e); if ((source != null) && (source.value == '" + HintText + @"')) {source.value = '';" + (effectiveHintCssClass.Length > 0 ? "source.className='" + normalCssClass + "';" : string.Empty) + @"}});");
//        //                }
//        //                if (isHasOnClientEnterKey == true)
//        //                {
//        //                    script.Append(@"
//        //	    	Kernel.AddEvent(textBox, 'keydown', function(e) {var source=Kernel.GetEventSource(e); if (Kernel.IsEnterKeyCode(e) == true) {Kernel.CancelEvent(e);" + m_onClientEnterKey + "}});");
//        //                }
//        //                script.Append(@"
//        //		    textBox = null;
//        //        }
//        //	});
//        ///* ]]> */
//        //</script>");
//        //                Page.ClientScript.RegisterStartupScript(GetType(), ClientID, script.ToString());
//        //            }
//        //        }
//    }
//}
