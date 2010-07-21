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
//        //        #region DATA
//        //        //public void RenderDataAction(string type, string value, string script, params string[] parameterArray) {
//        //        //   //+ title      
//        //        //   string cTitle = "";
//        //        //   if ((cScript != "") && (cScript.Substring(0, 1) == "{")) {
//        //        //      cTitle = cScript.Substring(1, cScript.IndexOf("}") - 1);
//        //        //      cScript = cScript.Substring(cScript.IndexOf("}") + 1);
//        //        //   }
//        //        //   Hash cAttribHash = Kernel.AttribEncode(vAttrib);
//        //        //   string cAddAction = cAttribHash.Slice("addtarget");
//        //        //   if (cAddAction != "") {
//        //        //      cScript = "var o=document.forms['" + m_cFormDataName + "'];var cLastAction=o.action;o.action+='" + cAddAction + "';" + cScript + "o.action=cLastAction;return(false);";
//        //        //   } else {
//        //        //      cScript = "var o=document.forms['" + m_cFormDataName + "'];" + cScript + "return(false);";
//        //        //   }
//        //        //   //+
//        //        //   switch (cType.ToLower()) {
//        //        //   case "button":
//        //        //   case "submit":
//        //        //   case "reset":
//        //        //      cAttribHash["title"] = cTitle;
//        //        //      cAttribHash["onclick"] = cScript;
//        //        //      cAttribHash["class"] = "iButton";
//        //        //      cAttribHash["type"] = cType;
//        //        //      x_Input(null, cValue, cAttribHash);
//        //        //      break;
//        //        //   case "image":
//        //        //      o_A("#", new string[] { "onclick=" + cScript, "title=" + cTitle });
//        //        //      x_Img(cValue, cAttribHash); x_A();
//        //        //      break;
//        //        //   //	case "imagebutton":
//        //        //   //		Dim cElementArray: cElementArray = Split(Replace(cName, "\", "/"), "/")
//        //        //   //		Dim cId: cId = Split(cElementArray(Ubound(cElementArray)) & ".", ".")(0)
//        //        //   //		Render_Button = "<input type=""image"" src="""& cName &""""& Kernel_Iif(cTitle <> "", " title="""& cTitle &"""", "") &" onclick="""& Render_Button &""" class="""& m_cScope &"-iButton"" id="""& cId &""" name="""& cId &""">"
//        //        //   //		break;
//        //        //   case "text":
//        //        //      cAttribHash["title"] = cTitle;
//        //        //      cAttribHash["onclick"] = cScript;
//        //        //      o_A("#", cAttribHash);
//        //        //      Write(cValue); x_A();
//        //        //      break;
//        //        //   }
//        //        //   cAttribHash = null;
//        //        //}

//        //        public DataChannel DataChannel
//        //        {
//        //            get
//        //            {
//        //                return m_formState.DataChannel;
//        //            }
//        //        }

//        //        //public void RenderDataError(params string[] parameterArray) {
//        //        //   if (m_formDataState.Error.Length > 0) {
//        //        //      //		Web.x_("a: " + (m_oFormDataState == null).ToString());
//        //        //      o_Tr(null);
//        //        //      //		Web.x_("b: " + (m_oFormDataState == null).ToString());
//        //        //      o_Td((vAttrib is string ? Kernel.Text2((string)vAttrib, new string[] { "class=iError", "colspan=2", "align=center" }) : vAttrib));
//        //        //      //		Web.x_("c: " + (m_oFormDataState == null).ToString());
//        //        //      x_(m_oFormDataState.Error);
//        //        //   }
//        //        //}

//        //        public string DataName
//        //        {
//        //            get
//        //            {
//        //                if (m_formName == null)
//        //                {
//        //                    throw new InvalidOperationException(Local.UndefinedHtmlForm);
//        //                }
//        //                return m_formName;
//        //            }
//        //        }

//        //        public void RenderDataLabel(string key)
//        //        {
//        //            if (key == null)
//        //            {
//        //                throw new ArgumentNullException("key");
//        //            }
//        //            RenderControl(m_formState.DataChannel[key].LabelControl);
//        //        }

//        //        public void RenderDataInput(string key)
//        //        {
//        //            if (key == null)
//        //            {
//        //                throw new ArgumentNullException("key");
//        //            }
//        //            RenderControl(m_formState.DataChannel[key].InputControl);
//        //        }
//        //        #endregion DATA

