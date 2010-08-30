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
    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    public class AMMediaType : IDisposable
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AMMediaType"/> is reclaimed by garbage collection.
        /// </summary>
        ~AMMediaType()
        {
            Dispose(false);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // remove me from the Finalization queue 
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (FormatSize != 0)
            {
                Marshal.FreeCoTaskMem(FormatPtr);
            }
            if (unkPtr != IntPtr.Zero)
            {
                Marshal.Release(unkPtr);
            }
        }

        /// <summary>
        /// MajorType
        /// </summary>
        public Guid MajorType;
        /// <summary>
        /// SubType
        /// </summary>
        public Guid SubType;
        /// <summary>
        /// FixedSizeSamples
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool FixedSizeSamples = true;
        /// <summary>
        /// TemporalCompression
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool TemporalCompression;
        /// <summary>
        /// SampleSize
        /// </summary>
        public int SampleSize = 1;
        /// <summary>
        /// FormatType
        /// </summary>
        public Guid FormatType;
        /// <summary>
        /// unkPtr
        /// </summary>
        public IntPtr unkPtr;
        /// <summary>
        /// FormatSize
        /// </summary>
        public int FormatSize;
        /// <summary>
        /// FormatPtr
        /// </summary>
        public IntPtr FormatPtr;
    }
}
