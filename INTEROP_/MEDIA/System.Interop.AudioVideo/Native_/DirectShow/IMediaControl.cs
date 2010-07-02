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
    /// IMediaControl
    /// </summary>
    [ComImport, Guid("56A868B1-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaControl
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Run();
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Pause();
        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Stop();
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <param name="filterState">State of the filter.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetState(int timeout, out int filterState);
        /// <summary>
        /// Renders the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        [PreserveSig]
        int RenderFile(string fileName);
        /// <summary>
        /// Adds the source filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filterInfo">The filter info.</param>
        /// <returns></returns>
        [PreserveSig]
        int AddSourceFilter([In] string fileName, [Out, MarshalAs(UnmanagedType.IDispatch)] out object filterInfo);
        /// <summary>
        /// Get_s the filter collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_FilterCollection([Out, MarshalAs(UnmanagedType.IDispatch)] out object collection);
        /// <summary>
        /// Get_s the reg filter collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [PreserveSig]
        int get_RegFilterCollection([Out, MarshalAs(UnmanagedType.IDispatch)] out object collection);
        /// <summary>
        /// Stops the when ready.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int StopWhenReady();
    }
}
