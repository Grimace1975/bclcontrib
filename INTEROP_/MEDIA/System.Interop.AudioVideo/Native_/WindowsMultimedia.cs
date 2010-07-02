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
    /// Wave native
    /// </summary>
    //+ [whole dll] http://www.koders.com/csharp/fidD708300B3B791B72849379A55DA564BA989FD16F.aspx?s=ShellExecute+SE_ERR_NOASSOC
    //+ [multimedia] http://www.cs.odu.edu/~wild/cs477/Fall97/mm1.htm
    //+ [example] http://64.124.13.3/projects/VMS/Sample/test.c
    //+ [forum] http://forums.microsoft.com/MSDN/ShowForum.aspx?ForumID=351&SiteID=1
    //+ http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2324729&SiteID=1
    internal class WindowsMultimedia
    {
        private const string WindowsMultimediaLibrary = "winmm.dll";

        // consts
        public const int MMSYSERR_NOERROR = 0; // no error
        public const int MM_WOM_OPEN = 0x3BB;
        public const int MM_WOM_CLOSE = 0x3BC;
        public const int MM_WOM_DONE = 0x3BD;
        public const int MM_WIM_OPEN = 0x3BE;
        public const int MM_WIM_CLOSE = 0x3BF;
        public const int MM_WIM_DATA = 0x3C0;
        public const int CALLBACK_FUNCTION = 0x00030000;    // dwCallback is a FARPROC
        public const int TIME_MS = 0x0001;  // time in milliseconds
        public const int TIME_SAMPLES = 0x0002;  // number of wave samples
        public const int TIME_BYTES = 0x0004;  // current byte offset

        // callbacks
        public delegate void WaveDelegate(IntPtr hdrvr, int uMsg, int dwUser, ref WaveHdr wavhdr, int dwParam2);

        // structs 
        [StructLayout(LayoutKind.Sequential)]
        public struct WaveHdr
        {
            public IntPtr lpData; // pointer to locked data buffer
            public int dwBufferLength; // length of data buffer
            public int dwBytesRecorded; // used for input only
            public IntPtr dwUser; // for client's use
            public int dwFlags; // assorted flags (see defines)
            public int dwLoops; // loop control counter
            public IntPtr lpNext; // PWaveHdr, reserved for driver
            public int reserved; // reserved for driver
        }


        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInGetErrorText(int err,
            [MarshalAs(UnmanagedType.LPStr, SizeConst = 1000)]
            byte[] lpText
        , int uSize);

        // WaveOut calls
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutGetNumDevs();
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutWrite(IntPtr hWaveOut, ref WaveHdr lpWaveOutHdr, int uSize);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutReset(IntPtr hWaveOut);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutClose(IntPtr hWaveOut);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutPause(IntPtr hWaveOut);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutRestart(IntPtr hWaveOut);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutGetPosition(IntPtr hWaveOut, out int lpInfo, int uSize);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutSetVolume(IntPtr hWaveOut, int dwVolume);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);

        // WaveIn calls
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInGetNumDevs();
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInAddBuffer(IntPtr hwi, ref WaveHdr pwh, int cbwh);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInClose(IntPtr hwi);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInOpen(out IntPtr phwi, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WaveHdr lpWaveInHdr, int uSize);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInReset(IntPtr hwi);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInStart(IntPtr hwi);
        [DllImport(WindowsMultimediaLibrary)]
        public static extern int waveInStop(IntPtr hwi);
    }
}
