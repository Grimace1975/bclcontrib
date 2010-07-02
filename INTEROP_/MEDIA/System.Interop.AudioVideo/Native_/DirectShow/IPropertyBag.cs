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
    /// IPropertyBag
    /// </summary>
    [ComImport, Guid("55272A00-42CB-11CE-8135-00AA004BB851"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyBag
    {
        /// <summary>
        /// Reads the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="pVar">The p var.</param>
        /// <param name="pErrorLog">The p error log.</param>
        /// <returns></returns>
        [PreserveSig]
        int Read([In, MarshalAs(UnmanagedType.LPWStr)] string propertyName, [In, Out, MarshalAs(UnmanagedType.Struct)] ref object pVar, [In] IntPtr pErrorLog);
        /// <summary>
        /// Writes the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="pVar">The p var.</param>
        /// <returns></returns>
        [PreserveSig]
        int Write([In, MarshalAs(UnmanagedType.LPWStr)] string propertyName, [In, MarshalAs(UnmanagedType.Struct)] ref object pVar);
    }
}
