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
    /// ISampleGrabber
    /// </summary>
    [ComImport, Guid("6B652FFF-11FE-4FCE-92AD-0266B5D7C78F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabber
    {
        /// <summary>
        /// Sets the one shot.
        /// </summary>
        /// <param name="oneShot">if set to <c>true</c> [one shot].</param>
        /// <returns></returns>
        [PreserveSig]
        int SetOneShot([In, MarshalAs(UnmanagedType.Bool)] bool oneShot);
        /// <summary>
        /// Sets the type of the media.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int SetMediaType([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Gets the type of the connected media.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetConnectedMediaType([Out, MarshalAs(UnmanagedType.LPStruct)] AMMediaType mediaType);
        /// <summary>
        /// Sets the buffer samples.
        /// </summary>
        /// <param name="bufferThem">if set to <c>true</c> [buffer them].</param>
        /// <returns></returns>
        [PreserveSig]
        int SetBufferSamples([In, MarshalAs(UnmanagedType.Bool)] bool bufferThem);
        /// <summary>
        /// Gets the current buffer.
        /// </summary>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetCurrentBuffer(ref int bufferSize, IntPtr buffer);
        /// <summary>
        /// Gets the current sample.
        /// </summary>
        /// <param name="sample">The sample.</param>
        /// <returns></returns>
        [PreserveSig]
        int GetCurrentSample(IntPtr sample);
        /// <summary>
        /// Sets the callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="whichMethodToCallback">The which method to callback.</param>
        /// <returns></returns>
        [PreserveSig]
        int SetCallback(ISampleGrabberCB callback, int whichMethodToCallback);
    }
}
