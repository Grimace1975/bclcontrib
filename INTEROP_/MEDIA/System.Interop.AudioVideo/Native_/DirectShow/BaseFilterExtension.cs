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
using System.Runtime.InteropServices;
namespace System.Interop.AudioVideo.Native_.DirectShow
{
    /// <summary>
    /// BaseFilterExtension
    /// </summary>
    internal static class BaseFilterExtension
    {
        /// <summary>
        /// Gets the pin.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="dir">The dir.</param>
        /// <param name="num">The num.</param>
        /// <returns></returns>
        public static IPin GetPin(this IBaseFilter filter, PinDirection dir, int num)
        {
            IPin[] pin = new IPin[1];
            IEnumPins pinsEnum = null;
            if (filter.EnumPins(out pinsEnum) == 0)
            {
                PinDirection pinDir;
                int n;
                while (pinsEnum.Next(1, pin, out n) == 0)
                {
                    pin[0].QueryDirection(out pinDir);
                    if (pinDir == dir)
                    {
                        if (num == 0)
                        {
                            return pin[0];
                        }
                        num--;
                    }
                    Marshal.ReleaseComObject(pin[0]); pin[0] = null;
                }
            }
            return null;
        }
    }
}
