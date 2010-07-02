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
namespace System.Interopt.AudioVideo.Native_
{
    /// <summary>
    /// Native waveformat
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class WaveFormat
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;

        /// <summary>
        /// Format types for native waveformat
        /// </summary>
        public enum FormatTag
        {
            Pcm = 1,
            Float = 3
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveFormat"/> class.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <param name="bits">The bits.</param>
        /// <param name="channels">The channels.</param>
        public WaveFormat(int rate, int bits, int channels)
        {
            wFormatTag = (short)FormatTag.Pcm;
            nChannels = (short)channels;
            nSamplesPerSec = rate;
            wBitsPerSample = (short)bits;
            cbSize = 0;
            //+
            nBlockAlign = (short)(channels * (bits / 8));
            nAvgBytesPerSec = nSamplesPerSec * nBlockAlign;
        }
    }
}
