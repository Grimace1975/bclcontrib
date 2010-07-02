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
namespace System.Interopt.AudioVideo.Native_.DirectShow
{
    /// <summary>
    /// IEnumPins
    /// </summary>
    [ComImport, Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPins
    {
        /// <summary>
        /// Nexts the specified c pins.
        /// </summary>
        /// <param name="cPins">The c pins.</param>
        /// <param name="pins">The pins.</param>
        /// <param name="pinsFetched">The pins fetched.</param>
        /// <returns></returns>
        [PreserveSig]
        int Next([In] int cPins, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IPin[] pins, [Out] out int pinsFetched);
        /// <summary>
        /// Skips the specified c pins.
        /// </summary>
        /// <param name="cPins">The c pins.</param>
        /// <returns></returns>
        [PreserveSig]
        int Skip([In] int cPins);
        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Reset();
        /// <summary>
        /// Clones the specified enum pins.
        /// </summary>
        /// <param name="enumPins">The enum pins.</param>
        /// <returns></returns>
        [PreserveSig]
        int Clone([Out] out IEnumPins enumPins);
    }
}
