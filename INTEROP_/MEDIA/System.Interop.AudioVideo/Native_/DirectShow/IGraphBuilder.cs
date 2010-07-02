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
    /// IGraphBuilder
    /// </summary>
    [ComImport, Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphBuilder
    {
        /// <summary>
        /// Adds the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [PreserveSig]
        int AddFilter([In] IBaseFilter filter, [In, MarshalAs(UnmanagedType.LPWStr)] string name);
        /// <summary>
        /// Removes the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [PreserveSig]
        int RemoveFilter([In] IBaseFilter filter);
        /// <summary>
        /// Enums the filters.
        /// </summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns></returns>
        [PreserveSig]
        int EnumFilters([Out] out IntPtr enumerator);
        /// <summary>
        /// Finds the name of the filter by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [PreserveSig]
        int FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string name, [Out] out IBaseFilter filter);
        /// <summary>
        /// Connects the direct.
        /// </summary>
        /// <param name="pinOut">The pin out.</param>
        /// <param name="pinIn">The pin in.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int ConnectDirect([In] IPin pinOut, [In] IPin pinIn, [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Reconnects the specified pin.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        [PreserveSig]
        int Reconnect([In] IPin pin);
        /// <summary>
        /// Disconnects the specified pin.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        [PreserveSig]
        int Disconnect([In] IPin pin);
        /// <summary>
        /// Sets the default sync source.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int SetDefaultSyncSource();
        /// <summary>
        /// Connects the specified pin out.
        /// </summary>
        /// <param name="pinOut">The pin out.</param>
        /// <param name="pinIn">The pin in.</param>
        /// <returns></returns>
        [PreserveSig]
        int Connect([In] IPin pinOut, [In] IPin pinIn);
        /// <summary>
        /// Renders the specified pin out.
        /// </summary>
        /// <param name="pinOut">The pin out.</param>
        /// <returns></returns>
        [PreserveSig]
        int Render([In] IPin pinOut);
        /// <summary>
        /// Renders the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="playList">The play list.</param>
        /// <returns></returns>
        [PreserveSig]
        int RenderFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string file,
            [In, MarshalAs(UnmanagedType.LPWStr)] string playList);
        /// <summary>
        /// Adds the source filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [PreserveSig]
        int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.LPWStr)] string fileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string filterName,
            [Out] out IBaseFilter filter);
        /// <summary>
        /// Sets the log file.
        /// </summary>
        /// <param name="hFile">The h file.</param>
        /// <returns></returns>
        [PreserveSig]
        int SetLogFile(IntPtr hFile);
        /// <summary>
        /// Aborts this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Abort();
        /// <summary>
        /// Shoulds the operation continue.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int ShouldOperationContinue();
    }
}
