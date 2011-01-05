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
using System.Text;
namespace System.Interop.AudioVideo.Win32
{
    /// <summary>
    /// Helper
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// Tries the specified err.
        /// </summary>
        /// <param name="err">The err.</param>
        public static void Try(int err)
        {
            if (err != Native_.WindowsMultimedia.MMSYSERR_NOERROR)
            {
                //string errorText = string.Empty.PadLeft(500);
                //Native_.WindowsMultimedia.waveInGetErrorText(err, errorText, errorText.Length);
                byte[] errorBuffer = new byte[1000];
                Native_.WindowsMultimedia.waveInGetErrorText(err, errorBuffer, errorBuffer.Length);
                string errorText = UTF8Encoding.Default.GetString(errorBuffer);
                throw new Exception(errorText);
            }
        }
    }
}
