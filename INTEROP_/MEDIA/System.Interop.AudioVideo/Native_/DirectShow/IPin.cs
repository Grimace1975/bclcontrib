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
    /// IPin
    /// </summary>
    [ComImport, Guid("56A86891-0AD4-11CE-B03A-0020AF0BA770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPin
    {
        /// <summary>
        /// Connects the specified receive pin.
        /// </summary>
        /// <param name="receivePin">The receive pin.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int Connect([In] IPin receivePin, [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Receives the connection.
        /// </summary>
        /// <param name="receivePin">The receive pin.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int ReceiveConnection([In] IPin receivePin, [In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int Disconnect();
        /// <summary>
        /// Connecteds to.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        [PreserveSig]
        int ConnectedTo([Out] out IPin pin);
        /// <summary>
        /// Connections the type of the media.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int ConnectionMediaType([Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Queries the pin info.
        /// </summary>
        /// <param name="pinInfo">The pin info.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryPinInfo([Out, MarshalAs(UnmanagedType.LPStruct)] PinInfo pinInfo);
        /// <summary>
        /// Queries the direction.
        /// </summary>
        /// <param name="pinDirection">The pin direction.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryDirection(out PinDirection pinDirection);
        /// <summary>
        /// Queries the id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryId([Out, MarshalAs(UnmanagedType.LPWStr)] out string id);
        /// <summary>
        /// Queries the accept.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryAccept([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Enums the media types.
        /// </summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns></returns>
        [PreserveSig]
        int EnumMediaTypes(IntPtr enumerator);
        /// <summary>
        /// Queries the internal connections.
        /// </summary>
        /// <param name="apPin">The ap pin.</param>
        /// <param name="nPin">The n pin.</param>
        /// <returns></returns>
        [PreserveSig]
        int QueryInternalConnections(IntPtr apPin, [In, Out] ref int nPin);
        /// <summary>
        /// Ends the of stream.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int EndOfStream();
        [PreserveSig]
        int BeginFlush();
        /// <summary>
        /// Ends the flush.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        int EndFlush();
        /// <summary>
        /// News the segment.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <param name="rate">The rate.</param>
        /// <returns></returns>
        [PreserveSig]
        int NewSegment(long start, long stop, double rate);
    }
}
