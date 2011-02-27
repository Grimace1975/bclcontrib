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
namespace System.Interop.MediaInfo.Native_
{
    /// <summary>
    /// MediaInfo
    /// </summary>
    internal class MediaInfo
    {
        private const string MediaInfoLibrary = "MediaInfo.dll";

        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Close")]
        internal static extern int Close(IntPtr handle);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Count_Get")]
        internal static extern int GetCount(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamKind streamKind, int streamId);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Delete")]
        internal static extern void Delete(IntPtr handle);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Get", CharSet = CharSet.Unicode)]
        internal static extern IntPtr __Get(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamKind streamKind, uint streamId, string parameter, [MarshalAs(UnmanagedType.U4)] InfoKind infoKind, [MarshalAs(UnmanagedType.U4)] InfoKind searchKind);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_GetI", CharSet = CharSet.Unicode)]
        internal static extern string GetI(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamKind streamKind, uint streamId, uint parameter, [MarshalAs(UnmanagedType.U4)] InfoKind infoKind);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Inform", CharSet = CharSet.Unicode)]
        internal static extern string Inform(IntPtr handle, [MarshalAs(UnmanagedType.U4)] uint reserved);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_New")]
        internal static extern IntPtr New();
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Open", CharSet = CharSet.Unicode)]
        internal static extern int Open(IntPtr handle, string fileName);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_Option", CharSet = CharSet.Unicode)]
        internal static extern string GetOption(IntPtr handle, string optionString, string value);
        [DllImport(MediaInfoLibrary, EntryPoint = "MediaInfo_State_Get")]
        internal static extern int GetState(IntPtr handle);

        internal static string Get(IntPtr handle, StreamKind streamKind, uint trackIndex, string nameOfParameter)
        {
            return Marshal.PtrToStringUni(__Get(handle, streamKind, trackIndex, nameOfParameter, InfoKind.Text, InfoKind.Name));
            //char* chPtr = (char*)MediaInfo.Get(handle, streamKind, trackIndex, nameOfParameter, InfoKind.Text, InfoKind.Name).ToPointer();
            //string str = "";
            //while (chPtr[0] != '\0')
            //{
            //    str = str + chPtr[0];
            //    chPtr++;
            //}
            //return str;
        }
    }
}
