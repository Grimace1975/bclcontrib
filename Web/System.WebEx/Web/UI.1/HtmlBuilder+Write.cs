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
        public void Write(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.Write(value);
            }
        }
        public void Write(object value)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.Write(textValue);
            }
        }
        public void Write(string value, string defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.Write(value);
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.Write(defaultValue);
            }
        }
        public void Write(object value, string defaultValue)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.Write(textValue);
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.Write(defaultValue);
            }
        }

        public void WriteBreak()
        {
            _writeCount++;
            _textWriter.WriteBreak();
        }

        public void WriteLine(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.Write(value);
                _textWriter.WriteBreak();
            }
        }
        public void WriteLine(object value)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.Write(textValue);
                _textWriter.WriteBreak();
            }
        }
        public void WriteLine(string value, string defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.Write(value);
                _textWriter.WriteBreak();
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.Write(defaultValue);
                _textWriter.WriteBreak();
            }
        }
        public void WriteLine(object value, string defaultValue)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.Write(textValue);
                _textWriter.WriteBreak();
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.Write(defaultValue);
                _textWriter.WriteBreak();
            }
        }

        public void WriteText(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(value);
            }
        }
        public void WriteText(object value)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(textValue);
            }
        }
        public void WriteText(string value, string defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(value);
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(defaultValue);
            }
        }
        public void WriteText(object value, string defaultValue)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(textValue);
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(defaultValue);
            }
        }

        public void WriteTextLine(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(value);
                _textWriter.WriteBreak();
            }
        }
        public void WriteTextLine(object value)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(textValue);
                _textWriter.WriteBreak();
            }
        }
        public void WriteTextLine(string value, string defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(value);
                _textWriter.WriteBreak();
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(defaultValue);
                _textWriter.WriteBreak();
            }
        }
        public void WriteTextLine(object value, string defaultValue)
        {
            string textValue = (value as string);
            if (!string.IsNullOrEmpty(textValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(textValue);
                _textWriter.WriteBreak();
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                _writeCount++;
                _textWriter.WriteEncodedText(defaultValue);
                _textWriter.WriteBreak();
            }
        }
    }
}