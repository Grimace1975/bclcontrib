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
    /// IBaseFilter
    /// </summary>
    [ComImport, Guid("56A86895-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter
    {
        /// <summary>
        /// Gets the class ID.
        /// </summary>
        /// <param name="ClassID">The class ID.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetClassID([Out] out System.Guid ClassID);
        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Stop();
        /// <summary>
        /// Pauses this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Pause();
        /// <summary>
        /// Runs the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        [PreserveSig]
        int Run(long start);
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <param name="milliSecsTimeout">The milli secs timeout.</param>
        /// <param name="filterState">State of the filter.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetState(int milliSecsTimeout, [Out] out int filterState);
        /// <summary>
        /// Sets the sync source.
        /// </summary>
        /// <param name="clock">The clock.</param>
        /// <returns></returns>
        [PreserveSig]
        int SetSyncSource([In] IntPtr clock);
        /// <summary>
        /// Gets the sync source.
        /// </summary>
        /// <param name="clock">The clock.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetSyncSource([Out] out IntPtr clock);
        /// <summary>
        /// Enums the pins.
        /// </summary>
        /// <param name="enumPins">The enum pins.</param>
        /// <returns></returns>
        [PreserveSig]
        int EnumPins([Out] out IEnumPins enumPins);
        /// <summary>
        /// Finds the pin.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        [PreserveSig]
        int FindPin([In, MarshalAs(UnmanagedType.LPWStr)] string id, [Out] out IPin pin);
        /// <summary>
        /// Queries the filter info.
        /// </summary>
        /// <param name="filterInfo">The filter info.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryFilterInfo([Out] FilterInfo filterInfo);
        /// <summary>
        /// Joins the filter graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [PreserveSig]
        int JoinFilterGraph([In] IFilterGraph graph, [In, MarshalAs(UnmanagedType.LPWStr)] string name);
        /// <summary>
        /// Queries the vendor info.
        /// </summary>
        /// <param name="vendorInfo">The vendor info.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryVendorInfo([Out, MarshalAs(UnmanagedType.LPWStr)] out string vendorInfo);
    }
}
