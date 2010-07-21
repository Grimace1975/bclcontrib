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
    /// <summary>
    /// HtmlTextBoxCommands
    /// </summary>
    [Flags]
    public enum HtmlTextBoxCommands
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 
        /// </summary>
        UndoRedo = 0x01,
        /// <summary>
        /// 
        /// </summary>
        Format = 0x02,
        /// <summary>
        /// 
        /// </summary>
        EnhancedFormat = 0x04,
        /// <summary>
        /// 
        /// </summary>
        Align = 0x08,
        /// <summary>
        /// 
        /// </summary>
        Indent = 0x10,
        /// <summary>
        /// 
        /// </summary>
        Insert = 0x20,
        /// <summary>
        /// 
        /// </summary>
        Table = 0x40,
        /// <summary>
        /// 
        /// </summary>
        Bullet = 0x80,
        /// <summary>
        /// 
        /// </summary>
        Font = 0x100,
        /// <summary>
        /// 
        /// </summary>
        CssStyle = 0x200,
        /// <summary>
        /// 
        /// </summary>
        ElementStyle = 0x400,
        /// <summary>
        /// 
        /// </summary>
        UserDefined = 0x800
    }
}
