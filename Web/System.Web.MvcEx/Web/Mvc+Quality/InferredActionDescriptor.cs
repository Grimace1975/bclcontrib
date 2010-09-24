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
namespace System.Web.Mvc
{
    /// <summary>
    /// InferredActionDescriptor
    /// </summary>
    public class InferredActionDescriptor : ActionDescriptor
    {
        private readonly string _actionName;
        private readonly ControllerDescriptor _controllerDescriptor;

        public InferredActionDescriptor(string actionName, ControllerDescriptor controllerDescriptor)
        {
            _actionName = actionName;
            _controllerDescriptor = controllerDescriptor;
        }

        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
        {
            return new InferredViewResult { ViewName = ActionName };
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return new ParameterDescriptor[] { new InferredParameterDescriptor(this, ActionName) };
        }

        public override string ActionName
        {
            get { return _actionName; }
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get { return _controllerDescriptor; }
        }
    }
}